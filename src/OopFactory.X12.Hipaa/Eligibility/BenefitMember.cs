namespace OopFactory.X12.Hipaa.Eligibility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;

    /// <summary>
    /// Represents a <see cref="Member"/> with benefit metadata
    /// </summary>
    public class BenefitMember : Member
    {
        public BenefitMember()
        {
            if (this.Diagnoses == null)
            {
                this.Diagnoses = new List<Lookup>();
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
        }

        [XmlAttribute]
        public string BirthSequenceNumber { get; set; }

        public ProviderInformation ProviderInfo { get; set; }

        [XmlElement(ElementName = "Diagnosis")]
        public List<Lookup> Diagnoses { get; set; }

        [XmlElement(ElementName = "RequestValidation")]
        public new List<RequestValidation> RequestValidations { get; set; }

        [XmlElement(ElementName = "Date")]
        public List<QualifiedDate> Dates { get; set; }

        [XmlElement(ElementName = "DateRange")]
        public List<QualifiedDateRange> DateRanges { get; set; }

        #region PlanDate properties
        public DateTime? PlanDate
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "291");
                return date == null ? (DateTime?)null : date.Date;
            }
        }

        [XmlAttribute(AttributeName = "PlanDate", DataType = "date")]
        public DateTime SerializablePlanDate => this.PlanDate ?? DateTime.MinValue;

        [XmlIgnore]
        public bool SerializablePlanDateSpecified => this.PlanDate.HasValue;
        #endregion

        #region PlanBeginDate properties
        public DateTime? PlanBeginDate
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "346");
                return date == null ? (DateTime?)null : date.Date;
            }
        }

        [XmlAttribute(AttributeName="PlanBeginDate", DataType="date")]
        public DateTime SerializablePlanBeginDate => this.PlanBeginDate ?? DateTime.MinValue;

        [XmlIgnore]
        public bool SerializablePlanBeginDateSpecified => this.PlanBeginDate.HasValue; 
        #endregion

        #region PlanEndDate properties
        public DateTime? PlanEndDate
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "347");
                return date?.Date;
            }
        }

        [XmlAttribute(AttributeName = "PlanEndDate", DataType = "date")]
        public DateTime SerializablePlanEndDate => this.PlanEndDate ?? DateTime.MinValue; 

        [XmlIgnore]
        public bool SerializablePlanEndDateSpecified => this.PlanEndDate.HasValue; 
        #endregion

        #region EligibilityDate properties
        public DateTime? EligibilityDate
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "307");
                return date?.Date;
            }
        }

        [XmlAttribute(AttributeName = "EligibilityDate", DataType = "date")]
        public DateTime SerializableEligibilityDate => this.EligibilityDate ?? DateTime.MinValue;

        [XmlIgnore]
        public bool SerializableEligibilityDateSpecified => this.EligibilityDate.HasValue;
        #endregion

        #region EligibilityBeginDate properties
        public DateTime? EligibilityBeginDate
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "356");
                return date?.Date;
            }
        }

        [XmlAttribute(AttributeName = "EligibilityBeginDate", DataType = "date")]
        public DateTime SerializableEligibilityBeginDate => this.EligibilityBeginDate ?? DateTime.MinValue;

        [XmlIgnore]
        public bool SerializableEligibilityBeginDateSpecified => this.EligibilityBeginDate.HasValue;
        #endregion

        #region EligibilityEndDate properties
        public DateTime? EligibilityEndDate
        {
            get
            {
                var date = Dates.FirstOrDefault(d => d.Qualifier == "357");
                return date == null ? (DateTime?)null : date.Date;
            }
        }

        [XmlAttribute(AttributeName = "EligibilityEndDate", DataType = "date")]
        public DateTime SerializableEligibilityEndDate
        {
            get { return EligibilityEndDate ?? DateTime.MinValue; }
            set { }
        }

        [XmlIgnore]
        public bool SerializableEligibilityEndDateSpecified
        {
            get { return EligibilityEndDate.HasValue; }
            set { }
        }
        #endregion
    }
}
