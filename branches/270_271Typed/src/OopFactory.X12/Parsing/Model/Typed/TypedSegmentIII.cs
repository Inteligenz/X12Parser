using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// ELIGIBILITY OR BENEFIT ADDITIONAL INQUIRY INFORMATION
    /// </summary>
    public class TypedSegmentIII : TypedSegment
    {
        public TypedSegmentIII()
            : base("III")
        {
        }

        public TypedSegmentIII(Segment segment)
            : base(segment)
        {
        }

        public CodeListQualifierCode III01_CodeListQualifierCodeEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<CodeListQualifierCode>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public IndustryCode III02_IndustryCodeEnum
        {
            get { return _segment.GetElement(2).ToEnumFromEDIFieldValue<IndustryCode>(); }
            set { _segment.SetElement(2, value.EDIFieldValue()); }
        }

    }
}
