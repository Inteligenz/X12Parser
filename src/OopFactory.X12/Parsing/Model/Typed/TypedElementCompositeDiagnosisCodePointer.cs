using System;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementCompositDiagnosisCodePointer : BaseElementReference
    {
        public TypedElementCompositDiagnosisCodePointer(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}",
                 Segment._delimiters.SubElementSeparator,
                 _1_DiagnosisCodePointer,
                 _2_DiagnosisCodePointer,
                 _3_DiagnosisCodePointer,
                 _4_DiagnosisCodePointer);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public int? _1_DiagnosisCodePointer { get; set; }

        public int? _2_DiagnosisCodePointer { get; set; }

        public int? _3_DiagnosisCodePointer { get; set; }

        public int? _4_DiagnosisCodePointer { get; set; }
    }
}