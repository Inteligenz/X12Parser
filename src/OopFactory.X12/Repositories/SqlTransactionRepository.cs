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
    public class SqlTransactionRepository
    {
        protected readonly string _dsn;
        protected readonly string _schema;
        protected readonly ISpecificationFinder _specFinder;
        protected readonly string[] _indexedSegments;
        
        public SqlTransactionRepository(string dsn)
            : this(dsn, new SpecificationFinder(), new string[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER" }, "dbo")
        {
        }

        public SqlTransactionRepository(string dsn, string schema)
            : this(dsn, new SpecificationFinder(), new string[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER" }, schema)
        {
        }
        
        public SqlTransactionRepository(string dsn, ISpecificationFinder specFinder, string[] indexedSegments, string schema = "dbo")
        {
            _dsn = dsn;
            _schema = schema;
            _specFinder = specFinder;
            _indexedSegments = indexedSegments;
        }

        public int Save(Interchange interchange, string filename, string userName)
        {
            EnsureSchema();
            int positionInInterchange = 1;
            
            int interchangeId = SaveInterchange(interchange, filename, userName);
            SaveSegment(interchange, positionInInterchange, interchangeId);

            foreach (var fg in interchange.FunctionGroups)
            {
                int functionalGroupId = SaveFunctionalGroup(fg);
                SaveSegment(fg, ++positionInInterchange, interchangeId, functionalGroupId);

                foreach (var tran in fg.Transactions)
                {
                    string transactionSetCode = tran.IdentifierCode;
                    int transactionSetId = SaveTransactionSet(tran);
                    SaveSegment(tran, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);

                    foreach (var seg in tran.Segments)
                    {
                        if (seg is HierarchicalLoopContainer)
                        {
                            positionInInterchange++;
                            SaveLoopAndChildren((HierarchicalLoopContainer)seg, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, transactionSetCode, null);
                        }
                        else
                            SaveSegment(seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);
                    }
                    foreach (var hl in tran.HLoops)
                    {
                        positionInInterchange++;
                        SaveLoopAndChildren(hl, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, transactionSetCode, null);
                    }

                    foreach (var seg in tran.TrailerSegments)
                        SaveSegment(seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);
                }

                foreach (var seg in fg.TrailerSegments)
                    SaveSegment(seg, ++positionInInterchange, interchangeId, functionalGroupId);
            }

            foreach (var seg in interchange.TrailerSegments)
                SaveSegment(seg, ++positionInInterchange, interchangeId);
            
            return interchangeId;
        }

        private RepoSegment RepoSegmentFromReader(SqlDataReader reader)
        {
            RepoSegment segment = new RepoSegment
            {
                InterchangeId = Convert.ToInt32(reader["InterchangeId"]),
                PositionInInterchange = Convert.ToInt32(reader["PositionInInterchange"]),
                SpecLoopId = Convert.ToString(reader["SpecLoopId"]),
                SegmentId = Convert.ToString(reader["SegmentId"]),
                Segment = Convert.ToString(reader["Segment"]),
                SegmentTerminator = Convert.ToString(reader["SegmentTerminator"])
            };

            if (!reader.IsDBNull(reader.GetOrdinal("FunctionalGroupId")))
                segment.FunctionalGroupId = Convert.ToInt32(reader["FunctionalGroupId"]);

            if (!reader.IsDBNull(reader.GetOrdinal("TransactionSetId")))
                segment.TransactionSetId = Convert.ToInt32(reader["TransactionSetId"]);

            if (!reader.IsDBNull(reader.GetOrdinal("ParentLoopId")))
                segment.ParentLoopId = Convert.ToInt32(reader["ParentLoopId"]);

            if (!reader.IsDBNull(reader.GetOrdinal("LoopId")))
                segment.LoopId = Convert.ToInt32(reader["LoopId"]);
            return segment;            
        }

        public List<RepoSegment> GetTransactionSetSegments(int transactionSetId, bool includeControlSegments)
        {
            using (var conn = new SqlConnection(_dsn))
            {
                SqlCommand cmd = new SqlCommand(string.Format(@"
select ts.InterchangeId, ts.FunctionalGroupId, ts.TransactionSetId, ts.ParentLoopId, ts.LoopId,
    ts.PositionInInterchange, l.SpecLoopId, ts.SegmentId, ts.Segment, i.SegmentTerminator
from [{0}].GetTransactionSetSegments(@transactionSetId, @includeControlSegments) ts
join [{0}].Interchange i on ts.InterchangeId = i.Id
left join [{0}].Loop l on ts.LoopId = l.Id
order by PositionInInterchange
", _schema), conn);
                cmd.Parameters.AddWithValue("@transactionSetId", transactionSetId);
                cmd.Parameters.AddWithValue("@includeControlSegments", includeControlSegments);

                conn.Open();
                var reader = cmd.ExecuteReader();

                List<RepoSegment> s = new List<RepoSegment>();
                while (reader.Read())
                {
                    s.Add(RepoSegmentFromReader(reader));
                }
                reader.Close();

                return s;
            }
        }

        public List<RepoSegment> GetTransactionSegments(int loopId, bool includeControlSegments)
        {
            using (var conn = new SqlConnection(_dsn))
            {
                SqlCommand cmd = new SqlCommand(string.Format(@"
select ts.InterchangeId, ts.FunctionalGroupId, ts.TransactionSetId, ts.ParentLoopId, ts.LoopId,
    ts.PositionInInterchange, l.SpecLoopId, ts.SegmentId, ts.Segment, i.SegmentTerminator
from [{0}].GetTransactionSegments(@loopId, @includeControlSegments) ts
join [{0}].Interchange i on ts.InterchangeId = i.Id
left join [{0}].Loop l on ts.LoopId = l.Id
order by PositionInInterchange", _schema), conn);
                cmd.Parameters.AddWithValue("@loopId", loopId);
                cmd.Parameters.AddWithValue("@includeControlSegments", includeControlSegments);

                conn.Open();
                var reader = cmd.ExecuteReader();

                List<RepoSegment> s = new List<RepoSegment>();
                while (reader.Read())
                {
                    s.Add(RepoSegmentFromReader(reader));
                }
                reader.Close();

                return s;
            }
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
            SaveSegment(loop, positionInInterchange, interchangeId, functionalGroupId, transactionSetId, parentId, loopId);
            
            foreach (var seg in loop.Segments)
            {
                if (seg is HierarchicalLoopContainer)
                {
                    positionInInterchange++;
                    SaveLoopAndChildren((HierarchicalLoopContainer)seg, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, transactionSetCode, loopId);
                }
                else
                    SaveSegment(seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId, loopId);
            }

            foreach (var hl in loop.HLoops)
            {
                positionInInterchange++;
                SaveLoopAndChildren(hl, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, transactionSetCode, loopId);
            }
            return loopId;
        }

        protected virtual void EnsureSchema()
        {
            if (!SchemaExists())
            {
                CreateContainerTable();
                CreateInterchangeTable();
                CreateFunctionGroupTable();
                CreateTransactionSetTable();
                CreateLoopTable();
                CreateSegmentTable();
                if (!FunctionExists("dbo", "SplitSegment"))
                    CreateSplitSegmentFunction();
                if (!FunctionExists("dbo", "FlatElements"))
                    CreateFlatElementsFunction();
                if (!FunctionExists(_schema, "GetAncestorLoops"))
                    CreateGetAncestorLoopsFunction();
                if (!FunctionExists(_schema, "GetDescendantLoops"))
                    CreateGetDescendantLoopsFunction();
                if (!FunctionExists(_schema, "GetTransactionSetSegments"))
                    CreateGetTransactionSetSegmentsFunction();
                if (!FunctionExists(_schema, "GetTransactionSegments"))
                    CreateGetTransactionSegmentsFunction();
            }
            foreach (var segmentId in _indexedSegments)
            {
                var spec = _specFinder.FindSegmentSpec("5010", segmentId);
                if (spec != null)
                    EnsureIndexedSegmentTable(spec);
            }
        }
        
        private bool SchemaExists()
        {
            var exists = ExecuteScalar(new SqlCommand(string.Format("select case when exists (select 1 from information_schema.tables where table_schema = '{0}' and table_name = 'Interchange') then 1 else 0 end ", _schema)));

            return Convert.ToInt32(exists) != 0;
        }

        private void CreateContainerTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Container](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](3) NOT NULL
 CONSTRAINT [PK_Container_{0}] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)", _schema));
        }

        private void CreateInterchangeTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Interchange](
	[Id] [int] NOT NULL,
	[SenderId] [varchar](15) NULL,
	[ReceiverId] [varchar](15) NULL,
	[ControlNumber] [varchar](50) NULL,
	[Date] [datetime] NULL,
	[SegmentTerminator] [varchar](1) NULL,
	[ElementSeparator] [varchar](1) NULL,
	[ComponentSeparator] [varchar](1) NULL,
    [Filename] [varchar](255) NULL,
    [CreatedBy] [varchar](50) NULL,
    [CreatedDate] datetime NULL,
 CONSTRAINT [PK_Interchange_{0}] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)", _schema));
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

INSERT INTO [{0}].[Container] VALUES ('ISA')

SELECT @id = scope_identity()

INSERT INTO [{0}].[Interchange] 
VALUES (@id, @senderId, @receiverId, @controlNumber, @date, @segmentTerminator, @elementSeparator, @componentSeparator, @filename, @createdBy, getdate())

SELECT @id

", _schema));
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
        
        private void CreateFunctionGroupTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[FunctionalGroup](
	[Id] [int] NOT NULL,
	[FunctionalIdCode] [varchar](2) NULL,
	[Date] [datetime] NULL,
	[ControlNumber] [varchar](9) NULL,
	[Version] [varchar](12) NULL,
    CONSTRAINT [PK_FunctionalGroup_{0}] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)
", _schema));
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

INSERT INTO [{0}].[Container] VALUES ('GS')

SELECT @id = scope_identity()

INSERT INTO [{0}].[FunctionalGroup]
VALUES (@id, @functionalIdCode, @date, @controlNumber, @version)

SELECT @id
", _schema));
            cmd.Parameters.AddWithValue("@functionalIdCode", idCode);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@controlNumber", controlNumber);
            cmd.Parameters.AddWithValue("@version", version);

            return Convert.ToInt32(ExecuteScalar(cmd));
        }

        private void CreateTransactionSetTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[TransactionSet](
	[Id] [int] NOT NULL,
	[IdentifierCode] [varchar](3) NULL,
	[ControlNumber] [varchar](9) NULL,
	[ImplementationConventionRef] [varchar](35) NULL,
 CONSTRAINT [PK_Transaction_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )
)
", _schema));
        }

        private int SaveTransactionSet(Transaction transaction)
        {
            SqlCommand cmd = new SqlCommand(string.Format(@"
DECLARE @id int

INSERT INTO [{0}].[Container] VALUES ('ST')

SELECT @id = scope_identity()

INSERT INTO [{0}].[TransactionSet] (Id, IdentifierCode, ControlNumber) 
VALUES (@id, @identifierCode, @controlNumber)

SELECT @id
", _schema));
            cmd.Parameters.AddWithValue("@identifierCode", transaction.IdentifierCode);
            cmd.Parameters.AddWithValue("@controlNumber", transaction.ControlNumber);

            return Convert.ToInt32(ExecuteScalar(cmd));
        }

        private void CreateLoopTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Loop](
    [Id] [int] NOT NULL,
    [ParentLoopId] [int] NULL,
    [InterchangeId] [int] NOT NULL,
    [TransactionSetId] [int] NOT NULL,
    [TransactionSetCode] [varchar](3) NOT NULL,
    [SpecLoopId] [varchar](7) NULL,
    [LevelId] [varchar](12) NULL,
    [LevelCode] [varchar](2) NULL,
    [StartingSegmentId] [varchar](3) NOT NULL,
    [EntityIdentifierCode] [varchar](3) NULL,
  CONSTRAINT [PK_Loop_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )
)
", _schema));
        }

        private int SaveHierarchicalLoop(HierarchicalLoop loop, int interchangeId, int transactionSetId, string transactionSetCode, int? parentLoopId)
        {
            SqlCommand cmd = new SqlCommand(string.Format(@"
DECLARE @id int

INSERT INTO [{0}].[Container] VALUES ('HL')

SELECT @id = scope_identity()

INSERT INTO [{0}].[Loop] (Id, ParentLoopId, InterchangeId, TransactionSetId, TransactionSetCode, SpecLoopId, LevelId, LevelCode, StartingSegmentId)
VALUES (@id, @parentLoopId, @interchangeId, @transactionSetId, @transactionSetCode, @specLoopId, @levelId, @levelCode, 'HL')

SELECT @id", _schema));
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

INSERT INTO [{0}].[Container] VALUES (@startingSegment)

SELECT @id = scope_identity()

INSERT INTO [{0}].[Loop] (Id, ParentLoopId, InterchangeId, TransactionSetId, TransactionSetCode, SpecLoopId, StartingSegmentId, EntityIdentifierCode)
VALUES (@id, @parentLoopId, @interchangeId, @transactionSetId, @transactionSetCode, @specLoopId, @startingSegment, @entityIdentifierCode)

SELECT @id", _schema));
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

        private void CreateSegmentTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Segment](
	[InterchangeId] [int] NOT NULL,
	[FunctionalGroupId] [int] NULL,
	[TransactionSetId] [int] NULL,
    [ParentLoopId] [int] NULL,
    [LoopId] [int] NULL,
	[PositionInInterchange] [int] NOT NULL,
	[SegmentId] [varchar](3) NULL,
	[Segment] [nvarchar](max) NULL,
 CONSTRAINT [PK_Segment_{0}] PRIMARY KEY CLUSTERED 
(
	[InterchangeId] ASC,
	[PositionInInterchange] ASC
)
)

CREATE NONCLUSTERED INDEX [IX_Segment_{0}] ON [{0}].[Segment] 
(
	[InterchangeId] ASC,
	[PositionInInterchange] ASC,
	[ParentLoopId] ASC,
	[LoopId] ASC,
	[SegmentId] ASC
)
", _schema));
        }

        private void EnsureIndexedSegmentTable(Parsing.Specification.SegmentSpecification spec)
        {
            if (!TableExists(_schema, spec.SegmentId))
            {
                var sql = new StringBuilder();

                sql.AppendFormat(@"
CREATE TABLE [{0}].[{1}](
	[InterchangeId] [int] NOT NULL,
	[PositionInInterchange] [int] NOT NULL,
    [ParentLoopId] [int] NULL,
    [LoopId] [int] NULL,
", _schema, spec.SegmentId);

                foreach (var element in spec.Elements)
                    if (element.MaxLength > 0 && element.MaxLength < 4000)
                        sql.AppendFormat("	[{0}] [nvarchar]({1}) NULL,\n", element.Reference, element.MaxLength);
                    else
                        sql.AppendFormat("	[{0}] [nvarchar](max) NULL,\n", element.Reference);


                sql.AppendFormat(@"
    CONSTRAINT [PK_{1}_{0}] PRIMARY KEY CLUSTERED ([InterchangeId] ASC, [PositionInInterchange] ASC)
) 
", _schema, spec.SegmentId);
                ExecuteCmd(sql.ToString());
            }
        }

        protected virtual void SaveSegment(Segment segment, int positionInInterchange, int interchangeId, int? functionalGroupId = null, int? transactionSetId = null, int? parentLoopId = null, int? loopId = null)
        {
            SqlCommand cmd = new SqlCommand(string.Format(@"
INSERT INTO [{0}].[Segment]
VALUES (@interchangeId, @functionalGroupId, @transactionSetId, @parentLoopId, @loopId, @positionInInterchange, @segmentId, @segment)", _schema));
            cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
            cmd.Parameters.AddWithValue("@functionalGroupId", functionalGroupId.HasValue ? (object)functionalGroupId.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@transactionSetId", transactionSetId.HasValue ? (object)transactionSetId.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@parentLoopId", parentLoopId.HasValue ? (object)parentLoopId.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@loopId", loopId.HasValue ? (object)loopId.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@positionInInterchange", positionInInterchange);
            cmd.Parameters.AddWithValue("@segmentId", segment.SegmentId);
            cmd.Parameters.AddWithValue("@segment", segment.SegmentString);

            ExecuteCmd(cmd);

            if (_indexedSegments.Contains(segment.SegmentId))
            {
                List<string> fieldNames = new List<string>();                
                List<string> parameterNames = new List<string>();
                for (int i = 1; i <= segment.ElementCount; i++)
                {
                    fieldNames.Add(string.Format("[{0:00}]", i));
                    parameterNames.Add(string.Format("@e{0:00}", i));
                }

                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("INSERT INTO [{0}].[{1}] (InterchangeId, PositionInInterchange, ParentLoopId, LoopId, {2}) VALUES (@interchangeId, @positionInInterchange, @parentLoopId, @loopId, {3})", _schema, segment.SegmentId, string.Join(",", fieldNames), string.Join(", ",parameterNames));


                cmd = new SqlCommand(sql.ToString());
                cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
                cmd.Parameters.AddWithValue("@positionInInterchange", positionInInterchange);
                cmd.Parameters.AddWithValue("@parentLoopId", (object)parentLoopId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@loopId", (object)loopId ?? DBNull.Value);

                var spec = _specFinder.FindSegmentSpec("5010", segment.SegmentId);

                for (int i = 1; i <= segment.ElementCount; i++)
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

                ExecuteCmd(cmd);
            }
        }

        private void CreateSplitSegmentFunction()
        {
            ExecuteCmd(@"
CREATE FUNCTION [dbo].[SplitSegment]
(
	@delimiter varchar(1),
	@segment nvarchar(max)
)
RETURNS 
@elements TABLE (Ref tinyint, Element varchar(max))
AS
BEGIN
    declare @reference int
    declare @frontIndex int
    declare @backIndex int

    set @reference = 1
    set @frontIndex = charindex(@delimiter, @segment, 1)
    set @backIndex = charindex(@delimiter, @segment, @frontIndex + 1)

    while (@backIndex > @frontIndex)
    begin
        insert into @elements values (@reference, substring(@segment, @frontIndex + 1, @backIndex - @frontIndex - 1))

        set @frontIndex = @backIndex
        set @backIndex = charindex(@delimiter, @segment, @frontIndex + 1)
        set @reference = @reference + 1
    end
    
    insert into @elements values (@reference, substring (@segment, @frontIndex + 1,len(@segment)-@frontIndex))

	RETURN 
END");
        }

        private void CreateFlatElementsFunction()
        {
            ExecuteCmd(new SqlCommand(@"
CREATE FUNCTION dbo.FlatElements
(	
	@delimiter varchar(1),
	@segment nvarchar(max)
)
RETURNS TABLE 
AS
RETURN 
(
	with elements as (
select Ref, Element 
from dbo.SplitSegment(@delimiter,@segment)
)
select 
  [01] = (select Element from elements where Ref = 1),
  [02] = (select Element from elements where Ref = 2),
  [03] = (select Element from elements where Ref = 3),
  [04] = (select Element from elements where Ref = 4),
  [05] = (select Element from elements where Ref = 5),
  [06] = (select Element from elements where Ref = 6),
  [07] = (select Element from elements where Ref = 7),
  [08] = (select Element from elements where Ref = 8),
  [09] = (select Element from elements where Ref = 9),
  [10] = (select Element from elements where Ref = 10),
  [11] = (select Element from elements where Ref = 11),
  [12] = (select Element from elements where Ref = 12),
  [13] = (select Element from elements where Ref = 13),
  [14] = (select Element from elements where Ref = 14),
  [15] = (select Element from elements where Ref = 15),
  [16] = (select Element from elements where Ref = 16),
  [17] = (select Element from elements where Ref = 17),
  [18] = (select Element from elements where Ref = 18),
  [19] = (select Element from elements where Ref = 19),
  [20] = (select Element from elements where Ref = 20),
  [21] = (select Element from elements where Ref = 21),
  [22] = (select Element from elements where Ref = 22),
  [23] = (select Element from elements where Ref = 23),
  [24] = (select Element from elements where Ref = 24),
  [25] = (select Element from elements where Ref = 25),
  [26] = (select Element from elements where Ref = 26),
  [27] = (select Element from elements where Ref = 27),
  [28] = (select Element from elements where Ref = 28),
  [29] = (select Element from elements where Ref = 29),
  [30] = (select Element from elements where Ref = 30),
  [31] = (select Element from elements where Ref = 31),
  [32] = (select Element from elements where Ref = 32),
  [33] = (select Element from elements where Ref = 33),
  [34] = (select Element from elements where Ref = 34)
)"));
        }

        private void CreateGetAncestorLoopsFunction()
        {
            ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].[GetAncestorLoops]
(	
	@loopId int,
	@includeSelf bit
)
RETURNS TABLE 
AS
RETURN 
(
    with parents as (
      select @loopId as [LoopId], l.*, 0 as [Level]
      from [{0}].[Loop] l
      where l.Id = @loopId

      union all

      select p.[LoopId], l.*, p.Level + 1 as [Level]
      from parents p
      join [{0}].[Loop] l on p.ParentLoopId = l.Id
    )
    select Id, ParentLoopId, InterchangeId, TransactionSetId, SpecLoopId, LevelId, LevelCode, StartingSegmentId, EntityIdentifierCode, [Level]
    from parents
    where @includeSelf = 1 or Level > 0
)", _schema));
        }

        private void CreateGetDescendantLoopsFunction()
        {
            ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].GetDescendantLoops
(	
	@loopId int,
	@includeSelf bit
)
RETURNS TABLE 
AS
RETURN 
(
  with children as (
    select @loopId as LoopId, l.*, -1 as Level
    from [{0}].Loop l
    where l.ParentLoopId = @loopId
  
    union all
  
    select c.LoopId, l.*, c.Level - 1 as Level
    from children c
    join [{0}].Loop l on c.Id = l.ParentLoopId
  )
  select Id, ParentLoopId, InterchangeId, TransactionSetId, SpecLoopId, LevelId, LevelCode, StartingSegmentId, EntityIdentifierCode, 0 as Level
  from [{0}].Loop 
  where Id = @loopId
  and @includeSelf = 1
  
  union
  
  select Id, ParentLoopId, InterchangeId, TransactionSetId, SpecLoopId, LevelId, LevelCode, StartingSegmentId, EntityIdentifierCode, Level
  from children
)", _schema));
        }

        private void CreateGetTransactionSetSegmentsFunction()
        {
            ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].GetTransactionSetSegments
(	
	@transactionSetId int, @includeControlSegments bit
)
RETURNS TABLE 
AS
RETURN 
(
    select *
    from [{0}].Segment
    where TransactionSetId = @TransactionSetId

    union

    select *
    from [{0}].Segment
    where FunctionalGroupId = (select top 1 FunctionalGroupId 
                                from [{0}].Segment 
                                where TransactionSetId = @transactionSetId)
    and segmentId in ('GS','GE')
    and @includeControlSegments = 1

    union

    select *
    from [{0}].Segment
    where InterchangeId = (select top 1 InterchangeId 
                                from [{0}].Segment 
                                where TransactionSetId = @transactionSetId)
    and segmentId in ('ISA','IEA')
    and @includeControlSegments = 1
)", _schema));
        }

        private void CreateGetTransactionSegmentsFunction()
        {
            ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].[GetTransactionSegments]
(	
	@loopId int, @includeControlSegments bit
)
RETURNS TABLE 
AS
RETURN 
(
  with transactionLoops as ( 
    select * from [{0}].GetAncestorLoops(@loopId, 1)
    union all
    select * from [{0}].GetDescendantLoops(@loopId, 0)
  )
  , ancestorsOtherChildLoops as (
    select distinct l.*
    from transactionLoops tl
    join [{0}].Loop l on l.ParentLoopId = tl.Id
    where tl.[Level] > 1 or (tl.Level = 1 and l.SpecLoopId <> (select SpecLoopId from [{0}].[Loop] where Id = @loopId))

    union all

    select l.*
    from ancestorsOtherChildLoops poc
    join [{0}].Loop l on poc.Id = l.ParentLoopId
    where l.SpecLoopId <> (select SpecLoopId from [{0}].[Loop] where Id = @loopId)
)
, transactionChildLoops as (  
    
    select distinct l.*
    from [{0}].Loop l
    where ParentLoopId is null
    and TransactionSetId = (select top 1 TransactionSetID from transactionLoops)
    and l.SpecLoopId <> (select SpecLoopId from [{0}].[Loop] where Id = @loopId)
  )
  , transactionSegments as (
    select *
    from [{0}].Segment
    where LoopId in (select Id from transactionLoops)
    or (LoopId is null and ParentLoopId in (select Id from transactionLoops))
    or LoopId in (select Id from ancestorsOtherChildLoops)
    or (LoopId is null and ParentLoopId in (select Id from ancestorsOtherChildLoops))
    or LoopId in (select Id from transactionChildLoops)
    or (LoopId is null and ParentLoopId in (select Id from transactionChildLoops))    
    or (TransactionSetId = (select top 1 TransactionSetId from transactionLoops) 
      and (SegmentId in ('ST','SE') or ParentLoopId is null and LoopId is null))
  )
    select * 
    from transactionSegments

    union all

    select *
    from [{0}].Segment
    where FunctionalGroupId = (select top 1 FunctionalGroupId from transactionSegments)
    and SegmentId in ('GS','GE') and @includeControlSegments = 1

    union all

    select *
    from [{0}].Segment
    where InterchangeId = (select top 1 InterchangeId from transactionSegments)
    and SegmentId in ('ISA','IEA') and @includeControlSegments = 1
)", _schema));
        }

        private void ExecuteCmd(string sql)
        {
            ExecuteCmd(new SqlCommand(sql));
        }

        private void ExecuteCmd(SqlCommand cmd)
        {
            using (var conn = new SqlConnection(_dsn))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
        }

        private object ExecuteScalar(SqlCommand cmd)
        {
            using (var conn = new SqlConnection(_dsn))
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteScalar();
            }
        }

        private bool FunctionExists(string schema, string functionName)
        {
            var result = ExecuteScalar(new SqlCommand(string.Format(@"select case when exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[{1}]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) then 1 else 0 end", schema, functionName)));

            return Convert.ToInt32(result) != 0;
        }

        private bool TableExists(string schema, string tableName)
        {
            var result = ExecuteScalar(new SqlCommand(string.Format(@"select case when EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[{1}]') AND type in (N'U')) then 1 else 0 end", schema, tableName)));

            return Convert.ToInt32(result) != 0;
        }

    }
}
