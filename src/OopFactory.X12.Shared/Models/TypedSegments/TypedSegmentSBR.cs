namespace OopFactory.X12.Shared.Models.TypedSegments
{
    public class TypedSegmentSBR : TypedSegment
    {
        public TypedSegmentSBR()
            : base("SBR")
        {
        }

        public string SBR01_PayerResponsibilitySequenceNumberCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string SBR02_IndividualRelationshipCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string SBR03_PolicyOrGroupNumber
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string SBR04_GroupName
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string SBR05_InsuranceTypeCode
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string SBR06_CoordinationOfBenefitsCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public string SBR07_YesNoCode
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string SBR08_EmploymentStatusCode
        {
            get { return this.Segment.GetElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string SBR09_ClaimFilingIndicatorCode
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }
    }
}
