namespace OopFactory.X12.Hipaa.Eligibility
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;

    public class EligibilityBenefitInformation
    {
        public EligibilityBenefitInformation()
        {
            if (this.Identifications == null)
            {
                this.Identifications = new List<Identification>();
            }

            if (this.RequestValidations == null)
            {
                this.RequestValidations = new List<RequestValidation>();
            }

            if (this.Dates == null)
            {
                this.Dates = new List<QualifiedDate>();
            }

            if (this.DateRanges == null)
            {
                this.DateRanges = new List<QualifiedDateRange>();
            }

            if (this.Messages == null)
            {
                this.Messages = new List<string>();
            }

            if (this.RelatedEntities == null)
            {
                this.RelatedEntities = new List<RelatedEntity>();
            }
        }

        public string ServiceTypeCount { get; set; }

        [XmlIgnore]
        public decimal? Amount { get; set; }

        #region Serializable Amount properties

        [XmlAttribute(AttributeName = "Amount")]
        public decimal SerializableAmount
        {
            get
            {
                return this.Amount ?? decimal.Zero;
            }
            set
            {
                this.Amount = value;
            }
        }

        [XmlIgnore]
        public bool SerializableAmountSpecified => this.Amount.HasValue;

        #endregion

        [XmlIgnore]
        public decimal? Percentage { get; set; }

        #region Serializable Percentage properties

        [XmlAttribute(AttributeName = "Percentage")]
        public decimal SerializablePercentage
        {
            get
            {
                return this.Percentage ?? decimal.Zero;
            }

            set
            {
                this.Percentage = value;
            }
        }

        [XmlIgnore]
        public bool SerializablePercentageSpecified => this.Percentage.HasValue;

        #endregion

        public Lookup InfoType { get; set; }

        public Lookup CoverageLevel { get; set; }

        [XmlElement(ElementName = "ServiceType")]
        public List<Lookup> ServiceTypes { get; set; }

        public Lookup InsuranceType { get; set; }

        public string PlanCoverageDescription { get; set; }

        public Lookup TimePeriod { get; set; }

        public QualifiedAmount Quantity { get; set; }

        public Lookup InPlanNetwork { get; set; }

        public Lookup AuthorizationCertificationRequired { get; set; }

        public MedicalProcedure Procedure { get; set; }

        [XmlElement(ElementName = "Identification")]
        public List<Identification> Identifications { get; set; }

        [XmlElement(ElementName = "RequestValidation")]
        public List<RequestValidation> RequestValidations { get; set; }

        [XmlElement(ElementName = "Date")]
        public List<QualifiedDate> Dates { get; set; }

        [XmlElement(ElementName = "DateRange")]
        public List<QualifiedDateRange> DateRanges { get; set; }

        [XmlElement(ElementName = "Message")]
        public List<string> Messages { get; set; }

        [XmlElement(ElementName = "AdditionalInfo")]
        public List<EligibilityBenefitAdditionalInformation> AdditionalInfos { get; set; }

        [XmlElement(ElementName = "RelatedEntity")]
        public List<RelatedEntity> RelatedEntities { get; set; }
    }
}
