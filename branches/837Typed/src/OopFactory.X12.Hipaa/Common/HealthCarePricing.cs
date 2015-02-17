using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Common
{
    public class HealthCarePricing
    {
        public Lookup PricingMethodology { get; set; }
        public decimal AllowedAmount { get; set; }
        public decimal? SavingsAmount { get; set; }
        public string RepricingOrgId { get; set; }
        public decimal? PricingRate { get; set; }
        public string ApprovedDrgCode { get; set; }
        public decimal? ApprovedDrgAmount { get; set; }
        public string ApprovedRevenueCode { get; set; }
        public string ProductOrServiceIdQualifier { get; set; }
        public string ApprovedProcedureCode { get; set; }
        public Lookup UnitOfMeasure { get; set; }
        public decimal? Quantity { get; set; }
        public Lookup RejectReason { get; set; }
        public Lookup PolicyComplaince { get; set; }
        public Lookup Exception { get; set; }
    }
}
