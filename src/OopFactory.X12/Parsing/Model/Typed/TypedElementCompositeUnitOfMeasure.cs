using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed {
    public class TypedElementCompositeUnitOfMeasure {
        private int _elementNumber;
        private Segment _segment;

        private string _unitOrBasisMeasCode1;
        private string _exponent1;
        private string _multiplier1;

        private string _unitOrBasisMeasCode2;
        private string _exponent2;
        private string _multiplier2;

        private string _unitOrBasisMeasCode3;
        private string _exponent3;
        private string _multiplier3;

        private string _unitOrBasisMeasCode4;
        private string _exponent4;
        private string _multiplier4;

        private string _unitOrBasisMeasCode5;
        private string _exponent5;
        private string _multiplier5;

        internal TypedElementCompositeUnitOfMeasure(Segment segment, int elementNumber) {
            _segment = segment;
            _elementNumber = elementNumber;
        }

        private void UpdateElement() {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}{0}{11}{0}{12}{0}{13}{0}{14}{0}{15}",
                _segment._delimiters.SubElementSeparator,
                _unitOrBasisMeasCode1,
                _exponent1,
                _multiplier1,
                _unitOrBasisMeasCode2,
                _exponent2,
                _multiplier2,
                _unitOrBasisMeasCode3,
                _exponent3,
                _multiplier3,
                _unitOrBasisMeasCode4,
                _exponent4,
                _multiplier4,
                _unitOrBasisMeasCode5,
                _exponent5,
                _multiplier5);

            value = value.TrimEnd(_segment._delimiters.SubElementSeparator);

            _segment.SetElement(_elementNumber, value);
        }

        public string _1_UnitOrBasisMeasCode {
            get { return _unitOrBasisMeasCode1; }
            set {
                _unitOrBasisMeasCode1 = value;
                UpdateElement();
            }
        }

        public string _2_Exponent1 {
            get { return _exponent1; }
            set {
                _exponent1 = value;
                UpdateElement();
            }
        }

        public string _3_Multiplier {
            get { return _multiplier1; }
            set {
                _multiplier1 = value;
                UpdateElement();
            }
        }

        public string _4_UnitOrBasisMeasCode {
            get { return _unitOrBasisMeasCode2; }
            set {
                _unitOrBasisMeasCode2 = value;
                UpdateElement();
            }
        }

        public string _5_Exponent2 {
            get { return _exponent2; }
            set {
                _exponent2 = value;
                UpdateElement();
            }
        }

        public string _6_Multiplier {
            get { return _multiplier2; }
            set {
                _multiplier2 = value;
                UpdateElement();
            }
        }

        public string _7_UnitOrBasisMeasCode {
            get { return _unitOrBasisMeasCode3; }
            set {
                _unitOrBasisMeasCode3 = value;
                UpdateElement();
            }
        }

        public string _8_Exponent3 {
            get { return _exponent3; }
            set {
                _exponent3 = value;
                UpdateElement();
            }
        }

        public string _9_Multiplier {
            get { return _multiplier3; }
            set {
                _multiplier3 = value;
                UpdateElement();
            }
        }

        public string _10_UnitOrBasisMeasCode {
            get { return _unitOrBasisMeasCode4; }
            set {
                _unitOrBasisMeasCode4 = value;
                UpdateElement();
            }
        }

        public string _11_Exponent4 {
            get { return _exponent4; }
            set {
                _exponent4 = value;
                UpdateElement();
            }
        }

        public string _12_Multiplier {
            get { return _multiplier4; }
            set {
                _multiplier4 = value;
                UpdateElement();
            }
        }

        public string _11_UnitOrBasisMeasCode {
            get { return _unitOrBasisMeasCode5; }
            set {
                _unitOrBasisMeasCode5 = value;
                UpdateElement();
            }
        }

        public string _12_Exponent5 {
            get { return _exponent5; }
            set {
                _exponent5 = value;
                UpdateElement();
            }
        }

        public string _13_Multiplier {
            get { return _multiplier5; }
            set {
                _multiplier5 = value;
                UpdateElement();
            }
        }
    }
}