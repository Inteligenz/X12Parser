
namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Total Monetary Value Summary
    /// </summary>
    public class TypedSegmentTS2 : TypedSegment
    {
        public TypedSegmentTS2()
            : base("TS2")
        {
        }
        public TypedSegmentTS2(Segment segment) : base(segment) { }

        public decimal? TS201_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public decimal? TS202_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? TS203_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public decimal? TS204_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public decimal? TS205_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public decimal? TS206_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public decimal? TS207_Quantity
        {
            get { return _segment.GetDecimalElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public decimal? TS208_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public decimal? TS209_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public decimal? TS210_Quantity
        {
            get { return _segment.GetDecimalElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public decimal? TS211_Quantity
        {
            get { return _segment.GetDecimalElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public decimal? TS212_Quantity
        {
            get { return _segment.GetDecimalElement(12); }
            set { _segment.SetElement(12, value); }
        }

        public decimal? TS213_Quantity
        {
            get { return _segment.GetDecimalElement(13); }
            set { _segment.SetElement(13, value); }
        }

        public decimal? TS214_Quantity
        {
            get { return _segment.GetDecimalElement(14); }
            set { _segment.SetElement(14, value); }
        }

        public decimal? TS215_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(15); }
            set { _segment.SetElement(15, value); }
        }

        public decimal? TS216_Quantity
        {
            get { return _segment.GetDecimalElement(16); }
            set { _segment.SetElement(16, value); }
        }

        public decimal? TS217_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(17); }
            set { _segment.SetElement(17, value); }
        }

        public decimal? TS218_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(18); }
            set { _segment.SetElement(18, value); }
        }

        public decimal? TS219_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(19); }
            set { _segment.SetElement(19, value); }
        }
    }
}
