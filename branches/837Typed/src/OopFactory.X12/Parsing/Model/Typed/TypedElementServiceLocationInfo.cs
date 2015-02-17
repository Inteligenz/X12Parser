using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementServiceLocationInfo
    {
        private int _elementNumber;
        private Segment _segment;
        private string _facilityCodeValue;
        private string _facilityCodeQualifier;
        private string _claimFrequencyTypeCode;

        internal TypedElementServiceLocationInfo(Segment segment, int elementNumber)
        {
            _segment = segment;
            _elementNumber = elementNumber;
        }

        private void UpdateElement()
        {
            string value = String.Format("{1}{0}{2}{0}{3}",
                _segment._delimiters.SubElementSeparator,
                _facilityCodeValue, _facilityCodeQualifier, _claimFrequencyTypeCode);
            value = value.TrimEnd(_segment._delimiters.SubElementSeparator);
            _segment.SetElement(_elementNumber, value);
        }

        public string _1_FacilityCodeValue
        {
            get { return _facilityCodeValue; }
            set
            {
                _facilityCodeValue = value;
                UpdateElement();
            }
        }

        public string _2_FacilityCodeQualifier
        {
            get { return _2_FacilityCodeQualifier; }
            set
            {
                _facilityCodeQualifier = value;
                UpdateElement();
            }
        }

        public string _3_ClaimFrequencyTypeCode
        {
            get { return _claimFrequencyTypeCode; }
            set
            {
                _claimFrequencyTypeCode = value;
                UpdateElement();
            }
        }

    }
}
