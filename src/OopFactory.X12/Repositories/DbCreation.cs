using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Repositories
{
    public class DbCreation<T> where T : struct
    {
        private string _dsn;
        private string _schema;
        private SqlDbType _identitySqlType;

        public DbCreation(string dsn, string schema)
        {
            _dsn = dsn;
            _schema = schema;
            if (typeof(T) == typeof(long))
                _identitySqlType = SqlDbType.BigInt;
            else
                _identitySqlType = SqlDbType.Int;            
        }

        public string Schema { get { return _schema; } }

        public void CreateContainerTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Container](
	[Id] [{1}] IDENTITY(1,1) NOT NULL,
    [SchemaName] [varchar](25) NOT NULL,
	[Type] [varchar](3) NOT NULL
    CONSTRAINT [PK_Container_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )
)", _schema, _identitySqlType));
        }

        public void CreateRevisionTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [dbo].[Revision](
	[Id] [{1}] IDENTITY(0,1) NOT NULL,
    [SchemaName] [varchar](25) NOT NULL,
	[Comments] [varchar](max) NOT NULL,
    [RevisionDate] datetime NOT NULL,
    [RevisedBy] varchar(50) NULL
    CONSTRAINT [PK_Revision_dbo] PRIMARY KEY CLUSTERED ( [Id] ASC )
)

INSERT INTO [dbo].[Revision]
VALUES ('dbo','Initial Load',getdate(),'system')
", _schema, _identitySqlType));
        }
        
        public void CreateInterchangeTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Interchange](
	[Id] [{1}] NOT NULL,
	[SenderId] [varchar](15) NULL,
	[ReceiverId] [varchar](15) NULL,
	[ControlNumber] [varchar](50) NULL,
	[Date] [datetime] NULL,
	[SegmentTerminator] [char](1) NULL,
	[ElementSeparator] [char](1) NULL,
	[ComponentSeparator] [char](1) NULL,
    [Filename] [varchar](255) NULL,
    [HasError] bit NULL,
    [CreatedBy] [varchar](50) NULL,
    [CreatedDate] datetime NULL,
 CONSTRAINT [PK_Interchange_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )
)", _schema, _identitySqlType));
        }

        public void CreateFunctionalGroupTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[FunctionalGroup](
	[Id] [{1}] NOT NULL,
	[FunctionalIdCode] [varchar](2) NULL,
	[Date] [datetime] NULL,
	[ControlNumber] [varchar](9) NULL,
	[Version] [varchar](12) NULL,
    CONSTRAINT [PK_FunctionalGroup_{0}] PRIMARY KEY CLUSTERED (	[Id] ASC )
)", _schema, _identitySqlType));
        }

        public void CreateTransactionSetTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[TransactionSet](
	[Id] [{1}] NOT NULL,
	[IdentifierCode] [varchar](3) NULL,
	[ControlNumber] [varchar](9) NULL,
	[ImplementationConventionRef] [varchar](35) NULL,
 CONSTRAINT [PK_Transaction_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )
)", _schema, _identitySqlType));
        }

        public void CreateLoopTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Loop](
    [Id] [{1}] NOT NULL,
    [ParentLoopId] [{1}] NULL,
    [InterchangeId] [{1}] NOT NULL,
    [TransactionSetId] [{1}] NOT NULL,
    [TransactionSetCode] [varchar](3) NOT NULL,
    [SpecLoopId] [varchar](7) NULL,
    [LevelId] [varchar](12) NULL,
    [LevelCode] [varchar](2) NULL,
    [StartingSegmentId] [varchar](3) NOT NULL,
    [EntityIdentifierCode] [varchar](3) NULL,
  CONSTRAINT [PK_Loop_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )
)", _schema, _identitySqlType));
        }

        public void CreateSegmentTable()
        {
            ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Segment](
	[InterchangeId] [{1}] NOT NULL,
	[FunctionalGroupId] [{1}] NULL,
	[TransactionSetId] [{1}] NULL,
    [ParentLoopId] [{1}] NULL,
    [LoopId] [{1}] NULL,
    [RevisionId] [{1}] NOT NULL,
    [Deleted] [bit] NOT NULL,
	[PositionInInterchange] [int] NOT NULL,
	[SegmentId] [varchar](3) NULL,
	[Segment] [nvarchar](max) NULL,
 CONSTRAINT [PK_Segment_{0}] PRIMARY KEY CLUSTERED 
(
	[InterchangeId] ASC,
	[PositionInInterchange] ASC,
    [RevisionId] ASC
)
)

