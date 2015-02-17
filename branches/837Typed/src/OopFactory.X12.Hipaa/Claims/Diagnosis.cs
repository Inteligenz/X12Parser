using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
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
        [XmlAttribute]
        public DiagnosisTypeEnum DiagnosisType
        {
            get
            {
                switch (Qualifier)
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
            set { }
        }

        [XmlAttribute]
        public CodeListEnum Version
        {
            get
            {
                switch (Qualifier)
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
            set { }
        }

        [XmlAttribute]
        public string Qualifier { get; set; }

        [XmlAttribute]
        public string Code { get; set; }

        public string FormattedCode()
        {
            if (string.IsNullOrWhiteSpace(Code) || Code.Length <= 3 || Code.Contains('.'))
                return Code;
            else if (Version == CodeListEnum.ICD9)
                return String.Format("{0}.{1}", Code.Substring(0, 3), Code.Substring(3));
            else
                return Code;

        }

        [XmlAttribute]
        public string PoiIndicator { get; set; }

        [XmlAttribute]
        public PresentOnAdmissionEnum Poi
        {
            get
            {
                switch (PoiIndicator)
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
            set { }
        }
    }
}
