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
        public TypedElementServiceLocationInfo(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
            if (0 < SubElements.Count()) _1_FacilityCodeValue = SubElements.ElementAt(0).ToEnumFromEDIFieldValue<PlaceOfServiceCodes>();
            if (1 < SubElements.Count()) _2_FacilityCodeQualifier = SubElements.ElementAt(1);
            if (2 < SubElements.Count()) _3_ClaimFrequencyTypeCode = SubElements.ElementAt(2);
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}",
                Segment._delimiters.SubElementSeparator,
                _1_FacilityCodeValue.EDIFieldValue(),
                _2_FacilityCodeQualifier,
                _3_ClaimFrequencyTypeCode);
            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public PlaceOfServiceCodes _1_FacilityCodeValue { get; set; }

        public string _2_FacilityCodeQualifier { get; set; }

        public string _3_ClaimFrequencyTypeCode { get; set; }
    }
}
