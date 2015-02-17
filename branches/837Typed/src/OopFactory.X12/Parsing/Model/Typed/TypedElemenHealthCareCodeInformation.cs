using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed {
    public class TypedElementHealthCareCodeInformation {
        private int _elementNumber;
        private Segment _segment;

        private string _codeListQualifierCode;
        private string _industryCode;
        private string _dateTimePeriodFormatQualifier;
        private DateTimePeriod _dateTimePeriod;
        private decimal? _monetaryAmount;
        private decimal? _quantity;
        private string _versionIdentifier;
        private string _industryCode1;
        private string _industryCode2;

        internal TypedElementHealthCareCodeInformation(Segment segment, int elementNumber) {
            _segment = segment;
            _elementNumber = elementNumber;
        }

        private void UpdateElement() {
            string dateTimePeriod = string.Empty;

            if (_dateTimePeriod != null) {
                dateTimePeriod = _dateTimePeriod.ToString();
            }

            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}",
                _segment._delimiters.SubElementSeparator,
                _codeListQualifierCode,
                _industryCode,
                _dateTimePeriodFormatQualifier,
                dateTimePeriod,
                _monetaryAmount,
                _quantity,
                _versionIdentifier,
                _industryCode1,
                _industryCode2);

            value = value.TrimEnd(_segment._delimiters.SubElementSeparator);

            _segment.SetElement(_elementNumber, value);
        }

        public string _1_CodeListQualifierCode {
            get { return _codeListQualifierCode; }
            set {
                _codeListQualifierCode = value;
                UpdateElement();
            }
        }

        public string _2_IndustryCode {
            get { return _industryCode; }
            set {
                _industryCode = value;
                UpdateElement();
            }
        }

        public string _3_DateTimePeriodFormatQualifier {
            get { return _dateTimePeriodFormatQualifier; }
            set {
                _dateTimePeriodFormatQualifier = value;
                UpdateElement();
            }
        }

        public DTPQualifier _3_DateTimePeriodFormatQualifierEnum {
            get { return _dateTimePeriodFormatQualifier.ToEnumFromEDIFieldValue<DTPQualifier>(); }
            set { 
                _dateTimePeriodFormatQualifier = value.EDIFieldValue();
                UpdateElement();
            }
        }

        public DateTimePeriod _4_DateTimePeriod {
            get { return _dateTimePeriod; }
            set {
                _dateTimePeriod = value;
                UpdateElement();
            }
        }

        public decimal? _5_MonetaryAmount {
            get { return _monetaryAmount; }
            set {
                _monetaryAmount = value;
                UpdateElement();
            }
        }

        public decimal? _6_Quantity {
            get { return _quantity; }
            set {
                _quantity = value;
                UpdateElement();
            }
        }

        public string _7_VersionIdentifier {
            get { return _versionIdentifier; }
            set {
                _versionIdentifier = value;
                UpdateElement();
            }
        }

        public string _8_IndustryCode {
            get { return _industryCode1; }
            set {
                _industryCode1 = value;
                UpdateElement();
            }
        }

        public string _9_IndustryCode {
            get { return _industryCode2; }
            set {
                _industryCode2 = value;
                UpdateElement();
            }
        }
    }
}