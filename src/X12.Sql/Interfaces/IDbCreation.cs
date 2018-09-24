namespace X12.Sql.Interfaces
{
    using X12.Specifications;

    /// <summary>
    /// Provides a common interface for working with a database
    /// </summary>
    public interface IDbCreation
    {
        string Schema { get; }

        IValidation Validator { get; }

        IExecutor Executor { get; }

        void CreateSchema();

        void CreateContainerTable();

        void CreateRevisionTable();

        void CreateX12CodeListTable();

        void CreateInterchangeTable();

        void CreateFunctionalGroupTable();

        void CreateTransactionSetTable();

        void CreateLoopTable();

        void CreateSegmentTable();

        void CreateParsingErrorTable();

        void CreateIndexedSegmentTable(SegmentSpecification spec, string commonSchema);

        void CreateEntityView(string commonSchema);

        int ElementCountInX12CodeListTable(string elementId);

        void AddToX12CodeListTable(string elementId, string code, string definition);

        void AddErrorIdToIndexedSegmentTable(string segmentId);

        void CreateSplitSegmentFunction();

        void CreateFlatElementsFunction();

        void CreateGetAncestorLoopsFunction();

        void CreateGetDescendantLoopsFunction();

        void CreateGetTransactionSetSegmentsFunction();

        void CreateGetTransactionSegmentsFunction();

        void RemoveIdentityColumn(string table);

        bool HasIdentityColumn(string table);
    }
}
