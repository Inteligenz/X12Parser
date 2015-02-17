using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed {
    public class TypedElementCompositDiagnosisCodePointer {
        private int _elementNumber;
        private Segment _segment;

        private string _diagnosisCodePointer1;
        private string _diagnosisCodePointer2;
        private string _diagnosisCodePointer3;
        private string _diagnosisCodePointer4;

        internal TypedElementCompositDiagnosisCodePointer(Segment segment, int elementNumber) {
            _segment = segment;
            _elementNumber = elementNumber;
        }

        private void UpdateElement() {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}",
                _segment._delimiters.SubElementSeparator,
                _diagnosisCodePointer1,
                _diagnosisCodePointer2,
                _diagnosisCodePointer3,
                _diagnosisCodePointer4);

            value = value.TrimEnd(_segment._delimiters.SubElementSeparator);

            _segment.SetElement(_elementNumber, value);
        }

        public string _1_DiagnosisCodePointer {
            get { return _diagnosisCodePointer1; }
            set {
                _diagnosisCodePointer1 = value;
                UpdateElement();
            }
        }

        public string _2_DiagnosisCodePointer {
            get { return _diagnosisCodePointer2; }
            set {
                _diagnosisCodePointer2 = value;
                UpdateElement();
            }
        }

        public string _3_DiagnosisCodePointer {
            get { return _diagnosisCodePointer3; }
            set {
                _diagnosisCodePointer3 = value;
                UpdateElement();
            }
        }

        public string _4_DiagnosisCodePointer {
            get { return _diagnosisCodePointer4; }
            set {
                _diagnosisCodePointer4 = value;
                UpdateElement();
            }
        }
    }
}