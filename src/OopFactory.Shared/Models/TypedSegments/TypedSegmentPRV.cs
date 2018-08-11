namespace OopFactory.X12.Shared.Models.TypedSegments
{
    public class TypedSegmentPRV : TypedSegment
    {
        public TypedSegmentPRV()
            : base("PRV")
        {
        }

        public string PRV01_ProviderCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string PRV02_ReferenceIdQualifier
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string PRV03_ProviderTaxonomyCode
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string PRV04_StateOrProvinceCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string PRV05_ProviderSpecialtyInformation
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string PRV06_ProviderOrganizationCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }
    }
}
