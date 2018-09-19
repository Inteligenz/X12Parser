namespace OopFactory.X12.Shared.Models.TypedSegments
{
    public class TypedSegmentAMT : TypedSegment
    {
        public TypedSegmentAMT()
            : base("AMT")
        {
        }

        public string AMT01_AmountQualifierCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string AMT02_MonetaryAmount
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string AMT03_CreditDebigFlagCode
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }
    }
}
