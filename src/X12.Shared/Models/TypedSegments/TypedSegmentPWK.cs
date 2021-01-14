namespace X12.Shared.Models.TypedSegments
{
    using X12.Shared.Enumerations;
    using X12.Shared.Extensions;

    public class TypedSegmentPWK : TypedSegment
    {
        public TypedSegmentPWK()
            : base("PWK")
        {
        }

        public string PWK01_ReportTypeCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string PWK02_ReportTransmissionCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public int? PWK03_ReportCopiesNeeded
        {
            get { return this.Segment.GetIntElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string PWK04_EntityIdentifierCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public EntityIdentifierCode PWK04_EntityIdentiferCodeEnum
        {
            get { return this.Segment.GetElement(4).ToEnumFromEdiFieldValue<EntityIdentifierCode>(); }
            set { this.Segment.SetElement(4, value.EdiFieldValue()); }
        }

        public string PWK05_IdentificationCodeQualifier
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public IdentificationCodeQualifier PWK05_IdentificationCodeQualifierEnum
        {
            get { return this.Segment.GetElement(5).ToEnumFromEdiFieldValue<IdentificationCodeQualifier>(); }
            set { this.Segment.SetElement(5, value.EdiFieldValue()); }
        }

        public string PWK06_IdentificationCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public string PWK07_Description
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string PWK08_ActionsIndicated
        {
            get { return this.Segment.GetElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string PWK09_RequestCategoryCode
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }
    }
}
