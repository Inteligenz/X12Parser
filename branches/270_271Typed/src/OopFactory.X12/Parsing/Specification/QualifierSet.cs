using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Parsing.Specification
{
    public class QualifierSet
    {
        public QualifierSet()
        {
            if (AllowedIdentifiers == null)
                AllowedIdentifiers = new List<AllowedIdentifier>();
        }

        [XmlAttribute]
        public string Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }
        
        [XmlElement(ElementName = "Allowed")]
        public List<AllowedIdentifier> AllowedIdentifiers { get; set; }
    }
}
