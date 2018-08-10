namespace OopFactory.X12.Specifications
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Xml.Serialization;

    using OopFactory.X12.Specifications.Interfaces;

    [DebuggerStepThrough]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "http://tempuri.org/X12ParserSpecification.xsd")]
    public class TransactionSpecification : IContainerSpecification
    {
        [XmlAttribute]
        public string TransactionSetIdentifierCode { get; set; }

        [XmlAttribute]
        public string FunctionalGroupCode { get; set; }
        
        [XmlElement]
        public string Name { get; set; }
        
        [XmlElement("Segment")]
        public List<SegmentSpecification> SegmentSpecifications { get; set; }

        [XmlElement("Loop")]
        public List<LoopSpecification> LoopSpecifications { get; set; }

        [XmlElement("HierarchicalLoop")]
        public List<HierarchicalLoopSpecification> HierarchicalLoopSpecifications { get; set; }

        public string Serialize()
        {
            var xmlSerializer = new XmlSerializer(typeof(TransactionSpecification));
            var mstream = new MemoryStream();
            xmlSerializer.Serialize(mstream, this);
            mstream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(mstream);
            return streamReader.ReadToEnd();
        }

        public static TransactionSpecification Deserialize(string xml)
        {
            var stringReader = new StringReader(xml);
            var xmlTextReader = new System.Xml.XmlTextReader(stringReader);
            var xmlSerializer = new XmlSerializer(typeof(TransactionSpecification));
            return (TransactionSpecification)xmlSerializer.Deserialize(xmlTextReader);
        }

        string IContainerSpecification.LoopId => string.Empty;
    }
}
