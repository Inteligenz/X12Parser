namespace OopFactory.X12.Hipaa.Common
{
    using System.Xml.Serialization;

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
