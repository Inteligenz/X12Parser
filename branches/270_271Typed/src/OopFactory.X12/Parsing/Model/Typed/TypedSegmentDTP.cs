using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Attributes;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentDTP : TypedSegment
    {

        public TypedSegmentDTP() : base("DTP") { }

        public DTPQualifier DTP01_DateTimeQualifier
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<DTPQualifier>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public DTPFormatQualifier DTP02_DateTimePeriodFormatQualifier
        {
            get { return _segment.GetElement(2).ToEnumFromEDIFieldValue<DTPFormatQualifier>(); }
            set { _segment.SetElement(2, value.EDIFieldValue()); }
        }

        public DateTimePeriod DTP03_Date
        {
            get
            {
                string element = _segment.GetElement(3);
                if (element.Length == 8)
                    return new DateTimePeriod(DateTime.ParseExact(element, "yyyyMMdd", null));
                if (element.Length == 17)
                    return new DateTimePeriod(DateTime.ParseExact(element.Substring(0,8), "yyyyMMdd", null),
                        DateTime.ParseExact(element.Substring(9), "yyyyMMdd", null));
                return null;
            }
            set {
                _segment.SetElement(3,
                                    value.IsDateRange
                                        ? String.Format("{0:yyyyMMdd}-{1:yyyyMMdd}", value.StartDate, value.EndDate)
                                        : String.Format("{0:yyyyMMdd}", value.StartDate));
            }
        }

    }


    /// <summary>
    /// Move this class in seperate file if being used by other classes.
    /// </summary>
    public class DateTimePeriod
    {
        public bool IsDateRange { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public DateTimePeriod(DateTime date)
        {
            this.StartDate = date;
            IsDateRange = false;
        }

        public DateTimePeriod(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            IsDateRange = true;
        }

    }
}
