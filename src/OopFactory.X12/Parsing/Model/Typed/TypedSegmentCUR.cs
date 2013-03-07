using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
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
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        /// <summary>
        /// CAD = Canadian dollars
        /// USD = US Dollars
        /// </summary>
        public string CUR02_CurrencyCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? CUR03_ExchangeRate
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string CUR04_EntityIdentifierCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string CUR05_CurrencyCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string CUR06_CurrencyMarketExchangeCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }
    }
}
