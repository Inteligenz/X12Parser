using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed {
    public class TypedElementCompositeMedicalProcedureIdentifier {
        private int _elementNumber;
        private Segment _segment;

        private string _productOrServiceIdQualifier;
        private string _procedureCode;
        private string _procedureModifier1;
        private string _procedureModifier2;
        private string _procedureModifier3;
        private string _procedureModifier4;
        private string _description;
        private string _productOrServiceId;

        internal TypedElementCompositeMedicalProcedureIdentifier(Segment segment, int elementNumber) {
            _segment = segment;
            _elementNumber = elementNumber;
        }

        private void UpdateElement() {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                _segment._delimiters.SubElementSeparator,
                _productOrServiceIdQualifier,
                _procedureCode,
                _procedureModifier1,
                _procedureModifier2,
                _procedureModifier3,
                _procedureModifier4,
                _description,
                _productOrServiceId);

            value = value.TrimEnd(_segment._delimiters.SubElementSeparator);

            _segment.SetElement(_elementNumber, value);
        }

        public string _1_ProductOrServiceIdQualifier {
            get { return _productOrServiceIdQualifier; }
            set {
                _productOrServiceIdQualifier = value;
                UpdateElement();
            }
        }

        public string _2_ProcedureCode {
            get { return _procedureCode; }
            set {
                _procedureCode = value;
                UpdateElement();
            }
        }

        public string _3_ProcedureModifier {
            get { return _procedureModifier1; }
            set {
                _procedureModifier1 = value;
                UpdateElement();
            }
        }

        public string _4_ProcedureModifier {
            get { return _procedureModifier2; }
            set {
                _procedureModifier2 = value;
                UpdateElement();
            }
        }

        public string _5_ProcedureModifier {
            get { return _procedureModifier3; }
            set {
                _procedureModifier3 = value;
                UpdateElement();
            }
        }

        public string _6_ProcedureModifier {
            get { return _procedureModifier4; }
            set {
                _procedureModifier4 = value;
                UpdateElement();
            }
        }

        public string _7_Description {
            get { return _description; }
            set {
                _description = value;
                UpdateElement();
            }
        }

        public string _8_ProductOrServiceId {
            get { return _productOrServiceId; }
            set {
                _productOrServiceId = value;
                UpdateElement();
            }
        }
    }
}