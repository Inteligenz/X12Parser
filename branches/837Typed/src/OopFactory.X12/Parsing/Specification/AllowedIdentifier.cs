using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Parsing.Specification
{
    public class AllowedIdentifier
    {
        [XmlAttribute]
        public string ID { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}
