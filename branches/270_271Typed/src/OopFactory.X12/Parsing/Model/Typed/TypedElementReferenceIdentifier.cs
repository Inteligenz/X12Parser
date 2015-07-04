using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementReferenceIdentifier : BaseElementReference
    {
        public TypedElementReferenceIdentifier(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }

        public override string ToString()
        {
            string value = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                 Segment._delimiters.SubElementSeparator,
                 _1_ReferenceIdentifierQualifier,
                 _2_ReferenceIdentifier,
                 _3_ReferenceIdentifierQualifier,
                 _4_ReferenceIdentifier,
                 _5_ReferenceIdentifierQualifier,
                 _6_ReferenceIdentifier);

            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public string _1_ReferenceIdentifierQualifier { get; set; }

        public string _2_ReferenceIdentifier { get; set; }

        public string _3_ReferenceIdentifierQualifier { get; set; }

        public string _4_ReferenceIdentifier { get; set; }

        public string _5_ReferenceIdentifierQualifier { get; set; }

        public string _6_ReferenceIdentifier { get; set; }
    }
}