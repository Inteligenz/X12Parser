namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using OopFactory.X12.Shared.Enumerations;
    using OopFactory.X12.Shared.Extensions;

    public class TypedSegmentCN1 : TypedSegment
    {
        public TypedSegmentCN1()
            : base("CN1")
        {
        }

        public string CN101_ContractTypeCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public ContractTypeCode CN101_ContractTypeCodeEnum
        {
            get { return this.Segment.GetElement(1).ToEnumFromEDIFieldValue<ContractTypeCode>(); }
            set { this.Segment.SetElement(1, value.EDIFieldValue()); }
        }

        public decimal? CN102_MonetaryAmount
        {
            get { return this.Segment.GetDecimalElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public decimal? CN103_Percent
        {
            get { return this.Segment.GetDecimalElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string CN104_ReferenceIdentification
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public decimal? CN105_TermsDiscountPercent
        {
            get { return this.Segment.GetDecimalElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string CN106_VersionIdentifier
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }
    }
}
