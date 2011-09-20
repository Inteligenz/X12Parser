using System;

namespace OopFactory.X12.Hipaa.Claims.Forms.Professional
{
    public class HCFA1500ServiceLine
    {
        public string Field24_CommentLine { get; set; }             // 61 characters (in grey area from boxes 24A through 24G
        public DateTime? Field24a_DateFrom { get; set; }             // MMDDCCYY
        public DateTime? Field24a_DateTo { get; set; }               // MMDDCCYY
        public string Field24b_PlaceOfService { get; set; }        // 2 digits
        public string Field24c_EmergencyIndicator  { get; set; }    // 2 digits
        public string Field24d_ProcedureCode { get; set; }          // 6 digits
        public string Field24d_Mod1  { get; set; }                  // 2 digits
        public string Field24d_Mod2 { get; set; }                   // 2 digits
        public string Field24d_Mod3  { get; set; }                  // 2 digits
        public string Field24d_Mod4  { get; set; }                  // 2 digits
        public string Field24e_DiagnosisPointer1  { get; set; }
        public string Field24e_DiagnosisPointer2 { get; set; }
        public string Field24e_DiagnosisPointer3 { get; set; }
        public string Field24e_DiagnosisPointer4 { get; set; }
        public decimal? Field24f_Charges { get; set; }
        public decimal Field24g_DaysOrUnits  { get; set; }           // 3 characters
        public string Field24h_EarlyPeriodicScreeningDiagnosisAndTreatment  { get; set; }   // 2 characters
        public string Field24i_RenderingProviderIdQualifier  { get; set; }
        public string Field24j_RenderingProviderId  { get; set; }   // 11 characters
        public string Field24j_RenderingProviderNpi  { get; set; }  // 10 characters
    }

}
