using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Model.Typed;
using OopFactory.X12.Parsing.Specification;
using OopFactory.X12.Validation.Model;

namespace OopFactory.X12.Validation
{
    public class X12AcknowledgmentService
    {
        ISpecificationFinder _specFinder;
        private char[] _ignoredChars;
                
        public X12AcknowledgmentService(ISpecificationFinder specFinder, char[] ignoredChars)
        {
            _specFinder = specFinder;
            _ignoredChars = ignoredChars;
        }

        public X12AcknowledgmentService(ISpecificationFinder specFinder) : this(specFinder, new char[] { }) { }

        public X12AcknowledgmentService(char[] ignoredChars) : this(new SpecificationFinder(), ignoredChars) { }

        public X12AcknowledgmentService() : this(new SpecificationFinder(), new char[] { }) { }

        public List<FunctionalGroupResponse> AcknowledgeTransactions(Stream x12Stream)
        {
            return AcknowledgeTransactions(x12Stream, Encoding.UTF8);
        }        

        public virtual List<FunctionalGroupResponse> AcknowledgeTransactions(Stream x12Stream, Encoding encoding)
        {
            var responses = new Dictionary<string,FunctionalGroupResponse>();

            using (var reader = new X12StreamReader(x12Stream, encoding, _ignoredChars))
            {
                var trans = reader.ReadNextTransaction();
                while (!string.IsNullOrEmpty(trans.Transactions.First()))
                {
                    string[] isaElements = reader.SplitSegment(trans.IsaSegment);
                    string[] gsElements = reader.SplitSegment(trans.GsSegment);
                    string functionalIdentifierCode = gsElements[1];
                    string groupControlNumber = gsElements[6];
                    string versionIdentifierCode = gsElements[8];

                    if (!responses.ContainsKey(groupControlNumber))
                    {
                        responses.Add(groupControlNumber, new FunctionalGroupResponse
                        {
                            SenderIdQualifier = isaElements[5],
                            SenderId = isaElements[6],
                            FunctionalIdCode = functionalIdentifierCode,
                            GroupControlNumber = groupControlNumber,
                            VersionIdentifierCode = versionIdentifierCode
                        });
                    }
                    var groupResponse = responses[groupControlNumber];
                    var response = AcknowledgeTransaction(reader, functionalIdentifierCode, versionIdentifierCode, trans.Transactions[0]);
                    groupResponse.TransactionSetResponses.Add(response);

                    trans = reader.ReadNextTransaction();
                }
                
            }

            return responses.Values.ToList();
        }

