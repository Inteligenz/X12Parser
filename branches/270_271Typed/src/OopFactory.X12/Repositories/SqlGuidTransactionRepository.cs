using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Specification;
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

            AddSqlToBatch("INSERT INTO [{0}].[Container] (Id, SchemaName, Type)  VALUES ('{1}','{2}','{3}') ", _commonDb.Schema, id, _schema, loop.SegmentId);

            AddSqlToBatch(GetSaveLoopSql(id, loop, interchangeId, transactionSetId, transactionSetCode, parentLoopId));

            return id;
        }
    }
}
