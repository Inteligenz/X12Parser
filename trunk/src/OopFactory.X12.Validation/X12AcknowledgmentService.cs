using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Model.Typed;
using OopFactory.X12.Validation.Model;

namespace OopFactory.X12.Validation
{
    public class X12AcknowledgmentService
    {
        ISpecificationFinder _specFinder;
        IControlNumberGenerator _controlNumberGenerator;

        public X12AcknowledgmentService(ISpecificationFinder specFinder, IControlNumberGenerator controlNumberGenerator)
        {
            _specFinder = specFinder;
            _controlNumberGenerator = controlNumberGenerator;
        }

        public X12AcknowledgmentService()
            : this(new SpecificationFinder(), new ControlNumberSequencer())
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
                    var response = AcknowledgeTransaction(reader, trans.Transactions[0]);
                    groupResponse.TransactionSetResponses.Add(response);

                    ValidateSegments(groupResponse, response, reader, trans.Transactions[0]);
                    trans = reader.ReadNextTransaction();
                }
                
            }

            return responses.Values.ToList();
        }

        protected virtual TransactionSetResponse AcknowledgeTransaction(X12StreamReader reader, string transaction)
        {
            string[] stElements = reader.SplitSegment(transaction);
            var response = new TransactionSetResponse
            {
                TransactionSetIdentifierCode = stElements[1],
                TransactionSetControlNumber = stElements[2]
            };
            if (stElements.Length >= 4)
                response.ImplementationConventionReference = stElements[3];

            // TODO:  Add error checking against specification here

            return response;
        }

        protected virtual void ValidateSegments(FunctionalGroupResponse groupResponse, TransactionSetResponse response, X12StreamReader reader, string transaction)
        {
            string[] segments = transaction.Split(reader.Delimiters.SegmentTerminator);

            var spec = _specFinder.FindTransactionSpec(groupResponse.FunctionalIdCode, groupResponse.VersionIdentifierCode, response.TransactionSetIdentifierCode);
        }

        public void Add999Transaction(FunctionGroup group, IEnumerable<FunctionalGroupResponse> groupResponses)
        {
            _controlNumberGenerator.Reset();

            foreach (var groupResponse in groupResponses)
            {
                var trans = group.AddTransaction("999", _controlNumberGenerator.GetNextControlNumber());
                if (group.VersionIdentifierCode.Contains("5010"))
                    trans.SetElement(3, group.VersionIdentifierCode);

                // Functional group response header
                var ak1 = trans.AddSegment<TypedSegmentAK1>(new TypedSegmentAK1());
                ak1.AK101_FunctionalIdCode = groupResponse.FunctionalIdCode;
                ak1.AK102_GroupControlNumber = groupResponse.GroupControlNumber;
                ak1.AK103_VersionIdentifierCode = groupResponse.VersionIdentifierCode;

                foreach (var response in groupResponse.TransactionSetResponses)
                {
                    // Transaction Set Response Header
                    var ak2 = trans.AddLoop<TypedLoopAK2>(new TypedLoopAK2());
                    ak2.AK201_TransactionSetIdentifierCode = response.TransactionSetIdentifierCode;
                    ak2.AK202_TransactionSetControlNumber = response.TransactionSetControlNumber;
                    if (!string.IsNullOrEmpty(response.ImplementationConventionReference))
                        ak2.AK203_ImplementationConventionReference = response.ImplementationConventionReference;



                    

                    // Transaction Set Response Trailer
                    var ik5 = ak2.AddSegment<TypedSegmentIK5>(new TypedSegmentIK5());
                    ik5.IK501_TransactionSetAcknowledgmentCode = response.AcknowledgmentCode.ToString().Substring(0, 1);

                    if (response.SyntaxErrorCodes.Count > 0)
                        ik5.IK502_SyntaxErrorCode = response.SyntaxErrorCodes[0];
                    if (response.SyntaxErrorCodes.Count > 1)
                        ik5.IK503_SyntaxErrorCode = response.SyntaxErrorCodes[1];
                    if (response.SyntaxErrorCodes.Count > 2)
                        ik5.IK504_SyntaxErrorCode = response.SyntaxErrorCodes[2];
                    if (response.SyntaxErrorCodes.Count > 3)
                        ik5.IK505_SyntaxErrorCode = response.SyntaxErrorCodes[3];
                    if (response.SyntaxErrorCodes.Count > 4)
                        ik5.IK506_SyntaxErrorCode = response.SyntaxErrorCodes[4];
                }

                // Functional group response trailer
                var ak9 = trans.AddSegment<TypedSegmentAK9>(new TypedSegmentAK9());
                ak9.AK901_FunctionalGroupAcknowledgeCode = groupResponse.AcknowledgmentCode.ToString().Substring(0, 1);
                ak9.AK902_NumberOfTransactionSetsIncluded = groupResponse.TransactionSetResponses.Count;
                ak9.AK903_NumberOfReceivedTransactionSets = groupResponse.TransactionSetResponses.Count;
                ak9.AK904_NumberOfAcceptedTransactionSets = groupResponse.TransactionSetResponses.Where(tsr => tsr.AcknowledgmentCode == AcknowledgmentCodeEnum.A_Accepted || tsr.AcknowledgmentCode == AcknowledgmentCodeEnum.E_Accepted_ButErrorsWereNoted).Count();

                if (groupResponse.SyntaxErrorCodes.Count > 0)
                    ak9.AK905_FunctionalGroupSyntaxErrorCode = groupResponse.SyntaxErrorCodes[0];
                if (groupResponse.SyntaxErrorCodes.Count > 1)
                    ak9.AK906_FunctionalGroupSyntaxErrorCode = groupResponse.SyntaxErrorCodes[1];
                if (groupResponse.SyntaxErrorCodes.Count > 2)
                    ak9.AK907_FunctionalGroupSyntaxErrorCode = groupResponse.SyntaxErrorCodes[2];
                if (groupResponse.SyntaxErrorCodes.Count > 3)
                    ak9.AK908_FunctionalGroupSyntaxErrorCode = groupResponse.SyntaxErrorCodes[3];
                if (groupResponse.SyntaxErrorCodes.Count > 4)
                    ak9.AK909_FunctionalGroupSyntaxErrorCode = groupResponse.SyntaxErrorCodes[4];

            }
        }
    }
}
