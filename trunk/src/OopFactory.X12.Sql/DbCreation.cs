namespace OopFactory.X12.Sql
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using System.Text;
	using Parsing.Specification;

	public interface IDbCreation
	{
		string Schema { get; }
		void CreateContainerTable();
		void CreateRevisionTable();
		void CreateX12CodeListTable();
		int ElementCountInX12CodeListTable(string elementId);
		void AddToX12CodeListTable(string elementId, string code, string definition);
		void CreateInterchangeTable();
		void CreateFunctionalGroupTable();
		void CreateTransactionSetTable();
		void CreateLoopTable();
		void CreateSegmentTable();
		void CreateParsingErrorTable();
		void CreateEntityView(string commonSchema);
		void CreateIndexedSegmentTable(SegmentSpecification spec, string commonSchema);
		void AddErrorIdToIndexedSegmentTable(string segmentId);
		void CreateSplitSegmentFunction();
		void CreateFlatElementsFunction();
		void CreateGetAncestorLoopsFunction();
		void CreateGetDescendantLoopsFunction();
		void CreateGetTransactionSetSegmentsFunction();
		void CreateGetTransactionSegmentsFunction();
		void CreateSchema();
		bool FunctionExists(string functionName);
		bool SchemaExists();
		bool TableExists(string tableName);
		bool ViewExists(string viewName);
		bool TableColumnExists(string tableName, string columnName);
		void ExecuteCmd(string sql);
		void ExecuteCmd(SqlCommand cmd);
		object ExecuteScalar(SqlCommand cmd);
		void RemoveIdentityColumn(string table);
		bool HasIdentityColumn(string table);
	}

	public class DbCreation : IDbCreation
	{
		private readonly string _dsn;
		private readonly SqlDbType _identitySqlType;
		private readonly string _dateType;

		public DbCreation(string dsn, string schema, Type identityType, string dateType = "date")
		{
			_dsn = dsn;
			Schema = schema;
			_dateType = dateType;
			if (identityType == typeof (Guid))
				_identitySqlType = SqlDbType.UniqueIdentifier;
			else if (identityType == typeof (long))
				_identitySqlType = SqlDbType.BigInt;
			else
				_identitySqlType = SqlDbType.Int;
		}

		public string Schema { get; private set; }

		public void CreateContainerTable()
		{
			ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Container](
	[Id] [{1}] NOT NULL,
    [SchemaName] [varchar](25) NOT NULL,
	[Type] [varchar](3) NOT NULL
    CONSTRAINT [PK_Container_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )
)", Schema, _identitySqlType));
		}

		public void CreateRevisionTable()
		{
			ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Revision](
	[Id] [int] IDENTITY(0,1) NOT NULL,
    [SchemaName] [varchar](25) NOT NULL,
	[Comments] [varchar](max) NOT NULL,
    [RevisionDate] datetime NOT NULL,
    [RevisedBy] varchar(50) NULL
    CONSTRAINT [PK_Revision_dbo] PRIMARY KEY CLUSTERED ( [Id] ASC )
)

