using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    [Flags]
    public enum CoverageLevelCode
    {
        [EDIFieldValue("CHD")]
        ChildrenOnly,
        [EDIFieldValue("DEP")]
        DependentsOnly,
        [EDIFieldValue("ECH")]
        EmployeeAndChildren,
        [EDIFieldValue("EMP")]
        EmployeeOnly,
        [EDIFieldValue("ESP")]
        EmployeeAndSpouse,
        [EDIFieldValue("FAM")]
        Family,
        [EDIFieldValue("IND")]
        Individual,
        [EDIFieldValue("SPC")]
        SpouseAndChildren,
        [EDIFieldValue("SPO")]
        SpouseOnly,
    }
}
