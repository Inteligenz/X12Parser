namespace X12.Shared.Models.TypedSegments
{
    public class TypedSegmentN9 : TypedSegment
    {
        public TypedSegmentN9()
            : base("N9")
        {
        }

        public string N901_ReferenceIdentificationQualifier
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string N902_ReferenceIdentification
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }
    }
}
