using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Attributes;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public enum YesNoConditionOrResponseCode
    {
        [EDIFieldValue("N")]
        No,
        [EDIFieldValue("U")]
        Unknown,
        [EDIFieldValue("W")]
        NotApplicable,
        [EDIFieldValue("Y")]
        Yes
    }
}
