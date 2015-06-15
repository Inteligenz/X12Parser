using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum QuantityQualifier
    {
        [EDIFieldValue("99")]
        QuantityUsed,
        [EDIFieldValue("CA")]
        Covered_Actual,
        [EDIFieldValue("CE")]
        Covered_Estimated,
        [EDIFieldValue("DB")]
        DeductibleBloodUnits,
        [EDIFieldValue("DY")]
        Days,
        [EDIFieldValue("HS")]
        Hours,
        [EDIFieldValue("LA")]
        Life_timeReserve_Actual,
        [EDIFieldValue("LE")]
        Life_timeReserve_Estimated,
        [EDIFieldValue("MN")]
        Month,
        [EDIFieldValue("P6")]
        NumberofServicesorProcedures,
        [EDIFieldValue("QA")]
        QuantityApproved,
        [EDIFieldValue("S7")]
        Age_HighValue,
        [EDIFieldValue("S8")]
        Age_LowValue,
        [EDIFieldValue("VS")]
        Visits,
        [EDIFieldValue("YY")]
        Years,
    }
}
