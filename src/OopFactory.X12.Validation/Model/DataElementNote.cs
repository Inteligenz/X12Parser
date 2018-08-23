namespace OopFactory.X12.Validation.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the IK40X note
    /// </summary>
    public class DataElementNote
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataElementNote"/> class
        /// </summary>
        public DataElementNote()
        {
            if (this.ContextErrors == null)
            {
                this.ContextErrors = new List<ContextError>();
            }
        }

        /// <summary>
        /// Gets or sets IK401
        /// </summary>
        public PositionInSegment PositionInSegment { get; set; }

        /// <summary>
        /// Gets or sets IK402
        /// </summary>
        public string DataElementReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets IK403
        /// </summary>
        public string SyntaxErrorCode { get; set; }

        /// <summary>
        /// Gets or sets IK404
        /// </summary>
        public string CopyOfBadElement { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of <see cref="ContextError"/> references
        /// </summary>
        public List<ContextError> ContextErrors { get; set; }
    }
}
