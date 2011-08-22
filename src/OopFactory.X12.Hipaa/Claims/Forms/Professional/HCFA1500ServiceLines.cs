using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Professional
{
    class HCFA1500ServiceLines
    {
        //string _field24_CommentLine;                                    // 61 characters (in grey area from boxes 24A through 24G
        //string _field24a_DateFrom;                                      // MMDDCCYY
        //string _field24a_DateTo;                                        // MMDDCCYY
        //string _field24b_PlaceOfService;                                // 2 digits
        //string _field24c_EmergencyIndicator;                            // 2 digits
        //string _field24d_ProcedureCode;                                 // 6 digits
        //string _field24d_Mod1;                                          // 2 digits
        //string _field24d_Mod2;                                          // 2 digits
        //string _field24d_Mod3;                                          // 2 digits
        //string _field24d_Mod4;                                          // 2 digits
        //string _field24e_DiagnosisIndicator;                            // 4 digits
        //string _field24f_ChargesDollars;                                // 6 digits
        //string _field24f_ChargesCents;                                  // 2 digits
        //string _field24g_DaysOrUnits;                                   // 3 characters
        //string _field24h_EarlyPeriodicScreeningDiagnosisAndTreatment;   // 2 characters
        //string _field24i_ID_Qualifier;                                  // 2 characters
        //string _field24j_RenderingProviderID;                           // 11 characters
        //string _field24j_RenderingProviderNPI_ID;                       // 10 characters

        public string Field24_CommentLine { get; set; }
        public string Field24a_DateFrom { get; set; }
        public string Field24a_DateTo { get; set; }
        public string _Field24b_PlaceOfService { get; set; }
        public string Field24c_EmergencyIndicator  { get; set; }
        public string Field24d_ProcedureCode { get; set; }
        public string Field24d_Mod1  { get; set; }
        public string Field24d_Mod2 { get; set; }
        public string Field24d_Mod3  { get; set; }
        public string Field24d_Mod4  { get; set; }
        public string Field24e_DiagnosisIndicator  { get; set; }
        public string Field24f_ChargesDollars { get; set; }
        public string Field24f_ChargesCents { get; set; }
        public string Field24g_DaysOrUnits  { get; set; }
        public string Field24h_EarlyPeriodicScreeningDiagnosisAndTreatment  { get; set; }
        public string Field24i_ID_Qualifier  { get; set; }
        public string Field24j_RenderingProviderID  { get; set; }
        public string Field24j_RenderingProviderNPI_ID  { get; set; }
    }

    class HCFA1500DiagCodes
    {
        //string _diagPart1;                                              // 3 characters
        //string _diagPart2;                                              // 1 digit
        //string _diagPart3;                                              // 4 digits

        public string DiagPart1 { get; set; }
        public string DiagPart2 { get; set; }
        public string DiagPart3 { get; set; }
    }
}
