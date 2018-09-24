namespace X12.Specifications
{
    using System.Xml.Serialization;

    /// <summary>
    /// Gets or sets the allowed ID on a segment
    /// </summary>
    public class AllowedIdentifier
    {
        /// <summary>
        /// Gets or sets the allowed Id
        /// </summary>
        /// <remarks>
        /// Name must remain in all caps.
        /// Otherwise, ISA allowed qualifiers won't be properly identified
        /// </remarks>
        [XmlAttribute]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the Id description
        /// </summary>
        [XmlText]
        public string Description { get; set; }
    }
}
