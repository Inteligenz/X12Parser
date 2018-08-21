namespace OopFactory.X12.Hipaa.Claims
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using OopFactory.X12.Hipaa.Common;

    public class BillingInformation
    {
        public BillingInformation()
        {
            if (this.Providers == null)
            {
                this.Providers = new List<Provider>();
            }
        }

        public Lookup Currency { get; set; }

        public ProviderInformation ProviderInfo { get; set; }

        [XmlElement(ElementName = "Provider")]
        public List<Provider> Providers { get; set; }

    }
}
