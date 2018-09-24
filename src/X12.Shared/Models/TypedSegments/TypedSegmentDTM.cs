namespace X12.Shared.Models.TypeSegment
{
    using X12.Shared.Enumerations;
    using X12.Shared.Extensions;

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
            get { return this.Segment.GetElement(1).ToEnumFromEdiFieldValue<DTPQualifier>(); }
            set { this.Segment.SetElement(1, value.EdiFieldValue()); }
        }

        public string DTM02_Date
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string DTM03_Time
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public TimeCode DTM04_TimeCode
        {
            get { return this.Segment.GetElement(4).ToEnumFromEdiFieldValue<TimeCode>(); }
            set { this.Segment.SetElement(4, value.EdiFieldValue()); }
        }

        public DTPFormatQualifier DTM05_DateTimePeriodFormatQualifier
        {
            get { return this.Segment.GetElement(5).ToEnumFromEdiFieldValue<DTPFormatQualifier>(); }
            set { this.Segment.SetElement(5, value.EdiFieldValue()); }
        }

        public string DTM06_DateTimePeriod
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }
    }
}
