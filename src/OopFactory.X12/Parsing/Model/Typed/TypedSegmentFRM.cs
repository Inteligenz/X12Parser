using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentFRM : TypedSegment
    {
        public TypedSegmentFRM()
            : base("FRM")
        {
        }

        public TypedSegmentFRM(Segment segment) : base(segment) { }

        public string FRM01_AssignedIdentification
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string FRM02_YesNoCondRespCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string FRM03_ReferenceIdentification {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public DateTime? FRM04_Date {
            get {
                string element = _segment.GetElement(4);
                if (element.Length == 8)
                    return DateTime.ParseExact(element, "yyyyMMdd", null);
                else
                    return null;
            }
            set {
                _segment.SetElement(4, String.Format("{0:yyyyMMdd}", value));
            }
        }

        public decimal? FRM05_PercentDecimalFormat {
            get { return _segment.GetDecimalElement(5); }
            set { _segment.SetElement(5, value); }
        }
    }
}
