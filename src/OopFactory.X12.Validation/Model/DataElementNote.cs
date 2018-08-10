namespace OopFactory.X12.Validation.Model
{
    using System.Collections.Generic;

    public class DataElementNote
    {
        public DataElementNote()
        {
            if (ContextErrors == null) ContextErrors = new List<ContextError>();
        }
        /// <summary>
        /// IK401
        /// </summary>
        public PositionInSegment PositionInSegment { get; set; }

        /// <summary>
        /// IK402
        /// </summary>
        public string DataElementReferenceNumber { get; set; }

        /// <summary>
        /// IK403
        /// </summary>
        public string SyntaxErrorCode { get; set; }

        /// <summary>
        /// IK404
        /// </summary>
        public string CopyOfBadElement { get; set; }
        
        public List<ContextError> ContextErrors { get; set; }
    }
}
