using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Pricing Infomration
    /// </summary>
    public class TypedSegmentCTP : TypedSegment
    {
        public TypedSegmentCTP()
            : base("CTP")
        { }

        public string CTP01_ClassOfTradeCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string CTP02_PriceIDCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? CTP03_UnitPrice
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public decimal? CTIP04_Quantity
        {
            get { return _segment.GetDecimalElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string CTP05_CompositeUnitOfMeasure
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string CTP06_PriceMultiplierQualifier
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public decimal? CTP07_Multiplier
        {
            get { return _segment.GetDecimalElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public decimal? CTP08_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string CTP09_BaseUnitPriceCode
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string CTP10_ConditionValue
        {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public int? CTP11_MultiplePriceQuantity
        {
            get { return _segment.GetIntElement(11); }
            set { _segment.SetElement(11, value); }
        }
    }
}
