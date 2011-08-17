using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Professional
{
    class HCFA1500Claim
    {
        /*
         * 2011/8/16, jhalliday - New Data Model for 837P (Professional) claim.
         * 
         * Team: dstrubhar, jhalliday and epkrause
         * 
         * Purpose:
         * To create a C# object model that will serve as a container for the X12 837P data
         * AS ENTERED from a HCFA 1500 Professional claim form.
         * 
         * Goal:
         * The team has the overall goal of creating tools that can be used to consume and
         * manipulate X12 messages (AKA files/documents) without the need to have a big project
         * budget.  For that reason, this and the related X12 Parser project tools are all open
         * source and freely usable.
         */

        // Fields in the HCFA1500 object model are defined in the order they appear on the HCFA1500 form.

        // First, we will declare the private variables.

        // Field 01 is the MEDICARE/MEDICAID/TRICARE-CHAMPUS/CHAMPVA/GROUP_HEALTH_PLAN/FECA_BLACK_LUNG field and is not in the X12 specification.
        private string _field01b_InsuredsIDNumber;
        private string _field02_PatientsLastName;                               // HCFA 1500 standard allows 29 total characters for these (3) fields
        private string _field02_PatientsFirstName;
        private string _field02_PatientsMiddleName;
        private string _field03_PatientsDateOfBirth;                            // MMDDCCYY - 8 characters
        private string _field03_PatientsSex;                                    // 1 = Male, 2 = Female;  1 character.
        private string _field04_InsuredsLastName;                               // HCFA 1500 standard allows 29 total characters for these (3) fields
        private string _field04_InsuredsFirstName;
        private string _field04_InsuredsMiddleName;
        private string _field05_PatientsAddress_Street;                         // 28 characters
        private string _field05_PatientsAddress_City;                           // 24 characters
        private string _field05_PatientsAddress_State;                          // 3 characters
        private string _field05_PatientsAddress_Zip;                            // 12 characters
        private string _field05_PatientsAreaCode;                               // 3 digits
        private string _field05_PatientsPhoneNumber;                            // 10 digits
        private string _field06_PatientRelationshipToInsured;                   // 1 digit.  1 = Self, 2 = Spouse, 3 = Child, 4 = Other
        private string _field07_InsuredsAddress_Street;                         // 29 characters (yes, '29' not 28)
        private string _field07_InsuredsAddress_City;                           // 23 characters (yes, '23' not 24)
        private string _field07_InsuredsAddress_State;                          // 4 characters (yes, '4' not 3)
        private string _field07_InsuredsAddress_Zip;                            // 12 characters
        private string _field07_InsuredsAreaCode;                               // 3 digits
        private string _field07_InsuredsPhoneNumber;                            // 10 digits
        private string _field08_PatientStatus;                                  // 1 digit.  1 = Single, 2 = Married, 3 = Other, 4 = Employed, 5 = Full-time Student, 6 = Part-Time Student
        private string _field09_OtherInsuredsLastName;                          // HCFA 1500 standard allows 28 total characters for these (3) fields
        private string _field09_OtherInsuredsFirstName;
        private string _field09_OtherInsuredsMiddleName;
        private string _field09a_OtherInsuredsPolicyOrGroup;                    // 28 characters
        private string _field09b_OtherInsuredsDateOfBirth;                      // MMDDCCYY - 8 characters
        private string _field09b_OtherInsuredsSex;                              // 1 = Male, 2 = Female;  1 character.
        private string _field09c_OtherInsuredsEmployerNameOrSchoolName;         // 28 characters
        private string _field09d_OtherInsuredsInsurancePlanNameOrProgramName;   // 28 characters
        private string _field10a_PatientConditionRelatedToEmployment;           // 1 = Yes, 2 = No
        private string _field10b_PatientConditionRelatedToAutoAccident;         // 1 = Yes, 2 = No
        private string _field10c_PatientConditionRelatedToOtherAccident;        // 1 = Yes, 2 = No
        private string _field10_PatientConditionRelatedTo_State;                // 2 characters
        private string _field10d_ReservedForLocalUse;                           // 19 characters_
        private string _field11_InsuredsPolicyGroupOfFECANumber;                // 29 characters
        private string _field11a_InsuredsDateOfBirth;                           // MMDDCCYY - 8 characters
        private string _field11a_InsuredsSex;                                   // 1 = Male, 2 = Female;  1 character.
        private string _field11b_InsuredsEmployerOrSchool;                      // 29 characters
        private string _field11c_InsuredsPlanOrProgramName;                     // 29 characters
        private string _field11d_IsThereOtherHealthBenefitPlan;                 // 1 = Yes, 2 = No
        private string _field12_PatientsOrAuthorizedSignature;                  // Signed field.  Store 1 = Signature On File, 2 = Signature NOT On File.  If SOF, enter date in next field
        private string _field12_PatientsOrAuthorizedSignatureDate;              // MMDDCCYY 
        private string _field13_InsuredsOrAuthorizedSignature;                  // Signed field.  Store 1 = Signature On File, 2 = Signature NOT On File.  If SOF, enter date in next field
        private string _field14_DateOfCurrentIllnessInjuryOrPregnancy;          // MMDDCCYY
        private string _field15_DatePatientHadSameOrSimilarIllness;             // MMDDCCYY
        private string _field16_DatePatientUnableToWork_Start;                  // MMDDCCYY
        private string _field16_DatePatientUnableToWork_End;                    // MMDDCCYY
        private string _field17_ReferringProviderOrOtherSource_LastName;        // HCFA 1500 standard allows 28 total characters for these (3) fields
        private string _field17_ReferringProviderOrOtherSource_FirstName;
        private string _field17_ReferringProviderOrOtherSource_MiddleName;
        private string _field17_ReferringProviderOrOtherSource_Credentials;     // ie., 'MD', 'MRCVS'
        private string _field17a_OtherID_Qualifier;                             // 2 digit alpha-numeric value
        private string _field17a_OtherID_Number;                                // 17 characters
        private string _field17b_NationalProviderIdentifier;                    // 10 digit numeric
        private string _field18_HospitalizationDateFrom;                        // MMDDCCYY
        private string _field18_HospitalizationDateTo;                          // MMDDCCYY
        private string _field19_ReservedForLocalUse;                            // 83 characters
        private string _field20_OutsideLab;                                     // 1 = Yes, 2 = No
        private string _field20_OutsideLabCharge;                               // 8 digit numeric with implied decimal.  ie '20300' is $203.00.
        private HCFA1500DiagCodes _field21_Diagnosis_1;                         // 3-1-4 part diagnosis code.
        private HCFA1500DiagCodes _field21_Diagnosis_2;                         // 3-1-4 part diagnosis code.
        private HCFA1500DiagCodes _field21_Diagnosis_3;                         // 3-1-4 part diagnosis code.
        private HCFA1500DiagCodes _field21_Diagnosis_4;                         // 3-1-4 part diagnosis code.
        private string _field22_MedicaidSubmissionCode;                         // 11 characters
        private string _field22_OriginalReferenceNumber;                        // 18 characters
        private string _field23_PriorAuthorizationNumber;                       // 29 characters
        private HCFA1500ServiceLines _field24SvcLines;                          // Service line details
        private string _field25_FederalTaxIDNumber;                             // 15 characters
        private string _field25_SSN_Or_EIN;                                     // 1 = SSN, 2 = EIN
        private string _field26_PatientAccountNumber;                           // 14 characters
        private string _field27_AcceptAssignment;                               // 1 = Yes, 2 = No.  Refers to acceptance of terms of payor's program.
        private string _field28_TotalChargeDollars;                             // 7 digits
        private string _field28_TotalChargeCents;                               // 2 digits
        private string _field29_AmountPaidDollars;                              // 6 digits
        private string _field29_AmountPaidCents;                                // 2 digits
        private string _field30_BalanceDueDollars;                              // 6 digits
        private string _field30_BalanceDueCents;                                // 2 digits
        private string _field31_PhysicianOrSupplierSignature;                   // Signed field.  Store 1 = Signature On File, 2 = Signature NOT On File.  If SOF, enter date in next field
        private string _field31_PhysicianOrSupplierSignatureDate;               // MMDDCCYY 
        private string _field32_FacilityLocationInfo_Name;                      // 26 characters
        private string _field32_FacilityLocationInfo_Street;                    // 26 characters
        private string _field32_FacilityLocationInfo_City;                      // 26 characters for this and next two fields combined
        private string _field32_FacilityLocationInfo_State;                     //
        private string _field32_FacilityLocationInfo_Zip;                       //
        private string _field32a_FacilityNationalProviderIdentifier;            // 10 characters
        private string _field32b_FacilityOtherID;                               // 14 characters
        private string _field33_BillingProvider_AreaCode;                       // 3 characters
        private string _field33_BillingProvider_PhoneNumber;                    // 9 characters
        private string _field33_BillingProvider_Name;                           // 29 characters
        private string _field33_BillingProvider_Street;                         // 29 characters
        private string _field33_BillingProvider_City;                           // 29 characters for this and next two fields combined
        private string _field33_BillingProvider_State;                          //
        private string _field33_BillingProvider_Zip;                            //
        private string _field33a_BillingProviderNationalProviderIdentifier;     // 10 characters
        private string _field33b_BillingProviderOtherID;                        // 17 characters


        // Next we will create the properties and their accessors.
                public string Field01b_InsuredsIDNumber 
        { 
            get { return _field01b_InsuredsIDNumber; } 
            set { _field01b_InsuredsIDNumber = value; } 
        }

        public string Field02_PatientsLastName                                // HCFA 1500 standard allows 29 total characters for these (3) fields
        { 
            get { return _field02_PatientsLastName; } 
            set { _field02_PatientsLastName = value; } 
        }

        public string Field02_PatientsFirstName 
        { 
            get { return _field02_PatientsFirstName; } 
            set { _field02_PatientsFirstName = value; } 
        }

        public string Field02_PatientsMiddleName 
        { 
            get { return _field02_PatientsMiddleName; } 
            set { _field02_PatientsMiddleName = value; } 
        }

        public string Field03_PatientsDateOfBirth                             // MMDDCCYY - 8 characters
        { 
            get { return _field03_PatientsDateOfBirth; } 
            set { _field03_PatientsDateOfBirth = value; } 
        }

        public string Field03_PatientsSex                                     // 1 = Male, 2 = Female;  1 character.
        { 
            get { return _field03_PatientsSex; }
            set { _field03_PatientsSex = value; }
        }

        public string Field04_InsuredsLastName                               // HCFA 1500 standard allows 29 total characters for these (3) fields
        { 
            get { return _field04_InsuredsLastName; } 
            set { _field04_InsuredsLastName = value; } 
        }

        public string Field04_InsuredsFirstName
        { 
            get { return _field04_InsuredsFirstName; } 
            set { _field04_InsuredsFirstName = value; } 
        }

        public string Field04_InsuredsMiddleName
        { 
            get { return _field04_InsuredsMiddleName; }
            set { _field04_InsuredsMiddleName = value; } 
        }

        public string Field05_PatientsAddress_Street                          // 28 characters
        { 
            get { return _field05_PatientsAddress_Street; } 
            set { _field05_PatientsAddress_Street = value; } 
        }

        public string Field05_PatientsAddress_City                            // 24 characters
        { 
            get { return _field05_PatientsAddress_City; }
            set { _field05_PatientsAddress_City = value; }
        }

        public string Field05_PatientsAddress_State                           // 3 characters
        { 
            get { return _field05_PatientsAddress_State; } 
            set { _field05_PatientsAddress_State = value; } 
        }

        public string Field05_PatientsAddress_Zip                             // 12 characters
        { 
            get { return _field05_PatientsAddress_Zip; } 
            set { _field05_PatientsAddress_Zip = value; } 
        }

        public string Field05_PatientsAreaCode                                // 3 digits
        { 
            get { return _field05_PatientsAreaCode; } 
            set { _field05_PatientsAreaCode = value; } 
        }

        public string Field05_PatientsPhoneNumber                             // 10 digits
        { 
            get { return _field05_PatientsPhoneNumber; } 
            set { _field05_PatientsPhoneNumber = value; } 
        }

        public string Field06_PatientRelationshipToInsured                    // 1 digit.  1 = Self, 2 = Spouse, 3 = Child, 4 = Other
        { 
            get { return _field06_PatientRelationshipToInsured; } 
            set { _field06_PatientRelationshipToInsured = value; }
        }

        public string Field07_InsuredsAddress_Street                         // 29 characters (yes, '29' not 28)
        { 
            get { return _field07_InsuredsAddress_Street; } 
            set { _field07_InsuredsAddress_Street = value; } 
        }

        public string Field07_InsuredsAddress_City                           // 23 characters (yes, '23' not 24)
        { 
            get { return _field07_InsuredsAddress_City; } 
            set { _field07_InsuredsAddress_City = value; } 
        }

        public string Field07_InsuredsAddress_State                           // 4 characters (yes, '4' not 3)
        { 
            get { return _field07_InsuredsAddress_State; } 
            set { _field07_InsuredsAddress_State = value; } 
        }

        public string Field07_InsuredsAddress_Zip                            // 12 characters
        { 
            get { return _field07_InsuredsAddress_Zip; } 
            set { _field07_InsuredsAddress_Zip = value; } 
        }

        public string Field07_InsuredsAreaCode                                // 3 digits
        { 
            get { return _field07_InsuredsAreaCode; } 
            set { _field07_InsuredsAreaCode = value; } 
        }

        public string Field07_InsuredsPhoneNumber                             // 10 digits
        { 
            get { return _field07_InsuredsPhoneNumber; } 
            set { _field07_InsuredsPhoneNumber = value; } 
        }

        public string Field08_PatientStatus                                  // 1 digit.  1 = Single, 2 = Married, 3 = Other, 4 = Employed, 5 = Full-time Student, 6 = Part-Time Student
        { 
            get { return _field08_PatientStatus; }
            set { _field08_PatientStatus = value; } 
        }

        public string Field09_OtherInsuredsLastName                           // HCFA 1500 standard allows 28 total characters for these (3) fields
        { 
            get { return _field09_OtherInsuredsLastName; } 
            set { _field09_OtherInsuredsLastName = value; } 
        }

        public string Field09_OtherInsuredsFirstName 
        { 
            get { return _field09_OtherInsuredsFirstName; } 
            set { _field09_OtherInsuredsFirstName = value; }
        }

        public string Field09_OtherInsuredsMiddleName 
        { 
            get { return _field09_OtherInsuredsMiddleName; }
            set { _field09_OtherInsuredsMiddleName = value; }
        }

        public string Field09a_OtherInsuredsPolicyOrGroup                     // 28 characters
        {
            get { return _field09a_OtherInsuredsPolicyOrGroup; } 
            set { _field09a_OtherInsuredsPolicyOrGroup = value; }
        }

        public string Field09b_OtherInsuredsDateOfBirth                       // MMDDCCYY - 8 characters
        { 
            get { return _field09b_OtherInsuredsDateOfBirth; } 
            set { _field09b_OtherInsuredsDateOfBirth = value; }
        }

        public string Field09b_OtherInsuredsSex                               // 1 = Male, 2 = Female;  1 character.
        { 
            get { return _field09b_OtherInsuredsSex; }
            set { _field09b_OtherInsuredsSex = value; } 
        }

        public string Field09c_OtherInsuredsEmployerNameOrSchoolName          // 28 characters
        { 
            get { return _field09c_OtherInsuredsEmployerNameOrSchoolName; } 
            set { _field09c_OtherInsuredsEmployerNameOrSchoolName = value; } 
        }

        public string Field09d_OtherInsuredsInsurancePlanNameOrProgramName    // 28 characters
        { 
            get { return _field09d_OtherInsuredsInsurancePlanNameOrProgramName; } 
            set { _field09d_OtherInsuredsInsurancePlanNameOrProgramName = value; }
        }

        public string Field10a_PatientConditionRelatedToEmployment            // 1 = Yes, 2 = No
        { 
            get { return _field10a_PatientConditionRelatedToEmployment; }
            set { _field10a_PatientConditionRelatedToEmployment = value; } 
        }

        public string Field10b_PatientConditionRelatedToAutoAccident          // 1 = Yes, 2 = No
        { 
            get { return _field10b_PatientConditionRelatedToAutoAccident; }
            set { _field10b_PatientConditionRelatedToAutoAccident = value; } 
        }

        public string Field10c_PatientConditionRelatedToOtherAccident         // 1 = Yes, 2 = No
        { 
            get { return _field10c_PatientConditionRelatedToOtherAccident; } 
            set { _field10c_PatientConditionRelatedToOtherAccident = value; } 
        }

        public string Field10_PatientConditionRelatedTo_State                 // 2 characters
        { 
            get { return _field10_PatientConditionRelatedTo_State; } 
            set { _field10_PatientConditionRelatedTo_State = value; }
        }

        public string Field10d_ReservedForLocalUse                            // 19 characters
        { 
            get { return _field10d_ReservedForLocalUse; } 
            set { _field10d_ReservedForLocalUse = value; } 
        }

        public string Field11_InsuredsPolicyGroupOfFECANumber                 // 29 characters
        { 
            get { return _field11_InsuredsPolicyGroupOfFECANumber; } 
            set { _field11_InsuredsPolicyGroupOfFECANumber = value; } 
        }

        public string Field11a_InsuredsDateOfBirth                            // MMDDCCYY - 8 characters
        { 
            get { return _field11a_InsuredsDateOfBirth; } 
            set { _field11a_InsuredsDateOfBirth = value; }
        }

        public string Field11a_InsuredsSex                                    // 1 = Male, 2 = Female;  1 character.
        { 
            get { return _field11a_InsuredsSex; }
            set { _field11a_InsuredsSex = value; } 
        }

        public string Field11b_InsuredsEmployerOrSchool                       // 29 characters
        { 
            get { return _field11b_InsuredsEmployerOrSchool; } 
            set { _field11b_InsuredsEmployerOrSchool = value; }
        }

        public string Field11c_InsuredsPlanOrProgramName                      // 29 characters
        {
            get { return _field11c_InsuredsPlanOrProgramName; } 
            set { _field11c_InsuredsPlanOrProgramName = value; } 
        }

        public string Field11d_IsThereOtherHealthBenefitPlan                  // 1 = Yes, 2 = No
        { 
            get { return _field11d_IsThereOtherHealthBenefitPlan; } 
            set { _field11d_IsThereOtherHealthBenefitPlan = value; } 
        }

        public string Field12_PatientsOrAuthorizedSignature                   // Signed field.  Store 1 = Signature On File, 2 = Signature NOT On File.  If SOF, enter date in next field
        { 
            get { return _field12_PatientsOrAuthorizedSignature; }
            set { _field12_PatientsOrAuthorizedSignature = value; } 
        }

        public string Field12_PatientsOrAuthorizedSignatureDate               // MMDDCCYY 
        { 
            get { return _field12_PatientsOrAuthorizedSignatureDate; } 
            set { _field12_PatientsOrAuthorizedSignatureDate = value; }
        }

        public string Field13_InsuredsOrAuthorizedSignature                   // Signed field.  Store 1 = Signature On File, 2 = Signature NOT On File.  If SOF, enter date in next field
        { 
            get { return _field13_InsuredsOrAuthorizedSignature; }
            set { _field13_InsuredsOrAuthorizedSignature = value; } 
        }

        public string Field14_DateOfCurrentIllnessInjuryOrPregnancy           // MMDDCCYY
        { 
            get { return _field14_DateOfCurrentIllnessInjuryOrPregnancy; } 
            set { _field14_DateOfCurrentIllnessInjuryOrPregnancy = value; } 
        }

        public string Field15_DatePatientHadSameOrSimilarIllness              // MMDDCCYY
        { 
            get { return _field15_DatePatientHadSameOrSimilarIllness; } 
            set { _field15_DatePatientHadSameOrSimilarIllness = value; }
        }

        public string Field16_DatePatientUnableToWork_Start                  // MMDDCCYY
        { 
            get { return _field16_DatePatientUnableToWork_Start; }
            set { _field16_DatePatientUnableToWork_Start = value; }
        }

        public string Field16_DatePatientUnableToWork_End                    // MMDDCCYY
        {
            get { return _field16_DatePatientUnableToWork_End; }
            set { _field16_DatePatientUnableToWork_End = value; } 
        }

        public string Field17_ReferringProviderOrOtherSource_LastName         // HCFA 1500 standard allows 28 total characters for these (3) fields
        { 
            get { return _field17_ReferringProviderOrOtherSource_LastName; } 
            set { _field17_ReferringProviderOrOtherSource_LastName = value; }
        }

        public string Field17_ReferringProviderOrOtherSource_FirstName
        {
            get { return _field17_ReferringProviderOrOtherSource_FirstName; } 
            set { _field17_ReferringProviderOrOtherSource_FirstName = value; } 
        }

        public string Field17_ReferringProviderOrOtherSource_MiddleName
        {
            get { return _field17_ReferringProviderOrOtherSource_MiddleName; } 
            set { _field17_ReferringProviderOrOtherSource_MiddleName = value; } 
        }

        public string Field17_ReferringProviderOrOtherSource_Credentials     // ie., 'MD', 'MRCVS'
        { 
            get { return _field17_ReferringProviderOrOtherSource_Credentials; } 
            set { _field17_ReferringProviderOrOtherSource_Credentials = value; }
        }

        public string Field17a_OtherID_Qualifier                             // 2 digit alpha-numeric value
        { 
            get { return _field17a_OtherID_Qualifier; }
            set { _field17a_OtherID_Qualifier = value; } 
        }

        public string Field17a_OtherID_Number                                // 17 characters
        { 
            get { return _field17a_OtherID_Number; } 
            set { _field17a_OtherID_Number = value; } 
        }

        public string Field17b_NationalProviderIdentifier                    // 10 digit numeric
        {
            get { return _field17b_NationalProviderIdentifier; } 
            set { _field17b_NationalProviderIdentifier = value; }
        }

        public string Field18_HospitalizationDateFrom                         // MMDDCCYY
        { 
            get { return _field18_HospitalizationDateFrom; } 
            set { _field18_HospitalizationDateFrom = value; }
        }

        public string Field18_HospitalizationDateTo                           // MMDDCCYY
        {
            get { return _field18_HospitalizationDateTo; } 
            set { _field18_HospitalizationDateTo = value; } 
        }

        public string Field19_ReservedForLocalUse                            // 83 characters
        {
            get { return _field19_ReservedForLocalUse; } 
            set { _field19_ReservedForLocalUse = value; } 
        }

        public string Field20_OutsideLab                                      // 1 = Yes, 2 = No
        { 
            get { return _field20_OutsideLab; }
            set { _field20_OutsideLab = value; }
        }

        public string Field20_OutsideLabCharge                                // 8 digit numeric with implied decimal.  ie '20300' is $203.00.
        {
            get { return _field20_OutsideLabCharge; } 
            set { _field20_OutsideLabCharge = value; } 
        }

        public HCFA1500DiagCodes Field21_Diagnosis_1                          // 3-1-4 part diagnosis code.
        {
            get { return _field21_Diagnosis_1; }
            set { _field21_Diagnosis_1 = value; } 
        }

        public HCFA1500DiagCodes Field21_Diagnosis_2                         // 3-1-4 part diagnosis code.
        {
            get { return _field21_Diagnosis_2; } 
            set { _field21_Diagnosis_2 = value; }
        }

        public HCFA1500DiagCodes Field21_Diagnosis_3                          // 3-1-4 part diagnosis code.
        {
            get { return _field21_Diagnosis_3; } 
            set { _field21_Diagnosis_3 = value; }
        }

        public HCFA1500DiagCodes Field21_Diagnosis_4                         // 3-1-4 part diagnosis code.
        {
            get { return _field21_Diagnosis_4; }
            set { _field21_Diagnosis_4 = value; }
        }

        public string Field22_MedicaidSubmissionCode                          // 11 characters
        {
            get { return _field22_MedicaidSubmissionCode; } 
            set { _field22_MedicaidSubmissionCode = value; }
        }

        public string Field22_OriginalReferenceNumber                        // 18 characters
        { 
            get { return _field22_OriginalReferenceNumber; } 
            set { _field22_OriginalReferenceNumber = value; } 
        }

        public string Field23_PriorAuthorizationNumber                       // 29 characters
        { 
            get { return _field23_PriorAuthorizationNumber; } 
            set { _field23_PriorAuthorizationNumber = value; }
        }

        public HCFA1500ServiceLines Field24SvcLines                          // Service line details
        { 
            get { return _field24SvcLines; }
            set { _field24SvcLines = value; } 
        }

        public string Field25_FederalTaxIDNumber                             // 15 characters
        { 
            get { return _field25_FederalTaxIDNumber; }
            set { _field25_FederalTaxIDNumber = value; } 
        }

        public string Field25_SSN_Or_EIN                                     // 1 = SSN, 2 = EIN
        {
            get { return _field25_SSN_Or_EIN; } 
            set { _field25_SSN_Or_EIN = value; } 
        }

        public string Field26_PatientAccountNumber                            // 14 characters
        { 
            get { return _field26_PatientAccountNumber; }
            set { _field26_PatientAccountNumber = value; } 
        }

        public string Field27_AcceptAssignment                               // 1 = Yes, 2 = No.  Refers to acceptance of terms of payor's program.
        {
            get { return _field27_AcceptAssignment; } 
            set { _field27_AcceptAssignment = value; }
        }

        public string Field28_TotalChargeDollars                             // 7 digits
        {
            get { return _field28_TotalChargeDollars; } 
            set { _field28_TotalChargeDollars = value; } 
        }
        public string Field28_TotalChargeCents             
            // 2 digits
        {
            get { return _field28_TotalChargeCents; }
            set { _field28_TotalChargeCents = value; }
        }
        public string Field29_AmountPaidDollars            
            // 6 digits
        {
            get { return _field29_AmountPaidDollars; }
            set { _field29_AmountPaidDollars = value; } 
        }

        public string Field29_AmountPaidCents                                // 2 digits
        {
            get { return _field29_AmountPaidCents; }
            set { _field29_AmountPaidCents = value; }
        }

        public string Field30_BalanceDueDollars                               // 6 digits
        {
            get { return _field30_BalanceDueDollars; } 
            set { _field30_BalanceDueDollars = value; }
        }

        public string Field30_BalanceDueCents                                // 2 digits
        {
            get { return _field30_BalanceDueCents; }
            set { _field30_BalanceDueCents = value; } 
        }

        public string Field31_PhysicianOrSupplierSignature                   // Signed field.  Store 1 = Signature On File, 2 = Signature NOT On File.  If SOF, enter date in next field
        { 
            get { return _field31_PhysicianOrSupplierSignature; }
            set { _field31_PhysicianOrSupplierSignature = value; }
        }

        public string Field31_PhysicianOrSupplierSignatureDate               // MMDDCCYY 
        {
            get { return _field31_PhysicianOrSupplierSignatureDate; } 
            set { _field31_PhysicianOrSupplierSignatureDate = value; } 
        }

        public string Field32FacilityLocationInfo_Name                      // 26 characters
        {
            get { return _field32_FacilityLocationInfo_Name; }
            set { _field32_FacilityLocationInfo_Name = value; }
        }

        public string Field32FacilityLocationInfo_Street                     // 26 characters
        {
            get { return _field32_FacilityLocationInfo_Street; } 
            set { _field32_FacilityLocationInfo_Street = value; } 
        }

        public string Field32FacilityLocationInfo_City                       // 26 characters for this and next two fields combined
        {
            get { return _field32_FacilityLocationInfo_City; } 
            set { _field32_FacilityLocationInfo_City = value; }
        }

        public string Field32FacilityLocationInfo_State                     //
        {
            get { return _field32_FacilityLocationInfo_State; } 
            set { _field32_FacilityLocationInfo_State = value; } 
        }

        public string Field32FacilityLocationInfo_Zip                        //
        {
            get { return _field32_FacilityLocationInfo_Zip; } 
            set { _field32_FacilityLocationInfo_Zip = value; } 
        }

        public string Field32aFacilityNationalProviderIdentifier            // 10 characters
        { 
            get { return _field32a_FacilityNationalProviderIdentifier; } 
            set { _field32a_FacilityNationalProviderIdentifier = value; }
        }

        public string Field32bFacilityOtherID                               // 14 characters
        {
            get { return _field32b_FacilityOtherID; }
            set { _field32b_FacilityOtherID = value; } 
        }

        public string Field33_BillingProvider_AreaCode                       // 3 characters
        { 
            get { return _field33_BillingProvider_AreaCode; }
            set { _field33_BillingProvider_AreaCode = value; } 
        }

        public string Field33_BillingProvider_PhoneNumber                    // 9 characters
        {
            get { return _field33_BillingProvider_PhoneNumber; } 
            set { _field33_BillingProvider_PhoneNumber = value; } 
        }

        public string Field33_BillingProvider_Name                           // 29 characters
        { 
            get { return _field33_BillingProvider_Name; } 
            set { _field33_BillingProvider_Name = value; }
        }

        public string Field33_BillingProvider_Street                          // 29 characters
        {
            get { return _field33_BillingProvider_Street; } 
            set { _field33_BillingProvider_Street = value; } 
        }

        public string Field33_BillingProvider_City                           // 29 characters for this and next two fields combined
        {
            get { return _field33_BillingProvider_City; } 
            set { _field33_BillingProvider_City = value; }
        }

        public string Field33_BillingProvider_State                           //
        {
            get { return _field33_BillingProvider_State; } 
            set { _field33_BillingProvider_State = value; } 
        }

        public string Field33_BillingProvider_Zip                             //
        { 
            get { return _field33_BillingProvider_Zip; }
            set { _field33_BillingProvider_Zip = value; } 
        }

        public string Field33a_BillingProviderNationalProviderIdentifier     // 10 characters
        { 
            get { return _field33a_BillingProviderNationalProviderIdentifier; }
            set { _field33a_BillingProviderNationalProviderIdentifier = value; } 
        }

        public string Field33b_BillingProviderOtherID                         // 17 characters
        { 
            get { return _field33b_BillingProviderOtherID; } 
            set { _field33b_BillingProviderOtherID = value; }
        }

    }
}
