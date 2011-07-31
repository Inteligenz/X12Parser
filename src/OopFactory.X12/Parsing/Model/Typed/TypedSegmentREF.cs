using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentREF : TypedSegment
    {
        public TypedSegmentREF()
            : base("REF")
        {
        }

        public string REF01_ReferenceIdQualifier
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string REF02_ReferenceId
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string REF03_Description
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string REF04_ReferenceId
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }
    }
}
