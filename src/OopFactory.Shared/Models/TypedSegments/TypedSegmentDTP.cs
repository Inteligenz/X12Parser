namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using System;

    using OopFactory.X12.Shared.Enumerations;
    using OopFactory.X12.Shared.Extensions;

    public class TypedSegmentDTP : TypedSegment
    {

        public TypedSegmentDTP() : base("DTP") { }

        public DTPQualifier DTP01_DateTimeQualifier
        {
            get { return this.Segment.GetElement(1).ToEnumFromEDIFieldValue<DTPQualifier>(); }
            set { this.Segment.SetElement(1, value.EDIFieldValue()); }
        }

        public DTPFormatQualifier DTP02_DateTimePeriodFormatQualifier
        {
            get { return this.Segment.GetElement(2).ToEnumFromEDIFieldValue<DTPFormatQualifier>(); }
            set { this.Segment.SetElement(2, value.EDIFieldValue()); }
        }

        public DateTimePeriod DTP03_Date
        {
            get
            {
                string element = this.Segment.GetElement(3);
                if (element.Length == 8)
                {
                    return new DateTimePeriod(DateTime.ParseExact(element, "yyyyMMdd", null));
                }
                if (element.Length == 17)
                {
                    return new DateTimePeriod(
                        DateTime.ParseExact(element.Substring(0, 8), "yyyyMMdd", null),
                        DateTime.ParseExact(element.Substring(9), "yyyyMMdd", null));
                }

                return null;
            }

            set
            {
                this.Segment.SetElement(3,
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
