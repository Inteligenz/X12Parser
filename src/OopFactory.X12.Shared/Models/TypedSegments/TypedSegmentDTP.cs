namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using System;

    using OopFactory.X12.Shared.Enumerations;
    using OopFactory.X12.Shared.Extensions;

    public class TypedSegmentDTP : TypedSegment
    {

        public TypedSegmentDTP()
            : base("DTP")
        {
        }

        public DTPQualifier DTP01_DateTimeQualifier
        {
            get { return this.Segment.GetElement(1).ToEnumFromEdiFieldValue<DTPQualifier>(); }
            set { this.Segment.SetElement(1, value.EdiFieldValue()); }
        }

        public DTPFormatQualifier DTP02_DateTimePeriodFormatQualifier
        {
            get { return this.Segment.GetElement(2).ToEnumFromEdiFieldValue<DTPFormatQualifier>(); }
            set { this.Segment.SetElement(2, value.EdiFieldValue()); }
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
                string date = value.IsDateRange
                                  ? $"{value.StartDate:yyyyMMdd}-{value.EndDate:yyyyMMdd}"
                                  : $"{value.StartDate:yyyyMMdd}";
                this.Segment.SetElement(3, date);
            }
        }
    }
}
