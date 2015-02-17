using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    [Serializable]
    public class UB04Claim
    {
        /*
         * 2011/8/15, jhalliday - New Data Model for 837I (Institutional) claim.
         * 
         * Team: dstrubhar, jhalliday and epkrause
         * 
         * Purpose:
         * To create a C# object model that will serve as a container for the X12 837I data
         * AS ENTERED from a UB-04 CMS-1450 Institutional (Hospital) claim form.
         * 
         * Goal:
         * The team has the overall goal of creating tools that can be used to consume and
         * manipulate X12 messages (AKA files/documents) without the need to have a big project
         * budget.  For that reason, this and the related X12 Parser project tools are all open
         * source and freely usable.
         */

        // Fields in the UB04 object model are defined in the order they appear on the UB-04 form.
        public UB04Claim() 
        {
            if (Field01_BillingProvider == null) Field01_BillingProvider = new UB04Block();
            if (Field02_PayToProvider == null) Field02_PayToProvider = new UB04Block();
            if (Field06_StatementCoversPeriod == null) Field06_StatementCoversPeriod = new UB04OccurrenceSpan();
            if (Field07 == null) Field07 = new UB04Block();
            if (Field09_PatientAddress == null) Field09_PatientAddress = new UB04PatientAddress();
            if (Field31a_Occurrence == null) Field31a_Occurrence = new UB04Occurrence();
            if (Field31b_Occurrence == null) Field31b_Occurrence = new UB04Occurrence();
            if (Field32a_Occurrence == null) Field32a_Occurrence = new UB04Occurrence();
            if (Field32b_Occurrence == null) Field32b_Occurrence = new UB04Occurrence();
            if (Field33a_Occurrence == null) Field33a_Occurrence = new UB04Occurrence();
            if (Field33b_Occurrence == null) Field33b_Occurrence = new UB04Occurrence();
            if (Field34a_Occurrence == null) Field34a_Occurrence = new UB04Occurrence();
            if (Field34b_Occurrence == null) Field34b_Occurrence = new UB04Occurrence();
            if (Field35a_OccurrenceSpan == null) Field35a_OccurrenceSpan = new UB04OccurrenceSpan();
            if (Field35b_OccurrenceSpan == null) Field35b_OccurrenceSpan = new UB04OccurrenceSpan();
            if (Field36a_OccurrenceSpan == null) Field36a_OccurrenceSpan = new UB04OccurrenceSpan();
            if (Field36b_OccurrenceSpan == null) Field36b_OccurrenceSpan = new UB04OccurrenceSpan();
            if (Field37 == null) Field37 = new UB04Block();
            if (Field38_ResponsibleParty == null) Field38_ResponsibleParty = new UB04Block();
            if (Field39a_Value == null) Field39a_Value = new UB04Value();
            if (Field39b_Value == null) Field39b_Value = new UB04Value();
            if (Field39c_Value == null) Field39c_Value = new UB04Value();
            if (Field39d_Value == null) Field39d_Value = new UB04Value();
            if (Field40a_Value == null) Field40a_Value = new UB04Value();
            if (Field40b_Value == null) Field40b_Value = new UB04Value();
            if (Field40c_Value == null) Field40c_Value = new UB04Value();
            if (Field40d_Value == null) Field40d_Value = new UB04Value();
            if (Field41a_Value == null) Field41a_Value = new UB04Value();
            if (Field41b_Value == null) Field41b_Value = new UB04Value();
            if (Field41c_Value == null) Field41c_Value = new UB04Value();
            if (Field41d_Value == null) Field41d_Value = new UB04Value();
            
            if (ServiceLines == null) ServiceLines = new List<UB04ServiceLine>();
            if (PayerA_Primary == null) PayerA_Primary = new UB04Payer();
            if (PayerB_Secondary == null) PayerB_Secondary = new UB04Payer();
            if (PayerC_Tertiary == null) PayerC_Tertiary = new UB04Payer();
            if (Field67_PrincipleDiagnosis == null) Field67_PrincipleDiagnosis = new UB04Diagnosis();
            if (Field67A_Diagnosis == null) Field67A_Diagnosis = new UB04Diagnosis();
            if (Field67B_Diagnosis == null) Field67B_Diagnosis = new UB04Diagnosis();
            if (Field67C_Diagnosis == null) Field67C_Diagnosis = new UB04Diagnosis();
            if (Field67D_Diagnosis == null) Field67D_Diagnosis = new UB04Diagnosis();
            if (Field67E_Diagnosis == null) Field67E_Diagnosis = new UB04Diagnosis();
            if (Field67F_Diagnosis == null) Field67F_Diagnosis = new UB04Diagnosis();
            if (Field67G_Diagnosis == null) Field67G_Diagnosis = new UB04Diagnosis();
            if (Field67H_Diagnosis == null) Field67H_Diagnosis = new UB04Diagnosis();
            if (Field67I_Diagnosis == null) Field67I_Diagnosis = new UB04Diagnosis();
            if (Field67J_Diagnosis == null) Field67J_Diagnosis = new UB04Diagnosis();
            if (Field67K_Diagnosis == null) Field67K_Diagnosis = new UB04Diagnosis();
            if (Field67L_Diagnosis == null) Field67L_Diagnosis = new UB04Diagnosis();
            if (Field67M_Diagnosis == null) Field67M_Diagnosis = new UB04Diagnosis();
            if (Field67N_Diagnosis == null) Field67N_Diagnosis = new UB04Diagnosis();
            if (Field67O_Diagnosis == null) Field67O_Diagnosis = new UB04Diagnosis();
            if (Field67P_Diagnosis == null) Field67P_Diagnosis = new UB04Diagnosis();
            if (Field67Q_Diagnosis == null) Field67Q_Diagnosis = new UB04Diagnosis();
            if (Field68 == null) Field68 = new UB04Block();
            if (Field69_AdmittingDiagnosisCode == null) Field69_AdmittingDiagnosisCode = new UB04Diagnosis();
            if (Field70a_PatientReasonDiagnosisCode == null) Field70a_PatientReasonDiagnosisCode = new UB04Diagnosis();
            if (Field70b_PatientReasonDiagnosisCode == null) Field70b_PatientReasonDiagnosisCode = new UB04Diagnosis();
            if (Field70c_PatientReasonDiagnosisCode == null) Field70c_PatientReasonDiagnosisCode = new UB04Diagnosis();
            if (Field72a_ExternalCauseOfInjury == null) Field72a_ExternalCauseOfInjury = new UB04Diagnosis();
            if (Field72b_ExternalCauseOfInjury == null) Field72b_ExternalCauseOfInjury = new UB04Diagnosis();
            if (Field72c_ExternalCauseOfInjury == null) Field72c_ExternalCauseOfInjury = new UB04Diagnosis();
            if (Field74_PrincipalProcedure == null) Field74_PrincipalProcedure = new UB04Occurrence();
            if (Field74a_OtherProcedure == null) Field74a_OtherProcedure = new UB04Occurrence();
            if (Field74b_OtherProcedure == null) Field74b_OtherProcedure = new UB04Occurrence();
            if (Field74c_OtherProcedure == null) Field74c_OtherProcedure = new UB04Occurrence();
            if (Field74d_OtherProcedure == null) Field74d_OtherProcedure = new UB04Occurrence();
            if (Field74e_OtherProcedure == null) Field74e_OtherProcedure = new UB04Occurrence();
            if (Field75 == null) Field75 = new UB04Block();
            if (Field76_AttendingPhysician == null) Field76_AttendingPhysician = new UB04Provider();
            if (Field77_OperatingPhysician == null) Field77_OperatingPhysician = new UB04Provider();
            if (Field78_OtherProvider == null) Field78_OtherProvider = new UB04Provider();
            if (Field79_OtherProvider == null) Field79_OtherProvider = new UB04Provider();
            if (Field80_Remarks == null) Field80_Remarks = new UB04Block();
            if (Field81a == null) Field81a = new UB04CodeCode();
            if (Field81b == null) Field81b = new UB04CodeCode();
            if (Field81c == null) Field81c = new UB04CodeCode();
            if (Field81d == null) Field81d = new UB04CodeCode();
        }


        // Now the accessor definitions:

        public UB04Block Field01_BillingProvider { get; set; }

        public UB04Block Field02_PayToProvider { get; set; } 
       
        /// <summary>
        /// Field 03a - a unique alpha-numeric number assigned by the provider.  Used to allow for the retrieval
        /// of individual patient financial records.  Optional field.
        /// </summary>
        public string Field03a_PatientControlNumber { get; set; }
        public string Field03b_MedicalRecordNumber { get; set; }
        
        /// <summary>
        /// Field 04 - Type of Bill, a three or four digit code that indicates the type of bill being submitted.
        /// Refer to the NUBC Guide for TOB frequency codes.  This is set as a string value because it may contain
        /// a leading zero.
        /// </summary>
        public string Field04_TypeOfBill { get; set; }

        /// <summary>
        /// Field 05 - Federal Tax ID Number.  This field may contain the tax id (TID) or the newer Employer Identification
        /// Number (EIN).  Affiliated subsidiaries are identified using federal tax sub-ID's.
        /// </summary>
        public string Field05_FederalTaxId { get; set; }

        public UB04OccurrenceSpan Field06_StatementCoversPeriod { get; set; }
        
        public UB04Block Field07 { get; set; }

        public string Field08_PatientName_a { get; set; }

        public string Field08_PatientName_b { get; set; }

        public UB04PatientAddress Field09_PatientAddress { get; set; }

        public string Field10_Birthdate { get; set; }
        // Field 11 - Gender/Sex.  'M' = Male; 'F' = Female, 'U' = Unknown
        public string Field11_Sex { get; set; }

        // Field 12 - Admission Date / Start of Care Date.  This is the date that patient care actually begins.  For
        // inpatient care it is the admission date.  For other types it is the day the care begins.
        public string Field12_AdmissionDate { get; set; }

        // Field 13 - Admission Hour.  A two-digit code indicating the hour of day that the care began (when they were admitted).
        // Use military time (00 through 23).
        public string Field13_AdmissionHour { get; set; } 
        
        // Field 14 - Priority (Type) of Visit.  The code for the priority of the admission or visit.
        public string Field14_AdmissionType { get; set; }

        // Field 15 - Point of Origina / Source of Admission or Visit.  Indicates the source of the referral for visit or 
        // admission (e.g., physician, clinic, facility, transfer, etc.).  Usually a single alpha-numeric digit.
        public string Field15_AdmissionSource { get; set; }

        // Field 16 - Discharge Hour.  A two-digit code indicating the hour of day that the care ended (when they were discharged).
        // Use military time (00 through 23).
        public string Field16_DischargeHour { get; set; }
        
        /// <summary>
        /// Field 17 - Patient Discharge Status.  Reports status of patient upon discharge - required for institutional claims. 
        /// Two digit numeric.
        /// </summary>
        public string Field17_DischargeStatus { get; set; }
                
        // Field 18-28 - Condition Codes.
        public string Field18_ConditionCode01 { get; set; }
        public string Field19_ConditionCode02 { get; set; }
        public string Field20_ConditionCode03 { get; set; }
        public string Field21_ConditionCode04 { get; set; }
        public string Field22_ConditionCode05 { get; set; }
        public string Field23_ConditionCode06 { get; set; }
        public string Field24_ConditionCode07 { get; set; }
        public string Field25_ConditionCode08 { get; set; }
        public string Field26_ConditionCode09 { get; set; }
        public string Field27_ConditionCode10 { get; set; }
        public string Field28_ConditionCode11 { get; set; }

        // Field 29 - Accident State.  This is the state in which the accident occurred.  Situational.
        public string Field29_AccidentState { get; set; }

        public string Field30 { get; set; }

        // Field 31 through 34 are occurrence codes and their corresponding dates.
        public UB04Occurrence Field31a_Occurrence { get; set; }
        public UB04Occurrence Field31b_Occurrence { get; set; }
        
        public UB04Occurrence Field32a_Occurrence { get; set; }
        public UB04Occurrence Field32b_Occurrence { get; set; }
        
        public UB04Occurrence Field33a_Occurrence { get; set; }
        public UB04Occurrence Field33b_Occurrence { get; set; }

        public UB04Occurrence Field34a_Occurrence { get; set; }
        public UB04Occurrence Field34b_Occurrence { get; set; }
        

        // Field 35 and 36 are occurrence codes and their corresponding dates.
        public UB04OccurrenceSpan Field35a_OccurrenceSpan { get; set; }
        public UB04OccurrenceSpan Field35b_OccurrenceSpan { get; set; }
        public UB04OccurrenceSpan Field36a_OccurrenceSpan { get; set; }
        public UB04OccurrenceSpan Field36b_OccurrenceSpan { get; set; }
        
        public UB04Block Field37 { get; set; }
        
        // Field 38 - Additional name of the person or entity responsible for payment of balance of bill after applicable
        // processing by other parties, insurers or organizations.
        public UB04Block Field38_ResponsibleParty { get;  set; }

        // Field 39 through 41 - Value Codes and Amounts.
        public UB04Value Field39a_Value { get; set; }
        public UB04Value Field39b_Value { get; set; }
        public UB04Value Field39c_Value { get; set; }
        public UB04Value Field39d_Value { get; set; }
        public UB04Value Field40a_Value { get; set; }
        public UB04Value Field40b_Value { get; set; }
        public UB04Value Field40c_Value { get; set; }
        public UB04Value Field40d_Value { get; set; }
        public UB04Value Field41a_Value { get; set; }
        public UB04Value Field41b_Value { get; set; }
        public UB04Value Field41c_Value { get; set; }
        public UB04Value Field41d_Value { get; set; }
        
        /// <summary>
        /// Field 42 through 49, up to 22 service lines per page
        /// </summary>
        [XmlElement(ElementName="ServiceLine")]
        public List<UB04ServiceLine> ServiceLines { get; set; }

        // Field 47 - Summary of all field 47 charges
        public decimal? Field47_Line23_TotalCharges { get; set; }
        // Field 48 - Summary of all field 48 charges
        public decimal? Field48_Line23_NonCoveredCharges { get; set; }

        public UB04Payer PayerA_Primary { get; set; }
        public UB04Payer PayerB_Secondary { get; set; }
        public UB04Payer PayerC_Tertiary { get; set; }

        public string Field56_NationalProviderIdentifier { get; set; }
        
        // Field 57 - Other Provider Identifier.  Not required.
        public string Field57_OtherProviderIdA { get; set; }
        public string Field57_OtherProviderIdB { get; set; }
        public string Field57_OtherProviderIdC { get; set; }

        // Field 63a through 63c - Treatment authorization codes.
        public string Field63A_TreatmentAuthorizationCode { get; set; }
        public string Field63B_TreatmentAuthorizationCode { get; set; }
        public string Field63C_TreatmentAuthorizationCode { get; set; }
        
        // Field 64a through 64c - Document Control Number (DCN).
        public string Field64A_DocumentControlNumber { get; set; }
        public string Field64B_DocumentControlNumber { get; set; }
        public string Field64C_DocumentControlNumber { get; set; }

        public string Field65a_EmployerName { get; set; }
        public string Field65b_EmployerName { get; set; }
        public string Field65c_EmployerName { get; set; }
                
        public string Field66_Version { get; set; }
        
        // Field 67 - Principle diagnosis code.
        public UB04Diagnosis Field67_PrincipleDiagnosis { get; set; }
        public UB04Diagnosis Field67A_Diagnosis { get; set; }
        public UB04Diagnosis Field67B_Diagnosis { get; set; }
        public UB04Diagnosis Field67C_Diagnosis { get; set; }
        public UB04Diagnosis Field67D_Diagnosis { get; set; }
        public UB04Diagnosis Field67E_Diagnosis { get; set; }
        public UB04Diagnosis Field67F_Diagnosis { get; set; }
        public UB04Diagnosis Field67G_Diagnosis { get; set; }
        public UB04Diagnosis Field67H_Diagnosis { get; set; }
        public UB04Diagnosis Field67I_Diagnosis { get; set; }
        public UB04Diagnosis Field67J_Diagnosis { get; set; }
        public UB04Diagnosis Field67K_Diagnosis { get; set; }
        public UB04Diagnosis Field67L_Diagnosis { get; set; }
        public UB04Diagnosis Field67M_Diagnosis { get; set; }
        public UB04Diagnosis Field67N_Diagnosis { get; set; }
        public UB04Diagnosis Field67O_Diagnosis { get; set; }
        public UB04Diagnosis Field67P_Diagnosis { get; set; }
        public UB04Diagnosis Field67Q_Diagnosis { get; set; }
        
        public UB04Block Field68 { get; set; }

        public UB04Diagnosis Field69_AdmittingDiagnosisCode { get; set; }

        public UB04Diagnosis Field70a_PatientReasonDiagnosisCode { get; set; }
        public UB04Diagnosis Field70b_PatientReasonDiagnosisCode { get; set; }
        public UB04Diagnosis Field70c_PatientReasonDiagnosisCode { get; set; }
        
        /// <summary>
        /// Field 71 - Prospective Payment System (PPS) Code.  Identifies the DRG based on the grouper software
        /// and is required only when the provider is under contract with a health plan.
        /// </summary>
        public string Field71_PPSCode { get; set; }

        public UB04Diagnosis Field72a_ExternalCauseOfInjury { get; set; }
        public UB04Diagnosis Field72b_ExternalCauseOfInjury { get; set; }
        public UB04Diagnosis Field72c_ExternalCauseOfInjury { get; set; }
        
        public string Field73 { get; set; }

        public UB04Occurrence Field74_PrincipalProcedure { get; set; }
        public UB04Occurrence Field74a_OtherProcedure { get; set; }
        public UB04Occurrence Field74b_OtherProcedure { get; set; }
        public UB04Occurrence Field74c_OtherProcedure { get; set; }
        public UB04Occurrence Field74d_OtherProcedure { get; set; }
        public UB04Occurrence Field74e_OtherProcedure { get; set; }
        
        public UB04Block Field75 { get; set; }

        public UB04Provider Field76_AttendingPhysician { get; set; }
        public UB04Provider Field77_OperatingPhysician { get; set; }
        public UB04Provider Field78_OtherProvider { get; set; }
        public UB04Provider Field79_OtherProvider { get; set; }
                
        // Field 80 - Remarks Field.  This is a freeform entry field for special notes.
        public UB04Block Field80_Remarks { get; set; }

        public UB04CodeCode Field81a { get; set; }
        public UB04CodeCode Field81b { get; set; }
        public UB04CodeCode Field81c { get; set; }
        public UB04CodeCode Field81d { get; set; }

        #region Serialization Methods
        public string Serialize()
        {
            StringWriter writer = new StringWriter();
            new XmlSerializer(typeof(UB04Claim)).Serialize(writer, this);
            return writer.ToString();
        }

        public static UB04Claim Deserialize(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UB04Claim));
            return (UB04Claim)serializer.Deserialize(new StringReader(xml));
        }
        #endregion
    }

}
