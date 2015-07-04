using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementRelatedCausesInfo : BaseElementReference
    {
        public TypedElementRelatedCausesInfo(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
            if (0 < SubElements.Count()) _1_RelatedCausesCode = SubElements.ElementAt(0);
            if (1 < SubElements.Count()) _2_RelatedCausesCode = SubElements.ElementAt(1);
            if (2 < SubElements.Count()) _3_RelatedCausesCode = SubElements.ElementAt(2);
            if (3 < SubElements.Count()) _4_StateOrProvidenceCode = SubElements.ElementAt(3);
            if (4 < SubElements.Count()) _5_CountryCode = SubElements.ElementAt(4);
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                Segment._delimiters.SubElementSeparator,
                _1_RelatedCausesCode,
                _2_RelatedCausesCode,
                _3_RelatedCausesCode,
                _4_StateOrProvidenceCode,
                _5_CountryCode);
            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public string _1_RelatedCausesCode { get; set; }
        public string _2_RelatedCausesCode { get; set; }
        public string _3_RelatedCausesCode { get; set; }
        public string _4_StateOrProvidenceCode { get; set; }
        public string _5_CountryCode { get; set; }
    }
}
