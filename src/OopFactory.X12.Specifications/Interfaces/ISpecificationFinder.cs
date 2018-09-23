namespace OopFactory.X12.Specifications.Interfaces
{
    using OopFactory.X12.Specifications;

    /// <summary>
    /// Provides an interface with methods for finding transaction and segment specifications
    /// </summary>
    public interface ISpecificationFinder
    {
        /// <summary>
        /// Gets the transaction specification for the provided codes
        /// </summary>
        /// <param name="functionalCode">Function code</param>
        /// <param name="versionCode">Specification version code</param>
        /// <param name="transactionSetCode">Transaction set code</param>
        /// <returns>Transaction specification which matches the codes provided</returns>
        /// <exception cref="System.NotSupportedException">Thrown if the codes provided do not map to a known specification</exception>
        TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode);

        /// <summary>
        /// Gets the segment specification for the version code and ID provided
        /// </summary>
        /// <param name="versionCode">Specification version</param>
        /// <param name="segmentId">Segment ID</param>
        /// <returns>Segment specification which matches the parameters provided; otherwise, null</returns>
        SegmentSpecification FindSegmentSpec(string versionCode, string segmentId);
    }
}
