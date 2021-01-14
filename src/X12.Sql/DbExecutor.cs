namespace X12.Sql
{
    using System.Data.SqlClient;

    using X12.Sql.Interfaces;

    /// <summary>
    /// Implements interface for executing SQL commands against a database
    /// </summary>
    public class DbExecutor : IExecutor
    {
        private readonly string dsn;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbExecutor"/> class with the specified data source name
        /// </summary>
        /// <param name="dsn">Database connection string</param>
        public DbExecutor(string dsn)
        {
            this.dsn = dsn;
        }

        /// <summary>
        /// Executes provided SQL string command against database
        /// </summary>
        /// <param name="sql">SQL command to be executed</param>
        public void ExecuteCmd(string sql)
        {
            this.ExecuteCmd(new SqlCommand(sql));
        }

        /// <summary>
        /// Executes a provided <see cref="SqlCommand"/> against database
        /// </summary>
        /// <param name="cmd">SQL Command to be executed</param>
        public void ExecuteCmd(SqlCommand cmd)
        {
            if (cmd.Transaction == null)
            {
                using (var conn = new SqlConnection(this.dsn))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes a provided <see cref="SqlCommand"/> and returns the result
        /// </summary>
        /// <param name="cmd">SQL Command to be executed</param>
        /// <returns>Result from the execution</returns>
        public object ExecuteScalar(SqlCommand cmd)
        {
            if (cmd.Transaction == null)
            {
                using (var conn = new SqlConnection(this.dsn))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    return cmd.ExecuteScalar();
                }
            }

            return cmd.ExecuteScalar();
        }
    }
}
