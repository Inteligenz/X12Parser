namespace OopFactory.X12.Validation
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using OopFactory.X12.Parsing;
    using OopFactory.X12.Specifications;
    using OopFactory.X12.Specifications.Enumerations;
    using OopFactory.X12.Specifications.Finders;
    using OopFactory.X12.Specifications.Interfaces;
    using OopFactory.X12.Validation.Model;

    /// <summary>
    /// Provides X12 acknowledgment
    /// </summary>
    public class X12AcknowledgmentService
    {
        private readonly ISpecificationFinder specFinder;
        private readonly char[] ignoredChars;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="X12AcknowledgmentService"/> class
        /// </summary>
        /// <param name="specFinder">Specification finder for obtaining X12 parsing details</param>
        /// <param name="ignoredChars">Ignored characters in the X12 document</param>
        public X12AcknowledgmentService(ISpecificationFinder specFinder, char[] ignoredChars)
        {
            this.specFinder = specFinder;
            this.ignoredChars = ignoredChars;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X12AcknowledgmentService"/> class
        /// </summary>
        /// <param name="specFinder">Specification finder for obtaining X12 parsing details</param>
        public X12AcknowledgmentService(ISpecificationFinder specFinder)
            : this(specFinder, new char[] { })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X12AcknowledgmentService"/> class
        /// </summary>
        /// <param name="ignoredChars">Ignored characters in the X12 document</param>
        public X12AcknowledgmentService(char[] ignoredChars)
            : this(new SpecificationFinder(), ignoredChars)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X12AcknowledgmentService"/> class
        /// </summary>
        public X12AcknowledgmentService()
            : this(new SpecificationFinder(), new char[] { })
        {
        }

        /// <summary>
        /// Builds a collection of <see cref="FunctionalGroupResponse"/> objects from Transactions
        /// </summary>
        /// <param name="x12Stream">Stream containing X12 Transactions</param>
        /// <returns>Collection of <see cref="FunctionalGroupResponse"/> objects</returns>
        public List<FunctionalGroupResponse> AcknowledgeTransactions(Stream x12Stream)
        {
            return this.AcknowledgeTransactions(x12Stream, Encoding.UTF8);
        }

        /// <summary>
        /// Builds a collection of <see cref="FunctionalGroupResponse"/> objects from Transactions
        /// </summary>
        /// <param name="x12Stream">Stream containing X12 Transactions</param>
        /// <param name="encoding">Stream encoding for proper reading</param>
        /// <returns>Collection of <see cref="FunctionalGroupResponse"/> objects</returns>
        public virtual List<FunctionalGroupResponse> AcknowledgeTransactions(Stream x12Stream, Encoding encoding)
        {
            var responses = new Dictionary<string, FunctionalGroupResponse>();

            using (var reader = new X12StreamReader(x12Stream, encoding, this.ignoredChars))
            {
                X12FlatTransaction trans = reader.ReadNextTransaction();
                while (!string.IsNullOrEmpty(trans.Transactions.First()))
                {
                    string[] isaElements = reader.SplitSegment(trans.IsaSegment);
                    string[] gsElements = reader.SplitSegment(trans.GsSegment);
                    string functionalIdentifierCode = gsElements[1];
                    string groupControlNumber = gsElements[6];
                    string versionIdentifierCode = gsElements[8];

                    if (!responses.ContainsKey(groupControlNumber))
                    {
                        responses.Add(
                            groupControlNumber,
                            new FunctionalGroupResponse
                                {
                                    SenderIdQualifier = isaElements[5],
                                    SenderId = isaElements[6],
                                    FunctionalIdCode = functionalIdentifierCode,
                                    GroupControlNumber = groupControlNumber,
                                    VersionIdentifierCode = versionIdentifierCode
                                });
                    }
                    
                    TransactionSetResponse response = this.AcknowledgeTransaction(reader, functionalIdentifierCode, versionIdentifierCode, trans.Transactions[0]);
                    responses[groupControlNumber].TransactionSetResponses.Add(response);

                    trans = reader.ReadNextTransaction();
                }
            }

            return responses.Values.ToList();
        }

        /// <summary>
        /// Builds a <see cref="TransactionSetResponse"/> object from the provided stream
        /// </summary>
        /// <param name="reader">Stream to pull transaction set data from</param>
        /// <param name="functionalCode">Function group code to associate with transaction set</param>
        /// <param name="versionIdentifierCode">Specification version code</param>
        /// <param name="transaction">Transaction segment string to be parsed</param>
        /// <returns>Transaction set response whether the set is valid, and the segment data</returns>
        protected virtual TransactionSetResponse AcknowledgeTransaction(X12StreamReader reader, string functionalCode, string versionIdentifierCode, string transaction)
        {
            string[] transactionElements = reader.SplitSegment(transaction);
            var response = new TransactionSetResponse
            {
                TransactionSetIdentifierCode = transactionElements[1],
                TransactionSetControlNumber = transactionElements[2]
            };
            if (transactionElements.Length >= 4)
            {
                response.ImplementationConventionReference = transactionElements[3];
            }

            TransactionSpecification transactionSpec = this.specFinder.FindTransactionSpec(
                functionalCode,
                versionIdentifierCode,
                response.TransactionSetIdentifierCode);

            if (transactionSpec == null)
            {
                response.SyntaxErrorCodes.Add("1");
                response.AcknowledgmentCode = AcknowledgmentCode.R_Rejected;
                return response;
            }

            var containers = new Stack<ContainerInformation>();
            var transactionContainer = new ContainerInformation { Spec = transactionSpec };
            containers.Push(transactionContainer);
            var segmentInfos = new List<SegmentInformation>();

            string[] segments = transaction.Split(new[] { reader.Delimiters.SegmentTerminator }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < segments.Length; i++)
            {
                string[] elements = segments[i].Split(reader.Delimiters.ElementSeparator);
                var segmentInfo = new SegmentInformation { SegmentId = elements[0], SegmentPosition = i + 1, Elements = elements };
                segmentInfo.Spec = this.specFinder.FindSegmentSpec(versionIdentifierCode, segmentInfo.SegmentId);
                segmentInfos.Add(segmentInfo);

                ContainerInformation currentContainer = containers.Peek();

                switch (segmentInfo.SegmentId)
                {
                    case "ST":
                    case "SE":
                        segmentInfo.LoopId = string.Empty;
                        transactionContainer.Segments.Add(segmentInfo);
                        break;
                    case "HL":
                        string hloopNumber = segmentInfo.Elements[1];
                        string hloopParentNumber = segmentInfo.Elements[2];
                        string hloopLevelCode = segmentInfo.Elements[3];
                        HierarchicalLoopSpecification hloopSpec = transactionSpec.HierarchicalLoopSpecifications.FirstOrDefault(hls => hls.LevelCode == hloopLevelCode);
                        if (hloopSpec != null)
                        {
                            while (!(containers.Peek().Spec is TransactionSpecification))
                            {
                                if (containers.Peek().HLoopNumber == hloopParentNumber)
                                {
                                    break;
                                }

                                containers.Pop();
                            }

                            segmentInfo.LoopId = hloopSpec.LoopId;
                            var hloopContainer = new ContainerInformation { Spec = hloopSpec, HLoopNumber = hloopNumber };
                            hloopContainer.Segments.Add(segmentInfo);
                            containers.Peek().Containers.Add(hloopContainer);
                            containers.Push(hloopContainer);
                        }
                        else
                        {
                            response.SegmentErrors.Add(this.CreateDataElementError(segmentInfo, 3, "I6", hloopLevelCode));
                            response.AcknowledgmentCode = AcknowledgmentCode.X_Rejected_ContentCouldNotBeAnalyzed;
                        }

                        break;
                    default:
                        bool matchFound = false;
                        do
                        {
                            var matchingLoopSpecs = currentContainer.Spec.LoopSpecifications.Where(ls => ls.StartingSegment.SegmentId == segmentInfo.SegmentId).ToList();

                            if (matchingLoopSpecs.Count > 0)
                            {
                                IContainerSpecification matchingLoopSpec;
                                if (matchingLoopSpecs.Count == 1)
                                {
                                    matchingLoopSpec = matchingLoopSpecs.First();
                                }
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
                                    response.SegmentErrors.Add(this.CreateSegmentError(segmentInfo, "6"));
                                    response.AcknowledgmentCode = AcknowledgmentCode.X_Rejected_ContentCouldNotBeAnalyzed;
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
                                    response.SegmentErrors.Add(this.CreateSegmentError(segmentInfo, "2")); 
                                    response.AcknowledgmentCode = AcknowledgmentCode.X_Rejected_ContentCouldNotBeAnalyzed;
                                    return response; 
                                }

                                containers.Pop();
                                currentContainer = containers.Peek();
                            }
                        }
                        while (!matchFound);

                        break;
                }

                response.SegmentErrors.AddRange(this.ValidateSegmentAgainstSpec(segmentInfo));
            }

            response.SegmentErrors.AddRange(this.ValidateContainerAgainstSpec(transactionContainer));
            
            var trailerSegment = segmentInfos.FirstOrDefault(si => si.SegmentId == "SE");
            if (trailerSegment == null)
            {
                response.SyntaxErrorCodes.Add("2");
            }
            else
            {
                if (trailerSegment.Elements.Length <= 2 || trailerSegment.Elements[2] != response.TransactionSetControlNumber)
                {
                    response.SyntaxErrorCodes.Add("3");
                }

                if (trailerSegment.Elements.Length >= 2)
                {
                    int segmentCount;
                    int.TryParse(trailerSegment.Elements[1], out segmentCount);
                    if (segmentCount != segmentInfos.Count)
                    {
                        response.SyntaxErrorCodes.Add("4");
                    }
                }
                else
                {
                    response.SyntaxErrorCodes.Add("4");
                }
            }

            if (response.SegmentErrors.Count > 0 || response.SyntaxErrorCodes.Count > 0)
            {
                if (response.SegmentErrors.Count > 0)
                {
                    response.SyntaxErrorCodes.Add("5");
                }

                if (response.AcknowledgmentCode == AcknowledgmentCode.A_Accepted)
                {
                    response.AcknowledgmentCode = AcknowledgmentCode.E_Accepted_ButErrorsWereNoted;
                }
            }

            return response;
        }

        /// <summary>
        /// Attempts to parse a container and validates it againsts its specification. A collection of <see cref="SegmentError"/> objects is returned
        /// </summary>
        /// <param name="container">Object to be validated</param>
        /// <returns>Collection of Segment errors, if found</returns>
        protected virtual IEnumerable<SegmentError> ValidateContainerAgainstSpec(ContainerInformation container)
        {
            var errors = new List<SegmentError>();

            foreach (var segmentSpec in container.Spec.SegmentSpecifications.Where(ss => ss.Usage == Usage.Required))
            {
                if (!container.Segments.Exists(s => s.SegmentId == segmentSpec.SegmentId))
                {
                    errors.Add(this.CreateSegmentError(
                        new SegmentInformation
                        { 
                            SegmentId = segmentSpec.SegmentId,
                            LoopId = container.Spec.LoopId,
                            SegmentPosition = container.Segments.Count > 0 ? container.Segments.First().SegmentPosition : 0
                        },
                        "3"));
                }

                if (segmentSpec.Repeat > 0 && container.Segments.Count(s => s.SegmentId == segmentSpec.SegmentId) > segmentSpec.Repeat)
                {
                    errors.Add(
                        this.CreateSegmentError(
                            container.Segments.Last(s => s.SegmentId == segmentSpec.SegmentId),
                            "5"));
                }
            }

            foreach (var loopSpec in container.Spec.LoopSpecifications.Where(ls => ls.Usage == Usage.Required))
            {
                if (!container.Containers.Exists(c => c.Spec.LoopId == loopSpec.LoopId))
                {
                    errors.Add(
                        this.CreateSegmentError(
                            new SegmentInformation
                                {
                                    SegmentId = loopSpec.StartingSegment.SegmentId,
                                    LoopId = container.Spec.LoopId,
                                    SegmentPosition = container.Segments.Count > 0 ? container.Segments.Last().SegmentPosition : 0
                                },
                                "I7"));
                }

                if (loopSpec.LoopRepeat > 0 && container.Containers.Count(c => c.Spec.LoopId == loopSpec.LoopId) > loopSpec.LoopRepeat)
                {
                    errors.Add(
                        this.CreateSegmentError(
                            container.Containers.Last(c => c.Spec.LoopId == loopSpec.LoopId).Segments.First(),
                            "4"));
                }
            }

            foreach (var childContainer in container.Containers)
            {
                errors.AddRange(this.ValidateContainerAgainstSpec(childContainer));
            }

            return errors;
        }

        /// <summary>
        /// Attempts to parse a segment and validates it againsts its specification. A collection of <see cref="SegmentError"/> objects is returned
        /// </summary>
        /// <param name="segmentInfo">Segment metadata to be validated</param>
        /// <returns>Collection of Segment errors, if found</returns>
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

                        if (string.IsNullOrEmpty(element) && elementSpec.Required)
                        {
                            errors.Add(this.CreateDataElementError(segmentInfo, iSpec + 1, "1", null));
                        }
                        else if (element.Length < elementSpec.MinLength && (elementSpec.Required || element.Length > 0))
                        {
                            errors.Add(this.CreateDataElementError(segmentInfo, iSpec + 1, "4", element));
                        }
                        else if (element.Length > elementSpec.MaxLength && elementSpec.MaxLength > 0)
                        {
                            errors.Add(this.CreateDataElementError(segmentInfo, iSpec + 1, "5", element));
                        }
                    }
                    else
                    {
                        if (elementSpec.Required)
                        {
                            errors.Add(this.CreateDataElementError(segmentInfo, iSpec + 1, "1", null));
                        }
                    }
                }

                if (segmentInfo.Elements.Length - 1 > segmentInfo.Spec.Elements.Count)
                {
                    int elementPosition = segmentInfo.Spec.Elements.Count + 1;
                    errors.Add(this.CreateDataElementError(segmentInfo, elementPosition, "3", segmentInfo.Elements[elementPosition]));
                }
            }

            return errors;            
        }

        /// <summary>
        /// Creates a <see cref="SegmentError"/> object with the segment metadata provided
        /// </summary>
        /// <param name="segmentInfo">Segment metadata object</param>
        /// <param name="syntaxErrorCode">Error code detailing syntax issue</param>
        /// <returns>Error object created with the metadata</returns>
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

        /// <summary>
        /// Creates a <see cref="SegmentError"/> object with the segment metadata provided
        /// </summary>
        /// <param name="segmentInfo">Segment metadata object</param>
        /// <param name="elementPositionInSegment">Element index position in segment</param>
        /// <param name="syntaxErrorCode">Error code detailing syntax issue</param>
        /// <param name="element">Element data</param>
        /// <returns>Error object created with the metadata</returns>
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
