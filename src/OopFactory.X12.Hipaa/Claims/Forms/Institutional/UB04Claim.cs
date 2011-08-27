using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
#if DEBUG
    [Serializable]
    public class UB04Claim
    {
        /*
         * 2011/8/15, jhalliday - New Data Model for 837I (Institutional) claim.
         * 
         * Team: dstrubhar, jhalliday and epkrause
         * 
         * Purpose:
         * To create a C# object model that will serve as a container for the X12 837I data
         * AS ENTERED from a UB-04 CMS-1450 (formerly UB-92) Institutional (Inpatient Hospital) claim form.
         * 
         * Goal:
         * The team has the overall goal of creating tools that can be used to consume and
         * manipulate X12 messages (AKA files/documents) without the need to have a big project
         * budget.  For that reason, this and the related X12 Parser project tools are all open
         * source and freely usable.
         */

        // Fields in the UB04 object model are defined in the order they appear on the UB-04 form.

        // First, we will declare the private variables.  Then the properties and their accessors.
        // From 2010AA loop
        private string _field01_01_BillingProviderLastName;
        //private string _field01_02_BillingProviderFirstName;              // Not used
        //private string _field01_03_BillingProviderMiddleName;             // Not used
        private string _field01_04_BillingProviderAddress1;
        private string _field01_05_BillingProviderAddress2;
        private string _field01_06_BillingProviderCity;
        private string _field01_08_BillingProviderState;
        private string _field01_09_BillingProviderZip;
        //private string _field01_10_ProviderZip_4;
        private string _field01_11_BillingProviderPhoneNumber;
        private string _field01_12_BillingProviderFaxNumber;
        private string _field01_13_BillingProviderCountryCode;
        // Pay-To is also known as the 'Subscriber' from the 2010BA loop.
        private string _field02_01_PayToLastName;
        //private string _field02_02_PayToFirstName;
        //private string _field02_03_PayToMiddleName;
        private string _field02_04_PayToAddress1;
        //private string _field02_05_PayToAddress2;
        private string _field02_06_PayToCity;
        private string _field02_08_PayToState;
        private string _field02_09_PayToZip;
        //private string _field02_10_ProviderZip_4;
        private string _field02_11_PayToCountryCode;
        private string _field03a_PatientControlNumber;
        private string _field03b_MedicalHealthRecordNumber;
        private string _field04_TypeOfBill;
        private string _field05_FederalTaxId;
        private string _field06_ServiceFromDate;
        private string _field06_ServiceToDate;
        //private string _field07_Filler;
        private string _field08a_PatientIdentifier;
        private string _field08b_01_PatientLastName;
        private string _field08b_02_PatientFirstName;
        private string _field08b_03_PatientMiddleName;
        private string _field09a_PatientStreet;
        private string _field09b_PatientCity;
        private string _field09c_PatientState;
        private string _field09d_PatientZip;
        private string _field09e_PatientCountry;
        private string _field10_PatientDOB;
        private string _field11_Sex;
        private string _field12_AdmissionDate;
        private string _field13_AdmissionHour;
        private string _field14_TypeOfVisit;
        private string _field15_SourceOfAdmission;
        private string _field16_DischargeHour;
        private string _field17_PatientDischargeStatus;
        private List<string> _field18_28_ConditionCodes;
        private string _field29_AccidentState;
        //private string _field30_Filler;
        private List<UB04OccurrenceCodesAndDates> _field31_34_OccurrenceCodesAndDates;
        private List<UB04OccurrenceSpanCodesAndDates> _field35_36_OccurrenceSpanCodesAndDates;
        //private string _field37_Filler;
        //private string _field38_AdditionalPartyName;
        //private List<UB04ValueCodesAndAmount> _field39_41_ValueCodesAndAmounts;
        private List<UB04ServiceLine_2300Loop> _field42_49_ServiceLines;
        private List<UB04TotalChargesLine> _field42_49_ServiceLinesTotal;
        private decimal _field47_SummaryTotalCharges;
        private decimal _field48_SummaryTotalNonCoveredCharges;

        //private string _field50a_PayerName;
        //private string _field50b_PayerSecondaryInsuranceCompanyName;
        //private string _field50c_PayerTertiaryInsuranceCompanyName;
        ////private string _field51a_Filler;
        ////private string _field51b_Filler;
        ////private string _field51c_Filler;
        //private string _field52a_ReleaseOfInformationCertificationIndicator;
        //private string _field52b_ReleaseOfInformationCertificationIndicator;
        //private string _field52c_ReleaseOfInformationCertificationIndicator;
        //private string _field53a_AssignmentOfBenefitsCertificationIndicator;
        //private string _field53b_AssignmentOfBenefitsCertificationIndicator;
        //private string _field53c_AssignmentOfBenefitsCertificationIndicator;
        //private decimal _field54a_PriorPayments;
        //private decimal _field54b_PriorPayments;
        //private decimal _field54c_PriorPayments;
        //private decimal _field55a_EstimatedAmountDue;
        //private decimal _field55b_EstimatedAmountDue;
        //private decimal _field55c_EstimatedAmountDue;
        private List<Field50_PayerName> _field50PayerName;
        private List<Field52_ReleaseOfInfoCertIndicator> _field52_ReleaseOfInfoCertIndicator;
        private List<Field53_AssignmentOfBenefitsCertIndicator> _field53_AssignmentOfBenefitsCertIndicator;
        private List<Field54_PriorPayments> _field54_PriorPayments;
        private List<Field55EstimatedAmountDue> _field55EstimatedAmountDue;

        private string _field56_NationalProviderIndicator;
        private string _field57_OtherProviderIdentifier;
        private string _field58a_InsuredsLastName;
        private string _field58b_InsuredsFirstName;
        private string _field58c_InsuredsMiddleName;
        private string _field59a_RelationshipToInsured;
        private string _field59b_RelationshipToInsured;
        private string _field59c_RelationshipToInsured;
        private string _field60a_InsuredsIniqueIdentificationNumber;
        private string _field60b_InsuredsIniqueIdentificationNumber;
        private string _field60c_InsuredsIniqueIdentificationNumber;
        private string _field61a_InsuredsGroupOrPlanName;
        private string _field61b_InsuredsGroupOrPlanName;
        private string _field61c_InsuredsGroupOrPlanName;
        private string _field62a_InsuredsGroupOrPlanNumber;
        private string _field62b_InsuredsGroupOrPlanNumber;
        private string _field62c_InsuredsGroupOrPlanNumber;
        private string _field63a_TreatmentAuthorizationCodes;
        private string _field63b_TreatmentAuthorizationCodes;
        private string _field63c_TreatmentAuthorizationCodes;
        private string _field64a_DocumentControlNumber;
        private string _field64b_DocumentControlNumber;
        private string _field64c_DocumentControlNumber;
        //private string _field65a_EmployerName;
        //private string _field65b_EmployerName;
        //private string _field65c_EmployerName;
        private string _field66_DiagnosisAndProcedureCodeQualifier;
        private string _field67_PrincipleDiagCode1_7;
        private string _field67_PrincipleDiagCode8;
        private string _field67a_q_OtherDiagCode1_7;
        private string _field67a_q_OtherDiagCode8;
        private string _field69_AdmittingDiagnosisCode;
        private string _field71_ProspectivePaymentSystemCode;
        private string _field72_ExternalCauseOfInjuryCode;

        private string _field76_AttendingProviderLastName;
        private string _field76_AttendingProviderFirstName;
        private string _field76_AttendingProviderMiddleName;
        private string _field76_AttendingProviderNameSuffix;
        private string _field76_AttendingProviderSecondaryQualifier;
        private string _field76_AttendingProviderNationalProviderIdentifier;
        private string _field76_AttendingProviderSecondaryIdentifier;

        private string _field77_OperatingPhysicianNationalProviderIdentifier;
        private string _field77_OperatingPhysicianSecondaryQualifier;
        private string _field77_OperatingPhysicianSecondaryIdentifier;
        private string _field77_OperatingPhysicianLastName;
        private string _field77_OperatingPhysicianFirstName;

        private string _field78_OtherProvider1NationalProviderIdentifier;
        private string _field78_OtherProvider1SecondaryQualifier;
        private string _field78_OtherProvider1SecondaryIdentifier;
        private string _field78_OtherProvider1LastName;
        private string _field78_OtherProvider1FirstName;

        private string _field79_RenderingProvider2NationalProviderIdentifier;
        private string _field79_RenderingProvider2SecondaryQualifier;
        private string _field79_RenderingProvider2SecondaryIdentifier;
        private string _field79_RenderingProvider2LastName;
        private string _field79_RenderingProvider2FirstName;

        private string _field79_ReferringProvider2NationalProviderIdentifier;
        private string _field79_ReferringProvider2SecondaryQualifier;
        private string _field79_ReferringProvider2SecondaryIdentifier;
        private string _field79_ReferringProvider2LastName;
        private string _field79_ReferringProvider2FirstName;

        private string _field80_Remarks;
        private List<UB04Code_Code> _field80Code_Code;

        public UB04Claim() { if (_field42_49_ServiceLines == null) _field42_49_ServiceLines = new List<UB04ServiceLine_2300Loop>(); }

        // Now the accessor definitions:

        // Field 01, the Facility Provider / Billing Provider has many possible parts.  All known potential
        // elements are listed here.
        [XmlAttribute]
        public string Field01_01_BillingProviderLastName           //<-- Facility name or last name of provider
        { get { return _field01_01_BillingProviderLastName; } set { _field01_01_BillingProviderLastName = value; } }

        //[XmlAttribute]
        //public string Field01_02_BillingProviderFirstName          //<-- Only if individual provider
        //{ get{ return _field01_02_BillingProviderFirstName; } set { _field01_02_BillingProviderFirstName = value; } }

        //[XmlAttribute]
        //public string Field01_03_BillingProviderMiddleName          //<-- Only if individual provider
        //{ get { return _field01_03_BillingProviderMiddleName; } set { _field01_03_BillingProviderMiddleName = value; } }

        public string Field01_04_BillingProviderAddress1
        { get { return _field01_04_BillingProviderAddress1; } set { _field01_04_BillingProviderAddress1 = value; } }

        public string Field01_05_BillingProviderAddress2
        { get { return _field01_05_BillingProviderAddress2; } set { _field01_05_BillingProviderAddress2 = value; } }

        public string Field01_06_BillingProviderCity { get { return _field01_06_BillingProviderCity; } set { _field01_06_BillingProviderCity = value; } }
        public string Field01_08_BillingProviderState { get { return _field01_08_BillingProviderState; } set { _field01_08_BillingProviderState = value; } }
        public string Field01_09_BillingProviderZip { get { return _field01_09_BillingProviderZip; } set { _field01_09_BillingProviderZip = value; } }

        public string Field01_11_BillingProviderPhoneNumber 
        { get { return _field01_11_BillingProviderPhoneNumber; } set { _field01_11_BillingProviderPhoneNumber = value; } }

        public string Field01_12_BillingProviderFaxNumber
        { get { return _field01_12_BillingProviderFaxNumber; } set { _field01_12_BillingProviderFaxNumber = value; } }

        public string Field01_13_BillingProviderCountryCode
        { get { return _field01_13_BillingProviderCountryCode; } set { _field01_13_BillingProviderCountryCode = value; } }

        // Field 02 - the Pay-To provider address.  This is usually provided only when different than Field 01.
        public string Field02_01_PayToLastName { get { return _field02_01_PayToLastName; } set { _field02_01_PayToLastName = value; } }
        //public string Field02_02_PayToFirstName { get { return _field02_02_PayToFirstName; } set { _field02_02_PayToFirstName = value; } }
        //public string Field02_03_PayToMiddleName { get { return _field02_03_PayToMiddleName; } set { _field02_03_PayToMiddleName = value; } }
        public string Field02_04_PayToAddress1 { get { return _field02_04_PayToAddress1; } set { _field02_04_PayToAddress1 = value; } }
        //public string Field02_05_PayToAddress2 { get { return _field02_05_PayToAddress2; } set { _field02_05_PayToAddress2 = value; } }
        public string Field02_06_PayToCity { get { return _field02_06_PayToCity; } set { _field02_06_PayToCity = value; } }
        public string Field02_08_PayToState { get { return _field02_08_PayToState; } set { _field02_08_PayToState = value; } }
        public string Field02_09_PayToZip { get { return _field02_09_PayToZip; } set { _field02_09_PayToZip = value; } }
        public string Field02_11_PayToCountryCode { get { return _field02_11_PayToCountryCode; } set { _field02_11_PayToCountryCode = value; } }

        // Field 03a - a unique alpha-numeric number assigned by the provider.  Used to allow for the retrieval
        // of individual patient financial records.  Optional field.
        public string Field03a_PatientControlNumber
        { get { return _field03a_PatientControlNumber; } set { _field03a_PatientControlNumber = value; } }

        // Field 03b - a value assigned by the provider that indicates the patient's medical record number.
        public string Field03b_MedicalHealthRecordNumber
        { get { return _field03b_MedicalHealthRecordNumber; } set { _field03b_MedicalHealthRecordNumber = value; } }

        // Field 04 - Type of Bill, a three or four digit code that indicates the type of bill being submitted.
        // Refer to the NUBC Guide for TOB frequency codes.  This is set as a string value because it may contain
        // a leading zero.
        public string Field04_TypeOfBill { get { return _field04_TypeOfBill; } set { _field04_TypeOfBill = value; } }

        // Field 05 - Federal Tax ID Number.  This field may contain the tax id (TID) or the newer Employer Identification
        // Number (EIN).  Affiliated subsidiaries are identified using federeal tax sub-ID's.
        public string Field05_FederalTaxId { get { return _field05_FederalTaxId; } set { _field05_FederalTaxId = value; } }

        // Field 06 - Service FROM and TO dates.  MMDDCCYY format.
        public string Field06_ServiceFromDate { get { return _field06_ServiceFromDate; } set { _field06_ServiceFromDate = value; } }

        public string Field06_ServiceToDate { get { return _field06_ServiceToDate; } set { _field06_ServiceToDate = value; } }

        // Field 08a - Patient Identification Number (Patient ID).
        public string Field08a_PatientIdentifier { get { return _field08a_PatientIdentifier; } set { _field08a_PatientIdentifier = value; } }

        // Field 08b-01 PatientLastName.  Required.
        public string Field08b_01_PatientLastName { get { return _field08b_01_PatientLastName; } set { _field08b_01_PatientLastName = value; } }

        // Field 08b-01 PatientFirstName
        public string Field08b_02_PatientFirstName { get { return _field08b_02_PatientFirstName; } set { _field08b_02_PatientFirstName = value; } }

        // Field 08b-01 PatientMiddleName
        public string Field08b_03_PatientMiddleName { get { return _field08b_03_PatientMiddleName; } set { _field08b_03_PatientMiddleName = value; } }

        // Field 09a - Patient Street.  Required.
        public string Field09a_PatientStreet { get { return _field09a_PatientStreet; } set { _field09a_PatientStreet = value; } }

        // Field 09b - Patient City.  Required.
        public string Field09b_PatientCity { get { return _field09b_PatientCity; } set { _field09b_PatientCity = value; } }

        // Field 09c - Patient State.  Required.
        public string Field09c_PatientState { get { return _field09c_PatientState; } set { _field09c_PatientState = value; } }

        // Field 09d - Patient Zip.  Required.
        public string Field09d_PatientZip { get { return _field09d_PatientZip; } set { _field09d_PatientZip = value; } }

        // Field 09e - Patient Country Code.  Not required.
        public string Field09e_PatientCountry { get { return _field09e_PatientCountry; } set { _field09e_PatientCountry = value; } }

        // Field 10 - Patient Date of Birth (DOB) in MMDDCCYY format
        public string Field10_PatientDOB { get { return _field10_PatientDOB; } set { _field10_PatientDOB = value; } }

        // Field 11 - Gender/Sex.  'M' = Male; 'F' = Female, 'U' = Unknown
        public string Field11_Sex { get { return _field11_Sex; } set { _field11_Sex = value; } }

        // Field 12 - Admission Date / Start of Care Date.  This is the date that patient care actually begins.  For
        // inpatient care it is the admission date.  For other types it is the day the care begins.
        public string Field12_AdmissionDate { get { return _field12_AdmissionDate; } set { _field12_AdmissionDate = value; } }

        // Field 13 - Admission Hour.  A two-digit code indicating the hour of day that the care began (when they were admitted).
        // Use military time (00 through 24).
        public string Field13_AdmissionHour { get { return _field13_AdmissionHour; } set { _field13_AdmissionHour = value; } }

        // Field 14 - Priority (Type) of Visit.  The code for the priority of the admission or visit.
        public string Field14_TypeOfVisit { get { return _field14_TypeOfVisit; } set { _field14_TypeOfVisit = value; } }

        // Field 15 - Point of Origina / Source of Admission or Visit.  Indicates the source of the referral for visit or 
        // admission (e.g., physician, clinic, facility, transfer, etc.).  Usually a single alpha-numeric digit.
        public string Field15_SourceOfAdmission { get { return _field15_SourceOfAdmission; } set { _field15_SourceOfAdmission = value; } }

        // Field 16 - Discharge Hour.  A two-digit code indicating the hour of day that the care ended (when they were discharged).
        // Use military time (00 through 24).
        public string Field16_DischargeHour { get { return _field16_DischargeHour; } set { _field16_DischargeHour = value; } }
        
        /// <summary>
        /// Field 17 - Patient Discharge Status.  Reports status of patient upon discharge - required for institutional claims. 
        /// Two digit numeric.
        /// </summary>
        [XmlAttribute]
        public string Field17_PatientDischargeStatus { get { return _field17_PatientDischargeStatus; } set { _field17_PatientDischargeStatus = value; } }

        [XmlIgnore]
        public bool Field17_PatientDischargeStatusSpecified { get; set; }

        // Field 18-28 - Condition Codes.
        public List<string> Field18_28_ConditionCodes { get { return _field18_28_ConditionCodes; } set { _field18_28_ConditionCodes = value; } }

        // Field 29 - Accident State.  This is the state in which the accident occurred.  Situational.
        public string Field29_AccidentState { get { return _field29_AccidentState; } set { _field29_AccidentState = value; } }

        // Field 31 through 34 are occurrence codes and their corresponding dates.
        public List<UB04OccurrenceCodesAndDates> Field31_34_OccurrenceCodesAndDates
        { get { return _field31_34_OccurrenceCodesAndDates; } set { _field31_34_OccurrenceCodesAndDates = value; } }

        // Field 35 and 36 are occurrence codes and their corresponding dates.
        public List<UB04OccurrenceSpanCodesAndDates> Field35_36_OccurrenceSpanCodesAndDates
        { get { return _field35_36_OccurrenceSpanCodesAndDates; } set { _field35_36_OccurrenceSpanCodesAndDates = value; } }

        // Field 38 - Additional name of the person or entity responsible for payment of balance of bill after applicable
        // processing by other parties, insurers or organizations.
        public string Field38_AdditionalPartyName { get;  set; }

        // Field 39 through 41 - Value Codes and Amounts.
        public string Field39a_Code { get; set; }
        public decimal Field39a_Amount { get; set; }
        public string Field39b_Code { get; set; }
        public decimal Field39b_Amount { get; set; }
        //public string Field39b_Code { get; set; }
        //public decimal Field39b_Amount { get; set; }
        public string Field39d_Code { get; set; }
        public decimal Field39d_Amount { get; set; }
        public string Field40a_Code { get; set; }
        public decimal Field40a_Amount { get; set; }
        public string Field40b_Code { get; set; }
        public decimal Field40b_Amount { get; set; }
        //public string Field40b_Code { get; set; }
        //public decimal Field40b_Amount { get; set; }
        public string Field40d_Code { get; set; }
        public decimal Field40d_Amount { get; set; }
        public string Field41a_Code { get; set; }
        public decimal Field41a_Amount { get; set; }
        public string Field41b_Code { get; set; }
        public decimal Field41b_Amount { get; set; }
        //public string Field41b_Code { get; set; }
        //public decimal Field41b_Amount { get; set; }
        public string Field41d_Code { get; set; }
        public decimal Field41d_Amount { get; set; }

        // Field 42 - Up to 22 service lines.
        [XmlElement("Field42_49_ServiceLines")]
        public List<UB04ServiceLine_2300Loop> Field42_49_2300Loop { get; set; }

        // Field 42 through 49 SUMMARY line.
        public List<UB04TotalChargesLine> Field42_49_ServiceLinesTotal
        { get { return _field42_49_ServiceLinesTotal; } set { _field42_49_ServiceLinesTotal = value; } }

        // Field 47 - Summary of all field 47 charges
        public decimal Field47_SummaryTotalCharges { get { return _field47_SummaryTotalCharges; } set { _field47_SummaryTotalCharges = value; } }
        // Field 48 - Summary of all field 48 charges
        public decimal Field48_SummaryTotalNonCoveredCharges { get { return _field48_SummaryTotalNonCoveredCharges; } set { _field48_SummaryTotalNonCoveredCharges = value; } }

        public List<Field50_PayerName> Field50_PayerName { get; set; }
        public List<Field52_ReleaseOfInfoCertIndicator> Field52_ReleaseOfInfoCertIndicator { get; set; }
        public List<Field53_AssignmentOfBenefitsCertIndicator> Field53_AssignmentOfBenefitsCertIndicator { get; set; }
        public List<Field54_PriorPayments> Field54_PriorPayments { get; set; }
        public string Field55PrimaryPayerEstimatedAmountDue { get; set; }
        public string Field55SecondaryPayerEstimatedAmountDue { get; set; }
        public string Field55TertiaryEstimatedAmountDue { get; set; }

        // Field 56 - National Provider Indicator (NPI), Billing Provider.  The unique provider indentifier
        // assigned by the health plan.
        public string Field56_NationalProviderIndicator
        {
            get { return _field56_NationalProviderIndicator; }
            set { _field56_NationalProviderIndicator = value; }
        }

        // Field 57 - Other Provider Identifier.  Not required.
        public string Field57_OtherProviderIdentifier
        {
            get { return _field57_OtherProviderIdentifier; }
            set { _field57_OtherProviderIdentifier = value; }
        }

        // Field 58a through 58c - Insured's name field for primary, secondary or tertiary insurance.
        // Enter name corresponding with items 50a, 50b or 50c.
        public string Field58a_SubscriberLastName
        {
            get { return _field58a_InsuredsLastName; }
            set { _field58a_InsuredsLastName = value; }
        }

        public string Field58b_SubscriberFirstName
        {
            get { return _field58b_InsuredsFirstName; }
            set { _field58b_InsuredsFirstName = value; }
        }

        public string Field58c_SubscriberMiddleName
        {
            get { return _field58c_InsuredsMiddleName; }
            set { _field58c_InsuredsMiddleName = value; }
        }

        // Field 59a through 59c - Patient's relationship to insured.
        public string Field59a_RelationshipToInsured
        {
            get { return _field59a_RelationshipToInsured; }
            set { _field59a_RelationshipToInsured = value; }
        }

        public string Field59b_RelationshipToInsured
        {
            get { return _field59b_RelationshipToInsured; }
            set { _field59b_RelationshipToInsured = value; }
        }

        public string Field59c_RelationshipToInsured
        {
            get { return _field59c_RelationshipToInsured; }
            set { _field59c_RelationshipToInsured = value; }
        }

        // Field 60a through 60c - Insured's Unique Identification number.
        public string Field60a_InsuredsIniqueIdentificationNumber
        {
            get { return _field60a_InsuredsIniqueIdentificationNumber; }
            set { _field60a_InsuredsIniqueIdentificationNumber = value; }
        }

        public string Field60b_InsuredsIniqueIdentificationNumber
        {
            get { return _field60b_InsuredsIniqueIdentificationNumber; }
            set { _field60b_InsuredsIniqueIdentificationNumber = value; }
        }

        public string Field60c_InsuredsIniqueIdentificationNumber
        {
            get { return _field60c_InsuredsIniqueIdentificationNumber; }
            set { _field60c_InsuredsIniqueIdentificationNumber = value; }
        }

        // Field 61a through 61c - Group or Plan Name.
        public string Field61a_InsuredsGroupOrPlanName
        {
            get { return _field61a_InsuredsGroupOrPlanName; }
            set { _field61a_InsuredsGroupOrPlanName = value; }
        }

        public string Field61b_InsuredsGroupOrPlanName
        {
            get { return _field61b_InsuredsGroupOrPlanName; }
            set { _field61b_InsuredsGroupOrPlanName = value; }
        }

        public string Field61c_InsuredsGroupOrPlanName
        {
            get { return _field61c_InsuredsGroupOrPlanName; }
            set { _field61c_InsuredsGroupOrPlanName = value; }
        }

        // Field 62a through 62c - Group or Plan Number.
        public string Field62a_InsuredsGroupOrPlanNumber
        {
            get { return _field62a_InsuredsGroupOrPlanNumber; }
            set { _field62a_InsuredsGroupOrPlanNumber = value; }
        }

        public string Field62b_InsuredsGroupOrPlanNumber
        {
            get { return _field62b_InsuredsGroupOrPlanNumber; }
            set { _field62b_InsuredsGroupOrPlanNumber = value; }
        }

        public string Field62c_InsuredsGroupOrPlanNumber
        {
            get { return _field62c_InsuredsGroupOrPlanNumber; }
            set { _field62c_InsuredsGroupOrPlanNumber = value; }
        }

        // Field 63a through 63c - Treatment authorization codes.
        public string Field63a_TreatmentAuthorizationCodes
        {
            get { return _field63a_TreatmentAuthorizationCodes; }
            set { _field63a_TreatmentAuthorizationCodes = value; }
        }

        public string Field63b_TreatmentAuthorizationCodes
        {
            get { return _field63b_TreatmentAuthorizationCodes; }
            set { _field63b_TreatmentAuthorizationCodes = value; }
        }

        public string Field63c_TreatmentAuthorizationCodes
        {
            get { return _field63c_TreatmentAuthorizationCodes; }
            set { _field63c_TreatmentAuthorizationCodes = value; }
        }

        // Field 64a through 64c - Document Control Number (DCN).
        public string Field64a_DocumentControlNumber
        {
            get { return _field64a_DocumentControlNumber; }
            set { _field64a_DocumentControlNumber = value; }
        }

        public string Field64b_DocumentControlNumber
        {
            get { return _field64b_DocumentControlNumber; }
            set { _field64b_DocumentControlNumber = value; }
        }

        public string Field64c_DocumentControlNumber
        {
            get { return _field64c_DocumentControlNumber; }
            set { _field64c_DocumentControlNumber = value; }
        }

        //// Field 65a through 65c - Employer name (of the insured).
        //public string Field65a_EmployerName
        //{
        //    get { return _field65a_EmployerName; }
        //    set { _field65a_EmployerName = value; }
        //}

        //public string Field65b_EmployerName
        //{
        //    get { return _field65b_EmployerName; }
        //    set { _field65b_EmployerName = value; }
        //}

        //public string Field65c_EmployerName
        //{
        //    get { return _field65c_EmployerName; }
        //    set { _field65c_EmployerName = value; }
        //}

        // Field 66 - Diagnosis and procedure code qualifier.  Not required.
        public string Field66_DiagnosisAndProcedureCodeQualifier
        {
            get { return _field66_DiagnosisAndProcedureCodeQualifier; }
            set { _field66_DiagnosisAndProcedureCodeQualifier = value; }
        }

        // Field 67 - Principle diagnosis code.
        public string Field67_PrincipleDiagCode1_7 { get { return _field67_PrincipleDiagCode1_7; } set { _field67_PrincipleDiagCode1_7 = value; } }
        public string Field67_PrincipleDiagCode8 { get { return _field67_PrincipleDiagCode8; } set { _field67_PrincipleDiagCode8 = value; } }

        // Field 67a through 67q (17 diagnosis codes).
        public string Field67a_OtherDiagCode1_7 { get ; set; }
        public string Field67a_OtherDiagCode8 { get; set; }
        public string Field67b_OtherDiagCode1_7 { get ; set; }
        public string Field67b_OtherDiagCode8 { get ; set; }
        public string Field67c_OtherDiagCode1_7 { get ; set; }
        public string Field67c_OtherDiagCode8 { get ; set; }
        public string Field67d_OtherDiagCode1_7 { get ; set; }
        public string Field67d_OtherDiagCode8 { get ; set; }
        public string Field67e_OtherDiagCode1_7 { get ; set; }
        public string Field67e_OtherDiagCode8 { get ; set; }
        public string Field67f_OtherDiagCode1_7 { get ; set; }
        public string Field67f_OtherDiagCode8 { get ; set; }
        public string Field67g_OtherDiagCode1_7 { get ; set; }
        public string Field67g_OtherDiagCode8 { get ; set; }
        public string Field67h_OtherDiagCode1_7 { get ; set; }
        public string Field67h_OtherDiagCode8 { get ; set; }
        public string Field67i_OtherDiagCode1_7 { get ; set; }
        public string Field67i_OtherDiagCode8 { get ; set; }
        public string Field67j_OtherDiagCode1_7 { get ; set; }
        public string Field67j_OtherDiagCode8 { get ; set; }
        public string Field67k_OtherDiagCode1_7 { get ; set; }
        public string Field67k_OtherDiagCode8 { get ; set; }
        public string Field67l_OtherDiagCode1_7 { get ; set; }
        public string Field67l_OtherDiagCode8 { get ; set; }
        public string Field67m_OtherDiagCode1_7 { get ; set; }
        public string Field67m_OtherDiagCode8 { get ; set; }
        public string Field67n_OtherDiagCode1_7 { get ; set; }
        public string Field67n_OtherDiagCode8 { get ; set; }
        public string Field67o_OtherDiagCode1_7 { get ; set; }
        public string Field67o_OtherDiagCode8 { get ; set; }
        public string Field67p_OtherDiagCode1_7 { get ; set; }
        public string Field67p_OtherDiagCode8 { get ; set; }
        public string Field67q_OtherDiagCode1_7 { get ; set; }
        public string Field67q_OtherDiagCode8 { get ; set; }

        // Field 69 - Admitting Diagnosis code.
        public string Field69_AdmittingDiagnosisCode
        {
            get { return _field69_AdmittingDiagnosisCode; }
            set { _field69_AdmittingDiagnosisCode = value; }
        }

        // Field 70a.  Patient's reason for visit # 1.
        public string Field70a_ReasonForVisit { get; set ; }

        // Field 70b.  Patient's reason for visit # 2.
        public string Field70b_ReasonForVisit { get; set; }

        // Field 70c.  Patient's reason for visit # 3.
        public string Field70c_ReasonForVisit { get; set; }
        
        // Field 71 - Prospective Payment System (PPS) Code.  Identifies the DRG based on the grouper software
        // and is required only when the provider is under contract with a health plan.
        public string Field71_ProspectivePaymentSystemCode
        {
            get { return _field71_ProspectivePaymentSystemCode; }
            set { _field71_ProspectivePaymentSystemCode = value; }
        }

        // Field 72 - External Cause of Injury Code (ECI). Box 1.  Characters 1 through 7.
        public string Field72a_ExternalCauseOfInjuryCode_1_7 { get; set; }
        // Field 72 - External Cause of Injury Code (ECI). Box 1.  8th character.
        public string Field72a_ExternalCauseOfInjuryCode_8 { get; set; }
        // Field 72 - External Cause of Injury Code (ECI). Box 2.  Characters 1 through 7.
        public string Field72b_ExternalCauseOfInjuryCode_1_7 { get; set; }
        // Field 72 - External Cause of Injury Code (ECI). Box 2.  8th character.
        public string Field72b_ExternalCauseOfInjuryCode_8 { get; set; }
        // Field 72 - External Cause of Injury Code (ECI). Box 3.  Characters 1 through 7.
        public string Field72c_ExternalCauseOfInjuryCode_1_7 { get; set; }
        // Field 72 - External Cause of Injury Code (ECI). Box 3.  8th character.
        public string Field72c_ExternalCauseOfInjuryCode_8 { get; set; }

        // Field 74 - PRINCIPAL procedure code and date.
        //public string Field74c_OtherProcedure_Code { get; set; }
        //public string Field74c_OtherProcedure_Date { get; set; }
        // Field 74a - Other procedure code and date group # 1.
        public string Field74a_OtherProcedure_Code { get; set; }
        public string Field74a_OtherProcedure_Date { get; set; }
        // Field 74b - Other procedure code and date group # 2.
        public string Field74b_OtherProcedure_Code { get; set; }
        public string Field74b_OtherProcedure_Date { get; set; }
        // Field 74c - Other procedure code and date group # 3.
        public string Field74c_OtherProcedure_Code { get; set; }
        public string Field74c_OtherProcedure_Date { get; set; }
        // Field 74d - Other procedure code and date group # 4.
        public string Field74d_OtherProcedure_Code { get; set; }
        public string Field74d_OtherProcedure_Date { get; set; }
        // Field 74e - Other procedure code and date group # 5.
        public string Field74e_OtherProcedure_Code { get; set; }
        public string Field74e_OtherProcedure_Date { get; set; }

        // Field 76 - Attending provider names and identifiers.
        public string Field76_AttendingProviderNationalProviderIdentifier { get; set; }
        public string Field76_AttendingProviderSecondaryQualifier { get; set; }
        public string Field76_AttendingProviderSecondaryIdentifier { get; set; }
        public string Field76_AttendingProviderLastName { get; set; }
        public string Field76_AttendingProviderFirstName { get; set; }
        public string Field76_AttendingProviderMiddleName { get; set; }
    
        // Field 77 - Operating physician name and identifiers.
        public string Field77_OperatingPhysicianNationalProviderIdentifier { get; set; }
        public string Field77_OperatingPhysicianSecondaryQualifier { get; set; }
        public string Field77_OperatingPhysicianSecondaryIdentifier { get; set; }
        public string Field77_OperatingPhysicianLastName { get; set; }
        public string Field77_OperatingPhysicianFirstName { get; set; }

        // Field 78_79 - Other Operating, Rendering or Referring Physician
        public string Field78_OtherOperatingNationalProviderIdentifier { get; set; }
        public string Field78_OtherOperatingSecondaryQualifier { get; set; }
        public string Field78_OtherOperatingSecondaryIdentifier { get; set; }
        public string Field78_OtherOperatingLastName { get; set; }
        public string Field78_OtherOperatingFirstName { get; set; }
        public string Field78_OtherOperatingMiddleName { get; set; }
        public string Field78_OtherOperatingSuffix { get; set; }

        // Field 78_79 - Other Operating, Rendering or Referring Physician
        public string Field79_RenderingProviderNationalProviderIdentifier { get; set; }
        public string Field79_RenderingProviderSecondaryQualifier { get; set; }
        public string Field79_RenderingProviderSecondaryIdentifier { get; set; }
        public string Field79_RenderingProviderLastName { get; set; }
        public string Field79_RenderingProviderFirstName { get; set; }
        public string Field79_RenderingProviderMiddleName { get; set; }
        public string Field79_RenderingProviderSuffix { get; set; }

        // Field 78_79 - Other Operating, Rendering or Referring Physician
        public string Field79_ReferringProviderNationalProviderIdentifier { get; set; }
        public string Field79_ReferringProviderSecondaryQualifier { get; set; }
        public string Field79_ReferringProviderSecondaryIdentifier { get; set; }
        public string Field79_ReferringProviderLastName { get; set; }
        public string Field79_ReferringProviderFirstName { get; set; }
        public string Field79_ReferringProviderMiddleName { get; set; }
        public string Field79_ReferringProviderSuffix { get; set; }

        // Field 80 - Remarks Field.  This is a freeform entry field for special notes.
        public string Field80_Remarks { get; set; }

        // Field 81a through 81d - Code-code field.  Used to report codes that overflow other
        // fields and for externally maintained codes NUBC has approved for the institutional data set.
        public string Field81_CodeCode_Note { get; set; }
        public string Field81_CodeCode_ProviderTaxonomyCode { get; set; }
        //public string Field81_CodeCode_b { get; set; }
        //public string Field81_CodeCode_c { get; set; }
        //public string Field81_CodeCode_d { get; set; }

        public string Serialize()
        {
            StringWriter writer = new StringWriter();
            new XmlSerializer(typeof(UB04Claim)).Serialize(writer, this);
            return writer.ToString();
        }

        public static UB04Claim Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UB04Claim));
            return (UB04Claim)serializer.Deserialize(new StringReader(xml));

        }
    }
#endif
}
