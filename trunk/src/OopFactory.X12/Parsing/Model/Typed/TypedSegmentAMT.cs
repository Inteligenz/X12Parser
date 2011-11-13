using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentAMT : TypedSegment
    {
        public TypedSegmentAMT()
            : base("AMT")
        {
        }

        public string AMT01_AmountQualifierCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string AMT02_MonetaryAmount
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string AMT03_CreditDebigFlagCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }
    }
}
