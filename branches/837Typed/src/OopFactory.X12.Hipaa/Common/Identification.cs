using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class Identification
    {
        [XmlAttribute]
        public string Qualifier { get; set; }
        [XmlAttribute]
        public string Id { get; set; }
        [XmlText]
        public string Description { get; set; }
    }
}
