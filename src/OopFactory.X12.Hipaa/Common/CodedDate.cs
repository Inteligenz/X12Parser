namespace OopFactory.X12.Hipaa.Common
{
    using System;
    using System.Xml.Serialization;

    public class CodedDate
    {
        [XmlAttribute]
        public string Code { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime Date { get; set; }
    }
}
