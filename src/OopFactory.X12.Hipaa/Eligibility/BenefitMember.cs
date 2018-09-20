namespace OopFactory.X12.Hipaa.Eligibility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;
    using OopFactory.X12.Hipaa.Enums;

    /// <summary>
    /// Represents a <see cref="Member"/> with benefit metadata
    /// </summary>
    public class BenefitMember : Member
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BenefitMember"/> class
        /// </summary>
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

        /// <summary>
        /// Gets or sets the birth sequence number for the member
        /// </summary>
        [XmlAttribute]
        public string BirthSequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ProviderInformation"/>
        /// </summary>
        public ProviderInformation ProviderInfo { get; set; }

        /// <summary>
        /// Gets or sets diagnosis received from a provider
        /// </summary>
        [XmlElement(ElementName = "Diagnosis")]
        public List<Lookup> Diagnoses { get; set; }

        /// <summary>
        /// Gets or sets the collection of request validations
        /// </summary>
        [XmlElement(ElementName = "RequestValidation")]
        public new List<RequestValidation> RequestValidations { get; set; }

        /// <summary>
        /// Gets or sets the collection of qualified dates
        /// </summary>
        [XmlElement(ElementName = ClaimElements.Date)]
        public List<QualifiedDate> Dates { get; set; }

        /// <summary>
        /// Gets or sets the collection of qualified date ranges
        /// </summary>
        [XmlElement(ElementName = ClaimElements.DateRange)]
        public List<QualifiedDateRange> DateRanges { get; set; }
        
        /// <summary>
        /// Gets the plan date from the collection of qualified dates
        /// </summary>
        public DateTime? PlanDate
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "291");
                return date?.Date;
            }
        }

        /// <summary>
        /// Gets the serializable form of the plan date
        /// </summary>
        [XmlAttribute(AttributeName = "PlanDate", DataType = "date")]
        public DateTime SerializablePlanDate => this.PlanDate ?? DateTime.MinValue;

        /// <summary>
        /// Indicates whether the plan date has a value or not
        /// </summary>
        [XmlIgnore]
        public bool SerializablePlanDateSpecified => this.PlanDate.HasValue;
        
        /// <summary>
        /// Gets the plan begin date from the collection of dates
        /// </summary>
        public DateTime? PlanBeginDate
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "346");
                return date?.Date;
            }
        }

        /// <summary>
        /// Gets the serializable form of the plan begin date
        /// </summary>
        [XmlAttribute(AttributeName = "PlanBeginDate", DataType = "date")]
        public DateTime SerializablePlanBeginDate => this.PlanBeginDate ?? DateTime.MinValue;

        /// <summary>
        /// Indicates whether the plan begin date has a value or not
        /// </summary>
        [XmlIgnore]
        public bool SerializablePlanBeginDateSpecified => this.PlanBeginDate.HasValue;
        
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
        
        /// <summary>
        /// Gets the date the eligibility begins
        /// </summary>
        public DateTime? EligibilityBeginDate
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "356");
                return date?.Date;
            }
        }

        /// <summary>
        /// Gets the serializable data for the <see cref="EligibilityBeginDate"/>
        /// </summary>
        [XmlAttribute(AttributeName = "EligibilityBeginDate", DataType = "date")]
        public DateTime SerializableEligibilityBeginDate => this.EligibilityBeginDate ?? DateTime.MinValue;

        /// <summary>
        /// Indicates whether the eligibility begin date has a value or not
        /// </summary>
        [XmlIgnore]
        public bool SerializableEligibilityBeginDateSpecified => this.EligibilityBeginDate.HasValue;
        
        /// <summary>
        /// Gets the date when the benefits end
        /// </summary>
        public DateTime? EligibilityEndDate
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "357");
                return date?.Date;
            }
        }

        /// <summary>
        /// Gets the serializable data for the <see cref="EligibilityEndDate"/>
        /// </summary>
        [XmlAttribute(AttributeName = "EligibilityEndDate", DataType = "date")]
        public DateTime SerializableEligibilityEndDate => this.EligibilityEndDate ?? DateTime.MinValue;

        /// <summary>
        /// Indicates whether the eligibility end date has a value or not
        /// </summary>
        [XmlIgnore]
        public bool SerializableEligibilityEndDateSpecified => this.EligibilityEndDate.HasValue;
    }
}
