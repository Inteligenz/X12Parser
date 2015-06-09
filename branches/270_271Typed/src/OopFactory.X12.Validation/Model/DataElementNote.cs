using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Validation.Model
{
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
