using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementRelatedCausesInfo
    {
        private int _elementNumber;
        private Segment _segment;
        private string _relatedCausesCode1;
        private string _relatedCausesCode2;
        private string _relatedCausesCode3;
        private string _stateOrProviceCode;
        private string _countryCode;

        internal TypedElementRelatedCausesInfo(Segment segment, int elementNumber)
        {
            _segment = segment;
            _elementNumber = elementNumber;
        }

        private void UpdateElement()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                    _segment._delimiters.SubElementSeparator,
                    _relatedCausesCode1, _relatedCausesCode2, _relatedCausesCode3, _stateOrProviceCode, _countryCode);
            value = value.TrimEnd(_segment._delimiters.SubElementSeparator);
            _segment.SetElement(_elementNumber, value);
        }

        public string _1_RelatedCausesCode
        {
            get { return _relatedCausesCode1; }
            set { 
                _relatedCausesCode1 = value; 
                UpdateElement(); 
            }
        }

        public string _2_RelatedCausesCode
        {
            get { return _relatedCausesCode2; }
            set
            {
                _relatedCausesCode2 = value;
                UpdateElement();
            }
        }

        public string _3_RelatedCausesCode
        {
            get { return _relatedCausesCode3; }
            set
            {
                _relatedCausesCode3 = value;
                UpdateElement();
            }
        }

        public string _4_StateOrProvidenceCode
        {
            get { return _stateOrProviceCode; }
            set
            {
                _stateOrProviceCode = value;
                UpdateElement();
            }
        }

        public string _5_CountryCode
        {
            get { return _countryCode; }
            set
            {
                _countryCode = value;
                UpdateElement();
            }
        }

    }
}
