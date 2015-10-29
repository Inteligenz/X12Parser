using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Specification;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace OopFactory.X12.Repositories
{
    /// <summary>
    /// Uses a Guid for all identity columns,
    /// batches the inserts of loops and segments,
    /// and allows for an overridable method for creating new Guids
    /// so that users can apply their own guid comb algorithms
    /// </summary>
	[Obsolete("Use OopFactory.X12.Sql library and namespace")]
	public class SqlGuidTransactionRepository : SqlTransactionRepository<Guid>
    {
        public SqlGuidTransactionRepository(string dsn, ISpecificationFinder specFinder, string[] indexedSegments, string schema = "dbo", string commonSchema = "dbo", int segmentBatchSize = 1000)
            : base(dsn, specFinder, indexedSegments, schema, commonSchema, segmentBatchSize)
        {
        }

        protected virtual Guid NewGuid()
        {
            return Guid.NewGuid();
        }

        protected override Guid SaveLoop(Loop loop, Guid interchangeId, Guid transactionSetId, string transactionSetCode, Guid? parentLoopId)
        {
            Guid id = NewGuid();

            _segmentBatch.AddLoop(id, loop, interchangeId,
                transactionSetId != Guid.Empty ? transactionSetId : (System.Guid?)null,
                transactionSetCode,
                parentLoopId != Guid.Empty ? parentLoopId : null,
                GetEntityTypeCode(loop));

            return id;
        }

        internal override void ExecuteBatch(SqlTransaction tran)
        {
            if (_segmentBatch.LoopCount > 0)
            {
                try
                {
                    using (var conn = tran == null ? new SqlConnection(_dsn) : tran.Connection)
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        using (var sbc = new SqlBulkCopy(conn))
                        {
                            sbc.DestinationTableName = string.Format("[{0}].[Container]", _commonDb.Schema);

                            var containerTable = new DataTable();
                            containerTable.Columns.Add("Id", typeof(Guid));
                            containerTable.Columns.Add("SchemaName", typeof(string));
                            containerTable.Columns.Add("Type", typeof(string));
                            foreach (DataRow row in _segmentBatch._loopTable.Rows)
                                containerTable.Rows.Add(row["Id"], _schema, row["StartingSegmentId"]);

                            foreach (DataColumn c in containerTable.Columns)
                                sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                            sbc.WriteToServer(containerTable);
                        }

                        using (var sbc = new SqlBulkCopy(conn))
                        {
                            sbc.DestinationTableName = string.Format("[{0}].[Loop]", _schema);
                            foreach (DataColumn c in _segmentBatch._loopTable.Columns)
                                sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                            sbc.WriteToServer(_segmentBatch._loopTable);                            
                        }
                    }

                    _segmentBatch._loopTable.Clear();
                }
                catch (Exception exc)
                {
                    Trace.WriteLine(exc.Message);
                    Trace.TraceInformation("Error Saving {0} loops to db starting with {1}.",
                        _segmentBatch.LoopCount,
                        _segmentBatch.StartingSegment);

                    throw;
                }
            }

            base.ExecuteBatch(tran);
        }
    }
}
