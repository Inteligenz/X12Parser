namespace OopFactory.X12.Specifications.Finders
{
    using OopFactory.X12.Specifications;

    public class InstitutionalClaimSpecificationFinder : SpecificationFinder
    {
        public override TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
        {
            if (transactionSetCode == "837")
            {
                return transactionSetCode == "837"
                           ? GetSpecification("837I-5010")
                           : GetSpecification("837I-4010");
            }

            return base.FindTransactionSpec(functionalCode, versionCode, transactionSetCode);
        }
    }
}
