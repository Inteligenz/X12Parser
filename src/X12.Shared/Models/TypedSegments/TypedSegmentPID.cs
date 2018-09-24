namespace X12.Shared.Models.TypedSegments
{
    using X12.Shared.Enumerations;
    using X12.Shared.Extensions;

    /// <summary>
    /// Product/Item Description
    /// </summary>
    public class TypedSegmentPID : TypedSegment
    {
        public TypedSegmentPID()
            : base("PID")
        {
        }

        /// <summary>
        /// F = Free form
        /// </summary>
        public string PID01_ItemDescriptionType
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string PID02_ProductProcessCharacteristicCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string PID03_AgencyQualifierCode
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string PID04_ProductDescriptionCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string PID05_Description
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string PID06_SurfaceLayerPositionCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public string PID07_SourceSubqualifier
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public YesNoConditionOrResponseCode PID08_YesNoConditionOrResponseCode
        {
            get { return this.Segment.GetElement(8).ToEnumFromEdiFieldValue<YesNoConditionOrResponseCode>(); }
            set { this.Segment.SetElement(8, value.EdiFieldValue()); }
        }

        public string PID09_LanguageCode
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }
    }
}