        protected virtual TransactionSetResponse AcknowledgeTransaction(X12StreamReader reader, string functionalCode, string versionIdentifierCode, string transaction)
        {
            string[] stElements = reader.SplitSegment(transaction);
            var response = new TransactionSetResponse
            {
                TransactionSetIdentifierCode = stElements[1],
                TransactionSetControlNumber = stElements[2]
            };
            if (stElements.Length >= 4)
                response.ImplementationConventionReference = stElements[3];

            var transactionSpec = _specFinder.FindTransactionSpec(functionalCode, versionIdentifierCode, response.TransactionSetIdentifierCode);

            if (transactionSpec == null)
            {
                response.SyntaxErrorCodes.Add("1"); // Transaction Set Not Supported
                response.AcknowledgmentCode = AcknowledgmentCodeEnum.R_Rejected;
                return response;
            }

            #region Validate against transaction specification
            Stack<ContainerInformation> containers = new Stack<ContainerInformation>();
            var transactionContainer = new ContainerInformation { Spec = transactionSpec };
            containers.Push(transactionContainer);

            List<SegmentInformation> segmentInfos = new List<SegmentInformation>();
            string[] segments = transaction.Split(new char[] { reader.Delimiters.SegmentTerminator }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < segments.Length; i++)
            {
                string[] elements = segments[i].Split(reader.Delimiters.ElementSeparator);
                var segmentInfo = new SegmentInformation { SegmentId = elements[0], SegmentPosition = i + 1, Elements = elements };
                segmentInfo.Spec = _specFinder.FindSegmentSpec(versionIdentifierCode, segmentInfo.SegmentId);
                segmentInfos.Add(segmentInfo);

                ContainerInformation currentContainer = containers.Peek();

                switch (segmentInfo.SegmentId)
                {
                    case "ST":
                    case "SE":
                        segmentInfo.LoopId = "";
                        transactionContainer.Segments.Add(segmentInfo);
                        break;
                    case "HL":
                        string hlNumber = segmentInfo.Elements[1];
                        string hlParentNumber = segmentInfo.Elements[2];
                        string hlLevelCode = segmentInfo.Elements[3];
                        var hlSpec = transactionSpec.HierarchicalLoopSpecifications.FirstOrDefault(hls => hls.LevelCode == hlLevelCode);
                        if (hlSpec != null)
                        {
                            while (!(containers.Peek().Spec is TransactionSpecification))
                            {
                                if (containers.Peek().HLoopNumber == hlParentNumber)
                                    break;
                                containers.Pop();
                            }
                            segmentInfo.LoopId = hlSpec.LoopId;
                            var hlContainer = new ContainerInformation { Spec = hlSpec, HLoopNumber = hlNumber };
                            hlContainer.Segments.Add(segmentInfo);
                            containers.Peek().Containers.Add(hlContainer);
                            containers.Push(hlContainer);
                        }
                        else
                        {
                            response.SegmentErrors.Add(CreateDataElementError(segmentInfo, 3, "I6", hlLevelCode)); //Code Value Not Used in Implementation
                            response.AcknowledgmentCode = AcknowledgmentCodeEnum.X_Rejected_ContentCouldNotBeAnalyzed;
                            return response; // end validation if HL cannot be recognized since hl spec will not be available
                        }
                        break;
                    default:
                        bool matchFound = false;
                        do
                        {
                            var matchingLoopSpecs = currentContainer.Spec.LoopSpecifications.Where(ls => ls.StartingSegment.SegmentId == segmentInfo.SegmentId).ToList();

                            if (matchingLoopSpecs.Count > 0)
                            {
                                IContainerSpecification matchingLoopSpec = null;
                                if (matchingLoopSpecs.Count == 1)
                                    matchingLoopSpec = matchingLoopSpecs.First();
                                else
                                {
                                    string entityCode = elements[1];
                                    matchingLoopSpec = matchingLoopSpecs.FirstOrDefault(ls => ls.StartingSegment.EntityIdentifiers.Exists(ei => ei.Code == entityCode));
                                }

                                if (matchingLoopSpec != null)
                                {
                                    segmentInfo.LoopId = matchingLoopSpec.LoopId;
                                    var loopContainer = new ContainerInformation { Spec = matchingLoopSpec };
                                    loopContainer.Segments.Add(segmentInfo);
                                    containers.Peek().Containers.Add(loopContainer);
                                    containers.Push(loopContainer);
                                    matchFound = true;
                                }
                                else
                                {
                                    response.SegmentErrors.Add(CreateSegmentError(segmentInfo, "6")); //Segment Not in Defined Transaction Set
                                    response.AcknowledgmentCode = AcknowledgmentCodeEnum.X_Rejected_ContentCouldNotBeAnalyzed;
                                    return response;
                                }
                            }
                            else if (currentContainer.Spec.SegmentSpecifications.Exists(ss => ss.SegmentId == segmentInfo.SegmentId))
                            {
                                segmentInfo.LoopId = currentContainer.Spec.LoopId;
                                currentContainer.Segments.Add(segmentInfo);
                                matchFound = true;
                            }
                            else
                            {
                                if (currentContainer.Spec is TransactionSpecification)
                                {
                                    response.SegmentErrors.Add(CreateSegmentError(segmentInfo, "2")); // Unexpected segment
                                    response.AcknowledgmentCode = AcknowledgmentCodeEnum.X_Rejected_ContentCouldNotBeAnalyzed;
                                    return response; // end validation if unrecognized segment encountered (cannot guarantee we are pointing at correct container)
                                }
                                else
                                {
                                    containers.Pop();
                                    currentContainer = containers.Peek();
                                }
                            }
                        } while (!matchFound);
                        break;
                }
                response.SegmentErrors.AddRange(ValidateSegmentAgainstSpec(segmentInfo));
            }

            response.SegmentErrors.AddRange(ValidateContainerAgainstSpec(transactionContainer));

            #endregion

            #region Validate transaction trailer
            var trailerSegment = segmentInfos.FirstOrDefault(si => si.SegmentId == "SE");
            if (trailerSegment == null)
            {
                response.SyntaxErrorCodes.Add("2"); //Transaction Set Trailer Missing
            }
            else
            {
                if (trailerSegment.Elements.Length <= 2 || trailerSegment.Elements[2] != response.TransactionSetControlNumber)
                    response.SyntaxErrorCodes.Add("3"); // Transaction Set Control Number in Header and Trailer Do Not Match

                if (trailerSegment.Elements.Length >= 2)
                {
                    int segmentCount;
                    int.TryParse(trailerSegment.Elements[1], out segmentCount);
                    if (segmentCount != segmentInfos.Count)
                        response.SyntaxErrorCodes.Add("4"); // Number of Included Segments Does Not Match Actual Count
                }
                else
                    response.SyntaxErrorCodes.Add("4"); // Number of Included Segments Does Not Match Actual Count
            }

            #endregion

            if (response.SegmentErrors.Count > 0 || response.SyntaxErrorCodes.Count > 0)
            {
                if (response.SegmentErrors.Count > 0)
                    response.SyntaxErrorCodes.Add("5"); //One or More Segments in Error
                if (response.AcknowledgmentCode == AcknowledgmentCodeEnum.A_Accepted)
                    response.AcknowledgmentCode = AcknowledgmentCodeEnum.E_Accepted_ButErrorsWereNoted;
            }
            return response;
        }

