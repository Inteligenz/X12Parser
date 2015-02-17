using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Common;
using OopFactory.X12.Hipaa.Claims.Forms;
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public class InstitutionalClaimToUB04ClaimFormTransformation : IClaimToClaimFormTransfomation
    {
        private string _formImagePath;
        public InstitutionalClaimToUB04ClaimFormTransformation(string formImagePath)
        {
            _formImagePath = formImagePath;
            PerPageTotalChargesView = false;
        }

        public bool PerPageTotalChargesView
        {
            get;
            set;
        }

        /// <summary>
        /// Implementation of mapping as described at http://ahca.myflorida.com/Medicaid/meds/pdf/837i_v2-1_crosswalk_v2.pdf
        /// Detailed instructions found at https://www.cms.gov/transmittals/downloads/R1104CP.pdf
        /// </summary>
        /// <param name="claim"></param>
        /// <returns></returns>
        public virtual UB04Claim TransformClaimToUB04(Claim claim)
        {
            var ub = new UB04Claim();
            Provider provider = claim.BillingProvider;
            SetBillingProviderAddressDetails(ub, provider, claim.SubmitterInfo);

            if (claim.PayToProvider != null && 
                claim.PayToProvider.Name != null &&
                claim.PayToProvider.Name.ToString() != provider.Name.ToString() &&
                claim.PayToProvider.Address != null && provider.Address != null &&
                claim.PayToProvider.Address.Line1 != provider.Address.Line1)
            {
                provider = claim.PayToProvider;
                ub.Field02_PayToProvider.Line1 = provider.Name.ToString();
                ub.Field02_PayToProvider.Line2 = provider.Address.Line1;

                if (string.IsNullOrWhiteSpace(provider.Address.Line2))
                    ub.Field02_PayToProvider.Line3 = provider.Address.Locale;
                else
                {
                    ub.Field02_PayToProvider.Line3 = provider.Address.Line2;
                    ub.Field02_PayToProvider.Line4 = provider.Address.Locale;
                }
            }

            ub.Field03a_PatientControlNumber = claim.PatientControlNumber;
            ub.Field03b_MedicalRecordNumber = claim.MedicalRecordNumber;
            ub.Field04_TypeOfBill = claim.BillTypeCode;
            if (claim.PayToProvider != null)
            {
                ub.Field05_FederalTaxId = claim.PayToProvider.TaxId;
            }
            if (claim.StatementFromDate != null)
            {
                ub.Field06_StatementCoversPeriod.FromDate = String.Format("{0:MMddyy}", claim.StatementFromDate);
            }
            if (claim.StatementToDate != null)
            {
                ub.Field06_StatementCoversPeriod.ThroughDate = String.Format("{0:MMddyy}", claim.StatementToDate);
            }

            ClaimMember patient = claim.Patient ?? claim.Subscriber;

            if (patient.Name != null &&
                patient.Name.Identification != null)
            {
                ub.Field08_PatientName_a = patient.Name.Identification.Id;
                ub.Field08_PatientName_b = patient.Name.ToString();
            }
            if (patient.Address != null)
            {
                string streetAddress;
                if (string.IsNullOrEmpty(patient.Address.Line2))
                {
                    streetAddress = patient.Address.Line1;
                }
                else
                {
                    streetAddress = string.Concat(patient.Address.Line1, ",", patient.Address.Line2);
                }
                if (streetAddress.Length > 48)
                {
                    ub.Field09_PatientAddress.a_Street = streetAddress.Substring(0, 48);
                }
                else
                {
                    ub.Field09_PatientAddress.a_Street = streetAddress;
                }
                ub.Field09_PatientAddress.b_City = patient.Address.City;
                ub.Field09_PatientAddress.c_State = patient.Address.StateCode;
                ub.Field09_PatientAddress.d_PostalCode = patient.Address.PostalCode;
                ub.Field09_PatientAddress.e_CountryCode = patient.Address.CountryCode;
            }
            if (patient.DateOfBirth != null)
            {
                ub.Field10_Birthdate = String.Format("{0:MMddyyyy}", patient.DateOfBirth);
            }
            ub.Field11_Sex = patient.Gender.ToString().Substring(0, 1);
            if (claim.AdmissionDate != null)
            {
                ub.Field12_AdmissionDate = String.Format("{0:MMddyy}", claim.AdmissionDate);
                ub.Field13_AdmissionHour = String.Format("{0:HH}", claim.AdmissionDate);
            }
            if (claim.AdmissionType != null)
            {
                ub.Field14_AdmissionType = claim.AdmissionType.Code;
            }
            if (claim.AdmissionSource != null)
            {
                ub.Field15_AdmissionSource = claim.AdmissionSource.Code;
            }
            if (claim.DischargeTime != null)
            {
                ub.Field16_DischargeHour = String.Format("{0:HH}", claim.DischargeTime);
            }
            if (claim.PatientStatus != null)
            {
                ub.Field17_DischargeStatus = claim.PatientStatus.Code;
            }
            if (claim.Conditions != null)
            {
                if (claim.Conditions.Count > 0) ub.Field18_ConditionCode01 = claim.Conditions[0].Code;
                if (claim.Conditions.Count > 1) ub.Field19_ConditionCode02 = claim.Conditions[1].Code;
                if (claim.Conditions.Count > 2) ub.Field20_ConditionCode03 = claim.Conditions[2].Code;
                if (claim.Conditions.Count > 3) ub.Field21_ConditionCode04 = claim.Conditions[3].Code;
                if (claim.Conditions.Count > 4) ub.Field22_ConditionCode05 = claim.Conditions[4].Code;
                if (claim.Conditions.Count > 5) ub.Field23_ConditionCode06 = claim.Conditions[5].Code;
                if (claim.Conditions.Count > 6) ub.Field24_ConditionCode07 = claim.Conditions[6].Code;
                if (claim.Conditions.Count > 7) ub.Field25_ConditionCode08 = claim.Conditions[7].Code;
                if (claim.Conditions.Count > 8) ub.Field26_ConditionCode09 = claim.Conditions[8].Code;
                if (claim.Conditions.Count > 9) ub.Field27_ConditionCode10 = claim.Conditions[9].Code;
                if (claim.Conditions.Count > 10) ub.Field28_ConditionCode11 = claim.Conditions[10].Code;
            }
            foreach (var identification in claim.Identifications)
            {
                if (identification.Qualifier != null && identification.Qualifier == "LU" && identification.Id != null)
                {
                    ub.Field29_AccidentState = identification.Id;
                }
            }

            if (claim.Occurrences != null)
            {
                if (claim.Occurrences.Count > 0) ub.Field31a_Occurrence.CopyFrom(claim.Occurrences[0]);
                if (claim.Occurrences.Count > 1) ub.Field31b_Occurrence.CopyFrom(claim.Occurrences[1]);
                if (claim.Occurrences.Count > 2) ub.Field32a_Occurrence.CopyFrom(claim.Occurrences[2]);
                if (claim.Occurrences.Count > 3) ub.Field32b_Occurrence.CopyFrom(claim.Occurrences[3]);
                if (claim.Occurrences.Count > 4) ub.Field33a_Occurrence.CopyFrom(claim.Occurrences[4]);
                if (claim.Occurrences.Count > 5) ub.Field33b_Occurrence.CopyFrom(claim.Occurrences[5]);
                if (claim.Occurrences.Count > 6) ub.Field34a_Occurrence.CopyFrom(claim.Occurrences[6]);
                if (claim.Occurrences.Count > 7) ub.Field34b_Occurrence.CopyFrom(claim.Occurrences[7]);
            }

            List<UB04OccurrenceSpan> spans = new List<UB04OccurrenceSpan>();

            if (claim.Occurrences != null)
            {
                if (claim.Occurrences.Count > 8) spans.Add(new UB04OccurrenceSpan().CopyFrom(claim.Occurrences[8]));
                if (claim.Occurrences.Count > 9) spans.Add(new UB04OccurrenceSpan().CopyFrom(claim.Occurrences[9]));
            }
            if (claim.OccurrenceSpans != null)
            {
                foreach (CodedDateRange span in claim.OccurrenceSpans)
                    spans.Add(new UB04OccurrenceSpan().CopyFrom(span));
            }

            if (spans.Count > 0) ub.Field35a_OccurrenceSpan = spans[0];
            if (spans.Count > 1) ub.Field35b_OccurrenceSpan = spans[1];
            if (spans.Count > 2) ub.Field36a_OccurrenceSpan = spans[2];
            if (spans.Count > 3) ub.Field36b_OccurrenceSpan = spans[3];

            List<string> blockLines = new List<string>();
            if (claim.Subscriber.Name != null)
            {
                blockLines.Add(claim.Subscriber.Name.ToString());
            }
            ub.Field38_ResponsibleParty.Line1 = blockLines[0];
            if (claim.Subscriber.Address != null)
            {
                blockLines.Add(claim.Subscriber.Address.Line1);

                if (!string.IsNullOrWhiteSpace(claim.Subscriber.Address.Line2))
                    blockLines.Add(claim.Subscriber.Address.Line2);
                blockLines.Add(claim.Subscriber.Address.Locale);
                if (blockLines.Count > 1)
                {
                    ub.Field38_ResponsibleParty.Line2 = blockLines[1];
                }
                if (blockLines.Count > 2)
                {
                    ub.Field38_ResponsibleParty.Line3 = blockLines[2];
                }
            }

            if (blockLines.Count > 3)
            {
                ub.Field38_ResponsibleParty.Line4 = blockLines[3];
            }

            if (claim.Values != null)
            {
                if (claim.Values.Count > 0) ub.Field39a_Value.CopyFrom(claim.Values[0]);
                if (claim.Values.Count > 1) ub.Field39b_Value.CopyFrom(claim.Values[1]);
                if (claim.Values.Count > 2) ub.Field39c_Value.CopyFrom(claim.Values[2]);
                if (claim.Values.Count > 3) ub.Field39d_Value.CopyFrom(claim.Values[3]);
                if (claim.Values.Count > 4) ub.Field40a_Value.CopyFrom(claim.Values[4]);
                if (claim.Values.Count > 5) ub.Field40b_Value.CopyFrom(claim.Values[5]);
                if (claim.Values.Count > 6) ub.Field40c_Value.CopyFrom(claim.Values[6]);
                if (claim.Values.Count > 7) ub.Field40d_Value.CopyFrom(claim.Values[7]);
                if (claim.Values.Count > 8) ub.Field41a_Value.CopyFrom(claim.Values[8]);
                if (claim.Values.Count > 9) ub.Field41b_Value.CopyFrom(claim.Values[9]);
                if (claim.Values.Count > 10) ub.Field41c_Value.CopyFrom(claim.Values[10]);
                if (claim.Values.Count > 11) ub.Field41d_Value.CopyFrom(claim.Values[11]);
            }

            foreach (var line in claim.ServiceLines)
            {
                ub.ServiceLines.Add(new UB04ServiceLine {
                    Field42_RevenueCode = line.RevenueCode, 
                    Field43_Description = line.RevenueCodeDescription,
                    Field44_ProcedureCodes = SetProcedureCodeWithModifiers(line.Procedure),
                    Field45_ServiceDate = line.ServiceDateFrom > DateTime.MinValue ? String.Format("{0:MMddyy}", line.ServiceDateFrom) : "",
                    Field46_ServiceUnits = line.Quantity.ToString(),
                    Field47_TotalCharges = line.ChargeAmount,
                    Field48_NonCoveredCharges = line.NonCoveredChargeAmount
                });
            }
            ub.Field47_Line23_TotalCharges = claim.TotalClaimChargeAmount;
            ub.Field48_Line23_NonCoveredCharges = claim.ServiceLines.Sum(sl => sl.NonCoveredChargeAmount);
            if (claim.BillingProvider != null)
            {
                ub.Field56_NationalProviderIdentifier = claim.BillingProvider.Npi;
                if (string.IsNullOrEmpty(claim.BillingProvider.Npi))
                {
                    if (claim.BillingProvider.Identifications.Count >= 1)
                        ub.Field57_OtherProviderIdA = claim.BillingProvider.Identifications[0].Id;
                    if (claim.BillingProvider.Identifications.Count >= 2)
                        ub.Field57_OtherProviderIdB = claim.BillingProvider.Identifications[1].Id;
                    if (claim.BillingProvider.Identifications.Count >= 3)
                        ub.Field57_OtherProviderIdC = claim.BillingProvider.Identifications[2].Id;
                }
            }

            SetCurrentPayer(claim, ub);
            if (claim.OtherSubscriberInformations.Count > 0)
            {
                var subscriber = claim.OtherSubscriberInformations[0];
                SetOtherPayers(subscriber, ub);
            }
            if (claim.OtherSubscriberInformations.Count > 1)
            {
                var subscriber = claim.OtherSubscriberInformations[1];
                SetOtherPayers(subscriber, ub);
            }
            
            var controlNumbers = claim.Identifications.Where(id => (new string[] {"F8","D9","9A","9C","LX"}).Contains(id.Qualifier)).ToList();
            if (controlNumbers.Count > 0)
                ub.Field64A_DocumentControlNumber = controlNumbers[0].Id;
            if (controlNumbers.Count > 1)
                ub.Field64B_DocumentControlNumber = controlNumbers[1].Id;
            if (controlNumbers.Count > 2)
                ub.Field64C_DocumentControlNumber = controlNumbers[2].Id;

            if (claim.Diagnoses.FirstOrDefault(d => d.Version == CodeListEnum.ICD9) != null)
                ub.Field66_Version = "9";
            if (claim.Diagnoses.FirstOrDefault(d => d.Version == CodeListEnum.ICD10) != null)
                ub.Field66_Version = "0";

            var principalDiagnosis = claim.Diagnoses.FirstOrDefault(d => d.DiagnosisType == DiagnosisTypeEnum.Principal);
            if (principalDiagnosis != null)
                ub.Field67_PrincipleDiagnosis.CopyFrom(principalDiagnosis);

            var otherDiagnoses = claim.Diagnoses.Where(d => d.DiagnosisType == DiagnosisTypeEnum.Other).ToList();
            if (otherDiagnoses.Count > 0) ub.Field67A_Diagnosis.CopyFrom(otherDiagnoses[0]);
            if (otherDiagnoses.Count > 1) ub.Field67B_Diagnosis.CopyFrom(otherDiagnoses[1]);
            if (otherDiagnoses.Count > 2) ub.Field67C_Diagnosis.CopyFrom(otherDiagnoses[2]);
            if (otherDiagnoses.Count > 3) ub.Field67D_Diagnosis.CopyFrom(otherDiagnoses[3]);
            if (otherDiagnoses.Count > 4) ub.Field67E_Diagnosis.CopyFrom(otherDiagnoses[4]);
            if (otherDiagnoses.Count > 5) ub.Field67F_Diagnosis.CopyFrom(otherDiagnoses[5]);
            if (otherDiagnoses.Count > 6) ub.Field67G_Diagnosis.CopyFrom(otherDiagnoses[6]);
            if (otherDiagnoses.Count > 7) ub.Field67H_Diagnosis.CopyFrom(otherDiagnoses[7]);
            if (otherDiagnoses.Count > 8) ub.Field67I_Diagnosis.CopyFrom(otherDiagnoses[8]);
            if (otherDiagnoses.Count > 9) ub.Field67J_Diagnosis.CopyFrom(otherDiagnoses[9]);
            if (otherDiagnoses.Count > 10) ub.Field67K_Diagnosis.CopyFrom(otherDiagnoses[10]);
            if (otherDiagnoses.Count > 11) ub.Field67L_Diagnosis.CopyFrom(otherDiagnoses[11]);
            if (otherDiagnoses.Count > 12) ub.Field67M_Diagnosis.CopyFrom(otherDiagnoses[12]);
            if (otherDiagnoses.Count > 13) ub.Field67N_Diagnosis.CopyFrom(otherDiagnoses[13]);
            if (otherDiagnoses.Count > 14) ub.Field67O_Diagnosis.CopyFrom(otherDiagnoses[14]);
            if (otherDiagnoses.Count > 15) ub.Field67P_Diagnosis.CopyFrom(otherDiagnoses[15]);
            if (otherDiagnoses.Count > 16) ub.Field67Q_Diagnosis.CopyFrom(otherDiagnoses[16]);

            var admittingDiagnosis = claim.Diagnoses.FirstOrDefault(d => d.DiagnosisType == DiagnosisTypeEnum.Admitting);
            if (admittingDiagnosis != null)
                ub.Field69_AdmittingDiagnosisCode.CopyFrom(admittingDiagnosis);
            var patientReasonDiagnoses = claim.Diagnoses.Where(d => d.DiagnosisType == DiagnosisTypeEnum.PatientReason).ToList();
            if (patientReasonDiagnoses.Count > 0) ub.Field70a_PatientReasonDiagnosisCode.CopyFrom(patientReasonDiagnoses[0]);
            if (patientReasonDiagnoses.Count > 1) ub.Field70b_PatientReasonDiagnosisCode.CopyFrom(patientReasonDiagnoses[1]);
            if (patientReasonDiagnoses.Count > 2) ub.Field70c_PatientReasonDiagnosisCode.CopyFrom(patientReasonDiagnoses[2]);

            if (claim.DiagnosisRelatedGroup != null)
                ub.Field71_PPSCode = claim.DiagnosisRelatedGroup.Code;

            var causes = claim.Diagnoses.Where(d => d.DiagnosisType == DiagnosisTypeEnum.ExternalCauseOfInjury).ToList();
            if (causes.Count > 0) ub.Field72a_ExternalCauseOfInjury.CopyFrom(causes[0]);
            if (causes.Count > 1) ub.Field72b_ExternalCauseOfInjury.CopyFrom(causes[1]);
            if (causes.Count > 2) ub.Field72c_ExternalCauseOfInjury.CopyFrom(causes[2]);

            var principalProcedure = claim.Procedures.FirstOrDefault(p => p.IsPrincipal);
            if (principalProcedure != null)
                ub.Field74_PrincipalProcedure.CopyFrom(principalProcedure);
            var otherProcedures = claim.Procedures.Where(p => !p.IsPrincipal).ToList();
            if (otherProcedures.Count > 0) ub.Field74a_OtherProcedure.CopyFrom(otherProcedures[0]);
            if (otherProcedures.Count > 1) ub.Field74b_OtherProcedure.CopyFrom(otherProcedures[1]);
            if (otherProcedures.Count > 2) ub.Field74c_OtherProcedure.CopyFrom(otherProcedures[2]);
            if (otherProcedures.Count > 3) ub.Field74d_OtherProcedure.CopyFrom(otherProcedures[3]);
            if (otherProcedures.Count > 4) ub.Field74e_OtherProcedure.CopyFrom(otherProcedures[4]);

            if (claim.AttendingProvider != null)
            {
                ub.Field76_AttendingPhysician.Npi = claim.AttendingProvider.Npi;
                if (claim.AttendingProvider.Name != null)
                {
                    ub.Field76_AttendingPhysician.LastName = claim.AttendingProvider.Name.LastName;
                    ub.Field76_AttendingPhysician.FirstName = claim.AttendingProvider.Name.FirstName;
                }
                var id = claim.AttendingProvider.Identifications.FirstOrDefault();
                if (id != null)
                {
                    ub.Field76_AttendingPhysician.IdentifierQualifier = id.Qualifier;
                    ub.Field76_AttendingPhysician.Identifier = id.Id;
                }
            }

            if (claim.OperatingPhysician != null)
            {
                ub.Field77_OperatingPhysician.Npi = claim.OperatingPhysician.Npi;
                if (claim.OperatingPhysician.Name != null)
                {
                    ub.Field77_OperatingPhysician.LastName = claim.OperatingPhysician.Name.LastName;
                    ub.Field77_OperatingPhysician.FirstName = claim.OperatingPhysician.Name.FirstName;
                }
                var id = claim.OperatingPhysician.Identifications.FirstOrDefault();
                if (id != null)
                {
                    ub.Field77_OperatingPhysician.IdentifierQualifier = id.Qualifier;
                    ub.Field77_OperatingPhysician.Identifier = id.Id;
                }
            }

            if (claim.OtherOperatingPhysician != null)
            {
                if (claim.RenderingProvider != null && claim.ReferringProvider != null)
                {
                    SetOtherProviders(claim.RenderingProvider, ub.Field78_OtherProvider);
                    SetOtherProviders(claim.ReferringProvider, ub.Field79_OtherProvider);
                }
                else
                {
                    SetOtherProviders(claim.OtherOperatingPhysician, ub.Field78_OtherProvider);
                    if (claim.RenderingProvider != null)
                    {
                        SetOtherProviders(claim.RenderingProvider, ub.Field79_OtherProvider);
                    }
                    if (claim.ReferringProvider != null)
                    {
                        SetOtherProviders(claim.ReferringProvider, ub.Field79_OtherProvider);
                    }
                }
            }
            else
            {
                if (claim.RenderingProvider != null)
                {
                    SetOtherProviders(claim.RenderingProvider, ub.Field78_OtherProvider);
                    if (claim.ReferringProvider != null)
                    {
                        SetOtherProviders(claim.ReferringProvider, ub.Field79_OtherProvider);
                    }
                }
                else
                {
                    if (claim.ReferringProvider != null)
                    {
                        SetOtherProviders(claim.ReferringProvider, ub.Field78_OtherProvider);
                    }
                }
            }
            
            if (claim.Notes != null)
            {
                List<string> remarksList = null;
                if (claim.Notes.Count != 0)
                {
                    if (claim.Notes.Count == 1)
                    {
                        remarksList = GetRemarksLineByLine(claim.Notes[0].Description);
                    }
                    else if (claim.Notes.Count == 2)
                    {
                        remarksList = GetRemarksLineByLine(string.Concat(claim.Notes[0].Description, "   ", claim.Notes[1].Description));
                    }
                    if (remarksList.Count > 0)
                    {
                        ub.Field80_Remarks.Line1 = remarksList[0];
                    }
                    if (remarksList.Count > 1)
                    {
                        ub.Field80_Remarks.Line2 = remarksList[1];
                    }
                    if (remarksList.Count > 2)
                    {
                        ub.Field80_Remarks.Line3 = remarksList[2];
                    }
                }

            }
            if (claim.ProviderInfo != null)
            {
                ub.Field81a.Qualifier = "B3";
                ub.Field81a.Code1 = claim.ProviderInfo.Id;
            }

			LimitFieldWidths(ub);

            return ub;
        }

        private void LimitFieldWidths(UB04Claim ub)
        {
            ub.Field02_PayToProvider.Line1 = SetStringLength(ub.Field02_PayToProvider.Line1, 28);
            ub.Field02_PayToProvider.Line2 = SetStringLength(ub.Field02_PayToProvider.Line2, 28);
            ub.Field02_PayToProvider.Line3 = SetStringLength(ub.Field02_PayToProvider.Line3, 28);
            ub.Field02_PayToProvider.Line4 = SetStringLength(ub.Field02_PayToProvider.Line4, 28);
            ub.Field03b_MedicalRecordNumber = SetStringLength(ub.Field03b_MedicalRecordNumber, 28);
            ub.Field05_FederalTaxId = SetStringLength(ub.Field05_FederalTaxId, 10);
            ub.Field08_PatientName_a = SetStringLength(ub.Field08_PatientName_a, 21);
            ub.Field08_PatientName_b = SetStringLength(ub.Field08_PatientName_b, 33);
            ub.Field09_PatientAddress.b_City = SetStringLength(ub.Field09_PatientAddress.b_City, 37);
            ub.Field29_AccidentState = SetStringLength(ub.Field29_AccidentState, 2);
            ub.Field38_ResponsibleParty.Line1 = SetStringLength(ub.Field38_ResponsibleParty.Line1, 49);
            ub.Field38_ResponsibleParty.Line2 = SetStringLength(ub.Field38_ResponsibleParty.Line2, 49);
            ub.Field38_ResponsibleParty.Line3 = SetStringLength(ub.Field38_ResponsibleParty.Line3, 49);
            ub.Field38_ResponsibleParty.Line4 = SetStringLength(ub.Field38_ResponsibleParty.Line4, 49);
            ub.Field56_NationalProviderIdentifier = SetStringLength(ub.Field56_NationalProviderIdentifier, 14);
            ub.Field76_AttendingPhysician.Npi = SetStringLength(ub.Field76_AttendingPhysician.Npi, 11);
            ub.Field76_AttendingPhysician.LastName = SetStringLength(ub.Field76_AttendingPhysician.LastName, 18);
            ub.Field76_AttendingPhysician.FirstName = SetStringLength(ub.Field76_AttendingPhysician.FirstName, 12);
            ub.Field76_AttendingPhysician.Identifier = SetStringLength(ub.Field76_AttendingPhysician.Identifier, 10);
            ub.Field77_OperatingPhysician.Npi = SetStringLength(ub.Field77_OperatingPhysician.Npi, 11);
            ub.Field77_OperatingPhysician.LastName = SetStringLength(ub.Field77_OperatingPhysician.LastName, 18);
            ub.Field77_OperatingPhysician.FirstName = SetStringLength(ub.Field77_OperatingPhysician.FirstName, 12);
            ub.Field77_OperatingPhysician.Identifier = SetStringLength(ub.Field77_OperatingPhysician.Identifier, 10);

            foreach (UB04ServiceLine line in ub.ServiceLines)
            {
                line.Field43_Description = SetStringLength(line.Field43_Description, 29);
            }
        }


        private void SetOtherProviders(Provider provider, UB04Provider ub04Provider)
        {
            ub04Provider.Npi = SetStringLength(provider.Npi, 11);
            if (provider.Name != null)
            {
                ub04Provider.LastName = SetStringLength(provider.Name.LastName, 18);
                ub04Provider.FirstName = SetStringLength(provider.Name.FirstName, 12);
                if (provider.Name.Type != null && provider.Name.Type.Identifier != null)
                {
                    ub04Provider.ProviderQualifier = provider.Name.Type.Identifier;
                }
            }
            var id = provider.Identifications.FirstOrDefault();
            if (id != null)
            {
                ub04Provider.IdentifierQualifier = id.Qualifier;
                ub04Provider.Identifier = SetStringLength(id.Id, 10);
            }
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

        private void SetBillingProviderAddressDetails(UB04Claim ub, Provider provider,SubmitterInfo submitterinfo)
        {
            if (provider == null || provider.Address == null)
            {
                return;
            }

            string billingProviderAddress;
            if (string.IsNullOrEmpty(provider.Address.Line2))
            {
                billingProviderAddress = provider.Address.Line1;
            }
            else
            {
                billingProviderAddress = string.Concat(provider.Address.Line1, ",", provider.Address.Line2);
            }
            ub.Field01_BillingProvider.Line1 = SetStringLength(provider.Name.ToString(), 28);
            ub.Field01_BillingProvider.Line2 = SetStringLength(billingProviderAddress, 28);
            ub.Field01_BillingProvider.Line3 = SetStringLength(provider.Address.Locale, 28);
            if (provider.Contacts.Count > 0 && provider.Contacts[0].Numbers.Count > 0 )
            {
                ub.Field01_BillingProvider.Line4 = provider.Contacts[0].Numbers[0].Number;
            }
            if (string.IsNullOrEmpty(ub.Field01_BillingProvider.Line4))
            {
                if (submitterinfo != null && 
                    submitterinfo.Providers != null && 
                    submitterinfo.Providers.Contacts.Count > 0 && 
                    submitterinfo.Providers.Contacts[0].Numbers.Count > 0)
                {
                    ub.Field01_BillingProvider.Line4 = submitterinfo.Providers.Contacts[0].Numbers[0].Number;
                }
            }
        }

        private List<string> GetRemarksLineByLine(string remark)
        {
            List<string> remarksList = new List<string>();
            try
            {
                while (remark.Length > 27)
                {
                    int index = remark.LastIndexOf(' ', 27);
                    if (index == -1)
                    {
                        index = 26;
                        remarksList.Add(remark.Substring(0, index + 1));
                        remark = remark.Substring(index + 1, remark.Length - (index + 1));
                        continue;
                    }
                    remarksList.Add(remark.Substring(0, index));
                    remark = remark.Substring(index + 1, remark.Length - (index + 1));
                }
                remarksList.Add(remark);
            }
            catch (Exception e)
            {
                
            }
            return remarksList;
        }

        private void SetOtherPayers(OtherSubscriberInformation subscriber, UB04Claim ub)
        {
            if (subscriber == null ||
                subscriber.SubscriberInformation == null)
            {
                return;
            }

            switch (subscriber.SubscriberInformation.PayerResponsibilitySequenceNumberCode)
            {
                case "P":
                {
                    if (subscriber.OtherPayer != null)
                    {
                        ub.PayerA_Primary.Field50_PayerName = SetStringLength(subscriber.OtherPayer.Formatted(), 26);
                        ub.PayerA_Primary.Field51_HealthPlanId = SetStringLength(subscriber.OtherPayer.Identification.Id, 17);
                        if (!string.IsNullOrEmpty(subscriber.OtherPayer.PriorAuthorizationNumber))
                        {
                            ub.Field63A_TreatmentAuthorizationCode = SetStringLength(subscriber.OtherPayer.PriorAuthorizationNumber, 34);
                        }
                    }
                    ub.PayerA_Primary.Field52_ReleaseOfInfoCertIndicator = subscriber.ReleaseOfInformationCode;
                    ub.PayerA_Primary.Field53_AssignmentOfBenefitsCertIndicator = subscriber.BenefitsAssignmentCertificationIndicator;
                    ub.PayerA_Primary.Field54_PriorPayments = subscriber.PayorPaidAmount;
                    ub.PayerA_Primary.Field55_EstimatedAmountDue = subscriber.RemainingPatientLiability;

                    ub.PayerA_Primary.Field58_InsuredsName = SetStringLength(subscriber.Name.Formatted(), 29);
                    ub.PayerA_Primary.Field59_PatientRelationship = subscriber.SubscriberInformation.IndividualRelationshipCode;
                    ub.PayerA_Primary.Field60_InsuredsUniqueId = SetStringLength(subscriber.Name.Identification.Id, 23);
                    ub.PayerA_Primary.Field61_GroupName = SetStringLength(subscriber.SubscriberInformation.Name, 17);
                    ub.PayerA_Primary.Field62_InsuredsGroupNumber = SetStringLength(subscriber.SubscriberInformation.ReferenceIdentification, 21);
                    break;
                }
                case "S":
                {
                    if (subscriber.OtherPayer != null)
                    {
                        ub.PayerB_Secondary.Field50_PayerName = SetStringLength(subscriber.OtherPayer.Formatted(), 26);
                        ub.PayerB_Secondary.Field51_HealthPlanId = SetStringLength(subscriber.OtherPayer.Identification.Id, 17);
                        if (!string.IsNullOrEmpty(subscriber.OtherPayer.PriorAuthorizationNumber))
                        {
                            ub.Field63B_TreatmentAuthorizationCode = SetStringLength(subscriber.OtherPayer.PriorAuthorizationNumber, 34);
                        }
                    }
                    ub.PayerB_Secondary.Field52_ReleaseOfInfoCertIndicator = subscriber.ReleaseOfInformationCode;
                    ub.PayerB_Secondary.Field53_AssignmentOfBenefitsCertIndicator = subscriber.BenefitsAssignmentCertificationIndicator;
                    ub.PayerB_Secondary.Field54_PriorPayments = subscriber.PayorPaidAmount;
                    ub.PayerB_Secondary.Field55_EstimatedAmountDue = subscriber.RemainingPatientLiability;

                    ub.PayerB_Secondary.Field58_InsuredsName = SetStringLength(subscriber.Name.Formatted(), 29);
                    ub.PayerB_Secondary.Field59_PatientRelationship = subscriber.SubscriberInformation.IndividualRelationshipCode;
                    ub.PayerB_Secondary.Field60_InsuredsUniqueId = SetStringLength(subscriber.Name.Identification.Id, 23);
                    ub.PayerB_Secondary.Field61_GroupName = SetStringLength(subscriber.SubscriberInformation.Name, 17);
                    ub.PayerB_Secondary.Field62_InsuredsGroupNumber = SetStringLength(subscriber.SubscriberInformation.ReferenceIdentification, 21);
                    break;
                }
                case "T":
                {
                    if (subscriber.OtherPayer != null)
                    {
                        ub.PayerC_Tertiary.Field50_PayerName = SetStringLength(subscriber.OtherPayer.Formatted(), 26);
                        ub.PayerC_Tertiary.Field51_HealthPlanId = SetStringLength(subscriber.OtherPayer.Identification.Id, 17);
                        if (!string.IsNullOrEmpty(subscriber.OtherPayer.PriorAuthorizationNumber))
                        {
                            ub.Field63C_TreatmentAuthorizationCode = SetStringLength(subscriber.OtherPayer.PriorAuthorizationNumber, 34);
                        }
                    }
                    ub.PayerC_Tertiary.Field52_ReleaseOfInfoCertIndicator = subscriber.ReleaseOfInformationCode;
                    ub.PayerC_Tertiary.Field53_AssignmentOfBenefitsCertIndicator = subscriber.BenefitsAssignmentCertificationIndicator;
                    ub.PayerC_Tertiary.Field54_PriorPayments = subscriber.PayorPaidAmount;
                    ub.PayerC_Tertiary.Field55_EstimatedAmountDue = subscriber.RemainingPatientLiability;

                    ub.PayerC_Tertiary.Field58_InsuredsName = SetStringLength(subscriber.Name.Formatted(), 29);
                    ub.PayerC_Tertiary.Field59_PatientRelationship = subscriber.SubscriberInformation.IndividualRelationshipCode;
                    ub.PayerC_Tertiary.Field60_InsuredsUniqueId = SetStringLength(subscriber.Name.Identification.Id, 23);
                    ub.PayerC_Tertiary.Field61_GroupName = SetStringLength(subscriber.SubscriberInformation.Name, 17);
                    ub.PayerC_Tertiary.Field62_InsuredsGroupNumber = SetStringLength(subscriber.SubscriberInformation.ReferenceIdentification, 21);
                    break;
                }
            }
        }

        private void SetCurrentPayer(Claim claim, UB04Claim ub)
        {
            if (claim.SubscriberInformation == null)
            {
                return;
            }

            switch (claim.SubscriberInformation.PayerResponsibilitySequenceNumberCode)
            {
                case "P":
                    {
                        if (claim.Payer != null)
                        {
                            ub.PayerA_Primary.Field50_PayerName = SetStringLength(claim.Payer.Name.Formatted(), 26);
                            ub.PayerA_Primary.Field51_HealthPlanId = SetStringLength(claim.Payer.Name.Identification.Id, 17);
                            if (!string.IsNullOrEmpty(claim.PriorAuthorizationNumber))
                            {
                                ub.Field63A_TreatmentAuthorizationCode = SetStringLength(claim.PriorAuthorizationNumber, 34);
                            }
                        }
                        ub.PayerA_Primary.Field52_ReleaseOfInfoCertIndicator = claim.ReleaseOfInformationCode;
                        ub.PayerA_Primary.Field53_AssignmentOfBenefitsCertIndicator = claim.BenefitsAssignmentCertificationIndicator;

                        ub.PayerA_Primary.Field58_InsuredsName = SetStringLength(claim.Subscriber.Name.Formatted(), 29);
                        ub.PayerA_Primary.Field59_PatientRelationship = claim.SubscriberInformation.IndividualRelationshipCode;

                        ub.PayerA_Primary.Field60_InsuredsUniqueId = SetStringLength(claim.Subscriber.MemberId, 23);
                        ub.PayerA_Primary.Field61_GroupName = SetStringLength(claim.SubscriberInformation.Name, 17);
                        ub.PayerA_Primary.Field62_InsuredsGroupNumber = SetStringLength(claim.SubscriberInformation.ReferenceIdentification, 21);
                        break;
                    }
                case "S":
                    {
                        if (claim.Payer != null)
                        {
                            ub.PayerB_Secondary.Field50_PayerName = SetStringLength(claim.Payer.Name.Formatted(), 26);
                            ub.PayerB_Secondary.Field51_HealthPlanId = SetStringLength(claim.Payer.Name.Identification.Id, 17);
                            if (!string.IsNullOrEmpty(claim.PriorAuthorizationNumber))
                            {
                                ub.Field63B_TreatmentAuthorizationCode = SetStringLength(claim.PriorAuthorizationNumber, 34);
                            }
                        }
                        ub.PayerB_Secondary.Field52_ReleaseOfInfoCertIndicator = claim.ReleaseOfInformationCode;
                        ub.PayerB_Secondary.Field53_AssignmentOfBenefitsCertIndicator = claim.BenefitsAssignmentCertificationIndicator;

                        ub.PayerB_Secondary.Field58_InsuredsName = SetStringLength(claim.Subscriber.Name.Formatted(), 29);
                        ub.PayerB_Secondary.Field59_PatientRelationship = claim.SubscriberInformation.IndividualRelationshipCode;

                        ub.PayerB_Secondary.Field60_InsuredsUniqueId = SetStringLength(claim.Subscriber.MemberId, 23);
                        ub.PayerB_Secondary.Field61_GroupName = SetStringLength(claim.SubscriberInformation.Name, 17);
                        ub.PayerB_Secondary.Field62_InsuredsGroupNumber = SetStringLength(claim.SubscriberInformation.ReferenceIdentification, 21);
                        break;
                    }
                case "T":
                    {
                        if (claim.Payer != null)
                        {
                            ub.PayerC_Tertiary.Field50_PayerName = SetStringLength(claim.Payer.Name.Formatted(), 26);
                            ub.PayerC_Tertiary.Field51_HealthPlanId = SetStringLength(claim.Payer.Name.Identification.Id, 17);
                            if (!string.IsNullOrEmpty(claim.PriorAuthorizationNumber))
                            {
                                ub.Field63C_TreatmentAuthorizationCode = SetStringLength(claim.PriorAuthorizationNumber, 34);
                            }
                        }
                        ub.PayerC_Tertiary.Field52_ReleaseOfInfoCertIndicator = claim.ReleaseOfInformationCode;
                        ub.PayerC_Tertiary.Field53_AssignmentOfBenefitsCertIndicator = claim.BenefitsAssignmentCertificationIndicator;

                        ub.PayerC_Tertiary.Field58_InsuredsName = SetStringLength(claim.Subscriber.Name.Formatted(), 29);
                        ub.PayerC_Tertiary.Field59_PatientRelationship = claim.SubscriberInformation.IndividualRelationshipCode;

                        ub.PayerC_Tertiary.Field60_InsuredsUniqueId = SetStringLength(claim.Subscriber.MemberId, 23);
                        ub.PayerC_Tertiary.Field61_GroupName = SetStringLength(claim.SubscriberInformation.Name, 17);
                        ub.PayerC_Tertiary.Field62_InsuredsGroupNumber = SetStringLength(claim.SubscriberInformation.ReferenceIdentification, 21);
                        break;
                    }
            }
        }

        private string SetProcedureCodeWithModifiers(MedicalProcedure procedure)
        {
            if (procedure == null)
            {
                return String.Empty;
            }

            StringBuilder procedureCode=new StringBuilder();
            procedureCode.Append(procedure.ProcedureCode);
            if (procedure.Modifier1 != null)
            {
                procedureCode.Append(" " + procedure.Modifier1);
            }
            if (procedure.Modifier2 != null)
            {
                procedureCode.Append(" " + procedure.Modifier2);
            }
            if (procedure.Modifier3 != null)
            {
                procedureCode.Append(" " + procedure.Modifier3);
            }
            if (procedure.Modifier4 != null)
            {
                procedureCode.Append(" " + procedure.Modifier4);
            }
            return procedureCode.ToString();
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text)
        {
            return AddBlock(page, x, y, width, text, TextAlignEnum.left);
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text, TextAlignEnum textAlign)
        {
            decimal xScale = 0.08333m; // 0.0839m;
            decimal yScale = 0.16667m; // 0.1656m;
            var block = new FormBlock
            {
                TextAlign = textAlign,
                Left = 0.06m + xScale * x,
                Top = 0.06m + yScale * y,
                Width = xScale * width,
                Height = yScale * 1.1m,
                Text = text
            };
            page.Blocks.Add(block);
            return block;
        }
        
        public virtual List<FormPage> TransformUB04ToFormPages(UB04Claim ub04)
        {
            List<FormPage> pages = new List<FormPage>();
            int pageCount = 1 + ((ub04.ServiceLines.Count - 1) / 22);
            FormPage page = null;
            int pageIndex = 0;
            for (int i = 0; i < ub04.ServiceLines.Count; i++)
            {
                if (i % 22 == 0)
                {
                    page = new FormPage();
                    pages.Add(page);
                    pageIndex++;
                    page.MasterReference = "ub04";
                    page.ImagePath = _formImagePath;

                    // header
                    // Box 1
                    AddBlock(page, 2, 1, 28, ub04.Field01_BillingProvider.Line1);
                    AddBlock(page, 2, 2, 28, ub04.Field01_BillingProvider.Line2);
                    AddBlock(page, 2, 3, 28, ub04.Field01_BillingProvider.Line3);
                    AddBlock(page, 2, 4, 28, ub04.Field01_BillingProvider.Line4);
                    
                    // Box 2
                    AddBlock(page, 32, 1, 28, ub04.Field02_PayToProvider.Line1);
                    AddBlock(page, 32, 2, 28, ub04.Field02_PayToProvider.Line2);
                    AddBlock(page, 32, 3, 28, ub04.Field02_PayToProvider.Line3);
                    AddBlock(page, 32, 4, 28, ub04.Field02_PayToProvider.Line4);

                    // Box 3
                    AddBlock(page, 65, 1, 27, ub04.Field03a_PatientControlNumber);
                    AddBlock(page, 65, 2, 27, ub04.Field03b_MedicalRecordNumber);
                    
                    // Box 4
                    AddBlock(page, 94, 2, 5, ub04.Field04_TypeOfBill); 
                    
                    // Box 5
                    AddBlock(page, 62, 4, 10, ub04.Field05_FederalTaxId);

                    // Box 6 - Statement Covers Period
                    AddBlock(page, 74, 4, 9, ub04.Field06_StatementCoversPeriod.FromDate);
                    AddBlock(page, 82, 4, 9, ub04.Field06_StatementCoversPeriod.ThroughDate);
                    
                    // Box 7 - Blank
                    AddBlock(page, 91, 3, 8, ub04.Field07.Line1);
                    AddBlock(page, 91, 4, 8, ub04.Field07.Line2);

                    // Box 8
                    AddBlock(page, 15, 5, 21, ub04.Field08_PatientName_a);
                    AddBlock(page, 3, 6, 33, ub04.Field08_PatientName_b);

                    // Box 9
                    AddBlock(page, 51, 5, 48, ub04.Field09_PatientAddress.a_Street);
                    AddBlock(page, 39, 6, 37, ub04.Field09_PatientAddress.b_City);
                    AddBlock(page, 78, 6, 2, ub04.Field09_PatientAddress.c_State);
                    AddBlock(page, 83, 6, 10, ub04.Field09_PatientAddress.d_PostalCode);
                    AddBlock(page, 96, 6, 3, ub04.Field09_PatientAddress.e_CountryCode);

                    // Box 10
                    AddBlock(page, 2, 8, 8, ub04.Field10_Birthdate);

                    // Box 11
                    AddBlock(page, 13, 8, 1, ub04.Field11_Sex);

                    // Box 12-15
                    AddBlock(page, 16, 8, 6, ub04.Field12_AdmissionDate);
                    AddBlock(page, 23, 8, 2, ub04.Field13_AdmissionHour);
                    AddBlock(page, 27, 8, 2, ub04.Field14_AdmissionType);
                    AddBlock(page, 30, 8, 2, ub04.Field15_AdmissionSource);

                    // Box 16
                    AddBlock(page, 34, 8, 2, ub04.Field16_DischargeHour);

                    // Box 17
                    AddBlock(page, 38, 8, 2,  ub04.Field17_DischargeStatus);
 
                    // Box 18 - 28 Condition Codes
                    AddBlock(page, 41, 8, 2, ub04.Field18_ConditionCode01);
                    AddBlock(page, 45, 8, 2, ub04.Field19_ConditionCode02);
                    AddBlock(page, 48, 8, 2, ub04.Field20_ConditionCode03);
                    AddBlock(page, 52, 8, 2, ub04.Field21_ConditionCode04);
                    AddBlock(page, 56, 8, 2, ub04.Field22_ConditionCode05);
                    AddBlock(page, 59, 8, 2, ub04.Field23_ConditionCode06);
                    AddBlock(page, 63, 8, 2, ub04.Field24_ConditionCode07);
                    AddBlock(page, 66, 8, 2, ub04.Field25_ConditionCode08);
                    AddBlock(page, 70, 8, 2, ub04.Field26_ConditionCode09);
                    AddBlock(page, 73.5m, 8, 2, ub04.Field27_ConditionCode10);
                    AddBlock(page, 77, 8, 2, ub04.Field28_ConditionCode11);

                    // Box 29
                    AddBlock(page, 81, 8, 2, ub04.Field29_AccidentState);
                    
                    // Box 30
                    AddBlock(page, 85, 8, 14, ub04.Field30);

                    // Box 31
                    AddBlock(page, 2, 10, 2, ub04.Field31a_Occurrence.Code);
                    AddBlock(page, 6, 10, 6, ub04.Field31a_Occurrence.Date);
                    AddBlock(page, 2, 11, 2, ub04.Field31b_Occurrence.Code);
                    AddBlock(page, 6, 11, 6, ub04.Field31b_Occurrence.Date);

                   // Box 32
                    AddBlock(page, 14, 10, 2, ub04.Field32a_Occurrence.Code);
                    AddBlock(page, 18, 10, 6, ub04.Field32a_Occurrence.Date);
                    AddBlock(page, 14, 11, 2, ub04.Field32b_Occurrence.Code);
                    AddBlock(page, 18, 11, 6, ub04.Field32b_Occurrence.Date);

                    // Box 33
                    AddBlock(page, 26, 10, 2, ub04.Field33a_Occurrence.Code);
                    AddBlock(page, 30, 10, 6, ub04.Field33a_Occurrence.Date);
                    AddBlock(page, 26, 11, 2, ub04.Field33b_Occurrence.Code);
                    AddBlock(page, 30, 11, 6, ub04.Field33b_Occurrence.Date);

                    // Box 34
                    AddBlock(page, 38, 10, 2, ub04.Field34a_Occurrence.Code);
                    AddBlock(page, 41, 10, 6, ub04.Field34a_Occurrence.Date);
                    AddBlock(page, 38, 11, 2, ub04.Field34b_Occurrence.Code);
                    AddBlock(page, 41, 11, 6, ub04.Field34b_Occurrence.Date);

                    // Box 35
                    AddBlock(page, 50, 10, 2, ub04.Field35a_OccurrenceSpan.Code);
                    AddBlock(page, 53, 10, 6, ub04.Field35a_OccurrenceSpan.FromDate);
                    AddBlock(page, 62, 10, 6, ub04.Field35a_OccurrenceSpan.ThroughDate);
                    AddBlock(page, 50, 11, 2, ub04.Field35b_OccurrenceSpan.Code);
                    AddBlock(page, 53, 11, 6, ub04.Field35b_OccurrenceSpan.FromDate);
                    AddBlock(page, 62, 11, 6, ub04.Field35b_OccurrenceSpan.ThroughDate);

                    // Box 36
                    AddBlock(page, 70, 10, 2, ub04.Field36a_OccurrenceSpan.Code);
                    AddBlock(page, 74, 10, 6, ub04.Field36a_OccurrenceSpan.FromDate);
                    AddBlock(page, 82, 10, 6, ub04.Field36a_OccurrenceSpan.ThroughDate);
                    AddBlock(page, 70, 11, 2, ub04.Field36b_OccurrenceSpan.Code);
                    AddBlock(page, 74, 11, 6, ub04.Field36b_OccurrenceSpan.FromDate);
                    AddBlock(page, 82, 11, 6, ub04.Field36b_OccurrenceSpan.ThroughDate);

                    // Box 37 - Blank
                    AddBlock(page, 90, 10, 9, ub04.Field37.Line1);
                    AddBlock(page, 90, 11, 9, ub04.Field37.Line2);

                    // Box 38 - Responsible Party
                    AddBlock(page, 2, 12, 48, ub04.Field38_ResponsibleParty.Line1);
                    AddBlock(page, 2, 13, 48, ub04.Field38_ResponsibleParty.Line2);
                    AddBlock(page, 2, 14, 48, ub04.Field38_ResponsibleParty.Line3);
                    AddBlock(page, 2, 15, 48, ub04.Field38_ResponsibleParty.Line4);
                    AddBlock(page, 2, 16, 48, ub04.Field38_ResponsibleParty.Line5);

                    // Box 39 - Value Codes
                    AddBlock(page, 53, 13, 2, ub04.Field39a_Value.Code);
                    AddBlock(page, 57, 13, 12, String.Format("{0:0.00}", ub04.Field39a_Value.Amount).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 53, 14, 2, ub04.Field39b_Value.Code);
                    AddBlock(page, 57, 14, 12, String.Format("{0:0.00}", ub04.Field39b_Value.Amount).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 53, 15, 2, ub04.Field39c_Value.Code);
                    AddBlock(page, 57, 15, 12, String.Format("{0:0.00}", ub04.Field39c_Value.Amount).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 53, 16, 2, ub04.Field39d_Value.Code);
                    AddBlock(page, 57, 16, 12, String.Format("{0:0.00}", ub04.Field39d_Value.Amount).Replace('.', ' '), TextAlignEnum.right);

                    // Box 40
                    AddBlock(page, 69, 13, 2, ub04.Field40a_Value.Code);
                    AddBlock(page, 72.5m, 13, 12, String.Format("{0:0.00}", ub04.Field40a_Value.Amount).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 69, 14, 2, ub04.Field40b_Value.Code);
                    AddBlock(page, 72.5m, 14, 12, String.Format("{0:0.00}", ub04.Field40b_Value.Amount).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 69, 15, 2, ub04.Field40c_Value.Code);
                    AddBlock(page, 72.5m, 15, 12, String.Format("{0:0.00}", ub04.Field40c_Value.Amount).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 69, 16, 2, ub04.Field40d_Value.Code);
                    AddBlock(page, 72.5m, 16, 12, String.Format("{0:0.00}", ub04.Field40d_Value.Amount).Replace('.', ' '), TextAlignEnum.right);

                    // Box 41 - Value Codes
                    AddBlock(page, 84, 13, 2, ub04.Field41a_Value.Code);
                    AddBlock(page, 88, 13, 12, String.Format("{0:0.00}", ub04.Field41a_Value.Amount).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 84, 14, 2, ub04.Field41b_Value.Code);
                    AddBlock(page, 88, 14, 12, String.Format("{0:0.00}", ub04.Field41b_Value.Amount).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 84, 15, 2, ub04.Field41c_Value.Code);
                    AddBlock(page, 88, 15, 12, String.Format("{0:0.00}", ub04.Field41c_Value.Amount).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 84, 16, 2, ub04.Field41d_Value.Code);
                    AddBlock(page, 88, 16, 12, String.Format("{0:0.00}", ub04.Field41d_Value.Amount).Replace('.', ' '), TextAlignEnum.right);
                }

                // service lines
                decimal y = 18 + (i % 22);
                var line = ub04.ServiceLines[i];
                // Box 42 - 49 - Service Lines
                AddBlock(page, 2, y, 4, line.Field42_RevenueCode);
                AddBlock(page, 7, y, 29, line.Field43_Description);
                AddBlock(page, 37, y, 17, line.Field44_ProcedureCodes);
                AddBlock(page, 56, y, 6, line.Field45_ServiceDate);
                AddBlock(page, 64, y, 9, line.Field46_ServiceUnits, TextAlignEnum.right);
                
                AddBlock(page, 74, y, 11, String.Format("{0:0.00}", line.Field47_TotalCharges).Replace('.',' '), TextAlignEnum.right);
                AddBlock(page, 86, y, 11, String.Format("{0:0.00}", line.Field48_NonCoveredCharges).Replace('.',' '), TextAlignEnum.right);
                AddBlock(page, 97, y, 2, line.Field49);

                if (i % 22 == 21 || i == ub04.ServiceLines.Count - 1) // Footer
                {
                    AddBlock(page, 13, 40, 3, pageIndex.ToString(), TextAlignEnum.right);
                    AddBlock(page, 20, 40, 3, pageCount.ToString(), TextAlignEnum.right);
                    if (PerPageTotalChargesView)
                    {
                        int lowIndex;
                        if (i % 22 == 21)
                        {
                            lowIndex = i - 21;
                        }
                        else
                        {
                            lowIndex = i - (i % 22);
                        }
                        decimal? pageCharges = 0;
                        decimal? nonCoveredCharges = 0;
                        for (int x = i; x >= lowIndex; x--)
                        {
                            if (ub04.ServiceLines[x].Field47_TotalCharges != null)
                            {
                                pageCharges += ub04.ServiceLines[x].Field47_TotalCharges;
                            }
                            if (ub04.ServiceLines[x].Field48_NonCoveredCharges != null)
                            {
                                nonCoveredCharges += ub04.ServiceLines[x].Field48_NonCoveredCharges;
                            }

                        }
                        AddBlock(page, 74, 40, 11, String.Format("{0:0.00}", pageCharges).Replace('.', ' '), TextAlignEnum.right);
                        AddBlock(page, 86, 40, 11, String.Format("{0:0.00}", nonCoveredCharges).Replace('.', ' '), TextAlignEnum.right);
                    }
                    else
                    {
                        if (pageIndex == pageCount)
                        {
                            AddBlock(page, 74, 40, 11, String.Format("{0:0.00}", ub04.Field47_Line23_TotalCharges).Replace('.', ' '), TextAlignEnum.right);
                            AddBlock(page, 86, 40, 11, String.Format("{0:0.00}", ub04.Field48_Line23_NonCoveredCharges).Replace('.', ' '), TextAlignEnum.right);
                        }
                    }

                    // Box 50
                    AddBlock(page, 2, 42, 26, ub04.PayerA_Primary.Field50_PayerName);
                    AddBlock(page, 2, 43, 26, ub04.PayerB_Secondary.Field50_PayerName);
                    AddBlock(page, 2, 44, 26, ub04.PayerC_Tertiary.Field50_PayerName);

                    // Box 51
                    AddBlock(page, 29, 42, 17, ub04.PayerA_Primary.Field51_HealthPlanId);
                    AddBlock(page, 29, 43, 17, ub04.PayerB_Secondary.Field51_HealthPlanId);
                    AddBlock(page, 29, 44, 17, ub04.PayerC_Tertiary.Field51_HealthPlanId);


                    // Box 52 - Release of Info
                    AddBlock(page, 46.5m, 42, 2, ub04.PayerA_Primary.Field52_ReleaseOfInfoCertIndicator);
                    AddBlock(page, 46.5m, 43, 2, ub04.PayerB_Secondary.Field52_ReleaseOfInfoCertIndicator);
                    AddBlock(page, 46.5m, 44, 2, ub04.PayerC_Tertiary.Field52_ReleaseOfInfoCertIndicator);

                    // Box 53
                    AddBlock(page, 50, 42, 2, ub04.PayerA_Primary.Field53_AssignmentOfBenefitsCertIndicator);
                    AddBlock(page, 50, 43, 2, ub04.PayerB_Secondary.Field53_AssignmentOfBenefitsCertIndicator);
                    AddBlock(page, 50, 44, 2, ub04.PayerC_Tertiary.Field53_AssignmentOfBenefitsCertIndicator);

                    // Box 54
                    AddBlock(page, 54.25m, 42, 11, String.Format("{0:0.00}", ub04.PayerA_Primary.Field54_PriorPayments).Replace('.',' '), TextAlignEnum.right);
                    AddBlock(page, 54.25m, 43, 11, String.Format("{0:0.00}", ub04.PayerB_Secondary.Field54_PriorPayments).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 54.25m, 44, 11, String.Format("{0:0.00}", ub04.PayerC_Tertiary.Field54_PriorPayments).Replace('.', ' '), TextAlignEnum.right);

                    // Box 55
                    AddBlock(page, 66.5m, 42, 12, String.Format("{0:0.00}", ub04.PayerA_Primary.Field55_EstimatedAmountDue).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 66.5m, 43, 12, String.Format("{0:0.00}", ub04.PayerB_Secondary.Field55_EstimatedAmountDue).Replace('.', ' '), TextAlignEnum.right);
                    AddBlock(page, 66.5m, 44, 12, String.Format("{0:0.00}", ub04.PayerC_Tertiary.Field55_EstimatedAmountDue).Replace('.', ' '), TextAlignEnum.right);

                    // Box 56
                    AddBlock(page, 85, 41, 10, ub04.Field56_NationalProviderIdentifier);

                    // Box 57
                    AddBlock(page, 82, 42, 17, ub04.Field57_OtherProviderIdA);
                    AddBlock(page, 82, 43, 17, ub04.Field57_OtherProviderIdB);
                    AddBlock(page, 82, 44, 17, ub04.Field57_OtherProviderIdC);

                    // Box 58
                    AddBlock(page, 2, 46, 29, ub04.PayerA_Primary.Field58_InsuredsName);
                    AddBlock(page, 2, 47, 29, ub04.PayerB_Secondary.Field58_InsuredsName);
                    AddBlock(page, 2, 48, 29, ub04.PayerC_Tertiary.Field58_InsuredsName);

                    // Box 59
                    AddBlock(page, 33, 46, 2, ub04.PayerA_Primary.Field59_PatientRelationship);
                    AddBlock(page, 33, 47, 2, ub04.PayerB_Secondary.Field59_PatientRelationship);
                    AddBlock(page, 33, 48, 2, ub04.PayerC_Tertiary.Field59_PatientRelationship);

                    // Box 60
                    AddBlock(page, 36, 46, 23, ub04.PayerA_Primary.Field60_InsuredsUniqueId);
                    AddBlock(page, 36, 47, 23, ub04.PayerB_Secondary.Field60_InsuredsUniqueId);
                    AddBlock(page, 36, 48, 23, ub04.PayerC_Tertiary.Field60_InsuredsUniqueId);

                    // Box 61
                    AddBlock(page, 60, 46, 17, ub04.PayerA_Primary.Field61_GroupName);
                    AddBlock(page, 60, 47, 17, ub04.PayerB_Secondary.Field61_GroupName);
                    AddBlock(page, 60, 48, 17, ub04.PayerC_Tertiary.Field61_GroupName);

                    // Box 62
                    AddBlock(page, 78, 46, 21, ub04.PayerA_Primary.Field62_InsuredsGroupNumber);
                    AddBlock(page, 78, 47, 21, ub04.PayerB_Secondary.Field62_InsuredsGroupNumber);
                    AddBlock(page, 78, 48, 21, ub04.PayerC_Tertiary.Field62_InsuredsGroupNumber);

                    // Box 63
                    AddBlock(page, 2, 50, 35, ub04.Field63A_TreatmentAuthorizationCode);
                    AddBlock(page, 2, 51, 35, ub04.Field63B_TreatmentAuthorizationCode);
                    AddBlock(page, 2, 52, 35, ub04.Field63C_TreatmentAuthorizationCode);

                    // Box 64 - Document Control Number
                    AddBlock(page, 39, 50, 30, ub04.Field64A_DocumentControlNumber);
                    AddBlock(page, 39, 51, 30, ub04.Field64B_DocumentControlNumber);
                    AddBlock(page, 39, 52, 30, ub04.Field64C_DocumentControlNumber);

                    // Box 65 - Employer Name
                    AddBlock(page, 70, 50, 29, ub04.Field65a_EmployerName);
                    AddBlock(page, 70, 51, 29, ub04.Field65b_EmployerName);
                    AddBlock(page, 70, 52, 29, ub04.Field65c_EmployerName);

                    // Box 66 - ICD Version
                    AddBlock(page, 1, 54, 1, ub04.Field66_Version);

                    // Box 67 - Primary Diagnosis
                    AddBlock(page, 3, 53, 6, ub04.Field67_PrincipleDiagnosis.Code);
                    AddBlock(page, 10.5m, 53, 1, ub04.Field67_PrincipleDiagnosis.PresentOnAdmissionIndicator);

                    // Box 67A
                    AddBlock(page, 13, 53, 6, ub04.Field67A_Diagnosis.Code);
                    AddBlock(page, 20, 53, 1, ub04.Field67A_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67B
                    AddBlock(page, 22, 53, 6, ub04.Field67B_Diagnosis.Code);
                    AddBlock(page, 29.75m, 53, 1, ub04.Field67B_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67C
                    AddBlock(page, 32, 53, 6, ub04.Field67C_Diagnosis.Code);
                    AddBlock(page, 39.25m, 53, 1, ub04.Field67C_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67D
                    AddBlock(page, 42, 53, 6, ub04.Field67D_Diagnosis.Code);
                    AddBlock(page, 49m, 53, 1, ub04.Field67D_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67E
                    AddBlock(page, 51, 53, 6, ub04.Field67E_Diagnosis.Code);
                    AddBlock(page, 58.5m, 53, 1, ub04.Field67E_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67F
                    AddBlock(page, 61, 53, 6, ub04.Field67F_Diagnosis.Code);
                    AddBlock(page, 68m, 53, 1, ub04.Field67F_Diagnosis.PresentOnAdmissionIndicator);
                    
                    // Box 67G
                    AddBlock(page, 70, 53, 6, ub04.Field67G_Diagnosis.Code);
                    AddBlock(page, 77.75m, 53, 1, ub04.Field67G_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67H
                    AddBlock(page, 80, 53, 6, ub04.Field67H_Diagnosis.Code);
                    AddBlock(page, 87.25m, 53, 1, ub04.Field67H_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67I
                    AddBlock(page, 3, 54, 6, ub04.Field67I_Diagnosis.Code);
                    AddBlock(page, 10.5m, 54, 1, ub04.Field67I_Diagnosis.PresentOnAdmissionIndicator);
                    
                    // Box 67J
                    AddBlock(page, 13, 54, 6, ub04.Field67J_Diagnosis.Code);
                    AddBlock(page, 20, 54, 1, ub04.Field67J_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67K
                    AddBlock(page, 22, 54, 6, ub04.Field67K_Diagnosis.Code);
                    AddBlock(page, 29.75m, 54, 1, ub04.Field67K_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67L
                    AddBlock(page, 32, 54, 6, ub04.Field67L_Diagnosis.Code);
                    AddBlock(page, 39.25m, 54, 1, ub04.Field67L_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67M
                    AddBlock(page, 42, 54, 6, ub04.Field67M_Diagnosis.Code);
                    AddBlock(page, 49m, 54, 1, ub04.Field67M_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67N
                    AddBlock(page, 51, 54, 6, ub04.Field67N_Diagnosis.Code);
                    AddBlock(page, 58.5m, 54, 1, ub04.Field67N_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67O
                    AddBlock(page, 61, 54, 6, ub04.Field67O_Diagnosis.Code);
                    AddBlock(page, 68m, 54, 1, ub04.Field67O_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67P
                    AddBlock(page, 70, 54, 6, ub04.Field67P_Diagnosis.Code);
                    AddBlock(page, 77.75m, 54, 1, ub04.Field67P_Diagnosis.PresentOnAdmissionIndicator);

                    // Box 67Q
                    AddBlock(page, 80, 54, 6, ub04.Field67Q_Diagnosis.Code);
                    AddBlock(page, 87.25m, 54, 1, ub04.Field67Q_Diagnosis.PresentOnAdmissionIndicator);
                    
                    // Box 68
                    AddBlock(page, 90, 53, 9, ub04.Field68.Line1);
                    AddBlock(page, 90, 54, 9, ub04.Field68.Line2);

                    // Box 69 - Admitting Diagnosis
                    AddBlock(page, 6, 55, 6, ub04.Field69_AdmittingDiagnosisCode.Code);

                    // Box 70 - Patient Reason Diagnosis
                    AddBlock(page, 21, 55, 6, ub04.Field70a_PatientReasonDiagnosisCode.Code);
                    AddBlock(page, 29, 55, 6, ub04.Field70b_PatientReasonDiagnosisCode.Code);
                    AddBlock(page, 38, 55, 6, ub04.Field70c_PatientReasonDiagnosisCode.Code);

                    // Box 71 - PPS Code
                    AddBlock(page, 51, 55, 5, ub04.Field71_PPSCode);

                    // Box 72 - External Cause of Injury
                    AddBlock(page, 59, 55, 6, ub04.Field72a_ExternalCauseOfInjury.Code);
                    AddBlock(page, 67m, 55, 1, ub04.Field72a_ExternalCauseOfInjury.PresentOnAdmissionIndicator);
                    AddBlock(page, 69, 55, 6, ub04.Field72b_ExternalCauseOfInjury.Code);
                    AddBlock(page, 76.75m, 55, 1, ub04.Field72b_ExternalCauseOfInjury.PresentOnAdmissionIndicator);
                    AddBlock(page, 79, 55, 6, ub04.Field72c_ExternalCauseOfInjury.Code);
                    AddBlock(page, 86.25m, 55, 1, ub04.Field72c_ExternalCauseOfInjury.PresentOnAdmissionIndicator);

                    // Box 73 - Blank
                    AddBlock(page, 89, 55, 10, ub04.Field73);

                    // Box 74
                    AddBlock(page, 2, 57, 8, ub04.Field74_PrincipalProcedure.Code);
                    AddBlock(page, 12, 57, 6, ub04.Field74_PrincipalProcedure.Date);
                    AddBlock(page, 20, 57, 8, ub04.Field74a_OtherProcedure.Code);
                    AddBlock(page, 29, 57, 6, ub04.Field74a_OtherProcedure.Date);
                    AddBlock(page, 38, 57, 8, ub04.Field74b_OtherProcedure.Code);
                    AddBlock(page, 48, 57, 6, ub04.Field74b_OtherProcedure.Date);
                    AddBlock(page, 2, 59, 8, ub04.Field74c_OtherProcedure.Code);
                    AddBlock(page, 12, 59, 6, ub04.Field74c_OtherProcedure.Date);
                    AddBlock(page, 20, 59, 8, ub04.Field74d_OtherProcedure.Code);
                    AddBlock(page, 29, 59, 6, ub04.Field74d_OtherProcedure.Date);
                    AddBlock(page, 38, 59, 8, ub04.Field74e_OtherProcedure.Code);
                    AddBlock(page, 48, 59, 6, ub04.Field74e_OtherProcedure.Date);

                    // Box 75
                    AddBlock(page, 56, 56, 4, ub04.Field75.Line1);
                    AddBlock(page, 56, 57, 4, ub04.Field75.Line2);
                    AddBlock(page, 56, 58, 4, ub04.Field75.Line3);
                    AddBlock(page, 56, 59, 4, ub04.Field75.Line4);

                    // Box 76
                    AddBlock(page, 72, 56, 10, ub04.Field76_AttendingPhysician.Npi);
                    AddBlock(page, 86, 56, 2, ub04.Field76_AttendingPhysician.IdentifierQualifier);
                    AddBlock(page, 89, 56, 10, ub04.Field76_AttendingPhysician.Identifier);
                    AddBlock(page, 64, 57, 18, ub04.Field76_AttendingPhysician.LastName);
                    AddBlock(page, 86, 57, 13, ub04.Field76_AttendingPhysician.FirstName);

                    // Box 77
                    AddBlock(page, 72, 58, 10, ub04.Field77_OperatingPhysician.Npi);
                    AddBlock(page, 86, 58, 2, ub04.Field77_OperatingPhysician.IdentifierQualifier);
                    AddBlock(page, 89, 58, 10, ub04.Field77_OperatingPhysician.Identifier);
                    AddBlock(page, 64, 59, 18, ub04.Field77_OperatingPhysician.LastName);
                    AddBlock(page, 86, 59, 13, ub04.Field77_OperatingPhysician.FirstName);

                    // Box 78
                    AddBlock(page, 72, 60, 10, ub04.Field78_OtherProvider.Npi);
                    AddBlock(page, 67, 60, 2, ub04.Field78_OtherProvider.ProviderQualifier);
                    AddBlock(page, 86, 60, 2, ub04.Field78_OtherProvider.IdentifierQualifier);
                    AddBlock(page, 89, 60, 10, ub04.Field78_OtherProvider.Identifier);
                    AddBlock(page, 64, 61, 18, ub04.Field78_OtherProvider.LastName);
                    AddBlock(page, 86, 61, 13, ub04.Field78_OtherProvider.FirstName);

                    // Box 79
                    AddBlock(page, 72, 62, 10, ub04.Field79_OtherProvider.Npi);
                    AddBlock(page, 67, 62, 2, ub04.Field79_OtherProvider.ProviderQualifier);
                    AddBlock(page, 86, 62, 2, ub04.Field79_OtherProvider.IdentifierQualifier);
                    AddBlock(page, 89, 62, 10, ub04.Field79_OtherProvider.Identifier);
                    AddBlock(page, 64, 63, 18, ub04.Field79_OtherProvider.LastName);
                    AddBlock(page, 86, 63, 13, ub04.Field79_OtherProvider.FirstName);

                    // Box 80
                    AddBlock(page, 2, 61, 27, ub04.Field80_Remarks.Line1);
                    AddBlock(page, 2, 62, 27, ub04.Field80_Remarks.Line2);
                    AddBlock(page, 2, 63, 27, ub04.Field80_Remarks.Line3);

                    // Box 81
                    AddBlock(page, 32, 60, 2, ub04.Field81a.Qualifier);
                    AddBlock(page, 35, 60, 10, ub04.Field81a.Code1);
                    AddBlock(page, 48, 60, 12, ub04.Field81a.Code2);
                    AddBlock(page, 32, 61, 2, ub04.Field81b.Qualifier);
                    AddBlock(page, 35, 61, 10, ub04.Field81b.Code1);
                    AddBlock(page, 48, 61, 12, ub04.Field81b.Code2);
                    AddBlock(page, 32, 62, 2, ub04.Field81c.Qualifier);
                    AddBlock(page, 35, 62, 10, ub04.Field81c.Code1);
                    AddBlock(page, 48, 62, 12, ub04.Field81c.Code2);
                    AddBlock(page, 32, 63, 2, ub04.Field81d.Qualifier);
                    AddBlock(page, 35, 63, 10, ub04.Field81d.Code1);
                    AddBlock(page, 48, 63, 12, ub04.Field81d.Code2);

                    // reorder by locations;
                    page.Blocks = page.Blocks.OrderBy(bl => bl.Top).ToList();
                }
            }
            return pages;
        }

        public List<FormPage> TransformClaimToClaimFormFoXml(Claim claim)
        {
            UB04Claim ub04 = TransformClaimToUB04(claim);

            return TransformUB04ToFormPages(ub04);
        }
    }
}