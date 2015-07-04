using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementHealthCareCodeInformation : BaseElementReference
    {
        public TypedElementHealthCareCodeInformation(Model.Segment _segment, int _elementNumber)
            : base(_segment, _elementNumber)
        {
        }

        public override string ToString()
        {
            string dateTimePeriod = string.Empty;

            if (_4_DateTimePeriod != null)
            {
                dateTimePeriod = _4_DateTimePeriod.ToString();
            }

            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}",
                Segment._delimiters.SubElementSeparator,
                _1_CodeListQualifierCode.EDIFieldValue(),
                _2_IndustryCode,
                _3_DateTimePeriodFormatQualifierEnum.EDIFieldValueSafe(),
                dateTimePeriod,
                _5_MonetaryAmount,
                _6_Quantity,
                _7_VersionIdentifier,
                _8_IndustryCode,
                _9_IndustryCode);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public CodeListQualifierCode _1_CodeListQualifierCode { get; set; }

        public string _2_IndustryCode { get; set; }
        
        public DTPQualifier? _3_DateTimePeriodFormatQualifierEnum { get; set; }

        public DateTimePeriod _4_DateTimePeriod { get; set; }

        public decimal? _5_MonetaryAmount { get; set; }

        public decimal? _6_Quantity { get; set; }

        public string _7_VersionIdentifier { get; set; }

        public string _8_IndustryCode { get; set; }

        public string _9_IndustryCode { get; set; }
    }
}