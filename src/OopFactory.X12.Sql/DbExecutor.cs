namespace OopFactory.X12.Sql
{
    using System.Data.SqlClient;

    using OopFactory.X12.Sql.Interfaces;

    /// <summary>
    /// Implements interface for executing SQL commands against a database
    /// </summary>
    public class DbExecutor : IExecutor
    {
        private readonly string dsn;

        public DbExecutor(string dsn)
        {
            this.dsn = dsn;
        }

        public void ExecuteCmd(string sql)
        {
            this.ExecuteCmd(new SqlCommand(sql));
        }

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
