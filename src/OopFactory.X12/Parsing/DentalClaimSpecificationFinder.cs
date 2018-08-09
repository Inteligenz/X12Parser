namespace OopFactory.X12.Parsing
{
    public class DentalClaimSpecificationFinder : SpecificationFinder
    {
        public override Specification.TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
        {
            return transactionSetCode == "837"
                       ? SpecificationFinder.GetSpecification("837D-4010")
                       : base.FindTransactionSpec(functionalCode, versionCode, transactionSetCode);
        }
    }
}
