using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class SubscriberInformation
    {
        [XmlAttribute]
        public string PayerResponsibilitySequenceNumberCode { get; set; }

        [XmlAttribute]
        public string IndividualRelationshipCode { get; set; }

        [XmlAttribute]
        public string ReferenceIdentification { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string InsuranceTypeCode { get; set; }

        [XmlAttribute]
        public string CoordinationOfBenefitsCode { get; set; }

        [XmlAttribute]
        public string YesNoConditionResponseCode { get; set; }

        [XmlAttribute]
        public string EmploymentStatusCode { get; set; }

        [XmlAttribute]
        public string ClaimFilingIndicatorCode { get; set; }
    }
}
