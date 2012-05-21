using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Dental
{
    public class J400Claim
    {
        public J400Claim()
        {
            if (Field03_BenefitPlanInformation == null) Field03_BenefitPlanInformation = new J400Block();
            if (Field11_OtherSubscriberBenefitPlanInformation == null) Field11_OtherSubscriberBenefitPlanInformation = new J400Block();
            if (Field12_SubscriberInformation == null) Field12_SubscriberInformation = new J400Block();
            if (Field20_PatientInformation == null) Field20_PatientInformation = new J400Block();
            if (ServiceLines == null) ServiceLines = new List<J400ServiceLine>();
            if (Field34_MissingTeethPositions == null) Field34_MissingTeethPositions = new List<string>();
            if (Field48_BillingDentistInformation == null) Field48_BillingDentistInformation = new J400Block();
        }

        // HEADER INFORMATION
        public bool Field01_TypeOfTransaction_StatementOfActualServices { get; set; }
        public bool Field01_TypeOfTransaction_Preauthorization { get; set; }
        public bool Field01_TypeOfTransaction_EpsdtTitleXIX { get; set; }
        public string Field02_PreauthorizationNumber { get; set; }

        // INSURANCE COMPANY/DENTAL BENEFIT PLAN INFORMATION
        public J400Block Field03_BenefitPlanInformation { get; set; }

        // OTHER COVERAGE
        public bool Field04_OtherDentalOrMedicalCoverage { get; set; }
        public string Field05_OtherSubscriberName { get; set; }
        public DateTime? Field06_OtherSubscriberDateOfBirth { get; set; }
        public bool Field07_OtherSubscriberGender_Male { get; set; }
        public bool Field07_OtherSubscriberGender_Female { get; set; }
        public string Field08_OtherSubscriberId { get; set; }
        public string Field09_OtherSubscriberGroupNumber { get; set; }
        public bool Field10_PatientRelationshipToOtherSubscriber_Self { get; set; }
        public bool Field10_PatientRelationshipToOtherSubscriber_Spouse { get; set; }
        public bool Field10_PatientRelationshipToOtherSubscriber_Dependent { get; set; }
        public bool Field10_PatientRelationshipToOtherSubscriber_Other { get; set; }
        public J400Block Field11_OtherSubscriberBenefitPlanInformation { get; set; }

        // POLICYHOLDER/SUBSCRIBER INFORMATION
        public J400Block Field12_SubscriberInformation { get; set; }
        public DateTime? Field13_SubscriberDateOfBirth { get; set; }
        public bool Field14_SubscriberGender_Male { get; set; }
        public bool Field14_SubscriberGender_Female { get; set; }
        public string Field15_SubscriberId { get; set; }
        public string Field16_SubscriberGroupNumber { get; set; }
        public string Field17_SubscriberEmployerName { get; set; }

        // PATIENT INFORMATION
        public bool Field18_PatientRelationshipToSubscriber_Self { get; set; }
        public bool Field18_PatientRelationshipToSubscriber_Spouse { get; set; }
        public bool Field18_PatientRelationshipToSubscriber_Dependent { get; set; }
        public bool Field18_PatientRelationshipToSubscriber_Other { get; set; }
        public bool Field19_PatientStudentStatus_FTS { get; set; }
        public bool Field19_PatientStudentStatus_PTS { get; set; }
        public J400Block Field20_PatientInformation { get; set; }
        public DateTime? Field21_PatientDateOfBirth { get; set; }
        public bool Field22_PatientGender_Male { get; set; }
        public bool Field22_PatientGender_Female { get; set; }
        public string Field23_PatientAccountNumber { get; set; }

        // RECORD OF SERVICES PROVIDED
        public List<J400ServiceLine> ServiceLines { get; set; }

        public decimal? Field32_OtherFees { get; set; }
        public decimal? Field33_TotalFee { get; set; }

        public List<string> Field34_MissingTeethPositions { get; set; }

        public string Field35_Remarks { get; set; }

        // AUTHORIZATIONS
        public string Field36_PatientSignature { get; set; }
        public DateTime? Field36_PatientSignatureDate { get; set; }

        public string Feild37_SubscriberSignature { get; set; }
        public DateTime? Field37_SubscriberSignatureDate { get; set; }

        // ANCILLARY CLAIM/TREATMENT INFORMATION
        public bool Field38_PlaceOfTreatment_ProvidersOffice { get; set; }
        public bool Field38_PlaceOfTreatment_Hospital { get; set; }
        public bool Field38_PlaceOfTreatment_Ecf { get; set; }
        public bool Field38_PlaceOfTreatment_Other { get; set; }

        public string Field39_NumberOfEnclosures_Radiographs { get; set; }
        public string Field39_NumberOfEnclosures_OralImages { get; set; }
        public string Field39_NumberOfEnclosures_Models { get; set; }

        public bool Field40_IsTreatmentForOrthodontics { get; set; }

        public DateTime? Field41_DateAppliancePlaced { get; set; }
        public string Field42_MonthsOfTreatmentRemaining { get; set; }
        public bool Field43_ReplacementOfProsthesis { get; set; }
        public DateTime? Field44_DateOfPriorPlacement { get; set; }
        public bool Field45_TreatmentResultingFrom_OccupationalIllnessInjury { get; set; }
        public bool Field45_TreatmentResultingFrom_AutoAccident { get; set; }
        public bool Field45_TreatmentResultingFrom_OtherAccident { get; set; }
        public DateTime? Field46_DateOfAccident { get; set; }
        public string Field47_AutoAccidentState { get; set; }

        // BILLING DENTIST OR DENTAL ENTITY
        public J400Block Field48_BillingDentistInformation { get; set; }
        public string Field49_BillingDentistNpi { get; set; }
        public string Field50_BillingDentistLicenseNumber { get; set; }
        public string Field51_BillingDentistSsnOrTin { get; set; }
        public string Field52_BillingDentistPhoneNumber { get; set; }
        public string FIeld52A_BillingDentistAdditionalProviderId { get; set; }

        // TREATING DENTIST AND TREATMENT LOCATION INFORMATION
        public string Field53_TreatingDentistSignature { get; set; }
        public DateTime? Field53_TreatingDentistSignatureDate { get; set; }
        public string Field54_TreatingDentistNpi { get; set; }
        public string Field55_TreatingDentistLicenseNumber { get; set; }
        public string Field56_TreatingDentistAddress { get; set; }
        public string Field56A_TreatingDentistProviderSpecialtyCode { get; set; }
        public string Field57_TreatingDentistPhoneNumber { get; set; }
        public string Field58_TeatingDentistAdditionalPRoviderId { get; set; }
    }
}
