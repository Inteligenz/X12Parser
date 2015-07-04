using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Linq;
namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementHealthCareClaimStatus : BaseElementReference
    {
        public TypedElementHealthCareClaimStatus(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
            if (0 < SubElements.Count()) _1_IndustryCode = SubElements.ElementAt(0);
            if (1 < SubElements.Count()) _2_IndustryCode = SubElements.ElementAt(1);
            if (2 < SubElements.Count()) _3_EntityIdentifierCode = SubElements.ElementAt(2).ToEnumFromEDIFieldValue<EntityIdentifierCode>();
        }

        public string _1_IndustryCode { get; set; }
        public string _2_IndustryCode { get; set; }
        private EntityIdentifierCode? _3_EntityIdentifierCode { get; set; }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}",
                Segment._delimiters.SubElementSeparator,
                _1_IndustryCode,
                _2_IndustryCode,
                _3_EntityIdentifierCode.EDIFieldValueSafe());

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }
    }
}
