using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum AmountQualifierCode
    {
        [EDIFieldValue("F5")]
        PatientAmountPaid,

        [EDIFieldValue("D")]
        PayorAmountPaid,

        [EDIFieldValue("A8")]
        NoncoveredCharges_Actual,

        [EDIFieldValue("EAF")]
        AmountOwed,

        [EDIFieldValue("T")]
        Tax,

        [EDIFieldValue("F4")]
        PostageClaimed,

        [EDIFieldValue("B6")]
        AllowedAmount_Actual,

        [EDIFieldValue("T3")]
        TotalSubmittedCharges,

        [EDIFieldValue("N1")]
        NetWorth,
    }
}
