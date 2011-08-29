using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public enum ClaimTypeEnum
    {
        Professional,
        Institutional,
        Dental
    }

    public class Claim
    {
        public Claim()
        {
            if (ServiceLines == null) ServiceLines = new List<ServiceLine>();
        }

        [XmlAttribute]
        public ClaimTypeEnum Type { get; set; }
        [XmlAttribute]
        public string PatientControlNumber { get; set; }
        [XmlAttribute]
        public decimal TotalClaimChargeAmount { get; set; }

        public Entity Submitter { get; set; }
        public Entity Receiver { get; set; }
        public BillingInformation BillingInfo { get; set; }
        public ClaimMember Subscriber { get; set; }
        public Entity Payer { get; set; }
        public ClaimMember Patient { get; set; }

        #region Institional Claim Properties
        [XmlElement(ElementName="Condition")]
        public List<Lookup> Conditions { get; set; }

        [XmlElement(ElementName="Occurrence")]
        public List<CodedDate> Occurrences { get; set; }

        [XmlElement(ElementName="OccurrenceSpan")]
        public List<CodedDateRange> OccurrenceSpans { get; set; }

        [XmlElement(ElementName="Value")]
        public List<CodedAmount> Values { get; set; }

        [XmlElement(ElementName="Procedure")]
        public List<CodedDate> Procedures { get; set; }
        
        #endregion

        [XmlElement(ElementName="ServiceLine")]
        public List<ServiceLine> ServiceLines { get; set; }
    }
}
