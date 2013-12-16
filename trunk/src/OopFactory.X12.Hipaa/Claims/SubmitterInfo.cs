using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public class SubmitterInfo
    {
        public SubmitterInfo()
        {
            if (Providers == null) Providers = new Provider();
        }
        [XmlElement(ElementName = "Provider")]
        public Provider Providers { get; set; }
    }
}
