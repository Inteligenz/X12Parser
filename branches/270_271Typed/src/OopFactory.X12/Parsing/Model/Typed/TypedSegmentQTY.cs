using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentQTY : TypedSegment
    {
        public TypedSegmentQTY()
            : base("QTY")
        {
        }
        public TypedSegmentQTY(Segment segment) : base(segment) { }
        public string QTY01_QuantityQualifier
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public decimal? QTY02_Quantity
        {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public TypedElementCompositeUnitOfMeasure QTY03_CompositeUnitOfMeasure
        {
            get { return new TypedElementCompositeUnitOfMeasure(_segment, 3); }
            set { _segment.SetElement(3, value); }
        }

        public decimal? QTY04_FreeFormMessage
        {
            get { return _segment.GetDecimalElement(4); }
            set { _segment.SetElement(4, value); }
        }
    }
}
