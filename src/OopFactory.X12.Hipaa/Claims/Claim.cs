namespace OopFactory.X12.Hipaa.Claims
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;
    using OopFactory.X12.Hipaa.Enums;

    /// <summary>
    /// Represents a health insurance claim object
    /// </summary>
    [XmlRoot(Namespace = "http://www.oopfactory.com/2011/XSL/Hipaa")]
    public class Claim
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Claim"/> class
        /// </summary>
        public Claim()
        {
            if (this.Dates == null)
            {
                this.Dates = new List<QualifiedDate>();
            }

            if (this.Amounts == null)
            {
                this.Amounts = new List<QualifiedAmount>();
            }

            if (this.DateRanges == null)
            {
                this.DateRanges = new List<QualifiedDateRange>();
            }

            if (this.Providers == null)
            {
                this.Providers = new List<Provider>();
            }

            if (this.ServiceLines == null)
            {
                this.ServiceLines = new List<ServiceLine>();
            }

            if (this.OtherSubscriberInformations == null)
            {
                this.OtherSubscriberInformations = new List<OtherSubscriberInformation>();
            }
        }

        [XmlAttribute]
        public string Version { get; set; }

        [XmlAttribute]
        public ClaimType Type { get; set; }

        [XmlAttribute]
        public string RelatedCauseCode1 { get; set; }

        [XmlAttribute]
        public string RelatedCauseCode2 { get; set; }

        [XmlAttribute]
        public string RelatedCauseCode3 { get; set; }

        [XmlAttribute]
        public string AutoAccidentState { get; set; }

        [XmlAttribute]
        public string PatientSignatureSourceCode { get; set; }

        [XmlAttribute]
        public string TransactionCode { get; set; }

        [XmlAttribute]
        public string ClaimNumber { get; set; }

        [XmlAttribute]
        public string BillTypeCode { get; set; }

        [XmlAttribute]
        public string PatientControlNumber { get; set; }

        [XmlAttribute]
        public decimal TotalClaimChargeAmount { get; set; }

        [XmlAttribute]
        public string ProviderSignatureOnFile { get; set; }

        [XmlAttribute]
        public string ProviderAcceptAssignmentCode { get; set; }

        [XmlAttribute]
        public string BenefitsAssignmentCertificationIndicator { get; set; }

        [XmlAttribute]
        public string ReleaseOfInformationCode { get; set; }

        [XmlAttribute]
        public string PriorAuthorizationNumber { get; set; }
        
        [XmlElement(ElementName = "Date")]
        public List<QualifiedDate> Dates { get; set; }
        
        [XmlElement(ElementName = "Amount")]
        public List<QualifiedAmount> Amounts { get; set; }

        [XmlElement(ElementName = "DateRange")]
        public List<QualifiedDateRange> DateRanges { get; set; }
        
        public ServiceLocationInformation ServiceLocationInfo { get; set; }

        public Entity Submitter { get; set; }

        public Entity Receiver { get; set; }

        public BillingInformation BillingInfo { get; set; }

        public ProviderInformation ProviderInfo { get; set; }

        public SubmitterInfo SubmitterInfo { get; set; }

        public ClaimMember Subscriber { get; set; }

        public Entity Payer { get; set; }

        public ClaimMember Patient { get; set; }

        [XmlElement(ElementName = "OtherSubscriberInformation")]
        public List<OtherSubscriberInformation> OtherSubscriberInformations { get; set; }

        #region Institional Claim Properties
        /// <summary>
        /// Box 3B on the UB04
        /// </summary>
        [XmlAttribute]
        public string MedicalRecordNumber { get; set; }

        /// <summary>
        /// Box 14 of the UB04
        /// </summary>
        public Lookup AdmissionType { get; set; }

        /// <summary>
        /// Box 15 of the UB04
        /// </summary>
        public Lookup AdmissionSource { get; set; }

        /// <summary>
        /// Box 17 of the UB04
        /// </summary>
        public Lookup PatientStatus { get; set; }

        /// <summary>
        ///  Box 71 of the UB04
        /// </summary>
        public Lookup DiagnosisRelatedGroup { get; set; }

        // Used by CMS-1500
        public SubscriberInformation SubscriberInformation { get; set; }

        [XmlElement(ElementName = "Condition")]
        public List<Lookup> Conditions { get; set; }

        [XmlElement(ElementName = "Occurrence")]
        public List<CodedDate> Occurrences { get; set; }

        [XmlElement(ElementName = "OccurrenceSpan")]
        public List<CodedDateRange> OccurrenceSpans { get; set; }

        [XmlElement(ElementName = "Value")]
        public List<CodedAmount> Values { get; set; }

        [XmlElement(ElementName = "Diagnosis")]
        public List<Diagnosis> Diagnoses { get; set; }

        [XmlElement(ElementName = "Procedure")]
        public List<InstitutionalProcedure> Procedures { get; set; }

        [XmlElement(ElementName = "Provider")]
        public List<Provider> Providers { get; set; }

        [XmlElement(ElementName = "Identification")]
        public List<Identification> Identifications { get; set; }

        #endregion

        [XmlElement(ElementName = "Note")]
        public List<Lookup> Notes { get; set; }

        [XmlElement(ElementName = "ServiceLine")]
        public List<ServiceLine> ServiceLines { get; set; }

        #region Calculated Fields
        public decimal? PatientAmountPaid
        {
            get
            {
                var amount = this.Amounts.FirstOrDefault(a => a.Qualifier == "F5");
                return amount?.Amount;
            }
        }

        /// <summary>
        /// Box 6 on the UB04
        /// </summary>
        public DateTime? StatementFromDate
        {
            get
            {
                var dateRange = this.DateRanges.FirstOrDefault(dr => dr.Qualifier == "434");
                if (dateRange != null)
                {
                    return dateRange.BeginDate;
                }
                else
                {
                    var date = this.Dates.FirstOrDefault(dr => dr.Qualifier == "434");
                    if (date != null)
                    {
                        return date.Date;
                    }
                    else if (this.ServiceLines.Count > 0)
                    {
                        return this.ServiceLines.Min(sl => sl.ServiceDateFrom);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        [XmlAttribute(AttributeName = "StatementFromDate", DataType = "date")]
        public DateTime SerializableStatementFromDate => this.StatementFromDate ?? DateTime.MinValue; 

        [XmlIgnore]
        public bool SerializableStatementFromDateSpecified => this.StatementFromDate.HasValue;

        /// <summary>
        /// Box 6 on the UB04
        /// </summary>
        public DateTime? StatementToDate
        {
            get
            {
                var dateRange = this.DateRanges.FirstOrDefault(dr => dr.Qualifier == "434");
                if (dateRange != null)
                {
                    return dateRange.EndDate;
                }
                else
                {
                    var date = this.Dates.FirstOrDefault(dr => dr.Qualifier == "434");
                    if (date != null)
                    {
                        return date.Date;
                    }
                    else if (this.ServiceLines.Count > 0)
                    {
                        return this.ServiceLines.Max(sl => sl.ServiceDateTo);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        [XmlAttribute(AttributeName = "StatementToDate", DataType = "date")]
        public DateTime SerializableStatementToDate => this.StatementToDate ?? DateTime.MinValue;

        [XmlIgnore]
        public bool SerializableStatementToDateSpecified => this.StatementToDate.HasValue;

        /// <summary>
        /// Box 12 and 13 on the UB04
        /// </summary>
        public DateTime? AdmissionDate
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "435");
                return date?.Date;
            }
        }

        /// <summary>
        /// Box 16 of the UB04
        /// </summary>
        public DateTime? DischargeTime
        {
            get
            {
                var date = this.Dates.FirstOrDefault(d => d.Qualifier == "096");
                return date?.Date;
            }
        }

        public Provider ServiceLocation
        {
            get
            {
                return this.ServiceFacilityLocation
                       ?? this.BillingInfo?.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "85");
            }
        }

        public Provider BillingProvider => this.BillingInfo?.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "85");

        public Provider PayToProvider
        {
            get
            {
                if (this.BillingInfo != null)
                {
                    var payToProvider = this.BillingInfo.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "87");
                    return payToProvider ?? this.BillingInfo.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "85");
                }
                else
                {
                    return null;
                }
            }
        }

        public Provider PayToPlan => this.BillingInfo?.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "PE");

        public Provider AttendingProvider => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "71");

        public Provider OperatingPhysician => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "72");

        public Provider OtherOperatingPhysician => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "ZZ");

        public Provider RenderingProvider => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "82");

        public Provider ServiceFacilityLocation => this.Providers.FirstOrDefault(p => new[] {"77", "FA", "LI", "TL"}.Contains(p.Name.Type.Identifier));

        public Provider ReferringProvider => this.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "DN" || p.Name.Type.Identifier == "P3");

        #endregion

        #region Serialization Methods
        public string Serialize()
        {
            var writer = new StringWriter();
            new XmlSerializer(typeof(Claim)).Serialize(writer, this);
            return writer.ToString();
        }

        public static Claim Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(Claim));
            return (Claim)serializer.Deserialize(new StringReader(xml));
        }
        #endregion
    }
}
