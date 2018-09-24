namespace X12.Validation.Model
{
    /// <summary>
    /// Represents the CTX0X error
    /// </summary>
    public class ContextError
    {
        /// <summary>
        /// Gets or sets CTX01 - 2
        /// </summary>
        public string IdentificationReference { get; set; }

        /// <summary>
        /// Gets or sets CTX02
        /// </summary>
        public string SegmentIdCode { get; set; }
        
        /// <summary>
        /// Gets or sets CTX03
        /// </summary>
        public int? SegmentPositionInTransactionSet { get; set; }
        
        /// <summary>
        /// Gets or sets CTX04
        /// </summary>
        public string LoopIdentifierCode { get; set; }

        /// <summary>
        /// Gets or sets CTX05
        /// </summary>
        public PositionInSegment PositionInSegment { get; set; }

        /// <summary>
        /// Gets or sets CTX06
        /// </summary>
        public string ReferenceInSegment { get; set; }
    }
}
