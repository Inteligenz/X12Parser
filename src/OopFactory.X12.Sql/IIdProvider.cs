namespace OopFactory.X12.Sql
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;

	public interface IIdProvider
	{
		void EnsureSchema();
		object NextId(string schema, string table);
	}

	public class IntHiLowIdentityProvider : IIdProvider
	{
		private readonly IDictionary<string, Ids> _ids = new Dictionary<string, Ids>();
		private readonly string _dsn;
		private readonly string _schema;
		private readonly int _batchSize;

		public IntHiLowIdentityProvider(string dsn, string schema, int batchSize)
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
							NextId int not null,
							[Table] varchar(100) not null
						  )
						end", _schema);

					cmd.CommandText = sql;
					cmd.ExecuteNonQuery();
				}
			}
		}

		public object NextId(string schema, string table)
		{
			if (!_ids.ContainsKey(table))
				_ids.Add(table, new Ids { NextId = 0, MaxId = 0 });

			var id = _ids[table];

			if (id != null && id.NextId < id.MaxId)
				return id.NextId++;

			var sql = @"
				declare @table varchar(100)
				set @table = '[{1}].[{2}]'
				select @nextId = NextId from [{0}].HiLo with (updlock, rowlock) where lower([table]) = lower(@table)
				if isnull(@nextId, 0) = 0
				begin
					declare @maxId int
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
					cmd.Parameters.Add("@nextId", SqlDbType.Int).Direction = ParameterDirection.Output;
					cmd.Parameters.Add("@batchSize", SqlDbType.Int).Value = _batchSize;
					cmd.CommandText = string.Format(sql, _schema, schema, table);
					cmd.ExecuteNonQuery();
					id.NextId = Convert.ToInt32(cmd.Parameters["@nextId"].Value);
					id.MaxId = id.NextId + _batchSize;
					tx.Commit();
					return id.NextId++;
				}
			}
		}

		private class Ids
		{
			public int NextId { get; set; }
			public int MaxId { get; set; }
		}
	}

	public class LongHiLowIdentityProvider : IIdProvider
	{
		private readonly IDictionary<string, Ids> _ids = new Dictionary<string, Ids>();
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
				_ids.Add(table, new Ids { NextId = 0, MaxId = 0 });

			var id = _ids[table];

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

		private class Ids
		{
			public long NextId { get; set; }
			public long MaxId { get; set; }
		}
	}

	public class GuidIdentityProvider : IIdProvider
	{
		public void EnsureSchema()
		{
			//do nothing
		}

		public object NextId(string schema, string table)
		{
			/*
			   * Could also use the built in Win32 function, but this will work equally as well and doesn't do any locking
			   * Sequential guids are more performant while reading than non sequential guids
			   */

			var guidArray = Guid.NewGuid().ToByteArray();

			var baseDate = new DateTime(1900, 1, 1);
			var now = DateTime.Now;

			// Get the days and milliseconds which will be used to build the byte string 
			var days = new TimeSpan(now.Ticks - baseDate.Ticks);
			var msecs = now.TimeOfDay;

			// Convert to a byte array 
			// Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
			var daysArray = BitConverter.GetBytes(days.Days);
			var msecsArray = BitConverter.GetBytes((long) (msecs.TotalMilliseconds/3.333333));

			// Reverse the bytes to match SQL Servers ordering 
			Array.Reverse(daysArray);
			Array.Reverse(msecsArray);

			// Copy the bytes into the guid 
			Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
			Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

			return new Guid(guidArray);
		}
	}
}