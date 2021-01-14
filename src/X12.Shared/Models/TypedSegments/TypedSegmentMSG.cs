namespace X12.Shared.Models.TypedSegments
{
    /// <summary>
    /// Message Text
    /// </summary>
    public class TypedSegmentMSG : TypedSegment
    {
        public TypedSegmentMSG()
            : base("MSG")
        {
        }

        public string MSG01_FreeFormMessageText
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string MSG02_PrinterCarriageControlCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public int? MSG03_Number
        {
            get { return this.Segment.GetIntElement(3); }
            set { this.Segment.SetElement(3, value); }
        }
    }
}
