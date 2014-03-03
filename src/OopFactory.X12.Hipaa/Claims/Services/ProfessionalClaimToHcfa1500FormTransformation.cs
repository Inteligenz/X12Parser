using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Common;
using OopFactory.X12.Hipaa.Claims.Forms;
using OopFactory.X12.Hipaa.Claims.Forms.Professional;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public class ProfessionalClaimToHcfa1500FormTransformation : ClaimTransformationService,  IClaimToClaimFormTransfomation
    {
        private string _formImagePath;

        public ProfessionalClaimToHcfa1500FormTransformation(string formImagePath)
        {
            _formImagePath = formImagePath;
        }

        private FormDate formatFormDate(DateTime? dateTime)
        {
            return new FormDate
            {
                MM = String.Format("{0:MM}", dateTime),
                DD = String.Format("{0:dd}", dateTime),
                YY = String.Format("{0:yy}", dateTime)
            };
        }

        /// <summary>
        /// Takes a generic claim object stream parameter and maps properties to  
        /// corresponding properties in the HCFA 1500 claim. Returns a HCFA1500 claim.
        /// Follows crosswalk published at http://www.nucc.org/images/stories/PDF/1500_form_map_to_837p_4010a1_v1-0_112008.pdf
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        public virtual HCFA1500Claim TransformClaimToHcfa1500(Claim claim)
        {
	        var hcfa = new HCFA1500Claim();

	        if (claim == null)
	        {
		        hcfa.Field24_ServiceLines = new List<HCFA1500ServiceLine>();
		        hcfa.Field24_ServiceLines.Add(new HCFA1500ServiceLine());
		        return hcfa;
	        }

	        String indicatorCode = null;
	        if (claim.SubscriberInformation != null &&
		        claim.SubscriberInformation.ClaimFilingIndicatorCode != null)
	        {
		        indicatorCode = claim.SubscriberInformation.ClaimFilingIndicatorCode;
	        }

	        switch (indicatorCode)
	        {
		        case "CH":
			        hcfa.Field01_TypeOfCoverageIsTricareChampus = true;
			        break;
		        case "MB":
			        hcfa.Field01_TypeOfCoverageIsMedicare = true;
			        break;
		        case "MC":
			        hcfa.Field01_TypeOfCoverageIsMedicaid = true;
			        break;
		        case "VA":
			        hcfa.Field01_TypeOfCoverageIsChampVa = true;
			        break;
		        default:
			        if (claim.SubscriberInformation != null && claim.SubscriberInformation.ClaimFilingIndicatorCode != null)
				        hcfa.Field01_TypeOfCoverageIsOther = true;
			        break;
	        }

	        // XXX: I don't see any code corresponding to FECA Black Lung in the 837P standard
	        hcfa.Field01_TypeOfCoverageIsFECABlkLung = false;
	        hcfa.Field01_TypeOfCoverageIsGroupHealthPlan = false;

	        ClaimMember patient = claim.Patient ?? claim.Subscriber;
	        ClaimMember subscriber = claim.Subscriber;

	        if (!String.IsNullOrEmpty(patient.MemberId))
	        {
		        hcfa.Field01a_InsuredsIDNumber = patient.MemberId;
	        }
	        else if (patient != null &&
		        patient.Name != null &&
		        patient.Name.Identification != null &&
		        !string.IsNullOrEmpty(patient.Name.Identification.Id))
	        {
		        hcfa.Field01a_InsuredsIDNumber = patient.Name.Identification.Id;
	        }
	        else if (!String.IsNullOrEmpty(subscriber.MemberId))
	        {
		        hcfa.Field01a_InsuredsIDNumber = subscriber.MemberId;
	        }
	        else if (subscriber != null &&
		        subscriber.Name != null &&
		        subscriber.Name.Identification != null &&
		        !string.IsNullOrEmpty(subscriber.Name.Identification.Id))
	        {
		        hcfa.Field01a_InsuredsIDNumber = subscriber.Name.Identification.Id;
	        }
	        hcfa.Field01a_InsuredsIDNumber = hcfa.Field01a_InsuredsIDNumber;
	
	        // Patient Name
	        if (patient.Name != null)
		        hcfa.Field02_PatientsName = patient.Name.Formatted();

	        // patient birthdate
	        if (patient.DateOfBirth != null)
		        hcfa.Field03_PatientsDateOfBirth = formatFormDate(patient.DateOfBirth);

	        hcfa.Field03_PatientsSexFemale = patient.Gender == GenderEnum.Female;
	        hcfa.Field03_PatientsSexMale = patient.Gender == GenderEnum.Male;

	        if (subscriber.Name != null)
		        hcfa.Field04_InsuredsName = subscriber.Name.Formatted();

	        // Patient Address
	        if (patient.Address != null)
	        {
		        hcfa.Field05_PatientsAddress_Street = String.Format("{0} {1}", patient.Address.Line1, patient.Address.Line2).TrimEnd();
		        hcfa.Field05_PatientsAddress_City = patient.Address.City;
		        hcfa.Field05_PatientsAddress_State = patient.Address.StateCode;
		        hcfa.Field05_PatientsAddress_Zip = patient.Address.PostalCode;
	        }

	        // Relationship information from https://www.cahabagba.com/part_b/msp/Providers_Electronic_Billing_Instructions.htm
	        String patientRelationship = String.Empty;
	        if (claim.Patient != null && claim.Patient.Relationship != null)
	        {
		        patientRelationship = claim.Patient.Relationship.Code;
	        }
            else if (claim.SubscriberInformation != null)
            {
                patientRelationship = claim.SubscriberInformation.IndividualRelationshipCode;
            }
	        switch (patientRelationship)
	        {
		        case "01":
			        hcfa.Field06_PatientRelationshipToInsuredIsSpouseOf = true;
			        break;
		        case "19":
			        hcfa.Field06_PatientRelationshipToInsuredIsChildOf = true;
			        break;
		        case "18":
			        hcfa.Field06_PatientRelationshipToInsuredIsSelf = true;
			        break;
		        default:
			        if (claim.SubscriberInformation != null && claim.SubscriberInformation.ClaimFilingIndicatorCode != null)
				        hcfa.Field06_PatientRelationshipToInsuredIsOther = true;
			        break;
	        }

	        if (subscriber.Address != null)
	        {
		        hcfa.Field07_InsuredsAddress_Street = subscriber.Address.Line1;
		        hcfa.Field07_InsuredsAddress_City = subscriber.Address.City;
		        hcfa.Field07_InsuredsAddress_State = subscriber.Address.StateCode;
		        hcfa.Field07_InsuredsAddress_Zip = subscriber.Address.PostalCode;
	        }

	        // Not present on 837P
	        hcfa.Field07_InsuredsAreaCode =  String.Empty;
	        hcfa.Field07_InsuredsPhoneNumber = String.Empty;

	        // Not present on 837P
	        hcfa.Field08_PatientStatusIsEmployed = false;
	        hcfa.Field08_PatientStatusIsFullTimeStudent = false;
	        hcfa.Field08_PatientStatusIsMarried = false;
	        hcfa.Field08_PatientStatusIsOther = false;
	        hcfa.Field08_PatientStatusIsPartTimeStudent = false;
	        hcfa.Field08_PatientStatusIsSingle = false;

	        OtherSubscriberInformation otherSubscriber = null;
	        if (claim.OtherSubscriberInformations != null)
	        {
		        otherSubscriber = claim.OtherSubscriberInformations.FirstOrDefault();
	        }
	
	
	        // No way to get below three fields using 837P
	        hcfa.Field09b_OtherInsuredIsFemale = false;
	        hcfa.Field09b_OtherInsuredIsMale = false;
	        hcfa.Field09b_OtherInsuredsDateOfBirth = new FormDate();

	        if (otherSubscriber != null)
	        {
		        if (otherSubscriber.Name != null)
		        {
			        hcfa.Field09_OtherInsuredsName = otherSubscriber.Name.Formatted();
		        }
		        if (otherSubscriber.SubscriberInformation != null)
		        {
			        hcfa.Field09a_OtherInsuredsPolicyOrGroup = otherSubscriber.SubscriberInformation.ReferenceIdentification;
		        }
		        hcfa.Field09b_OtherInsuredsDateOfBirth = formatFormDate(otherSubscriber.DateOfBirth);
		        hcfa.Field09b_OtherInsuredIsFemale = otherSubscriber.Gender == GenderEnum.Female;
		        hcfa.Field09b_OtherInsuredIsMale = otherSubscriber.Gender == GenderEnum.Male;
		        hcfa.Field09c_OtherInsuredsEmployerNameOrSchoolName = String.Empty; // XXX: OK to assume org in last name? , Edit: this field should be left blank
		        if (otherSubscriber.OtherPayer != null)
		        {
			        hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName = otherSubscriber.OtherPayer.LastName;
		        }
	        }

	        hcfa.Field10a_PatientConditionRelatedToEmployment = claim.RelatedCauseCode1 == "EM" || claim.RelatedCauseCode2 == "EM" || claim.RelatedCauseCode3 == "EM";
	        hcfa.Field10b_PatientConditionRelatedToAutoAccident = claim.RelatedCauseCode1 == "AA" || claim.RelatedCauseCode2 == "AA" || claim.RelatedCauseCode3 == "AA";
	        hcfa.Field10c_PatientConditionRelatedToOtherAccident = claim.RelatedCauseCode1 == "AB" || claim.RelatedCauseCode1 == "AP" || claim.RelatedCauseCode1 == "OA" ||
														           claim.RelatedCauseCode2 == "AB" || claim.RelatedCauseCode2 == "AP" || claim.RelatedCauseCode2 == "OA" ||
														           claim.RelatedCauseCode3 == "AB" || claim.RelatedCauseCode3 == "AP" || claim.RelatedCauseCode3 == "OA";
	        hcfa.Field10b_PatientConditionRelToAutoAccidentState = claim.AutoAccidentState;

	        if (hcfa.Field10a_PatientConditionRelatedToEmployment)
		        hcfa.Field10d_ReservedForLocalUse = String.Empty;

	        if (claim.SubscriberInformation != null)
		        hcfa.Field11_InsuredsPolicyGroupOfFECANumber = claim.SubscriberInformation.ReferenceIdentification;
	        if (subscriber != null)
	        {
		        hcfa.Field11a_InsuredsDateOfBirth = formatFormDate(subscriber.DateOfBirth);
		        hcfa.Field11a_InsuredsSexIsFemale = subscriber.Gender == GenderEnum.Female;
		        hcfa.Field11a_InsuredsSexIsMale = subscriber.Gender == GenderEnum.Male;
	        }
	        if (claim.Payer != null)
	        {
		        hcfa.Field11b_InsuredsEmployerOrSchool = String.Empty; // should be left blank
		        if (claim.Payer.Name != null)
		        {
			        hcfa.Field11c_InsuredsPlanOrProgramName = claim.Payer.Name.LastName;
		        }
	        }

	        hcfa.Field11d_IsThereOtherHealthBenefitPlan = otherSubscriber != null;

	        hcfa.Field12_PatientsOrAuthorizedSignature = claim.ReleaseOfInformationCode == "Y" ? "Signature on file" : String.Empty;

	        hcfa.Field12_PatientsOrAuthorizedSignatureDate = new FormDate();

	        hcfa.Field13_InsuredsOrAuthorizedSignature = claim.BenefitsAssignmentCertificationIndicator == "Y" ? "Signature on file" : String.Empty;

	        var onsetDate = claim.Dates.FirstOrDefault(dr => dr.Qualifier == "431") ?? claim.Dates.FirstOrDefault(dr => dr.Qualifier == "439");

	        if (onsetDate != null)
	        {
		        hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy = formatFormDate(onsetDate.Date);
	        }

	        var similarIllnessDate = claim.Dates.FirstOrDefault(dr => dr.Qualifier == "438"); // only supported in 4010 837P
	        if (similarIllnessDate != null)
	        {
		        hcfa.Field15_DatePatientHadSameOrSimilarIllness = formatFormDate(similarIllnessDate.Date);
	        }

	        var disabilityStart = claim.Dates.FirstOrDefault(dr => dr.Qualifier == "360");
	        var disabilityEnd = claim.Dates.FirstOrDefault(dr => dr.Qualifier == "361");
	        if (disabilityStart != null)
	        {
		        hcfa.Field16_DatePatientUnableToWork_Start = formatFormDate(disabilityStart.Date);
	        }
	        if (disabilityEnd != null)
	        {
		        hcfa.Field16_DatePatientUnableToWork_End = formatFormDate(disabilityEnd.Date);
	        }

	        var referringProvider = claim.Providers.FirstOrDefault(pr => pr.Name.Type.Identifier == "DN" && pr.Name.Identification.Qualifier == "XX");
	        if (referringProvider != null)
	        {
		        hcfa.Field17_ReferringProviderOrOtherSource_Name = referringProvider.Name.Formatted();

		        var id = referringProvider.Identifications.FirstOrDefault();
		        if (id != null)
		        {
			        hcfa.Field17a_OtherID_Number = id.Id;
			        hcfa.Field17a_OtherID_Qualifier = id.Qualifier;
		        }

		        hcfa.Field17b_NationalProviderIdentifier = referringProvider.Npi;
	        }

	        // Admission date and hour
	        hcfa.Field18_HospitalizationDateFrom = new FormDate();
	        hcfa.Field18_HospitalizationDateTo = new FormDate();
	        if (claim.AdmissionDate.HasValue)
	        {
		        hcfa.Field18_HospitalizationDateFrom = formatFormDate(claim.AdmissionDate);
	        }
	        if (claim.DischargeTime.HasValue)
	        {
		        hcfa.Field18_HospitalizationDateTo = formatFormDate(claim.DischargeTime);
	        }

	        // Populating Loc19 with notes from 837
	        // There can only be one note
	        hcfa.Field19_ReservedForLocalUse = (claim.Notes.Count >= 1) ? claim.Notes[0].Description : System.String.Empty;
	
	        // Outside services are stored in claim service lines
	        double totalAmountSpent = 0.0;
	        foreach (var line in claim.ServiceLines)
	        {
		        if (line.PurchasedServiceIdentifier != null)
		        {
			        hcfa.Field20_OutsideLab = true;
			        if (line.PurchasedServiceAmount != null)
			        {
				        totalAmountSpent += Convert.ToDouble(line.PurchasedServiceAmount);
			        }
		        }
	        }

	        hcfa.Field20_OutsideLabCharges = (Decimal)totalAmountSpent;

	        var principalDiagnosis = claim.Diagnoses.FirstOrDefault(d => d.DiagnosisType == DiagnosisTypeEnum.Principal);
	        var otherDiagnoses = claim.Diagnoses.Where(d => d.DiagnosisType == DiagnosisTypeEnum.Other).ToList();

	        // Diagnosis codes
	        if (principalDiagnosis != null)
		        hcfa.Field21_Diagnosis1 = principalDiagnosis.FormattedCode();
	        if (otherDiagnoses.Count >= 1)
		        hcfa.Field21_Diagnosis2 = otherDiagnoses[0].FormattedCode();
	        if (otherDiagnoses.Count >= 2)
		        hcfa.Field21_Diagnosis3 = otherDiagnoses[1].FormattedCode();
	        if (otherDiagnoses.Count >= 3)
		        hcfa.Field21_Diagnosis4 = otherDiagnoses[2].FormattedCode();

	        var frequencyType = "";
	        if (claim.BillTypeCode.Length == 3)
	        {
		        frequencyType = claim.BillTypeCode.Substring(2, 1);
	        }
	        if (frequencyType == "7" || frequencyType == "8")
		        hcfa.Field22_MedicaidSubmissionCode = frequencyType;
	        else
		        hcfa.Field22_MedicaidSubmissionCode = String.Empty;

	        var originalRef = claim.Identifications.FirstOrDefault(id => id.Qualifier == "F8");

	        if (originalRef != null)
		        hcfa.Field22_OriginalReferenceNumber = originalRef.Id;
	        else
		        hcfa.Field22_OriginalReferenceNumber = String.Empty;

	        hcfa.Field23_PriorAuthorizationNumber = claim.PriorAuthorizationNumber;

	        var hcfaServiceLines = new List<HCFA1500ServiceLine>();

	        // Service Lines
	        foreach (var line in claim.ServiceLines)
	        {
		        var hcfaLine = new HCFA1500ServiceLine();
		        hcfaLine.DateFrom = new FormDate
		        {
			        MM = String.Format("{0:MM}", line.ServiceDateFrom),
			        DD = String.Format("{0:dd}", line.ServiceDateFrom),
			        YY = String.Format("{0:yy}", line.ServiceDateFrom)
		        };
		        hcfaLine.DateTo = new FormDate
		        {
			        MM = String.Format("{0:MM}", line.ServiceDateTo),
			        DD = String.Format("{0:dd}", line.ServiceDateTo),
			        YY = String.Format("{0:yy}", line.ServiceDateTo)
		        };

		        if (line.PlaceOfService != null && !string.IsNullOrWhiteSpace(line.PlaceOfService.Code))
			        hcfaLine.PlaceOfService = line.PlaceOfService.Code;
		        else
			        hcfaLine.PlaceOfService = claim.ServiceLocationInfo.FacilityCode;
		
		        hcfaLine.EmergencyIndicator = line.EmergencyIndicator;

		        hcfaLine.ProcedureCode = line.Procedure.ProcedureCode;
		        hcfaLine.ProcedureCode = line.Procedure.ProcedureCode;
		        hcfaLine.Mod1 = line.Procedure.Modifier1;
		        hcfaLine.Mod2 = line.Procedure.Modifier2;
		        hcfaLine.Mod3 = line.Procedure.Modifier3;
		        hcfaLine.Mod4 = line.Procedure.Modifier4;

		        hcfaLine.DiagnosisPointer1 = line.DiagnosisCodePointer1;
		        hcfaLine.DiagnosisPointer2 = line.DiagnosisCodePointer2;
		        hcfaLine.DiagnosisPointer3 = line.DiagnosisCodePointer3;
		        hcfaLine.DiagnosisPointer4 = line.DiagnosisCodePointer4;

		        hcfaLine.Charges = line.ChargeAmount;
		        hcfaLine.DaysOrUnits = line.Quantity;
		        hcfaLine.EarlyPeriodicScreeningDiagnosisAndTreatment = line.EpsdtIndicator;

		        if (line.RenderingProvider != null && !string.IsNullOrWhiteSpace(line.RenderingProvider.Npi))
			        hcfaLine.RenderingProviderNpi = line.RenderingProvider.Npi;
		        else if (claim.RenderingProvider != null && !string.IsNullOrWhiteSpace(claim.RenderingProvider.Npi))
			        hcfaLine.RenderingProviderNpi = claim.RenderingProvider.Npi;

		        if (line.RenderingProvider != null && line.RenderingProvider.Identifications.Count > 0)
		        {
			        hcfaLine.RenderingProviderIdQualifier = line.RenderingProvider.Identifications[0].Qualifier;
			        hcfaLine.RenderingProviderId = line.RenderingProvider.Identifications[0].Id;
		        }
		        else if (line.RenderingProvider != null && line.RenderingProvider.ProviderInfo != null)
		        {
			        hcfaLine.RenderingProviderIdQualifier = line.RenderingProvider.ProviderInfo.Qualifier;
			        hcfaLine.RenderingProviderId = line.RenderingProvider.ProviderInfo.Id;
		        }
		        else if (claim.RenderingProvider != null && claim.RenderingProvider.Identifications.Count > 0)
		        {
			        hcfaLine.RenderingProviderIdQualifier = claim.RenderingProvider.Identifications[0].Qualifier;
			        hcfaLine.RenderingProviderId = claim.RenderingProvider.Identifications[0].Id;
		        }
		        else if (claim.RenderingProvider != null && claim.RenderingProvider.ProviderInfo != null)
		        {
			        hcfaLine.RenderingProviderIdQualifier = claim.RenderingProvider.ProviderInfo.Qualifier;
			        hcfaLine.RenderingProviderId = claim.RenderingProvider.ProviderInfo.Id;
		        }


		        hcfaServiceLines.Add(hcfaLine);
	        }
	        hcfa.Field24_ServiceLines = hcfaServiceLines;


	        // Federal Tax Number
            if (claim.PayToProvider != null && !string.IsNullOrWhiteSpace(claim.PayToProvider.TaxId))
	        {
		        hcfa.Field25_FederalTaxIDNumber = claim.PayToProvider.TaxId;
		        if (claim.PayToProvider.Identifications.Exists(id=>id.Qualifier == "EI"))
			        hcfa.Field25_IsEIN = true;
		        if (claim.PayToProvider.Identifications.Exists(id => id.Qualifier == "SY"))
			        hcfa.Field25_IsSSN = true;
	        }
	        else
	        {
                if (claim.BillingProvider != null)
                {
                    hcfa.Field25_FederalTaxIDNumber = claim.BillingProvider.TaxId;
                    if (claim.BillingProvider.Identifications.Exists(id => id.Qualifier == "EI"))
                        hcfa.Field25_IsEIN = true;
                    if (claim.BillingProvider.Identifications.Exists(id => id.Qualifier == "SY"))
                        hcfa.Field25_IsSSN = true;
                }
	        }
		
	        // shouldnt we represent hcfa.Field25_IsSSN and Field25_IsEIN to know which type TaxID?
	        hcfa.Field26_PatientAccountNumber = claim.PatientControlNumber;

	        if (claim.ProviderAcceptAssignmentCode == "A" || claim.ProviderAcceptAssignmentCode == "B")
		        hcfa.Field27_AcceptAssignment = true;
	        else if (claim.ProviderAcceptAssignmentCode == "C")
		        hcfa.Field27_AcceptAssignment = false;

	        hcfa.Field28_TotalCharge = claim.TotalClaimChargeAmount;
	        hcfa.Field29_AmountPaid = claim.PatientAmountPaid ?? 0;
	        foreach (var otherSubscriberObj in claim.OtherSubscriberInformations)
	        {
		        if (otherSubscriberObj.Amounts.Count > 0)
			        hcfa.Field29_AmountPaid += otherSubscriberObj.Amounts[0].Amount;
	        }
	

	        hcfa.Field30_BalanceDue = hcfa.Field28_TotalCharge - hcfa.Field29_AmountPaid; // does not exist on 837P

	        if (claim.ProviderSignatureOnFile == "Y")
		        hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile = true;
	        else if (claim.ProviderSignatureOnFile == "N")
		        hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile = false;

	        // Service Location
	        var serviceLocation = claim.ServiceLocation;
	        if (serviceLocation != null)
	        {
		        if (serviceLocation.Name != null)
			        hcfa.Field32_ServiceFacilityLocation_Name = serviceLocation.Name.LastName;
		        else
			        hcfa.Field32_ServiceFacilityLocation_Name = null;

		        if (serviceLocation.Address != null)
		        {
			        hcfa.Field32_ServiceFacilityLocation_Street = serviceLocation.Address.Line1;
			        hcfa.Field32_ServiceFacilityLocation_City = serviceLocation.Address.City;
			        hcfa.Field32_ServiceFacilityLocation_State = serviceLocation.Address.StateCode;
			        hcfa.Field32_ServiceFacilityLocation_Zip = serviceLocation.Address.PostalCode;
		        }
		        else
		        {
			        hcfa.Field32_ServiceFacilityLocation_Street = string.Empty;
			        hcfa.Field32_ServiceFacilityLocation_City = string.Empty;
			        hcfa.Field32_ServiceFacilityLocation_State = string.Empty;
			        hcfa.Field32_ServiceFacilityLocation_Zip = string.Empty;
		        }

		        hcfa.Field32a_ServiceFacilityLocation_Npi = serviceLocation.Npi;
		        if (serviceLocation.Identifications != null && serviceLocation.Identifications.Count > 0)
			        hcfa.Field32b_ServiceFacilityLocation_OtherID = serviceLocation.Identifications.First().Id;
		        else
			        hcfa.Field32b_ServiceFacilityLocation_OtherID = null;
	        }
	        // Pay To Provider
	        if (claim.BillingProvider != null)
	        {
		        if (claim.BillingProvider.Name != null)
			        hcfa.Field33_BillingProvider_Name = claim.BillingProvider.Name.LastName;
		        else
			        hcfa.Field33_BillingProvider_Name = null;

		        if (claim.BillingProvider.Address != null)
		        {
			        hcfa.Field33_BillingProvider_Street = claim.BillingProvider.Address.Line1;
			        hcfa.Field33_BillingProvider_City = claim.BillingProvider.Address.City;
			        hcfa.Field33_BillingProvider_State = claim.BillingProvider.Address.StateCode;
		            hcfa.Field33_BillingProvider_Zip = claim.BillingProvider.Address.PostalCode;
		        }
		        else
		        {
			        hcfa.Field33_BillingProvider_Street = string.Empty;
			        hcfa.Field33_BillingProvider_City = string.Empty;
			        hcfa.Field33_BillingProvider_State = string.Empty;
			        hcfa.Field33_BillingProvider_Zip = string.Empty;
		        }

		        hcfa.Field33a_BillingProvider_Npi = claim.BillingProvider.Npi;
	        }

	        LimitFieldWidths(hcfa);

	        return hcfa;
        }


        private void LimitFieldWidths(HCFA1500Claim hcfa)
        {
            hcfa.Field01a_InsuredsIDNumber = SetStringLength(hcfa.Field01a_InsuredsIDNumber, 35);
            hcfa.Field02_PatientsName = SetStringLength(hcfa.Field02_PatientsName, 28);
            hcfa.Field04_InsuredsName = SetStringLength(hcfa.Field04_InsuredsName, 30);
            hcfa.Field05_PatientsAddress_Street = SetStringLength(hcfa.Field05_PatientsAddress_Street, 28);
            hcfa.Field05_PatientsAddress_City = SetStringLength(hcfa.Field05_PatientsAddress_City, 29);
            hcfa.Field05_PatientsAddress_Zip = SetStringLength(hcfa.Field05_PatientsAddress_Zip, 14);
            hcfa.Field07_InsuredsAddress_Street = SetStringLength(hcfa.Field07_InsuredsAddress_Street, 35);
            hcfa.Field07_InsuredsAddress_City = SetStringLength(hcfa.Field07_InsuredsAddress_City, 28);
            hcfa.Field07_InsuredsAddress_Zip = SetStringLength(hcfa.Field07_InsuredsAddress_Zip, 14);
            hcfa.Field09_OtherInsuredsName = SetStringLength(hcfa.Field09_OtherInsuredsName, 28);
            hcfa.Field09a_OtherInsuredsPolicyOrGroup = SetStringLength(hcfa.Field09a_OtherInsuredsPolicyOrGroup, 28);
            hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName = SetStringLength(hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName, 28);
            hcfa.Field11_InsuredsPolicyGroupOfFECANumber = SetStringLength(hcfa.Field11_InsuredsPolicyGroupOfFECANumber, 35);
            hcfa.Field11c_InsuredsPlanOrProgramName = SetStringLength(hcfa.Field11c_InsuredsPlanOrProgramName, 35);
            hcfa.Field17_ReferringProviderOrOtherSource_Name = SetStringLength(hcfa.Field17_ReferringProviderOrOtherSource_Name, 26);
            hcfa.Field17a_OtherID_Qualifier = SetStringLength(hcfa.Field17a_OtherID_Qualifier, 3);
            hcfa.Field17a_OtherID_Number = SetStringLength(hcfa.Field17a_OtherID_Number, 16);
            hcfa.Field17b_NationalProviderIdentifier = SetStringLength(hcfa.Field17b_NationalProviderIdentifier, 16);
            hcfa.Field22_MedicaidSubmissionCode = SetStringLength(hcfa.Field22_MedicaidSubmissionCode, 11);
            hcfa.Field22_OriginalReferenceNumber = SetStringLength(hcfa.Field22_OriginalReferenceNumber, 18);
            hcfa.Field23_PriorAuthorizationNumber = SetStringLength(hcfa.Field23_PriorAuthorizationNumber, 30);

            foreach (var line in hcfa.Field24_ServiceLines)
            {
                line.RenderingProviderNpi = SetStringLength(line.RenderingProviderNpi, 12);
            }
            hcfa.Field25_FederalTaxIDNumber = SetStringLength(hcfa.Field25_FederalTaxIDNumber, 15);
            hcfa.Field26_PatientAccountNumber = SetStringLength(hcfa.Field26_PatientAccountNumber, 14);
            hcfa.Field32_ServiceFacilityLocation_Name = SetStringLength(hcfa.Field32_ServiceFacilityLocation_Name, 31);
            hcfa.Field32_ServiceFacilityLocation_Street = SetStringLength(hcfa.Field32_ServiceFacilityLocation_Street, 31);
            hcfa.Field32_ServiceFacilityLocation_City = SetStringLength(hcfa.Field32_ServiceFacilityLocation_City, 16);
            hcfa.Field32_ServiceFacilityLocation_State = SetStringLength(hcfa.Field32_ServiceFacilityLocation_State, 2);
            hcfa.Field32_ServiceFacilityLocation_Zip = SetStringLength(hcfa.Field32_ServiceFacilityLocation_Zip, 10);
            hcfa.Field32a_ServiceFacilityLocation_Npi = SetStringLength(hcfa.Field32a_ServiceFacilityLocation_Npi, 11);
            hcfa.Field32b_ServiceFacilityLocation_OtherID = SetStringLength(hcfa.Field32b_ServiceFacilityLocation_OtherID, 17);
            hcfa.Field33_BillingProvider_Name = SetStringLength(hcfa.Field33_BillingProvider_Name, 35);
            hcfa.Field33_BillingProvider_Street = SetStringLength(hcfa.Field33_BillingProvider_Street, 31);
            hcfa.Field33_BillingProvider_City = SetStringLength(hcfa.Field33_BillingProvider_City, 19);
            hcfa.Field33_BillingProvider_State = SetStringLength(hcfa.Field33_BillingProvider_State, 2);
            hcfa.Field33_BillingProvider_Zip = SetStringLength(hcfa.Field33_BillingProvider_Zip, 10);
            hcfa.Field33a_BillingProvider_Npi = SetStringLength(hcfa.Field33a_BillingProvider_Npi, 10);
        }

        private string SetStringLength(string source, int limit)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(source))
            {
                if (source.Length > limit)
                {
                    result = source.Substring(0, limit);
                }
                else
                {
                    return source;
                }
            }
            return result;
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text)
        {
            return AddBlock(page, x, y, width, text, TextAlignEnum.left);
        }

        // Returns an "X" if the boolean is true, "" otherwise.
        // Used for filling in the CMS 1500 form where X's are placed where true
        private String ConditionalMarker(Boolean b)
        {
            return b ? "X" : ""; 
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text, TextAlignEnum textAlign)
        {
            decimal xScale = 0.1m;
            decimal yScale = 0.1685m;
            var block = new FormBlock
            {
                TextAlign = textAlign,
                Left = -0.21m + xScale * x,
                Top = 0.1m + yScale * y,
                Width = xScale * width,
                Height = yScale * 1.1m,
                Text = text
            };
            page.Blocks.Add(block);
            return block;
        }

        public virtual List<FormPage> TransformHcfa1500ToFormPages(HCFA1500Claim hcfa)
        {
            List<FormPage> pages = new List<FormPage>();
            FormPage page = null;
            for (int i = 0; i < hcfa.Field24_ServiceLines.Count; i++)
            {
                if (i % 6 == 0)
                {
                    page = new FormPage();
                    pages.Add(page);
                    page.MasterReference = "hcfa1500";
                    page.ImagePath = _formImagePath;

                    // Render header
                    // LINE 1
                    AddBlock(page, 4, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsMedicare));
                    AddBlock(page, 11, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsMedicaid));
                    AddBlock(page, 18, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsTricareChampus));
                    AddBlock(page, 27, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsChampVa));
                    AddBlock(page, 34, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsGroupHealthPlan));
                    AddBlock(page, 42, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsFECABlkLung));
                    AddBlock(page, 48, 7, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsOther));
                    AddBlock(page, 53, 7, 30, hcfa.Field01a_InsuredsIDNumber);

                    // LINE 2
                    AddBlock(page, 4, 9, 28.5m, hcfa.Field02_PatientsName);
                    AddBlock(page, 34, 9, 3, hcfa.Field03_PatientsDateOfBirth.MM);
                    AddBlock(page, 37, 9, 3, hcfa.Field03_PatientsDateOfBirth.DD);
                    AddBlock(page, 40, 9, 3, hcfa.Field03_PatientsDateOfBirth.YY);
                    AddBlock(page, 44.5m, 9, 2.5m, ConditionalMarker(hcfa.Field03_PatientsSexMale), TextAlignEnum.center);
                    AddBlock(page, 49.5m, 9, 2.5m, ConditionalMarker(hcfa.Field03_PatientsSexFemale), TextAlignEnum.center);
                    AddBlock(page, 53, 9, 30, hcfa.Field04_InsuredsName);

                    // LINE 3
                    AddBlock(page, 4, 11, 28.5m, hcfa.Field05_PatientsAddress_Street);
                    AddBlock(page, 36, 11, 2, ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsSelf)); //self
                    AddBlock(page, 41, 11, 2, ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsSpouseOf)); //spouse
                    AddBlock(page, 45, 11, 2, ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsChildOf)); //child
                    AddBlock(page, 50, 11, 2, ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsOther)); // other
                    AddBlock(page, 53, 11, 30, hcfa.Field07_InsuredsAddress_Street);

                    // LINE 4
                    AddBlock(page, 4, 13, 25, hcfa.Field05_PatientsAddress_City);
                    AddBlock(page, 29, 13, 3.5m, hcfa.Field05_PatientsAddress_State);

                    // Field 8 unused in CMS-1500
                    AddBlock(page, 38, 13, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsSingle));
                    AddBlock(page, 44, 13, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsMarried));
                    AddBlock(page, 50, 13, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsOther));

                    AddBlock(page, 53, 13, 23, hcfa.Field07_InsuredsAddress_City);
                    AddBlock(page, 77, 13, 6, hcfa.Field07_InsuredsAddress_State);

                    // LINE 5
                    AddBlock(page, 4, 15, 13, hcfa.Field05_PatientsAddress_Zip);
                    AddBlock(page, 18, 15, 14.5m, hcfa.Field05_PatientsTelephone);

                    // Field 8 unused in CMS-1500
                    AddBlock(page, 38, 15, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsEmployed));
                    AddBlock(page, 44, 15, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsFullTimeStudent));
                    AddBlock(page, 50, 15, 2, ConditionalMarker(hcfa.Field08_PatientStatusIsPartTimeStudent));

                    AddBlock(page, 53, 15, 12, hcfa.Field07_InsuredsAddress_Zip);
                    AddBlock(page, 68.5m, 15, 14.5m, hcfa.Field07_InsuredsPhoneNumber);

                    // LINE 6
                    AddBlock(page, 4, 17, 28.5m, hcfa.Field09_OtherInsuredsName);
                    AddBlock(page, 53, 17, 30, hcfa.Field11_InsuredsPolicyGroupOfFECANumber);

                    // LINE 7
                    AddBlock(page, 4, 19, 28.5m, hcfa.Field09a_OtherInsuredsPolicyOrGroup);
                    AddBlock(page, 38, 19, 2, ConditionalMarker(hcfa.Field10a_PatientConditionRelatedToEmployment));
                    AddBlock(page, 44, 19, 2, ConditionalMarker(!hcfa.Field10a_PatientConditionRelatedToEmployment));
                    AddBlock(page, 56, 19, 3, hcfa.Field11a_InsuredsDateOfBirth.MM);
                    AddBlock(page, 59, 19, 3, hcfa.Field11a_InsuredsDateOfBirth.DD);
                    AddBlock(page, 62, 19, 3, hcfa.Field11a_InsuredsDateOfBirth.YY);
                    AddBlock(page, 71.25m, 19, 2, ConditionalMarker(hcfa.Field11a_InsuredsSexIsMale), TextAlignEnum.center);
                    AddBlock(page, 78.5m, 19, 2, ConditionalMarker(hcfa.Field11a_InsuredsSexIsFemale), TextAlignEnum.center);

                    // LINE 8
                    // Field 9b is not supplied by 837P data.
                    AddBlock(page, 5, 21, 3, hcfa.Field09b_OtherInsuredsDateOfBirth.MM);
                    AddBlock(page, 8, 21, 3, hcfa.Field09b_OtherInsuredsDateOfBirth.DD);
                    AddBlock(page, 11, 21, 3, hcfa.Field09b_OtherInsuredsDateOfBirth.YY);
                    AddBlock(page, 21, 21, 2, ConditionalMarker(hcfa.Field09b_OtherInsuredIsMale));
                    AddBlock(page, 27, 21, 2, ConditionalMarker(hcfa.Field09b_OtherInsuredIsFemale));

                    AddBlock(page, 38, 21, 2, ConditionalMarker(hcfa.Field10b_PatientConditionRelatedToAutoAccident));
                    AddBlock(page, 44, 21, 2, ConditionalMarker(!hcfa.Field10b_PatientConditionRelatedToAutoAccident));
                    AddBlock(page, 48, 21, 2.5m, hcfa.Field10b_PatientConditionRelToAutoAccidentState);
                    AddBlock(page, 53, 21, 30, hcfa.Field11b_InsuredsEmployerOrSchool);

                    // LINE 9
                    AddBlock(page, 4, 23, 28.5m, hcfa.Field09c_OtherInsuredsEmployerNameOrSchoolName);
                    AddBlock(page, 38, 23, 2, ConditionalMarker(hcfa.Field10c_PatientConditionRelatedToOtherAccident));
                    AddBlock(page, 44, 23, 2, ConditionalMarker(!hcfa.Field10c_PatientConditionRelatedToOtherAccident));
                    AddBlock(page, 53, 23, 30, hcfa.Field11c_InsuredsPlanOrProgramName);

                    // LINE 10
                    AddBlock(page, 4, 25, 28.5m, hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName);
                    AddBlock(page, 33, 25, 20, hcfa.Field10d_ReservedForLocalUse);
                    AddBlock(page, 55, 25, 2, ConditionalMarker(hcfa.Field11d_IsThereOtherHealthBenefitPlan));
                    AddBlock(page, 60, 25, 2, ConditionalMarker(!hcfa.Field11d_IsThereOtherHealthBenefitPlan)); 

                    // LINE 11
                    AddBlock(page, 9, 29, 25, hcfa.Field12_PatientsOrAuthorizedSignature, TextAlignEnum.center);
                    AddBlock(page, 39, 29, 14, hcfa.Field12_PatientsOrAuthorizedSignatureDate.ToString(), TextAlignEnum.center);
                    AddBlock(page, 59, 29, 24, hcfa.Field13_InsuredsOrAuthorizedSignature, TextAlignEnum.center);

                    // LINE 12
                    if (hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy != null)
                    {
                        AddBlock(page, 5, 31, 3, hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy.MM);
                        AddBlock(page, 8, 31, 3, hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy.DD);
                        AddBlock(page, 11, 31, 3, hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy.YY);
                    }

                    // Field 15 
                    AddBlock(page, 40, 31, 3, hcfa.Field15_DatePatientHadSameOrSimilarIllness.MM);
                    AddBlock(page, 43, 31, 3, hcfa.Field15_DatePatientHadSameOrSimilarIllness.DD);
                    AddBlock(page, 46, 31, 3, hcfa.Field15_DatePatientHadSameOrSimilarIllness.YY);

                    if (hcfa.Field16_DatePatientUnableToWork_Start != null)
                    {
                        AddBlock(page, 57, 31, 3, hcfa.Field16_DatePatientUnableToWork_Start.MM);
                        AddBlock(page, 60, 31, 3, hcfa.Field16_DatePatientUnableToWork_Start.DD);
                        AddBlock(page, 63, 31, 3, hcfa.Field16_DatePatientUnableToWork_Start.YY);
                    }

                    if (hcfa.Field16_DatePatientUnableToWork_End != null)
                    {
                        AddBlock(page, 71, 31, 3, hcfa.Field16_DatePatientUnableToWork_End.MM);
                        AddBlock(page, 74, 31, 3, hcfa.Field16_DatePatientUnableToWork_End.DD);
                        AddBlock(page, 77, 31, 3, hcfa.Field16_DatePatientUnableToWork_End.YY);
                    }

                    // LINE 13
                    AddBlock(page, 4, 33, 26, hcfa.Field17_ReferringProviderOrOtherSource_Name);
                    AddBlock(page, 33, 32, 3, hcfa.Field17a_OtherID_Qualifier);
                    AddBlock(page, 36, 32, 16, hcfa.Field17a_OtherID_Number);
                    AddBlock(page, 36, 33, 16, hcfa.Field17b_NationalProviderIdentifier);

                    // Field 18
                    AddBlock(page, 57, 33, 3, hcfa.Field18_HospitalizationDateFrom.MM);
                    AddBlock(page, 60, 33, 3, hcfa.Field18_HospitalizationDateFrom.DD);
                    AddBlock(page, 63, 33, 3, hcfa.Field18_HospitalizationDateFrom.YY);
                    AddBlock(page, 71, 33, 3, hcfa.Field18_HospitalizationDateTo.MM);
                    AddBlock(page, 74, 33, 3, hcfa.Field18_HospitalizationDateTo.DD);
                    AddBlock(page, 77, 33, 3, hcfa.Field18_HospitalizationDateTo.YY);

                    // LINE 14
                    
                    // We limit the length of the remark to only the size of the block. 
                    if (hcfa.Field19_ReservedForLocalUse != null && hcfa.Field19_ReservedForLocalUse.Length > 58)
                        AddBlock(page, 4, 35, 49, hcfa.Field19_ReservedForLocalUse.Substring(0, 58));
                    else
                        AddBlock(page, 4, 35, 49, hcfa.Field19_ReservedForLocalUse);
                    AddBlock(page, 55, 35, 2, ConditionalMarker(hcfa.Field20_OutsideLab));
                    AddBlock(page, 60, 35, 2, ConditionalMarker(!hcfa.Field20_OutsideLab));
                    AddBlock(page, 65, 35, 9, hcfa.Field20_OutsideLab ? Convert.ToString(hcfa.Field20_OutsideLabCharges) : "", TextAlignEnum.right);
                    AddBlock(page, 74, 35, 9, "", TextAlignEnum.right); // Note, we do not use second charge box at all here.

                    // Line 15
                    AddBlock(page, 6.5m, 37, 8, hcfa.Field21_Diagnosis1);
                    AddBlock(page, 33.5m, 37, 8, hcfa.Field21_Diagnosis3);
                    AddBlock(page, 53, 37, 11, hcfa.Field22_MedicaidSubmissionCode);
                    AddBlock(page, 65, 37, 18, hcfa.Field22_OriginalReferenceNumber);

                    // Line 16
                    AddBlock(page, 6.5m, 39, 8, hcfa.Field21_Diagnosis2);
                    AddBlock(page, 33.5m, 39, 8, hcfa.Field21_Diagnosis4);
                    AddBlock(page, 53, 39, 30, hcfa.Field23_PriorAuthorizationNumber);

                }

                // Render service lines
                decimal y = 42 + 2 * (i % 6);
                var line = hcfa.Field24_ServiceLines[i];
                AddBlock(page, 4, y, 60, line.CommentLine);
                AddBlock(page, 68, y, 3, line.RenderingProviderIdQualifier);
                AddBlock(page, 71, y, 12, line.RenderingProviderId);

                if (line.DateFrom != null)
                {
                    AddBlock(page, 4, y + 1, 3, line.DateFrom.MM);
                    AddBlock(page, 7, y + 1, 3, line.DateFrom.DD);
                    AddBlock(page, 10, y + 1, 3, line.DateFrom.YY);
                }
                else
                {
                    AddBlock(page, 4, y + 1, 3, string.Empty);
                    AddBlock(page, 7, y + 1, 3, string.Empty);
                    AddBlock(page, 10, y + 1, 3, string.Empty);
                }
                if (line.DateTo != null)
                {
                    AddBlock(page, 13, y + 1, 3, line.DateTo.MM);
                    AddBlock(page, 16, y + 1, 3, line.DateTo.DD);
                    AddBlock(page, 19, y + 1, 3, line.DateTo.YY);
                }
                else
                {
                    AddBlock(page, 13, y + 1, 3, string.Empty);
                    AddBlock(page, 16, y + 1, 3, string.Empty);
                    AddBlock(page, 19, y + 1, 3, string.Empty);
                }
                AddBlock(page, 22, y + 1, 3, line.PlaceOfService);
                AddBlock(page, 25, y + 1, 2, line.EmergencyIndicator);
                AddBlock(page, 29, y + 1, 6, line.ProcedureCode);
                AddBlock(page, 36, y + 1, 3, line.Mod1);
                AddBlock(page, 39, y + 1, 3, line.Mod2);
                AddBlock(page, 42, y + 1, 3, line.Mod3);
                AddBlock(page, 45, y + 1, 3, line.Mod4);
                AddBlock(page, 48, y + 1, 2, line.DiagnosisPointer1);
                AddBlock(page, 49, y + 1, 2, line.DiagnosisPointer2);
                AddBlock(page, 50, y + 1, 2, line.DiagnosisPointer3);
                AddBlock(page, 51, y + 1, 2, line.DiagnosisPointer4);
                AddBlock(page, 53, y + 1, 9, String.Format("{0:0.00}", line.Charges).Replace(".", " "), TextAlignEnum.right);
                AddBlock(page, 62, y + 1, 4, String.Format("{0}", line.DaysOrUnits), TextAlignEnum.right);
                AddBlock(page, 66, y + 1, 2, line.EarlyPeriodicScreeningDiagnosisAndTreatment);
                AddBlock(page, 71, y + 1, 12, line.RenderingProviderNpi);



                if (i % 6 == 5 || i == hcfa.Field24_ServiceLines.Count - 1) // Footer
                {
                    // Render footer
                    AddBlock(page, 4, 55, 15, hcfa.Field25_FederalTaxIDNumber);
                    if (hcfa.Field25_IsSSN)
                        AddBlock(page, 20, 55, 2, "X");
                    if (hcfa.Field25_IsEIN)
                        AddBlock(page, 22, 55, 2, "X");

                    AddBlock(page, 26, 55, 14, hcfa.Field26_PatientAccountNumber);

                    if (hcfa.Field27_AcceptAssignment.HasValue)
                    {
                        if (hcfa.Field27_AcceptAssignment.Value == true)
                            AddBlock(page, 41, 55, 2, "X");
                        else
                            AddBlock(page, 46, 55, 2, "X");
                    }

                    AddBlock(page, 55, 55, 9, String.Format("{0:0.00}", hcfa.Field28_TotalCharge).Replace(".", " "), TextAlignEnum.right);
                    AddBlock(page, 65, 55, 9, String.Format("{0:0.00}", hcfa.Field29_AmountPaid).Replace(".", " "), TextAlignEnum.right);
                    AddBlock(page, 74, 55, 9, String.Format("{0:0.00}", hcfa.Field30_BalanceDue).Replace(".", " "), TextAlignEnum.right);

                    // Box 31
                    if (hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile.HasValue)
                    {
                        AddBlock(page, 4, 58, 21, "PROVIDER SIGNATURE", TextAlignEnum.center);
                        if (hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile.Value == true)
                            AddBlock(page, 4, 59, 21, "IS ON FILE", TextAlignEnum.center);
                        else
                            AddBlock(page, 4, 59, 21, "NOT ON FILE", TextAlignEnum.center);
                    }
                    // Box 32
                    AddBlock(page, 26, 57, 27, hcfa.Field32_ServiceFacilityLocation_Name);
                    AddBlock(page, 26, 58, 27, hcfa.Field32_ServiceFacilityLocation_Street);
                    AddBlock(page, 26, 59, 27, String.Format("{0}, {1} {2}", hcfa.Field32_ServiceFacilityLocation_City, hcfa.Field32_ServiceFacilityLocation_State, hcfa.Field32_ServiceFacilityLocation_Zip));
                    AddBlock(page, 27, 60, 10, hcfa.Field32a_ServiceFacilityLocation_Npi);
                    AddBlock(page, 38, 60, 15, hcfa.Field32b_ServiceFacilityLocation_OtherID);

                    // Box 33
                    AddBlock(page, 69, 56, 27, hcfa.Field33_BillingProvider_TelephoneNumber); 
                    AddBlock(page, 53, 57, 27, hcfa.Field33_BillingProvider_Name);
                    AddBlock(page, 53, 58, 27, hcfa.Field33_BillingProvider_Street);
                    AddBlock(page, 53, 59, 27, String.Format("{0}, {1} {2}", hcfa.Field33_BillingProvider_City, hcfa.Field33_BillingProvider_State, hcfa.Field33_BillingProvider_Zip));
                    AddBlock(page, 54, 60, 10, hcfa.Field33a_BillingProvider_Npi);
                    AddBlock(page, 65, 60, 15, hcfa.Field33b_BillingProvider_OtherID);

                }

            }

            return pages;
        }

        public virtual List<FormPage> TransformClaimToClaimFormFoXml(Claim claim)
        {
            HCFA1500Claim hcfa = TransformClaimToHcfa1500(claim);

            return TransformHcfa1500ToFormPages(hcfa);
        }
    }
}
