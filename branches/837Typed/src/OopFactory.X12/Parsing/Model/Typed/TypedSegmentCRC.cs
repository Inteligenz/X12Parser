using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentCRC : TypedSegment
    {
        public TypedSegmentCRC()
            : base("CRC")
        {
        }

        public string CRC01_CodeCategory
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string CRC02_CertificationConditionIndicator {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string CRC03_ConditionCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string CRC04_ConditionCode {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string CRC05_ConditionCode {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string CRC06_ConditionCode {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string CRC07_ConditionCode {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }
    }
}
