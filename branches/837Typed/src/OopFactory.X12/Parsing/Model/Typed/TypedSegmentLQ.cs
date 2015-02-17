using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentLQ : TypedSegment
    {
        public TypedSegmentLQ()
            : base("LQ")
        {
        }

        public string LQ01_CodeListQualifierCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string LQ02_IndustryCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }
    }
}
