namespace OopFactory.X12.Shared.Models.TypedLoops
{
    public class TypedLoopSBR : TypedLoop
    {
        public TypedLoopSBR()
            : base("SBR")
        {
        }

        public string SBR01_PayerResponsibilitySequenceNumberCode
        {
            get { return this.Loop.GetElement(1); }
            set { this.Loop.SetElement(1, value); }
        }

        public string SBR02_IndividualRelationshipCode
        {
            get { return this.Loop.GetElement(2); }
            set { this.Loop.SetElement(2, value); }
        }

        public string SBR03_PolicyOrGroupNumber
        {
            get { return this.Loop.GetElement(3); }
            set { this.Loop.SetElement(3, value); }
        }

        public string SBR04_GroupName
        {
            get { return this.Loop.GetElement(4); }
            set { this.Loop.SetElement(4, value); }
        }

        public string SBR05_InsuranceTypeCode
        {
            get { return this.Loop.GetElement(5); }
            set { this.Loop.SetElement(5, value); }
        }

        public string SBR06_CoordinationOfBenefitsCode
        {
            get { return this.Loop.GetElement(6); }
            set { this.Loop.SetElement(6, value); }
        }

        public string SBR07_YesNoCode
        {
            get { return this.Loop.GetElement(7); }
            set { this.Loop.SetElement(7, value); }
        }

        public string SBR08_EmploymentStatusCode
        {
            get { return this.Loop.GetElement(8); }
            set { this.Loop.SetElement(8, value); }
        }

        public string SBR09_ClaimFilingIndicatorCode
        {
            get { return this.Loop.GetElement(9); }
            set { this.Loop.SetElement(9, value); }
        }
    }
}
