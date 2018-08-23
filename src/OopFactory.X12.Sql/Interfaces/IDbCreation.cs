namespace OopFactory.X12.Sql.Interfaces
{
    using System.Data.SqlClient;

    using OopFactory.X12.Specifications;

    /// <summary>
    /// Provides a common interface for working with a database
    /// </summary>
    public interface IDbCreation
    {
        string Schema { get; }
        void CreateSchema();

        /* Sql Execution Methods */
        void ExecuteCmd(string sql);
        void ExecuteCmd(SqlCommand cmd);
        object ExecuteScalar(SqlCommand cmd);

        /* Table creation */
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

        /* Function creation methods */
        void CreateSplitSegmentFunction();
        void CreateFlatElementsFunction();
        void CreateGetAncestorLoopsFunction();
        void CreateGetDescendantLoopsFunction();
        void CreateGetTransactionSetSegmentsFunction();
        void CreateGetTransactionSegmentsFunction();

        /* Validation methods */
        bool FunctionExists(string functionName);
        bool SchemaExists();
        bool TableExists(string tableName);
        bool ViewExists(string viewName);
        bool TableColumnExists(string tableName, string columnName);

        void RemoveIdentityColumn(string table);
        bool HasIdentityColumn(string table);
    }
}
