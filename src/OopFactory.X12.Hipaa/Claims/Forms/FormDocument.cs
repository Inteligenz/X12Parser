namespace OopFactory.X12.Hipaa.Claims.Forms
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Enums;

    /// <summary>
    /// Represents a single collection of <see cref="FormPage"/> objects
    /// </summary>
    public class FormDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDocument"/> class
        /// </summary>
        public FormDocument()
        {
            if (this.Pages == null)
            {
                this.Pages = new List<FormPage>();
            }
        }

        /// <summary>
        /// Gets or sets the collection of <see cref="FormPage"/> objects that make up the document
        /// </summary>
        [XmlElement(ElementName = FormElements.Page)]
        public List<FormPage> Pages { get; set; }

        /// <summary>
        /// Converts an XML string into its equivalent <see cref="FormDocument"/> object
        /// </summary>
        /// <param name="xml">String data to be deserialized</param>
        /// <returns>Representative <see cref="FormDocument"/> object</returns>
        public static FormDocument Deserialize(string xml)
        {
            var serializer = new XmlSerializer(typeof(FormDocument));
            return (FormDocument)serializer.Deserialize(new StringReader(xml));
        }

        /// <summary>
        /// Converts the object into its XML equivalent
        /// </summary>
        /// <returns>XML string that represents the object</returns>
        public string Serialize()
        {
            var writer = new StringWriter();
            new XmlSerializer(typeof(FormDocument)).Serialize(writer, this);
            return writer.ToString();
        }
    }
}
