using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentPS1 : TypedSegment
    {
        public TypedSegmentPS1()
            : base("PS1")
        {
        }

        public string PS101_ReferenceId
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string PS102_MonentaryAmount
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string PS103_StateOrProvinceCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }
    }
}
