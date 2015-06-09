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
    public static class X12Extensions
    {
        public static void Add999Transaction(this FunctionGroup group, IEnumerable<FunctionalGroupResponse> groupResponses)
        {
            int transactionId = 0;
            
            foreach (var groupResponse in groupResponses)
            {
                var trans = group.AddTransaction("999", string.Format("{0:0000}", ++transactionId));
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


                    foreach (var segmentError in response.SegmentErrors.OrderBy(se => se.SegmentPosition))
                    {
                        var ik3 = ak2.AddLoop<TypedLoopIK3>(new TypedLoopIK3());
                        ik3.IK301_SegmentIdCode = segmentError.SegmentIdCode;
                        ik3.IK302_SegmentPositionInTransactionSet = segmentError.SegmentPosition;
                        if (segmentError.LoopIdentifierCode != null)
                            ik3.IK303_LoopIdentifierCode = segmentError.LoopIdentifierCode;
                        if (segmentError.ImplementationSegmentSyntaxErrorCode != null)
                            ik3.IK304_SyntaxErrorCode = segmentError.ImplementationSegmentSyntaxErrorCode;

                        foreach (var context in segmentError.ContextErrors)
                        {
                            var ctx = ik3.AddSegment<TypedSegmentCTX>(new TypedSegmentCTX());
                            ctx.CTX01._1_ContextName = "SITUATIONAL TRIGGER";
                            ctx.CTX01._2_ContextReference = context.IdentificationReference;
                            ctx.CTX02_SegmentIdCode = context.SegmentIdCode;
                            ctx.CTX03_SegmentPositionInTransactionSet = context.SegmentPositionInTransactionSet;
                            ctx.CTX04_LoopIdentifierCode = context.LoopIdentifierCode;
                        }

                        foreach (var elementNote in segmentError.ElementNotes)
                        {
                            var ik4 = ik3.AddLoop<TypedLoopIK4>(new TypedLoopIK4());
                            ik4.IK401._1_ElementPositionInSegment = elementNote.PositionInSegment.ElementPositionInSegment;
                            ik4.IK401._2_ComponentDataElementPositionInComposite = elementNote.PositionInSegment.ComponentDataElementPositionInComposite;
                            ik4.IK401._3_RepeatingDataElementPosition = elementNote.PositionInSegment.RepeatingDataElementPosition;
                            if (elementNote.DataElementReferenceNumber != null)
                                ik4.IK402_DataElementReferenceNumber = elementNote.DataElementReferenceNumber;
                            ik4.IK403_SyntaxErrorCode = elementNote.SyntaxErrorCode;
                            if (elementNote.CopyOfBadElement != null)
                                ik4.IK404_CopyOfBaDataElement = elementNote.CopyOfBadElement;

                            foreach (var context in elementNote.ContextErrors)
                            {
                                var ctx = ik4.AddSegment<TypedSegmentCTX>(new TypedSegmentCTX());
                                ctx.CTX01._1_ContextName = "SITUATIONAL TRIGGER";
                                ctx.CTX01._2_ContextReference = context.IdentificationReference;
                                ctx.CTX02_SegmentIdCode = context.SegmentIdCode;
                                ctx.CTX03_SegmentPositionInTransactionSet = context.SegmentPositionInTransactionSet;
                                ctx.CTX04_LoopIdentifierCode = context.LoopIdentifierCode;
                            }
                        }
                    }

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
