using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentMOA : TypedSegment
    {
        public TypedSegmentMOA()
            : base("MOA")
        {
        }
        public TypedSegmentMOA(Segment segment) : base(segment) { }

        public decimal? MOA01_Percentage
        {
            get { return _segment.GetDecimalElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public decimal? MOA02_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string MOA03_ReferenceIdentification
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string MOA04_ReferenceIdentification
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string MOA05_ReferenceIdentification
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string MOA06_ReferenceIdentification
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string MOA07_ReferenceIdentification
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public decimal? MOA08_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public decimal? MOA09_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(9); }
            set { _segment.SetElement(9, value); }
        }
    }
}
