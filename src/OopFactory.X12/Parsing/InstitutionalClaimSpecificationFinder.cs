namespace OopFactory.X12.Parsing
{
    public class InstitutionalClaimSpecificationFinder : SpecificationFinder
    {
        public override Specification.TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
        {
            if (transactionSetCode == "837")
            {
                return transactionSetCode == "837"
                           ? SpecificationFinder.GetSpecification("837I-5010")
                           : SpecificationFinder.GetSpecification("837I-4010");
            }

            return base.FindTransactionSpec(functionalCode, versionCode, transactionSetCode);
        }
    }
}
