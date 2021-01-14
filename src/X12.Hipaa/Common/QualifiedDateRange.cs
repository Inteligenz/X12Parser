namespace X12.Hipaa.Common
{
    using System;
    using System.Xml.Serialization;

    public class QualifiedDateRange
    {
        [XmlAttribute]
        public string Qualifier { get; set; }

        [XmlAttribute(DataType="date")]
        public DateTime BeginDate { get; set; }

        [XmlAttribute(DataType="date")]
        public DateTime EndDate { get; set; }

        [XmlText]
        public string Description { get; set; }
    }
}
