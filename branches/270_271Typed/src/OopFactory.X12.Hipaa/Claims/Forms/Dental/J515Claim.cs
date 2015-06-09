using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Dental
{
#if DEBUG
    class J515Claim
    {
        /*
         * 2011/8/16, jhalliday - New Data Model for 837D (Dental) claim.
         * 
         * Team: dstrubhar, jhalliday and epkrause
         * 
         * Purpose:
         * To create a C# object model that will serve as a container for the X12 837D data
         * AS ENTERED from a J515 (ADA Dental Claim Form) dental claim form.
         * 
         * Goal:
         * The team has the overall goal of creating tools that can be used to consume and
         * manipulate X12 messages (AKA files/documents) without the need to have a big project
         * budget.  For that reason, this and the related X12 Parser project tools are all open
         * source and freely usable.

         * Fields in the J515 object model are defined in the order they appear on the J515 form.
         * 
         * Field Descriptions:   (putting this here instead of commenting each field)
         * 
         * 1. 
         */
        
        // First, we will declare the private variables.  Then the properties and their accessors.
                
        private string _field01_TypeOfTransaction;                                      // 1 = Statement of Actual Services, 2 = Request for Predetermination/Preauthorization, 3 = EPSDT / Title XIX
        private string _field02_PredeterminationOrPreauthorizationNumber;               // characters
        private string _field03_PrimaryPayer_Name;                                      // 
        private string _field03_PrimaryPayer_Address;                                   // 
        private string _field03_PrimaryPayer_City;                                      // 
        private string _field03_PrimaryPayer_State;                                     // 
        private string _field03_PrimaryPayer_Zip;                                       // 
        private string _field04_OtherDentalOrMedicalCoverage;                           // 1 = No, 2 = Yes
        private string _field05_SubscriberName_Last;                                    //
        private string _field05_SubscriberName_First;                                   //
        private string _field05_SubscriberName_Middle;                                  //
        private string _field05_SubscriberName_Suffix;                                  //
        private string _field06_DateOfBirth;                                            // MMDDCCYY
        private string _field07_Gender;                                                 // 1 = Male, 2 = Female
        private string _field08_SubscriberIdentifier;                                   // SSN or ID#
        private string _field09_PlanOrGroupNumber;                                      //
        private string _field10_RelationshipToPrimarySubscriber_Self;                   // 1 = Yes, 2 = No  (checkbox)
        private string _field10_RelationshipToPrimarySubscriber_Spouse;                 // 1 = Yes, 2 = No  (checkbox)
        private string _field10_RelationshipToPrimarySubscriber_Dependent;              // 1 = Yes, 2 = No  (checkbox)
        private string _field10_RelationshipToPrimarySubscriber_Other;                  // 1 = Yes, 2 = No  (checkbox)
        private string _field11_OtherCarrier_Name;                                      //
        private string _field11_OtherCarrier_Address;                                   //
        private string _field11_OtherCarrier_City;                                      //
        private string _field11_OtherCarrier_State;                                     //
        private string _field11_OtherCarrier_Zip;                                       //
        private string _field12_PrimarySubscriberName_Last;                             //
        private string _field12_PrimarySubscriberName_First;                            //
        private string _field12_PrimarySubscriberName_Middle;                           //
        private string _field12_PrimarySubscriberName_Suffix;                           //
        private string _field12_PrimarySubscriber_Address;                              //
        private string _field12_PrimarySubscriber_City;                                 //
        private string _field12_PrimarySubscriber_State;                                //
        private string _field12_PrimarySubscriber_Zip;                                  //
        private string _field13_PrimarySubscriberDateOfBirth;                           // MMDDCCYY
        private string _field14_Gender;                                                 // 1 = Male, 2 = Female
        private string _field15_SubscriberIdentifier;                                   // SSN and ID#
        private string _field16_PlanOrGroupNumber;                                      //
        private string _field17_EmployerName;                                           //
        private string _field18_RelationshipToPrimarySubscriber;                        // 1 = Self, 2 = Spouse, 3 = Dependent Child, 4 = Other
        private string _field19_StudentStatus;                                          // 1 = Full Time Student, 2 = Part Time Student
        private string _field20_PatientName_Last;                                       //
        private string _field20_PatientName_First;                                      //
        private string _field20_PatientName_Middle;                                     //
        private string _field20_PatientName_Suffix;                                     //
        private string _field20_PatientAddress;                                         //
        private string _field20_PatientCity;                                            //
        private string _field20_PatientState;                                           //
        private string _field20_PatientZip;                                             //
        private string _field21_PatientDateOfBirth;                                     // MMDDCCYY
        private string _field22_PatientGender;                                          // 1 = Male, 2 = Female
        private string _field23_PatientID_OrAccountNumber;                              // Dentist assigned
        private List<J515ServiceLines> _field24_31_ServiceLines;                        // Review J515ServiceLines class for details
        private decimal _field32_OtherFees;                                              //
        private decimal _field33_TotalFees;                                              //
        private List<Field34_MissingTeethInfo_Permanent> _field34_MissingTeethInfo_Permanent;  // 32 teeth of an adult
        private List<Field34_MissingTeethInfo_Primary> _field34_MissingTeethInfo_Primary;      // 20 teeth of a child
        private string _field35_Remarks;                                                //
        private string _field36_Authorizations_PatientGuardianSignature;                // 1 = Signed, 2 = Unsigned
        private string _field36_Authorizations_PatientGuardianSignatureDate;            // MMDDCCYY - Date signed
        private string _field37_Authorizations_SubscriberSignature;                     // 1 = Signed, 2 = Unsigned
        private string _field37_Authorizations_SubscriberSignatureDate;                 // MMDDCCYY - Date signed
        private string _field38_PlaceOfTreatment;                                       // 1 = Provider's Office, 2 = Hospital, 3 = ECF, 4 = Other
        private string _field39_NumberOfEnclosures_Radiographs;                         //
        private string _field39_NumberOfEnclosures_OralImages;                          //
        private string _field39_NumberOfEnclosures_Models;                              //
        private string _field40_IsTreatmentForOrthodontics;                             // 1 = No, 2 = Yes
        private string _field41_DateAppliancePlaced;                                    // MMDDCCYY
        private string _field42_MonthsOfTreatmentRemaining;                             //
        private string _field43_ReplacementOfProsthesis;                                // 1 = No, 2 = Yes
        private string _field44_DatePriorReplacement;                                   // MMDDCCYY
        private string _field45_TreatmentResultingFrom_OccupationalIllnessOrInjury;     // 1 = Yes, 2 = No
        private string _field45_TreatmentResultingFrom_AutoAccident;                    // 1 = Yes, 2 = No
        private string _field45_TreatmentResultingFrom_OtherAccident;                   // 1 = Yes, 2 = No
        private string _field46_DateOfAccident;                                         // MMDDCCYY
        private string _field47_AutoAccidentState;                                      //
        private string _field48_BillingDentistOrDentalEntity_Name;                      //
        private string _field48_BillingDentistOrDentalEntity_Address;                   //
        private string _field48_BillingDentistOrDentalEntity_City;                      //
        private string _field48_BillingDentistOrDentalEntity_State;                     //
        private string _field48_BillingDentistOrDentalEntity_Zip;                       //
        private string _field49_BillingProviderID;                                      //
        private string _field50_BillingProviderLicenseNumber;                           //
        private string _field51_ProviderSSN_OrTaxIDNumber;                              //
        private string _field52_ProviderPhone_AreaCode;                                 // 3 digits
        private string _field52_ProviderPhone_Number;                                   // ? digits
        private string _field53_TreatingDentistSignature;                               // 1 = Signed, 2 = Unsigned
        private string _field53_TreatingDentistSignatureDate;                           // MMDDCCYY - Date signed
        private string _field54_PerformingProviderID;                                   //
        private string _field55_PerformingProviderLicenseNumber;                        //
        private string _field56_PerformingProviderAddress;                              //
        private string _field56_PerformingProviderCity;                                 //
        private string _field56_PerformingProviderState;                                //
        private string _field56_PerformingProviderZip;                                  //
        private string _field57_PerformingProviderPhone_AreaCode;                       //
        private string _field57_PerformingProviderPhone_Number;                         //
        private string _field58_TreatingProviderSpecialty;                              // 10 characters

    
        public string Field01_TypeOfTransaction 
        { 
            get { return _field01_TypeOfTransaction; } 
            set { _field01_TypeOfTransaction = value; } 
        }

        public string Field02_PredeterminationOrPreauthorizationNumber 
        { 
            get { return _field02_PredeterminationOrPreauthorizationNumber; } 
            set { _field02_PredeterminationOrPreauthorizationNumber = value; } 
        }

        public string Field03_PrimaryPayer_Name 
        {
            get { return _field03_PrimaryPayer_Name; }
            set { _field03_PrimaryPayer_Name = value; }
        }

        public string Field03_PrimaryPayer_Address 
        { 
            get { return _field03_PrimaryPayer_Address; }
            set { _field03_PrimaryPayer_Address = value; } 
        }

        public string Field03_PrimaryPayer_City 
        { 
            get { return _field03_PrimaryPayer_City; }
            set { _field03_PrimaryPayer_City = value; }
        }

        public string Field03_PrimaryPayer_State 
        { 
            get { return _field03_PrimaryPayer_State; }
            set { _field03_PrimaryPayer_State = value; } 
        }

        public string Field03_PrimaryPayer_Zip 
        { 
            get { return _field03_PrimaryPayer_Zip; } 
            set { _field03_PrimaryPayer_Zip = value; } 
        }

        public string Field04_OtherDentalOrMedicalCoverage 
        { 
            get { return _field04_OtherDentalOrMedicalCoverage; }
            set { _field04_OtherDentalOrMedicalCoverage = value; } 
        }

        public string Field05_SubscriberName_Last 
        {
            get { return _field05_SubscriberName_Last; } 
            set { _field05_SubscriberName_Last = value; } 
        }

        public string Field05_SubscriberName_First 
        {
            get { return _field05_SubscriberName_First; }
            set { _field05_SubscriberName_First = value; }
        }

        public string Field05_SubscriberName_Middle
        { 
            get { return _field05_SubscriberName_Middle; } 
            set { _field05_SubscriberName_Middle = value; } 
        }

        public string Field05_SubscriberName_Suffix 
        { 
            get { return _field05_SubscriberName_Suffix; }
            set { _field05_SubscriberName_Suffix = value; } 
        }

        public string Field06_DateOfBirth 
        { 
            get { return _field06_DateOfBirth; } 
            set { _field06_DateOfBirth = value; }
        }

        public string Field07_Gender 
        { 
            get { return _field07_Gender; }
            set { _field07_Gender = value; } 
        }

        public string Field08_SubscriberIdentifier 
        { 
            get { return _field08_SubscriberIdentifier; } 
            set { _field08_SubscriberIdentifier = value; } 
        }

        public string Field09_PlanOrGroupNumber 
        {
            get { return _field09_PlanOrGroupNumber; } 
            set { _field09_PlanOrGroupNumber = value; }
        }

        public string Field10_RelationshipToPrimarySubscriber_Self 
        {
            get { return _field10_RelationshipToPrimarySubscriber_Self; }
            set { _field10_RelationshipToPrimarySubscriber_Self = value; }
        }

        public string Field10_RelationshipToPrimarySubscriber_Spouse 
        { 
            get { return _field10_RelationshipToPrimarySubscriber_Spouse; } 
            set { _field10_RelationshipToPrimarySubscriber_Spouse = value; } 
        }

        public string Field10_RelationshipToPrimarySubscriber_Dependent 
        { 
            get { return _field10_RelationshipToPrimarySubscriber_Dependent; }
            set { _field10_RelationshipToPrimarySubscriber_Dependent = value; }
        }

        public string Field10_RelationshipToPrimarySubscriber_Other 
        { 
            get { return _field10_RelationshipToPrimarySubscriber_Other; } 
            set { _field10_RelationshipToPrimarySubscriber_Other = value; } 
        }

        public string Field11_OtherCarrier_Name 
        { 
            get { return _field11_OtherCarrier_Name; } 
            set { _field11_OtherCarrier_Name = value; }
        }

        public string Field11_OtherCarrier_Address 
        {
            get { return _field11_OtherCarrier_Address; } 
            set { _field11_OtherCarrier_Address = value; }
        }

        public string Field11_OtherCarrier_City 
        { 
            get { return _field11_OtherCarrier_City; } 
            set { _field11_OtherCarrier_City = value; } 
        }

        public string Field11_OtherCarrier_State 
        { 
            get { return _field11_OtherCarrier_State; } 
            set { _field11_OtherCarrier_State = value; } 
        }

        public string Field11_OtherCarrier_Zip 
        { 
            get { return _field11_OtherCarrier_Zip; } 
            set { _field11_OtherCarrier_Zip = value; } 
        }

        public string Field12_PrimarySubscriberName_Last 
        { 
            get { return _field12_PrimarySubscriberName_Last; } 
            set { _field12_PrimarySubscriberName_Last = value; }
        }

        public string Field12_PrimarySubscriberName_First { 
            get { return _field12_PrimarySubscriberName_First; } 
            set { _field12_PrimarySubscriberName_First = value; } 
        }

        public string Field12_PrimarySubscriberName_Middle
        {
            get { return _field12_PrimarySubscriberName_Middle; }
            set { _field12_PrimarySubscriberName_Middle = value; } 
        }

        public string Field12_PrimarySubscriberName_Suffix 
        { 
            get { return _field12_PrimarySubscriberName_Suffix; }
            set { _field12_PrimarySubscriberName_Suffix = value; }
        }

        public string Field12_PrimarySubscriber_Address 
        {
            get { return _field12_PrimarySubscriber_Address; } 
            set { _field12_PrimarySubscriber_Address = value; }
        }

        public string Field12_PrimarySubscriber_City
        { 
            get { return _field12_PrimarySubscriber_City; } 
            set { _field12_PrimarySubscriber_City = value; } 
        }

        public string Field12_PrimarySubscriber_State 
        { 
            get { return _field12_PrimarySubscriber_State; } 
            set { _field12_PrimarySubscriber_State = value; }
        }

        public string Field12_PrimarySubscriber_Zip 
        { 
            get { return _field12_PrimarySubscriber_Zip; } 
            set { _field12_PrimarySubscriber_Zip = value; }
        }

        public string Field13_PrimarySubscriberDateOfBirth 
        { 
            get { return _field13_PrimarySubscriberDateOfBirth; } 
            set { _field13_PrimarySubscriberDateOfBirth = value; } 
        }

        public string Field14_Gender 
        { 
            get { return _field14_Gender; }
            set { _field14_Gender = value; } 
        }

        public string Field15_SubscriberIdentifier
        { 
            get { return _field15_SubscriberIdentifier; } 
            set { _field15_SubscriberIdentifier = value; }
        }

        public string Field16_PlanOrGroupNumber
        { 
            get { return _field16_PlanOrGroupNumber; } 
            set { _field16_PlanOrGroupNumber = value; } 
        }

        public string Field17_EmployerName 
        { 
            get { return _field17_EmployerName; }
            set { _field17_EmployerName = value; } 
        }

        public string Field18_RelationshipToPrimarySubscriber
        {
            get { return _field18_RelationshipToPrimarySubscriber; } 
            set { _field18_RelationshipToPrimarySubscriber = value; }
        }

        public string Field19_StudentStatus 
        { 
            get { return _field19_StudentStatus; } 
            set { _field19_StudentStatus = value; } 
        }

        public string Field20_PatientName_Last 
        { 
            get { return _field20_PatientName_Last; } 
            set { _field20_PatientName_Last = value; } 
        }

        public string Field20_PatientName_First
        { 
            get { return _field20_PatientName_First; } 
            set { _field20_PatientName_First = value; } 
        }

        public string Field20_PatientName_Middle 
        { 
            get { return _field20_PatientName_Middle; } 
            set { _field20_PatientName_Middle = value; } 
        }

        public string Field20_PatientName_Suffix 
        { 
            get { return _field20_PatientName_Suffix; } 
            set { _field20_PatientName_Suffix = value; }
        }

        public string Field20_PatientAddress 
        { 
            get { return _field20_PatientAddress; } 
            set { _field20_PatientAddress = value; }
        }

        public string Field20_PatientCity 
        { 
            get { return _field20_PatientCity; } 
            set { _field20_PatientCity = value; }
        }

        public string Field20_PatientState 
        { 
            get { return _field20_PatientState; } 
            set { _field20_PatientState = value; } 
        }

        public string Field20_PatientZip 
        { 
            get { return _field20_PatientZip; } 
            set { _field20_PatientZip = value; } 
        }

        public string Field21_PatientDateOfBirth 
        { 
            get { return _field21_PatientDateOfBirth; } 
            set { _field21_PatientDateOfBirth = value; }
        }

        public string Field22_PatientGender
        { 
            get { return _field22_PatientGender; } 
            set { _field22_PatientGender = value; } 
        }

        public string Field23_PatientID_OrAccountNumber 
        { 
            get { return _field23_PatientID_OrAccountNumber; } 
            set { _field23_PatientID_OrAccountNumber = value; } 
        }

        public List<J515ServiceLines> Field24_31_ServiceLines 
        { 
            get { return _field24_31_ServiceLines; } 
            set { _field24_31_ServiceLines = value; }
        }

        public decimal Field32_OtherFees 
        { 
            get { return _field32_OtherFees; } 
            set { _field32_OtherFees = value; } 
        }

        public decimal Field33_TotalFees 
        { 
            get { return _field33_TotalFees; } 
            set { _field33_TotalFees = value; } 
        }

        public List<Field34_MissingTeethInfo_Permanent> Field34_MissingTeethInfo_Permanent
        {
            get { return _field34_MissingTeethInfo_Permanent; }
            set { _field34_MissingTeethInfo_Permanent = value; } 
        }

        public List<Field34_MissingTeethInfo_Primary> Field34_MissingTeethInfo_Primary
        {
            get { return _field34_MissingTeethInfo_Primary; }
            set { _field34_MissingTeethInfo_Primary = value; }
        }

        public string Field35_Remarks 
        { 
            get { return _field35_Remarks; } 
            set { _field35_Remarks = value; } 
        }

        public string Field36_Authorizations_PatientGuardianSignature 
        { 
            get { return _field36_Authorizations_PatientGuardianSignature; } 
            set { _field36_Authorizations_PatientGuardianSignature = value; } 
        }

        public string Field36_Authorizations_PatientGuardianSignatureDate 
        {
            get { return _field36_Authorizations_PatientGuardianSignatureDate; } 
            set { _field36_Authorizations_PatientGuardianSignatureDate = value; }
        }

        public string Field37_Authorizations_SubscriberSignature 
        { 
            get { return _field37_Authorizations_SubscriberSignature; }
            set { _field37_Authorizations_SubscriberSignature = value; } 
        }

        public string Field37_Authorizations_SubscriberSignatureDate 
        {
            get { return _field37_Authorizations_SubscriberSignatureDate; }
            set { _field37_Authorizations_SubscriberSignatureDate = value; }
        }

        public string Field38_PlaceOfTreatment 
        { 
            get { return _field38_PlaceOfTreatment; } 
            set { _field38_PlaceOfTreatment = value; }
        }

        public string Field39_NumberOfEnclosures_Radiographs 
        { 
            get { return _field39_NumberOfEnclosures_Radiographs; } 
            set { _field39_NumberOfEnclosures_Radiographs = value; } 
        }

        public string Field39_NumberOfEnclosures_OralImages 
        { 
            get { return _field39_NumberOfEnclosures_OralImages; } 
            set { _field39_NumberOfEnclosures_OralImages = value; } 
        }

        public string Field39_NumberOfEnclosures_Models 
        {
            get { return _field39_NumberOfEnclosures_Models; } 
            set { _field39_NumberOfEnclosures_Models = value; }
        }

        public string Field40_IsTreatmentForOrthodontics 
        { 
            get { return _field40_IsTreatmentForOrthodontics; } 
            set { _field40_IsTreatmentForOrthodontics = value; } 
        }

        public string Field41_DateAppliancePlaced 
        { 
            get { return _field41_DateAppliancePlaced; } 
            set { _field41_DateAppliancePlaced = value; } 
        }

        public string Field42_MonthsOfTreatmentRemaining 
        { 
            get { return _field42_MonthsOfTreatmentRemaining; } 
            set { _field42_MonthsOfTreatmentRemaining = value; } 
        
        }
        public string Field43_ReplacementOfProsthesis 
        {
            get { return _field43_ReplacementOfProsthesis; } 
            set { _field43_ReplacementOfProsthesis = value; } 
        }

        public string Field44_DatePriorReplacement 
        { 
            get { return _field44_DatePriorReplacement; } 
            set { _field44_DatePriorReplacement = value; }
        }

        public string Field45_TreatmentResultingFrom_OccupationalIllnessOrInjury 
        { 
            get { return _field45_TreatmentResultingFrom_OccupationalIllnessOrInjury; } 
            set { _field45_TreatmentResultingFrom_OccupationalIllnessOrInjury = value; }
        }

        public string Field45_TreatmentResultingFrom_AutoAccident 
        { 
            get { return _field45_TreatmentResultingFrom_AutoAccident; } 
            set { _field45_TreatmentResultingFrom_AutoAccident = value; }
        }

        public string Field45_TreatmentResultingFrom_OtherAccident 
        { 
            get { return _field45_TreatmentResultingFrom_OtherAccident; }
            set { _field45_TreatmentResultingFrom_OtherAccident = value; }
        }

        public string Field46_DateOfAccident 
        { 
            get { return _field46_DateOfAccident; } 
            set { _field46_DateOfAccident = value; } 
        }

        public string Field47_AutoAccidentState 
        { 
            get { return _field47_AutoAccidentState; } 
            set { _field47_AutoAccidentState = value; } 
        }

        public string Field48_BillingDentistOrDentalEntity_Name 
        { 
            get { return _field48_BillingDentistOrDentalEntity_Name; } 
            set { _field48_BillingDentistOrDentalEntity_Name = value; } 
        }

        public string Field48_BillingDentistOrDentalEntity_Address
        { 
            get { return _field48_BillingDentistOrDentalEntity_Address; } 
            set { _field48_BillingDentistOrDentalEntity_Address = value; } 
        }

        public string Field48_BillingDentistOrDentalEntity_City
        {
            get { return _field48_BillingDentistOrDentalEntity_City; } 
            set { _field48_BillingDentistOrDentalEntity_City = value; } 
        }

        public string Field48_BillingDentistOrDentalEntity_State 
        { 
            get { return _field48_BillingDentistOrDentalEntity_State; } 
            set { _field48_BillingDentistOrDentalEntity_State = value; } 
        }

        public string Field48_BillingDentistOrDentalEntity_Zip 
        { 
            get { return _field48_BillingDentistOrDentalEntity_Zip; }
            set { _field48_BillingDentistOrDentalEntity_Zip = value; } 
        }

        public string Field49_BillingProviderID 
        { 
            get { return _field49_BillingProviderID ; } 
            set { _field49_BillingProviderID = value; } 
        }

        public string Field50_BillingProviderLicenseNumber
        {
            get { return _field50_BillingProviderLicenseNumber; } 
            set { _field50_BillingProviderLicenseNumber = value; }
        }

        public string Field51_ProviderSSN_OrTaxIDNumber 
        { 
            get { return _field51_ProviderSSN_OrTaxIDNumber; }
            set { _field51_ProviderSSN_OrTaxIDNumber = value; }
        }

        public string Field52_ProviderPhone_AreaCode 
        { 
            get { return _field52_ProviderPhone_AreaCode; } 
            set { _field52_ProviderPhone_AreaCode = value; } 
        }

        public string Field52_ProviderPhone_Number 
        { 
            get { return _field52_ProviderPhone_Number; } 
            set { _field52_ProviderPhone_Number = value; } 
        }

        public string Field53_TreatingDentistSignature
        { 
            get { return _field53_TreatingDentistSignature; }
            set { _field53_TreatingDentistSignature = value; }
        }

        public string Field53_TreatingDentistSignatureDate 
        { 
            get { return _field53_TreatingDentistSignatureDate; } 
            set { _field53_TreatingDentistSignatureDate = value; }
        }

        public string Field54_PerformingProviderID
        { 
            get { return _field54_PerformingProviderID; } 
            set { _field54_PerformingProviderID = value; }
        }

        public string Field55_PerformingProviderLicenseNumber
        {
            get { return _field55_PerformingProviderLicenseNumber; }
            set { _field55_PerformingProviderLicenseNumber = value; }
        }

        public string Field56_PerformingProviderAddress
        { 
            get { return _field56_PerformingProviderAddress; } 
            set { _field56_PerformingProviderAddress = value; }
        }

        public string Field56_PerformingProviderCity 
        { 
            get { return _field56_PerformingProviderCity; } 
            set { _field56_PerformingProviderCity = value; } 
        }

        public string Field56_PerformingProviderState 
        { 
            get { return _field56_PerformingProviderState; } 
            set { _field56_PerformingProviderState = value; } 
        }
        
        public string Field56_PerformingProviderZip 
        {
            get { return _field56_PerformingProviderZip; } 
            set { _field56_PerformingProviderZip = value; } 
        }

        public string Field57_PerformingProviderPhone_AreaCode 
        { 
            get { return _field57_PerformingProviderPhone_AreaCode; } 
            set { _field57_PerformingProviderPhone_AreaCode = value; }
        }
        
        public string Field57_PerformingProviderPhone_Number
        { 
            get { return _field57_PerformingProviderPhone_Number; } 
            set { _field57_PerformingProviderPhone_Number = value; } 
        }
        
        public string Field58_TreatingProviderSpecialty 
        { 
            get { return _field58_TreatingProviderSpecialty; } 
            set { _field58_TreatingProviderSpecialty = value; } 
        }
}
#endif
}
