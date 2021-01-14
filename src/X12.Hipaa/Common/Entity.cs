namespace X12.Hipaa.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a base unit which stores common data
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class
        /// </summary>
        public Entity()
        {
            if (this.Name == null)
            {
                this.Name = new EntityName();
            }

            if (this.Identifications == null)
            {
                this.Identifications = new List<Identification>();
            }

            if (this.Contacts == null)
            {
                this.Contacts = new List<Contact>();
            }

            if (this.RequestValidations != null)
            {
                this.RequestValidations = new List<RequestValidation>();
            }
        }

        /// <summary>
        /// Gets or sets the name of the <see cref="Entity"/>
        /// </summary>
        public EntityName Name { get; set; }

        /// <summary>
        /// Gets or sets the address of the <see cref="Entity"/>
        /// </summary>
        public PostalAddress Address { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Identification"/> objects
        /// </summary>
        [XmlElement(ElementName = "Identification")]
        public List<Identification> Identifications { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Contact"/> collection
        /// </summary>
        [XmlElement(ElementName = "Contact")]
        public List<Contact> Contacts { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="RequestValidation"/> collection
        /// </summary>
        [XmlElement(ElementName = "RequestValidation")]
        public List<RequestValidation> RequestValidations { get; set; }

        /// <summary>
        /// Returns the reference id with a matching qualifier
        /// </summary>
        /// <param name="qualifier">Filter to get the correct reference id</param>
        /// <returns>The id of the reference which matches the qualifier; otherwise, null</returns>
        protected string GetReferenceId(string qualifier)
        {
            var reference = this.Identifications.FirstOrDefault(id => id.Qualifier == qualifier);
            return reference?.Id;
        }
    }
}