CREATE NONCLUSTERED INDEX [IX_Segment_{0}] ON [{0}].[Segment] 
(
	[InterchangeId] ASC,
	[PositionInInterchange] ASC,
	[ParentLoopId] ASC,
	[LoopId] ASC,
    [RevisionId] ASC,
	[SegmentId] ASC
)
", _schema, _identitySqlType));
        }

        public void CreateIndexedSegmentTable(SegmentSpecification spec)
        {
                var sql = new StringBuilder();

                sql.AppendFormat(@"
CREATE TABLE [{0}].[{1}](
	[InterchangeId] [{2}] NOT NULL,
	[PositionInInterchange] [int] NOT NULL,
    [ParentLoopId] [{2}] NULL,
    [LoopId] [{2}] NULL,
    [RevisionId] [{2}] NOT NULL,
    [Deleted] [bit] NOT NULL,
", _schema, spec.SegmentId, _identitySqlType);

                foreach (var element in spec.Elements)
                    if (element.MaxLength > 0 && element.MaxLength < 4000)
                    {
                        switch (element.Type)
                        {
                            case ElementDataTypeEnum.Decimal:
                                int scale = element.MaxLength * 2;
                                int precision = element.MaxLength > 8 ? element.MaxLength / 2 : 4;
                                sql.AppendFormat("  [{0}] [decimal]({1},{2}) NULL,\n", element.Reference, scale, precision);
                                break;
                            case ElementDataTypeEnum.Numeric:
                                if (element.ImpliedDecimalPlaces == 0)
                                {
                                    if (element.MaxLength < 5)
                                        sql.AppendFormat("  [{0}] [smallint] NULL,\n", element.Reference);
                                    else if (element.MaxLength <= 10)
                                        sql.AppendFormat("  [{0}] [int] NULL,\n", element.Reference);
                                    else 
                                        sql.AppendFormat("  [{0}] [bigint] NULL,\n", element.Reference);                                        
                                }
                                else
                                {
                                    scale = element.MaxLength - element.ImpliedDecimalPlaces + 2;
                                    precision = element.ImpliedDecimalPlaces;
                                    sql.AppendFormat("  [{0}] [decimal]({1},{2}) NULL,\n", element.Reference, scale, precision);
                                }
                                break;
                            case ElementDataTypeEnum.Date:
                                sql.AppendFormat("  [{0}] [date] NULL,\n", element.Reference);
                                break;
                            default:
                                sql.AppendFormat("	[{0}] [nvarchar]({1}) NULL,\n", element.Reference, element.MaxLength);
                                break;
                        }
                    }
                    else
                        sql.AppendFormat("	[{0}] [nvarchar](max) NULL,\n", element.Reference);


                sql.AppendFormat(@"
    CONSTRAINT [PK_{1}_{0}] PRIMARY KEY CLUSTERED ([InterchangeId] ASC, [PositionInInterchange] ASC, [RevisionId] ASC)
) 

CREATE NONCLUSTERED INDEX [IX_{1}_{0}] ON [{0}].[{1}] 
(
	[InterchangeId] ASC,
	[PositionInInterchange] ASC,
    [RevisionId] ASC,
    [Deleted] ASC,
	[ParentLoopId] ASC,
	[LoopId] ASC
)", _schema, spec.SegmentId);
                ExecuteCmd(sql.ToString());

                ExecuteCmd(string.Format(@"
CREATE VIEW [{0}].[LastRev{1}]
AS
select *
from [{0}].[{1}] a
where RevisionId = (select max(RevisionId) 
                    from [{0}].[{1}] b 
                    where a.InterchangeId = b.InterchangeId 
                      and a.PositionInInterchange = b.PositionInInterchange)", _schema, spec.SegmentId));
            
        }

        public void CreateSplitSegmentFunction()
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

        public void CreateFlatElementsFunction()
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

        public void CreateGetAncestorLoopsFunction()
        {
            ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].[GetAncestorLoops]
(	
	@loopId {1},
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
)", _schema, _identitySqlType));
        }

        public void CreateGetDescendantLoopsFunction()
        {
            ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].GetDescendantLoops
(	
	@loopId {1},
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
)", _schema, _identitySqlType));
        }

        public void CreateGetTransactionSetSegmentsFunction()
        {
            ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].GetTransactionSetSegments
(	
	@transactionSetId {1}, @includeControlSegments bit, @revisionId {1}
)
RETURNS TABLE 
AS
RETURN 
(
  with allSegments as (
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
  )
  , revisedSegments as (
  select *, RowNum = ROW_NUMBER() OVER (PARTITION BY InterchangeId, PositionInInterchange ORDER BY RevisionId desc)
  from allSegments
  where RevisionId <= @revisionId
  )
  select *
  from revisedSegments
  where RowNum = 1 and Deleted = 0
)", _schema, _identitySqlType));
        }

        public void CreateGetTransactionSegmentsFunction()
        {
            ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].[GetTransactionSegments]
(	
	@loopId {1}, @includeControlSegments bit, @revisionId {1}
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
  , allSegments as (
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
  )
  , revisedSegments as (
  select *, RowNum = ROW_NUMBER() OVER (PARTITION BY InterchangeId, PositionInInterchange ORDER BY RevisionId desc)
  from allSegments
  where RevisionId <= @revisionId
  )
  select *
  from revisedSegments
  where RowNum = 1 and Deleted = 0
)", _schema, _identitySqlType));
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

        public bool FunctionExists(string functionName)
        {
            var result = ExecuteScalar(new SqlCommand(string.Format(@"select case when exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[{1}]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) then 1 else 0 end", _schema, functionName)));

            return Convert.ToInt32(result) != 0;
        }

        public bool TableExists(string tableName)
        {
            var result = ExecuteScalar(new SqlCommand(string.Format(@"select case when EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[{1}]') AND type in (N'U')) then 1 else 0 end", _schema, tableName)));

            return Convert.ToInt32(result) != 0;
        }
    }
}
