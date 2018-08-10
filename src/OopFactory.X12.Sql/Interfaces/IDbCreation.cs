namespace OopFactory.X12.Sql.Interfaces
{
    using System.Data.SqlClient;

    using OopFactory.X12.Specifications;

    public interface IDbCreation
    {
        string Schema { get; }
        void CreateContainerTable();
        void CreateRevisionTable();
        void CreateX12CodeListTable();
        int ElementCountInX12CodeListTable(string elementId);
        void AddToX12CodeListTable(string elementId, string code, string definition);
        void CreateInterchangeTable();
        void CreateFunctionalGroupTable();
        void CreateTransactionSetTable();
        void CreateLoopTable();
        void CreateSegmentTable();
        void CreateParsingErrorTable();
        void CreateEntityView(string commonSchema);
        void CreateIndexedSegmentTable(SegmentSpecification spec, string commonSchema);
        void AddErrorIdToIndexedSegmentTable(string segmentId);
        void CreateSplitSegmentFunction();
        void CreateFlatElementsFunction();
        void CreateGetAncestorLoopsFunction();
        void CreateGetDescendantLoopsFunction();
        void CreateGetTransactionSetSegmentsFunction();
        void CreateGetTransactionSegmentsFunction();
        void CreateSchema();
        bool FunctionExists(string functionName);
        bool SchemaExists();
        bool TableExists(string tableName);
        bool ViewExists(string viewName);
        bool TableColumnExists(string tableName, string columnName);
        void ExecuteCmd(string sql);
        void ExecuteCmd(SqlCommand cmd);
        object ExecuteScalar(SqlCommand cmd);
        void RemoveIdentityColumn(string table);
        bool HasIdentityColumn(string table);
    }
}
