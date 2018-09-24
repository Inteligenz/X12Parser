namespace X12.Shared.Models.TypedSegments
{
    public class TypedSegmentIK5 : TypedSegment
    {
        public TypedSegmentIK5() : base("IK5") { }

        public string IK501_TransactionSetAcknowledgmentCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string IK502_SyntaxErrorCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string IK503_SyntaxErrorCode
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }
        public string IK504_SyntaxErrorCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }
        public string IK505_SyntaxErrorCode
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }
        public string IK506_SyntaxErrorCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }
    }
}
