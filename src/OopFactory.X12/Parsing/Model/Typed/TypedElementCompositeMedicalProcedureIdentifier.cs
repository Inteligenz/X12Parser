using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementCompositeMedicalProcedureIdentifier : BaseElementReference
    {

        public TypedElementCompositeMedicalProcedureIdentifier(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
            Segment = segment;
            ElementNumber = elementNumber;
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                Segment._delimiters.SubElementSeparator,
                _1_ProductOrServiceIdQualifier.EDIFieldValue(),
                _2_ProcedureCode,
                _3_ProcedureModifier,
                _4_ProcedureModifier,
                _5_ProcedureModifier,
                _6_ProcedureModifier,
                _7_Description,
                _8_ProductOrServiceId);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public ProductOrServiceIdQualifiers _1_ProductOrServiceIdQualifier { get; set; }

        public string _2_ProcedureCode { get; set; }

        public string _3_ProcedureModifier { get; set; }

        public string _4_ProcedureModifier { get; set; }

        public string _5_ProcedureModifier { get; set; }

        public string _6_ProcedureModifier { get; set; }

        public string _7_Description { get; set; }

        public string _8_ProductOrServiceId { get; set; }
    }
}