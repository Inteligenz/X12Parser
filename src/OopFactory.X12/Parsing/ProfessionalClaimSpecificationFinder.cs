namespace OopFactory.X12.Parsing
{
    public class ProfessionalClaimSpecificationFinder : SpecificationFinder
    {
        public override Specification.TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
        {
            if (transactionSetCode == "837")
            {
                return versionCode.Contains("5010")
                           ? SpecificationFinder.GetSpecification("837P-5010")
                           : SpecificationFinder.GetSpecification("837-4010");
            }

            return base.FindTransactionSpec(functionalCode, versionCode, transactionSetCode);
        }
    }
}
