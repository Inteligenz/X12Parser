namespace X12.Specifications
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using X12.Specifications.Enumerations;

    /// <summary>
    /// Represents the element information
    /// </summary>
    public class ElementSpecification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementSpecification"/> class
        /// </summary>
        public ElementSpecification()
        {
            if (this.AllowedIdentifiers == null)
            {
                this.AllowedIdentifiers = new List<AllowedIdentifier>();
            }
        }

        /// <summary>
        /// Gets or sets the element name
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the element reference
        /// </summary>
        [XmlAttribute]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the element is required
        /// </summary>
        [XmlAttribute]
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the element data type
        /// </summary>
        [XmlAttribute]
        public ElementDataType Type { get; set; }

        /// <summary>
        /// Gets or sets the implied decimal places, for decimal elements
        /// </summary>
        [XmlAttribute]
        public int ImpliedDecimalPlaces { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of the element
        /// </summary>
        [XmlAttribute]
        public int MinLength { get; set; }

        /// <summary>
        /// Gets or sets the max length of the element
        /// </summary>
        [XmlAttribute]
        public int MaxLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the element is composite
        /// </summary>
        [XmlAttribute]
        public bool IsComposite { get; set; }

        /// <summary>
        /// Gets or sets the max number of components in the element
        /// </summary>
        [XmlAttribute]
        public int MaxComponents { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the allowed identifiers list is inclusive
        /// </summary>
        [XmlAttribute]
        public bool AllowedListInclusive { get; set; }

        /// <summary>
        /// Gets or sets the qualifier set reference string
        /// </summary>
        [XmlAttribute]
        public string QualifierSetRef { get; set; }

        /// <summary>
        /// Gets or sets the qualifier set Id string
        /// </summary>
        [XmlAttribute]
        public string QualifierSetId { get; set; }

        /// <summary>
        /// Gets or sets the collection of allowed identifiers
        /// </summary>
        [XmlElement(ElementName = X12Elements.Allowed)]
        public List<AllowedIdentifier> AllowedIdentifiers { get; set; }
    }
}
