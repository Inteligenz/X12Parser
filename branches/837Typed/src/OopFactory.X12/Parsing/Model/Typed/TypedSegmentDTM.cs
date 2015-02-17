using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Date/Time Reference
    /// </summary>
    public class TypedSegmentDTM : TypedSegment
    {
        public TypedSegmentDTM()
            : base("DTM")
        {
        }

        public DTPQualifier DTM01_DateTimeQualifier
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<DTPQualifier>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public string DTM02_Date
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string DTM03_Time
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public TimeCode DTM04_TimeCode
        {
            get { return _segment.GetElement(4).ToEnumFromEDIFieldValue<TimeCode>(); }
            set { _segment.SetElement(4, value.EDIFieldValue()); }
        }

        public DTPFormatQualifier DTM05_DateTimePeriodFormatQualifier
        {
            get { return _segment.GetElement(5).ToEnumFromEDIFieldValue<DTPFormatQualifier>(); }
            set { _segment.SetElement(5, value.EDIFieldValue()); }
        }

        public string DTM06_DateTimePeriod
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }
    }
}
