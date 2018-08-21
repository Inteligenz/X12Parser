namespace OopFactory.X12.Hipaa.Common
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Contact
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class
        /// </summary>
        public Contact()
        {
            if (this.Numbers == null)
            {
                this.Numbers = new List<ContactNumber>();
            }
        }

        [XmlAttribute]
        public string FunctionCode { get; set; }

        public string Name { get; set; }

        [XmlElement(ElementName = "Number")]
        public List<ContactNumber> Numbers { get; set; }
    }

    public class ContactNumber
    {
        [XmlAttribute]
        public string Qualifier { get; set; }

        [XmlText]
        public string Number { get; set; }
    }
}
