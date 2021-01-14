namespace X12.Hipaa.Claims
{
    using System.Linq;
    using System.Xml.Serialization;

    using X12.Hipaa.Enums;
    
    public class Diagnosis
    {
        /// <summary>
        /// Gets the <see cref="DiagnosisType"/> value represented by the object's qualifier
        /// </summary>
        [XmlAttribute]
        public DiagnosisType DiagnosisType
        {
            get
            {
                switch (this.Qualifier)
                {
                    case "ABK":
                    case "BK":
                        return DiagnosisType.Principal;
                    case "ABJ":
                    case "BJ":
                        return DiagnosisType.Admitting;
                    case "APR":
                    case "PR":
                        return DiagnosisType.PatientReason;
                    case "ABN":
                    case "BN":
                        return DiagnosisType.ExternalCauseOfInjury;
                    case "ABF":
                    case "BF":
                        return DiagnosisType.Other;
                    default:
                        return DiagnosisType.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="CodeList"/> value represented by object's qualifier
        /// </summary>
        [XmlAttribute]
        public CodeList Version
        {
            get
            {
                switch (this.Qualifier)
                {
                    case "ABK":
                    case "ABJ":
                    case "APR":
                    case "ABN":
                    case "ABF":
                        return CodeList.ICD10;
                    case "BK":
                    case "BJ":
                    case "PR":
                    case "BN":
                    case "BF":
                        return CodeList.ICD9;
                    default:
                        return CodeList.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="PresentOnAdmission"/> value represented by the object's POI indicator
        /// </summary>
        [XmlAttribute]
        public PresentOnAdmission Poi
        {
            get
            {
                switch (this.PoiIndicator)
                {
                    case "N":
                        return PresentOnAdmission.No;
                    case "Y":
                        return PresentOnAdmission.Yes;
                    case "W":
                        return PresentOnAdmission.NotApplicable;
                    default:
                        return PresentOnAdmission.Unknown;
                }
            }
        }

        [XmlAttribute]
        public string Qualifier { get; set; }

        [XmlAttribute]
        public string Code { get; set; }

        [XmlAttribute]
        public string PoiIndicator { get; set; }

        /// <summary>
        /// Returns the object's code in a formatted string
        /// </summary>
        /// <returns>string representation with object's code</returns>
        public string FormattedCode()
        {
            if (string.IsNullOrWhiteSpace(this.Code) || this.Code.Length <= 3 || this.Code.Contains('.'))
            {
                return this.Code;
            }

            return this.Version == CodeList.ICD9 ? $"{this.Code.Substring(0, 3)}.{this.Code.Substring(3)}" : this.Code;
        }
    }
}
