namespace OopFactory.X12.Hipaa.Claims
{
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;

    public class DrugIdentification
    {
        [XmlAttribute]
        public string Ndc { get; set; }

        [XmlAttribute]
        public decimal Quantity { get; set; }

        public Lookup UnitOfMeasure { get; set; }

        public Identification Identification { get; set; }
    }
}
