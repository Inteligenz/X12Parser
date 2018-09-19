namespace OopFactory.X12.Sql.Interfaces
{
    /// <summary>
    /// Provides an interface for validating SQL structure (i.e. tables, columns, views, functions, and schema)
    /// </summary>
    public interface IValidation
    {
        /// <summary>
        /// Validates that the specified function exists in the database
        /// </summary>
        /// <param name="functionName">Function name to validate</param>
        /// <returns>True if the function exists in the database; otherwise, false</returns>
        bool FunctionExists(string functionName);

        /// <summary>
        /// Validates that the specified schema exists in the database
        /// </summary>
        /// <returns>True if the schema exists in the database; otherwise, false</returns>
        bool SchemaExists();

        /// <summary>
        /// Validates that the specified table exists in the database
        /// </summary>
        /// <param name="tableName">Table name to validate</param>
        /// <returns>True if the table exists in the database; otherwise, false</returns>
        bool TableExists(string tableName);

        /// <summary>
        /// Validates that the specified view exists in the database
        /// </summary>
        /// <param name="viewName">View name to validate</param>
        /// <returns>True if the view exists in the database; otherwise, false</returns>
        bool ViewExists(string viewName);

        /// <summary>
        /// Validates that the specified column exists in the specified table of the database
        /// </summary>
        /// <param name="tableName">Table name containing column</param>
        /// <param name="columnName">Column name to validate</param>
        /// <returns>True if the column exists in the database; otherwise, false</returns>
        bool TableColumnExists(string tableName, string columnName);
    }
}
