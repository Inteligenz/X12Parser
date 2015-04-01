using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentMIA : TypedSegment
    {
        public TypedSegmentMIA()
            : base("MIA")
        {
        }

        public decimal? MIA01_Quantity {
            get { return _segment.GetDecimalElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public decimal? MIA02_MonetaryAmount {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? MIA03_Quantity {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public decimal? MIA04_MonetaryAmount {
            get { return _segment.GetDecimalElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string MIA05_ReferenceIdentification {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public decimal? MIA06_MonetaryAmount {
            get { return _segment.GetDecimalElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public decimal? MIA07_MonetaryAmount {
            get { return _segment.GetDecimalElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public decimal? MIA08_MonetaryAmount {
            get { return _segment.GetDecimalElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public decimal? MIA09_MonetaryAmount {
            get { return _segment.GetDecimalElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public decimal? MIA10_MonetaryAmount {
            get { return _segment.GetDecimalElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public decimal? MIA11_MonetaryAmount {
            get { return _segment.GetDecimalElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public decimal? MIA12_MonetaryAmount {
            get { return _segment.GetDecimalElement(12); }
            set { _segment.SetElement(12, value); }
        }

        public decimal? MIA13_MonetaryAmount {
            get { return _segment.GetDecimalElement(13); }
            set { _segment.SetElement(13, value); }
        }

        public decimal? MIA14_MonetaryAmount {
            get { return _segment.GetDecimalElement(14); }
            set { _segment.SetElement(14, value); }
        }

        public decimal? MIA15_Quantity {
            get { return _segment.GetDecimalElement(15); }
            set { _segment.SetElement(15, value); }
        }

        public decimal? MIA16_MonetaryAmount {
            get { return _segment.GetDecimalElement(16); }
            set { _segment.SetElement(16, value); }
        }

        public decimal? MIA17_MonetaryAmount {
            get { return _segment.GetDecimalElement(17); }
            set { _segment.SetElement(17, value); }
        }

        public decimal? MIA18_MonetaryAmount {
            get { return _segment.GetDecimalElement(18); }
            set { _segment.SetElement(18, value); }
        }

        public decimal? MIA19_MonetaryAmount {
            get { return _segment.GetDecimalElement(19); }
            set { _segment.SetElement(19, value); }
        }

        public string MIA20_ReferenceIdentification {
            get { return _segment.GetElement(20); }
            set { _segment.SetElement(20, value); }
        }

        public string MIA21_ReferenceIdentification {
            get { return _segment.GetElement(21); }
            set { _segment.SetElement(21, value); }
        }

        public string MIA22_ReferenceIdentification {
            get { return _segment.GetElement(22); }
            set { _segment.SetElement(22, value); }
        }

        public string MIA23_ReferenceIdentification {
            get { return _segment.GetElement(23); }
            set { _segment.SetElement(23, value); }
        }

        public decimal? MIA24_MonetaryAmount {
            get { return _segment.GetDecimalElement(24); }
            set { _segment.SetElement(24, value); }
        }
    }
}
