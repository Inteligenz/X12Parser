namespace OopFactory.X12.Sql.Interfaces
{
    using System.Data.SqlClient;

    /// <summary>
    /// Provides interface for executing SQL commands against a database
    /// </summary>
    public interface IExecutor
    {
        /// <summary>
        /// Executes provided SQL string command against database
        /// </summary>
        /// <param name="sql">SQL command to be executed</param>
        void ExecuteCmd(string sql);

        /// <summary>
        /// Executes a provided <see cref="SqlCommand"/> against database
        /// </summary>
        /// <param name="cmd">SQL Command to be executed</param>
        void ExecuteCmd(SqlCommand cmd);

        /// <summary>
        /// Executes a provided <see cref="SqlCommand"/> and returns the result
        /// </summary>
        /// <param name="cmd">SQL Command to be executed</param>
        /// <returns>Result from the execution</returns>
        object ExecuteScalar(SqlCommand cmd);
    }
}
