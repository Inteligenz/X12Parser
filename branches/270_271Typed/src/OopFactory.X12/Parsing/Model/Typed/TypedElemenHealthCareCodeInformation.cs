using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Linq;
namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementHealthCareCodeInformation : BaseElementReference
    {
        public TypedElementHealthCareCodeInformation(Model.Segment _segment, int _elementNumber)
            : base(_segment, _elementNumber)
        {
            if (0 < SubElements.Count()) _1_CodeListQualifierCode = SubElements.ElementAt(0).ToEnumFromEDIFieldValue<CodeListQualifierCode>();
            if (1 < SubElements.Count()) _2_IndustryCode = SubElements.ElementAt(1);
            if (2 < SubElements.Count()) _3_DateTimePeriodFormatQualifierEnum = SubElements.ElementAt(2).ToEnumFromEDIFieldValue<DTPQualifier>();
            if (3 < SubElements.Count()) _4_DateTimePeriod = new DateTimePeriod(SubElements.ElementAt(3));
            if (4 < SubElements.Count()) _5_MonetaryAmount = Convert.ToDecimal(SubElements.ElementAt(4));
            if (5 < SubElements.Count()) _6_Quantity = Convert.ToDecimal(SubElements.ElementAt(5));
            if (6 < SubElements.Count()) _7_VersionIdentifier = SubElements.ElementAt(6);
            if (7 < SubElements.Count()) _8_IndustryCode = SubElements.ElementAt(7);
            if (8 < SubElements.Count()) _9_IndustryCode = SubElements.ElementAt(8);
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