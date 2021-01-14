namespace X12.Specifications.Finders
{
    using X12.Specifications;

    /// <summary>
    /// Provides methods for finding dental claims specifications
    /// </summary>
    public class DentalClaimSpecificationFinder : SpecificationFinder
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
            return transactionSetCode == "837"
                       ? SpecificationFinder.GetSpecification("837D-4010")
                       : base.FindTransactionSpec(functionalCode, versionCode, transactionSetCode);
        }
    }
}
