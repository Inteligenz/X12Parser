using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentCR3 : TypedSegment
    {
        public TypedSegmentCR3()
            : base("CR3")
        {
        }

        public TypedSegmentCR3(Segment seg) : base(seg) { }

        public string CR301_CertificationTypeCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string CR302_UnitBasisMeasCode {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? CR303_DurableMedicalEquipmentDuration
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string CR304_InsulinDependentCode {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string CR305_Description {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }
    }
}
