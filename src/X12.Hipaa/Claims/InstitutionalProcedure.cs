namespace X12.Hipaa.Claims
{
    using System;
    using System.Linq;
    using System.Xml.Serialization;
    
    using X12.Hipaa.Enums;

    public class InstitutionalProcedure
    {
        [XmlAttribute]
        public bool IsPrincipal => new[] { "BBR", "BR", "CAH" }.Contains(this.Qualifier);

        [XmlAttribute]
        public CodeList Version
        {
            get
            {
                switch (this.Qualifier)
                {
                    case "BBR":
                    case "BBQ":
                        return CodeList.ICD10;
                    case "BR":
                    case "BQ":
                        return CodeList.ICD9;
                    case "CAH":
                        return CodeList.ABC;
                    default:
                        return CodeList.Unknown;
                }
            }
        }

        [XmlAttribute]
        public string Qualifier { get; set; }

        [XmlAttribute]
        public string Code { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime Date { get; set; }
    }
}
