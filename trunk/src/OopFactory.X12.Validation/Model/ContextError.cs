using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Validation.Model
{
    public class ContextError
    {
        /// <summary>
        /// CTX01 - 2
        /// </summary>
        public string IdentificationReference { get; set; }

        /// <summary>
        /// CTX02
        /// </summary>
        public string SegmentIdCode { get; set; }
        
        /// <summary>
        /// CTX03
        /// </summary>
        public int? SegmentPositionInTransactionSet { get; set; }
        
        /// <summary>
        /// CTX04
        /// </summary>
        public string LoopIdentifierCode { get; set; }

        /// <summary>
        /// CTX05
        /// </summary>
        public PositionInSegment PositionInSegment { get; set; }

        /// <summary>
        /// CTX06
        /// </summary>
        public string ReferenceInSegment { get; set; }


    }
}
