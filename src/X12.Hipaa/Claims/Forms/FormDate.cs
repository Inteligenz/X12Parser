namespace X12.Hipaa.Claims.Forms
{
    using System.Xml.Serialization;

    /// <summary>
    /// Date container for forms
    /// </summary>
    public class FormDate
    {
        /// <summary>
        /// Gets or sets the month property
        /// </summary>
        [XmlAttribute]
        public string Month { get; set; }

        /// <summary>
        /// Gets or sets the day property
        /// </summary>
        [XmlAttribute]
        public string Day { get; set; }
        
        /// <summary>
        /// Gets or sets the year property
        /// </summary>
        [XmlAttribute]
        public string Year { get; set; }

        /// <summary>
        /// Returns the string representation of the <see cref="FormDate"/>
        /// </summary>
        /// <returns>String representation of <see cref="FormDate"/></returns>
        public override string ToString()
        {
            return $"{this.Month}/{this.Day}/{this.Year}";
        }
    }
}
