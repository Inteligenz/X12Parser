using System;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Claims.Forms.Professional
{
    public class HCFA1500ServiceLine
    {
        public string CommentLine { get; set; }             // 61 characters (in grey area from boxes 24A through 24G
        public FormDate DateFrom { get; set; }             // MMDDCCYY
        public FormDate DateTo { get; set; }               // MMDDCCYY
        [XmlAttribute]
        public string PlaceOfService { get; set; }        // 2 digits
        [XmlAttribute]
        public string EmergencyIndicator { get; set; }    // 2 digits
        [XmlAttribute]
        public string ProcedureCode { get; set; }          // 6 digits
        [XmlAttribute]
        public string Mod1 { get; set; }                  // 2 digits
        [XmlAttribute]
        public string Mod2 { get; set; }                   // 2 digits
        [XmlAttribute]
        public string Mod3 { get; set; }                  // 2 digits
        [XmlAttribute]
        public string Mod4 { get; set; }                  // 2 digits
        [XmlAttribute]
        public string DiagnosisPointer1 { get; set; }
        [XmlAttribute]
        public string DiagnosisPointer2 { get; set; }
        [XmlAttribute]
        public string DiagnosisPointer3 { get; set; }
        [XmlAttribute]
        public string DiagnosisPointer4 { get; set; }
        public decimal? Charges { get; set; }
        public decimal? DaysOrUnits { get; set; }           // 3 characters
        [XmlAttribute]
        public string EarlyPeriodicScreeningDiagnosisAndTreatment { get; set; }   // 2 characters
        [XmlAttribute]
        public string RenderingProviderIdQualifier { get; set; }
        [XmlAttribute]
        public string RenderingProviderId { get; set; }   // 11 characters
        [XmlAttribute]
        public string RenderingProviderNpi { get; set; }  // 10 characters
    }

}
