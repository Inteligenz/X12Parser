namespace OopFactory.X12.Shared.Models.TypedSegments
{
    /// <summary>
    /// Pricing Infomration
    /// </summary>
    public class TypedSegmentCTP : TypedSegment
    {
        public TypedSegmentCTP()
            : base("CTP")
        {
        }

        public string CTP01_ClassOfTradeCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string CTP02_PriceIDCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public decimal? CTP03_UnitPrice
        {
            get { return this.Segment.GetDecimalElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public decimal? CTIP04_Quantity
        {
            get { return this.Segment.GetDecimalElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string CTP05_CompositeUnitOfMeasure
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string CTP06_PriceMultiplierQualifier
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public decimal? CTP07_Multiplier
        {
            get { return this.Segment.GetDecimalElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public decimal? CTP08_MonetaryAmount
        {
            get { return this.Segment.GetDecimalElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string CTP09_BaseUnitPriceCode
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }

        public string CTP10_ConditionValue
        {
            get { return this.Segment.GetElement(10); }
            set { this.Segment.SetElement(10, value); }
        }

        public int? CTP11_MultiplePriceQuantity
        {
            get { return this.Segment.GetIntElement(11); }
            set { this.Segment.SetElement(11, value); }
        }
    }
}