INSERT INTO [{0}].[Revision] (SchemaName,Comments,RevisionDate,RevisedBy)
VALUES ('dbo','Initial Load',getdate(),'system')
", Schema));
		}

		public void CreateX12CodeListTable()
		{
			ExecuteCmd(string.Format(@"CREATE TABLE [{0}].[X12CodeList](
	[ElementId] [varchar](4) NOT NULL,
	[Code] [varchar](6) NOT NULL,
	[Definition] [varchar](500) NULL,
 CONSTRAINT [PK_X12CodeList] PRIMARY KEY CLUSTERED 
(
	[ElementId] ASC,
	[Code] ASC
)
)
", Schema));
		}

		public int ElementCountInX12CodeListTable(string elementId)
		{
			var cmd =
				new SqlCommand(string.Format(@"select count(*) from [{0}].X12CodeList where ElementId = @elementId", Schema));
			cmd.Parameters.AddWithValue("@elementId", elementId);

			return Convert.ToInt32(ExecuteScalar(cmd));
		}

		public void AddToX12CodeListTable(string elementId, string code, string definition)
		{
			var cmd =
				new SqlCommand(
					string.Format(
						@"insert into [{0}].X12CodeList (ElementId, Code, Definition) VALUES (@elementId, @code, @definition)",
						Schema));
			cmd.Parameters.AddWithValue("@elementId", elementId);
			cmd.Parameters.AddWithValue("@code", code);
			cmd.Parameters.AddWithValue("@definition", definition);

			ExecuteCmd(cmd);
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
)", Schema, _identitySqlType));
		}

		public void CreateFunctionalGroupTable()
		{
			ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[FunctionalGroup](
	[Id] [{1}] NOT NULL,
    [InterchangeId] [{1}] NOT NULL,
	[FunctionalIdCode] [varchar](2) NULL,
	[Date] [datetime] NULL,
	[ControlNumber] [varchar](9) NULL,
	[Version] [varchar](12) NULL,
    CONSTRAINT [PK_FunctionalGroup_{0}] PRIMARY KEY CLUSTERED (	[Id] ASC )
)", Schema, _identitySqlType));
		}

		public void CreateTransactionSetTable()
		{
			ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[TransactionSet](
	[Id] [{1}] NOT NULL,
    [InterchangeId] [{1}] NOT NULL,
    [FunctionalGroupId] [{1}] NOT NULL,
	[IdentifierCode] [varchar](3) NULL,
	[ControlNumber] [varchar](9) NULL,
	[ImplementationConventionRef] [varchar](35) NULL,
 CONSTRAINT [PK_Transaction_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )
)", Schema, _identitySqlType));
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
)", Schema, _identitySqlType));
		}

		public void CreateSegmentTable()
		{
			ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[Segment](
	[InterchangeId] [{1}] NOT NULL,
	[PositionInInterchange] [int] NOT NULL,
    [RevisionId] [int] NOT NULL,
	[FunctionalGroupId] [{1}] NULL,
	[TransactionSetId] [{1}] NULL,
    [ParentLoopId] [{1}] NULL,
    [LoopId] [{1}] NULL,
    [Deleted] [bit] NOT NULL,
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
    [RevisionId] ASC,
	[ParentLoopId] ASC,
	[LoopId] ASC,
	[SegmentId] ASC
)
", Schema, _identitySqlType));
		}

		public void CreateParsingErrorTable()
		{
			ExecuteCmd(string.Format(@"
CREATE TABLE [{0}].[ParsingError](
	[Id] [{1}] NOT NULL,
    [InterchangeId] [{1}] NOT NULL,
    [PositionInInterchange] [int] NOT NULL,
    [RevisionId] [int] NOT NULL,
    [Message] [varchar](max) NOT NULL,
CONSTRAINT [PK_ParsingError_{0}] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)
)", Schema, _identitySqlType));
		}

		public void CreateEntityView(string commonSchema)
		{
			ExecuteCmd(string.Format(@"
CREATE VIEW [{0}].[Entity]
  AS
select  l.Id as EntityId, l.EntityIdentifierCode, eic.Definition as EntityIdentifier
, l.InterchangeId, l.TransactionSetId, l.TransactionSetCode, l.ParentLoopId, l.SpecLoopId, l.StartingSegmentId
, Name = isnull(n1.[02], case nm1.[02] when '2' then nm1.[03] when '1' then nm1.[03] + ', ' + nm1.[04] + isnull(' ' + nm1.[05],'') end)
, IsPerson = case nm1.[02] when '1' then 1 else 0 end
, LastName = nm1.[03]
, FirstName = nm1.[04]
, MiddleName = nm1.[05]
, NamePrefix = nm1.[06]
, NameSuffix = nm1.[07]
, IdQualifier = isnull(n1.[03],nm1.[08])
, Identification = isnull(n1.[04],nm1.[09])
, Ssn = case when n1.[03] = '34' then n1.[04]
             when nm1.[08] = '34' then nm1.[09] 
             else (select top 1 [02] from [{0}].REF where l.Id = ref.ParentLoopId and [01] = 'SY') end
, Npi = case when n1.[03] = 'XX' then n1.[04]
             when nm1.[08] = 'XX' then nm1.[09]
             else (select top 1 [02] from [{0}].REF where l.Id = ref.ParentLoopId and [01] = 'HPI') end
, TelephoneNumber = coalesce((select top 1 [04] from [{0}].PER where per.ParentLoopId = l.Id and per.[03]='TE')
                    ,(select top 1 [06] from [{0}].PER where per.ParentLoopId = l.Id and per.[05]='TE')
                    ,(select top 1 [08] from [{0}].PER where per.ParentLoopId = l.Id and per.[07]='TE'))
, AddressLine1 = n3.[01]
, AddressLine2 = n3.[02]
, City = n4.[01]
, StateCode = n4.[02]
, PostalCode = n4.[03]
, County = case n4.[05] when 'CY' then n4.[06] else null end
, CountryCode = n4.[07]
, DateOfBirth = dmg.[02]
, Gender = dmg.[03]
from [{0}].[Loop] l
left join [{1}].X12CodeList eic on l.EntityIdentifierCode = eic.Code and eic.ElementId = '98'
left join [{0}].[N1] on l.Id = n1.LoopId
left join [{0}].[NM1] on l.Id = nm1.LoopId
left join [{0}].N3 on l.Id = n3.ParentLoopId
left join [{0}].N4 on l.Id = n4.ParentLoopId
left join [{0}].[DMG] on l.Id = dmg.ParentLoopId
where l.StartingSegmentId in ('N1','NM1','ENT','NX1','PT','IN1','NX1') ", Schema, commonSchema));
		}

		public void CreateIndexedSegmentTable(SegmentSpecification spec, string commonSchema)
		{
			var sql = new StringBuilder();

			sql.AppendFormat(@"
CREATE TABLE [{0}].[{1}](
	[InterchangeId] [{2}] NOT NULL,
	[PositionInInterchange] [int] NOT NULL,
    [RevisionId] [int] NOT NULL,
    [TransactionSetId] [{2}] NULL,
    [ParentLoopId] [{2}] NULL,
    [LoopId] [{2}] NULL,
    [Deleted] [bit] NOT NULL,
    [ErrorId] [{2}] NULL,
", Schema, spec.SegmentId, _identitySqlType);

			foreach (var element in spec.Elements)
				if (element.MaxLength > 0 && element.MaxLength < 4000)
				{
					switch (element.Type)
					{
						case ElementDataTypeEnum.Decimal:
							var precision = element.MaxLength > 18 ? 38 : element.MaxLength*2;
							var scale = element.MaxLength > 8 ? element.MaxLength/2 : 4;
							sql.AppendFormat("  [{0}] [decimal]({1},{2}) NULL,\n", element.Reference, precision, scale);
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
								precision = element.MaxLength - element.ImpliedDecimalPlaces + 2;
								scale = element.ImpliedDecimalPlaces;
								sql.AppendFormat("  [{0}] [decimal]({1},{2}) NULL,\n", element.Reference, precision, scale);
							}
							break;
						case ElementDataTypeEnum.Date:
							sql.AppendFormat("  [{0}] [{1}] NULL,\n", element.Reference, _dateType);
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
)
", Schema, spec.SegmentId);
			ExecuteCmd(sql.ToString());

			ExecuteCmd(string.Format(@"
CREATE VIEW [{0}].[LastRev{1}]
AS
select *
from [{0}].[{1}] a
where RevisionId = (select max([RevisionId])
                    from [{0}].[{1}] b 
                    where a.InterchangeId = b.InterchangeId 
                      and a.PositionInInterchange = b.PositionInInterchange
                    )", Schema, spec.SegmentId, commonSchema));
		}

		public void AddErrorIdToIndexedSegmentTable(string segmentId)
		{
			ExecuteCmd(string.Format("ALTER TABLE [{0}].[{1}] ADD [ErrorId] [{2}] NULL;", Schema, segmentId, _identitySqlType));
		}

		public void CreateSplitSegmentFunction()
		{
			ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].[SplitSegment]
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
END", Schema));
		}

		public void CreateFlatElementsFunction()
		{
			ExecuteCmd(new SqlCommand(string.Format(@"
CREATE FUNCTION [{0}].[FlatElements]
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
from [{0}].SplitSegment(@delimiter,@segment)
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
)", Schema)));
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
)", Schema, _identitySqlType));
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
)", Schema, _identitySqlType));
		}

		public void CreateGetTransactionSetSegmentsFunction()
		{
			ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].GetTransactionSetSegments
(	
	@transactionSetId {1}, @includeControlSegments bit, @revisionId int
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
)", Schema, _identitySqlType));
		}

		public void CreateGetTransactionSegmentsFunction()
		{
			ExecuteCmd(string.Format(@"
CREATE FUNCTION [{0}].[GetTransactionSegments]
(	
	@loopId {1}, @includeControlSegments bit, @revisionId int
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
    join [{0}].Loop l on l.ParentLoopId = tl.Id and tl.StartingSegmentId <> 'HL'
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
    and l.StartingSegmentId <> 'HL'
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
)", Schema, _identitySqlType));
		}

		public void ExecuteCmd(string sql)
		{
			ExecuteCmd(new SqlCommand(sql));
		}

		public void ExecuteCmd(SqlCommand cmd)
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

		public object ExecuteScalar(SqlCommand cmd)
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
			return cmd.ExecuteScalar();
		}

		public void RemoveIdentityColumn(string table)
		{
			using (var conn = new SqlConnection(_dsn))
			{
				conn.Open();

				using (var tx = conn.BeginTransaction())
				{
					var createTempColCmd = conn.CreateCommand();
					createTempColCmd.Transaction = tx;
					createTempColCmd.CommandText = string.Format(@"
						alter table [{0}].[{1}] drop constraint PK_{1}_{0}
						alter table [{0}].[{1}] add TempId int null", Schema, table);

					var updateAndRenameCmd = conn.CreateCommand();
					updateAndRenameCmd.Transaction = tx;
					updateAndRenameCmd.CommandText = string.Format(@"
						update [{0}].[{1}] set TempId = Id
						alter table [{0}].[{1}] alter column TempId int not null
						alter table [{0}].[{1}] drop column Id
						exec sp_rename '[{0}].[{1}].TempId', 'Id', 'COLUMN'
						alter table [{0}].[{1}] add constraint PK_{1}_{0} primary key clustered (Id)", Schema, table);

					createTempColCmd.ExecuteNonQuery();
					updateAndRenameCmd.ExecuteNonQuery();
					tx.Commit();
				}
			}
		}

		public bool HasIdentityColumn(string table)
		{
			var cmd = new SqlCommand(@"SELECT CASE WHEN EXISTS( SELECT 1
					FROM     SYS.IDENTITY_COLUMNS 
					WHERE object_id = object_id(@tablename)) THEN 1 ELSE 0 END");

			cmd.Parameters.AddWithValue("tablename", string.Format("[{0}].[{1}]", Schema, table));
			var result = ExecuteScalar(cmd);
			return Convert.ToBoolean(result);
		}

		public void CreateSchema()
		{
			ExecuteCmd(new SqlCommand(string.Format(@"CREATE SCHEMA [{0}] AUTHORIZATION [dbo]", Schema)));
		}

		public bool FunctionExists(string functionName)
		{
			var result =
				ExecuteScalar(
					new SqlCommand(
						string.Format(
							@"select case when exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[{1}]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT')) then 1 else 0 end",
							Schema,
							functionName)));

			return Convert.ToInt32(result) != 0;
		}

		public bool SchemaExists()
		{
			var result =
				ExecuteScalar(
					new SqlCommand(
						string.Format(
							@"select case when EXISTS (SELECT * FROM sys.schemas WHERE name = N'{0}') then 1 else 0 end",
							Schema)));

			return Convert.ToInt32(result) != 0;
		}

		public bool TableExists(string tableName)
		{
			var result =
				ExecuteScalar(
					new SqlCommand(
						string.Format(
							@"select case when EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[{1}]') AND type in (N'U')) then 1 else 0 end",
							Schema,
							tableName)));

			return Convert.ToInt32(result) != 0;
		}

		public bool ViewExists(string viewName)
		{
			var result =
				ExecuteScalar(
					new SqlCommand(
						string.Format(
							@"select case when EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[{0}].[{1}]')) then 1 else 0 end",
							Schema,
							viewName)));

			return Convert.ToInt32(result) != 0;
		}

		public bool TableColumnExists(string tableName, string columnName)
		{
			var result = ExecuteScalar(new SqlCommand(string.Format(@"select case when EXISTS 
(select *
from information_schema.columns
where table_schema='{0}' 
and Table_name = '{1}'
and column_name = '{2}') then 1 else 0 end", Schema, tableName, columnName)));

			return Convert.ToInt32(result) != 0;
		}
	}
}