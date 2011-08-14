using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
#if DEBUG
    public class TypedSegmentSV1 : TypedSegment
    {
        public TypedSegmentSV1() : base("SV1")
        {
        }

        public string SV101_HealthCareCodeInformation
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }


        public string SV103_HealthCareCodeInformation
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string SV104_HealthCareCodeInformation
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }
        public string SV105_HealthCareCodeInformation
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string SV107_HealthCareCodeInformation
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string SV109_HealthCareCodeInformation
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string SV110_HealthCareCodeInformation
        {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public string SV112_HealthCareCodeInformation
        {
            get { return _segment.GetElement(12); }
            set { _segment.SetElement(12, value); }
        }        
        
        public string SV115_HealthCareCodeInformation
        {
            get { return _segment.GetElement(15); }
            set { _segment.SetElement(15, value); }
        }
    }
#endif
}
