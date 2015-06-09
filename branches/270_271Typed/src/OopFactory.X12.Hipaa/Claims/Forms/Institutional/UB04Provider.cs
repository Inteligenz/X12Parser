using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    public class UB04Provider
    {
        public string ProviderQualifier { get; set; }
        public string Npi { get; set; }
        public string IdentifierQualifier { get; set; }
        public string Identifier { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
