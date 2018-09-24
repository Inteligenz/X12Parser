namespace X12.Shared.Models.TypedSegments
{
    public class TypedSegmentHI : TypedSegment
    {
        public TypedSegmentHI() : base("HI")
        {
        }

        public string HI01_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string HI02_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }
        public string HI03_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string HI04_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }
        public string HI05_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string HI06_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }
        public string HI07_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string HI08_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(8); }
            set { this.Segment.SetElement(8, value); }
        }
        public string HI09_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }

        public string HI10_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(10); }
            set { this.Segment.SetElement(10, value); }
        }
        public string HI11_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(11); }
            set { this.Segment.SetElement(11, value); }
        }

        public string HI12_HealthCareCodeInformation
        {
            get { return this.Segment.GetElement(12); }
            set { this.Segment.SetElement(12, value); }
        }
    }
}
