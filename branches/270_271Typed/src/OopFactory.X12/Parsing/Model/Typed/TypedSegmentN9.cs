using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentN9 : TypedSegment
    {
        public TypedSegmentN9()
            : base("N9")
        {
        }

        public string N901_ReferenceIdentificationQualifier
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string N902_ReferenceIdentification
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }
    }
}
