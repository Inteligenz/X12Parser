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

            hcfa.Field01_TypeOfCoverageIsChampVa = false;
            hcfa.Field01_TypeOfCoverageIsFECABlkLung = false;
            hcfa.Field01_TypeOfCoverageIsGroupHealthPlan = false;
            hcfa.Field01_TypeOfCoverageIsMedicaid = false;
            hcfa.Field01_TypeOfCoverageIsMedicare = false;
            hcfa.Field01_TypeOfCoverageIsOther = false;
            hcfa.Field01_TypeOfCoverageIsOther = false;
            hcfa.Field01_TypeOfCoverageIsTricareChampus = false;

            hcfa.Field01a_InsuredsIDNumber = string.Empty;

            ClaimMember patient = claim.Patient ?? claim.Subscriber;
            // Patient Name
            hcfa.Field02_PatientsName = String.Format("{0}, {1} {2}", patient.Name.LastName, patient.Name.FirstName, patient.Name.MiddleName).Trim();

            // patient birthdate
            hcfa.Field03_PatientsDateOfBirth = new FormDate
            {
                MM = String.Format("{0:MM}", patient.DateOfBirth),
                DD = String.Format("{0:dd}", patient.DateOfBirth),
                YY = String.Format("{0:yy}", patient.DateOfBirth)
            };
            if (patient.Gender == GenderEnum.Male)
            {
                hcfa.Field03_PatientsSexFemale = false;
                hcfa.Field03_PatientsSexMale = true;
            }
            else if (patient.Gender == GenderEnum.Female)
            {
                hcfa.Field03_PatientsSexFemale = true;
                hcfa.Field03_PatientsSexMale = false;
            }

            var subscriberName = claim.Subscriber.Name;

            hcfa.Field04_InsuredsName = String.Format("{0}, {1} {2}", subscriberName.LastName, subscriberName.FirstName, subscriberName.MiddleName);

            // Patient Address
            if (patient.Address != null)
            {
                hcfa.Field05_PatientsAddress_Street = String.Format("{0} {1}", patient.Address.Line1, patient.Address.Line2).TrimEnd();
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

            hcfa.Field08_PatientStatusIsEmployed = false;
            hcfa.Field08_PatientStatusIsFullTimeStudent = false;
            hcfa.Field08_PatientStatusIsMarried = false;
            hcfa.Field08_PatientStatusIsOther = false;
            hcfa.Field08_PatientStatusIsPartTimeStudent = false;
            hcfa.Field08_PatientStatusIsSingle = false;

            hcfa.Field09_OtherInsuredsName = string.Empty;
            hcfa.Field09a_OtherInsuredsPolicyOrGroup = string.Empty;
            hcfa.Field09b_OtherInsuredIsFemale = false;
            hcfa.Field09b_OtherInsuredIsMale = false;
            hcfa.Field09b_OtherInsuredsDateOfBirth = new Forms.FormDate();
            hcfa.Field09c_OtherInsuredsEmployerNameOrSchoolName = string.Empty;
            hcfa.Field09d_OtherInsuredsInsurancePlanNameOrProgramName = string.Empty;

            hcfa.Field10a_PatientConditionRelatedToEmployment = false;
            hcfa.Field10b_PatientConditionRelatedToAutoAccident = false;
            hcfa.Field10b_PatientConditionRelToAutoAccidentState = string.Empty;
            hcfa.Field10c_PatientConditionRelatedToOtherAccident = false;
            hcfa.Field10d_ReservedForLocalUse = string.Empty;

            hcfa.Field11_InsuredsPolicyGroupOfFECANumber = string.Empty;
            hcfa.Field11a_InsuredsDateOfBirth = new FormDate();
            hcfa.Field11a_InsuredsSexIsFemale = false;
            hcfa.Field11a_InsuredsSexIsMale = false;
            hcfa.Field11b_InsuredsEmployerOrSchool = string.Empty;
            hcfa.Field11c_InsuredsPlanOrProgramName = string.Empty;
            hcfa.Field11d_IsThereOtherHealthBenefitPlan = false;

            hcfa.Field12_PatientsOrAuthorizedSignature = string.Empty;
            hcfa.Field12_PatientsOrAuthorizedSignatureDate = new FormDate();

            hcfa.Field13_InsuredsOrAuthorizedSignature = string.Empty;

            hcfa.Field14_DateOfCurrentIllnessInjuryOrPregnancy = new FormDate();

            hcfa.Field15_DatePatientHadSameOrSimilarIllness = new FormDate();

            hcfa.Field16_DatePatientUnableToWork_End = new FormDate();
            hcfa.Field16_DatePatientUnableToWork_Start = new FormDate();

            hcfa.Field17_ReferringProviderOrOtherSource_Credentials = string.Empty;
            hcfa.Field17_ReferringProviderOrOtherSource_FirstName = string.Empty;
            hcfa.Field17_ReferringProviderOrOtherSource_LastName = string.Empty;
            hcfa.Field17_ReferringProviderOrOtherSource_MiddleName = string.Empty;
            hcfa.Field17a_OtherID_Number = string.Empty;
            hcfa.Field17a_OtherID_Qualifier = string.Empty;
            hcfa.Field17b_NationalProviderIdentifier = string.Empty;


            // Admission date and hour
            hcfa.Field18_HospitalizationDateFrom = new FormDate();
            hcfa.Field18_HospitalizationDateTo = new FormDate();

            hcfa.Field19_ReservedForLocalUse = string.Empty;

            hcfa.Field20_OutsideLab = false;
            hcfa.Field20_OutsideLabCharges = 0;

            // Diagnosis codes
            if (claim.Diagnoses.Count >= 1)
                hcfa.Field21_Diagnosis1 = claim.Diagnoses[0].Code;
            if (claim.Diagnoses.Count >= 2)
                hcfa.Field21_Diagnosis1 = claim.Diagnoses[1].Code;
            if (claim.Diagnoses.Count >= 3)
                hcfa.Field21_Diagnosis1 = claim.Diagnoses[2].Code;
            if (claim.Diagnoses.Count >= 4)
                hcfa.Field21_Diagnosis1 = claim.Diagnoses[3].Code;

            
            hcfa.Field22_MedicaidSubmissionCode = string.Empty;
            hcfa.Field22_OriginalReferenceNumber = string.Empty;

            hcfa.Field23_PriorAuthorizationNumber = string.Empty;

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
                hcfaLine.EarlyPeriodicScreeningDiagnosisAndTreatment = string.Empty;

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
            hcfa.Field33_BillingProvider_Name = claim.PayToProvider.Name.LastName;
            hcfa.Field33_BillingProvider_Street = claim.PayToProvider.Address.Line1;
            hcfa.Field33_BillingProvider_City = claim.PayToProvider.Address.City;
            hcfa.Field33_BillingProvider_State = claim.PayToProvider.Address.StateCode;
            hcfa.Field33_BillingProvider_Zip = claim.PayToProvider.Address.PostalCode;
            return hcfa;
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text)
        {
            return AddBlock(page, x, y, width, text, TextAlignEnum.left);
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
                    AddBlock(page, 4, 7, 2, "X");
                    AddBlock(page, 11, 7, 2, "X");
                    AddBlock(page, 18, 7, 2, "X");
                    AddBlock(page, 27, 7, 2, "X");
                    AddBlock(page, 34, 7, 2, "X");
                    AddBlock(page, 42, 7, 2, "X");
                    AddBlock(page, 48, 7, 2, "X");
                    AddBlock(page, 53, 7, 30, "INSURED'S ID NUMBER");

                    // LINE 2
                    AddBlock(page, 4, 9, 28.5m, hcfa.Field02_PatientsName);
                    AddBlock(page, 34, 9, 3, hcfa.Field03_PatientsDateOfBirth.MM);
                    AddBlock(page, 37, 9, 3, hcfa.Field03_PatientsDateOfBirth.DD);
                    AddBlock(page, 40, 9, 3, hcfa.Field03_PatientsDateOfBirth.YY);
                    if (hcfa.Field03_PatientsSexMale)
                        AddBlock(page, 44.5m, 9, 2.5m, "X", TextAlignEnum.center);
                    if (hcfa.Field03_PatientsSexFemale)
                        AddBlock(page, 49.5m, 9, 2.5m, "X", TextAlignEnum.center);
                    AddBlock(page, 53, 9, 30, hcfa.Field04_InsuredsName);

                    // LINE 3
                    AddBlock(page, 4, 11, 28.5m, hcfa.Field05_PatientsAddress_Street);
                    AddBlock(page, 36, 11, 2, "X");
                    AddBlock(page, 41, 11, 2, "X");
                    AddBlock(page, 45, 11, 2, "X");
                    AddBlock(page, 50, 11, 2, "X");
                    AddBlock(page, 53, 11, 30, "INSURED'S ADDRESS");

                    // LINE 4
                    AddBlock(page, 4, 13, 25, hcfa.Field05_PatientsAddress_City);
                    AddBlock(page, 29, 13, 3.5m, hcfa.Field05_PatientsAddress_State);

                    AddBlock(page, 38, 13, 2, "X");
                    AddBlock(page, 44, 13, 2, "X");
                    AddBlock(page, 50, 13, 2, "X");

                    AddBlock(page, 53, 13, 23, "CITY");
                    AddBlock(page, 77, 13, 6, "TX");

                    // LINE 5
                    AddBlock(page, 4, 15, 13, hcfa.Field05_PatientsAddress_Zip);
                    AddBlock(page, 18, 15, 14.5m, "888 555-1234"); // hcfa.Field05_PatientsTelephone);
                    AddBlock(page, 38, 15, 2, "X");
                    AddBlock(page, 44, 15, 2, "X");
                    AddBlock(page, 50, 15, 2, "X");
                    AddBlock(page, 53, 15, 12, "ZIP CODE");
                    AddBlock(page, 68.5m, 15, 14.5m, "888 555-1234");

                    // LINE 6
                    AddBlock(page, 4, 17, 28.5m, "OTHER INSURED'S NAME");
                    AddBlock(page, 53, 17, 30, "INSURED'S POLICY GROUP");

                    // LINE 7
                    AddBlock(page, 4, 19, 28.5m, "OTHER INSURED'S POLICY");
                    AddBlock(page, 38, 19, 2, "X");
                    AddBlock(page, 44, 19, 2, "X");
                    AddBlock(page, 56, 19, 3, "MM");
                    AddBlock(page, 59, 19, 3, "DD");
                    AddBlock(page, 62, 19, 3, "YY");
                    AddBlock(page, 71.25m, 19, 2, "X");
                    AddBlock(page, 78.5m, 19, 2, "X");

                    // LINE 8
                    AddBlock(page, 5, 21, 3, "MM");
                    AddBlock(page, 8, 21, 3, "DD");
                    AddBlock(page, 11, 21, 3, "YY");
                    AddBlock(page, 21, 21, 2, "X");
                    AddBlock(page, 27, 21, 2, "X");
                    AddBlock(page, 38, 21, 2, "X");
                    AddBlock(page, 44, 21, 2, "X");
                    AddBlock(page, 48, 21, 2.5m, "TX");
                    AddBlock(page, 53, 21, 30, "EMPLOYER'S NAME OR SCHOOL NAME");

                    // LINE 9
                    AddBlock(page, 4, 23, 28.5m, "EMPLOYER'S NAME OR SCHOOL NAME");
                    AddBlock(page, 38, 23, 2, "X");
                    AddBlock(page, 44, 23, 2, "X");
                    AddBlock(page, 53, 23, 30, "INSURANCE PLAN NAME OR PROGRAM NAME");

                    // LINE 10
                    AddBlock(page, 4, 25, 28.5m, "INSURANCE PLAN NAME OR PROGRAM NAME");
                    AddBlock(page, 33, 25, 20, "RSERVED FOR LOCAL USE");
                    AddBlock(page, 55, 25, 2, "X");
                    AddBlock(page, 60, 25, 2, "X");

                    // LINE 11
                    AddBlock(page, 9, 29, 25, "SIGNATURE ON FILE", TextAlignEnum.center);
                    AddBlock(page, 39, 29, 14, "MM/DD/YY", TextAlignEnum.center);
                    AddBlock(page, 59, 29, 24, "SIGNATURE ON FILE", TextAlignEnum.center);

                    // LINE 12
                    AddBlock(page, 5, 31, 3, "MM");
                    AddBlock(page, 8, 31, 3, "DD");
                    AddBlock(page, 11, 31, 3, "YY");

                    AddBlock(page, 40, 31, 3, "MM");
                    AddBlock(page, 43, 31, 3, "DD");
                    AddBlock(page, 46, 31, 3, "YY");

                    AddBlock(page, 57, 31, 3, "MM");
                    AddBlock(page, 60, 31, 3, "DD");
                    AddBlock(page, 63, 31, 3, "YY");

                    AddBlock(page, 71, 31, 3, "MM");
                    AddBlock(page, 74, 31, 3, "DD");
                    AddBlock(page, 77, 31, 3, "YY");

                    // LINE 13
                    AddBlock(page, 4, 33, 26, "NAME OF REFERRING PROVIDER");
                    AddBlock(page, 33, 32, 3, "QUA");
                    AddBlock(page, 36, 32, 16, "REFERRING PROVIDER ID");
                    AddBlock(page, 36, 33, 16, "NPI");

                    AddBlock(page, 57, 33, 3, "MM");
                    AddBlock(page, 60, 33, 3, "DD");
                    AddBlock(page, 63, 33, 3, "YY");

                    AddBlock(page, 71, 33, 3, "MM");
                    AddBlock(page, 74, 33, 3, "DD");
                    AddBlock(page, 77, 33, 3, "YY");

                    // LINE 14
                    AddBlock(page, 4, 35, 49, "RESERVED FOR LOCAL USE");
                    AddBlock(page, 55, 35, 2, "X");
                    AddBlock(page, 60, 35, 2, "X");
                    AddBlock(page, 65, 35, 9, "00.00", TextAlignEnum.right);
                    AddBlock(page, 74, 35, 9, "00.00", TextAlignEnum.right);

                    // Line 15
                    AddBlock(page, 6.5m, 37, 8, "123 45");
                    AddBlock(page, 33.5m, 37, 8, "123 45");
                    AddBlock(page, 53, 37, 11, "MC RESUB CODE");
                    AddBlock(page, 65, 37, 18, "ORIGINAL REF NO");

                    // Line 16
                    AddBlock(page, 6.5m, 39, 8, "123 45");
                    AddBlock(page, 33.5m, 39, 8, "123 45");
                    AddBlock(page, 53, 39, 30, "PRIOR AUTHORIZATION");

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
                    AddBlock(page, 69, 56, 27, "888 555-1234");
                    AddBlock(page, 53, 57, 27, "BILLING PROVIDER INFO 1");
                    AddBlock(page, 53, 58, 27, "BILLING PROVIDER INFO 2");
                    AddBlock(page, 53, 59, 27, "BILLING PROVIDER INFO 3");
                    AddBlock(page, 54, 60, 10, "1234567890");
                    AddBlock(page, 65, 60, 15, "123456789012345");

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
