using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Validation.Model
{
    public class PositionInSegment
    {
        /// <summary>
        /// 1
        /// </summary>
        public int? ElementPositionInSegment { get; set; }

        /// <summary>
        /// 3
        /// </summary>
        public int? ComponentDataElementPositionInComposite { get; set; }

        /// <summary>
        /// 3
        /// </summary>
        public int? RepeatingDataElementPosition { get; set; }
    }
}
