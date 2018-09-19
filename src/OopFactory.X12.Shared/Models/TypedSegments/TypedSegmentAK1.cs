namespace OopFactory.X12.Shared.Models.TypedSegments
{
    public class TypedSegmentAK1 : TypedSegment
    {
        public TypedSegmentAK1()
            : base("AK1")
        {
        }

        public string AK101_FunctionalIdCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string AK102_GroupControlNumber
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string AK103_VersionIdentifierCode
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }
    }
}
