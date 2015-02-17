using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentHI : TypedSegment
    {
        public TypedSegmentHI() : base("HI")
        {
        }

        public string HI01_HealthCareCodeInformation
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string HI02_HealthCareCodeInformation
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }
        public string HI03_HealthCareCodeInformation
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string HI04_HealthCareCodeInformation
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }
        public string HI05_HealthCareCodeInformation
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string HI06_HealthCareCodeInformation
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }
        public string HI07_HealthCareCodeInformation
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string HI08_HealthCareCodeInformation
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }
        public string HI09_HealthCareCodeInformation
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string HI10_HealthCareCodeInformation
        {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }
        public string HI11_HealthCareCodeInformation
        {
            get { return _segment.GetElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public string HI12_HealthCareCodeInformation
        {
            get { return _segment.GetElement(12); }
            set { _segment.SetElement(12, value); }
        }
    }
}
