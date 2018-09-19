namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using OopFactory.X12.Shared.Models.TypedElements;

    public class TypedSegmentCTX : TypedSegment
    {
        public TypedElementContextIdentification CTX01 { get; }

        public TypedElementPositionInSegment CTX05 { get; }

        public TypedSegmentCTX() : base("CTX") 
        {
            this.CTX01 = new TypedElementContextIdentification(this.Segment, 1);
            this.CTX05 = new TypedElementPositionInSegment(this.Segment, 5);
        }

        public string CTX02_SegmentIdCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public int? CTX03_SegmentPositionInTransactionSet
        {
            get
            {
                int position;
                if (int.TryParse(this.Segment.GetElement(3), out position))
                {
                    return position;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                this.Segment.SetElement(3, string.Format("{0}", value));
            }
        }

        public string CTX04_LoopIdentifierCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }
    }
}
