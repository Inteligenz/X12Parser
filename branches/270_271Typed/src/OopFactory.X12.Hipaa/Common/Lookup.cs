using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class Lookup
    {
        [XmlAttribute]
        public string Code { get; set; }
        [XmlText]
        public string Description { get; set; }
    }
}
