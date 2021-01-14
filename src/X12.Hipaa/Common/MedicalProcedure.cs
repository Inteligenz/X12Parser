namespace X12.Hipaa.Common
{
    using System.Xml.Serialization;

    public class MedicalProcedure
    {
        [XmlAttribute]        
        public string Qualifier { get; set; }

        [XmlAttribute]
        public string ProcedureCode { get; set; }

        [XmlAttribute]
        public string ProcedureCodeEnd { get; set; }

        [XmlAttribute]
        public string Modifier1 { get; set; }

        [XmlAttribute]
        public string Modifier2 { get; set; }

        [XmlAttribute]
        public string Modifier3 { get; set; }

        [XmlAttribute]
        public string Modifier4 { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}
