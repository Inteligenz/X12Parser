using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentAK4 : TypedSegment
    {
        private TypedElementPositionInSegment _AK401;

        public TypedSegmentAK4()
            : base("AK4") {
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters) {
            base.Initialize(parent, delimiters);
            _AK401 = new TypedElementPositionInSegment(_segment, 1);
        }

        public TypedElementPositionInSegment AK401_PositionInSegment {
            get { return _AK401; }
        }

        public string AK402_DataElementReferenceCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string AK403_DataElementSyntaxErrorCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string AK404_CopyOfBadDataElement {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }
    }
}
