using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class ProviderInformation : Identification
    {
        [XmlAttribute]
        public string ProviderCode { get; set; }

        [XmlAttribute]
        public string ProviderDescription { get; set; }

    }
}
