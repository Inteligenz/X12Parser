namespace X12.Hipaa.Claims
{
    using System.Xml.Serialization;

    using X12.Hipaa.Common;

    /// <summary>
    /// Represents information associated with a submitter
    /// </summary>
    public class SubmitterInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitterInfo"/> class
        /// </summary>
        public SubmitterInfo()
        {
            if (this.Providers == null)
            {
                this.Providers = new Provider();
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Provider"/> for the submitter
        /// </summary>
        [XmlElement(ElementName = Enums.ClaimElements.Provider)]
        public Provider Providers { get; set; }
    }
}
