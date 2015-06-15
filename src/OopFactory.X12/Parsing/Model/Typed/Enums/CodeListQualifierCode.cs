using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum CodeListQualifierCode
    {
        [EDIFieldValue("BF")]
        Diagnosis,
        [EDIFieldValue("BK")]
        PrincipalDiagnosis,
        [EDIFieldValue("ZZ")]
        MutuallyDefined
    }
}
