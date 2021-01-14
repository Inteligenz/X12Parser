namespace X12.Shared.Models.TypedSegments
{
    /// <summary>
    /// Total Monetary Value Summary
    /// </summary>
    public class TypedSegmentTDS : TypedSegment
    {
        public TypedSegmentTDS()
            : base("TDS")
        {
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? TDS01_AmountN2
        {
            get { return this.Segment.GetIntElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? TDS02_AmountN2
        {
            get { return this.Segment.GetIntElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? TDS03_AmountN2
        {
            get { return this.Segment.GetIntElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? TDS04_AmountN2
        {
            get { return this.Segment.GetIntElement(4); }
            set { this.Segment.SetElement(4, value); }
        }
    }
}
