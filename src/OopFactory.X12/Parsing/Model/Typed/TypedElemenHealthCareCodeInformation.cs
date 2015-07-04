using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementHealthCareCodeInformation : BaseElementReference
    {
        private CodeListQualifierCode _codeListQualifierCode;
        private string _industryCode;
        private string _dateTimePeriodFormatQualifier;
        private DateTimePeriod _dateTimePeriod;
        private decimal? _monetaryAmount;
        private decimal? _quantity;
        private string _versionIdentifier;
        private string _industryCode1;
        private string _industryCode2;

        public TypedElementHealthCareCodeInformation(Model.Segment _segment, int _elementNumber)
            : base(_segment, _elementNumber)
        {
        }

        public override string ToString()
        {
            string dateTimePeriod = string.Empty;

            if (_dateTimePeriod != null)
            {
                dateTimePeriod = _dateTimePeriod.ToString();
            }

            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}",
                Segment._delimiters.SubElementSeparator,
                _codeListQualifierCode.EDIFieldValue(),
                _industryCode,
                _dateTimePeriodFormatQualifier,
                dateTimePeriod,
                _monetaryAmount,
                _quantity,
                _versionIdentifier,
                _industryCode1,
                _industryCode2);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public CodeListQualifierCode _1_CodeListQualifierCode
        {
            get { return _codeListQualifierCode; }
            set
            {
                _codeListQualifierCode = value;
                
            }
        }

        public string _2_IndustryCode
        {
            get { return _industryCode; }
            set
            {
                _industryCode = value;
                
            }
        }

        public string _3_DateTimePeriodFormatQualifier
        {
            get { return _dateTimePeriodFormatQualifier; }
            set
            {
                _dateTimePeriodFormatQualifier = value;
                
            }
        }

        public DTPQualifier _3_DateTimePeriodFormatQualifierEnum
        {
            get { return _dateTimePeriodFormatQualifier.ToEnumFromEDIFieldValue<DTPQualifier>(); }
            set
            {
                _dateTimePeriodFormatQualifier = value.EDIFieldValue();
                
            }
        }

        public DateTimePeriod _4_DateTimePeriod
        {
            get { return _dateTimePeriod; }
            set
            {
                _dateTimePeriod = value;
                
            }
        }

        public decimal? _5_MonetaryAmount
        {
            get { return _monetaryAmount; }
            set
            {
                _monetaryAmount = value;
                
            }
        }

        public decimal? _6_Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                
            }
        }

        public string _7_VersionIdentifier
        {
            get { return _versionIdentifier; }
            set
            {
                _versionIdentifier = value;
                
            }
        }

        public string _8_IndustryCode
        {
            get { return _industryCode1; }
            set
            {
                _industryCode1 = value;
                
            }
        }

        public string _9_IndustryCode
        {
            get { return _industryCode2; }
            set
            {
                _industryCode2 = value;
                
            }
        }
    }
}