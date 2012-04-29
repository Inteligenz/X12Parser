using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims.Forms.Professional
{
    public class HCFA1500Claim
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
        public HCFA1500Claim()
        {
            if (Field03_PatientsDateOfBirth == null) Field03_PatientsDateOfBirth = new FormDate();
            if (Field09b_OtherInsuredsDateOfBirth == null) Field09b_OtherInsuredsDateOfBirth = new FormDate();
            if (Field11a_InsuredsDateOfBirth == null) Field11a_InsuredsDateOfBirth = new FormDate();
            if (Field12_PatientsOrAuthorizedSignatureDate == null) Field12_PatientsOrAuthorizedSignatureDate = new FormDate();
            if (Field14_DateOfCurrentIllnessInjuryOrPregnancy == null) Field14_DateOfCurrentIllnessInjuryOrPregnancy = new FormDate();
            if (Field15_DatePatientHadSameOrSimilarIllness == null) Field15_DatePatientHadSameOrSimilarIllness = new FormDate();
            if (Field16_DatePatientUnableToWork_Start == null) Field16_DatePatientUnableToWork_Start = new FormDate();
            if (Field16_DatePatientUnableToWork_End == null) Field16_DatePatientUnableToWork_End = new FormDate();
            if (Field18_HospitalizationDateFrom == null) Field18_HospitalizationDateFrom = new FormDate();
            if (Field18_HospitalizationDateTo == null) Field18_HospitalizationDateTo = new FormDate();
            if (Field24_ServiceLines == null) Field24_ServiceLines = new List<HCFA1500ServiceLine>();
        }

        // Fields in the HCFA1500 object model are defined in the order they appear on the HCFA1500 form.

        public bool Field01_TypeOfCoverageIsMedicare { get; set; }
        public bool Field01_TypeOfCoverageIsMedicaid { get; set; }
        public bool Field01_TypeOfCoverageIsTricareChampus { get; set; }
        public bool Field01_TypeOfCoverageIsChampVa { get; set; }
        public bool Field01_TypeOfCoverageIsGroupHealthPlan { get; set; }
        public bool Field01_TypeOfCoverageIsFECABlkLung { get; set; }
        public bool Field01_TypeOfCoverageIsOther { get; set; }
        public string Field01a_InsuredsIDNumber  { get; set; }
        public string Field02_PatientsName   { get; set; }                              // HCFA 1500 standard allows 29 total characters
        public FormDate Field03_PatientsDateOfBirth   { get; set; }                           // MMDDCCYY - 8 characters
        public bool Field03_PatientsSexMale  { get; set; }
        public bool Field03_PatientsSexFemale { get; set; }  
        public string Field04_InsuredsName  { get; set; }                              // HCFA 1500 standard allows 29 total characters for these (3) fields
        public string Field05_PatientsAddress_Street  { get; set; }                         // 28 characters
        public string Field05_PatientsAddress_City  { get; set; }                           // 24 characters
        public string Field05_PatientsAddress_State { get; set; }                           // 3 characters
        public string Field05_PatientsAddress_Zip { get; set; }                             // 12 characters
        public string Field05_PatientsTelephone { get; set; }                             // 10 digits
        public bool Field06_PatientRelationshipToInsuredIsSelf { get; set; }
        public bool Field06_PatientRelationshipToInsuredIsSpouseOf { get; set; }
        public bool Field06_PatientRelationshipToInsuredIsChildOf { get; set; }
        public bool Field06_PatientRelationshipToInsuredIsOther { get; set; }
        public string Field07_InsuredsAddress_Street { get; set; }                         // 29 characters (yes, '29' not 28)
        public string Field07_InsuredsAddress_City { get; set; }                           // 23 characters (yes, '23' not 24)
        public string Field07_InsuredsAddress_State { get; set; }                           // 4 characters (yes, '4' not 3)
        public string Field07_InsuredsAddress_Zip { get; set; }                            // 12 characters
        public string Field07_InsuredsAreaCode { get; set; }                                // 3 digits
        public string Field07_InsuredsPhoneNumber { get; set; }                             // 10 digits
        public bool Field08_PatientStatusIsSingle { get; set; }
        public bool Field08_PatientStatusIsMarried { get; set; }
        public bool Field08_PatientStatusIsOther { get; set; }
        public bool Field08_PatientStatusIsEmployed { get; set; }
        public bool Field08_PatientStatusIsFullTimeStudent { get; set; }
        public bool Field08_PatientStatusIsPartTimeStudent { get; set; }
        public string Field09_OtherInsuredsName { get; set; }                           // HCFA 1500 standard allows 28 total characters
        public string Field09a_OtherInsuredsPolicyOrGroup { get; set; }                     // 28 characters
        public FormDate Field09b_OtherInsuredsDateOfBirth { get; set; }                       // MMDDCCYY - 8 characters, goes to DMG02 (page 151) from X12 spec.
        public bool Field09b_OtherInsuredIsMale { get; set; }                               // 1 = Male, 2 = Female;  1 character.
        public bool Field09b_OtherInsuredIsFemale { get; set; }
        public string Field09c_OtherInsuredsEmployerNameOrSchoolName { get; set; }          // 28 characters
        public string Field09d_OtherInsuredsInsurancePlanNameOrProgramName  { get; set; }   // 28 characters
        public bool Field10a_PatientConditionRelatedToEmployment { get; set; }           // 1 = Yes, 2 = No
        public bool Field10b_PatientConditionRelatedToAutoAccident { get; set; }
        public string Field10b_PatientConditionRelToAutoAccidentState { get; set; }                 // 2 characters// 1 = Yes, 2 = No
        public bool Field10c_PatientConditionRelatedToOtherAccident { get; set; }         // 1 = Yes, 2 = No
        public string Field10d_ReservedForLocalUse { get; set; }                            // 19 characters
        public string Field11_InsuredsPolicyGroupOfFECANumber { get; set; }                 // 29 characters
        public FormDate Field11a_InsuredsDateOfBirth { get; set; }                            // MMDDCCYY - 8 characters
        public bool Field11a_InsuredsSexIsMale { get; set; }                                    // 1 = Male, 2 = Female;  1 character.
        public bool Field11a_InsuredsSexIsFemale { get; set; }  
        public string Field11b_InsuredsEmployerOrSchool { get; set; }                       // 29 characters
        public string Field11c_InsuredsPlanOrProgramName { get; set; }                      // 29 characters
        public bool Field11d_IsThereOtherHealthBenefitPlan { get; set; }                  // 1 = Yes, 2 = No
        public string Field12_PatientsOrAuthorizedSignature { get; set; }                   // Signed field.  Store 1 = Signature On File, 2 = Signature NOT On File.  If SOF, enter date in next field
        public FormDate Field12_PatientsOrAuthorizedSignatureDate { get; set; }               // MMDDCCYY 
        public string Field13_InsuredsOrAuthorizedSignature { get; set; }                   // Signed field.  Store 1 = Signature On File, 2 = Signature NOT On File.  If SOF, enter date in next field
        public FormDate Field14_DateOfCurrentIllnessInjuryOrPregnancy { get; set; }           // MMDDCCYY
        public FormDate Field15_DatePatientHadSameOrSimilarIllness { get; set; }              // MMDDCCYY
        public FormDate Field16_DatePatientUnableToWork_Start { get; set; }                  // MMDDCCYY
        public FormDate Field16_DatePatientUnableToWork_End { get; set; }                    // MMDDCCYY
        public string Field17_ReferringProviderOrOtherSource_Name { get; set; }         // HCFA 1500 standard allows 28 total characters for this field
        public string Field17a_OtherID_Qualifier { get; set; }                             // 2 digit alpha-numeric value
        public string Field17a_OtherID_Number { get; set; }                                // 17 characters
        public string Field17b_NationalProviderIdentifier { get; set; }                    // 10 digit numeric
        public FormDate Field18_HospitalizationDateFrom { get; set; }                         // MMDDCCYY
        public FormDate Field18_HospitalizationDateTo { get; set; }                           // MMDDCCYY
        public string Field19_ReservedForLocalUse { get; set; }                            // 83 characters
        public bool Field20_OutsideLab { get; set; }                                      // 1 = Yes, 2 = No
        public decimal? Field20_OutsideLabCharges { get; set; }                                // 8 digit numeric with implied decimal.  ie '20300' is $203.00.                        // 3-1-4 part diagnosis code.
        public string Field21_Diagnosis1 { get; set; }
        public string Field21_Diagnosis2 { get; set; }
        public string Field21_Diagnosis3 { get; set; }
        public string Field21_Diagnosis4 { get; set; }
        public string Field22_MedicaidSubmissionCode { get; set; }                          // 11 characters
        public string Field22_OriginalReferenceNumber { get; set; }                        // 18 characters
        public string Field23_PriorAuthorizationNumber { get; set; }                       // 29 characters                         // Service line details
        [XmlElement(ElementName="Field24_ServiceLine")]
        public List<HCFA1500ServiceLine> Field24_ServiceLines { get; set; }
        public string Field25_FederalTaxIDNumber { get; set; }                             // 15 characters
        public bool Field25_IsEIN { get; set; }                                     // 1 = SSN, 2 = EIN
        public bool Field25_IsSSN { get; set; }
        public string Field26_PatientAccountNumber { get; set; }                            // 14 characters
        public bool? Field27_AcceptAssignment { get; set; }                               // 1 = Yes, 2 = No.  Refers to acceptance of terms of payor's program.
        public decimal Field28_TotalCharge { get; set; }                                  // 7 digits           
        public decimal? Field29_AmountPaid { get; set; }  // 6 digits                               // 2 digits
        public decimal? Field30_BalanceDue { get; set; }                                       // 6 digits                               // 2 digits
        public bool? Field31_PhysicianOrSupplierSignatureIsOnFile { get; set; }                      // Signed field.  Store true = Signature On File, false = Signature NOT On File.  If SOF, enter date in next field
        public string Field32_ServiceFacilityLocation_Name { get; set; }                      // 26 characters
        public string Field32_ServiceFacilityLocation_Street { get; set; }                     // 26 characters
        public string Field32_ServiceFacilityLocation_City { get; set; }                       // 26 characters for this and next two fields combined
        public string Field32_ServiceFacilityLocation_State { get; set; }                     //
        public string Field32_ServiceFacilityLocation_Zip { get; set; }                        //
        public string Field32a_ServiceFacilityLocation_Npi { get; set; }            // 10 characters
        public string Field32b_ServiceFacilityLocation_OtherID { get; set; }                               // 14 characters
        public string Field33_BillingProvider_TelephoneNumber { get; set; }                    // 9 characters
        public string Field33_BillingProvider_Name { get; set; }                          // 29 characters
        public string Field33_BillingProvider_Street { get; set; }                          // 29 characters
        public string Field33_BillingProvider_City { get; set; }                           // 29 characters for this and next two fields combined
        public string Field33_BillingProvider_State { get; set; }                           //
        public string Field33_BillingProvider_Zip { get; set; }                             //
        public string Field33a_BillingProvider_Npi { get; set; }     // 10 characters
        public string Field33b_BillingProvider_OtherID { get; set; }                         // 17 characters
        #region Serialization Methods

        public string Serialize()
        {
            var writer = new StringWriter();
            new XmlSerializer(typeof(HCFA1500Claim)).Serialize(writer, this);
            return writer.ToString();
        }

        public static HCFA1500Claim Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(HCFA1500Claim));
            return (HCFA1500Claim)serializer.Deserialize(new StringReader(xml));
        }
        #endregion
    }
}
