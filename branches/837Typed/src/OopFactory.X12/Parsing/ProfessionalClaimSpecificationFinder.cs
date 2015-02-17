using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing
{
    public class ProfessionalClaimSpecificationFinder : SpecificationFinder
    {
        public override Specification.TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
        {
            if (transactionSetCode == "837")
            {
                if (versionCode.Contains("5010"))
                    return SpecificationFinder.GetSpecification("837P-5010");
                else
                    return SpecificationFinder.GetSpecification("837-4010");
            }
            else
                return base.FindTransactionSpec(functionalCode, versionCode, transactionSetCode);
        }
    }
}
