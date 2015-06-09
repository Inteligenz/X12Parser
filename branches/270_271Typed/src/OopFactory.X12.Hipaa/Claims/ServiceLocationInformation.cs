using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public class ServiceLocationInformation
    {
        [XmlAttribute]
        public string Qualifier { get; set; }
        [XmlAttribute]
        public string FacilityCode { get; set; }
        [XmlAttribute]
        public string FrequencyTypeCode { get; set; }
    }
}
