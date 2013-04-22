using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Parsing.Specification
{
    public class ElementSpecification
    {
        public ElementSpecification()
        {
            if (AllowedIdentifiers == null)
                AllowedIdentifiers = new List<AllowedIdentifier>();
        }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Reference { get; set; }
        [XmlAttribute]
        public bool Required { get; set; }
        [XmlAttribute]
        public ElementDataTypeEnum Type { get; set; }
        [XmlAttribute]
        public int ImpliedDecimalPlaces { get; set; }
        [XmlAttribute]
        public int MinLength { get; set; }
        [XmlAttribute]
        public int MaxLength { get; set; }
        [XmlAttribute]
        public bool IsComposite { get; set; }
        [XmlAttribute]
        public int MaxComponents { get; set; }
        [XmlAttribute]
        public bool AllowedListInclusive { get; set; }
        [XmlAttribute]
        public string QualifierSetRef { get; set; }
        [XmlAttribute]
        public string QualifierSetId { get; set; }

        [XmlElement(ElementName="Allowed")]
        public List<AllowedIdentifier> AllowedIdentifiers { get; set; }
    }
}
