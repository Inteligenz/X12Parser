using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentCAS : TypedSegment
    {
        public TypedSegmentCAS()
            : base("CAS")
        {
        }
        public TypedSegmentCAS(Segment seg) : base(seg) { }

        public string CAS01_ClaimAdjustmentGroupCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string CAS02_ClaimAdjustmentReasonCode {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? CAS03_MonetaryAmount {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public decimal? CAS04_Quantity {
            get { return _segment.GetDecimalElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string CAS05_ClaimAdjustmentReasonCode {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public decimal? CAS06_MonetaryAmount {
            get { return _segment.GetDecimalElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public decimal? CAS07_Quantity {
            get { return _segment.GetDecimalElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string CAS08_ClaimAdjustmentReasonCode {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public decimal? CAS09_MonetaryAmount {
            get { return _segment.GetDecimalElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public decimal? CAS10_Quantity {
            get { return _segment.GetDecimalElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public string CAS11_ClaimAdjustmentReasonCode {
            get { return _segment.GetElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public decimal? CAS12_MonetaryAmount {
            get { return _segment.GetDecimalElement(12); }
            set { _segment.SetElement(12, value); }
        }

        public decimal? CAS13_Quantity {
            get { return _segment.GetDecimalElement(13); }
            set { _segment.SetElement(13, value); }
        }

        public string CAS14_ClaimAdjustmentReasonCode {
            get { return _segment.GetElement(14); }
            set { _segment.SetElement(14, value); }
        }

        public decimal? CAS15_MonetaryAmount {
            get { return _segment.GetDecimalElement(15); }
            set { _segment.SetElement(15, value); }
        }

        public decimal? CAS16_Quantity {
            get { return _segment.GetDecimalElement(16); }
            set { _segment.SetElement(16, value); }
        }

        public string CAS17_ClaimAdjustmentReasonCode {
            get { return _segment.GetElement(17); }
            set { _segment.SetElement(17, value); }
        }

        public decimal? CAS18_MonetaryAmount {
            get { return _segment.GetDecimalElement(18); }
            set { _segment.SetElement(18, value); }
        }

        public decimal? CAS19_Quantity {
            get { return _segment.GetDecimalElement(19); }
            set { _segment.SetElement(19, value); }
        }
    }
}
