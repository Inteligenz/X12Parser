namespace X12.Hipaa.Claims
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a claims document
    /// </summary>
    [XmlRoot(Namespace = "http://www.oopfactory.com/2011/XSL/Hipaa")]
    public class ClaimDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimDocument"/> class
        /// </summary>
        public ClaimDocument()
        {
            if (this.Claims == null)
            {
                this.Claims = new List<Claim>();
            }
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="Claim"/> objects
        /// </summary>
        [XmlElement(ElementName = Enums.FormElements.Claim)]
        public List<Claim> Claims { get; set; }

        /// <summary>
        /// Deserializes an XML representation of an object and returns the <see cref="ClaimDocument"/>
        /// </summary>
        /// <param name="xml">string representation of object to deserialize</param>
        /// <returns><see cref="ClaimDocument"/> represented by string</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if string does not contain a valid <see cref="ClaimDocument"/></exception>
        public static ClaimDocument Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(ClaimDocument));
            return (ClaimDocument)serializer.Deserialize(new StringReader(xml));
        }

        /// <summary>
        /// Serializes the object to XML and returns the <c>string</c> representation
        /// </summary>
        /// <returns>String representation of the object in XML</returns>
        public string Serialize()
        {
            var writer = new StringWriter();
            new XmlSerializer(typeof(ClaimDocument)).Serialize(writer, this);
            return writer.ToString();
        }
    }
}
