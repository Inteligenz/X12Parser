namespace OopFactory.X12.Hipaa.Common
{
    using System;
    using System.Xml.Serialization;

    public class CodedDateRange
    {
        [XmlAttribute]
        public string Code { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime FromDate { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime ThroughDate { get; set; }
    }
}
