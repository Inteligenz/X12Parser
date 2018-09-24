namespace X12.Specifications.Finders
{
    using X12.Specifications;

    /// <summary>
    /// Provides methods for finding professional claims specifications
    /// </summary>
    public class ProfessionalClaimSpecificationFinder : SpecificationFinder
    {
        /// <summary>
        /// Gets the transaction specification for the provided codes
        /// </summary>
        /// <param name="functionalCode">Function code</param>
        /// <param name="versionCode">Specification version code</param>
        /// <param name="transactionSetCode">Transaction set code</param>
        /// <returns>Transaction specification which matches the codes provided</returns>
        /// <exception cref="System.NotSupportedException">Thrown if the codes provided do not map to a known specification</exception>
        public override TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode)
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
