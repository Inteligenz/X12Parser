using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementReferenceIdentifier : BaseElementReference
    {
        private string _referenceIdentifierQualifier1;
        private string _referenceIdentifier1;

        private string _referenceIdentifierQualifier2;
        private string _referenceIdentifier2;

        private string _referenceIdentifierQualifier3;
        private string _referenceIdentifier3;

        public TypedElementReferenceIdentifier(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                 Segment._delimiters.SubElementSeparator,
                 _referenceIdentifierQualifier1,
                 _referenceIdentifier1,
                 _referenceIdentifierQualifier2,
                 _referenceIdentifier2,
                 _referenceIdentifierQualifier3,
                 _referenceIdentifier3);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public string _1_ReferenceIdentifierQualifier
        {
            get { return _referenceIdentifierQualifier1; }
            set
            {
                _referenceIdentifierQualifier1 = value;
                
            }
        }

        public string _2_ReferenceIdentifier
        {
            get { return _referenceIdentifier1; }
            set
            {
                _referenceIdentifier1 = value;
                
            }
        }

        public string _3_ReferenceIdentifierQualifier
        {
            get { return _referenceIdentifierQualifier2; }
            set
            {
                _referenceIdentifierQualifier2 = value;
                
            }
        }

        public string _4_ReferenceIdentifier
        {
            get { return _referenceIdentifier2; }
            set
            {
                _referenceIdentifier2 = value;
                
            }
        }

        public string _5_ReferenceIdentifierQualifier
        {
            get { return _referenceIdentifierQualifier3; }
            set
            {
                _referenceIdentifierQualifier3 = value;
                
            }
        }

        public string _6_ReferenceIdentifier
        {
            get { return _referenceIdentifier3; }
            set
            {
                _referenceIdentifier3 = value;
                
            }
        }
    }
}