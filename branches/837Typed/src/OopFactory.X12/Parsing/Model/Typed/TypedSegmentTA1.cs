using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentTA1 : TypedSegment
    {
        public TypedSegmentTA1()
            : base("TA1")
        {
        }

        public string TA101_InterchangeControlNumber
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public DateTime? TA102_Date {
            get {
                // Old school ISA format: yyMMdd
                string element = _segment.GetElement(2);

                if (string.IsNullOrWhiteSpace(element))
                    return null;

                DateTime d;

                if (!DateTime.TryParseExact(element, "yyMMdd", null, System.Globalization.DateTimeStyles.None, out d)) {
                    return null;
                }

                return d; 
            }
            set {
                // Old school ISA format: yyMMdd
                if (value == null || value.HasValue == false) {
                    _segment.SetElement(2, string.Empty);
                }

                _segment.SetElement(2, value.Value.ToString("yyMMdd")); 
            }
        }

        public TimeSpan? TA103_Time {
            get {
                string element = _segment.GetElement(3);

                if (string.IsNullOrWhiteSpace(element))
                    return null;

                // TimeSpan TryParse in Mono seems broken. Manually do it.
                int length = element.Length;
                string sHours = null;
                string sMin = null;
                TimeSpan ts; 

                // HMM Time: 800
                if (length == 3) {
                    sHours = element.Substring(0, 1);
                    sMin = element.Substring(1);
                }

                // HHMM Time: 0800
                if (length == 4) {
                    // TimeSpan TryParse in Mono seems broken. Manually do it.
                    sHours = element.Substring(0, 2);
                    sMin = element.Substring(2);

                }

                int iHours = 0;
                int iMin = 0;

                if (!int.TryParse(sHours, out iHours)) {
                    return null;
                }

                if (!int.TryParse(sMin, out iMin)) {
                    return null;
                }

                try {
                    ts = new TimeSpan(iHours, iMin, 0);
                } catch (Exception) {
                    return null;
                }

                return new TimeSpan?(ts);
            } set {
                if (value == null || value.HasValue == false) {
                    _segment.SetElement(3, string.Empty);
                }

                _segment.SetElement(3, String.Format("{0:hhmm}", value)); 
            }
        }

        public string TA104_InterchangeAcknowledgmentCode {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string TA105_InterchangeNoteCode {
            get { return _segment.GetElement(5); }
            set {
                _segment.SetElement(5, String.Format("{0,-3}", value)); 
            }
        }
    }
}
