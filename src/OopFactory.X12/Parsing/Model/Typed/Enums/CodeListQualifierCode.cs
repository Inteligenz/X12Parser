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
        [EDIFieldValue("ABF")]
        Diagnosis_ICD_10,
        [EDIFieldValue("ABK")]
        PrincipalDiagnosis_ICD_10,
        [EDIFieldValue("ABJ")]
        AdmittingDiagnosis_ICD_10,
        [EDIFieldValue("ZZ")]
        MutuallyDefined,
        [EDIFieldValue("BG")]
        Condition
    }
}
