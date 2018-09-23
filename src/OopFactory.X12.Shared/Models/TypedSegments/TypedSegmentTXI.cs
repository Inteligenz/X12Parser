namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using OopFactory.X12.Shared.Enumerations;
    using OopFactory.X12.Shared.Extensions;

    /// <summary>
    /// Tax Information
    /// </summary>
    public class TypedSegmentTXI : TypedSegment
    {
        public TypedSegmentTXI()
            : base("TXI")
        {
        }

        public string TXI01_TaxTypeCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public decimal? TXI02_MonetaryAmount
        {
            get { return this.Segment.GetDecimalElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public decimal? TXI03_Percent
        {
            get { return this.Segment.GetDecimalElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string TXI04_TaxJurisdictionCodeQualifier
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string TXI05_TaxJurisdictionCode
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string TXI06_TaxExemptCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public RelationshipCode TXI07_RelationshipCode
        {
            get { return this.Segment.GetElement(7).ToEnumFromEdiFieldValue<RelationshipCode>(); }
            set { this.Segment.SetElement(7, value.EdiFieldValue()); }
        }

        public decimal? TXI08_DollarBasisForPercent
        {
            get { return this.Segment.GetDecimalElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string TXI09_TaxIdentificationNumber
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }

        public string TXI10_AssignedIdentification
        {
            get { return this.Segment.GetElement(10); }
            set { this.Segment.SetElement(10, value); }
        }
    }
}
