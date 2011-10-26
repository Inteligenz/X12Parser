using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims.Forms
{
    public class FormDate
    {
        [XmlAttribute]
        public string MM { get; set; }
        [XmlAttribute]
        public string DD { get; set; }
        [XmlAttribute]
        public string YY { get; set; }
    }
}
