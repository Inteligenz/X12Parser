namespace OopFactory.X12.Specifications.Finders
{
    using OopFactory.X12.Specifications;

    public class DentalClaimSpecificationFinder : SpecificationFinder
    {
        public override TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
        {
            return transactionSetCode == "837"
                       ? GetSpecification("837D-4010")
                       : base.FindTransactionSpec(functionalCode, versionCode, transactionSetCode);
        }
    }
}
