namespace X12.Shared.Models.TypedSegments
{
    using X12.Shared.Enumerations;

    public class TypedSegmentPER : TypedSegment
    {
        public TypedSegmentPER()
            : base("PER")
        {
        }

        public string PER01_ContactFunctionCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string PER02_Name
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        private CommunicationNumberQualifer GetQualifier(int elementNumber)
        {
            switch (this.Segment.GetElement(elementNumber))
            {
                case "EM": return CommunicationNumberQualifer.ElectronicMail;
                case "EX": return CommunicationNumberQualifer.TelephoneExtension;
                case "FX": return CommunicationNumberQualifer.Facsimile;
                case "TE": return CommunicationNumberQualifer.Telephone;
                default: return CommunicationNumberQualifer.Undefined;
            }
        }

        private void SetQualifier(int elementNumber, CommunicationNumberQualifer value)
        {
            switch (value)
            {
                case CommunicationNumberQualifer.ElectronicMail:
                    this.Segment.SetElement(elementNumber, "EM");
                    break;
                case CommunicationNumberQualifer.TelephoneExtension:
                    this.Segment.SetElement(elementNumber, "EX");
                    break;
                case CommunicationNumberQualifer.Facsimile:
                    this.Segment.SetElement(elementNumber, "FX");
                    break;
                case CommunicationNumberQualifer.Telephone:
                    this.Segment.SetElement(elementNumber, "TE");
                    break;
                default:
                    this.Segment.SetElement(elementNumber, string.Empty);
                    break;
            }
        }

        public CommunicationNumberQualifer PER03_CommunicationNumberQualifier
        {
            get { return this.GetQualifier(3); }
            set { this.SetQualifier(3, value); }
        }

        public string PER04_CommunicationNumber
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public CommunicationNumberQualifer PER05_CommunicationNumberQualifier
        {
            get { return this.GetQualifier(5); }
            set { this.SetQualifier(5, value); }
        }

        public string PER06_CommunicationNumber
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public CommunicationNumberQualifer PER07_CommunicationNumberQualifier
        {
            get { return GetQualifier(7); }
            set { SetQualifier(7, value); }
        }

        public string PER08_CommunicationNumber
        {
            get { return this.Segment.GetElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string PER09_ContactInquiryReference
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }
    }
}
