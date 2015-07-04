using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum ClaimAdjustmentGroupCodes
    {
        [EDIFieldValue("CO")]
        ContractualObligations,
        [EDIFieldValue("CR")]
        CorrectionAndReversals,
        [EDIFieldValue("OA")]
        OtherAdjustments,
        [EDIFieldValue("PI")]
        PayorInitiatedReductions,
        [EDIFieldValue("PR")]
        PatientResponsibility,
    }
}
