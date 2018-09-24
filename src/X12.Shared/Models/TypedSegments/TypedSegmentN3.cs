namespace X12.Shared.Models.TypedSegments
{
    public class TypedSegmentN3 : TypedSegment
    {
        public TypedSegmentN3()
            : base("N3")
        {
        }

        public string N301_AddressInformation
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string N302_AddressInformation
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }
    }
}
