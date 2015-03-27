using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Attributes;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentDTP : TypedSegment
    {

        public TypedSegmentDTP() : base("DTP") { }

        public string DTP01_DateTimeQualifier {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public DTPQualifier DTP01_DateTimeQualifierEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<DTPQualifier>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public string DTP02_DateTimePeriodFormatQualifier {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public DTPFormatQualifier DTP02_DateTimePeriodFormatQualifierEnum
        {
            get { return _segment.GetElement(2).ToEnumFromEDIFieldValue<DTPFormatQualifier>(); }
            set { _segment.SetElement(2, value.EDIFieldValue()); }
        }

        public DateTimePeriod DTP03_Date
        {
            get { return _segment.GetDateTimePeriodElement(3); }
            set { 
                _segment.SetElement(3, value); 
                // Also set the appropriate format qualifier
                if (value != null) {
                    this.DTP02_DateTimePeriodFormatQualifier = value.Qualifier;
                }
            }
        }

    }
}
