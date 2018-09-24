namespace X12.Validation
{
    using X12.Specifications;

    /// <summary>
    /// Information about the segment
    /// </summary>
    public class SegmentInformation
    {
        /// <summary>
        /// Gets or sets the unique segment identifier
        /// </summary>
        public string SegmentId { get; set; }

        /// <summary>
        /// Gets or sets the segment position
        /// </summary>
        public int SegmentPosition { get; set; }

        /// <summary>
        /// Gets or sets the elements in the segment
        /// </summary>
        public string[] Elements { get; set; }

        /// <summary>
        /// Gets or sets the loop identifier
        /// </summary>
        public string LoopId { get; set; }

        /// <summary>
        /// Gets or sets the segment specification
        /// </summary>
        public SegmentSpecification Spec { get; set; }

        /// <summary>
        /// Returns the string representation of the segment information
        /// </summary>
        /// <returns>String representation of the segment information</returns>
        public override string ToString()
        {
            return $"Id={this.SegmentId}, Pos={this.SegmentPosition}, Loop={this.LoopId}";
        }
    }
}
