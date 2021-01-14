namespace X12.Hipaa.Common
{
    using System.Linq;
    using System.Xml.Serialization;

    public class Provider : Entity
    {
        [XmlAttribute]
        public string Npi => this.Name?.Identification?.Qualifier == "XX"
                           ? this.Name.Identification.Id 
                           : this.GetReferenceId("HPI");

        [XmlAttribute]
        public string TaxId
        {
            get
            {
                if (this.Name?.Identification != null 
                    && new[] { "FI", "24" }.Contains(this.Name.Identification.Qualifier))
                {
                    return this.Name.Identification.Id;
                }

                var taxId = this.GetReferenceId("EI");
                return taxId ?? this.GetReferenceId("TJ");
            }
        }

        [XmlAttribute]
        public string ServiceProviderNumber
        {
            get
            {
                if (this.Name?.Identification != null
                    && this.Name.Identification.Qualifier == "SV")
                {
                    return this.Name.Identification.Id;
                }

                return null;
            }
        }

        public ProviderInformation ProviderInfo { get; set; }
    }
}
