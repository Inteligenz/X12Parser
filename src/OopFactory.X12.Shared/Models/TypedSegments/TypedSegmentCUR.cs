namespace OopFactory.X12.Shared.Models.TypedSegments
{
    /// <summary>
    /// Currency
    /// </summary>
    public class TypedSegmentCUR : TypedSegment
    {
        public TypedSegmentCUR()
            : base("CUR")
        {
        }

        /// <summary>
        /// BY = Buying Party (Purchaser)
        /// SE = Selling Party
        /// </summary>
        public string CUR01_EntityIdentifierCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        /// <summary>
        /// CAD = Canadian dollars
        /// USD = US Dollars
        /// </summary>
        public string CUR02_CurrencyCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public decimal? CUR03_ExchangeRate
        {
            get { return this.Segment.GetDecimalElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string CUR04_EntityIdentifierCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string CUR05_CurrencyCode
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string CUR06_CurrencyMarketExchangeCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }
    }
}
