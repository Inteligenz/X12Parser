namespace OopFactory.X12.Specifications.Finders
{
    using OopFactory.X12.Specifications;

    public class ProfessionalClaimSpecificationFinder : SpecificationFinder
    {
        public override TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
        {
            if (transactionSetCode == "837")
            {
                return versionCode.Contains("5010")
                           ? GetSpecification("837P-5010")
                           : GetSpecification("837-4010");
            }

            return base.FindTransactionSpec(functionalCode, versionCode, transactionSetCode);
        }
    }
}
