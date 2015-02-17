using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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

    [XmlRoot(Namespace = "http://www.oopfactory.com/2011/XSL/Hipaa")]
    public class Claim
    {
        public Claim()
        {
            if (Dates == null) Dates = new List<QualifiedDate>();
            if (Amounts == null) Amounts = new List<QualifiedAmount>();
            if (DateRanges == null) DateRanges = new List<QualifiedDateRange>();
            if (Providers == null) Providers = new List<Provider>();
            if (ServiceLines == null) ServiceLines = new List<ServiceLine>();
            if (OtherSubscriberInformations == null) OtherSubscriberInformations = new List<OtherSubscriberInformation>();
        }
        [XmlAttribute]
        public string Version { get; set; }
        [XmlAttribute]
        public ClaimTypeEnum Type { get; set; }
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

        [XmlElement(ElementName="ServiceLine")]
        public List<ServiceLine> ServiceLines { get; set; }

        #region Calculated Fields

        public decimal? PatientAmountPaid
        {
            get
            {
                var amount = Amounts.FirstOrDefault(a => a.Qualifier == "F5");
                if (amount != null)
                    return amount.Amount;
                else
                    return null;
            }
        }

        /// <summary>
        /// Box 6 on the UB04
        /// </summary>
        public DateTime? StatementFromDate
        {
            get
            {
                var dateRange = DateRanges.FirstOrDefault(dr => dr.Qualifier == "434");
                if (dateRange != null)
                    return dateRange.BeginDate;
                else
                {
                    var date = Dates.FirstOrDefault(dr => dr.Qualifier == "434");
                    if (date != null)
                        return date.Date;
                    else if (ServiceLines.Count > 0)
                        return ServiceLines.Min(sl => sl.ServiceDateFrom);
                    else
                        return null;
                }
            }
        }

        [XmlAttribute(AttributeName = "StatementFromDate", DataType = "date")]
        public DateTime SerializableStatementFromDate
        {
            get { return StatementFromDate ?? DateTime.MinValue; }
            set { }
        }

        [XmlIgnore]
        public bool SerializableStatementFromDateSpecified
        {
            get { return StatementFromDate.HasValue; }
            set { }
        }

        /// <summary>
        /// Box 6 on the UB04
        /// </summary>
        public DateTime? StatementToDate
        {
            get
            {
                var dateRange = DateRanges.FirstOrDefault(dr => dr.Qualifier == "434");
                if (dateRange != null)
                    return dateRange.EndDate;
                else
                {
                    var date = Dates.FirstOrDefault(dr => dr.Qualifier == "434");
                    if (date != null)
                        return date.Date;
                    else if (ServiceLines.Count > 0)
                        return ServiceLines.Max(sl => sl.ServiceDateTo);
                    else
                        return null;
                }
            }
        }

        [XmlAttribute(AttributeName = "StatementToDate", DataType = "date")]
        public DateTime SerializableStatementToDate
        {
            get { return StatementToDate ?? DateTime.MinValue; }
            set { }
        }

        [XmlIgnore]
        public bool SerializableStatementToDateSpecified
        {
            get { return StatementToDate.HasValue; }
            set { }
        }

        /// <summary>
        /// Box 12 and 13 on the UB04
        /// </summary>
        public DateTime? AdmissionDate
        {
            get
            {
                var date = Dates.FirstOrDefault(d => d.Qualifier == "435");
                if (date != null)
                    return date.Date;
                else
                    return null;
            }
        }

        /// <summary>
        /// Box 16 of the UB04
        /// </summary>
        public DateTime? DischargeTime
        {
            get
            {
                var date = Dates.FirstOrDefault(d => d.Qualifier == "096");
                if (date != null)
                    return date.Date;
                else
                    return null;
            }
        }

        public Provider ServiceLocation
        {
            get
            {
                var serviceFacilityLocation = ServiceFacilityLocation;
                if (serviceFacilityLocation != null)
                    return serviceFacilityLocation;
                else
                {
                    if (BillingInfo != null)
                        return BillingInfo.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "85");
                    else
                        return null;
                }
            }
        }

        public Provider BillingProvider
        {
            get
            {
                if (BillingInfo != null)
                {
                    return BillingInfo.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "85");
                }
                else
                    return null;
            }
        }

        public Provider PayToProvider
        {
            get
            {
                if (BillingInfo != null)
                {
                    var payToProvider = BillingInfo.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "87");
                    if (payToProvider != null)
                        return payToProvider;
                    else // the billing provider is the pay to provider when the pay-to provider is not present
                        return BillingInfo.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "85");
                }
                else
                    return null;
            }
        }

        public Provider PayToPlan
        {
            get
            {
                if (BillingInfo != null)
                    return BillingInfo.Providers.FirstOrDefault(p => p.Name.Type.Identifier == "PE");
                else
                    return null;
            }
        }

        public Provider AttendingProvider { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "71"); } }
        public Provider OperatingPhysician { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "72"); } }
        public Provider OtherOperatingPhysician { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "ZZ"); } }
        public Provider RenderingProvider { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "82"); } }
        public Provider ServiceFacilityLocation { get { return Providers.FirstOrDefault(p => new string[] {"77", "FA", "LI", "TL"}.Contains(p.Name.Type.Identifier)); } }
        public Provider ReferringProvider { get { return Providers.FirstOrDefault(p => p.Name.Type.Identifier == "DN" || p.Name.Type.Identifier == "P3"); } }

        #endregion

        #region Serialization Methods
        public string Serialize()
        {
            StringWriter writer = new StringWriter();
            new XmlSerializer(typeof(Claim)).Serialize(writer, this);
            return writer.ToString();
        }

        public static Claim Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Claim));
            return (Claim)serializer.Deserialize(new StringReader(xml));
        }
        #endregion
    }
}
