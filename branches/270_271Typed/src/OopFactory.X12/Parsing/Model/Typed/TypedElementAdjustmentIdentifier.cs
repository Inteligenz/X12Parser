using System;
using System.Linq;
namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementAdjustmentIdentifier : BaseElementReference
    {
        public TypedElementAdjustmentIdentifier(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
            if (0 < SubElements.Count()) _1_AdjustmentReasonCode = SubElements.ElementAt(0);
            if (1 < SubElements.Count()) _2_ReferenceIdentification = SubElements.ElementAt(1);
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}",
                 Segment._delimiters.SubElementSeparator,
                 _1_AdjustmentReasonCode,
                 _2_ReferenceIdentification);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public string _1_AdjustmentReasonCode { get; set; }

        public string _2_ReferenceIdentification { get; set; }
    }
}
