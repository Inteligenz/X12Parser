namespace X12.Hipaa.Claims.Forms.Dental
{
    using System.Collections.Generic;

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
         */

        //// TODO: The following private strings are useless. However, they have comments that match their respective property
        //// It would be highly beneficial to put the comment with the property, for ease of use
        private string
            _field01_TypeOfTransaction; // 1 = Statement of Actual Services, 2 = Request for Predetermination/Preauthorization, 3 = EPSDT / Title XIX

        private string _field04_OtherDentalOrMedicalCoverage; // 1 = No, 2 = Yes

        private string _field06_DateOfBirth; // MMDDCCYY

        private string _field07_Gender; // 1 = Male, 2 = Female

        private string _field08_SubscriberIdentifier; // SSN or ID#

        private string _field10_RelationshipToPrimarySubscriber_Self; // 1 = Yes, 2 = No  (checkbox)

        private string _field10_RelationshipToPrimarySubscriber_Spouse; // 1 = Yes, 2 = No  (checkbox)

        private string _field10_RelationshipToPrimarySubscriber_Dependent; // 1 = Yes, 2 = No  (checkbox)

        private string _field10_RelationshipToPrimarySubscriber_Other; // 1 = Yes, 2 = No  (checkbox)

        private string _field13_PrimarySubscriberDateOfBirth; // MMDDCCYY

        private string _field14_Gender; // 1 = Male, 2 = Female

        private string _field15_SubscriberIdentifier; // SSN and ID#

        private string _field18_RelationshipToPrimarySubscriber; // 1 = Self, 2 = Spouse, 3 = Dependent Child, 4 = Other

        private string _field19_StudentStatus; // 1 = Full Time Student, 2 = Part Time Student

        private string _field21_PatientDateOfBirth; // MMDDCCYY

        private string _field22_PatientGender; // 1 = Male, 2 = Female

        private string _field23_PatientID_OrAccountNumber; // Dentist assigned

        private List<J515ServiceLines> _field24_31_ServiceLines; // Review J515ServiceLines class for details

        private List<Field34_MissingTeethInfo_Permanent> _field34_MissingTeethInfo_Permanent; // 32 teeth of an adult

        private List<Field34_MissingTeethInfo_Primary> _field34_MissingTeethInfo_Primary; // 20 teeth of a child

        private string _field36_Authorizations_PatientGuardianSignature; // 1 = Signed, 2 = Unsigned

        private string _field36_Authorizations_PatientGuardianSignatureDate; // MMDDCCYY - Date signed

        private string _field37_Authorizations_SubscriberSignature; // 1 = Signed, 2 = Unsigned

        private string _field37_Authorizations_SubscriberSignatureDate; // MMDDCCYY - Date signed

        private string _field38_PlaceOfTreatment; // 1 = Provider's Office, 2 = Hospital, 3 = ECF, 4 = Other

        private string _field40_IsTreatmentForOrthodontics; // 1 = No, 2 = Yes

        private string _field41_DateAppliancePlaced; // MMDDCCYY

        private string _field43_ReplacementOfProsthesis; // 1 = No, 2 = Yes

        private string _field44_DatePriorReplacement; // MMDDCCYY

        private string _field45_TreatmentResultingFrom_OccupationalIllnessOrInjury; // 1 = Yes, 2 = No

        private string _field45_TreatmentResultingFrom_AutoAccident; // 1 = Yes, 2 = No

        private string _field45_TreatmentResultingFrom_OtherAccident; // 1 = Yes, 2 = No

        private string _field46_DateOfAccident; // MMDDCCYY

        private string _field52_ProviderPhone_AreaCode; // 3 digits

        private string _field52_ProviderPhone_Number; // ? digits

        private string _field53_TreatingDentistSignature; // 1 = Signed, 2 = Unsigned

        private string _field53_TreatingDentistSignatureDate; // MMDDCCYY - Date signed

        private string _field58_TreatingProviderSpecialty; // 10 characters

        public string Field01_TypeOfTransaction { get; set; }

        public string Field02_PredeterminationOrPreauthorizationNumber { get; set; }

        public string Field03_PrimaryPayer_Name { get; set; }

        public string Field03_PrimaryPayer_Address { get; set; }

        public string Field03_PrimaryPayer_City { get; set; }

        public string Field03_PrimaryPayer_State { get; set; }

        public string Field03_PrimaryPayer_Zip { get; set; }

        public string Field04_OtherDentalOrMedicalCoverage { get; set; }

        public string Field05_SubscriberName_Last { get; set; }

        public string Field05_SubscriberName_First { get; set; }

        public string Field05_SubscriberName_Middle { get; set; }

        public string Field05_SubscriberName_Suffix { get; set; }

        public string Field06_DateOfBirth { get; set; }

        public string Field07_Gender { get; set; }

        public string Field08_SubscriberIdentifier { get; set; }

        public string Field09_PlanOrGroupNumber { get; set; }

        public string Field10_RelationshipToPrimarySubscriber_Self { get; set; }

        public string Field10_RelationshipToPrimarySubscriber_Spouse { get; set; }

        public string Field10_RelationshipToPrimarySubscriber_Dependent { get; set; }

        public string Field10_RelationshipToPrimarySubscriber_Other { get; set; }

        public string Field11_OtherCarrier_Name { get; set; }

        public string Field11_OtherCarrier_Address { get; set; }

        public string Field11_OtherCarrier_City { get; set; }

        public string Field11_OtherCarrier_State { get; set; }

        public string Field11_OtherCarrier_Zip { get; set; }

        public string Field12_PrimarySubscriberName_Last { get; set; }

        public string Field12_PrimarySubscriberName_First { get; set; }

        public string Field12_PrimarySubscriberName_Middle { get; set; }

        public string Field12_PrimarySubscriberName_Suffix { get; set; }

        public string Field12_PrimarySubscriber_Address { get; set; }

        public string Field12_PrimarySubscriber_City { get; set; }

        public string Field12_PrimarySubscriber_State { get; set; }

        public string Field12_PrimarySubscriber_Zip { get; set; }

        public string Field13_PrimarySubscriberDateOfBirth { get; set; }

        public string Field14_Gender { get; set; }

        public string Field15_SubscriberIdentifier { get; set; }

        public string Field16_PlanOrGroupNumber { get; set; }

        public string Field17_EmployerName { get; set; }

        public string Field18_RelationshipToPrimarySubscriber { get; set; }

        public string Field19_StudentStatus { get; set; }

        public string Field20_PatientName_Last { get; set; }

        public string Field20_PatientName_First { get; set; }

        public string Field20_PatientName_Middle { get; set; }

        public string Field20_PatientName_Suffix { get; set; }

        public string Field20_PatientAddress { get; set; }

        public string Field20_PatientCity { get; set; }

        public string Field20_PatientState { get; set; }

        public string Field20_PatientZip { get; set; }

        public string Field21_PatientDateOfBirth { get; set; }

        public string Field22_PatientGender { get; set; }

        public string Field23_PatientID_OrAccountNumber { get; set; }

        public List<J515ServiceLines> Field24_31_ServiceLines { get; set; }

        public decimal Field32_OtherFees { get; set; }

        public decimal Field33_TotalFees { get; set; }

        public List<Field34_MissingTeethInfo_Permanent> Field34_MissingTeethInfo_Permanent { get; set; }

        public List<Field34_MissingTeethInfo_Primary> Field34_MissingTeethInfo_Primary { get; set; }

        public string Field35_Remarks { get; set; }

        public string Field36_Authorizations_PatientGuardianSignature { get; set; }

        public string Field36_Authorizations_PatientGuardianSignatureDate { get; set; }

        public string Field37_Authorizations_SubscriberSignature { get; set; }

        public string Field37_Authorizations_SubscriberSignatureDate { get; set; }

        public string Field38_PlaceOfTreatment { get; set; }

        public string Field39_NumberOfEnclosures_Radiographs { get; set; }

        public string Field39_NumberOfEnclosures_OralImages { get; set; }

        public string Field39_NumberOfEnclosures_Models { get; set; }

        public string Field40_IsTreatmentForOrthodontics { get; set; }

        public string Field41_DateAppliancePlaced { get; set; }

        public string Field42_MonthsOfTreatmentRemaining { get; set; }

        public string Field43_ReplacementOfProsthesis { get; set; }

        public string Field44_DatePriorReplacement { get; set; }

        public string Field45_TreatmentResultingFrom_OccupationalIllnessOrInjury { get; set; }

        public string Field45_TreatmentResultingFrom_AutoAccident { get; set; }

        public string Field45_TreatmentResultingFrom_OtherAccident { get; set; }

        public string Field46_DateOfAccident { get; set; }

        public string Field47_AutoAccidentState { get; set; }

        public string Field48_BillingDentistOrDentalEntity_Name { get; set; }

        public string Field48_BillingDentistOrDentalEntity_Address { get; set; }

        public string Field48_BillingDentistOrDentalEntity_City { get; set; }

        public string Field48_BillingDentistOrDentalEntity_State { get; set; }

        public string Field48_BillingDentistOrDentalEntity_Zip { get; set; }

        public string Field49_BillingProviderID { get; set; }

        public string Field50_BillingProviderLicenseNumber { get; set; }

        public string Field51_ProviderSSN_OrTaxIDNumber { get; set; }

        public string Field52_ProviderPhone_AreaCode { get; set; }

        public string Field52_ProviderPhone_Number { get; set; }

        public string Field53_TreatingDentistSignature { get; set; }

        public string Field53_TreatingDentistSignatureDate { get; set; }

        public string Field54_PerformingProviderID { get; set; }

        public string Field55_PerformingProviderLicenseNumber { get; set; }

        public string Field56_PerformingProviderAddress { get; set; }

        public string Field56_PerformingProviderCity { get; set; }

        public string Field56_PerformingProviderState { get; set; }

        public string Field56_PerformingProviderZip { get; set; }

        public string Field57_PerformingProviderPhone_AreaCode { get; set; }

        public string Field57_PerformingProviderPhone_Number { get; set; }

        public string Field58_TreatingProviderSpecialty { get; set; }
    }
#endif
}
