namespace OopFactory.X12.Sql.IdentityProviders
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using OopFactory.X12.Sql.Interfaces;

    public class LongHiLowIdentityProvider : IIdentityProvider
    {
        private readonly IDictionary<string, Identity<long>> _ids = new Dictionary<string, Identity<long>>();
        private readonly string _dsn;
        private readonly string _schema;
        private readonly int _batchSize;

        public LongHiLowIdentityProvider(string dsn, string schema, int batchSize)
        {
            _dsn = dsn;
            _schema = schema;
            _batchSize = batchSize;
        }

        public void EnsureSchema()
        {
            using (var conn = new SqlConnection(_dsn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    var sql = string.Format(@"
						if not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[HiLo]') AND type in (N'U'))
						begin
						  CREATE TABLE [{0}].HiLo (
							NextId bigint not null,
							[Table] varchar(100) not null
						  )
						end", _schema);

                    cmd.CommandText = sql;
                }
            }
        }

        public object NextId(string schema, string table)
        {
            if (!_ids.ContainsKey(table))
                _ids.Add(table, new Identity<long> { NextId = 0, MaxId = 0 });

            Identity<long> id = _ids[table];

            if (id != null && id.NextId < id.MaxId)
                return id.NextId++;

            var sql = @"
				declare @table varchar(100)
				set @table = '[{1}].[{2}]'
				select @nextId = NextId from [{0}].HiLo with (updlock, rowlock) where lower([table]) = lower(@table)
				if isnull(@nextId, 0) = 0
				begin
					declare @maxId bigint
					select @maxId = isnull(max(Id), 0) + 1 from [{1}].[{2}]
					insert into [{0}].HiLo (NextId, [Table]) values (@maxId, @table)
					select @nextId = 1
				end
				update [{0}].HiLo set NextId = NextId + @batchSize where lower([table]) = lower(@table)";

            using (var conn = new SqlConnection(_dsn))
            {
                conn.Open();

                using (var tx = conn.BeginTransaction(IsolationLevel.Serializable))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;
                    cmd.Parameters.Add("@nextId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@batchSize", SqlDbType.Int).Value = _batchSize;
                    cmd.Parameters.Add("@table", SqlDbType.VarChar, 100).Value = table;
                    cmd.CommandText = string.Format(sql, _schema);
                    cmd.ExecuteNonQuery();
                    id.NextId = Convert.ToInt64(cmd.Parameters["@nextId"].Value);
                    id.MaxId = id.NextId + _batchSize;
                    tx.Commit();
                    return id.NextId++;
                }
            }
        }
    }
}
