using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims
{
    public class ToothInformation
    {
        [XmlAttribute]
        public string ToothCode { get; set; }

        [XmlElement(ElementName="ToothSurface")]
        public List<Common.Lookup> ToothSurfaces { get; set; }
    }
}
