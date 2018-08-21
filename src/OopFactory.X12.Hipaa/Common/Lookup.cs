namespace OopFactory.X12.Hipaa.Common
{
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a code-description object for storing metadata
    /// </summary>
    public class Lookup
    {
        /// <summary>
        /// Gets or sets the unique code representing the object
        /// </summary>
        [XmlAttribute]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the description of the object
        /// </summary>
        [XmlText]
        public string Description { get; set; }
    }
}
