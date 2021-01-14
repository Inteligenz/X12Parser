namespace X12.Hipaa.Claims.Forms
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using X12.Hipaa.Enums;

    /// <summary>
    /// Represents a single page of a form
    /// </summary>
    public class FormPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormPage"/> class
        /// </summary>
        public FormPage()
        {
            if (this.Blocks == null)
            {
                this.Blocks = new List<FormBlock>();
            }
        }

        public string MasterReference { get; set; }

        /// <summary>
        /// Gets or sets the path to the form page image
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="FormBlock"/> objects that make up the form page
        /// </summary>
        [XmlElement(ElementName = FormElements.Block)]
        public List<FormBlock> Blocks { get; set; }
    }
}
