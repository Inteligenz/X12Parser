namespace OopFactory.X12.Specifications
{
    using System.Diagnostics;
    using System.Xml.Serialization;

    [DebuggerStepThrough()]
    [XmlType(AnonymousType = true)]
    public class Lookup
    {
        [XmlAttribute]
        public string Code { get; set; }
    }
}
