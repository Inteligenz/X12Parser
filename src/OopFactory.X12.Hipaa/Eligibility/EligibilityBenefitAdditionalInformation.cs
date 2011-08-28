using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Eligibility
{
    public class EligibilityBenefitAdditionalInformation
    {
        [XmlAttribute]
        public string Qualifier { get; set; }
        [XmlAttribute]
        public string IndustryCode { get; set; }
        [XmlAttribute]
        public string CodeCategory { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
