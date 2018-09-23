namespace OopFactory.X12.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    
    using OopFactory.X12.Shared.Models;
    using OopFactory.X12.Specifications;
    using OopFactory.X12.Specifications.Enumerations;
    using OopFactory.X12.Specifications.Finders;
    using OopFactory.X12.Specifications.Interfaces;
    using OopFactory.X12.Sql.IdentityProviders;
    using OopFactory.X12.Sql.Interfaces;
    using OopFactory.X12.Sql.Properties;

    /// <summary>
    ///     Class for storing, retrieving and revising X12 messages.
    ///     This library only does inserts.  Edits and Deletes are accomplished through revisions, but all revisions are
    ///     retained.
    ///     The Get methods will allow you choose the revision you want.
    /// </summary>
    public class SqlTransactionRepository : SqlReadOnlyTransactionRepository, IParsingErrorRepo
    {
        private readonly Dictionary<string, SegmentSpecification> specs;
        private readonly int batchSize;
        private readonly IIdentityProvider idProvider;
        private bool schemaEnsured;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTransactionRepository"/> class
        /// </summary>
        /// <param name="dsn">Data source information</param>
        /// <param name="identityType">Identity type</param>
        public SqlTransactionRepository(string dsn, Type identityType)
            : this(dsn, new SpecificationFinder(), new[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER" }, identityType, "dbo")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTransactionRepository"/> class
        /// </summary>
        /// <param name="dsn">Data source information</param>
        /// <param name="schema">Database schema for data access</param>
        /// <param name="identityType">Identity type</param>
        public SqlTransactionRepository(string dsn, string schema, Type identityType)
            : this(dsn, new SpecificationFinder(), new[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER" }, identityType, schema)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTransactionRepository"/> class
        /// </summary>
        /// <param name="dsn">Data source information</param>
        /// <param name="specFinder">Specification finder for data structure information</param>
        /// <param name="indexedSegments">Segments stored in database</param>
        /// <param name="identityType">Identity type</param>
        /// <param name="schema">Database schema for data access</param>
        /// <param name="commonSchema">Common database schema</param>
        /// <param name="segmentBatchSize">Number of segments to pull from database at a time</param>
        /// <param name="sqlDateType">"Date" type used by database</param>
        public SqlTransactionRepository(
            string dsn,
            ISpecificationFinder specFinder,
            string[] indexedSegments,
            Type identityType,
            string schema = "dbo",
            string commonSchema = "dbo",
            int segmentBatchSize = 1000,
            string sqlDateType = "date")
            : base(dsn, identityType, schema)
        {
            this.CommonDb = new DbCreation(dsn, commonSchema, identityType, sqlDateType);
            this.TransactionDb = new DbCreation(dsn, schema, identityType, sqlDateType);
            this.idProvider = this.GetIdProvider(dsn, commonSchema, identityType, segmentBatchSize / 10);
            this.schemaEnsured = false;
            this.batchSize = segmentBatchSize;
            this.SegmentBatch = new SegmentBatch(this, identityType);
            this.specs = new Dictionary<string, SegmentSpecification>();
            foreach (var segmentId in indexedSegments)
            {
                var spec = specFinder.FindSegmentSpec("5010", segmentId.Trim());
                this.specs.Add(segmentId.Trim(), spec);
            }
        }

        /// <summary>
        /// Gets or sets the container for batch segments
        /// </summary>
        internal SegmentBatch SegmentBatch { get; set; }

        /// <summary>
        /// Gets or sets the common datbase creation information
        /// </summary>
        protected IDbCreation CommonDb { get; set; }

        /// <summary>
        /// Gets or sets the transaction database creation information
        /// </summary>
        protected IDbCreation TransactionDb { get; set; }

        /// <summary>
        ///     override this with no implementation when your database is already created and you will not need to check for
        ///     existance of the objects.
        /// </summary>
        public virtual void EnsureSchema()
        {
            if (!this.schemaEnsured)
            {
                if (!this.CommonDb.Validator.SchemaExists())
                {
                    this.CommonDb.CreateSchema();
                }

                if (!this.CommonDb.Validator.TableExists("Container"))
                {
                    this.CommonDb.CreateContainerTable();
                }

                if (!this.CommonDb.Validator.TableExists("Revision"))
                {
                    this.CommonDb.CreateRevisionTable();
                }

                if (!this.CommonDb.Validator.TableExists("X12CodeList"))
                {
                    this.CommonDb.CreateX12CodeListTable();
                }

                if (!this.TransactionDb.Validator.SchemaExists())
                {
                    this.TransactionDb.CreateSchema();
                }

                if (!this.TransactionDb.Validator.TableExists("Interchange"))
                {
                    this.TransactionDb.CreateInterchangeTable();
                }

                if (!this.TransactionDb.Validator.TableExists("FunctionalGroup"))
                {
                    this.TransactionDb.CreateFunctionalGroupTable();
                }

                if (!this.TransactionDb.Validator.TableExists("TransactionSet"))
                {
                    this.TransactionDb.CreateTransactionSetTable();
                }

                if (!this.TransactionDb.Validator.TableExists("Loop"))
                {
                    this.TransactionDb.CreateLoopTable();
                }

                if (!this.TransactionDb.Validator.TableExists("Segment"))
                {
                    this.TransactionDb.CreateSegmentTable();
                }

                if (!this.TransactionDb.Validator.TableExists("ParsingError"))
                {
                    this.TransactionDb.CreateParsingErrorTable();
                }

                if (!this.CommonDb.Validator.FunctionExists("SplitSegment"))
                {
                    this.CommonDb.CreateSplitSegmentFunction();
                }

                if (!this.CommonDb.Validator.FunctionExists("FlatElements"))
                {
                    this.CommonDb.CreateFlatElementsFunction();
                }

                if (!this.TransactionDb.Validator.FunctionExists("GetAncestorLoops"))
                {
                    this.TransactionDb.CreateGetAncestorLoopsFunction();
                }

                if (!this.TransactionDb.Validator.FunctionExists("GetDescendantLoops"))
                {
                    this.TransactionDb.CreateGetDescendantLoopsFunction();
                }

                if (!this.TransactionDb.Validator.FunctionExists("GetTransactionSetSegments"))
                {
                    this.TransactionDb.CreateGetTransactionSetSegmentsFunction();
                }

                if (!this.TransactionDb.Validator.FunctionExists("GetTransactionSegments"))
                {
                    this.TransactionDb.CreateGetTransactionSegmentsFunction();
                }

                foreach (var spec in this.specs.Values)
                {
                    if (!this.TransactionDb.Validator.TableExists(spec.SegmentId))
                    {
                        this.TransactionDb.CreateIndexedSegmentTable(spec, this.CommonDb.Schema);
                    }
                    else if (!this.TransactionDb.Validator.TableColumnExists(spec.SegmentId, "ErrorId"))
                    {
                        this.TransactionDb.AddErrorIdToIndexedSegmentTable(spec.SegmentId);
                    }

                    foreach (var element in spec.Elements)
                    {
                        if (element.Type == ElementDataType.Identifier
                            && !string.IsNullOrEmpty(element.QualifierSetId)
                            && element.AllowedIdentifiers.Count > 0)
                        {
                            if (this.CommonDb.ElementCountInX12CodeListTable(element.QualifierSetId) == 0)
                            {
                                foreach (var identifier in element.AllowedIdentifiers)
                                {
                                    this.CommonDb.AddToX12CodeListTable(
                                        element.QualifierSetId,
                                        identifier.ID,
                                        identifier.Description);
                                }
                            }
                        }
                    }
                }

                if (!this.TransactionDb.Validator.ViewExists("Entity")
                    && this.specs.ContainsKey("NM1")
                    && this.specs.ContainsKey("N1")
                    && this.specs.ContainsKey("N3")
                    && this.specs.ContainsKey("N4")
                    && this.specs.ContainsKey("PER")
                    && this.specs.ContainsKey("DMG"))
                {
                    this.TransactionDb.CreateEntityView(this.CommonDb.Schema);
                }

                if (this.CommonDb.HasIdentityColumn("Container"))
                {
                    this.CommonDb.RemoveIdentityColumn("Container");
                }

                if (this.TransactionDb.HasIdentityColumn("Interchange"))
                {
                    this.TransactionDb.RemoveIdentityColumn("Interchange");
                }

                if (this.TransactionDb.HasIdentityColumn("FunctionalGroup"))
                {
                    this.TransactionDb.RemoveIdentityColumn("FunctionalGroup");
                }

                if (this.TransactionDb.HasIdentityColumn("TransactionSet"))
                {
                    this.TransactionDb.RemoveIdentityColumn("TransactionSet");
                }

                if (this.TransactionDb.HasIdentityColumn("Loop"))
                {
                    this.TransactionDb.RemoveIdentityColumn("Loop");
                }

                if (this.TransactionDb.HasIdentityColumn("ParsingError"))
                {
                    this.TransactionDb.RemoveIdentityColumn("ParsingError");
                }

                this.idProvider.EnsureSchema();
                this.schemaEnsured = true;
            }
        }

        /// <summary>
        ///     Saves the entire interchange into the database as individual segments and the relationships between the segments
        ///     and loops
        /// </summary>
        /// <param name="interchange">The parsed interchange object</param>
        /// <param name="filename">Name of file to save to</param>
        /// <param name="userName">Username used to access file</param>
        /// <returns>The interchangeId from the database</returns>
        public object Save(Interchange interchange, string filename, string userName)
        {
            this.EnsureSchema();
            var positionInInterchange = 1;

            var interchangeId = this.SaveInterchange(interchange, filename, userName);
            try
            {
                this.SaveSegment(null, interchange, positionInInterchange, interchangeId);

                foreach (var fg in interchange.FunctionGroups)
                {
                    var functionalGroupId = this.SaveFunctionalGroup(fg, interchangeId);
                    this.SaveSegment(null, fg, ++positionInInterchange, interchangeId, functionalGroupId);

                    foreach (Transaction tran in fg.Transactions)
                    {
                        string transactionSetCode = tran.IdentifierCode;
                        var transactionSetId = this.SaveTransactionSet(tran, interchangeId, functionalGroupId);
                        this.SaveSegment(null, tran, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);

                        foreach (Segment seg in tran.Segments)
                        {
                            if (seg is HierarchicalLoopContainer hlContainer)
                            {
                                positionInInterchange++;
                                this.SaveLoopAndChildren(
                                    hlContainer,
                                    ref positionInInterchange,
                                    interchangeId,
                                    functionalGroupId,
                                    transactionSetId,
                                    transactionSetCode,
                                    null);
                            }
                            else
                            {
                                this.SaveSegment(
                                    null,
                                    seg,
                                    ++positionInInterchange,
                                    interchangeId,
                                    functionalGroupId,
                                    transactionSetId);
                            }
                        }

                        foreach (HierarchicalLoop hl in tran.HLoops)
                        {
                            positionInInterchange++;
                            this.SaveLoopAndChildren(
                                hl,
                                ref positionInInterchange,
                                interchangeId,
                                functionalGroupId,
                                transactionSetId,
                                transactionSetCode,
                                null);
                        }

                        foreach (Segment seg in tran.TrailerSegments)
                        {
                            this.SaveSegment(
                                null,
                                seg,
                                ++positionInInterchange,
                                interchangeId,
                                functionalGroupId,
                                transactionSetId);
                        }
                    }

                    foreach (Segment seg in fg.TrailerSegments)
                    {
                        this.SaveSegment(null, seg, ++positionInInterchange, interchangeId, functionalGroupId);
                    }
                }

                foreach (Segment seg in interchange.TrailerSegments)
                {
                    this.SaveSegment(null, seg, ++positionInInterchange, interchangeId);
                }

                this.ExecuteBatch(null);
                return interchangeId;
            }
            catch (Exception)
            {
                this.MarkInterchangeWithError(interchangeId);
                throw;
            }
        }

        /// <summary>
        ///     This will save revisions to an x12 transaction that was returned from the GetTransactionSegments method.
        ///     The update is stored as inserts into the database, and only the most current revision that hasn't been deleted is
        ///     returned on the next retrieval
        /// </summary>
        /// <param name="segments">
        ///     The segments to be updated, only segments with a different SegmentString or Deleted value will
        ///     be updated
        /// </param>
        /// <param name="comments">The reason for the revision</param>
        /// <param name="revisedBy">Ther username of the user who has made the revision</param>
        /// <returns>Revision identifier generated from save</returns>
        public int SaveRevision(IList<RepoSegment> segments, string comments, string revisedBy)
        {
            int? revisionId;
            using (var conn = new SqlConnection(this.Dsn))
            {
                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try
                {
                    var sql = $"INSERT INTO [{this.Schema}].[Revision] (SchemaName,Comments,RevisionDate,RevisedBy)"
                        + "VALUES (@schemaName, @comments, getdate(), @revisedBy)"
                        + "SELECT scope_identity()";

                    var cmd = new SqlCommand(sql, conn, sqlTran);
                    cmd.Parameters.AddWithValue("@schemaName", this.Schema);
                    cmd.Parameters.AddWithValue("@comments", comments);
                    cmd.Parameters.AddWithValue("@revisedBy", revisedBy);
                    revisionId = Convert.ToInt32(this.ExecuteScalar(cmd));

                    foreach (var segment in segments)
                    {
                        this.SaveSegment(
                            sqlTran,
                            segment.Segment,
                            segment.PositionInInterchange,
                            segment.InterchangeId,
                            segment.FunctionalGroupId,
                            segment.TransactionSetId,
                            segment.ParentLoopId,
                            segment.LoopId,
                            revisionId,
                            segment.RevisionId,
                            segment.Deleted);
                    }

                    sqlTran.Commit();
                }
                catch (Exception)
                {
                    sqlTran.Rollback();
                    throw;
                }
            }

            return revisionId.Value;
        }

        /// <summary>
        /// Stores the parsing error into the database
        /// </summary>
        /// <param name="interchangeId">ID of <see cref="Interchange"/> with warning</param>
        /// <param name="positionInInterchange">Place in interchange where warning occurred</param>
        /// <param name="revisionId">ID of revision warning was thrown in</param>
        /// <param name="errorMessage">Message to be stored for the warning</param>
        /// <returns>Returns the ID of the error stored</returns>
        public object PersistParsingError(
            object interchangeId,
            int positionInInterchange,
            int? revisionId,
            string errorMessage)
        {
            var errorId = this.idProvider.NextId(this.Schema, "ParsingError");

            var cmd = new SqlCommand(
                    $"INSERT INTO [{this.Schema}].ParsingError (Id, InterchangeId,PositionInInterchange,RevisionId,Message)\n"
                    + "VALUES (@id, @interchangeId, @positionInInterchange, @revisionId, @message)");

            cmd.Parameters.AddWithValue("@id", errorId);
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            cmd.Parameters.AddWithValue("@positionInInterchange", positionInInterchange);
            cmd.Parameters.AddWithValue("@revisionId", revisionId ?? 0);
            cmd.Parameters.AddWithValue("@message", errorMessage);

            this.ExecuteCmd(cmd);

            return errorId;
        }

        /// <summary>
        /// Executes a segment batch with the provided <see cref="SqlTransaction"/>
        /// </summary>
        /// <param name="tran">SQL Transaction to be executed</param>
        internal virtual void ExecuteBatch(SqlTransaction tran)
        {
            if (this.SegmentBatch.LoopCount > 0)
            {
                try
                {
                    using (var conn = tran == null ? new SqlConnection(this.Dsn) : tran.Connection)
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }

                        using (var sbc = new SqlBulkCopy(conn))
                        {
                            sbc.DestinationTableName = $"[{this.CommonDb.Schema}].[Container]";

                            var containerTable = new DataTable();
                            containerTable.Columns.Add("Id", this.IdentityType);
                            containerTable.Columns.Add("SchemaName", typeof(string));
                            containerTable.Columns.Add("Type", typeof(string));

                            foreach (DataRow row in this.SegmentBatch.LoopTable.Rows)
                            {
                                var containerId = this.idProvider.NextId(this.CommonDb.Schema, "Container");
                                containerTable.Rows.Add(containerId, this.Schema, row["StartingSegmentId"]);
                            }

                            foreach (DataColumn c in containerTable.Columns)
                            {
                                sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                            }

                            sbc.WriteToServer(containerTable);
                        }

                        using (var sbc = new SqlBulkCopy(conn))
                        {
                            sbc.DestinationTableName = $"[{this.Schema}].[Loop]";
                            foreach (DataColumn c in this.SegmentBatch.LoopTable.Columns)
                            {
                                sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                            }

                            sbc.WriteToServer(this.SegmentBatch.LoopTable);
                        }
                    }

                    this.SegmentBatch.LoopTable.Clear();
                }
                catch (Exception exc)
                {
                    Trace.WriteLine(exc.Message);
                    Trace.TraceInformation(
                        "Error Saving {0} loops to db starting with {1}.",
                        this.SegmentBatch.LoopCount,
                        this.SegmentBatch.StartingSegment);

                    throw;
                }
            }

            if (this.SegmentBatch.SegmentCount > 0)
            {
                try
                {
                    using (var conn = tran == null ? new SqlConnection(this.Dsn) : tran.Connection)
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }

                        using (var sbc = new SqlBulkCopy(conn))
                        {
                            sbc.DestinationTableName = $"[{this.Schema}].Segment";
                            foreach (DataColumn c in this.SegmentBatch.SegmentTable.Columns)
                            {
                                sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                            }

                            sbc.WriteToServer(this.SegmentBatch.SegmentTable);

                            foreach (KeyValuePair<string, DataTable> pair in this.SegmentBatch.ParsedTables)
                            {
                                sbc.ColumnMappings.Clear();

                                sbc.DestinationTableName = $"[{this.Schema}].[{pair.Key}]";
                                foreach (DataColumn c in pair.Value.Columns)
                                {
                                    sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                                }

                                sbc.WriteToServer(pair.Value);
                            }
                        }
                    }

                    this.SegmentBatch = new SegmentBatch(this, this.IdentityType);
                }
                catch (Exception exc)
                {
                    Trace.WriteLine(exc.Message);
                    Trace.TraceInformation(
                        "Error Saving {0} segments to db starting with {1}.",
                        this.SegmentBatch.SegmentCount,
                        this.SegmentBatch.StartingSegment);

                    throw;
                }
            }
        }

        /// <summary>
        /// Executed the provided command agains the TransactionDb
        /// </summary>
        /// <param name="cmd">SQL command being executed</param>
        protected void ExecuteCmd(SqlCommand cmd)
        {
            this.TransactionDb.Executor.ExecuteCmd(cmd);
        }

        /// <summary>
        /// Executes the provided <see cref="SqlCommand"/> and returns the result
        /// </summary>
        /// <param name="cmd">SQL Command to be executed</param>
        /// <returns>Result of the command execution</returns>
        protected object ExecuteScalar(SqlCommand cmd)
        {
            return this.TransactionDb.Executor.ExecuteScalar(cmd);
        }

        /// <summary>
        /// Builds the SQL command to insert the provided segment ID into the database
        /// </summary>
        /// <param name="segmentId"><see cref="Segment"/> identifier to insert</param>
        /// <returns>SQL string query</returns>
        protected virtual string GetContainerIdSql(string segmentId)
        {
            return string.Format(
                @"INSERT INTO [{1}].[Container] (Id, SchemaName, Type) VALUES (@containerId, '{0}','{2}');",
                this.Schema,
                this.CommonDb.Schema,
                segmentId);
        }

        /// <summary>
        /// Gets the Entity type code from the provided <see cref="Loop"/>
        /// </summary>
        /// <param name="loop">Loop to obtain the Entity type code from</param>
        /// <returns>Entity type code, if present; otherwise null</returns>
        protected virtual string GetEntityTypeCode(Loop loop)
        {
            if (new[] { "CLI", "CUR", "G18", "MRC", "N1", "NM1", "NX1", "RDI" }.Contains(loop.SegmentId))
            {
                return loop.GetElement(1);
            }

            if (new[] { "ENT", "LCD", "NX1", "PLA", "PT" }.Contains(loop.SegmentId))
            {
                return loop.GetElement(2);
            }

            if (new[] { "IN1", "NX1", "SCH" }.Contains(loop.SegmentId))
            {
                return loop.GetElement(3);
            }

            return null;
        }

        /// <summary>
        /// Returns SQL command to insert the provided Loop data
        /// </summary>
        /// <param name="id">Loop ID</param>
        /// <param name="loop">Loop object with additional metadata to be inserted</param>
        /// <param name="interchangeId">Interchange ID</param>
        /// <param name="transactionSetId">Transaction set ID</param>
        /// <param name="transactionSetCode">Transaction set code</param>
        /// <param name="parentLoopId">Parent Loop ID</param>
        /// <returns>String containing SQL command with provided data inserted</returns>
        protected string GetSaveLoopSql(
            object id,
            Loop loop,
            object interchangeId,
            object transactionSetId,
            string transactionSetCode,
            object parentLoopId)
        {
            string entityIdentifierCode = this.GetEntityTypeCode(loop);

            return string.Format(
@"INSERT INTO [{0}].[Loop] (Id, ParentLoopId, InterchangeId, TransactionSetId, TransactionSetCode, SpecLoopId, StartingSegmentId, EntityIdentifierCode)
VALUES ('{1}', {2}, '{3}', '{4}', '{5}', '{6}', '{7}', {8}) ",
                this.Schema,
                id,
                parentLoopId == this.DefaultIdentityTypeValue ? "NULL" : $"'{parentLoopId}'",
                interchangeId,
                transactionSetId,
                transactionSetCode,
                loop.Specification.LoopId,
                loop.SegmentId,
                entityIdentifierCode == null ? "NULL" : $"'{entityIdentifierCode}'");
        }


        /// <summary>
        /// Saves the loop data to the SegmentBatch
        /// </summary>
        /// <param name="loop">Loop object with additional metadata to be inserted</param>
        /// <param name="interchangeId">Interchange ID</param>
        /// <param name="transactionSetId">Transaction set ID</param>
        /// <param name="transactionSetCode">Transaction set code</param>
        /// <param name="parentLoopId">Parent Loop ID</param>
        /// <returns>Id generated by the ID Provider</returns>
        protected virtual object SaveLoop(
            Loop loop,
            object interchangeId,
            object transactionSetId,
            string transactionSetCode,
            object parentLoopId)
        {
            var id = this.idProvider.NextId(this.Schema, "Loop");

            this.SegmentBatch.AddLoop(
                id,
                loop,
                interchangeId,
                transactionSetId != this.DefaultIdentityTypeValue ? transactionSetId : null,
                transactionSetCode,
                parentLoopId != this.DefaultIdentityTypeValue ? parentLoopId : null,
                this.GetEntityTypeCode(loop));

            return id;
        }

        /// <summary>
        /// Saves <see cref="Segment"/> information to SegmentBatch
        /// </summary>
        /// <param name="tran"><see cref="Transaction"/> information to be saved</param>
        /// <param name="segment">Segment information to be saved</param>
        /// <param name="positionInInterchange">Segment position in interchange</param>
        /// <param name="interchangeId">Interchange ID</param>
        /// <param name="functionalGroupId">FunctionalGroup ID</param>
        /// <param name="transactionSetId">Transaction Set ID segment belongs to</param>
        /// <param name="parentLoopId">Parent Loop ID for segment</param>
        /// <param name="loopId">Loop ID</param>
        /// <param name="revisionId">Revision ID</param>
        /// <param name="previousRevisionId">Previous revision ID</param>
        /// <param name="deleted">Flag if segment is deleted</param>
        protected virtual void SaveSegment(
            SqlTransaction tran,
            DetachedSegment segment,
            int positionInInterchange,
            object interchangeId,
            object functionalGroupId = null,
            object transactionSetId = null,
            object parentLoopId = null,
            object loopId = null,
            int? revisionId = null,
            int? previousRevisionId = null,
            bool deleted = false)
        {
            if (!revisionId.HasValue || this.SegmentHasChanged(segment, positionInInterchange, interchangeId, previousRevisionId) || deleted)
            {
                this.SegmentBatch.AddSegment(
                    tran,
                    interchangeId,
                    positionInInterchange,
                    revisionId ?? 0,
                    this.ConvertT(functionalGroupId),
                    this.ConvertT(transactionSetId),
                    this.ConvertT(parentLoopId),
                    this.ConvertT(loopId),
                    deleted,
                    segment,
                    this.specs.ContainsKey(segment.SegmentId) ? this.specs[segment.SegmentId] : null);

                if (tran != null || this.SegmentBatch.SegmentTable.Rows.Count >= this.batchSize)
                {
                    this.ExecuteBatch(tran);
                }
            }
        }

        private IIdentityProvider GetIdProvider(string dsn, string commonSchema, Type identityType, int segmentBatchSize)
        {
            if (!identityType.IsValueType)
            {
                throw new ArgumentException(Resources.InvalidIdentityType, nameof(identityType));
            }

            if (!(identityType == typeof(Guid) || identityType == typeof(long) || identityType == typeof(int)))
            {
                throw new ArgumentException(Resources.UnsupportedIdentityType, nameof(identityType));
            }

            if (identityType == typeof(Guid))
            {
                return new GuidIdentityProvider();
            }

            if (identityType == typeof(long))
            {
                return new LongHiLowIdentityProvider(dsn, commonSchema, segmentBatchSize);
            }

            return new IntHiLowIdentityProvider(dsn, commonSchema, segmentBatchSize);
        }

        private object SaveLoopAndChildren(
            HierarchicalLoopContainer loop,
            ref int positionInInterchange,
            object interchangeId,
            object functionalGroupId,
            object transactionSetId,
            string transactionSetCode,
            object parentId)
        {
            object loopId = null;
            if (loop is HierarchicalLoop hLoop)
            {
                loopId = this.SaveHierarchicalLoop(
                    hLoop,
                    interchangeId,
                    transactionSetId,
                    transactionSetCode,
                    parentId);
            }
            else if (loop is Loop loopContainer)
            {
                loopId = this.SaveLoop(loopContainer, interchangeId, transactionSetId, transactionSetCode, parentId);
            }

            if (loopId == null || loopId == this.DefaultIdentityTypeValue)
            {
                throw new InvalidOperationException(
                    string.Format(Resources.LoopCreationError, interchangeId, positionInInterchange));
            }

            this.SaveSegment(
                null,
                loop,
                positionInInterchange,
                interchangeId,
                functionalGroupId,
                transactionSetId,
                parentId,
                loopId);

            foreach (var seg in loop.Segments)
            {
                if (seg is HierarchicalLoopContainer hierarchicalLoopContainer)
                {
                    positionInInterchange++;
                    this.SaveLoopAndChildren(
                        hierarchicalLoopContainer,
                        ref positionInInterchange,
                        interchangeId,
                        functionalGroupId,
                        transactionSetId,
                        transactionSetCode,
                        loopId);
                }
                else
                {
                    this.SaveSegment(
                        null,
                        seg,
                        ++positionInInterchange,
                        interchangeId,
                        functionalGroupId,
                        transactionSetId,
                        loopId);
                }
            }

            foreach (var hl in loop.HLoops)
            {
                positionInInterchange++;
                this.SaveLoopAndChildren(
                    hl,
                    ref positionInInterchange,
                    interchangeId,
                    functionalGroupId,
                    transactionSetId,
                    transactionSetCode,
                    loopId);
            }

            return loopId;
        }

        private void MarkInterchangeWithError(object interchangeId)
        {
            var cmd =
                new SqlCommand($"UPDATE [{this.Schema}].Interchange SET HasError = 1 WHERE Id = @interchangeId");
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            this.ExecuteCmd(cmd);
        }

        private object SaveInterchange(Interchange interchange, string filename, string userName)
        {
            var date = DateTime.MaxValue;

            try
            {
                date = interchange.InterchangeDate;
            }
            catch (Exception exc)
            {
                Trace.TraceWarning(
                    Resources.InterchangeDateTimeParsingError,
                    interchange.GetElement(9),
                    interchange.GetElement(10),
                    exc.Message);
            }

            var interchangeId = this.idProvider.NextId(this.Schema, "Interchange");
            var containerId = this.idProvider.NextId(this.CommonDb.Schema, "Container");

            var cmd = new SqlCommand(
                this.GetContainerIdSql("ISA")
                + $"INSERT INTO [{this.Schema}].[Interchange] (Id, SenderId, ReceiverId, ControlNumber, [Date], SegmentTerminator, ElementSeparator, ComponentSeparator, Filename, HasError, CreatedBy, CreatedDate)\n"
                + "VALUES (@id, @senderId, @receiverId, @controlNumber, @date, @segmentTerminator, @elementSeparator, @componentSeparator, @filename, 0, @createdBy, getdate())");

            cmd.Parameters.AddWithValue("@id", interchangeId);
            cmd.Parameters.AddWithValue("@containerId", containerId);
            cmd.Parameters.AddWithValue("@senderId", interchange.InterchangeSenderId);
            cmd.Parameters.AddWithValue("@receiverId", interchange.InterchangeReceiverId);
            cmd.Parameters.AddWithValue("@controlNumber", interchange.InterchangeControlNumber);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@segmentTerminator", interchange.Delimiters.SegmentTerminator);
            cmd.Parameters.AddWithValue("@elementSeparator", interchange.Delimiters.ElementSeparator);
            cmd.Parameters.AddWithValue("@componentSeparator", interchange.Delimiters.SubElementSeparator);
            cmd.Parameters.AddWithValue("@filename", filename);
            cmd.Parameters.AddWithValue("@createdBy", userName);

            this.ExecuteCmd(cmd);

            return interchangeId;
        }

        private object SaveFunctionalGroup(FunctionGroup functionGroup, object interchangeId)
        {
            string idCode;
            var date = DateTime.MaxValue;
            int controlNumber = 0;
            string version;

            if (functionGroup.FunctionalIdentifierCode.Length <= 2)
            {
                idCode = functionGroup.FunctionalIdentifierCode;
            }
            else
            {
                idCode = functionGroup.FunctionalIdentifierCode.Substring(0, 2);
                Trace.TraceWarning(
                    Resources.FunctionalIdentifierTruncatedWarning,
                    functionGroup.FunctionalIdentifierCode);
            }

            try
            {
                date = functionGroup.Date;
            }
            catch (Exception exc)
            {
                Trace.TraceWarning(
                    Resources.FunctionalGroupDateTimeParsingError,
                    functionGroup.GetElement(4),
                    functionGroup.GetElement(5),
                    exc.Message);
            }

            try
            {
                controlNumber = functionGroup.ControlNumber;
            }
            catch (Exception exc)
            {
                Trace.TraceWarning(
                    Resources.FunctionalGroupControlNumberParsingError,
                    functionGroup.GetElement(6),
                    exc.Message);
            }

            if (functionGroup.VersionIdentifierCode.Length <= 12)
            {
                version = functionGroup.VersionIdentifierCode;
            }
            else
            {
                version = functionGroup.VersionIdentifierCode.Substring(0, 12);
                Trace.TraceWarning(
                    Resources.FunctionalGroupVersionNumberTruncatedWarning,
                    functionGroup.VersionIdentifierCode);
            }

            var functionalGroupId = this.idProvider.NextId(this.Schema, "FunctionalGroup");
            var containerId = this.idProvider.NextId(this.CommonDb.Schema, "Container");

            var cmd = new SqlCommand(
                this.GetContainerIdSql("GS") 
                + $"INSERT INTO [{this.Schema}].[FunctionalGroup] (Id, InterchangeId, FunctionalIdCode, Date, ControlNumber, Version)\n"
                + "VALUES (@id, @interchangeId, @functionalIdCode, @date, @controlNumber, @version)");

            cmd.Parameters.AddWithValue("@id", functionalGroupId);
            cmd.Parameters.AddWithValue("@containerId", containerId);
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            cmd.Parameters.AddWithValue("@functionalIdCode", idCode);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@controlNumber", controlNumber);
            cmd.Parameters.AddWithValue("@version", version);

            this.ExecuteCmd(cmd);

            return functionalGroupId;
        }

        private object SaveTransactionSet(Transaction transaction, object interchangeId, object functionalGroupId)
        {
            string controlNumber = transaction.ControlNumber;
            if (controlNumber.Length > 9)
            {
                controlNumber = controlNumber.Substring(0, 9);
                Trace.TraceWarning(
                    Resources.TransactionControlNumberTruncatedWarning,
                    transaction.ControlNumber);
            }

            var transactionSetId = this.idProvider.NextId(this.Schema, "TransactionSet");
            var containerId = this.idProvider.NextId(this.CommonDb.Schema, "Container");

            var cmd = new SqlCommand(
                this.GetContainerIdSql("ST")
                + $"INSERT INTO [{this.Schema}].[TransactionSet] (Id, InterchangeId, FunctionalGroupId, IdentifierCode, ControlNumber)\n"
                + "VALUES (@id, @interchangeId, @functionalGroupId, @identifierCode, @controlNumber)");

            cmd.Parameters.AddWithValue("@id", transactionSetId);
            cmd.Parameters.AddWithValue("@containerId", containerId);
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            cmd.Parameters.AddWithValue("@functionalGroupId", functionalGroupId);
            cmd.Parameters.AddWithValue("@identifierCode", transaction.IdentifierCode);
            cmd.Parameters.AddWithValue("@controlNumber", controlNumber);

            this.ExecuteCmd(cmd);

            return transactionSetId;
        }

        private object SaveHierarchicalLoop(
            HierarchicalLoop loop,
            object interchangeId,
            object transactionSetId,
            string transactionSetCode,
            object parentLoopId)
        {
            var hierarchicalLoopId = this.idProvider.NextId(this.Schema, "Loop");
            var containerId = this.idProvider.NextId(this.CommonDb.Schema, "Container");

            var cmd = new SqlCommand(
                this.GetContainerIdSql("HL")
                + $"INSERT INTO [{this.Schema}].[Loop] (Id, ParentLoopId, InterchangeId, TransactionSetId, TransactionSetCode, SpecLoopId, LevelId, LevelCode, StartingSegmentId)\n"
                + "VALUES (@id, @parentLoopId, @interchangeId, @transactionSetId, @transactionSetCode, @specLoopId, @levelId, @levelCode, 'HL')");

            cmd.Parameters.AddWithValue("@id", hierarchicalLoopId);
            cmd.Parameters.AddWithValue("@containerId", containerId);
            cmd.Parameters.AddWithValue("@parentLoopId", parentLoopId != null && parentLoopId != this.DefaultIdentityTypeValue ? parentLoopId : DBNull.Value);
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            cmd.Parameters.AddWithValue("@transactionSetId", transactionSetId);
            cmd.Parameters.AddWithValue("@transactionSetCode", transactionSetCode);
            cmd.Parameters.AddWithValue("@specLoopId", loop.Specification.LoopId);
            cmd.Parameters.AddWithValue("@levelId", loop.Id);
            cmd.Parameters.AddWithValue("@levelCode", loop.LevelCode);

            this.ExecuteCmd(cmd);

            return hierarchicalLoopId;
        }

        private bool SegmentHasChanged(
            DetachedSegment segment,
            int positionInInterchange,
            object interchangeId,
            int? previousRevisionId)
        {
            using (var conn = new SqlConnection(this.Dsn))
            {
                var cmd = new SqlCommand(
                    string.Format(
@"SELECT RevisionId, Deleted, Segment, r.RevisedBy, r.RevisionDate
FROM [{0}].Segment s
LEFT JOIN [{1}].Revision r ON s.RevisionId = r.Id
WHERE InterchangeId = @interchangeId AND PositionInInterchange = @positionInInterchange
ORDER BY RevisionId DESC",
                        this.Schema,
                        this.CommonDb.Schema),
                    conn);

                cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
                cmd.Parameters.AddWithValue("@positionInInterchange", positionInInterchange);

                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (Convert.ToBoolean(reader["Deleted"]))
                    {
                        throw new InvalidOperationException(
                            string.Format(
                                Resources.SegmentAlreadyDeletedError,
                                segment.SegmentId,
                                interchangeId,
                                positionInInterchange,
                                reader["RevisedBy"],
                                reader["RevisionDate"]));
                    }

                    if (previousRevisionId.HasValue && Convert.ToInt64(reader["RevisionId"]) != Convert.ToInt64(previousRevisionId))
                    {
                        throw new InvalidOperationException(
                            string.Format(
                                Resources.SegmentAlreadyRevisedError,
                                segment.SegmentId,
                                interchangeId,
                                positionInInterchange,
                                reader["RevisedBy"],
                                reader["RevisionDate"]));
                    }

                    return Convert.ToString(reader["Segment"]) != segment.SegmentString;
                }

                throw new InvalidOperationException(
                    string.Format(
                        Resources.SegmentDoesNotExist,
                        interchangeId,
                        positionInInterchange));
            }
        }
    }
}