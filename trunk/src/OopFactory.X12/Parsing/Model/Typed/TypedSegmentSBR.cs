using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentSBR : TypedSegment
    {
        public TypedSegmentSBR()
            : base("SBR")
        {
        }

        public string SBR01_PayerResponsibilitySequenceNumberCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string SBR02_IndividualRelationshipCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string SBR03_PolicyOrGroupNumber
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string SBR04_GroupName
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string SBR05_InsuranceTypeCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string SBR06_CoordinationOfBenefitsCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string SBR07_YesNoCode
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string SBR08_EmploymentStatusCode
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string SBR09_ClaimFilingIndicatorCode
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }
    }
}
