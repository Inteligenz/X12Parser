using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// REQUEST VALIDATION
    /// </summary>
    public class TypedSegmentAAA : TypedSegment
    {
        public TypedSegmentAAA()
            : base("AAA")
        {
        }

        public TypedSegmentAAA(Segment segment)
            : base(segment)
        {
        }

        public YesNoConditionOrResponseCode AAA01_ValidRequestIndicator
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<YesNoConditionOrResponseCode>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public RejectReasonCode AAA03_RejectReasonCode
        {
            get { return _segment.GetElement(3).ToEnumFromEDIFieldValue<RejectReasonCode>(); }
            set { _segment.SetElement(3, value.EDIFieldValue()); }
        }

        public FollowupActionCode AAA04_FollowUpActionCode
        {
            get { return _segment.GetElement(4).ToEnumFromEDIFieldValue<FollowupActionCode>(); }
            set { _segment.SetElement(4, value.EDIFieldValue()); }
        }
    }
}
