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
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public string SBR02_IndividualRelationshipCode
        {
            get { return _loop.GetElement(2); }
            set { _loop.SetElement(2, value); }
        }

        public string SBR03_PolicyOrGroupNumber
        {
            get { return _loop.GetElement(3); }
            set { _loop.SetElement(3, value); }
        }

        public string SBR04_GroupName
        {
            get { return _loop.GetElement(4); }
            set { _loop.SetElement(4, value); }
        }

        public string SBR05_InsuranceTypeCode
        {
            get { return _loop.GetElement(5); }
            set { _loop.SetElement(5, value); }
        }

        public string SBR06_CoordinationOfBenefitsCode
        {
            get { return _loop.GetElement(6); }
            set { _loop.SetElement(6, value); }
        }

        public string SBR07_YesNoCode
        {
            get { return _loop.GetElement(7); }
            set { _loop.SetElement(7, value); }
        }

        public string SBR08_EmploymentStatusCode
        {
            get { return _loop.GetElement(8); }
            set { _loop.SetElement(8, value); }
        }

        public string SBR09_ClaimFilingIndicatorCode
        {
            get { return _loop.GetElement(9); }
            set { _loop.SetElement(9, value); }
        }
    }
}
