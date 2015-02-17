using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed {
    public class TypedElementReferenceIdentifier {
        private int _elementNumber;
        private Segment _segment;

        private string _referenceIdentifierQualifier1;
        private string _referenceIdentifier1;

        private string _referenceIdentifierQualifier2;
        private string _referenceIdentifier2;

        private string _referenceIdentifierQualifier3;
        private string _referenceIdentifier3;

        internal TypedElementReferenceIdentifier(Segment segment, int elementNumber) {
            _segment = segment;
            _elementNumber = elementNumber;
        }

        private void UpdateElement() {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                _segment._delimiters.SubElementSeparator,
                _referenceIdentifierQualifier1,
                _referenceIdentifier1,
                _referenceIdentifierQualifier2,
                _referenceIdentifier2,
                _referenceIdentifierQualifier3,
                _referenceIdentifier3);

            value = value.TrimEnd(_segment._delimiters.SubElementSeparator);

            _segment.SetElement(_elementNumber, value);
        }

        public string _1_ReferenceIdentifierQualifier {
            get { return _referenceIdentifierQualifier1; }
            set {
                _referenceIdentifierQualifier1 = value;
                UpdateElement();
            }
        }

        public string _2_ReferenceIdentifier {
            get { return _referenceIdentifier1; }
            set {
                _referenceIdentifier1 = value;
                UpdateElement();
            }
        }

        public string _3_ReferenceIdentifierQualifier {
            get { return _referenceIdentifierQualifier2; }
            set {
                _referenceIdentifierQualifier2 = value;
                UpdateElement();
            }
        }

        public string _4_ReferenceIdentifier {
            get { return _referenceIdentifier2; }
            set {
                _referenceIdentifier2 = value;
                UpdateElement();
            }
        }

        public string _5_ReferenceIdentifierQualifier {
            get { return _referenceIdentifierQualifier3; }
            set {
                _referenceIdentifierQualifier3 = value;
                UpdateElement();
            }
        }

        public string _6_ReferenceIdentifier {
            get { return _referenceIdentifier3; }
            set {
                _referenceIdentifier3 = value;
                UpdateElement();
            }
        }
    }
}