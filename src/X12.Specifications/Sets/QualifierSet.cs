namespace X12.Specifications.Sets
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using X12.Specifications;
    using X12.Specifications.Enumerations;

    /// <summary>
    /// Represents a collection of allowed identifiers
    /// </summary>
    public class QualifierSet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QualifierSet"/> class
        /// </summary>
        public QualifierSet()
        {
            if (this.AllowedIdentifiers == null)
            {
                this.AllowedIdentifiers = new List<AllowedIdentifier>();
            }
        }

        /// <summary>
        /// Gets or sets the ID of the qualifier set
        /// </summary>
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the qualifier set
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the collection of allowed identifiers
        /// </summary>
        [XmlElement(ElementName = X12Elements.Allowed)]
        public List<AllowedIdentifier> AllowedIdentifiers { get; set; }
    }
}
