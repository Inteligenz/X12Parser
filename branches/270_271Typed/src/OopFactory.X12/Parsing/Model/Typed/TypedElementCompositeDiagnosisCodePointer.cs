using System;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementCompositDiagnosisCodePointer : BaseElementReference
    {
        private int? _diagnosisCodePointer1;
        private int? _diagnosisCodePointer2;
        private int? _diagnosisCodePointer3;
        private int? _diagnosisCodePointer4;

        public TypedElementCompositDiagnosisCodePointer(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}",
                 Segment._delimiters.SubElementSeparator,
                 _diagnosisCodePointer1,
                 _diagnosisCodePointer2,
                 _diagnosisCodePointer3,
                 _diagnosisCodePointer4);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public int? _1_DiagnosisCodePointer
        {
            get { return _diagnosisCodePointer1; }
            set { _diagnosisCodePointer1 = value; }
        }

        public int? _2_DiagnosisCodePointer
        {
            get { return _diagnosisCodePointer2; }
            set { _diagnosisCodePointer2 = value; }
        }

        public int? _3_DiagnosisCodePointer
        {
            get { return _diagnosisCodePointer3; }
            set { _diagnosisCodePointer3 = value; }
        }

        public int? _4_DiagnosisCodePointer
        {
            get { return _diagnosisCodePointer4; }
            set { _diagnosisCodePointer4 = value; }
        }
    }
}