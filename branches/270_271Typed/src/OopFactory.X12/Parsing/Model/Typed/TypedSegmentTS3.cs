using System;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Total Monetary Value Summary
    /// </summary>
    public class TypedSegmentTS3 : TypedSegment
    {
        public TypedSegmentTS3()
            : base("TS3")
        {
        }
        public TypedSegmentTS3(Segment segment) : base(segment) { }

        public string TS301_ReferenceIdentification
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string TS302_ReferenceIdentification
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public DateTime? TS303_Date
        {
            get { return _segment.GetDate8Element(3); }
            set { _segment.SetDate8Element(3, value); }
        }

        public int? TS304_Quantity
        {
            get { return _segment.GetIntElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public decimal? TS305_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public decimal? TS306_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public decimal? TS307_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public decimal? TS308_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public decimal? TS309_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public decimal? TS310_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public decimal? TS311_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public decimal? TS312_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(12); }
            set { _segment.SetElement(12, value); }
        }

        public decimal? TS313_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(13); }
            set { _segment.SetElement(13, value); }
        }

        public decimal? TS314_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(14); }
            set { _segment.SetElement(14, value); }
        }

        public decimal? TS315_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(15); }
            set { _segment.SetElement(15, value); }
        }

        public decimal? TS316_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(16); }
            set { _segment.SetElement(16, value); }
        }

        public decimal? TS317_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(17); }
            set { _segment.SetElement(17, value); }
        }

        public decimal? TS318_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(18); }
            set { _segment.SetElement(18, value); }
        }

        public decimal? TS319_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(19); }
            set { _segment.SetElement(19, value); }
        }

        public decimal? TS320_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(20); }
            set { _segment.SetElement(20, value); }
        }

        public decimal? TS321_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(21); }
            set { _segment.SetElement(21, value); }
        }

        public decimal? TS322_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(22); }
            set { _segment.SetElement(22, value); }
        }

        public decimal? TS323_Quantity
        {
            get { return _segment.GetDecimalElement(23); }
            set { _segment.SetElement(23, value); }
        }

        public decimal? TS324_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(24); }
            set { _segment.SetElement(24, value); }
        }

    }
}
