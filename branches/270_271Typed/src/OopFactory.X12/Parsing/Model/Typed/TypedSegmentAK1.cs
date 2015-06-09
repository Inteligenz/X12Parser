using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentAK1 : TypedSegment
    {
        public TypedSegmentAK1()
            : base("AK1")
        {
        }

        public string AK101_FunctionalIdCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string AK102_GroupControlNumber
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string AK103_VersionIdentifierCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }
    }
}
