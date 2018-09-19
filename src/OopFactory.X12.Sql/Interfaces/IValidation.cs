namespace OopFactory.X12.Sql.Interfaces
{
    public interface IValidation
    {
        bool FunctionExists(string functionName);

        bool SchemaExists();

        bool TableExists(string tableName);

        bool ViewExists(string viewName);

        bool TableColumnExists(string tableName, string columnName);
    }
}
