using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentN3 : TypedSegment
    {
        public TypedSegmentN3()
            : base("N3")
        {
        }

        public string N301_AddressInformation
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string N302_AddressInformation
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }
    }
}
