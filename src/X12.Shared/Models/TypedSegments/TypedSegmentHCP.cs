namespace X12.Shared.Models.TypeSegment
{
    using X12.Shared.Enumerations;
    using X12.Shared.Extensions;
    
    /// <summary>
    /// Health Care Pricing, to specify pricing or repricing information about a health care claim or line item
    /// </summary>
    public class TypedSegmentHCP : TypedSegment
    {
        public TypedSegmentHCP()
            : base("HCP")
        {
        }

        public string HCP01_PricingMethodology
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public PricingMethodology HCP01_PricingMethodologyEnum
        {
            get { return this.Segment.GetElement(1).ToEnumFromEdiFieldValue<PricingMethodology>(); }
            set { this.Segment.SetElement(1, value.EdiFieldValue()); }
        }

        public decimal? HCP02_AllowedAmount
        {
            get { return this.Segment.GetDecimalElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public decimal? HCP03_SavingsAmount
        {
            get { return this.Segment.GetDecimalElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string HCP04_RepricingOrganizationIdentificationNumber
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public decimal? HCP05_Rate
        {
            get { return this.Segment.GetDecimalElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string HCP06_ApprovedDrgCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public decimal? HCP07_ApprovedDrgAmount
        {
            get { return this.Segment.GetDecimalElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string HCP08_ApprovedRevenueCode
        {
            get { return this.Segment.GetElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string HCP09_Qualifier
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }

        public string HCP10_ApprovedProcedureCode
        {
            get { return this.Segment.GetElement(10); }
            set { this.Segment.SetElement(10, value); }
        }

        public string HCP11_UnitOrBasisForMeasurementCode
        {
            get { return this.Segment.GetElement(11); }
            set { this.Segment.SetElement(11, value); }
        }

        public UnitOrBasisOfMeasurementCode HCP11_UnitOrBasisOfMeasurementCodeEnum
        {
            get { return this.Segment.GetElement(11).ToEnumFromEdiFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { this.Segment.SetElement(11, value.EdiFieldValue()); }
        }

        public decimal? HCP12_Quantity
        {
            get { return this.Segment.GetDecimalElement(12); }
            set { this.Segment.SetElement(12, value); }
        }

        public string HCP13_RejectReasonCode
        {
            get { return this.Segment.GetElement(13); }
            set { this.Segment.SetElement(13, value); }
        }

        public string HCP14_PolicyComplianceCode
        {
            get { return this.Segment.GetElement(14); }
            set { this.Segment.SetElement(14, value); }
        }

        public string HCP15_ExceptionCode
        {
            get { return this.Segment.GetElement(15); }
            set { this.Segment.SetElement(15, value); }
        }
    }
}
