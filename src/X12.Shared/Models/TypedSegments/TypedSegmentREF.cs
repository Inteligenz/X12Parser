namespace X12.Shared.Models.TypedSegments
{
    public class TypedSegmentREF : TypedSegment
    {
        public TypedSegmentREF()
            : base("REF")
        {
        }

        public string REF01_ReferenceIdQualifier
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string REF02_ReferenceId
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string REF03_Description
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string REF04_ReferenceId
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }
    }
}
