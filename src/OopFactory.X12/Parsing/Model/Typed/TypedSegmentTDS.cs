using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
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
            get { return _segment.GetIntElement(1); }
            set { _segment.SetElement(1, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? TDS02_AmountN2
        {
            get { return _segment.GetIntElement(2); }
            set { _segment.SetElement(2, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? TDS03_AmountN2
        {
            get { return _segment.GetIntElement(3); }
            set { _segment.SetElement(3, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? TDS04_AmountN2
        {
            get { return _segment.GetIntElement(4); }
            set { _segment.SetElement(4, value); }
        }
    }
}
