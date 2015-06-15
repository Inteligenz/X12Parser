using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopSBR : TypedLoop
    {
        public TypedLoopSBR()
            : base("SBR")
        {
        }

        public string SBR01_PayerResponsibilitySequenceNumberCode
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public string SBR02_IndividualRelationshipCode
        {
            get { return Loop.GetElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public string SBR03_PolicyOrGroupNumber
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public string SBR04_GroupName
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }

        public string SBR05_InsuranceTypeCode
        {
            get { return Loop.GetElement(5); }
            set { Loop.SetElement(5, value); }
        }

        public string SBR06_CoordinationOfBenefitsCode
        {
            get { return Loop.GetElement(6); }
            set { Loop.SetElement(6, value); }
        }

        public string SBR07_YesNoCode
        {
            get { return Loop.GetElement(7); }
            set { Loop.SetElement(7, value); }
        }

        public string SBR08_EmploymentStatusCode
        {
            get { return Loop.GetElement(8); }
            set { Loop.SetElement(8, value); }
        }

        public string SBR09_ClaimFilingIndicatorCode
        {
            get { return Loop.GetElement(9); }
            set { Loop.SetElement(9, value); }
        }
    }
}
