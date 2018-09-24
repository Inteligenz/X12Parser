namespace X12.Specifications.Sets
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using X12.Specifications;
    using X12.Specifications.Enumerations;

    /// <summary>
    /// Represents a collection of segment and qualifier set objects
    /// </summary>
    [XmlRoot(Namespace = "http://tempuri.org/X12ParserSpecification.xsd")]
    public class SegmentSet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SegmentSet"/> class
        /// </summary>
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

        /// <summary>
        /// Gets or sets the name of the segment set
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of qualifier sets
        /// </summary>
        [XmlElement(X12Elements.QualifierSet)]
        public List<QualifierSet> QualifierSets { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of segments in the set
        /// </summary>
        [XmlElement(X12Elements.Segment)]
        public List<SegmentSpecification> Segments { get; set; }

        /// <summary>
        /// Deserializes an XML string into its <see cref="SegmentSet"/> representation
        /// </summary>
        /// <param name="xml">XML string to be parsed</param>
        /// <returns>Segment set deserialized from the XML string</returns>
        public static SegmentSet Deserialize(string xml)
        {
            var stringReader = new StringReader(xml);
            var xmlTextReader = new System.Xml.XmlTextReader(stringReader);
            var xmlSerializer = new XmlSerializer(typeof(SegmentSet));

            return (SegmentSet)xmlSerializer.Deserialize(xmlTextReader);
        }

        /// <summary>
        /// Serializes a new instance of the <see cref="SegmentSet"/> class
        /// </summary>
        /// <returns>XML string representing the segment set</returns>
        public string Serialize()
        {
            var xmlSerializer = new XmlSerializer(typeof(SegmentSet));
            var mstream = new MemoryStream();
            xmlSerializer.Serialize(mstream, this);
            mstream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(mstream);
            return streamReader.ReadToEnd();
        }
    }
}
