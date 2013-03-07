using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Attributes;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public enum AllowanceOrChargeIndicator
    {
        [EDIFieldValue("A")]
        Allowance,
        [EDIFieldValue("C")]
        Charge,
        [EDIFieldValue("N")]
        NoAllowanceOrCharge,
        [EDIFieldValue("P")]
        Promotion,
        [EDIFieldValue("Q")]
        ChargeRequest,
        [EDIFieldValue("R")]
        AllowanceRequest,
        [EDIFieldValue("S")]
        Service
    }
}
