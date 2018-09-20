namespace OopFactory.X12.Hipaa.Claims.Forms.Professional
{
    using System.Xml.Serialization;

    public class HCFA1500ServiceLine
    {
        /// <summary>
        /// Gets or sets the form comment line, 61 characters (in grey area from boxes 24A through 24G
        /// </summary>
        public string CommentLine { get; set; } 

        /// <summary>
        /// Gets or sets the date the service began (MMDDCCYY)
        /// </summary>
        public FormDate DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the date the service ends (MMDDCCYY)
        /// </summary>
        public FormDate DateTo { get; set; }

        /// <summary>
        /// Gets or sets the place of service, 2 digits
        /// </summary>
        [XmlAttribute]
        public string PlaceOfService { get; set; }

        /// <summary>
        /// Gets or sets the emergency indicator, 2 digits
        /// </summary>
        [XmlAttribute]
        public string EmergencyIndicator { get; set; }

        /// <summary>
        /// Gets or sets the procedure code, 6 digits
        /// </summary>
        [XmlAttribute]
        public string ProcedureCode { get; set; }

        /// <summary>
        /// Gets or sets the mod1 field, 2 digits
        /// </summary>
        [XmlAttribute]
        public string Mod1 { get; set; }

        /// <summary>
        /// Gets or sets the mod2 field, 2 digits
        /// </summary>
        [XmlAttribute]
        public string Mod2 { get; set; }

        /// <summary>
        /// Gets or sets the mod3 field, 2 digits
        /// </summary>
        [XmlAttribute]
        public string Mod3 { get; set; }

        /// <summary>
        /// Gets or sets the mod4 field, 2 digits
        /// </summary>
        [XmlAttribute]
        public string Mod4 { get; set; }

        [XmlAttribute]
        public string DiagnosisPointer1 { get; set; }

        [XmlAttribute]
        public string DiagnosisPointer2 { get; set; }

        [XmlAttribute]
        public string DiagnosisPointer3 { get; set; }

        [XmlAttribute]
        public string DiagnosisPointer4 { get; set; }

        public decimal? Charges { get; set; }

        /// <summary>
        /// Gets or sets the days or units, 3 characters
        /// </summary>
        public decimal? DaysOrUnits { get; set; }

        [XmlAttribute]
        public string EarlyPeriodicScreeningDiagnosisAndTreatment { get; set; } // 2 characters

        [XmlAttribute]
        public string RenderingProviderIdQualifier { get; set; }

        /// <summary>
        /// Gets or sets the rendering provider identifier, 11 characters
        /// </summary>
        [XmlAttribute]
        public string RenderingProviderId { get; set; }

        [XmlAttribute]
        public string RenderingProviderNpi { get; set; } // 10 characters
    }
}
