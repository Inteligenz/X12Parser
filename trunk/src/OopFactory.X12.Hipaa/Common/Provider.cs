using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Hipaa.Common
{
    public class Provider : Entity
    {
        [XmlAttribute]
        public string Npi
        {
            get
            {
                if (Name != null && Name.Identification != null && Name.Identification.Qualifier == "XX")
                    return Name.Identification.Id;
                else
                    return GetReferenceId("HPI");
            }
            set { }
        }

        [XmlAttribute]
        public string TaxId
        {
            get
            {
                if (Name != null && Name.Identification != null && new string[] { "FI", "24" }.Contains(Name.Identification.Qualifier) )
                    return Name.Identification.Id;
                else
                {
                    var taxId = GetReferenceId("EI");
                    if (taxId != null)
                        return taxId;
                    else
                        return GetReferenceId("TJ");

                }
            }
            set { }
        }

        [XmlAttribute]
        public string ServiceProviderNumber
        {
            get
            {
                if (Name != null && Name.Identification != null && Name.Identification.Qualifier == "SV")
                    return Name.Identification.Id;
                else
                    return null;
            }
            set { }
        }

        public ProviderInformation ProviderInfo { get; set; }
    }
}
