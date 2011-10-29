using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
#if DEBUG
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
            if (ServiceLines == null) ServiceLines = new List<UB04ServiceLine>(); 
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
        
        public string Field68 { get; set; }

        public string Field69_AdmittingDiagnosis { get; set; }
        
        public string Field70a_PatientReasonDiagnosis { get; set ; }
        public string Field70b_PatientReasonDiagnosis { get; set; }
        public string Field70c_PatientReasonDiagnosis { get; set; }
        
        /// <summary>
        /// Field 71 - Prospective Payment System (PPS) Code.  Identifies the DRG based on the grouper software
        /// and is required only when the provider is under contract with a health plan.
        /// </summary>
        public string Field71_PPSCode { get; set; }
        
        public string Field72a_ExternalCauseOfInjury { get; set; }
        public string Field72a_ExternalCauseOfInjury_POA { get; set; }
        public string Field72b_ExternalCauseOfInjury { get; set; }
        public string Field72b_ExternalCauseOfInjury_POA { get; set; }
        public string Field72c_ExternalCauseOfInjury { get; set; }
        public string Field72c_ExternalCauseOfInjury_POA { get; set; }

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
                
    }
#endif
}
