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
        private string _formTemplatePath;

        public ProfessionalClaimToHcfa1500FormTransformation(string formTemplatePath)
        {
            _formTemplatePath = formTemplatePath;
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
            hcfa.Field08_Reserved = string.Empty;

	        OtherSubscriberInformation otherSubscriber = null;
	        if (claim.OtherSubscriberInformations != null)
	        {
		        otherSubscriber = claim.OtherSubscriberInformations.FirstOrDefault();
	        }
	
            hcfa.Field09b_Reserved = string.Empty;

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

		        hcfa.Field09c_Reserved = String.Empty;

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
		        hcfa.Field11b_OtherClaimId = String.Empty; // should be left blank
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
	        hcfa.Field19_AdditionalClaimInfo = (claim.Notes.Count >= 1) ? claim.Notes[0].Description : System.String.Empty;
	
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
		        hcfa.Field21_DiagnosisA = principalDiagnosis.FormattedCode();
	        if (otherDiagnoses.Count >= 1)
		        hcfa.Field21_DiagnosisB = otherDiagnoses[0].FormattedCode();
	        if (otherDiagnoses.Count >= 2)
		        hcfa.Field21_DiagnosisC = otherDiagnoses[1].FormattedCode();
	        if (otherDiagnoses.Count >= 3)
		        hcfa.Field21_DiagnosisD = otherDiagnoses[2].FormattedCode();
            if (otherDiagnoses.Count >= 4)
                hcfa.Field21_DiagnosisE = otherDiagnoses[3].FormattedCode();
            if (otherDiagnoses.Count >= 5)
                hcfa.Field21_DiagnosisF = otherDiagnoses[4].FormattedCode();
            if (otherDiagnoses.Count >= 6)
                hcfa.Field21_DiagnosisG = otherDiagnoses[5].FormattedCode();
            if (otherDiagnoses.Count >= 7)
                hcfa.Field21_DiagnosisH = otherDiagnoses[6].FormattedCode();
            if (otherDiagnoses.Count >= 8)
                hcfa.Field21_DiagnosisI = otherDiagnoses[7].FormattedCode();
            if (otherDiagnoses.Count >= 9)
                hcfa.Field21_DiagnosisJ = otherDiagnoses[8].FormattedCode();
            if (otherDiagnoses.Count >= 10)
                hcfa.Field21_DiagnosisK = otherDiagnoses[9].FormattedCode();
            if (otherDiagnoses.Count >= 11)
                hcfa.Field21_DiagnosisL = otherDiagnoses[10].FormattedCode();

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

		        hcfaLine.DiagnosisPointerA = line.DiagnosisCodePointer1;
		        hcfaLine.DiagnosisPointerB = line.DiagnosisCodePointer2;
		        hcfaLine.DiagnosisPointerC = line.DiagnosisCodePointer3;
		        hcfaLine.DiagnosisPointerD = line.DiagnosisCodePointer4;

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

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text, string fieldName)
        {
            return AddBlock(page, x, y, width, text, fieldName, TextAlignEnum.left);
        }

        // Returns an "X" if the boolean is true, "" otherwise.
        // Used for filling in the CMS 1500 form where X's are placed where true
        private String ConditionalMarker(Boolean b)
        {
            return b ? "X" : ""; 
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text, string fieldName, TextAlignEnum textAlign)
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
                Text = text,
                FieldName = fieldName
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
                    page.ImagePath = _formTemplatePath;

                    // Render header
                    // LINE 1
                    AddBlock(page, 9.4m, 10, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsMedicare), "Field01_TypeOfCoverageIsMedicare");
                    AddBlock(page, 15.4m, 10, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsMedicaid), "Field01_TypeOfCoverageIsMedicaid");
                    AddBlock(page, 21.4m, 10, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsTricareChampus), "Field01_TypeOfCoverageIsTricareChampus");
                    AddBlock(page, 29.3m, 10, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsChampVa), "Field01_TypeOfCoverageIsChampVa");
                    AddBlock(page, 35.4m, 10, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsGroupHealthPlan), "Field01_TypeOfCoverageIsGroupHealthPlan");
                    AddBlock(page, 42.5m, 10, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsFECABlkLung), "Field01_TypeOfCoverageIsFECABlkLung");
                    AddBlock(page, 47.7m, 10, 2, ConditionalMarker(hcfa.Field01_TypeOfCoverageIsOther), "Field01_TypeOfCoverageIsOther");
                    AddBlock(page, 52.5m, 9.9m, 30, hcfa.Field01a_InsuredsIDNumber, "Field01a_InsuredsIDNumber");

                    // LINE 2
                    AddBlock(page, 10, 11.5m, 28.5m, hcfa.Field02_PatientsName, "Field02_PatientsName");
                    AddBlock(page, 35.4m, 11.6m, 3, hcfa.Field03_PatientsDateOfBirth.MM, "Field03_PatientsDateOfBirthMM");
                    AddBlock(page, 38.3m, 11.6m, 3, hcfa.Field03_PatientsDateOfBirth.DD, "Field03_PatientsDateOfBirthDD");
                    AddBlock(page, 41.5m, 11.6m, 3, hcfa.Field03_PatientsDateOfBirth.YY, "Field03_PatientsDateOfBirthYY");
                    AddBlock(page, 44.6m, 11.6m, 2.5m, ConditionalMarker(hcfa.Field03_PatientsSexMale), "Field03_PatientsSexMale", TextAlignEnum.center);
                    AddBlock(page, 49.1m, 11.6m, 2.5m, ConditionalMarker(hcfa.Field03_PatientsSexFemale), "Field03_PatientsSexFemale", TextAlignEnum.center);
                    AddBlock(page, 52.5m, 11.5m, 30, hcfa.Field04_InsuredsName, "Field04_InsuredsName");

                    // LINE 3
                    AddBlock(page, 10, 13.3m, 28.5m, hcfa.Field05_PatientsAddress_Street, "Field05_PatientsAddress_Street");
                    AddBlock(page, 37.2m, 13.4m, 2, ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsSelf), "Field06_PatientRelationshipToInsuredIsSelf"); //self
                    AddBlock(page, 41.6m, 13.4m, 2, ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsSpouseOf), "Field06_PatientRelationshipToInsuredIsSpouseOf"); //spouse
                    AddBlock(page, 45.1m, 13.4m, 2, ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsChildOf), "Field06_PatientRelationshipToInsuredIsChildOf"); //child
                    AddBlock(page, 49.5m, 13.4m, 2, ConditionalMarker(hcfa.Field06_PatientRelationshipToInsuredIsOther), "Field06_PatientRelationshipToInsuredIsOther"); // other
                    AddBlock(page, 52.5m, 13.3m, 30, hcfa.Field07_InsuredsAddress_Street, "Field07_InsuredsAddress_Street");

                    // LINE 4
                    AddBlock(page, 10, 15, 25, hcfa.Field05_PatientsAddress_City, "Field05_PatientsAddress_City");
                    AddBlock(page, 31.4m, 15, 3.5m, hcfa.Field05_PatientsAddress_State, "Field05_PatientsAddress_State");

                    // Field 8 unused in CMS-1500
                    AddBlock(page, 35, 15, 20, hcfa.Field08_Reserved, "Field08_Reserved");

                    AddBlock(page, 52.5m, 15, 23, hcfa.Field07_InsuredsAddress_City, "Field07_InsuredsAddress_City");
                    AddBlock(page, 72.9m, 15, 6, hcfa.Field07_InsuredsAddress_State, "Field07_InsuredsAddress_State");

                    // LINE 5
                    AddBlock(page, 10, 16.8m, 13, hcfa.Field05_PatientsAddress_Zip, "Field05_PatientsAddress_Zip");
                    AddBlock(page, 21.6m, 16.8m, 14.5m, hcfa.Field05_PatientsTelephone, "Field05_PatientsTelephone");
                    
                    AddBlock(page, 52.6m, 16.7m, 12, hcfa.Field07_InsuredsAddress_Zip, "Field07_InsuredsAddress_Zip");
                    AddBlock(page, 68.7m, 16.7m, 14.5m, hcfa.Field07_InsuredsPhoneNumber, "Field07_InsuredsPhoneNumber");
                    AddBlock(page, 65.3m, 16.7m, 14.5m, hcfa.Field07_InsuredsAreaCode, "Field07_InsuredsAreaCode");
                    
                    // LINE 6
                    AddBlock(page, 10, 18.4m, 28.5m, hcfa.Field09_OtherInsuredsName, "Field09_OtherInsuredsName");
                    AddBlock(page, 53, 18.5m, 30, hcfa.Field11_InsuredsPolicyGroupOfFECANumber, "Field11_InsuredsPolicyGroupOfFECANumber");

                    // LINE 7
                    AddBlock(page, 10, 20.1m, 28.5m, hcfa.Field09a_OtherInsuredsPolicyOrGroup, "Field09a_OtherInsuredsPolicyOrGroup");
                    AddBlock(page, 39, 20.3m, 2, ConditionalMarker(hcfa.Field10a_PatientConditionRelatedToEmployment), "Field10a_PatientConditionRelatedToEmploymentYes");
                    AddBlock(page, 44.2m, 20.3m, 2, ConditionalMarker(!hcfa.Field10a_PatientConditionRelatedToEmployment), "Field10a_PatientConditionRelatedToEmploymentNo");
                    AddBlock(page, 55, 20.3m, 3, hcfa.Field11a_InsuredsDateOfBirth.MM, "Field11a_InsuredsDateOfBirthMM");
                    AddBlock(page, 57.7m, 20.3m, 3, hcfa.Field11a_InsuredsDateOfBirth.DD, "Field11a_InsuredsDateOfBirthDD");
                    AddBlock(page, 61, 20.3m, 3, hcfa.Field11a_InsuredsDateOfBirth.YY, "Field11a_InsuredsDateOfBirthYY");
                    AddBlock(page, 67.6m, 20.3m, 2, ConditionalMarker(hcfa.Field11a_InsuredsSexIsMale), "Field11a_InsuredsSexIsMale", TextAlignEnum.center);
                    AddBlock(page, 73.7m, 20.3m, 2, ConditionalMarker(hcfa.Field11a_InsuredsSexIsFemale), "Field11a_InsuredsSexIsFemale", TextAlignEnum.center);

                    // LINE 8
                    AddBlock(page, 10, 21.9m, 28.5m, hcfa.Field09b_Reserved, "Field09b_Reserved");

                    AddBlock(page, 39, 22, 2, ConditionalMarker(hcfa.Field10b_PatientConditionRelatedToAutoAccident), "Field10b_PatientConditionRelatedToAutoAccidentYes");
                    AddBlock(page, 44.2m, 22, 2, ConditionalMarker(!hcfa.Field10b_PatientConditionRelatedToAutoAccident), "Field10b_PatientConditionRelatedToAutoAccidentNo");
                    AddBlock(page, 47.8m, 22, 2.5m, hcfa.Field10b_PatientConditionRelToAutoAccidentState, "Field10b_PatientConditionRelToAutoAccidentState");
                    AddBlock(page, 53, 21.9m, 30, hcfa.Field11b_OtherClaimId, "Field11b_OtherClaimId");

                    // LINE 9
                    AddBlock(page, 10, 23.7m, 28.5m, hcfa.Field09c_Reserved, "Field09c_Reserved");
                    AddBlock(page, 39, 23.7m, 2, ConditionalMarker(hcfa.Field10c_PatientConditionRelatedToOtherAccident), "Field10c_PatientConditionRelatedToOtherAccidentYes");
                    AddBlock(page, 44.2m, 23.7m, 2, ConditionalMarker(!hcfa.Field10c_PatientConditionRelatedToOtherAccident), "Field10c_PatientConditionRelatedToOtherAccidentNo");
                    AddBlock(page, 53, 23.7m, 30, hcfa.Field11c_InsuredsPlanOrProgramName, "Field11c_InsuredsPlanOrProgramName");

                    // LINE 10
                    AddBlock(page, 10, 25.4m, 28.5m, hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName, "Field09d_OtherInsuredsInsurancePlanNameOrProgramName");
                    AddBlock(page, 35.5m, 25.4m, 20, hcfa.Field10d_ReservedForLocalUse, "Field10d_ReservedForLocalUse");
                    AddBlock(page, 53.8m, 25.5m, 2, ConditionalMarker(hcfa.Field11d_IsThereOtherHealthBenefitPlan), "Field11d_IsThereOtherHealthBenefitPlanYes");
                    AddBlock(page, 58.2m, 25.5m, 2, ConditionalMarker(!hcfa.Field11d_IsThereOtherHealthBenefitPlan), "Field11d_IsThereOtherHealthBenefitPlanNo"); 

                    // LINE 11
                    AddBlock(page, 9, 28.7m, 25, hcfa.Field12_PatientsOrAuthorizedSignature, "Field12_PatientsOrAuthorizedSignature", TextAlignEnum.center);
                    AddBlock(page, 39, 28.7m, 14, hcfa.Field12_PatientsOrAuthorizedSignatureDate.ToString(), "Field12_PatientsOrAuthorizedSignatureDate", TextAlignEnum.center);
                    AddBlock(page, 54.5m, 28.7m, 24, hcfa.Field13_InsuredsOrAuthorizedSignature, "Field13_InsuredsOrAuthorizedSignature", TextAlignEnum.center);

                    // LINE 12
                    if (hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy != null)
                    {
                        AddBlock(page, 10.4m, 30.65m, 3, hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy.MM, "Field14_DateOfCurrentIllnessInjuryOrPregnancyMM");
                        AddBlock(page, 12.9m, 30.65m, 3, hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy.DD, "Field14_DateOfCurrentIllnessInjuryOrPregnancyDD");
                        AddBlock(page, 16.2m, 30.65m, 3, hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy.YY, "Field14_DateOfCurrentIllnessInjuryOrPregnancyYY");
                    }

                    // Field 15 
                    if (hcfa.Field15_DatePatientHadSameOrSimilarIllness != null) {
                        AddBlock(page, 40.9m, 30.65m, 3, hcfa.Field15_DatePatientHadSameOrSimilarIllness.MM, "Field15_DatePatientHadSameOrSimilarIllnessMM");
                        AddBlock(page, 43.6m, 30.65m, 3, hcfa.Field15_DatePatientHadSameOrSimilarIllness.DD, "Field15_DatePatientHadSameOrSimilarIllnessDD");
                        AddBlock(page, 46.8m, 30.65m, 3, hcfa.Field15_DatePatientHadSameOrSimilarIllness.YY, "Field15_DatePatientHadSameOrSimilarIllnessYY");
                    }

                    if (hcfa.Field16_DatePatientUnableToWork_Start != null)
                    {
                        AddBlock(page, 55.7m, 30.65m, 3, hcfa.Field16_DatePatientUnableToWork_Start.MM, "Field16_DatePatientUnableToWork_StartMM");
                        AddBlock(page, 58.4m, 30.65m, 3, hcfa.Field16_DatePatientUnableToWork_Start.DD, "Field16_DatePatientUnableToWork_StartDD");
                        AddBlock(page, 61.8m, 30.65m, 3, hcfa.Field16_DatePatientUnableToWork_Start.YY, "Field16_DatePatientUnableToWork_StartYY");
                    }

                    if (hcfa.Field16_DatePatientUnableToWork_End != null)
                    {
                        AddBlock(page, 67.9m, 30.65m, 3, hcfa.Field16_DatePatientUnableToWork_End.MM, "Field16_DatePatientUnableToWork_EndMM");
                        AddBlock(page, 70.7m, 30.65m, 3, hcfa.Field16_DatePatientUnableToWork_End.DD, "Field16_DatePatientUnableToWork_EndDD");
                        AddBlock(page, 74, 30.65m, 3, hcfa.Field16_DatePatientUnableToWork_End.YY, "Field16_DatePatientUnableToWork_EndYY");
                    }

                    // LINE 13
                    AddBlock(page, 10, 32.3m, 26, hcfa.Field17_ReferringProviderOrOtherSource_Name, "Field17_ReferringProviderOrOtherSource_Name");
                    AddBlock(page, 34.5m, 31.5m, 3, hcfa.Field17a_OtherID_Qualifier, "Field17a_OtherID_Qualifier");
                    AddBlock(page, 37, 31.5m, 16, hcfa.Field17a_OtherID_Number, "Field17a_OtherID_Number");
                    AddBlock(page, 37, 32.4m, 16, hcfa.Field17b_NationalProviderIdentifier, "Field17b_NationalProviderIdentifier");

                    // Field 18
                    if (hcfa.Field18_HospitalizationDateFrom != null) {
                        AddBlock(page, 55.7m, 32.3m, 3, hcfa.Field18_HospitalizationDateFrom.MM, "Field18_HospitalizationDateFromMM");
                        AddBlock(page, 58.4m, 32.3m, 3, hcfa.Field18_HospitalizationDateFrom.DD, "Field18_HospitalizationDateFromDD");
                        AddBlock(page, 61.8m, 32.3m, 3, hcfa.Field18_HospitalizationDateFrom.YY, "Field18_HospitalizationDateFromYY");
                    }

                    if (hcfa.Field18_HospitalizationDateTo != null) {
                        AddBlock(page, 67.9m, 32.3m, 3, hcfa.Field18_HospitalizationDateTo.MM, "Field18_HospitalizationDateToMM");
                        AddBlock(page, 70.7m, 32.3m, 3, hcfa.Field18_HospitalizationDateTo.DD, "Field18_HospitalizationDateToDD");
                        AddBlock(page, 74, 32.3m, 3, hcfa.Field18_HospitalizationDateTo.YY, "Field18_HospitalizationDateToYY");
                    }

                    // LINE 14
                    // We limit the length of the remark to only the size of the block. 
                    if (hcfa.Field19_AdditionalClaimInfo != null && hcfa.Field19_AdditionalClaimInfo.Length > 58)
                        AddBlock(page, 10, 34.1m, 49, hcfa.Field19_AdditionalClaimInfo.Substring(0, 58), "Field19_AdditionalClaimInfo");
                    else
                        AddBlock(page, 10, 34.1m, 49, hcfa.Field19_AdditionalClaimInfo, "Field19_AdditionalClaimInfo");

                    AddBlock(page, 53.9m, 34.1m, 2, ConditionalMarker(hcfa.Field20_OutsideLab), "Field20_OutsideLabYes");
                    AddBlock(page, 58.3m, 34.1m, 2, ConditionalMarker(!hcfa.Field20_OutsideLab), "Field20_OutsideLabNo");
                    AddBlock(page, 64.1m, 34.1m, 9, hcfa.Field20_OutsideLab ? Convert.ToString(hcfa.Field20_OutsideLabCharges) : "", "Field20_OutsideLabCharges", TextAlignEnum.right);

                    // Line 15
                    AddBlock(page, 53, 35.8m, 11, hcfa.Field22_MedicaidSubmissionCode, "Field22_MedicaidSubmissionCode");
                    AddBlock(page, 63, 35.8m, 18, hcfa.Field22_OriginalReferenceNumber, "Field22_OriginalReferenceNumber");

                    // Line 16
                    AddBlock(page, 53, 37.5m, 30, hcfa.Field23_PriorAuthorizationNumber, "Field23_PriorAuthorizationNumber");

                    // DX CODES
                    AddBlock(page, 11, 35.8m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisA), "Field21_DiagnosisA");
                    AddBlock(page, 22.2m, 35.8m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisB), "Field21_DiagnosisB");
                    AddBlock(page, 33.6m, 35.8m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisC), "Field21_DiagnosisC");
                    AddBlock(page, 45.1m, 35.85m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisD), "Field21_DiagnosisD");
                    AddBlock(page, 11, 36.7m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisE), "Field21_DiagnosisE");
                    AddBlock(page, 22.2m, 36.7m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisF), "Field21_DiagnosisF");
                    AddBlock(page, 33.6m, 36.7m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisG), "Field21_DiagnosisG");
                    AddBlock(page, 45.1m, 36.75m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisH), "Field21_DiagnosisH");
                    AddBlock(page, 11, 37.6m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisI), "Field21_DiagnosisI");
                    AddBlock(page, 22.2m, 37.6m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisJ), "Field21_DiagnosisJ");
                    AddBlock(page, 33.6m, 37.6m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisK), "Field21_DiagnosisK");
                    AddBlock(page, 45.1m, 37.6m, 8, PointerNumbericToAlpha(hcfa.Field21_DiagnosisL), "Field21_DiagnosisL");

                }

                // Render service lines
                decimal y = 40 + 1.71m * (i % 6);
                var line = hcfa.Field24_ServiceLines[i];
                string lineNumber = ((i % 6) + 1).ToString();

                AddBlock(page, 10, y + 0.11m, 60, line.CommentLine, string.Format("Field24{0}_Comment", lineNumber));
                AddBlock(page, 65.2m, y + 0.11m, 3, line.RenderingProviderIdQualifier, string.Format("Field24{0}_RenderingProviderIdQualifier", lineNumber));
                AddBlock(page, 68, y + 0.11m, 12, line.RenderingProviderId, string.Format("Field24{0}_RenderingProviderId", lineNumber));

                if (line.DateFrom != null)
                {
                    AddBlock(page, 9.4m, y + 1, 3, line.DateFrom.MM, string.Format("Field24{0}_DateFrom", lineNumber));
                    AddBlock(page, 11.9m, y + 1, 3, line.DateFrom.DD, string.Format("Field24{0}_DateFrom", lineNumber));
                    AddBlock(page, 14.5m, y + 1, 3, line.DateFrom.YY, string.Format("Field24{0}_DateFrom", lineNumber));
                }

                if (line.DateTo != null)
                {
                    AddBlock(page, 17.2m, y + 1, 3, line.DateTo.MM, string.Format("Field24{0}_DateTo", lineNumber));
                    AddBlock(page, 19.8m, y + 1, 3, line.DateTo.DD, string.Format("Field24{0}_DateTo", lineNumber));
                    AddBlock(page, 22.3m, y + 1, 3, line.DateTo.YY, string.Format("Field24{0}_DateTo", lineNumber));
                }

                AddBlock(page, 25, y + 1, 3, line.PlaceOfService, string.Format("Field24{0}_PlaceOfService", lineNumber));
                AddBlock(page, 28, y + 1, 2, line.EmergencyIndicator, string.Format("Field24{0}_EmergencyIndicator", lineNumber));
                AddBlock(page, 29, y + 1, 6, line.ProcedureCode, string.Format("Field24{0}_ProcedureCode", lineNumber));
                AddBlock(page, 36.8m, y + 1, 3, line.Mod1, string.Format("Field24{0}_Mod1", lineNumber));
                AddBlock(page, 39.8m, y + 1, 3, line.Mod2, string.Format("Field24{0}_Mod2", lineNumber));
                AddBlock(page, 42.5m, y + 1, 3, line.Mod3, string.Format("Field24{0}_Mod3", lineNumber));
                AddBlock(page, 45.1m, y + 1, 3, line.Mod4, string.Format("Field24{0}_Mod4", lineNumber));
                AddBlock(page, 47.6m, y + 1, 2, line.DiagnosisPointerA, string.Format("Field24{0}_DiagnosisPointerA", lineNumber));
                AddBlock(page, 48.6m, y + 1, 2, line.DiagnosisPointerB, string.Format("Field24{0}_DiagnosisPointerB", lineNumber));
                AddBlock(page, 49.6m, y + 1, 2, line.DiagnosisPointerC, string.Format("Field24{0}_DiagnosisPointerC", lineNumber));
                AddBlock(page, 50.6m, y + 1, 2, line.DiagnosisPointerD, string.Format("Field24{0}_DiagnosisPointerD", lineNumber));
                AddBlock(page, 51, y + 1, 9, String.Format("{0:0.00}", line.Charges).Replace(".", " "), string.Format("Field24{0}_Charges", lineNumber), TextAlignEnum.right);
                AddBlock(page, 59.3m, y + 1, 4, String.Format("{0}", line.DaysOrUnits), string.Format("Field24{0}_DaysOrUnits", lineNumber), TextAlignEnum.right);
                AddBlock(page, 63.4m, y + 1, 2, line.EarlyPeriodicScreeningDiagnosisAndTreatment, string.Format("Field24{0}_EarlyPeriodicScreeningDiagnosisAndTreatment", lineNumber));
                AddBlock(page, 68, y + 1, 12, line.RenderingProviderNpi, string.Format("Field24{0}_RenderingProviderNpi", lineNumber));



                if (i % 6 == 5 || i == hcfa.Field24_ServiceLines.Count - 1) // Footer
                {
                    // Render footer
                    AddBlock(page, 10, 51.2m, 15, hcfa.Field25_FederalTaxIDNumber, "");

                    if (hcfa.Field25_IsSSN)
                        AddBlock(page, 23.4m, 51.35m, 2, "X", "Field25_FederalTaxIDNumberIsSSN");
                    if (hcfa.Field25_IsEIN)
                        AddBlock(page, 25.1m, 51.35m, 2, "X", "Field25_FederalTaxIDNumberIsEIN");

                    AddBlock(page, 29, 51.2m, 14, hcfa.Field26_PatientAccountNumber, "Field26_PatientAccountNumber");

                    if (hcfa.Field27_AcceptAssignment.HasValue)
                    {
                        if (hcfa.Field27_AcceptAssignment.Value == true)
                        AddBlock(page, 41.75m, 51.35m, 2, "X", "Field27_AcceptAssignmentYes");
                        else
                        AddBlock(page, 46, 51.35m, 2, "X", "Field27_AcceptAssignmentNo");
                    }

                    AddBlock(page, 53, 51.2m, 9, String.Format("{0:0.00}", hcfa.Field28_TotalCharge).Replace(".", " "), "Field28_TotalCharge", TextAlignEnum.right);
                    AddBlock(page, 61.5m, 51.2m, 9, String.Format("{0:0.00}", hcfa.Field29_AmountPaid).Replace(".", " "), "Field29_AmountPaid", TextAlignEnum.right);
                    AddBlock(page, 69.5m, 51.2m, 9, String.Format("{0:0.00}", hcfa.Field30_BalanceDue).Replace(".", " "), "Field30_BalanceDue", TextAlignEnum.right);

                    // Box 31
                    if (hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile.HasValue) {
                        if (hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile.Value == true)
                            AddBlock(page, 9.8m, 55, 21, "SIGNATURE IS ON FILE", "Field31_PhysicianOrSupplierSignatureIsOnFile", TextAlignEnum.left);
                        else
                            AddBlock(page, 9.8m, 55, 21, "SIGNATURE NOT ON FILE", "Field31_PhysicianOrSupplierSignatureIsOnFile", TextAlignEnum.left);
                    }

                    // Box 32
                    AddBlock(page, 28.7m, 53, 27, hcfa.Field32_ServiceFacilityLocation_Name, "Field32_ServiceFacilityLocation_Name");
                    AddBlock(page, 28.7m, 53.65m, 27, hcfa.Field32_ServiceFacilityLocation_Street, "Field32_ServiceFacilityLocation_Street");
                    AddBlock(page, 28.7m, 54.4m, 27, String.Format("{0}, {1} {2}", hcfa.Field32_ServiceFacilityLocation_City, hcfa.Field32_ServiceFacilityLocation_State, hcfa.Field32_ServiceFacilityLocation_Zip), "Field32_ServiceFacilityLocation_CityStateZip");
                    AddBlock(page, 29.3m, 55.65m, 10, hcfa.Field32a_ServiceFacilityLocation_Npi, "Field32a_ServiceFacilityLocation_Npi");
                    AddBlock(page, 39.2m, 55.65m, 15, hcfa.Field32b_ServiceFacilityLocation_OtherID, "Field32b_ServiceFacilityLocation_OtherID");

                    // Box 33
                    AddBlock(page, 66.1m, 52.2m, 27, hcfa.Field33_BillingProvider_TelephoneNumber, "Field33_BillingProvider_TelephoneNumber"); 
                    AddBlock(page, 52.4m, 53, 27, hcfa.Field33_BillingProvider_Name, "Field33_BillingProvider_Name");
                    AddBlock(page, 52.4m, 53.65m, 27, hcfa.Field33_BillingProvider_Street, "Field33_BillingProvider_Street");
                    AddBlock(page, 52.4m, 54.4m, 27, String.Format("{0}, {1} {2}", hcfa.Field33_BillingProvider_City, hcfa.Field33_BillingProvider_State, hcfa.Field33_BillingProvider_Zip), "Field33_BillingProvider_CityStateZip");
                    AddBlock(page, 53.2m, 55.65m, 10, hcfa.Field33a_BillingProvider_Npi, "Field33a_BillingProvider_Npi");
                    AddBlock(page, 62.9m, 55.65m, 15, hcfa.Field33b_BillingProvider_OtherID, "Field33b_BillingProvider_OtherID");

                }

            }

            return pages;
        }

        public virtual List<FormPage> TransformHcfa1500ToClaimFormFoXml(HCFA1500Claim hcfa) {
            return TransformHcfa1500ToFormPages(hcfa);
        }

        public virtual List<FormPage> TransformClaimToClaimFormFoXml(Claim claim)
        {
            HCFA1500Claim hcfa = TransformClaimToHcfa1500(claim);

            return TransformHcfa1500ToFormPages(hcfa);
        }

        protected string PointerNumbericToAlpha(string pointer) {
            if (string.IsNullOrWhiteSpace(pointer)) {
                return string.Empty;
            }

            string p = pointer.Trim();

            switch (p) {
                case "1":
                    p = "A";
                    break;
                case "2":
                    p = "B";
                    break;
                case "3":
                    p = "C";
                    break;
                case "4":
                    p = "D";
                    break;
                case "5":
                    p = "E";
                    break;
                case "6":
                    p = "F";
                    break;
                case "7":
                    p = "G";
                    break;
                case "8":
                    p = "H";
                    break;
                case "9":
                    p = "I";
                    break;
            }

            return p;
        }
    }
}
