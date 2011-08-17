using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Professional
{
    class HCFA1500ServiceLines
    {
        string _field24_CommentLine;                                    // 61 characters (in grey area from boxes 24A through 24G
        string _field24a_DateFrom;                                      // MMDDCCYY
        string _field24a_DateTo;                                        // MMDDCCYY
        string _field24b_PlaceOfService;                                // 2 digits
        string _field24c_EmergencyIndicator;                            // 2 digits
        string _field24d_ProcedureCode;                                 // 6 digits
        string _field24d_Mod1;                                          // 2 digits
        string _field24d_Mod2;                                          // 2 digits
        string _field24d_Mod3;                                          // 2 digits
        string _field24d_Mod4;                                          // 2 digits
        string _field24e_DiagnosisIndicator;                            // 4 digits
        string _field24f_ChargesDollars;                                // 6 digits
        string _field24f_ChargesCents;                                  // 2 digits
        string _field24g_DaysOrUnits;                                   // 3 characters
        string _field24h_EarlyPeriodicScreeningDiagnosisAndTreatment;   // 2 characters
        string _field24i_ID_Qualifier;                                  // 2 characters
        string _field24j_RenderingProviderID;                           // 11 characters
        string _field24j_RenderingProviderNPI_ID;                       // 10 characters

        public string Field24_CommentLine
        {
            get { return _field24_CommentLine; }
            set { _field24_CommentLine = value; }
        }

        public string Field24a_DateFrom
        {
            get { return _field24a_DateFrom; }
            set { _field24a_DateFrom = value; }
        }

        public string Field24a_DateTo
        {
            get { return _field24a_DateTo; }
            set { _field24a_DateTo = value; }
        }

        public string _Field24b_PlaceOfService
        {
            get { return _field24b_PlaceOfService; }
            set { _field24b_PlaceOfService = value; }
        }

        
        public string Field24c_EmergencyIndicator 
        {
            get { return _field24c_EmergencyIndicator; }
            set { _field24c_EmergencyIndicator = value; } 
        }

        public string Field24d_ProcedureCode
        {
            get { return _field24d_ProcedureCode; }
            set { _field24d_ProcedureCode = value; } 
        }

        public string Field24d_Mod1 
        { 
            get { return _field24d_Mod1; } 
            set { _field24d_Mod1 = value; }
        }

        public string Field24d_Mod2
        { 
            get { return _field24d_Mod2; } 
            set { _field24d_Mod2 = value; }
        }

        public string Field24d_Mod3 
        {
            get { return _field24d_Mod3; }
            set { _field24d_Mod3 = value; } 
        }

        public string Field24d_Mod4 
        { 
            get { return _field24d_Mod4; } 
            set { _field24d_Mod4 = value; } 
        }

        public string Field24e_DiagnosisIndicator 
        {
            get { return _field24e_DiagnosisIndicator; }
            set { _field24e_DiagnosisIndicator = value; }
        }

        public string Field24f_ChargesDollars
        {
            get { return _field24f_ChargesDollars; } 
            set { _field24f_ChargesDollars = value; }
        }

        public string Field24f_ChargesCents
        { 
            get { return _field24f_ChargesCents; }
            set { _field24f_ChargesCents = value; } 
        }

        public string Field24g_DaysOrUnits 
        {
            get { return _field24g_DaysOrUnits; } 
            set { _field24g_DaysOrUnits = value; } 
        }

        public string Field24h_EarlyPeriodicScreeningDiagnosisAndTreatment 
        {
            get { return Field24h_EarlyPeriodicScreeningDiagnosisAndTreatment; } 
            set { _field24h_EarlyPeriodicScreeningDiagnosisAndTreatment = value; } 
        }

        public string Field24i_ID_Qualifier 
        { 
            get { return _field24i_ID_Qualifier; } 
            set { _field24i_ID_Qualifier = value; }
        }
        
        public string Field24j_RenderingProviderID 
        { 
            get { return _field24j_RenderingProviderID; } 
            set { _field24j_RenderingProviderID = value; } 
        }
        
        public string Field24j_RenderingProviderNPI_ID 
        { 
            get { return _field24j_RenderingProviderNPI_ID; } 
            set { _field24j_RenderingProviderNPI_ID = value; } 
        }

    }

    class HCFA1500DiagCodes
    {
        string _diagPart1;                                              // 3 characters
        string _diagPart2;                                              // 1 digit
        string _diagPart3;                                              // 4 digits

        public string DiagPart1
        {
            get { return _diagPart1; }
            set { _diagPart1 = value; }
        }

        public string DiagPart2
        {
            get { return _diagPart2; }
            set { _diagPart2 = value; }
        }

        public string DiagPart3
        {
            get { return _diagPart3; }
            set { _diagPart3 = value; }
        }
    }
}
