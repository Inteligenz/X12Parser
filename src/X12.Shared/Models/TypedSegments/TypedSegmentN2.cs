using System;
namespace X12.Shared.Models.TypedSegments
{
    public class TypedSegmentN2 : TypedSegment
    {
        public TypedSegmentN2()
            : base("N2")
        {
        }

        public string N201_Name
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string N202_Name
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }
    }
}
