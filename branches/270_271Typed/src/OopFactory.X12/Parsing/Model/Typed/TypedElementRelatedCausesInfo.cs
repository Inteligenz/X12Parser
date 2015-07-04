using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementRelatedCausesInfo : BaseElementReference
    {
        private string _relatedCausesCode1;
        private string _relatedCausesCode2;
        private string _relatedCausesCode3;
        private string _stateOrProviceCode;
        private string _countryCode;

        public TypedElementRelatedCausesInfo(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                Segment._delimiters.SubElementSeparator,
                _relatedCausesCode1, _relatedCausesCode2, _relatedCausesCode3, _stateOrProviceCode, _countryCode);
            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public string _1_RelatedCausesCode
        {
            get { return _relatedCausesCode1; }
            set { _relatedCausesCode1 = value; }
        }

        public string _2_RelatedCausesCode
        {
            get { return _relatedCausesCode2; }
            set { _relatedCausesCode2 = value; }
        }

        public string _3_RelatedCausesCode
        {
            get { return _relatedCausesCode3; }
            set { _relatedCausesCode3 = value; }
        }

        public string _4_StateOrProvidenceCode
        {
            get { return _stateOrProviceCode; }
            set { _stateOrProviceCode = value; }
        }

        public string _5_CountryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; }
        }
    }
}
