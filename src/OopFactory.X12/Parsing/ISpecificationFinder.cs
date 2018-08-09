namespace OopFactory.X12.Parsing
{
    using OopFactory.X12.Parsing.Specification;

    public interface ISpecificationFinder
    {
        TransactionSpecification FindTransactionSpec(string functionalCode, string versionCode, string transactionSetCode);

        SegmentSpecification FindSegmentSpec(string versionCode, string segmentId);
    }
}
