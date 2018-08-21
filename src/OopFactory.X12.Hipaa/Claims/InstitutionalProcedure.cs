namespace OopFactory.X12.Hipaa.Claims
{
    using System;
    using System.Linq;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;

    public class InstitutionalProcedure
    {
        [XmlAttribute]
        public bool IsPrincipal => new[] { "BBR", "BR", "CAH" }.Contains(this.Qualifier);

        [XmlAttribute]
        public CodeListEnum Version
        {
            get
            {
                switch (this.Qualifier)
                {
                    case "BBR":
                    case "BBQ":
                        return CodeListEnum.ICD10;
                    case "BR":
                    case "BQ":
                        return CodeListEnum.ICD9;
                    case "CAH":
                        return CodeListEnum.ABC;
                    default:
                        return CodeListEnum.Unknown;
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
