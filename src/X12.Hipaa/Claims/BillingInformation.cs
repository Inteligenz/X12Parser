namespace X12.Hipaa.Claims
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using X12.Hipaa.Common;

    /// <summary>
    /// Represents the billing information for a claim
    /// </summary>
    public class BillingInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BillingInformation"/> class
        /// </summary>
        public BillingInformation()
        {
            if (this.Providers == null)
            {
                this.Providers = new List<Provider>();
            }
        }

        /// <summary>
        /// Gets or sets the currency used for the claim
        /// </summary>
        public Lookup Currency { get; set; }

        /// <summary>
        /// Gets or sets the provider's information
        /// </summary>
        public ProviderInformation ProviderInfo { get; set; }

        /// <summary>
        /// Gets or sets the collection of providers
        /// </summary>
        [XmlElement(ElementName = Enums.ClaimElements.Provider)]
        public List<Provider> Providers { get; set; }
    }
}
