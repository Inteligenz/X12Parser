using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Model.Claims
{
    public class Claim
    {
        [XmlAttribute]
        public ClaimTypeEnum Type { get; set; }

        [XmlAttribute]
        public string PatientControlNumber { get; set; }

        [XmlAttribute]
        public decimal TotalCharges { get; set; }

        [XmlElement(ElementName = "Identification")]
        public List<QualifiedNumber> Identifications { get; set; }

        public Subscriber Subscriber { get; set; }

        public Member Patient { get; set; }

        [XmlElement(ElementName = "Provider")]
        public List<Provider> Providers { get; set; }

        [XmlElement(ElementName = "Occurrence")]
        public List<Occurrence> Occurrences { get; set; }

        [XmlElement(ElementName = "OccurrenceSpan")]
        public List<OccurrenceSpan> OccurrenceSpans { get; set; }

        [XmlElement(ElementName = "ServiceLine")]
        public List<ServiceLine> ServiceLines { get; set; }        
    }
}
