namespace OopFactory.X12.Specifications.Sets
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using OopFactory.X12.Specifications;

    [XmlRoot(Namespace = "http://tempuri.org/X12ParserSpecification.xsd")]
    public class SegmentSet
    {
        public SegmentSet()
        {
            if (this.QualifierSets == null)
            {
                this.QualifierSets = new List<QualifierSet>();
            }

            if (this.Segments == null)
            {
                this.Segments = new List<SegmentSpecification>();
            }
        }

        public string Name { get; set; }

        [XmlElement("QualifierSet")]
        public List<QualifierSet> QualifierSets { get; set; }
        
        [XmlElement("Segment")]
        public List<SegmentSpecification> Segments { get; set; }

        public string Serialize()
        {
            var xmlSerializer = new XmlSerializer(typeof(SegmentSet));
            var mstream = new MemoryStream();
            xmlSerializer.Serialize(mstream, this);
            mstream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(mstream);
            return streamReader.ReadToEnd();
        }

        public static SegmentSet Deserialize(string xml)
        {
            var stringReader = new StringReader(xml);
            var xmlTextReader = new System.Xml.XmlTextReader(stringReader);
            var xmlSerializer = new XmlSerializer(typeof(SegmentSet));
           
            return (SegmentSet)xmlSerializer.Deserialize(xmlTextReader);
        }
    }
}
