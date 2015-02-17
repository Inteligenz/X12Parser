using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace OopFactory.X12.Parsing.Specification
{
    [XmlRoot(Namespace="http://tempuri.org/X12ParserSpecification.xsd")]
    public class SegmentSet
    {
        public SegmentSet()
        {
            if (QualifierSets == null) QualifierSets = new List<QualifierSet>();
            if (Segments == null) Segments = new List<SegmentSpecification>();
        }

        public string Name { get; set; }

        [XmlElement("QualifierSet")]
        public List<QualifierSet> QualifierSets { get; set; }
        
        [XmlElement("Segment")]
        public List<SegmentSpecification> Segments { get; set; }

        public string Serialize()
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(SegmentSet));
            System.IO.MemoryStream mstream = new System.IO.MemoryStream();
            xmlSerializer.Serialize(mstream, this);
            mstream.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader streamReader = new StreamReader(mstream);
            return streamReader.ReadToEnd();
        }

        public static SegmentSet Deserialize(string xml)
        {
            System.IO.StringReader stringReader = new System.IO.StringReader(xml);
            System.Xml.XmlTextReader xmlTextReader = new System.Xml.XmlTextReader(stringReader);
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(SegmentSet));
           
            return ((SegmentSet)(xmlSerializer.Deserialize(xmlTextReader)));
        }
    }
}
