namespace OopFactory.X12.Hipaa.Claims
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;
    using OopFactory.X12.Hipaa.Enums;

    public class OtherSubscriberInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OtherSubscriberInformation"/> class
        /// </summary>
        public OtherSubscriberInformation()
        {
            if (this.Name == null)
            {
                this.Name = new EntityName();
            }

            if (this.OtherPayer == null)
            {
                this.OtherPayer = new EntityName();
            }

            if (this.Adjustments == null)
            {
                this.Adjustments = new List<ClaimsAdjustment>();
            }

            if (this.Amounts == null)
            {
                this.Amounts = new List<QualifiedAmount>();
            }

            if (this.Providers == null)
            {
                this.Providers = new List<Provider>();
            }
        }

        [XmlAttribute]
        public Gender Gender { get; set; }

        [XmlIgnore]
        public DateTime? DateOfBirth { get; set; }

        #region Serializable DateOfBirth Properties
        [XmlAttribute(AttributeName = "DateOfBirth", DataType = "date")]
        public DateTime SerializableDateOfBirth
        {
            get { return this.DateOfBirth ?? DateTime.MinValue; }
            set { this.DateOfBirth = value; }
        }

        [XmlIgnore]
        public bool SerializableDateOfBirthSpecified => this.DateOfBirth.HasValue;
        #endregion

        [XmlAttribute]
        public string BenefitsAssignmentCertificationIndicator { get; set; }

        [XmlAttribute]
        public string ReleaseOfInformationCode { get; set; }

        public decimal? PayorPaidAmount
        {
            get
            {
                var amount = this.Amounts.FirstOrDefault(a => a.Qualifier == "D");
                return amount?.Amount ?? 0;
            }
        }

        public decimal? RemainingPatientLiability
        {
            get
            {
                var amount = this.Amounts.FirstOrDefault(a => a.Qualifier == "EAF");
                return amount?.Amount;
            }
        }

        public decimal? NonCoveredChargeAmount
        {
            get
            {
                var amount = this.Amounts.FirstOrDefault(a => a.Qualifier == "A8");
                return amount?.Amount;
            }
        }

        public SubscriberInformation SubscriberInformation { get; set; }

        public EntityName Name { get; set; }
        
        public EntityName OtherPayer { get; set; }

        [XmlElement(ElementName = "Adjustment")]
        public List<ClaimsAdjustment> Adjustments { get; set; }

        [XmlElement(ElementName = "Amount")]
        public List<QualifiedAmount> Amounts { get; set; }

        [XmlElement(ElementName = "Provider")]
        public List<Provider> Providers { get; set; }
    }
}
