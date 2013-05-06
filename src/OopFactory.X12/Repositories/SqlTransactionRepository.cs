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
    /// Class for storing, retrieving and revising X12 messages.
    /// This library only does inserts.  Edits and Deletes are accomplished through revisions, but all revisions are retained.
    /// The Get methods will allow you choose the revision you want.
    /// </summary>
    /// <typeparam name="T">The type of all identity columns:  supports int or long</typeparam>
    public class SqlTransactionRepository<T> : SqlReadOnlyTransactionRepository<T> where T : struct
    {
        protected DbCreation<T> _commonDb;
        protected DbCreation<T> _transactionDb;
        private bool _schemaEnsured;
        private readonly Dictionary<string, SegmentSpecification> _specs;
        private int _batchSize;
        private int _batchCount;
        private StringBuilder _batchSql;
        
        public SqlTransactionRepository(string dsn)
            : this(dsn, new SpecificationFinder(), new string[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER" }, "dbo")
        {
        }

        public SqlTransactionRepository(string dsn, string schema)
            : this(dsn, new SpecificationFinder(), new string[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER" }, schema)
        {
        }
        
        public SqlTransactionRepository(string dsn, ISpecificationFinder specFinder, string[] indexedSegments, string schema = "dbo", string commonSchema = "dbo", int segmentBatchSize = 1000)
            : base(dsn, schema)
        {
            _commonDb = new DbCreation<T>(dsn, commonSchema);
            _transactionDb = new DbCreation<T>(dsn, schema);
            _schemaEnsured = false;
            _batchSize = segmentBatchSize;
            _batchCount = 0;
            _batchSql = new StringBuilder();
            _specs = new Dictionary<string, SegmentSpecification>();
            foreach (var segmentId in indexedSegments)
            {
                var spec = specFinder.FindSegmentSpec("5010", segmentId.Trim());
                _specs.Add(segmentId.Trim(), spec);
            }
        }

        /// <summary>
        /// override this with no implementation when your database is already created and you will not need to check for existance of the objects.
        /// </summary>
        protected virtual void EnsureSchema()
        {
            if (!_schemaEnsured) // this only needs to be done once
            {
                if (!_commonDb.SchemaExists())
                    _commonDb.CreateSchema();

                if (!_commonDb.TableExists("Container"))
                    _commonDb.CreateContainerTable();

                if (!_commonDb.TableExists("Revision"))
                    _commonDb.CreateRevisionTable();

                if (!_commonDb.TableExists("X12CodeList"))
                    _commonDb.CreateX12CodeListTable();

                if (!_transactionDb.SchemaExists())
                    _transactionDb.CreateSchema();

                if (!_transactionDb.TableExists("Interchange"))
                    _transactionDb.CreateInterchangeTable();

                if (!_transactionDb.TableExists("FunctionalGroup"))
                    _transactionDb.CreateFunctionalGroupTable();

                if (!_transactionDb.TableExists("TransactionSet"))
                    _transactionDb.CreateTransactionSetTable();

                if (!_transactionDb.TableExists("Loop"))
                    _transactionDb.CreateLoopTable();

                if (!_transactionDb.TableExists("Segment"))
                    _transactionDb.CreateSegmentTable();

                if (!_transactionDb.TableExists("ParsingError"))
                    _transactionDb.CreateParsingErrorTable();

                if (!_commonDb.FunctionExists("SplitSegment"))
                    _commonDb.CreateSplitSegmentFunction();

                if (!_commonDb.FunctionExists("FlatElements"))
                    _commonDb.CreateFlatElementsFunction();

                if (!_transactionDb.FunctionExists("GetAncestorLoops"))
                    _transactionDb.CreateGetAncestorLoopsFunction();

                if (!_transactionDb.FunctionExists("GetDescendantLoops"))
                    _transactionDb.CreateGetDescendantLoopsFunction();

                if (!_transactionDb.FunctionExists("GetTransactionSetSegments"))
                    _transactionDb.CreateGetTransactionSetSegmentsFunction();

                if (!_transactionDb.FunctionExists("GetTransactionSegments"))
                    _transactionDb.CreateGetTransactionSegmentsFunction();

                foreach (var spec in _specs.Values)
                {
                    if (!_transactionDb.TableExists(spec.SegmentId))
                        _transactionDb.CreateIndexedSegmentTable(spec, _commonDb.Schema);
                    else if (!_transactionDb.TableColumnExists(spec.SegmentId, "ErrorId"))
                        _transactionDb.AddErrorIdToIndexedSegmentTable(spec.SegmentId);

                    foreach (var element in spec.Elements)
                    {
                        if (element.Type == ElementDataTypeEnum.Identifier && !string.IsNullOrEmpty(element.QualifierSetId) && element.AllowedIdentifiers.Count > 0)
                        {
                            if (_commonDb.ElementCountInX12CodeListTable(element.QualifierSetId) == 0)
                            {
                                foreach (var identifier in element.AllowedIdentifiers)
                                    _commonDb.AddToX12CodeListTable(element.QualifierSetId, identifier.ID, identifier.Description);
                            }
                        }
                    }
                }

                if (!_transactionDb.ViewExists("Entity")
                    && _specs.ContainsKey("NM1")
                    && _specs.ContainsKey("N3")
                    && _specs.ContainsKey("N4")
                    && _specs.ContainsKey("PER")
                    && _specs.ContainsKey("DMG"))
                    _transactionDb.CreateEntityView(_commonDb.Schema);

                _schemaEnsured = true;
            }
        }

        /// <summary>
        /// Saves the entire interchange into the database as individual segments and the relationships between the segments and loops
        /// </summary>
        /// <param name="interchange">The parsed interchange object</param>
        /// <param name="filename"></param>
        /// <param name="userName"></param>
        /// <returns>The interchangeId from the database</returns>
        public T Save(Interchange interchange, string filename, string userName)
        {
            EnsureSchema();
            InitBatch();
            int positionInInterchange = 1;
            
            T interchangeId = SaveInterchange(interchange, filename, userName);
            try
            {
                SaveSegment(null, interchange, positionInInterchange, interchangeId);

                foreach (var fg in interchange.FunctionGroups)
                {
                    T functionalGroupId = SaveFunctionalGroup(fg, interchangeId);
                    SaveSegment(null, fg, ++positionInInterchange, interchangeId, functionalGroupId);

                    foreach (var tran in fg.Transactions)
                    {
                        string transactionSetCode = tran.IdentifierCode;
                        T transactionSetId = SaveTransactionSet(tran, interchangeId, functionalGroupId);
                        SaveSegment(null, tran, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);

                        foreach (var seg in tran.Segments)
                        {
                            if (seg is HierarchicalLoopContainer)
                            {
                                positionInInterchange++;
                                SaveLoopAndChildren((HierarchicalLoopContainer)seg, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, transactionSetCode, null);
                            }
                            else
                                SaveSegment(null, seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);
                        }
                        foreach (var hl in tran.HLoops)
                        {
                            positionInInterchange++;
                            SaveLoopAndChildren(hl, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, transactionSetCode, null);
                        }

                        foreach (var seg in tran.TrailerSegments)
                            SaveSegment(null, seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);
                    }

                    foreach (var seg in fg.TrailerSegments)
                        SaveSegment(null, seg, ++positionInInterchange, interchangeId, functionalGroupId);
                }

                foreach (var seg in interchange.TrailerSegments)
                    SaveSegment(null, seg, ++positionInInterchange, interchangeId);

                ExecuteBatch();
                return interchangeId;
            }
            catch (Exception)
            {
                MarkInterchangeWithError(interchangeId);
                throw;
            }
        }

        /// <summary>
        /// This will save revisions to an x12 transaction that was returned from the GetTransactionSegments method.
        /// The update is stored as inserts into the database, and only the most current revision that hasn't been deleted is returned on the next retrieval
        /// </summary>
        /// <param name="segments">The segments to be updated, only segments with a different SegmentString or Deleted value will be updated</param>
        /// <param name="comments">The reason for the revision</param>
        /// <param name="revisedBy">Ther username of the user who has made the revision</param>
        /// <returns></returns>
        public int SaveRevision(IList<RepoSegment<T>> segments, string comments, string revisedBy)
        {
            int? revisionId;
            using (SqlConnection conn = new SqlConnection(_dsn))
            {
                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try
                {

                    string sql = string.Format(@"
insert into [{0}].[Revision] (SchemaName,Comments,RevisionDate,RevisedBy) 
values (@schemaName, @comments, getdate(), @revisedBy)

select scope_identity()", _commonDb.Schema);

                    SqlCommand cmd = new SqlCommand(sql, conn, sqlTran);
                    cmd.Parameters.AddWithValue("@schemaName", _schema);
                    cmd.Parameters.AddWithValue("@comments", comments);
                    cmd.Parameters.AddWithValue("@revisedBy", revisedBy);
                    revisionId = Convert.ToInt32(ExecuteScalar(cmd));

                    foreach (var segment in segments)
                    {
                        SaveSegment(sqlTran, segment.Segment, segment.PositionInInterchange, segment.InterchangeId, segment.FunctionalGroupId, segment.TransactionSetId, segment.ParentLoopId, segment.LoopId, revisionId, segment.RevisionId, segment.Deleted);
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

        private T SaveLoopAndChildren(HierarchicalLoopContainer loop, ref int positionInInterchange, T interchangeId, T functionalGroupId, T transactionSetId, string transactionSetCode, T? parentId)
        {
            T? loopId = null;
            if (loop is HierarchicalLoop)
            {
                loopId = SaveHierarchicalLoop((HierarchicalLoop)loop, interchangeId, transactionSetId, transactionSetCode, parentId);
            }
            else if (loop is Loop)
            {
                loopId = SaveLoop((Loop)loop, interchangeId, transactionSetId, transactionSetCode, parentId);
            }
            if (loopId.HasValue)
            {
                SaveSegment(null, loop, positionInInterchange, interchangeId, functionalGroupId, transactionSetId, parentId, loopId);

                foreach (var seg in loop.Segments)
                {
                    if (seg is HierarchicalLoopContainer)
                    {
                        positionInInterchange++;
                        SaveLoopAndChildren((HierarchicalLoopContainer)seg, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, transactionSetCode, loopId);
                    }
                    else
                        SaveSegment(null, seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId, loopId);
                }

                foreach (var hl in loop.HLoops)
                {
                    positionInInterchange++;
                    SaveLoopAndChildren(hl, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, transactionSetCode, loopId);
                }
                return loopId.Value;
            }
            else
                throw new InvalidOperationException(string.Format("Loop could not be created for interchange {0} position {1}.", interchangeId, positionInInterchange));
        }

        private void MarkInterchangeWithError(T interchangeId)
        {
            var cmd = new SqlCommand(string.Format("update [{0}].Interchange set HasError = 1 where Id = @interchangeId", _schema));
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            ExecuteCmd(cmd);
        }

        protected virtual string GetContainerIdSql(string segmentId)
        {
            if (typeof(T) == typeof(Guid))
            {
                return string.Format(@"
DECLARE @id uniqueidentifier

SET @id = newid()

INSERT INTO [{1}].[Container] VALUES (@id, '{0}','{2}')

SELECT @id ", _schema, _commonDb.Schema, segmentId);
            }
            else
            {
                return string.Format(@"
DECLARE @id int

INSERT INTO [{1}].[Container] VALUES ('{0}','{2}')

SELECT @id = scope_identity() ", _schema, _commonDb.Schema, segmentId);
            }

        }

        private T SaveInterchange(Interchange interchange, string filename, string userName)
        {
            DateTime date = DateTime.MaxValue;

            try
            {
                date = interchange.InterchangeDate;
            }
            catch (Exception exc)
            {
                Trace.TraceWarning("Interchange date '{0}' and time '{1}' could not be parsed. {2}", interchange.GetElement(9), interchange.GetElement(10), exc.Message);
            }
            
            SqlCommand cmd = new SqlCommand(GetContainerIdSql("ISA") + string.Format(@"
INSERT INTO [{0}].[Interchange] (Id, SenderId, ReceiverId, ControlNumber, [Date], SegmentTerminator, ElementSeparator, ComponentSeparator, Filename, HasError, CreatedBy, CreatedDate)
VALUES (@id, @senderId, @receiverId, @controlNumber, @date, @segmentTerminator, @elementSeparator, @componentSeparator, @filename, 0, @createdBy, getdate())

SELECT @id ", _schema));
            cmd.Parameters.AddWithValue("@senderId", interchange.InterchangeSenderId);
            cmd.Parameters.AddWithValue("@receiverId", interchange.InterchangeReceiverId);
            cmd.Parameters.AddWithValue("@controlNumber", interchange.InterchangeControlNumber);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@segmentTerminator", interchange.Delimiters.SegmentTerminator);
            cmd.Parameters.AddWithValue("@elementSeparator", interchange.Delimiters.ElementSeparator);
            cmd.Parameters.AddWithValue("@componentSeparator", interchange.Delimiters.SubElementSeparator);
            cmd.Parameters.AddWithValue("@filename", filename);
            cmd.Parameters.AddWithValue("@createdBy", userName);

            var interchangeId = ExecuteScalar(cmd);
            return base.ConvertT(interchangeId);
        }

        private T SaveFunctionalGroup(FunctionGroup functionGroup, T interchangeId)
        {
            string idCode;
            DateTime date = DateTime.MaxValue;
            int controlNumber = 0;
            string version;

            if (functionGroup.FunctionalIdentifierCode.Length <= 2)
                idCode = functionGroup.FunctionalIdentifierCode;
            else
            {
                idCode = functionGroup.FunctionalIdentifierCode.Substring(0, 2);
                Trace.TraceWarning("FunctionalIdentifier code '{0}' will be truncated because it exceeds the max length of 2.", functionGroup.FunctionalIdentifierCode);
            }
            try
            {
                date = functionGroup.Date;
            }
            catch (Exception exc)
            {
                Trace.TraceWarning("FunctionalGroup date '{0}' and time '{1}' could not be parsed. {2}", functionGroup.GetElement(4), functionGroup.GetElement(5), exc.Message);
            }
            try
            {
                controlNumber = functionGroup.ControlNumber;
            }
            catch (Exception exc)
            {
                Trace.TraceWarning("FunctionalGroup control number '{0}' could not be parsed. {1}", functionGroup.GetElement(6), exc.Message);
            }
            if (functionGroup.VersionIdentifierCode.Length <= 12)
                version = functionGroup.VersionIdentifierCode;
            else
            {
                version = functionGroup.VersionIdentifierCode.Substring(0, 12);
                Trace.TraceWarning("FunctionalGroup version number '{0}' will be truncated because it exceeds the max length of 12.", functionGroup.VersionIdentifierCode);
            }

            SqlCommand cmd = new SqlCommand(GetContainerIdSql("GS") + string.Format(@"
INSERT INTO [{0}].[FunctionalGroup] (Id, InterchangeId, FunctionalIdCode, Date, ControlNumber, Version)
VALUES (@id, @interchangeId, @functionalIdCode, @date, @controlNumber, @version)

SELECT @id
", _schema, _commonDb.Schema));
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            cmd.Parameters.AddWithValue("@functionalIdCode", idCode);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@controlNumber", controlNumber);
            cmd.Parameters.AddWithValue("@version", version);

            return ConvertT(ExecuteScalar(cmd));
        }
        
        private T SaveTransactionSet(Transaction transaction, T interchangeId, T functionalGroupId)
        {
            string controlNumber = transaction.ControlNumber;
            if (controlNumber.Length > 9)
            {
                controlNumber = controlNumber.Substring(0, 9);
                Trace.TraceWarning("Transaction control number '{0}' will be truncated because it exceeds the max length of 9.",transaction.ControlNumber);
            }

            SqlCommand cmd = new SqlCommand(GetContainerIdSql("ST") + string.Format(@"
INSERT INTO [{0}].[TransactionSet] (Id, InterchangeId, FunctionalGroupId, IdentifierCode, ControlNumber) 
VALUES (@id, @interchangeId, @functionalGroupId, @identifierCode, @controlNumber)

SELECT @id
", _schema, _commonDb.Schema));
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            cmd.Parameters.AddWithValue("@functionalGroupId", functionalGroupId);
            cmd.Parameters.AddWithValue("@identifierCode", transaction.IdentifierCode);
            cmd.Parameters.AddWithValue("@controlNumber", controlNumber);

            return ConvertT(ExecuteScalar(cmd));
        }
        
        private T SaveHierarchicalLoop(HierarchicalLoop loop, T interchangeId, T transactionSetId, string transactionSetCode, T? parentLoopId)
        {
            SqlCommand cmd = new SqlCommand(GetContainerIdSql("HL") + string.Format(@"
INSERT INTO [{0}].[Loop] (Id, ParentLoopId, InterchangeId, TransactionSetId, TransactionSetCode, SpecLoopId, LevelId, LevelCode, StartingSegmentId)
VALUES (@id, @parentLoopId, @interchangeId, @transactionSetId, @transactionSetCode, @specLoopId, @levelId, @levelCode, 'HL')

SELECT @id", _schema, _commonDb.Schema));
            cmd.Parameters.AddWithValue("@parentLoopId", (object)parentLoopId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            cmd.Parameters.AddWithValue("@transactionSetId", transactionSetId);
            cmd.Parameters.AddWithValue("@transactionSetCode", transactionSetCode);
            cmd.Parameters.AddWithValue("@specLoopId", loop.Specification.LoopId);
            cmd.Parameters.AddWithValue("@levelId", loop.Id);
            cmd.Parameters.AddWithValue("@levelCode", loop.LevelCode);

            return ConvertT(ExecuteScalar(cmd));            
        }

        protected virtual string GetEntityTypeCode(Loop loop)
        {
            if (new string[] { "CLI", "CUR", "G18", "MRC", "N1", "NM1", "NX1", "RDI" }.Contains(loop.SegmentId))
                return loop.GetElement(1);

            if (new string[] { "ENT", "LCD", "NX1", "PLA", "PT" }.Contains(loop.SegmentId))
                return loop.GetElement(2);

            if (new string[] { "IN1", "NX1", "SCH" }.Contains(loop.SegmentId))
                return loop.GetElement(3);

            return null;
        }

        protected string GetSaveLoopSql(T id, Loop loop, T interchangeId, T transactionSetId, string transactionSetCode, T? parentLoopId)
        {
            string entityIdentifierCode = GetEntityTypeCode(loop);

            StringBuilder sql = new StringBuilder();

            sql.AppendFormat(@"
INSERT INTO [{0}].[Loop] (Id, ParentLoopId, InterchangeId, TransactionSetId, TransactionSetCode, SpecLoopId, StartingSegmentId, EntityIdentifierCode)
VALUES ('{1}', {2}, '{3}', '{4}', '{5}', '{6}', '{7}', {8}) ", _schema
           , id
           , !parentLoopId.HasValue ? "NULL" : string.Format("'{0}'", parentLoopId)
           , interchangeId
           , transactionSetId
           , transactionSetCode
           , loop.Specification.LoopId
           , loop.SegmentId
           , entityIdentifierCode == null ? "NULL" : string.Format("'{0}'", entityIdentifierCode)
           );

            return sql.ToString();
        }
        
        protected virtual T SaveLoop(Loop loop, T interchangeId, T transactionSetId, string transactionSetCode, T? parentLoopId)
        {
            T? id = null;

            SqlCommand cmd = new SqlCommand(GetContainerIdSql(loop.SegmentId));

            try
            {
                 id = ConvertT(ExecuteScalar(cmd));
            }
            catch (Exception exc)
            {
                Trace.TraceError(exc.Message);
                throw;
            }

            try
            {
                ExecuteCmd(new SqlCommand(GetSaveLoopSql(id.Value, loop, interchangeId, transactionSetId, transactionSetCode, parentLoopId)
                    + " SELECT @id"));
            }
            catch (Exception exc)
            {
                Trace.TraceError(exc.Message);
                throw;
            }

            return id.Value;
        }

        private bool SegmentHasChanged(DetachedSegment segment, int positionInInterchange, T interchangeId, int? previousRevisionId)
        {
            using (var conn = new SqlConnection(_dsn))
            {
                SqlCommand cmd = new SqlCommand(string.Format(@"
select RevisionId, Deleted, Segment, r.RevisedBy, r.RevisionDate
from [{0}].Segment s
left join [{1}].Revision r on s.RevisionId = r.Id
where InterchangeId = @interchangeId and PositionInInterchange = @positionInInterchange
order by RevisionId desc", _schema, _commonDb.Schema), conn);
                cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
                cmd.Parameters.AddWithValue("@positionInInterchange", positionInInterchange);

                conn.Open();
                var reader = cmd.ExecuteReader();
                // only need to read first row
                if (reader.Read())
                {
                    if (Convert.ToBoolean(reader["Deleted"]))
                        throw new InvalidOperationException(string.Format("Segment {0} of interchange {1} in position {2} has already been deleted by {3} at {4}.", segment.SegmentId, interchangeId, positionInInterchange, reader["RevisedBy"], reader["RevisionDate"]));

                    if (previousRevisionId.HasValue && Convert.ToInt64(reader["RevisionId"]) != Convert.ToInt64(previousRevisionId))
                        throw new InvalidOperationException(string.Format("Segment {0} of interchange {1} in position {2} has already been revised by {3} at {4}.", segment.SegmentId, interchangeId, positionInInterchange, reader["RevisedBy"], reader["RevisionDate"]));

                    

                    return Convert.ToString(reader["Segment"]) != segment.SegmentString;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("A segment does not exist for interchange {0} at position {1}.", interchangeId, positionInInterchange));
                }
            }
        }
        protected virtual void SaveSegment(SqlTransaction tran, DetachedSegment segment, int positionInInterchange, T interchangeId, T? functionalGroupId = null, T? transactionSetId = null, T? parentLoopId = null, T? loopId = null, int? revisionId = null, int? previousRevisionId = null, bool deleted = false)
        {
            if (!revisionId.HasValue || SegmentHasChanged(segment, positionInInterchange, interchangeId, previousRevisionId) || deleted)
            {
                string segmentSql = string.Format(@"
INSERT INTO [{0}].[Segment] (InterchangeId, FunctionalGroupId, TransactionSetId, ParentLoopId, LoopId, RevisionId, Deleted, PositionInInterchange, SegmentId, Segment)
VALUES ({1}, {2}, {3}, {4}, {5}, isnull({6},0), {7}, {8}, '{9}', '{10}') ", 
                    _schema,
                    string.Format("'{0}'", interchangeId),
                    (object)functionalGroupId == null ? "NULL" : string.Format("'{0}'", functionalGroupId),
                    (object)transactionSetId == null ? "NULL" : string.Format("'{0}'", transactionSetId),
                    (object)parentLoopId == null ? "NULL" : string.Format("'{0}'", parentLoopId),
                    (object)loopId == null ? "NULL" : string.Format("'{0}'", loopId),
                    (object)revisionId == null ? "NULL" : string.Format("'{0}'", revisionId),
                    deleted ? "1" : "0",
                    positionInInterchange,
                    segment.SegmentId.Replace("'", "''"),
                    segment.SegmentString.Replace("'","''"));

                if (tran != null)
                {
                    SqlCommand cmd = new SqlCommand(segmentSql);
                    cmd.Connection = tran.Connection;
                    cmd.Transaction = tran;
                    ExecuteCmd(cmd);
                }
                else
                    AddSqlToBatch(segmentSql);


                if (_specs.ContainsKey(segment.SegmentId))
                {
                    StringBuilder parsingError = new StringBuilder();

                    List<string> fieldNames = new List<string>();
                    List<string> parameterNames = new List<string>();
                    var spec = _specs[segment.SegmentId]; 
                    int maxElements = spec != null ? spec.Elements.Count : 0;
                    for (int i = 1; i <= segment.ElementCount; i++)
                    {
                        if (i <= maxElements)
                        {
                            fieldNames.Add(string.Format("[{0:00}]", i));
                        }
                        else
                        {
                            string val = segment.GetElement(i);
                            string message = string.Format("Element {2}{3:00} in position {1} of interchange {0} with value {4} will NOT be indexed because it exceeds the {5} specified number of elements.", interchangeId, positionInInterchange, segment.SegmentId, i, val, maxElements);
                            Trace.TraceInformation(message);
                            parsingError.AppendLine(message);
                        }
                    }

                    StringBuilder sql = new StringBuilder();
                    sql.AppendFormat(@"INSERT INTO [{0}].[{1}] (InterchangeId, PositionInInterchange, TransactionSetId, ParentLoopId, LoopId, RevisionId, Deleted, {2}, ErrorId)
                    VALUES ({3}, {4}, {5}, {6}, {7}, isnull({8},0), {9}, ",
                        _schema, segment.SegmentId, string.Join(",", fieldNames),
                    string.Format("'{0}'", interchangeId),
                        positionInInterchange,
                        (object)transactionSetId == null ? "NULL" : string.Format("'{0}'", transactionSetId),
                        (object)parentLoopId == null ? "NULL" : string.Format("'{0}'", parentLoopId),
                        (object)loopId == null ? "NULL" : string.Format("'{0}'", loopId),
                        (object)revisionId == null ? "NULL" : string.Format("'{0}'", revisionId),
                        deleted ? "1" : "0");

                    for (int i = 1; i <= segment.ElementCount && i <= maxElements; i++)
                    {
                        string val = segment.GetElement(i);
                        if (spec != null)
                        {
                            var elementSpec = spec.Elements[i - 1];
                            int maxLength = elementSpec.MaxLength;

                            if (maxLength > 0 && val.Length > maxLength)
                            {
                                string message = string.Format("Element {2}{3:00} in position {1} of interchange {0} will be truncated because {4} exceeds the max length of {5}.", interchangeId, positionInInterchange, segment.SegmentId, i, val, maxLength);
                                Trace.TraceInformation(message);
                                parsingError.AppendLine(message);
                                val = val.Substring(0, maxLength);
                            }

                            if (elementSpec.Type == ElementDataTypeEnum.Numeric && elementSpec.ImpliedDecimalPlaces > 0)
                            {
                                int intVal = 0;
                                if (string.IsNullOrWhiteSpace(val))
                                {
                                    sql.Append("NULL, ");
                                }
                                else if (int.TryParse(val, out intVal))
                                {
                                    decimal denominator = (decimal)Math.Pow(10, elementSpec.ImpliedDecimalPlaces);
                                    sql.AppendFormat("{0}, ", (decimal)intVal / denominator);
                                }
                                else
                                {
                                    string message = string.Format("Element {2}{3:00} in position {1} of interchange {0} cannot be indexed because '{4}' could not be parsed into an implied decimal with precision {5}.", interchangeId, positionInInterchange, segment.SegmentId, i, val, elementSpec.ImpliedDecimalPlaces);
                                    Trace.TraceInformation(message);
                                    parsingError.AppendLine(message);
                                    sql.AppendFormat("NULL, ");
                                }
                            }
                            else
                            {
                                if (elementSpec.Type == ElementDataTypeEnum.Numeric || elementSpec.Type == ElementDataTypeEnum.Decimal)
                                {
                                    if (!string.IsNullOrWhiteSpace(val))
                                        sql.AppendFormat("{0}, ", val);
                                    else
                                        sql.Append("NULL, ");
                                }
                                else
                                    sql.AppendFormat("'{0}', ", val.Replace("'", "''"));
                            }
                        }
                    }
                    T? errorId = null;
                    string errorMessage = parsingError.ToString();
                    if (!string.IsNullOrWhiteSpace(errorMessage)) 
                    {
                        var errSql = string.Format(@"
INSERT INTO [{0}].ParsingError (InterchangeId, PositionInInterchange, RevisionId, Message) 
VALUES (@interchangeId, @positionInInterchange, isnull(@revisionId,0), @message) 
SELECT scope_identity()", _schema);

                        if (typeof(T) == typeof(Guid))
                        {
                            errSql = string.Format(@"DECLARE @id uniqueidentifier
SET @id = newid()
INSERT INTO [{0}].ParsingError (Id, InterchangeId, PositionInInterchange, RevisionId, Message) 
VALUES (@id,@interchangeId, @positionInInterchange, isnull(@revisionId,0), @message) 
SELECT @id", _schema);
                        }

                        SqlCommand errCmd = new SqlCommand(errSql);

                        errCmd.Parameters.AddWithValue("@interchangeId", interchangeId);
                        errCmd.Parameters.AddWithValue("@positionInInterchange", positionInInterchange);
                        errCmd.Parameters.AddWithValue("@revisionId", (object) revisionId ?? DBNull.Value);
                        errCmd.Parameters.AddWithValue("@message", errorMessage);

                        errorId = ConvertT(ExecuteScalar(errCmd));

                    }
                    sql.AppendFormat("{0})", (object)errorId == null ? "NULL":string.Format("'{0}'", errorId));

                    if (tran != null)
                    {
                        var cmd = new SqlCommand(sql.ToString());
                        cmd.Connection = tran.Connection;
                        cmd.Transaction = tran;
                        ExecuteCmd(cmd);
                    }
                    else
                        AddSqlToBatch(sql.ToString());
                }
            }
        }

        protected void AddSqlToBatch(string sql, params object[] args)
        {
            _batchCount++;
            _batchSql.AppendFormat(sql, args);
            if (_batchCount >= _batchSize)
                ExecuteBatch();                
        }

        private void InitBatch()
        {
            _batchCount = 0;
            _batchSql = new StringBuilder();
        }

        private void ExecuteBatch()
        {
            string sql = _batchSql.ToString();
            if (!string.IsNullOrWhiteSpace(sql))
            {
                try
                {
                    ExecuteCmd(new SqlCommand(sql));
                    InitBatch();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        protected void ExecuteCmd(SqlCommand cmd)
        {
            _transactionDb.ExecuteCmd(cmd);
        }

        protected object ExecuteScalar(SqlCommand cmd)
        {
            return _transactionDb.ExecuteScalar(cmd);
        }
    }
}