        protected virtual IEnumerable<SegmentError> ValidateContainerAgainstSpec(ContainerInformation container)
        {
            var errors = new List<SegmentError>();

            foreach (var segmentSpec in container.Spec.SegmentSpecifications.Where(ss => ss.Usage == UsageEnum.Required))
            {
                if (!container.Segments.Exists(s => s.SegmentId == segmentSpec.SegmentId))
                {
                    errors.Add(CreateSegmentError(new SegmentInformation {
                        SegmentId = segmentSpec.SegmentId,
                        LoopId = container.Spec.LoopId,
                        SegmentPosition = container.Segments.Count > 0 ? container.Segments.First().SegmentPosition : 0},
                        "3")); // Required segment is missing
                }

                if (segmentSpec.Repeat > 0 && container.Segments.Count(s => s.SegmentId == segmentSpec.SegmentId) > segmentSpec.Repeat)
                {
                    errors.Add(CreateSegmentError(container.Segments.Last(s=>s.SegmentId == segmentSpec.SegmentId),
                        "5")); // Segment Exceeds Maximum Use
                }
            }

            foreach (var loopSpec in container.Spec.LoopSpecifications.Where(ls => ls.Usage == UsageEnum.Required))
            {
                if (!container.Containers.Exists(c => c.Spec.LoopId == loopSpec.LoopId))
                {
                    errors.Add(CreateSegmentError(new SegmentInformation
                    {
                        SegmentId = loopSpec.StartingSegment.SegmentId,
                        LoopId = container.Spec.LoopId,
                        SegmentPosition = container.Segments.Count > 0 ? container.Segments.Last().SegmentPosition : 0
                    }, "I7")); // Implementation Loop Occurs Under Minimum Times
                }

                if (loopSpec.LoopRepeat > 0 && container.Containers.Count(c => c.Spec.LoopId == loopSpec.LoopId) > loopSpec.LoopRepeat)
                {
                    errors.Add(CreateSegmentError(container.Containers.Last(c=>c.Spec.LoopId == loopSpec.LoopId).Segments.First(),
                        "4")); // Loop Occurs Over Maximum Times
                }
            }

            foreach (var childContainer in container.Containers)
            {
                errors.AddRange(ValidateContainerAgainstSpec(childContainer));
            }
            return errors;
        }

        protected virtual IList<SegmentError> ValidateSegmentAgainstSpec(SegmentInformation segmentInfo)
        {
            var errors = new List<SegmentError>();
            if (segmentInfo.Spec != null)
            {
                for (int iSpec = 0; iSpec < segmentInfo.Spec.Elements.Count; iSpec++)
                {
                    var elementSpec = segmentInfo.Spec.Elements[iSpec];

                    if (iSpec < segmentInfo.Elements.Length - 1)
                    {
                        string element = segmentInfo.Elements[iSpec + 1];

                        if (element == "" && elementSpec.Required)
                            errors.Add(CreateDataElementError(segmentInfo, iSpec + 1, "1", null));
                        else if (element.Length < elementSpec.MinLength && (elementSpec.Required || element.Length > 0))
                        {
                            errors.Add(CreateDataElementError(segmentInfo, iSpec + 1, "4", element));
                        }
                        else if (element.Length > elementSpec.MaxLength && elementSpec.MaxLength > 0)
                        {
                            errors.Add(CreateDataElementError(segmentInfo, iSpec + 1, "5", element));
                        }
                        
                    }
                    else
                    {
                        if (elementSpec.Required) // required element is missing from segment
                            errors.Add(CreateDataElementError(segmentInfo, iSpec + 1, "1", null));

                    }
                }

                if (segmentInfo.Elements.Length - 1 > segmentInfo.Spec.Elements.Count)
                {
                    int elementPosition = segmentInfo.Spec.Elements.Count + 1;
                    errors.Add(CreateDataElementError(segmentInfo, elementPosition, "3", segmentInfo.Elements[elementPosition]));
                }
            }
            return errors;            
        }

        protected SegmentError CreateSegmentError(SegmentInformation segmentInfo, string syntaxErrorCode)
        {
            return new SegmentError
            {
                SegmentIdCode = segmentInfo.SegmentId,
                SegmentPosition = segmentInfo.SegmentPosition,
                LoopIdentifierCode = segmentInfo.LoopId,
                ImplementationSegmentSyntaxErrorCode = syntaxErrorCode
            };
        }

        protected SegmentError CreateDataElementError(SegmentInformation segmentInfo, int elementPositionInSegment, string syntaxErrorCode, string element)
        {
            var error = new SegmentError
            {
                SegmentIdCode = segmentInfo.SegmentId,
                SegmentPosition = segmentInfo.SegmentPosition,
                LoopIdentifierCode = segmentInfo.LoopId,
                ImplementationSegmentSyntaxErrorCode = "8"
            };
            error.ElementNotes.Add(new DataElementNote
            {
                PositionInSegment = new PositionInSegment { ElementPositionInSegment = elementPositionInSegment },
                SyntaxErrorCode = syntaxErrorCode,
                CopyOfBadElement = element
            });
            return error;
        }
    }
}
