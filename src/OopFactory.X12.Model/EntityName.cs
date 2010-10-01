using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model
{
    public class EntityName
    {
        [XmlAttribute]
        public bool IsPerson { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public string Last { get; set; }
    }
}
