using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Eligibility or Benefit Inquiry
    /// </summary>
    public class TypedLoopEQ : TypedLoop
    {
        public TypedLoopEQ()
            : base("EQ")
        {
        }

        public ServiceTypeCode EQ01_ServiceTypeCodeEnum
        {
            get { return Loop.GetElement(1).ToEnumFromEDIFieldValue<ServiceTypeCode>(); }
            set { Loop.SetElement(1, value.EDIFieldValue()); }
        }


        public string EQ02_CompositeMedicalProcedure
        {
            get { return Loop.GetElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public CoverageLevelCode EQ03_CoverageLevelCodeEnum
        {
            get { return Loop.GetElement(3).ToEnumFromEDIFieldValue<CoverageLevelCode>(); }
            set { Loop.SetElement(3, value.EDIFieldValue()); }
        }

        public InsuranceTypeCode EQ04_InsuranceTypeCodeEnum
        {
            get { return Loop.GetElement(4).ToEnumFromEDIFieldValue<InsuranceTypeCode>(); }
            set { Loop.SetElement(4, value.EDIFieldValue()); }
        }
    }
}
