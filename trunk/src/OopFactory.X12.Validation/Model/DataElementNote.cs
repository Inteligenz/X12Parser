using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Validation.Model
{
    public class DataElementNote
    {
        /// <summary>
        /// IK401
        /// </summary>
        public PositionInSegment PositionInSegment { get; set; }

        /// <summary>
        /// IK402
        /// </summary>
        public string DataElementReferenceNumber { get; set; } 
        
        public List<ContextError> ContextErrors { get; set; }
    }
}
