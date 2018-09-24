namespace X12.Shared.Models.TypedElements
{
    public class TypedElementRelatedCausesInfo
    {
        private readonly int elementNumber;

        private readonly Segment segment;

        private string relatedCausesCode1;

        private string relatedCausesCode2;

        private string relatedCausesCode3;

        private string stateOrProviceCode;

        private string countryCode;

        internal TypedElementRelatedCausesInfo(Segment segment, int elementNumber)
        {
            this.segment = segment;
            this.elementNumber = elementNumber;
        }

        private void UpdateElement()
        {
            string value = string.Format(
                "{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                this.segment.Delimiters.SubElementSeparator,
                this.relatedCausesCode1,
                this.relatedCausesCode2,
                this.relatedCausesCode3,
                this.stateOrProviceCode,
                this.countryCode);
            value = value.TrimEnd(this.segment.Delimiters.SubElementSeparator);
            this.segment.SetElement(this.elementNumber, value);
        }

        public string _1_RelatedCausesCode
        {
            get
            {
                return this.relatedCausesCode1;
            }

            set
            {
                this.relatedCausesCode1 = value;
                this.UpdateElement();
            }
        }

        public string _2_RelatedCausesCode
        {
            get
            {
                return this.relatedCausesCode2;
            }

            set
            {
                this.relatedCausesCode2 = value;
                this.UpdateElement();
            }
        }

        public string _3_RelatedCausesCode
        {
            get
            {
                return this.relatedCausesCode3;
            }

            set
            {
                this.relatedCausesCode3 = value;
                this.UpdateElement();
            }
        }

        public string _4_StateOrProvidenceCode
        {
            get
            {
                return this.stateOrProviceCode;
            }

            set
            {
                this.stateOrProviceCode = value;
                this.UpdateElement();
            }
        }

        public string _5_CountryCode
        {
            get
            {
                return this.countryCode;
            }

            set
            {
                this.countryCode = value;
                this.UpdateElement();
            }
        }
    }
}
