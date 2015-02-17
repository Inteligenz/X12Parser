using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentLIN : TypedSegment
    {
        public TypedSegmentLIN()
            : base("LIN")
        {
        }

        public string LIN01_AssignedIdentification
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string LIN02_ProductOrServiceIdQualifier
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string LIN03_ProductOrServiceId
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }
    }
}
