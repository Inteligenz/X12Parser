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
