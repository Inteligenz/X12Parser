using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model.Claims
{
    public class Claim
    {
        [XmlElement(ElementName = "Identification")]
        public List<QualifiedNumber> Identifications { get; set; }

        public Subscriber Subscriber { get; set; }

        [XmlElement(ElementName = "Provider")]
        public List<Provider> Providers { get; set; }

        [XmlElement(ElementName = "ServiceLine")]
        public List<ServiceLine> ServiceLines { get; set; }        
    }
}
