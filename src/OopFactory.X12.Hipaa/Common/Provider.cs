namespace OopFactory.X12.Hipaa.Common
{
    using System.Linq;
    using System.Xml.Serialization;

    public class Provider : Entity
    {
        [XmlAttribute]
        public string Npi
        {
            get
            {
                if (this.Name?.Identification != null && this.Name.Identification.Qualifier == "XX")
                {
                    return this.Name.Identification.Id;
                }
                else
                {
                    return this.GetReferenceId("HPI");
                }
            }
        }

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
                else
                {
                    var taxId = this.GetReferenceId("EI");
                    return taxId ?? this.GetReferenceId("TJ");
                }
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
                else
                {
                    return null;
                }
            }
        }

        public ProviderInformation ProviderInfo { get; set; }
    }
}
