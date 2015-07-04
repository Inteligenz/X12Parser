using System;
using System.Linq;
namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementCompositDiagnosisCodePointer : BaseElementReference
    {
        public TypedElementCompositDiagnosisCodePointer(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
            if (0 < SubElements.Count()) _1_DiagnosisCodePointer = Convert.ToInt32(SubElements.ElementAt(0));
            if (1 < SubElements.Count()) _2_DiagnosisCodePointer = Convert.ToInt32(SubElements.ElementAt(1));
            if (2 < SubElements.Count()) _3_DiagnosisCodePointer = Convert.ToInt32(SubElements.ElementAt(2));
            if (3 < SubElements.Count()) _4_DiagnosisCodePointer = Convert.ToInt32(SubElements.ElementAt(3));
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