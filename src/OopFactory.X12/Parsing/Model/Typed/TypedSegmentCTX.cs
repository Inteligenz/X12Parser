using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentCTX : TypedSegment
    {
        public TypedSegmentCTX()
            : base("CTX")
        {
        }
        public TypedSegmentCTX(Segment seg) : base(seg) { }

        public TypedElementContextIdentification CTX01
        {
            get { return new TypedElementContextIdentification(_segment, 1); }
            set { _segment.SetElement(1, value); }
        }

        public string CTX02_SegmentIdCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public int? CTX03_SegmentPositionInTransactionSet
        {
            get
            {
                int position;
                if (int.TryParse(_segment.GetElement(3), out position))
                    return position;
                else
                    return null;
            }
            set { _segment.SetElement(3, string.Format("{0}", value)); }
        }

        public string CTX04_LoopIdentifierCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public TypedElementPositionInSegment CTX05
        {
            get { return new TypedElementPositionInSegment(_segment, 5); }
            set { _segment.SetElement(5, value); }
        }


    }
}
