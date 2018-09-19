namespace OopFactory.X12.Sql
{
    using System;
    using System.Data.SqlClient;

    using OopFactory.X12.Sql.Interfaces;

    /// <summary>
    /// Implements interface for validating database structure
    /// </summary>
    public class DbValidation : IValidation
    {
        private readonly DbExecutor executor;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbValidation"/> class with the specified schema and data source name
        /// </summary>
        /// <param name="schema">Schema to use for validation</param>
        /// <param name="dsn">Database connection string</param>
        public DbValidation(string schema, string dsn)
        {
            this.Schema = schema;
            this.executor = new DbExecutor(dsn);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbValidation"/> class with the specified schema and database executor
        /// </summary>
        /// <param name="schema">Schema to use for validation</param>
        /// <param name="executor">Object for executing SQL commands</param>
        public DbValidation(string schema, DbExecutor executor)
        {
            this.Schema = schema;
            this.executor = executor;
        }

        /// <summary>
        /// Gets the database schema used for SQL commands
        /// </summary>
        public string Schema { get; }

        /// <summary>
        /// Validates that the specified schema exists in the database
        /// </summary>
        /// <returns>True if the schema exists in the database; otherwise, false</returns>
        public bool SchemaExists()
        {
            var result =
                this.executor.ExecuteScalar(
                    new SqlCommand(
                        $"SELECT CASE WHEN EXISTS (SELECT * FROM sys.schemas WHERE name = N'{this.Schema}') THEN 1 ELSE 0 END"));

            return Convert.ToInt32(result) != 0;
        }

        /// <summary>
        /// Validates that the specified function exists in the database
        /// </summary>
        /// <param name="functionName">Function name to validate</param>
        /// <returns>True if the function exists in the database; otherwise, false</returns>
        public bool FunctionExists(string functionName)
        {
            var result =
                this.executor.ExecuteScalar(
                    new SqlCommand(
                        string.Format(
                            @"SELECT CASE WHEN EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[{1}]') AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT')) THEN 1 ELSE 0 END",
                            this.Schema,
                            functionName)));

            return Convert.ToInt32(result) != 0;
        }

        /// <summary>
        /// Validates that the specified table exists in the database
        /// </summary>
        /// <param name="tableName">Table name to validate</param>
        /// <returns>True if the table exists in the database; otherwise, false</returns>
        public bool TableExists(string tableName)
        {
            var result =
                this.executor.ExecuteScalar(
                    new SqlCommand(
                        $"SELECT CASE WHEN EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{this.Schema}].[{tableName}]') AND type IN (N'U')) THEN 1 ELSE 0 END"));

            return Convert.ToInt32(result) != 0;
        }

        /// <summary>
        /// Validates that the specified view exists in the database
        /// </summary>
        /// <param name="viewName">View name to validate</param>
        /// <returns>True if the view exists in the database; otherwise, false</returns>
        public bool ViewExists(string viewName)
        {
            var result =
                this.executor.ExecuteScalar(
                    new SqlCommand(
                        $"SELECT CASE WHEN EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[{this.Schema}].[{viewName}]')) THEN 1 ELSE 0 END"));

            return Convert.ToInt32(result) != 0;
        }

        /// <summary>
        /// Validates that the specified column exists in the specified table of the database
        /// </summary>
        /// <param name="tableName">Table name containing column</param>
        /// <param name="columnName">Column name to validate</param>
        /// <returns>True if the column exists in the database; otherwise, false</returns>
        public bool TableColumnExists(string tableName, string columnName)
        {
            var result = this.executor.ExecuteScalar(new SqlCommand(string.Format(
@"SELECT CASE WHEN EXISTS 
(SELECT *
FROM information_schema.columns
WHERE table_schema='{0}' 
AND Table_name = '{1}'
AND column_name = '{2}') THEN 1 ELSE 0 END",
                this.Schema,
                tableName,
                columnName)));

            return Convert.ToInt32(result) != 0;
        }
    }
}
