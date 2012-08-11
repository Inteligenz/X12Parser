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
       
        public X12AcknowledgmentService(ISpecificationFinder specFinder)
        {
            _specFinder = specFinder;
        }

        public X12AcknowledgmentService()
            : this(new SpecificationFinder())
        {
        }

        public List<FunctionalGroupResponse> AcknowledgeTransactions(Stream x12Stream)
        {
            return AcknowledgeTransactions(x12Stream, Encoding.UTF8);
        }        

        public virtual List<FunctionalGroupResponse> AcknowledgeTransactions(Stream x12Stream, Encoding encoding)
        {
            var responses = new Dictionary<string,FunctionalGroupResponse>();

            using (var reader = new X12StreamReader(x12Stream, encoding))
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
                    var response = AcknowledgeTransaction(reader, versionIdentifierCode, trans.Transactions[0]);
                    groupResponse.TransactionSetResponses.Add(response);

                    trans = reader.ReadNextTransaction();
                }
                
            }

            return responses.Values.ToList();
        }

        protected virtual TransactionSetResponse AcknowledgeTransaction(X12StreamReader reader, string versionIdentifierCode, string transaction)
        {
            string[] stElements = reader.SplitSegment(transaction);
            var response = new TransactionSetResponse
            {
                TransactionSetIdentifierCode = stElements[1],
                TransactionSetControlNumber = stElements[2]
            };
            if (stElements.Length >= 4)
                response.ImplementationConventionReference = stElements[3];

            string[] segments = transaction.Split(reader.Delimiters.SegmentTerminator);

            for (int i = 0; i < segments.Length; i++)
            {
                string[] elements = segments[i].Split(reader.Delimiters.ElementSeparator);
                var segmentSpec = _specFinder.FindSegmentSpec(versionIdentifierCode, elements[0]);

                response.SegmentErrors.AddRange(ValidateSegmentAgainstSpec(segmentSpec, elements, i + 1));

            }

            // TODO:  Add error checking against specification here

            return response;
        }

        protected virtual IList<SegmentError> ValidateSegmentAgainstSpec(SegmentSpecification segmentSpec, string[] elements, int segmentPosition)
        {
            var errors = new List<SegmentError>();
            if (segmentSpec != null)
            {
                for (int iSpec = 0; iSpec < segmentSpec.Elements.Count; iSpec++)
                {
                    var elementSpec = segmentSpec.Elements[iSpec];

                    if (iSpec < elements.Length - 1)
                    {
                        string element = elements[iSpec + 1];

                        if (element == "" && elementSpec.Required)
                            errors.Add(CreateDataElementError(elements[0], segmentPosition, iSpec + 1, "1", null));
                        else if (element.Length < elementSpec.MinLength && (elementSpec.Required || element.Length > 0))
                        {
                            errors.Add(CreateDataElementError(elements[0], segmentPosition, iSpec + 1, "4", element));
                        }
                        else if (element.Length > elementSpec.MaxLength && elementSpec.MaxLength > 0)
                        {
                            errors.Add(CreateDataElementError(elements[0], segmentPosition, iSpec + 1, "5", element));
                        }
                        
                    }
                    else
                    {
                        if (elementSpec.Required) // required element is missing from segment
                            errors.Add(CreateDataElementError(elements[0], segmentPosition, iSpec + 1, "1", null));

                    }
                }

                if (elements.Length - 1 > segmentSpec.Elements.Count)
                {
                    int elementPosition = segmentSpec.Elements.Count + 1;
                    errors.Add(CreateDataElementError(elements[0], segmentPosition, elementPosition, "3", elements[elementPosition]));
                }
            }
            return errors;            
        }

        private SegmentError CreateDataElementError(string segmentId, int segmentPosition, int elementPositionInSegment, string syntaxErrorCode, string element)
        {
            var error = new SegmentError
            {
                SegmentIdCode = segmentId,
                SegmentPosition = segmentPosition,
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
