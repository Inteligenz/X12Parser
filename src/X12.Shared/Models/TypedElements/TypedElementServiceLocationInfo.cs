namespace X12.Shared.Models.TypedElements
{
    public class TypedElementServiceLocationInfo
    {
        private readonly int elementNumber;

        private readonly Segment segment;

        private string facilityCodeValue;

        private string facilityCodeQualifier;

        private string claimFrequencyTypeCode;

        internal TypedElementServiceLocationInfo(Segment segment, int elementNumber)
        {
            this.segment = segment;
            this.elementNumber = elementNumber;
        }

        private void UpdateElement()
        {
            string value = string.Format(
                "{1}{0}{2}{0}{3}",
                this.segment.Delimiters.SubElementSeparator,
                this.facilityCodeValue,
                this.facilityCodeQualifier,
                this.claimFrequencyTypeCode);
            value = value.TrimEnd(this.segment.Delimiters.SubElementSeparator);
            this.segment.SetElement(this.elementNumber, value);
        }

        public string _1_FacilityCodeValue
        {
            get
            {
                return this.facilityCodeValue;
            }

            set
            {
                this.facilityCodeValue = value;
                this.UpdateElement();
            }
        }

        public string _2_FacilityCodeQualifier
        {
            get
            {
                return this.facilityCodeQualifier;
            }

            set
            {
                this.facilityCodeQualifier = value;
                this.UpdateElement();
            }
        }

        public string _3_ClaimFrequencyTypeCode
        {
            get
            {
                return this.claimFrequencyTypeCode;
            }

            set
            {
                this.claimFrequencyTypeCode = value;
                this.UpdateElement();
            }
        }
    }
}
