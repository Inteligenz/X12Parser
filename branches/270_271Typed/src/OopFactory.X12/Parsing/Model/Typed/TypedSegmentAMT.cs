using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentAMT : TypedSegment
    {
        public TypedSegmentAMT()
            : base("AMT")
        {
        }

        public TypedSegmentAMT(Segment seg) : base(seg) { }

        public string AMT01_AmountQualifierCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public AmountQualifierCode AMT01_AmountQualifierCodeEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<AmountQualifierCode>(); }
            set { _segment.SetElement(6, value.EDIFieldValue()); }
        }

        public decimal AMT02_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(2).GetValueOrDefault(); }
            set { _segment.SetElement(2, value); }
        }

        public string AMT03_CreditDebigFlagCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }
    }
}
