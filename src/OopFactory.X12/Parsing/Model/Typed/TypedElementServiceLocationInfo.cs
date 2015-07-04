using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementServiceLocationInfo : BaseElementReference
    {
        private PlaceOfServiceCodes _facilityCodeValue;
        private string _facilityCodeQualifier;
        private string _claimFrequencyTypeCode;

        public TypedElementServiceLocationInfo(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}",
                Segment._delimiters.SubElementSeparator,
                _facilityCodeValue.EDIFieldValue(),
                _facilityCodeQualifier,
                _claimFrequencyTypeCode);
            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public PlaceOfServiceCodes _1_FacilityCodeValue
        {
            get { return _facilityCodeValue; }
            set { _facilityCodeValue = value; }
        }

        public string _2_FacilityCodeQualifier
        {
            get { return _2_FacilityCodeQualifier; }
            set { _facilityCodeQualifier = value; }
        }

        public string _3_ClaimFrequencyTypeCode
        {
            get { return _claimFrequencyTypeCode; }
            set { _claimFrequencyTypeCode = value; }
        }
    }
}
