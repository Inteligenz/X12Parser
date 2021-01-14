namespace X12.Validation.Model
{
    /// <summary>
    /// A collection of positions in a segment
    /// </summary>
    public class PositionInSegment
    {
        /// <summary>
        /// Gets or sets the first position in a Segment
        /// </summary>
        public int? ElementPositionInSegment { get; set; }

        /// <summary>
        /// Gets or sets the second position in a Segment
        /// </summary>
        public int? ComponentDataElementPositionInComposite { get; set; }

        /// <summary>
        /// Gets or sets the third position in a Segment
        /// </summary>
        public int? RepeatingDataElementPosition { get; set; }
    }
}
