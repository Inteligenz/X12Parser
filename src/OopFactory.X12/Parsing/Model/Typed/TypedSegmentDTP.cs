using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
#if DEBUG
    public class TypedSegmentDTP : TypedSegment
    {
        public TypedSegmentDTP() : base("DTP"){}

        public string DTP01_DateTimeQualifier
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }       
        }

        public string DTP02_DateTimePeriodFormatQualifier
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }              
        }

        public DateTime? DTP03_Date
        {
            get
            {
                string element = _segment.GetElement(3);
                if (element.Length == 8)
                    return DateTime.ParseExact(element, "yyyyMMdd", null);
                return null;
            }
            set
            {
                _segment.SetElement(3, String.Format("{0:yyyyMMdd}", value));
            }
        }

    }
#endif
    

}
