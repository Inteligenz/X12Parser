using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model.Claims
{
    public class Procedure
    {
        [XmlAttribute]
        public string Qualifier { get; set; }
        [XmlAttribute]
        public string Code { get; set; }
        [XmlAttribute]
        public string Mod1 { get; set; }
        [XmlAttribute]
        public string Mod2 { get; set; }
        [XmlAttribute]
        public string Mod3 { get; set; }
        [XmlAttribute]
        public string Mod4 { get; set; }
    }
}
