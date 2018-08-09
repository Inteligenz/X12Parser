namespace OopFactory.X12.Specifications.Interfaces
{
    using OopFactory.X12.Specifications;

    public interface ISpecificationFinder
    {
        TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode);

        SegmentSpecification FindSegmentSpec(string versionCode, string segmentId);
    }
}
