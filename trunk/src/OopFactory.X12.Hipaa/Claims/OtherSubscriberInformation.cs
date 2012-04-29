using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public class OtherSubscriberInformation
    {
        public OtherSubscriberInformation()
        {
            if (Name == null) Name = new EntityName();
            if (OtherPayer == null) OtherPayer = new EntityName();
            if (Adjustments == null) Adjustments = new List<ClaimsAdjustment>();
            if (Amounts == null) Amounts = new List<QualifiedAmount>();
            if (Providers == null) Providers = new List<Provider>();
        }
        [XmlAttribute]
        public GenderEnum Gender { get; set; }

        [XmlIgnore]
        public DateTime? DateOfBirth { get; set; }

        #region Serializable DateOfBirth Properties
        [XmlAttribute(AttributeName = "DateOfBirth", DataType = "date")]
        public DateTime SerializableDateOfBirth
        {
            get { return DateOfBirth ?? DateTime.MinValue; }
            set { DateOfBirth = value; }
        }

        [XmlIgnore]
        public bool SerializableDateOfBirthSpecified
        {
            get { return DateOfBirth.HasValue; }
            set { }
        }
        #endregion

        [XmlAttribute]
        public string BenefitsAssignmentCertificationIndicator { get; set; }
        [XmlAttribute]
        public string ReleaseOfInformationCode { get; set; }

        public decimal? PayorPaidAmount
        {
            get
            {
                var amount = Amounts.FirstOrDefault(a => a.Qualifier == "D");
                return amount == null ? 0 : amount.Amount;
            }
        }

        public decimal? RemainingPatientLiability
        {
            get
            {
                var amount = Amounts.FirstOrDefault(a => a.Qualifier == "EAF");
                return amount == null ? (decimal?)null : amount.Amount;
            }
        }

        public decimal? NonCoveredChargeAmount
        {
            get
            {
                var amount = Amounts.FirstOrDefault(a => a.Qualifier == "A8");
                return amount == null ? (decimal?)null : amount.Amount;
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
