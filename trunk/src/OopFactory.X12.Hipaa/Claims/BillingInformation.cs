using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public class BillingInformation
    {
        public BillingInformation()
        {
            if (Providers == null) Providers = new List<Provider>();
        }

        public List<Provider> Providers { get; set; }        
    }
}
