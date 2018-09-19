namespace OopFactory.X12.Shared.Models.TypedSegments
{
    public class TypedSegmentN4 : TypedSegment
    {
        public TypedSegmentN4()
            : base("N4")
        {
        }

        public string N401_CityName
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string N402_StateOrProvinceCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string N403_PostalCode
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string N404_CountryCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string N405_LocationQualifier
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string N406_LocationIdentifier
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public string N407_CountrySubdivisionCode
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }
    }
}
