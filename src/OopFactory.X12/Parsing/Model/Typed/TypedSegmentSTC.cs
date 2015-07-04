using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentSTC : TypedSegment
    {
        public TypedSegmentSTC() : base("STC") { }

        public TypedSegmentSTC(Segment segment) : base(segment) { }

        public TypedElementHealthCareClaimStatus STC01_HealthCareClaimStatus
        {
            get { return new TypedElementHealthCareClaimStatus(_segment, 1); }
            set { _segment.SetElement(1, value); }
        }

        public DateTime? STC02_Date
        {
            get { return _segment.GetDate8Element(2); }
            set { _segment.SetDate8Element(2, value); }
        }

        public ActionCodes STC03_ActionCode
        {
            get { return _segment.GetElement(2).ToEnumFromEDIFieldValue<ActionCodes>(); }
            set { _segment.SetElement(2, value.EDIFieldValue()); }
        }

        public decimal? STC04_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public TypedElementHealthCareClaimStatus STC10_HealthCareClaimStatus
        {
            get { return new TypedElementHealthCareClaimStatus(_segment, 10); }
            set { _segment.SetElement(10, value); }
        }

        public TypedElementHealthCareClaimStatus STC11_HealthCareClaimStatus
        {
            get { return new TypedElementHealthCareClaimStatus(_segment, 11); }
            set { _segment.SetElement(11, value); }
        }
    }
}
