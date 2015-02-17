using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentN2 : TypedSegment
    {
        public TypedSegmentN2()
            : base("N2")
        {
        }

        public string N201_Name
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string N202_Name
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }
    }
}
