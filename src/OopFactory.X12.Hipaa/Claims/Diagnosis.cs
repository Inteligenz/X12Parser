namespace OopFactory.X12.Hipaa.Claims
{
    using System.Linq;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;

    public enum DiagnosisTypeEnum
    {
        Unknown,
        Principal,
        Admitting,
        PatientReason,
        ExternalCauseOfInjury,
        Other
    }

    public enum PresentOnAdmissionEnum
    {
        Unknown,
        No,
        Yes,
        NotApplicable
    }

    public class Diagnosis
    {
        /// <summary>
        /// Gets the <see cref="DiagnosisTypeEnum"/> value represented by the object's qualifier
        /// </summary>
        [XmlAttribute]
        public DiagnosisTypeEnum DiagnosisType
        {
            get
            {
                switch (this.Qualifier)
                {
                    case "ABK":
                    case "BK":
                        return DiagnosisTypeEnum.Principal;
                    case "ABJ":
                    case "BJ":
                        return DiagnosisTypeEnum.Admitting;
                    case "APR":
                    case "PR":
                        return DiagnosisTypeEnum.PatientReason;
                    case "ABN":
                    case "BN":
                        return DiagnosisTypeEnum.ExternalCauseOfInjury;
                    case "ABF":
                    case "BF":
                        return DiagnosisTypeEnum.Other;
                    default:
                        return DiagnosisTypeEnum.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="CodeListEnum"/> value represented by object's qualifier
        /// </summary>
        [XmlAttribute]
        public CodeListEnum Version
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
                        return CodeListEnum.ICD10;
                    case "BK":
                    case "BJ":
                    case "PR":
                    case "BN":
                    case "BF":
                        return CodeListEnum.ICD9;
                    default:
                        return CodeListEnum.Unknown;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="PresentOnAdmissionEnum"/> value represented by the object's POI indicator
        /// </summary>
        [XmlAttribute]
        public PresentOnAdmissionEnum Poi
        {
            get
            {
                switch (this.PoiIndicator)
                {
                    case "N":
                        return PresentOnAdmissionEnum.No;
                    case "Y":
                        return PresentOnAdmissionEnum.Yes;
                    case "W":
                        return PresentOnAdmissionEnum.NotApplicable;
                    default:
                        return PresentOnAdmissionEnum.Unknown;
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
            else if (this.Version == CodeListEnum.ICD9)
            {
                return $"{this.Code.Substring(0, 3)}.{this.Code.Substring(3)}";
            }
            else
            {
                return this.Code;
            }
        }
    }
}
