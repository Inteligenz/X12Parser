namespace OopFactory.X12.Specifications
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
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Id description
        /// </summary>
        [XmlText]
        public string Description { get; set; }
    }
}
