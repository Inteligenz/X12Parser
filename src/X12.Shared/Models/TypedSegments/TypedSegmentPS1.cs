namespace X12.Shared.Models.TypedSegments
{
    public class TypedSegmentPS1 : TypedSegment
    {
        public TypedSegmentPS1()
            : base("PS1")
        {
        }

        public string PS101_ReferenceId
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string PS102_MonentaryAmount
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string PS103_StateOrProvinceCode
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }
    }
}
