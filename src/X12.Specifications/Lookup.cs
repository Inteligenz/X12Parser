namespace X12.Specifications
{
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a lookup object with an associated lookup code
    /// </summary>
    [DebuggerStepThrough]
    [XmlType(AnonymousType = true)]
    public class Lookup
    {
        /// <summary>
        /// Gets or sets the lookup code
        /// </summary>
        [XmlAttribute]
        public string Code { get; set; }
    }
}
