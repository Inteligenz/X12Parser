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

            String indicatorCode = null;
            if (claim != null &&
                claim.SubscriberInformation != null &&
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

            if (!string.IsNullOrEmpty(patient.MemberId))
            {
                hcfa.Field01a_InsuredsIDNumber = patient.MemberId;
            }
            else if (patient != null &&
                patient.Name != null &&
                patient.Name.Identification != null &&
                patient.Name.Identification.Id != null)
            {
                hcfa.Field01a_InsuredsIDNumber = patient.Name.Identification.Id;
            }
            else if (!string.IsNullOrEmpty(subscriber.MemberId))
            {
                hcfa.Field01a_InsuredsIDNumber = subscriber.MemberId;
            }
            else if (subscriber != null &&
                subscriber.Name != null &&
                subscriber.Name.Identification != null &&
                subscriber.Name.Identification.Id != null)
            {
                hcfa.Field01a_InsuredsIDNumber = subscriber.Name.Identification.Id;
            }
            
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
            String patientRelationship = "";
            if (claim.SubscriberInformation != null)
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
                        hcfa.Field01_TypeOfCoverageIsOther = true;
                    break;
            }

            if (subscriber.Address != null)
            {
                hcfa.Field07_InsuredsAddress_City = subscriber.Address.City;
                hcfa.Field07_InsuredsAddress_State = subscriber.Address.StateCode;
                hcfa.Field07_InsuredsAddress_Street = subscriber.Address.Line1;
                hcfa.Field07_InsuredsAddress_Zip = subscriber.Address.PostalCode;
            }

            // Not present on 837P
            hcfa.Field07_InsuredsAreaCode =  String.Empty;
            hcfa.Field07_InsuredsPhoneNumber = string.Empty;

            // Not present on 837P
            hcfa.Field08_PatientStatusIsEmployed = false;
            hcfa.Field08_PatientStatusIsFullTimeStudent = false;
            hcfa.Field08_PatientStatusIsMarried = false;
            hcfa.Field08_PatientStatusIsOther = false;
            hcfa.Field08_PatientStatusIsPartTimeStudent = false;
            hcfa.Field08_PatientStatusIsSingle = false;

            var otherSubscriber = claim.OtherSubscriberInformations.FirstOrDefault();
            
            // No way to get below three fields using 837P
            hcfa.Field09b_OtherInsuredIsFemale = false;
            hcfa.Field09b_OtherInsuredIsMale = false;
            hcfa.Field09b_OtherInsuredsDateOfBirth = new FormDate();

            if (otherSubscriber != null)
            {
                hcfa.Field09_OtherInsuredsName = otherSubscriber.Name.Formatted();
                hcfa.Field09a_OtherInsuredsPolicyOrGroup = otherSubscriber.SubscriberInformation.ReferenceIdentification;
                hcfa.Field09b_OtherInsuredsDateOfBirth = formatFormDate(otherSubscriber.DateOfBirth);
                hcfa.Field09b_OtherInsuredIsFemale = otherSubscriber.Gender == GenderEnum.Female;
                hcfa.Field09b_OtherInsuredIsMale = otherSubscriber.Gender == GenderEnum.Male;
                hcfa.Field09c_OtherInsuredsEmployerNameOrSchoolName = otherSubscriber.OtherPayer.LastName; // XXX: OK to assume org in last name?
                hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName = otherSubscriber.OtherPayer.Identification.Id;
            }

            hcfa.Field10a_PatientConditionRelatedToEmployment = claim.RelatedCauseCode1 == "AA" || claim.RelatedCauseCode2 == "AA" || claim.RelatedCauseCode3 == "AA";
            hcfa.Field10b_PatientConditionRelatedToAutoAccident = claim.RelatedCauseCode1 == "EM" || claim.RelatedCauseCode2 == "EM" || claim.RelatedCauseCode3 == "EM";
            hcfa.Field10c_PatientConditionRelatedToOtherAccident = claim.RelatedCauseCode1 == "AB" || claim.RelatedCauseCode1 == "AP" || claim.RelatedCauseCode1 == "OA" ||
                                                                   claim.RelatedCauseCode2 == "AB" || claim.RelatedCauseCode2 == "AP" || claim.RelatedCauseCode2 == "OA" ||
                                                                   claim.RelatedCauseCode3 == "AB" || claim.RelatedCauseCode3 == "AP" || claim.RelatedCauseCode3 == "OA";
            hcfa.Field10b_PatientConditionRelToAutoAccidentState = claim.AutoAccidentState;
            hcfa.Field10d_ReservedForLocalUse = string.Empty;

            hcfa.Field11_InsuredsPolicyGroupOfFECANumber = string.Empty;
            hcfa.Field11a_InsuredsDateOfBirth = formatFormDate(subscriber.DateOfBirth);
            hcfa.Field11a_InsuredsSexIsFemale = subscriber.Gender == GenderEnum.Female;
            hcfa.Field11a_InsuredsSexIsMale = subscriber.Gender == GenderEnum.Male;
            hcfa.Field11b_InsuredsEmployerOrSchool = string.Empty;
            hcfa.Field11c_InsuredsPlanOrProgramName = string.Empty;

            hcfa.Field11d_IsThereOtherHealthBenefitPlan = otherSubscriber != null;

            hcfa.Field12_PatientsOrAuthorizedSignature = string.Empty;
            switch (claim.PatientSignatureSourceCode)
            {
                case "B":
                case "C":
                case "M":
                case "S":
                    hcfa.Field12_PatientsOrAuthorizedSignature = "Signature on file";
                    break;
                case "P":
                    hcfa.Field12_PatientsOrAuthorizedSignature = "Signature generated by provider";
                    break;
            }

            hcfa.Field12_PatientsOrAuthorizedSignatureDate = new FormDate();

            hcfa.Field13_InsuredsOrAuthorizedSignature = string.Empty;


            var onsetDate = claim.Dates.FirstOrDefault(dr => dr.Qualifier == "431");
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
            

            hcfa.Field19_ReservedForLocalUse = string.Empty;

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
                hcfa.Field22_MedicaidSubmissionCode = string.Empty;

            var originalRef = claim.Identifications.FirstOrDefault(id => id.Qualifier == "F8");

            if (originalRef != null)
                hcfa.Field22_OriginalReferenceNumber = originalRef.Id;
            else
                hcfa.Field22_OriginalReferenceNumber = string.Empty;

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
            if (!string.IsNullOrWhiteSpace(claim.PayToProvider.TaxId))
            {
                hcfa.Field25_FederalTaxIDNumber = claim.PayToProvider.TaxId;
                if (claim.PayToProvider.Identifications.Exists(id=>id.Qualifier == "EI"))
                    hcfa.Field25_IsEIN = true;
                if (claim.PayToProvider.Identifications.Exists(id => id.Qualifier == "SY"))
                    hcfa.Field25_IsSSN = true;
            }
            else
            {
                hcfa.Field25_FederalTaxIDNumber = claim.BillingProvider.TaxId;
                if (claim.BillingProvider.Identifications.Exists(id => id.Qualifier == "EI"))
                    hcfa.Field25_IsEIN = true;
                if (claim.BillingProvider.Identifications.Exists(id => id.Qualifier == "SY"))
                    hcfa.Field25_IsSSN = true;
            }
                
            // shouldnt we represent hcfa.Field25_IsSSN and Field25_IsEIN to know which type TaxID?
            hcfa.Field26_PatientAccountNumber = claim.PatientControlNumber;

            if (claim.ProviderAcceptAssignmentCode == "A" || claim.ProviderAcceptAssignmentCode == "B")
                hcfa.Field27_AcceptAssignment = true;
            else if (claim.ProviderAcceptAssignmentCode == "C")
                hcfa.Field27_AcceptAssignment = false;

            hcfa.Field28_TotalCharge = claim.TotalClaimChargeAmount;

            hcfa.Field29_AmountPaid = claim.PatientAmountPaid;

            hcfa.Field30_BalanceDue = null; // does not exist on 837P

            if (claim.ProviderSignatureOnFile == "Y")
                hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile = true;
            else if (claim.ProviderSignatureOnFile == "N")
                hcfa.Field31_PhysicianOrSupplierSignatureIsOnFile = false;

            // Service Location
            var serviceLocation = claim.ServiceLocation;
            hcfa.Field32_ServiceFacilityLocation_Name = serviceLocation.Name.LastName;
            hcfa.Field32_ServiceFacilityLocation_Street = serviceLocation.Address.Line1;
            hcfa.Field32_ServiceFacilityLocation_City = serviceLocation.Address.City;
            hcfa.Field32_ServiceFacilityLocation_State = serviceLocation.Address.StateCode;
            hcfa.Field32_ServiceFacilityLocation_Zip = serviceLocation.Address.PostalCode;
            hcfa.Field32a_ServiceFacilityLocation_Npi = serviceLocation.Npi;
            if (serviceLocation.Identifications.Count > 0)
                hcfa.Field32b_ServiceFacilityLocation_OtherID = serviceLocation.Identifications.First().Id;
            // Pay To Provider
            hcfa.Field33_BillingProvider_Name = claim.BillingProvider.Name.LastName;
            hcfa.Field33_BillingProvider_Street = claim.BillingProvider.Address.Line1;
            hcfa.Field33_BillingProvider_City = claim.BillingProvider.Address.City;
            hcfa.Field33_BillingProvider_State = claim.BillingProvider.Address.StateCode;
            hcfa.Field33_BillingProvider_Zip = claim.BillingProvider.Address.PostalCode;
            hcfa.Field33a_BillingProvider_Npi = claim.BillingProvider.Npi;
            return hcfa;
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
                    AddBlock(page, 53, 17, 30, hcfa.Field09a_OtherInsuredsPolicyOrGroup);

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
                    AddBlock(page, 53, 21, 30, hcfa.Field09c_OtherInsuredsEmployerNameOrSchoolName);

                    // LINE 9
                    AddBlock(page, 4, 23, 28.5m, hcfa.Field09c_OtherInsuredsEmployerNameOrSchoolName);
                    AddBlock(page, 38, 23, 2, ConditionalMarker(hcfa.Field10c_PatientConditionRelatedToOtherAccident));
                    AddBlock(page, 44, 23, 2, ConditionalMarker(!hcfa.Field10c_PatientConditionRelatedToOtherAccident));
                    AddBlock(page, 53, 23, 30, hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName);

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

                AddBlock(page, 4, y + 1, 3, line.DateFrom.MM);
                AddBlock(page, 7, y + 1, 3, line.DateFrom.DD);
                AddBlock(page, 10, y + 1, 3, line.DateFrom.YY);
                AddBlock(page, 13, y + 1, 3, line.DateTo.MM);
                AddBlock(page, 16, y + 1, 3, line.DateTo.DD);
                AddBlock(page, 19, y + 1, 3, line.DateTo.YY);
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
