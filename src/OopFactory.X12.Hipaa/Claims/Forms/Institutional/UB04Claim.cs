using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
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
        private string _field01_01_ProviderLastName;
        private string _field01_02_ProviderFirstName;
        private string _field01_03_ProviderMiddleName;
        private string _field01_04_ProviderAddress1;
        private string _field01_05_ProviderAddress2;
        private string _field01_06_ProviderCity;
        private string _field01_08_ProviderState;
        private string _field01_09_ProviderZip;
        private string _field01_10_ProviderZip_4;
        private string _field01_11_ProviderPhoneNumber;
        private string _field01_12_ProviderFaxNumber;
        private string _field01_13_ProviderCountryCode;
        private string _field02_01_ProviderLastName;
        private string _field02_02_ProviderFirstName;
        private string _field02_03_ProviderMiddleName;
        private string _field02_04_ProviderAddress1;
        private string _field02_05_ProviderAddress2;
        private string _field02_06_ProviderCity;
        private string _field02_08_ProviderState;
        private string _field02_09_ProviderZip;
        private string _field02_10_ProviderZip_4;
        private string _field02_11_ProviderCountryCode;
        private string _field03a_PatientControlNumber;
        private string _field03b_MedicalHealthRecordNumber;
        private string _field04_TypeOfBill;
        private string _field05_FederalTaxId;
        private string _field06_ServiceFromDate;
        private string _field06_ServiceToDate;
        private string _field07_Filler;
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
        private string _field30_Filler;
        private List<UB04OccurrenceCodesAndDates> _field31_34_OccurrenceCodesAndDates;
        private List<UB04OccurrenceSpanCodesAndDates> _field35_36_OccurrenceSpanCodesAndDates;
        private string _field37_Filler;
        private string _field38_AdditionalPartyName;
        private List<UB04ValueCodesAndAmounts> _field39_41_ValueCodesAndAmounts;
        private List<UB04ServiceLine> _field42_49_ServiceLines;
        private List<UB04TotalChargesLine> _field42_49_ServiceLinesTotal;
        private string _field50a_PayerName;
        private string _field50b_PayerSecondaryInsuranceCompanyName;
        private string _field50c_PayerTertiaryInsuranceCompanyName;
        private string _field51a_Filler;
        private string _field51b_Filler;
        private string _field51c_Filler;
        private string _field52a_ReleaseOfInformationCertificationIndicator;
        private string _field52b_ReleaseOfInformationCertificationIndicator;
        private string _field52c_ReleaseOfInformationCertificationIndicator;
        private string _field53a_AssignmentOfBenefitsCertificationIndicator;
        private string _field53b_AssignmentOfBenefitsCertificationIndicator;
        private string _field53c_AssignmentOfBenefitsCertificationIndicator;
        private decimal _field54a_PriorPayments;
        private decimal _field54b_PriorPayments;
        private decimal _field54c_PriorPayments;
        private decimal _field55a_EstimatedAmountDue;
        private decimal _field55b_EstimatedAmountDue;
        private decimal _field55c_EstimatedAmountDue;
        private string _field56_NationalProviderIndicator;
        private string _field57_OtherProviderIdentifier;
        private string _field58a_InsuredsName;
        private string _field58b_InsuredsName;
        private string _field58c_InsuredsName;
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
        private string _field65a_EmployerName;
        private string _field65b_EmployerName;
        private string _field65c_EmployerName;
        private string _field66_DiagnosisAndProcedureCodeQualifier;
        private string _field67_PrincipleDiagCode;
        private List<string> _field67a_67q_OtherDiagCodes;
        private string _field68_Filler;
        private string _field69_AdmittingDiagnosisCode;
        private List<string> _field70a_70c_ReasonForVisit;
        private string _field71_ProspectivePaymentSystemCode;
        private string _field72_ExternalCauseOfInjuryCode;
        private string _field73_Filler;
        private List<UB04OtherProcedureCodes> _field74_OtherProcedureCodesAndDates;
        private string _field75_Filler;
        private string _field76_AttendingProviderNationalProviderIdentifier;
        private string _field76_AttendingProviderSecondaryQualifier;
        private string _field76_AttendingProviderLastName;
        private string _field76_AttendingProviderFirstName;
        private string _field77_OperatingPhysicianNationalProviderIdentifier;
        private string _field77_OperatingPhysicianSecondaryQualifier;
        private string _field77_OperatingPhysicianLastName;
        private string _field77_OperatingPhysicianFirstName;
        private string _field78_OtherProvider1NationalProviderIdentifier;
        private string _field78_OtherProvider1SecondaryQualifier;
        private string _field78_OtherProvider1LastName;
        private string _field78_OtherProvider1FirstName;
        private string _field79_OtherProvider2NationalProviderIdentifier;
        private string _field79_OtherProvider2SecondaryQualifier;
        private string _field79_OtherProvider2LastName;
        private string _field79_OtherProvider2FirstName;
        private string _field80_Remarks;
        private List<UB04Code_Code> _field80Code_Code;

        public UB04Claim()
        {
            if (_field42_49_ServiceLines == null) _field42_49_ServiceLines = new List<UB04ServiceLine>();
        }

        // Now the accessor definitions:

        // Field 01, the Facility Provider / Billing Provider has many possible parts.  All known potential
        // elements are listed here.
        [XmlAttribute]
        public string Field01_01_ProviderLastName           //<-- Facility name or last name of provider
        {
            get
            {
                return _field01_01_ProviderLastName;
            }
            set
            {
                _field01_01_ProviderLastName = value;
            }
        }

        [XmlAttribute]
        public string Field01_02_ProviderFirstName          //<-- Only if individual provider
        {
            get
            {
                return _field01_02_ProviderFirstName;
            }
            set
            {
                _field01_02_ProviderFirstName = value;
            }
        }

        [XmlAttribute]
        public string Field01_03_ProviderMiddleName          //<-- Only if individual provider
        {
            get
            {
                return _field01_03_ProviderMiddleName;
            }
            set
            {
                _field01_03_ProviderMiddleName = value;
            }
        }

        public string Field01_04_ProviderAddress1
        {
            get
            {
                return _field01_04_ProviderAddress1;
            }
            set
            {
                _field01_04_ProviderAddress1 = value;
            }
        }

        public string Field01_05_ProviderAddress2
        {
            get
            {
                return _field01_05_ProviderAddress2;
            }
            set
            {
                _field01_05_ProviderAddress2 = value;
            }
        }

        public string Field01_06_ProviderCity
        {
            get
            {
                return _field01_06_ProviderCity;
            }
            set
            {
                _field01_06_ProviderCity = value;
            }
        }

        public string Field01_08_ProviderState
        {
            get
            {
                return _field01_08_ProviderState;
            }
            set
            {
                _field01_08_ProviderState = value;
            }
        }

        public string Field01_09_ProviderZip
        {
            get
            {
                return _field01_09_ProviderZip;
            }
            set
            {
                _field01_09_ProviderZip = value;
            }
        }

        public string Field01_10_ProviderZip_4
        {
            get
            {
                return _field01_10_ProviderZip_4;
            }
            set
            {
                _field01_10_ProviderZip_4 = value;
            }
        }

        public string Field01_11_ProviderPhoneNumber
        {
            get
            {
                return _field01_11_ProviderPhoneNumber;
            }
            set
            {
                _field01_11_ProviderPhoneNumber = value;
            }
        }

        public string Field01_12_ProviderFaxNumber
        {
            get
            {
                return _field01_12_ProviderFaxNumber;
            }
            set
            {
                _field01_12_ProviderFaxNumber = value;
            }
        }

        public string Field01_13_ProviderCountryCode
        {
            get
            {
                return _field01_13_ProviderCountryCode;
            }
            set
            {
                _field01_13_ProviderCountryCode = value;
            }
        }

        // Field 02 - the Pay-To provider address.  This is usually provided only when different than Field 01.
        public string Field02_01_ProviderLastName
        {
            get
            {
                return _field02_01_ProviderLastName;
            }
            set
            {
                _field02_01_ProviderLastName = value;
            }
        }

        public string Field02_02_ProviderFirstName
        {
            get
            {
                return _field02_02_ProviderFirstName;
            }
            set
            {
                _field02_02_ProviderFirstName = value;
            }
        }

        public string Field02_03_ProviderMiddleName
        {
            get
            {
                return _field02_03_ProviderMiddleName;
            }
            set
            {
                _field02_03_ProviderMiddleName = value;
            }
        }

        public string Field02_04_ProviderAddress1
        {
            get
            {
                return _field02_04_ProviderAddress1;
            }
            set
            {
                _field02_04_ProviderAddress1 = value;
            }
        }

        public string Field02_05_ProviderAddress2
        {
            get
            {
                return _field02_05_ProviderAddress2;
            }
            set
            {
                _field02_05_ProviderAddress2 = value;
            }
        }

        public string Field02_06_ProviderCity
        {
            get
            {
                return _field02_06_ProviderCity;
            }
            set
            {
                _field02_06_ProviderCity = value;
            }
        }

        public string Field02_08_ProviderState
        {
            get
            {
                return _field02_08_ProviderState;
            }
            set
            {
                _field02_08_ProviderState = value;
            }
        }

        public string Field02_09_ProviderZip
        {
            get
            {
                return _field02_09_ProviderZip;
            }
            set
            {
                _field02_09_ProviderZip = value;
            }
        }

        public string Field02_10_ProviderZip_4
        {
            get
            {
                return _field02_10_ProviderZip_4;
            }
            set
            {
                _field02_10_ProviderZip_4 = value;
            }
        }

        public string Field02_11_ProviderCountryCode
        {
            get
            {
                return _field02_11_ProviderCountryCode;
            }
            set
            {
                _field02_11_ProviderCountryCode = value;
            }
        }

        // Field 03a - a unique alpha-numeric number assigned by the provider.  Used to allow for the retrieval
        // of individual patient financial records.  Optional field.
        public string Field03a_PatientControlNumber
        {
            get
            {
                return _field03a_PatientControlNumber;
            }
            set
            {
                _field03a_PatientControlNumber = value;
            }
        }

        // Field 03b - a value assigned by the provider that indicates the patient's medical record number.
        public string Field03b_MedicalHealthRecordNumber
        {
            get
            {
                return _field03b_MedicalHealthRecordNumber;
            }
            set
            {
                _field03b_MedicalHealthRecordNumber = value;
            }
        }

        // Field 04 - Type of Bill, a three or four digit code that indicates the type of bill being submitted.
        // Refer to the NUBC Guide for TOB frequency codes.  This is set as a string value because it may contain
        // a leading zero.
        public string Field04_TypeOfBill
        {
            get
            {
                return _field04_TypeOfBill;
            }
            set
            {
                _field04_TypeOfBill = value;
            }
        }

        // Field 05 - Federal Tax ID Number.  This field may contain the tax id (TID) or the newer Employer Identification
        // Number (EIN).  Affiliated subsidiaries are identified using federeal tax sub-ID's.
        public string Field05_FederalTaxId
        {
            get
            {
                return _field05_FederalTaxId;
            }
            set
            {
                _field05_FederalTaxId = value;
            }
        }

        // Field 06 - Service FROM and TO dates.  MMDDCCYY format.
        public string Field06_ServiceFromDate
        {
            get
            {
                return _field06_ServiceFromDate;
            }
            set
            {
                _field06_ServiceFromDate = value;
            }
        }

        public string Field06_ServiceToDate
        {
            get
            {
                return _field06_ServiceToDate;
            }
            set
            {
                _field06_ServiceToDate = value;
            }
        }

        // Field 07 - Reserved by NUBC for future use.
        public string Field07_Filler
        {
            get
            {
                return _field07_Filler;
            }
            set
            {
                _field07_Filler = value;
            }
        }

        // Field 08a - Patient Identification Number (Patient ID).
        public string Field08a_PatientIdentifier
        {
            get
            {
                return _field08a_PatientIdentifier;
            }
            set
            {
                _field08a_PatientIdentifier = value;
            }
        }

        // Field 08b-01 PatientLastName.  Required.
        public string Field08b_01_PatientLastName
        {
            get
            {
                return _field08b_01_PatientLastName;
            }
            set
            {
                _field08b_01_PatientLastName = value;
            }
        }

        // Field 08b-01 PatientFirstName
        public string Field08b_02_PatientFirstName
        {
            get
            {
                return _field08b_02_PatientFirstName;
            }
            set
            {
                _field08b_02_PatientFirstName = value;
            }
        }

        // Field 08b-01 PatientMiddleName
        public string Field08b_03_PatientMiddleName
        {
            get
            {
                return _field08b_03_PatientMiddleName;
            }
            set
            {
                _field08b_03_PatientMiddleName = value;
            }
        }

        // Field 09a - Patient Street.  Required.
        public string Field09a_PatientStreet
        {
            get
            {
                return _field09a_PatientStreet;
            }
            set
            {
                _field09a_PatientStreet = value;
            }
        }

        // Field 09b - Patient City.  Required.
        public string Field09b_PatientCity
        {
            get
            {
                return _field09b_PatientCity;
            }
            set
            {
                _field09b_PatientCity = value;
            }
        }

        // Field 09c - Patient State.  Required.
        public string Field09c_PatientState
        {
            get
            {
                return _field09c_PatientState;
            }
            set
            {
                _field09c_PatientState = value;
            }
        }

        // Field 09d - Patient Zip.  Required.
        public string Field09d_PatientZip
        {
            get
            {
                return _field09d_PatientZip;
            }
            set
            {
                _field09d_PatientZip = value;
            }
        }

        // Field 09e - Patient Country Code.  Not required.
        public string Field09e_PatientCountry
        {
            get
            {
                return _field09e_PatientCountry;
            }
            set
            {
                _field09e_PatientCountry = value;
            }
        }

        // Field 10 - Patient Date of Birth (DOB) in MMDDCCYY format
        public string Field10_PatientDOB
        {
            get
            {
                return _field10_PatientDOB;
            }
            set
            {
                _field10_PatientDOB = value;
            }
        }

        // Field 11 - Gender/Sex.  'M' = Male; 'F' = Female
        public string Field11_Sex
        {
            get
            {
                return _field11_Sex;
            }
            set
            {
                _field11_Sex = value;
            }
        }

        // Field 12 - Admission Date / Start of Care Date.  This is the date that patient care actually begins.  For
        // inpatient care it is the admission date.  For other types it is the day the care begins.
        public string Field12_AdmissionDate
        {
            get
            {
                return _field12_AdmissionDate;
            }
            set
            {
                _field12_AdmissionDate = value;
            }
        }

        // Field 13 - Admission Hour.  A two-digit code indicating the hour of day that the care began (when they were admitted).
        // Use military time (00 through 24).
        public string Field13_AdmissionHour
        {
            get
            {
                return _field13_AdmissionHour;
            }
            set
            {
                _field13_AdmissionHour = value;
            }
        }

        // Field 14 - Priority (Type) of Visit.  The code for the priority of the admission or visit.
        public string Field14_TypeOfVisit
        {
            get
            {
                return _field14_TypeOfVisit;
            }
            set
            {
                _field14_TypeOfVisit = value;
            }
        }

        // Field 15 - Point of Origina / Source of Admission or Visit.  Indicates the source of the referral for visit or 
        // admission (e.g., physician, clinic, facility, transfer, etc.).  Usually a single alpha-numeric digit.
        public string Field15_SourceOfAdmission
        {
            get
            {
                return _field15_SourceOfAdmission;
            }
            set
            {
                _field15_SourceOfAdmission = value;
            }
        }

        // Field 16 - Discharge Hour.  A two-digit code indicating the hour of day that the care ended (when they were discharged).
        // Use military time (00 through 24).
        public string Field16_DischargeHour
        {
            get
            {
                return _field16_DischargeHour;
            }
            set
            {
                _field16_DischargeHour = value;
            }
        }
        
        /// <summary>
        /// Field 17 - Patient Discharge Status.  Reports status of patient upon discharge - required for institutional claims. 
        /// Two digit numeric.
        /// </summary>
        [XmlAttribute]
        public string Field17_PatientDischargeStatus
        {
            get
            {
                return _field17_PatientDischargeStatus;
            }
            set
            {
                _field17_PatientDischargeStatus = value;
            }
        }

        [XmlIgnore]
        public bool Field17_PatientDischargeStatusSpecified { get; set; }

        // Field 18-28 - Condition Codes.
        public List<string> Field18_28_ConditionCodes
        {
            get
            {
                return _field18_28_ConditionCodes;
            }
            set
            {
                _field18_28_ConditionCodes = value;
            }
        }

        // Field 29 - Accident State.  This is the state in which the accident occurred.  Situational.
        public string Field29_AccidentState
        {
            get
            {
                return _field29_AccidentState;
            }
            set
            {
                _field29_AccidentState = value;
            }
        }

        // Field 30 - Reserved by NUBC for future use.
        public string Field30_Filler
        {
            get
            {
                return _field30_Filler;
            }
            set
            {
                _field30_Filler = ""; // Default for this 'always blank' field.
            }
        }

        // Field 31 through 34 are occurrence codes and their corresponding dates.
        public List<UB04OccurrenceCodesAndDates> Field31_34_OccurrenceCodesAndDates
        {
            get
            {
                return _field31_34_OccurrenceCodesAndDates;
            }
            set
            {
                _field31_34_OccurrenceCodesAndDates = value;
            }
        }

        // Field 35 and 36 are occurrence codes and their corresponding dates.
        public List<UB04OccurrenceSpanCodesAndDates> Field35_36_OccurrenceSpanCodesAndDates
        {
            get
            {
                return _field35_36_OccurrenceSpanCodesAndDates;
            }
            set
            {
                _field35_36_OccurrenceSpanCodesAndDates = value;
            }
        }

        // Field 37 - Reserved by NUBC for future use.
        public string Field37_Filler
        {
            get
            {
                return _field37_Filler;
            }
            set
            {
                _field37_Filler = value;
            }
        }

        // Field 38 - Additional name of the person or entity responsible for payment of balance of bill after applicable
        // processing by other parties, insurers or organizations.
        public string Field38_AdditionalPartyName
        {
            get
            {
                return _field38_AdditionalPartyName;
            }
            set
            {
                _field38_AdditionalPartyName = value;
            }
        }

        // Field 39 through 41 - Value Codes and Amounts.
        public List<UB04ValueCodesAndAmounts> Field39_41_ValueCodesAndAmounts
        {
            get
            {
                return _field39_41_ValueCodesAndAmounts;
            }
            set
            {
                _field39_41_ValueCodesAndAmounts = value;
            }
        }

        // Field 42 - Up to 22 service lines.
        [XmlElement("Field42_49_ServiceLine")]
        public List<UB04ServiceLine> Field42_49_ServiceLines
        {
            get
            {
                return _field42_49_ServiceLines;
            }
            set
            {
                _field42_49_ServiceLines = value;
            }
        }

        // Field 42 through 49 SUMMARY line.
        public List<UB04TotalChargesLine> Field42_49_ServiceLinesTotal
        {
            get
            {
                return _field42_49_ServiceLinesTotal;
            }
            set
            {
                _field42_49_ServiceLinesTotal = value;
            }
        }

        // Field 50a - Payer Name
        public string Field50a_PayerName
        {
            get
            {
                return _field50a_PayerName;
            }
            set
            {
                _field50a_PayerName = value;
            }
        }
        
        // Field 50b - Payer secondary insurance company name
        public string Field50b_PayerSecondaryInsuranceCompanyName
        {
            get { return _field50b_PayerSecondaryInsuranceCompanyName; }
            set { _field50b_PayerSecondaryInsuranceCompanyName = value; }
        }

        // Field 50c - Payer tertiary insurance carrier name, if any.
        public string Field50c_PayerTertiaryInsuranceCompanyName
        {
            get { return _field50c_PayerTertiaryInsuranceCompanyName; }
            set { _field50c_PayerTertiaryInsuranceCompanyName = value; }
        }

        // Field 51a through 51c - leave blank.
        public string Field51a_Filler
        {
            get { return _field51a_Filler; }
            set { _field51a_Filler = value; }
        }

        public string Field51b_Filler
        {
            get { return _field51b_Filler; }
            set { _field51b_Filler = value; }
        }

        public string Field51c_Filler
        {
            get { return _field51c_Filler; }
            set { _field51c_Filler = value; }
        }

        // Field 52 - Release of Information Certification Indicator.  Not required.
        public string Field52a_ReleaseOfInformationCertificationIndicator
        {
            get { return _field52a_ReleaseOfInformationCertificationIndicator; }
            set { _field52a_ReleaseOfInformationCertificationIndicator = value; }
        }

        public string Field52b_ReleaseOfInformationCertificationIndicator
        {
            get { return _field52b_ReleaseOfInformationCertificationIndicator; }
            set { _field52b_ReleaseOfInformationCertificationIndicator = value; }
        }

        public string Field52c_ReleaseOfInformationCertificationIndicator
        {
            get { return _field52c_ReleaseOfInformationCertificationIndicator; }
            set { _field52c_ReleaseOfInformationCertificationIndicator = value; }
        }

        // Field 53a through 53c - Assignment of benefits certification indicator.  Enter 'Y' or 'N' corresponding 
        // with item 50a, 50b or 50c.
        public string Field53a_AssignmentOfBenefitsCertificationIndicator
        {
            get { return _field53a_AssignmentOfBenefitsCertificationIndicator; }
            set { _field53a_AssignmentOfBenefitsCertificationIndicator = value; }
        }

        public string Field53b_AssignmentOfBenefitsCertificationIndicator
        {
            get { return _field53b_AssignmentOfBenefitsCertificationIndicator; }
            set { _field53b_AssignmentOfBenefitsCertificationIndicator = value; }
        }

        public string Field53c_AssignmentOfBenefitsCertificationIndicator
        {
            get { return _field53c_AssignmentOfBenefitsCertificationIndicator; }
            set { _field53c_AssignmentOfBenefitsCertificationIndicator = value; }
        }

        // Field 54a through 54c - Prior payments by Payer.  Enter a $ amount corresponding 
        // with items 50a, 50b or 50c.
        public string Field54a_PriorPayments
        {
            get { return _field54a_PriorPayments; }
            set { _field54a_PriorPayments = value; }
        }

        public string Field54b_PriorPayments
        {
            get { return _field54b_PriorPayments; }
            set { _field54b_PriorPayments = value; }
        }

        public string Field54c_PriorPayments
        {
            get { return _field54c_PriorPayments; }
            set { _field54c_PriorPayments = value; }
        }

        // Field 55a through 55c - Estimated $ amount due.  Enter a $ amount corresponding 
        // with item 50a, 50b or 50c depending on who 
        public string Field55a_EstimatedAmountDue
        {
            get { return _field55a_EstimatedAmountDue; }
            set { _field55a_EstimatedAmountDue = value; }
        }

        public string Field55b_EstimatedAmountDue
        {
            get { return _field55b_EstimatedAmountDue; }
            set { _field55b_EstimatedAmountDue = value; }
        }

        public string Field55c_EstimatedAmountDue
        {
            get { return _field55c_EstimatedAmountDue; }
            set { _field55c_EstimatedAmountDue = value; }
        }

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
        public string Field58a_InsuredsName
        {
            get { return _field58a_InsuredsName; }
            set { _field58a_InsuredsName = value; }
        }

        public string Field58b_InsuredsName
        {
            get { return _field58b_InsuredsName; }
            set { _field58b_InsuredsName = value; }
        }

        public string Field58c_InsuredsName
        {
            get { return _field58c_InsuredsName; }
            set { _field58c_InsuredsName = value; }
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

        // Field 65a through 65c - Employer name (of the insured).
        public string Field65a_EmployerName
        {
            get { return _field65a_EmployerName; }
            set { _field65a_EmployerName = value; }
        }

        public string Field65b_EmployerName
        {
            get { return _field65b_EmployerName; }
            set { _field65b_EmployerName = value; }
        }

        public string Field65c_EmployerName
        {
            get { return _field65c_EmployerName; }
            set { _field65c_EmployerName = value; }
        }

        // Field 66 - Diagnosis and procedure code qualifier.  Not required.
        public string Field66_DiagnosisAndProcedureCodeQualifier
        {
            get { return _field66_DiagnosisAndProcedureCodeQualifier; }
            set { _field66_DiagnosisAndProcedureCodeQualifier = value; }
        }

        // Field 67 - Principle diagnosis code.
        public string Field67_PrincipleDiagCode
        {
            get { return _field67_PrincipleDiagCode; }
            set { _field67_PrincipleDiagCode = value; }
        }

        // Field 67a through 67q (17 diagnosis codes).
        public List<string> Field67a_67q_OtherDiagCodes
        {
            get { return _field67a_67q_OtherDiagCodes; }
            set { _field67a_67q_OtherDiagCodes = value; }
        }

        // Field 68 - Reserved by NUBC for future use.
        public string Field68_Filler
        {
            get { return _field68_Filler; }
            set { _field68_Filler = value; }
        }

        // Field 69 - Admitting Diagnosis code.
        public string Field69_AdmittingDiagnosisCode
        {
            get { return _field69_AdmittingDiagnosisCode; }
            set { _field69_AdmittingDiagnosisCode = value; }
        }

        // Field 70a through 70c.  Patient's reason for visit.
        public List<string> Field70a_70c_ReasonForVisit
        {
            get { return _field70a_70c_ReasonForVisit; }
            set { _field70a_70c_ReasonForVisit = value; }
        }

        // Field 71 - Prospective Payment System (PPS) Code.  Identifies the DRG based on the grouper software
        // and is required only when the provider is under contract with a health plan.
        public string Field71_ProspectivePaymentSystemCode
        {
            get { return _field71_ProspectivePaymentSystemCode; }
            set { _field71_ProspectivePaymentSystemCode = value; }
        }

        // Field 72 - External Cause of Injury Code (ECI).
        public string Field72_ExternalCauseOfInjuryCode
        {
            get { return _field72_ExternalCauseOfInjuryCode; }
            set { _field72_ExternalCauseOfInjuryCode = value; }
        }

        // Field 73 - Reserved by NUBC for future use.
        public string Field73_Filler
        {
            get { return _field73_Filler; }
            set { _field73_Filler = value; }
        }

        // Field 74a through 74c - Other procedure codes and dates.
        public List<UB04OtherProcedureCodes> Field74_OtherProcedureCodesAndDates
        {
            get { return _field74_OtherProcedureCodesAndDates; }
            set { _field74_OtherProcedureCodesAndDates = value; }
        }

        // Field 75 - Reserved by NUBC for future use.
        public string Field75_Filler
        {
            get { return _field75_Filler; }
            set { _field75_Filler = value; }
        }

        // Field 76 - Attending provider names and identifiers.
        public string Field76_AttendingProviderNationalProviderIdentifier
        {
            get { return _field76_AttendingProviderNationalProviderIdentifier; }
            set { _field76_AttendingProviderNationalProviderIdentifier = value; }
        }

        public string Field76_AttendingProviderSecondaryQualifier
        {
            get { return _field76_AttendingProviderSecondaryQualifier; }
            set { _field76_AttendingProviderSecondaryQualifier = value; }
        }

        public string Field76_AttendingProviderLastName
        {
            get { return _field76_AttendingProviderLastName; }
            set { _field76_AttendingProviderLastName = value; }
        }

        public string Field76_AttendingProviderFirstName
        {
            get { return _field76_AttendingProviderFirstName; }
            set { _field76_AttendingProviderFirstName = value; }
        }

        // Field 77 - Operating physician name and identifiers.
        public string Field77_OperatingPhysicianNationalProviderIdentifier
        {
            get { return _field77_OperatingPhysicianNationalProviderIdentifier; }
            set { _field77_OperatingPhysicianNationalProviderIdentifier = value; }
        }

        public string Field77_OperatingPhysicianSecondaryQualifier
        {
            get { return _field77_OperatingPhysicianSecondaryQualifier; }
            set { _field77_OperatingPhysicianSecondaryQualifier = value; }
        }

        public string Field77_OperatingPhysicianLastName
        {
            get { return _field77_OperatingPhysicianLastName; }
            set { _field77_OperatingPhysicianLastName = value; }
        }

        public string Field77_OperatingPhysicianFirstName
        {
            get { return _field77_OperatingPhysicianFirstName; }
            set { _field77_OperatingPhysicianFirstName = value; }
        }

        // Field 78 - Other provider # 1 name and identifiers.
        public string Field78_OtherProvider1NationalProviderIdentifier
        {
            get { return _field78_OtherProvider1NationalProviderIdentifier; }
            set { _field78_OtherProvider1NationalProviderIdentifier = value; }
        }

        public string Field78_OtherProvider1SecondaryQualifier
        {
            get { return _field78_OtherProvider1SecondaryQualifier; }
            set { _field78_OtherProvider1SecondaryQualifier = value; }
        }

        public string Field78_OtherProvider1LastName
        {
            get { return _field78_OtherProvider1LastName; }
            set { _field78_OtherProvider1LastName = value; }
        }

        public string Field78_OtherProvider1FirstName
        {
            get { return _field78_OtherProvider1FirstName; }
            set { _field78_OtherProvider1FirstName = value; }
        }

        // Field 79 - Other provider # 2 name and identifiers.
        public string Field79_OtherProvider2NationalProviderIdentifier
        {
            get { return _field79_OtherProvider2NationalProviderIdentifier; }
            set { _field79_OtherProvider2NationalProviderIdentifier = value; }
        }

        public string Field79_OtherProvider2SecondaryQualifier
        {
            get { return _field79_OtherProvider2SecondaryQualifier; }
            set { _field79_OtherProvider2SecondaryQualifier = value; }
        }

        public string Field79_OtherProvider2LastName
        {
            get { return _field79_OtherProvider2LastName; }
            set { _field79_OtherProvider2LastName = value; }
        }

        public string Field79_OtherProvider2FirstName
        {
            get { return _field79_OtherProvider2FirstName; }
            set { _field79_OtherProvider2FirstName = value; }
        }

        // Field 80 - Remarks Field.  This is a freeform entry field for special notes.
        public string Field80_Remarks
        {
            get { return _field80_Remarks; }
            set { _field80_Remarks = value; }
        }

        // Field 81a through 81d - Code-code field.  Used to report codes that overflow other
        // fields and for externally maintained codes NUBC has approved for the institutional data set.
        public List<UB04Code_Code> Field80Code_Code
        {
            get { return _field80Code_Code; }
            set { _field80Code_Code = value; }
        }

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
}
