using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Eligibility
{
    public class BenefitMember : Member
    {
        public BenefitMember()
        {
            if (Relationship == null) Relationship = new Lookup();
            if (ProviderInfo == null) ProviderInfo = new ProviderInformation();
            if (Diagnoses == null) Diagnoses = new List<QualifiedCode>();
        }

        public DateTime? IssueDate { get; set; }
        public DateTime? PlanDate { get; set; }

        [XmlAttribute]
        public string BirthSequenceNumber { get; set; }

        public Lookup Relationship { get; set; }
        
        public ProviderInformation ProviderInfo { get; set; }

        [XmlElement(ElementName="Diagnosis")]
        public List<QualifiedCode> Diagnoses { get; set; }
    }
}
