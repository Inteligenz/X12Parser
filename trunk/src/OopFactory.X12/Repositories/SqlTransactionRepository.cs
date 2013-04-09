using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing;
using System.Data.SqlClient;

namespace OopFactory.X12.Repositories
{
    public class SqlTransactionRepository
    {
        private string _dsn;
        private string _schema;
        private ISpecificationFinder _specFinder;
        private string[] _indexedSegments;
        
        public SqlTransactionRepository(string dsn)
            : this(dsn, new SpecificationFinder(), new string[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER", "CLM", "BIN", "BDS" }, "dbo")
        {
        }

        public SqlTransactionRepository(string dsn, string schema)
            : this(dsn, new SpecificationFinder(), new string[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER", "CLM", "BIN", "BDS" }, schema)
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
            if (!EnsureSchema())
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
            }
            foreach (var segmentId in _indexedSegments)
            {
                var spec = _specFinder.FindSegmentSpec("5010", segmentId);
                if (spec != null)
                    EnsureIndexedSegmentTable(spec);
            }
            int positionInInterchange = 1;
            
            int interchangeId = SaveInterchange(interchange, filename, userName);
            SaveSegment(interchange, positionInInterchange, interchangeId);

            foreach (var fg in interchange.FunctionGroups)
            {
                int functionalGroupId = SaveFunctionalGroup(fg);
                SaveSegment(fg, ++positionInInterchange, interchangeId, functionalGroupId);

                foreach (var tran in fg.Transactions)
                {
                    int transactionSetId = SaveTransactionSet(tran);
                    SaveSegment(tran, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);

                    foreach (var seg in tran.Segments)
                    {
                        if (seg is HierarchicalLoopContainer)
                        {
                            positionInInterchange++;
                            SaveLoopAndChildren((HierarchicalLoopContainer)seg, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, null);
                        }
                        else
                            SaveSegment(seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);
                    }
                    foreach (var hl in tran.HLoops)
                    {
                        positionInInterchange++;
                        SaveLoopAndChildren(hl, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, null);
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

        private int SaveLoopAndChildren(HierarchicalLoopContainer loop, ref int positionInInterchange, int interchangeId, int functionalGroupId, int transactionSetId, int? parentId)
        {
            int loopId = 0;
            if (loop is HierarchicalLoop)
            {
                loopId = SaveHierarchicalLoop((HierarchicalLoop)loop);
            }
            else if (loop is Loop)
            {
                loopId = SaveLoop((Loop)loop);
            }
            SaveSegment(loop, positionInInterchange, interchangeId, functionalGroupId, transactionSetId, parentId, loopId);
            
            foreach (var seg in loop.Segments)
            {
                if (seg is HierarchicalLoopContainer)
                {
                    positionInInterchange++;
                    SaveLoopAndChildren((HierarchicalLoopContainer)seg, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, loopId);
                }
                else
                    SaveSegment(seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId, loopId);
            }

            foreach (var hl in loop.HLoops)
            {
                positionInInterchange++;
                SaveLoopAndChildren(hl, ref positionInInterchange, interchangeId, functionalGroupId, transactionSetId, loopId);
            }
            return loopId;
        }
        
        private bool EnsureSchema()
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
            cmd.Parameters.AddWithValue("@date", interchange.InterchangeDate);
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
            SqlCommand cmd = new SqlCommand(string.Format(@"
DECLARE @id int

INSERT INTO [{0}].[Container] VALUES ('GS')

SELECT @id = scope_identity()

INSERT INTO [{0}].[FunctionalGroup]
VALUES (@id, @functionalIdCode, @date, @controlNumber, @version)

SELECT @id
", _schema));
            cmd.Parameters.AddWithValue("@functionalIdCode", functionGroup.FunctionalIdentifierCode);
            cmd.Parameters.AddWithValue("@date", functionGroup.Date);
            cmd.Parameters.AddWithValue("@controlNumber", functionGroup.ControlNumber);
            cmd.Parameters.AddWithValue("@version", functionGroup.VersionIdentifierCode);

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
    [SpecLoopId] [varchar](6) NULL,
    [LevelId] [varchar](12) NULL,
    [LevelCode] [varchar](2) NULL,
    [StartingSegmentId] [varchar](3) NULL,
    [EntityIdentifierCode] [varchar](3) NULL,
  CONSTRAINT [PK_Loop_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )
)
", _schema));
        }

        private int SaveHierarchicalLoop(HierarchicalLoop loop)
        {
            SqlCommand cmd = new SqlCommand(string.Format(@"
DECLARE @id int

INSERT INTO [{0}].[Container] VALUES ('HL')

SELECT @id = scope_identity()

INSERT INTO [{0}].[Loop] (Id, SpecLoopId, LevelId, LevelCode, StartingSegmentId)
VALUES (@id, @loopId, @levelId, @levelCode, 'HL')

SELECT @id", _schema));
            cmd.Parameters.AddWithValue("@loopId", loop.Specification.LoopId);
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
        
        private int SaveLoop(Loop loop)
        {
            string entityIdentifierCode = GetEntityTypeCode(loop);

            SqlCommand cmd = new SqlCommand(string.Format(@"
DECLARE @id int

INSERT INTO [{0}].[Container] VALUES (@startingSegment)

SELECT @id = scope_identity()

INSERT INTO [{0}].[Loop] (Id, SpecLoopId, StartingSegmentId, EntityIdentifierCode)
VALUES (@id, @loopId, @startingSegment, @entityIdentifierCode)

SELECT @id", _schema));
            cmd.Parameters.AddWithValue("@loopId", loop.Specification.LoopId);
            cmd.Parameters.AddWithValue("@startingSegment", loop.SegmentId);
            cmd.Parameters.AddWithValue("@entityIdentifierCode", entityIdentifierCode != null ? (object)entityIdentifierCode : DBNull.Value);

            return Convert.ToInt32(ExecuteScalar(cmd));
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

        private void SaveSegment(Segment segment, int positionInInterchange, int interchangeId, int? functionalGroupId = null, int? transactionSetId = null, int? parentLoopId = null, int? loopId = null)
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
                sql.AppendFormat("INSERT INTO [{0}].[{1}] (InterchangeId, PositionInInterchange, {2}) VALUES (@interchangeId, @positionInInterchange, {3})", _schema, segment.SegmentId, string.Join(",", fieldNames), string.Join(", ",parameterNames));


                cmd = new SqlCommand(sql.ToString());
                cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
                cmd.Parameters.AddWithValue("@positionInInterchange", positionInInterchange);

                for (int i = 1; i <= segment.ElementCount; i++)
                    cmd.Parameters.AddWithValue(string.Format("@e{0:00}", i), segment.GetElement(i));

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
