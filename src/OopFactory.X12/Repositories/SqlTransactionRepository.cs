using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing;
using System.Data.SqlClient;
using System.Diagnostics;

namespace OopFactory.X12.Repositories
{
    public class SqlTransactionRepository : SqlReadOnlyTransactionRepository
    {
        protected readonly ISpecificationFinder _specFinder;
        protected readonly string[] _indexedSegments;
        protected DbCreation _commonDb;
        protected DbCreation _transactionDb;
        
        public SqlTransactionRepository(string dsn)
            : this(dsn, new SpecificationFinder(), new string[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER" }, "dbo")
        {
        }

        public SqlTransactionRepository(string dsn, string schema)
            : this(dsn, new SpecificationFinder(), new string[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER" }, schema)
        {
        }
        
        public SqlTransactionRepository(string dsn, ISpecificationFinder specFinder, string[] indexedSegments, string schema = "dbo", string commonSchema = "dbo")
            : base(dsn, schema)
        {
            _specFinder = specFinder;
            _indexedSegments = indexedSegments;
            _commonDb = new DbCreation(dsn, commonSchema);
            _transactionDb = new DbCreation(dsn, schema);
        }

        protected virtual void EnsureSchema()
        {
            if (!_commonDb.TableExists("Container"))
                _commonDb.CreateContainerTable();

            if (!_commonDb.TableExists("Revision"))
                _commonDb.CreateRevisionTable();

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

            foreach (var segmentId in _indexedSegments)
            {
                var spec = _specFinder.FindSegmentSpec("5010", segmentId);
                if (spec != null && !_transactionDb.TableExists(segmentId))
                    _transactionDb.CreateIndexedSegmentTable(spec);
            }
        }

        public int Save(Interchange interchange, string filename, string userName)
        {
            EnsureSchema();
            int positionInInterchange = 1;
            
            int interchangeId = SaveInterchange(interchange, filename, userName);
            try
            {
                SaveSegment(null, interchange, positionInInterchange, interchangeId);

                foreach (var fg in interchange.FunctionGroups)
                {
                    int functionalGroupId = SaveFunctionalGroup(fg);
                    SaveSegment(null, fg, ++positionInInterchange, interchangeId, functionalGroupId);

                    foreach (var tran in fg.Transactions)
                    {
                        string transactionSetCode = tran.IdentifierCode;
                        int transactionSetId = SaveTransactionSet(tran);
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

                return interchangeId;
            }
            catch (Exception)
            {
                MarkInterchangeWithError(interchangeId);
                throw;
            }
        }

        public int SaveRevision(int loopId, IList<RepoSegment> segments, string comments, string revisedBy)
        {
            int revisionId = 1;
            using (SqlConnection conn = new SqlConnection(_dsn))
            {
                conn.Open();
                var sqlTran = conn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand(string.Format(@"
insert into [{0}].[Revision] (SchemaName,Comments,RevisionDate,RevisedBy) 
values (@schemaName, @comments, getdate(), @revisedBy)

select scope_identity()", _commonDb.Schema), conn, sqlTran);
                    cmd.Parameters.AddWithValue("@schemaName", _schema);
                    cmd.Parameters.AddWithValue("@comments", comments);
                    cmd.Parameters.AddWithValue("@revisedBy", revisedBy);
                    revisionId = Convert.ToInt32(ExecuteScalar(cmd));

                    foreach (var segment in segments)
                    {
                        var newSegment = new DetachedSegment(new X12DelimiterSet(segment.SegmentTerminator, segment.ElementSeparator, ':'), segment.SegmentString);
                        SaveSegment(sqlTran, newSegment, segment.PositionInInterchange, segment.InterchangeId, segment.FunctionalGroupId, segment.TransactionSetId, segment.ParentLoopId, segment.LoopId, revisionId, segment.RevisionId, segment.Deleted);
                    }

                    sqlTran.Commit();
                }
                catch (Exception)
                {
                    sqlTran.Rollback();
                    throw;
                }
            }
            return revisionId;
        }

        private int SaveLoopAndChildren(HierarchicalLoopContainer loop, ref int positionInInterchange, int interchangeId, int functionalGroupId, int transactionSetId, string transactionSetCode, int? parentId)
        {
            int loopId = 0;
            if (loop is HierarchicalLoop)
            {
                loopId = SaveHierarchicalLoop((HierarchicalLoop)loop, interchangeId, transactionSetId, transactionSetCode, parentId);
            }
            else if (loop is Loop)
            {
                loopId = SaveLoop((Loop)loop, interchangeId, transactionSetId, transactionSetCode, parentId);
            }
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
            return loopId;
        }

        private void MarkInterchangeWithError(int interchangeId)
        {
            var cmd = new SqlCommand(string.Format("update [{0}].Interchange set HasError = 1 where Id = @interchangeId", _schema));
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            ExecuteCmd(cmd);
        }

        private int SaveInterchange(Interchange interchange, string filename, string userName)
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
            SqlCommand cmd = new SqlCommand(string.Format(@"
DECLARE @id int

INSERT INTO [{1}].[Container] VALUES ('{0}','ISA')

SELECT @id = scope_identity()

INSERT INTO [{0}].[Interchange] (Id, SenderId, ReceiverId, ControlNumber, [Date], SegmentTerminator, ElementSeparator, ComponentSeparator, Filename, HasError, CreatedBy, CreatedDate)
VALUES (@id, @senderId, @receiverId, @controlNumber, @date, @segmentTerminator, @elementSeparator, @componentSeparator, @filename, 0, @createdBy, getdate())

SELECT @id

", _schema, _commonDb.Schema));
            cmd.Parameters.AddWithValue("@senderId", interchange.InterchangeSenderId);
            cmd.Parameters.AddWithValue("@receiverId", interchange.InterchangeReceiverId);
            cmd.Parameters.AddWithValue("@controlNumber", interchange.InterchangeControlNumber);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@segmentTerminator", interchange.Delimiters.SegmentTerminator);
            cmd.Parameters.AddWithValue("@elementSeparator", interchange.Delimiters.ElementSeparator);
            cmd.Parameters.AddWithValue("@componentSeparator", interchange.Delimiters.SubElementSeparator);
            cmd.Parameters.AddWithValue("@filename", filename);
            cmd.Parameters.AddWithValue("@createdBy", userName);
            

            return Convert.ToInt32(ExecuteScalar(cmd));
        }

        private int SaveFunctionalGroup(FunctionGroup functionGroup)
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

            SqlCommand cmd = new SqlCommand(string.Format(@"
DECLARE @id int

INSERT INTO [{1}].[Container] VALUES ('{0}','GS')

SELECT @id = scope_identity()

INSERT INTO [{0}].[FunctionalGroup]
VALUES (@id, @functionalIdCode, @date, @controlNumber, @version)

SELECT @id
", _schema, _commonDb.Schema));
            cmd.Parameters.AddWithValue("@functionalIdCode", idCode);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@controlNumber", controlNumber);
            cmd.Parameters.AddWithValue("@version", version);

            return Convert.ToInt32(ExecuteScalar(cmd));
        }
        
        private int SaveTransactionSet(Transaction transaction)
        {
            string controlNumber = transaction.ControlNumber;
            if (controlNumber.Length > 9)
            {
                controlNumber = controlNumber.Substring(0, 9);
                Trace.TraceWarning("Transaction control number '{0}' will be truncated because it exceeds the max length of 9.",transaction.ControlNumber);
            }

            SqlCommand cmd = new SqlCommand(string.Format(@"
DECLARE @id int

INSERT INTO [{1}].[Container] VALUES ('{0}','ST')

SELECT @id = scope_identity()

INSERT INTO [{0}].[TransactionSet] (Id, IdentifierCode, ControlNumber) 
VALUES (@id, @identifierCode, @controlNumber)

SELECT @id
", _schema, _commonDb.Schema));
            cmd.Parameters.AddWithValue("@identifierCode", transaction.IdentifierCode);
            cmd.Parameters.AddWithValue("@controlNumber", controlNumber);

            return Convert.ToInt32(ExecuteScalar(cmd));
        }
        
        private int SaveHierarchicalLoop(HierarchicalLoop loop, int interchangeId, int transactionSetId, string transactionSetCode, int? parentLoopId)
        {
            SqlCommand cmd = new SqlCommand(string.Format(@"
DECLARE @id int

INSERT INTO [{1}].[Container] VALUES ('{0}','HL')

SELECT @id = scope_identity()

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

            return Convert.ToInt32(ExecuteScalar(cmd));            
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
        
        private int SaveLoop(Loop loop, int interchangeId, int transactionSetId, string transactionSetCode, int? parentLoopId)
        {
            string entityIdentifierCode = GetEntityTypeCode(loop);

            SqlCommand cmd = new SqlCommand(string.Format(@"
DECLARE @id int

INSERT INTO [{1}].[Container] VALUES ('{0}',@startingSegment)

SELECT @id = scope_identity()

INSERT INTO [{0}].[Loop] (Id, ParentLoopId, InterchangeId, TransactionSetId, TransactionSetCode, SpecLoopId, StartingSegmentId, EntityIdentifierCode)
VALUES (@id, @parentLoopId, @interchangeId, @transactionSetId, @transactionSetCode, @specLoopId, @startingSegment, @entityIdentifierCode)

SELECT @id", _schema, _commonDb.Schema));
            cmd.Parameters.AddWithValue("@parentLoopId", (object)parentLoopId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            cmd.Parameters.AddWithValue("@transactionSetId", transactionSetId);
            cmd.Parameters.AddWithValue("@transactioNSetCode", transactionSetCode);
            cmd.Parameters.AddWithValue("@specLoopId", loop.Specification.LoopId);
            cmd.Parameters.AddWithValue("@startingSegment", loop.SegmentId);
            cmd.Parameters.AddWithValue("@entityIdentifierCode", entityIdentifierCode != null ? (object)entityIdentifierCode : DBNull.Value);

            try
            {
                return Convert.ToInt32(ExecuteScalar(cmd));
            }
            catch (Exception exc)
            {
                Trace.TraceError(exc.Message);
                throw;
            }
        }

        private bool SegmentHasChanged(DetachedSegment segment, int positionInInterchange, int interchangeId, int previousRevisionId)
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

                    if (Convert.ToInt32(reader["RevisionId"]) != previousRevisionId)
                        throw new InvalidOperationException(string.Format("Segment {0} of interchange {1} in position {2} has already been revised by {3} at {4}.", segment.SegmentId, interchangeId, positionInInterchange, reader["RevisedBy"], reader["RevisionDate"]));

                    

                    return Convert.ToString(reader["Segment"]) != segment.SegmentString;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("A segment does not exist for interchange {0} at position {1}.", interchangeId, positionInInterchange));
                }
            }
        }
        protected virtual void SaveSegment(SqlTransaction tran, DetachedSegment segment, int positionInInterchange, int interchangeId, int? functionalGroupId = null, int? transactionSetId = null, int? parentLoopId = null, int? loopId = null, int revisionId = 0, int previousRevisionId = 0, bool deleted = false)
        {
            if (revisionId == 0 || SegmentHasChanged(segment, positionInInterchange, interchangeId, previousRevisionId) || deleted)
            {
                SqlCommand cmd = new SqlCommand(string.Format(@"
INSERT INTO [{0}].[Segment]
VALUES (@interchangeId, @functionalGroupId, @transactionSetId, @parentLoopId, @loopId, @revisionId, @deleted, @positionInInterchange, @segmentId, @segment)", _schema));
                cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
                cmd.Parameters.AddWithValue("@functionalGroupId", functionalGroupId.HasValue ? (object)functionalGroupId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@transactionSetId", transactionSetId.HasValue ? (object)transactionSetId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@parentLoopId", parentLoopId.HasValue ? (object)parentLoopId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@loopId", loopId.HasValue ? (object)loopId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@revisionId", revisionId);
                cmd.Parameters.AddWithValue("@deleted", deleted);
                cmd.Parameters.AddWithValue("@positionInInterchange", positionInInterchange);
                cmd.Parameters.AddWithValue("@segmentId", segment.SegmentId);
                cmd.Parameters.AddWithValue("@segment", segment.SegmentString);

                if (tran != null)
                {
                    cmd.Connection = tran.Connection;
                    cmd.Transaction = tran;
                }

                ExecuteCmd(cmd);

                if (_indexedSegments.Contains(segment.SegmentId))
                {
                    List<string> fieldNames = new List<string>();
                    List<string> parameterNames = new List<string>();
                    var spec = _specFinder.FindSegmentSpec("5010", segment.SegmentId);
                    int maxElements = spec != null ? spec.Elements.Count : 0;
                    for (int i = 1; i <= segment.ElementCount; i++)
                    {
                        if (i <= maxElements)
                        {
                            fieldNames.Add(string.Format("[{0:00}]", i));
                            parameterNames.Add(string.Format("@e{0:00}", i));
                        }
                        else
                        {
                            string val = segment.GetElement(i);
                            Trace.TraceWarning("Element {2}{3:00} in position {1} of interchange {0} with value {4} will NOT be indexed because it exceeds the {5} specified number of elements.", interchangeId, positionInInterchange, segment.SegmentId, i, val, maxElements);
                        }
                    }

                    StringBuilder sql = new StringBuilder();
                    sql.AppendFormat("INSERT INTO [{0}].[{1}] (InterchangeId, PositionInInterchange, ParentLoopId, LoopId, RevisionId, Deleted, {2}) VALUES (@interchangeId, @positionInInterchange, @parentLoopId, @loopId, @revisionId, @deleted, {3})", _schema, segment.SegmentId, string.Join(",", fieldNames), string.Join(", ", parameterNames));


                    cmd = new SqlCommand(sql.ToString());
                    cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
                    cmd.Parameters.AddWithValue("@positionInInterchange", positionInInterchange);
                    cmd.Parameters.AddWithValue("@parentLoopId", (object)parentLoopId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@loopId", (object)loopId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@revisionId", revisionId);
                    cmd.Parameters.AddWithValue("@deleted", deleted);

                    for (int i = 1; i <= segment.ElementCount && i <= maxElements; i++)
                    {
                        string val = segment.GetElement(i);

                        if (spec != null && spec.Elements.Count >= i)
                        {
                            int maxLength = spec.Elements[i - 1].MaxLength;

                            if (maxLength > 0 && val.Length > maxLength)
                            {
                                Trace.TraceWarning("Element {2}{3:00} in position {1} of interchange {0} will be truncated because {4} exceeds the max length of {5}.", interchangeId, positionInInterchange, segment.SegmentId, i, val, maxLength);
                                val = val.Substring(0, maxLength);
                            }
                        }

                        cmd.Parameters.AddWithValue(string.Format("@e{0:00}", i), val);
                    }
                    if (tran != null)
                    {
                        cmd.Connection = tran.Connection;
                        cmd.Transaction = tran;
                    }

                    ExecuteCmd(cmd);
                }
            }
        }

        private void ExecuteCmd(SqlCommand cmd)
        {
            if (cmd.Transaction == null)
            {
                using (var conn = new SqlConnection(_dsn))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                }
            }
            else
                cmd.ExecuteNonQuery();
        }

        private object ExecuteScalar(SqlCommand cmd)
        {
            if (cmd.Transaction == null)
            {

                using (var conn = new SqlConnection(_dsn))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    return cmd.ExecuteScalar();
                }
            }
            else
            {
                return cmd.ExecuteScalar();
            }
        }
    }
}
