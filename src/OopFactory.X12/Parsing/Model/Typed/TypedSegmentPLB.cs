using System;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentPLB : TypedSegment
    {
        public TypedSegmentPLB()
            : base("PLB")
        {
        }

        public TypedSegmentPLB(Segment segment) : base(segment) { }

        public string PLB01_ReferenceIdentification
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public DateTime? PLB02_Date
        {
            get { return _segment.GetDate8Element(2); }
            set { _segment.SetDate8Element(2, value); }
        }

        public TypedElementAdjustmentIdentifier PLB03_AdjustmentIdentifier
        {
            get { return new TypedElementAdjustmentIdentifier(_segment, 3); }
            set { _segment.SetElement(3, value); }
        }

        public decimal? PLB04_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(4); }
            set { _segment.SetElement(3, value); }
        }

        public TypedElementAdjustmentIdentifier PLB05_AdjustmentIdentifier
        {
            get { return new TypedElementAdjustmentIdentifier(_segment, 5); }
            set { _segment.SetElement(5, value); }
        }

        public decimal? PLB06_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public TypedElementAdjustmentIdentifier PLB07_AdjustmentIdentifier
        {
            get { return new TypedElementAdjustmentIdentifier(_segment, 7); }
            set { _segment.SetElement(7, value); }
        }

        public decimal? PLB08_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public TypedElementAdjustmentIdentifier PLB09_AdjustmentIdentifier
        {
            get { return new TypedElementAdjustmentIdentifier(_segment, 9); }
            set { _segment.SetElement(9, value); }
        }

        public decimal? PLB10_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public TypedElementAdjustmentIdentifier PLB11_AdjustmentIdentifier
        {
            get { return new TypedElementAdjustmentIdentifier(_segment, 11); }
            set { _segment.SetElement(11, value); }
        }

        public decimal? PLB12_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(12); }
            set { _segment.SetElement(12, value); }
        }

        public TypedElementAdjustmentIdentifier PLB13_AdjustmentIdentifier
        {
            get { return new TypedElementAdjustmentIdentifier(_segment, 13); }
            set { _segment.SetElement(13, value); }
        }

        public decimal? PLB14_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(14); }
            set { _segment.SetElement(14, value); }
        }
    }
}
