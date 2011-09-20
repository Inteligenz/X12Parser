using System;
using System.IO;
using System.Linq;
using OopFactory.X12.Hipaa.Claims.Forms.Professional;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public partial class ClaimTransformationService
    {

        /// <summary>
        /// Takes a generic claim object stream parameter and maps properties to 
        /// corresponding properties in the HCFA 1500 claim. Returns a HCFA1500 claim.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// ORDER BY HOW THEY APPEAR ON THE HCFA Form
        public HCFA1500Claim TransformX12837ToHcfa1500Model(Stream stream)
        {
            Claim claim = Transform837ToClaimDocument(stream).Claims.First();
            var hcfa = new HCFA1500Claim();

            hcfa.Field01_TypeOfCoverageIsChampVa = false;
            hcfa.Field01_TypeOfCoverageIsFECABlkLung = false;
            hcfa.Field01_TypeOfCoverageIsGroupHealthPlan = false;
            hcfa.Field01_TypeOfCoverageIsMedicaid = false;
            hcfa.Field01_TypeOfCoverageIsMedicare = false;
            hcfa.Field01_TypeOfCoverageIsOther = false;
            hcfa.Field01_TypeOfCoverageIsOther = false;
            hcfa.Field01_TypeOfCoverageIsTricareChampus = false;

            hcfa.Field01b_InsuredsIDNumber = string.Empty;

            ClaimMember patient = claim.Patient ?? claim.Subscriber;
            // Patient Name
            hcfa.Field02_PatientsLastName = patient.Name.LastName;
            hcfa.Field02_PatientsFirstName = patient.Name.FirstName;
            hcfa.Field02_PatientsMiddleName = patient.Name.MiddleName;
            // patient.MemberId // where on the 1500 is the MemberId?

            // patient birthdate
            hcfa.Field03_PatientsDateOfBirth = patient.DateOfBirth;
            // patient sex  // should I get rid of false and just use null?
            if (patient.Gender == GenderEnum.Male)
            {
                hcfa.Field03_PatientsSexFemale = null; // is this necessary or would always be null at this point anyway?
                hcfa.Field03_PatientsSexMale = true;
            }
            else if (patient.Gender == GenderEnum.Female)
            {
                hcfa.Field03_PatientsSexFemale = true;
                hcfa.Field03_PatientsSexMale = null;
            }

            hcfa.Field04_InsuredsFirstName = string.Empty;
            hcfa.Field04_InsuredsLastName = string.Empty;
            hcfa.Field04_InsuredsMiddleName = string.Empty;

            // Patient Address
            if (patient.Address != null)
            {
                hcfa.Field05_PatientsAddress_Street = patient.Address.Line1;
                // do we care about , patient.Address.Line2?
                hcfa.Field05_PatientsAddress_City = patient.Address.City;
                hcfa.Field05_PatientsAddress_State = patient.Address.StateCode;
                hcfa.Field05_PatientsAddress_Zip = patient.Address.PostalCode;
            }

            hcfa.Field06_PatientRelationshipToInsuredIsChildOf = false;
            hcfa.Field06_PatientRelationshipToInsuredIsOther = false;
            hcfa.Field06_PatientRelationshipToInsuredIsSelf = false;
            hcfa.Field06_PatientRelationshipToInsuredIsSpouseOf = false;

            hcfa.Field07_InsuredsAddress_City = string.Empty;
            hcfa.Field07_InsuredsAddress_State = string.Empty;
            hcfa.Field07_InsuredsAddress_Street = string.Empty;
            hcfa.Field07_InsuredsAddress_Zip = string.Empty;
            hcfa.Field07_InsuredsAreaCode = string.Empty;
            hcfa.Field07_InsuredsPhoneNumber = string.Empty;

            hcfa.Field08_PatientStatusEmployed = false;
            hcfa.Field08_PatientStatusFullTimeStudent = false;
            hcfa.Field08_PatientStatusMarried = false;
            hcfa.Field08_PatientStatusOther = false;
            hcfa.Field08_PatientStatusPartTimeStudent = false;
            hcfa.Field08_PatientStatusSingle = false;

            hcfa.Field09_OtherInsuredsFirstName = string.Empty;
            hcfa.Field09_OtherInsuredsLastName = string.Empty;
            hcfa.Field09_OtherInsuredsMiddleName = string.Empty;
            hcfa.Field09a_OtherInsuredsPolicyOrGroup = string.Empty;
            hcfa.Field09b_OtherInsuredIsFemale = false;
            hcfa.Field09b_OtherInsuredIsMale = false;
            hcfa.Field09b_OtherInsuredsDateOfBirth = DateTime.Now;
            hcfa.Field09c_OtherInsuredsEmployerNameOrSchoolName = string.Empty;
            hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName = string.Empty;

            hcfa.Field10a_PatientConditionRelatedToEmployment = false;
            hcfa.Field10b_PatientConditionRelatedToAutoAccident = false;
            hcfa.Field10b_PtConditionRelToAutoAccidentState = string.Empty;
            hcfa.Field10c_PatientConditionRelatedToOtherAccident = false;
            hcfa.Field10d_ReservedForLocalUse = string.Empty;

            hcfa.Field11_InsuredsPolicyGroupOfFECANumber = string.Empty;
            hcfa.Field11a_InsuredsDateOfBirth = DateTime.Now;
            hcfa.Field11a_InsuredsSexIsFemale = false;
            hcfa.Field11a_InsuredsSexIsMale = false;
            hcfa.Field11b_InsuredsEmployerOrSchool = string.Empty;
            hcfa.Field11c_InsuredsPlanOrProgramName = string.Empty;
            hcfa.Field11d_IsThereOtherHealthBenefitPlan = false;

            hcfa.Field12_PatientsOrAuthorizedSignature = string.Empty;
            hcfa.Field12_PatientsOrAuthorizedSignatureDate = DateTime.Now;

            hcfa.Field13_InsuredsOrAuthorizedSignature = string.Empty;

            hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy = DateTime.Now;

            hcfa.Field15_DatePatientHadSameOrSimilarIllness = DateTime.Now;

            hcfa.Field16_DatePatientUnableToWork_End = DateTime.Now;
            hcfa.Field16_DatePatientUnableToWork_Start = DateTime.Now;

            hcfa.Field17_ReferringProviderOrOtherSource_Credentials = string.Empty;
            hcfa.Field17_ReferringProviderOrOtherSource_FirstName = string.Empty;
            hcfa.Field17_ReferringProviderOrOtherSource_LastName = string.Empty;
            hcfa.Field17_ReferringProviderOrOtherSource_MiddleName = string.Empty;
            hcfa.Field17a_OtherID_Number = string.Empty;
            hcfa.Field17a_OtherID_Qualifier = string.Empty;
            hcfa.Field17b_NationalProviderIdentifier = string.Empty;
            

            // Admission date and hour
            hcfa.Field18_HospitalizationDateFrom = claim.AdmissionDate;
            // Statement Covers Period
            hcfa.Field18_HospitalizationDateFrom = claim.StatementFromDate;
            hcfa.Field18_HospitalizationDateTo = claim.StatementToDate;

            hcfa.Field19_ReservedForLocalUse = string.Empty;

            hcfa.Field20_OutsideLab = false;
            hcfa.Field20_OutsideLabCharges = 0;

            // Diagnosis codes
            foreach (var d in claim.Diagnoses)
            {
                var hcfaDiagnosis = new HCFA1500Diagnosis();
                hcfaDiagnosis.Field21_Diagnosis = d.Code;
                hcfa.Field21_Diagnoses.Add(hcfaDiagnosis);
            }

            hcfa.Field22_MedicaidSubmissionCode = string.Empty;
            hcfa.Field22_OriginalReferenceNumber = string.Empty;

            hcfa.Field23_PriorAuthorizationNumber = string.Empty;



            // Service Lines
            foreach (var line in claim.ServiceLines)
            {
                var hcfaLine = new HCFA1500ServiceLine();
                hcfaLine.Field24a_DateFrom = line.ServiceDateFrom;
                hcfaLine.Field24a_DateTo = line.ServiceDateTo;
                hcfaLine.Field24b_PlaceOfService = string.Empty;
                hcfaLine.Field24c_EmergencyIndicator = string.Empty;

                hcfaLine.Field24d_ProcedureCode = line.Procedure.ProcedureCode;
                // line.RevenueCode
                hcfaLine.Field24d_ProcedureCode = line.Procedure.ProcedureCode;
                hcfaLine.Field24d_Mod1 = line.Procedure.Modifier1;
                hcfaLine.Field24d_Mod2 = line.Procedure.Modifier2;
                hcfaLine.Field24d_Mod3 = line.Procedure.Modifier3;
                hcfaLine.Field24d_Mod4 = line.Procedure.Modifier4;

                hcfaLine.Field24e_DiagnosisPointer1 = string.Empty;
                hcfaLine.Field24e_DiagnosisPointer2 = string.Empty;
                hcfaLine.Field24e_DiagnosisPointer3 = string.Empty;
                hcfaLine.Field24e_DiagnosisPointer4 = string.Empty;

                hcfaLine.Field24f_Charges = line.ChargeAmount;
                hcfaLine.Field24g_DaysOrUnits = line.Quantity; //??
                hcfaLine.Field24h_EarlyPeriodicScreeningDiagnosisAndTreatment = string.Empty;
                hcfaLine.Field24i_RenderingProviderIdQualifier = string.Empty;
                hcfaLine.Field24j_RenderingProviderId = string.Empty;
                hcfaLine.Field24j_RenderingProviderNpi = string.Empty;
                //hcfaLine.Field24_CommentLine = line.Notes[0]; //. - is Comment Line same as notes?

                // line.NonCoveredChargeAmount

                // line.OperatingPhysician
                // line.OperatingPhysician.Name.LastName
                // line.OperatingPhysician.Name.FirstName
                if (line.OperatingPhysician != null)
                {
                    hcfaLine.Field24j_RenderingProviderNpi = line.OperatingPhysician.Npi;
                    hcfaLine.Field24j_RenderingProviderId = line.OperatingPhysician.ProviderInfo.Id;
                }

                hcfa.Field24_ServiceLines.Add(hcfaLine);
            }
            // Federal Tax Number
            hcfa.Field25_FederalTaxIDNumber = claim.PayToProvider.TaxId;
            // shouldnt we represent hcfa.Field25_IsSSN and Field25_IsEIN to know which type TaxID?
            hcfa.Field26_PatientAccountNumber = claim.PatientControlNumber;

            hcfa.Field27_AcceptAssignment = false;

            hcfa.Field28_TotalCharge = 0;

            hcfa.Field29_AmountPaid = 0;

            hcfa.Field30_BalanceDue = 0;

            hcfa.Field31_PhysicianOrSupplierSignatureDate = DateTime.Now;
            hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile = false;

            // Service Location
            hcfa.Field32_FacilityLocationInfo_Name = claim.ServiceLocation.Name.LastName;
            hcfa.Field32_FacilityLocationInfo_Street = claim.ServiceLocation.Address.Line1;
            hcfa.Field32_FacilityLocationInfo_City = claim.ServiceLocation.Address.City;
            hcfa.Field32_FacilityLocationInfo_State = claim.ServiceLocation.Address.StateCode;
            hcfa.Field32_FacilityLocationInfo_Zip = claim.ServiceLocation.Address.PostalCode;
            hcfa.Field32a_FacilityNationalProviderIdentifier = claim.ServiceLocationInfo.FacilityCode;
            hcfa.Field32b_FacilityOtherID = string.Empty;
            // Pay To Provider
            hcfa.Field33_BillingProvider_Name = claim.PayToProvider.Name.LastName;
            hcfa.Field33_BillingProvider_Street = claim.PayToProvider.Address.Line1;
            hcfa.Field33_BillingProvider_City = claim.PayToProvider.Address.City;
            hcfa.Field33_BillingProvider_State = claim.PayToProvider.Address.StateCode;
            hcfa.Field33_BillingProvider_Zip = claim.PayToProvider.Address.PostalCode;
            return hcfa;
        }
    }
}
