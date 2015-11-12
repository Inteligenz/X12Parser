namespace OopFactory.X12.Sql
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Diagnostics;
	using System.Linq;
	using System.Text;
	using Parsing;
	using Parsing.Model;
	using Parsing.Specification;

	public interface IParsingErrorRepo
	{
		object PersistParsingError(object interchangeId, int positionInInterchange, int? revisionId, string errorMessage);
	}

	/// <summary>
	///     Class for storing, retrieving and revising X12 messages.
	///     This library only does inserts.  Edits and Deletes are accomplished through revisions, but all revisions are
	///     retained.
	///     The Get methods will allow you choose the revision you want.
	/// </summary>
	/// <typeparam name="T">The type of all identity columns:  supports int or long</typeparam>
	public class SqlTransactionRepository : SqlReadOnlyTransactionRepository, IParsingErrorRepo
	{
		protected IDbCreation _commonDb;
		protected IDbCreation _transactionDb;
		private bool _schemaEnsured;
		private readonly Dictionary<string, SegmentSpecification> _specs;
		private readonly int _batchSize;
		private readonly IIdProvider _idProvider;
		//private int _batchCount;
		//private StringBuilder _batchSql;
		internal SegmentBatch _segmentBatch;

		public SqlTransactionRepository(string dsn, Type identityType)
			: this(dsn, new SpecificationFinder(), new[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER" }, identityType, "dbo")
		{
		}

		public SqlTransactionRepository(string dsn, string schema, Type identityType)
			: this(dsn, new SpecificationFinder(), new[] { "REF", "NM1", "N1", "N3", "N4", "DMG", "PER" }, identityType, schema)
		{
		}

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
			_commonDb = new DbCreation(dsn, commonSchema, identityType, sqlDateType);
			_transactionDb = new DbCreation(dsn, schema, identityType, sqlDateType);
			_idProvider = GetIdProvider(dsn, commonSchema, identityType, segmentBatchSize/10);
			_schemaEnsured = false;
			_batchSize = segmentBatchSize;
			_segmentBatch = new SegmentBatch(this, identityType);
			_specs = new Dictionary<string, SegmentSpecification>();
			foreach (var segmentId in indexedSegments)
			{
				var spec = specFinder.FindSegmentSpec("5010", segmentId.Trim());
				_specs.Add(segmentId.Trim(), spec);
			}
		}

		private IIdProvider GetIdProvider(string dsn, string commonSchema, Type identityType, int segmentBatchSize)
		{
			if (!identityType.IsValueType)
				throw new ArgumentException("identityType must be a value type", "identityType");

			if (!(identityType == typeof (Guid) || identityType == typeof (long) || identityType == typeof (int)))
				throw new ArgumentException("Only Guid, Long, and Int identity types are supported", "identityType");

			if (identityType == typeof (Guid))
				return new GuidIdentityProvider();

			if (identityType == typeof (long))
				return new LongHiLowIdentityProvider(dsn, commonSchema, segmentBatchSize);

			return new IntHiLowIdentityProvider(dsn, commonSchema, segmentBatchSize);
		}

		/// <summary>
		///     override this with no implementation when your database is already created and you will not need to check for
		///     existance of the objects.
		/// </summary>
		public virtual void EnsureSchema()
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
						if (element.Type == ElementDataTypeEnum.Identifier && !string.IsNullOrEmpty(element.QualifierSetId) &&
						    element.AllowedIdentifiers.Count > 0)
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
				    && _specs.ContainsKey("N1")
				    && _specs.ContainsKey("N3")
				    && _specs.ContainsKey("N4")
				    && _specs.ContainsKey("PER")
				    && _specs.ContainsKey("DMG"))
					_transactionDb.CreateEntityView(_commonDb.Schema);

				if (_commonDb.HasIdentityColumn("Container"))
					_commonDb.RemoveIdentityColumn("Container");

				if (_transactionDb.HasIdentityColumn("Interchange"))
					_transactionDb.RemoveIdentityColumn("Interchange");

				if (_transactionDb.HasIdentityColumn("FunctionalGroup"))
					_transactionDb.RemoveIdentityColumn("FunctionalGroup");

				if (_transactionDb.HasIdentityColumn("TransactionSet"))
					_transactionDb.RemoveIdentityColumn("TransactionSet");

				if (_transactionDb.HasIdentityColumn("Loop"))
					_transactionDb.RemoveIdentityColumn("Loop");

				if (_transactionDb.HasIdentityColumn("ParsingError"))
					_transactionDb.RemoveIdentityColumn("ParsingError");

				_idProvider.EnsureSchema();

				_schemaEnsured = true;
			}
		}

		/// <summary>
		///     Saves the entire interchange into the database as individual segments and the relationships between the segments
		///     and loops
		/// </summary>
		/// <param name="interchange">The parsed interchange object</param>
		/// <param name="filename"></param>
		/// <param name="userName"></param>
		/// <returns>The interchangeId from the database</returns>
		public object Save(Interchange interchange, string filename, string userName)
		{
			EnsureSchema();
			var positionInInterchange = 1;

			var interchangeId = SaveInterchange(interchange, filename, userName);
			try
			{
				SaveSegment(null, interchange, positionInInterchange, interchangeId);

				foreach (var fg in interchange.FunctionGroups)
				{
					var functionalGroupId = SaveFunctionalGroup(fg, interchangeId);
					SaveSegment(null, fg, ++positionInInterchange, interchangeId, functionalGroupId);

					foreach (var tran in fg.Transactions)
					{
						string transactionSetCode = tran.IdentifierCode;
						var transactionSetId = SaveTransactionSet(tran, interchangeId, functionalGroupId);
						SaveSegment(null, tran, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);

						foreach (var seg in tran.Segments)
						{
							if (seg is HierarchicalLoopContainer)
							{
								positionInInterchange++;
								SaveLoopAndChildren(
									(HierarchicalLoopContainer) seg,
									ref positionInInterchange,
									interchangeId,
									functionalGroupId,
									transactionSetId,
									transactionSetCode,
									null);
							}
							else
								SaveSegment(null, seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);
						}
						foreach (var hl in tran.HLoops)
						{
							positionInInterchange++;
							SaveLoopAndChildren(
								hl,
								ref positionInInterchange,
								interchangeId,
								functionalGroupId,
								transactionSetId,
								transactionSetCode,
								null);
						}

						foreach (var seg in tran.TrailerSegments)
							SaveSegment(null, seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId);
					}

					foreach (var seg in fg.TrailerSegments)
						SaveSegment(null, seg, ++positionInInterchange, interchangeId, functionalGroupId);
				}

				foreach (var seg in interchange.TrailerSegments)
					SaveSegment(null, seg, ++positionInInterchange, interchangeId);

				ExecuteBatch(null);
				return interchangeId;
			}
			catch (Exception)
			{
				MarkInterchangeWithError(interchangeId);
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
		/// <returns></returns>
		public int SaveRevision(IList<RepoSegment> segments, string comments, string revisedBy)
		{
			int? revisionId;
			using (var conn = new SqlConnection(_dsn))
			{
				conn.Open();
				var sqlTran = conn.BeginTransaction();
				try
				{
					var sql = string.Format(@"
insert into [{0}].[Revision] (SchemaName,Comments,RevisionDate,RevisedBy) 
values (@schemaName, @comments, getdate(), @revisedBy)

select scope_identity()", _commonDb.Schema);

					var cmd = new SqlCommand(sql, conn, sqlTran);
					cmd.Parameters.AddWithValue("@schemaName", _schema);
					cmd.Parameters.AddWithValue("@comments", comments);
					cmd.Parameters.AddWithValue("@revisedBy", revisedBy);
					revisionId = Convert.ToInt32(ExecuteScalar(cmd));

					foreach (var segment in segments)
					{
						SaveSegment(
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
			if (loop is HierarchicalLoop)
			{
				loopId = SaveHierarchicalLoop(
					(HierarchicalLoop) loop,
					interchangeId,
					transactionSetId,
					transactionSetCode,
					parentId);
			}
			else if (loop is Loop)
			{
				loopId = SaveLoop((Loop) loop, interchangeId, transactionSetId, transactionSetCode, parentId);
			}

			if (loopId != null && loopId != _defaultIdentityTypeValue)
			{
				SaveSegment(null, loop, positionInInterchange, interchangeId, functionalGroupId, transactionSetId, parentId, loopId);

				foreach (var seg in loop.Segments)
				{
					if (seg is HierarchicalLoopContainer)
					{
						positionInInterchange++;
						SaveLoopAndChildren(
							(HierarchicalLoopContainer) seg,
							ref positionInInterchange,
							interchangeId,
							functionalGroupId,
							transactionSetId,
							transactionSetCode,
							loopId);
					}
					else
						SaveSegment(null, seg, ++positionInInterchange, interchangeId, functionalGroupId, transactionSetId, loopId);
				}

				foreach (var hl in loop.HLoops)
				{
					positionInInterchange++;
					SaveLoopAndChildren(
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
			throw new InvalidOperationException(
				string.Format("Loop could not be created for interchange {0} position {1}.", interchangeId, positionInInterchange));
		}

		private void MarkInterchangeWithError(object interchangeId)
		{
			var cmd =
				new SqlCommand(string.Format("update [{0}].Interchange set HasError = 1 where Id = @interchangeId", _schema));
			cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
			ExecuteCmd(cmd);
		}

		protected virtual string GetContainerIdSql(string segmentId)
		{
			return string.Format(
				@"INSERT INTO [{1}].[Container] (Id, SchemaName, Type) VALUES (@containerId, '{0}','{2}');",
				_schema,
				_commonDb.Schema,
				segmentId);
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
					"Interchange date '{0}' and time '{1}' could not be parsed. {2}",
					interchange.GetElement(9),
					interchange.GetElement(10),
					exc.Message);
			}

			var interchangeId = _idProvider.NextId(_schema, "Interchange");
			var containerId = _idProvider.NextId(_commonDb.Schema, "Container");

			var cmd = new SqlCommand(GetContainerIdSql("ISA") + string.Format(@"
INSERT INTO [{0}].[Interchange] (Id, SenderId, ReceiverId, ControlNumber, [Date], SegmentTerminator, ElementSeparator, ComponentSeparator, Filename, HasError, CreatedBy, CreatedDate)
VALUES (@id, @senderId, @receiverId, @controlNumber, @date, @segmentTerminator, @elementSeparator, @componentSeparator, @filename, 0, @createdBy, getdate())
", _schema));
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

			ExecuteCmd(cmd);

			return interchangeId;
		}

		private object SaveFunctionalGroup(FunctionGroup functionGroup, object interchangeId)
		{
			string idCode;
			var date = DateTime.MaxValue;
			var controlNumber = 0;
			string version;

			if (functionGroup.FunctionalIdentifierCode.Length <= 2)
				idCode = functionGroup.FunctionalIdentifierCode;
			else
			{
				idCode = functionGroup.FunctionalIdentifierCode.Substring(0, 2);
				Trace.TraceWarning(
					"FunctionalIdentifier code '{0}' will be truncated because it exceeds the max length of 2.",
					functionGroup.FunctionalIdentifierCode);
			}
			try
			{
				date = functionGroup.Date;
			}
			catch (Exception exc)
			{
				Trace.TraceWarning(
					"FunctionalGroup date '{0}' and time '{1}' could not be parsed. {2}",
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
					"FunctionalGroup control number '{0}' could not be parsed. {1}",
					functionGroup.GetElement(6),
					exc.Message);
			}
			if (functionGroup.VersionIdentifierCode.Length <= 12)
				version = functionGroup.VersionIdentifierCode;
			else
			{
				version = functionGroup.VersionIdentifierCode.Substring(0, 12);
				Trace.TraceWarning(
					"FunctionalGroup version number '{0}' will be truncated because it exceeds the max length of 12.",
					functionGroup.VersionIdentifierCode);
			}

			var functionalGroupId = _idProvider.NextId(_schema, "FunctionalGroup");
			var containerId = _idProvider.NextId(_commonDb.Schema, "Container");

			var cmd = new SqlCommand(GetContainerIdSql("GS") + string.Format(@"
INSERT INTO [{0}].[FunctionalGroup] (Id, InterchangeId, FunctionalIdCode, Date, ControlNumber, Version)
VALUES (@id, @interchangeId, @functionalIdCode, @date, @controlNumber, @version)
", _schema, _commonDb.Schema));
			cmd.Parameters.AddWithValue("@id", functionalGroupId);
			cmd.Parameters.AddWithValue("@containerId", containerId);
			cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
			cmd.Parameters.AddWithValue("@functionalIdCode", idCode);
			cmd.Parameters.AddWithValue("@date", date);
			cmd.Parameters.AddWithValue("@controlNumber", controlNumber);
			cmd.Parameters.AddWithValue("@version", version);

      ExecuteCmd(cmd);

			return functionalGroupId;
		}

		private object SaveTransactionSet(Transaction transaction, object interchangeId, object functionalGroupId)
		{
			string controlNumber = transaction.ControlNumber;
			if (controlNumber.Length > 9)
			{
				controlNumber = controlNumber.Substring(0, 9);
				Trace.TraceWarning(
					"Transaction control number '{0}' will be truncated because it exceeds the max length of 9.",
					transaction.ControlNumber);
			}

			var transactionSetId = _idProvider.NextId(_schema, "TransactionSet");
			var containerId = _idProvider.NextId(_commonDb.Schema, "Container");

			var cmd = new SqlCommand(GetContainerIdSql("ST") + string.Format(@"
INSERT INTO [{0}].[TransactionSet] (Id, InterchangeId, FunctionalGroupId, IdentifierCode, ControlNumber) 
VALUES (@id, @interchangeId, @functionalGroupId, @identifierCode, @controlNumber)
", _schema, _commonDb.Schema));
			cmd.Parameters.AddWithValue("@id", transactionSetId);
			cmd.Parameters.AddWithValue("@containerId", containerId);
			cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
			cmd.Parameters.AddWithValue("@functionalGroupId", functionalGroupId);
			cmd.Parameters.AddWithValue("@identifierCode", transaction.IdentifierCode);
			cmd.Parameters.AddWithValue("@controlNumber", controlNumber);

			ExecuteCmd(cmd);

			return transactionSetId;
		}

		private object SaveHierarchicalLoop(
			HierarchicalLoop loop,
			object interchangeId,
			object transactionSetId,
			string transactionSetCode,
			object parentLoopId)
		{
			var hlId = _idProvider.NextId(_schema, "Loop");
			var containerId = _idProvider.NextId(_commonDb.Schema, "Container");

			var cmd = new SqlCommand(GetContainerIdSql("HL") + string.Format(@"
INSERT INTO [{0}].[Loop] (Id, ParentLoopId, InterchangeId, TransactionSetId, TransactionSetCode, SpecLoopId, LevelId, LevelCode, StartingSegmentId)
VALUES (@id, @parentLoopId, @interchangeId, @transactionSetId, @transactionSetCode, @specLoopId, @levelId, @levelCode, 'HL')
", _schema, _commonDb.Schema));
			cmd.Parameters.AddWithValue("@id", hlId);
			cmd.Parameters.AddWithValue("@containerId", containerId);
			cmd.Parameters.AddWithValue("@parentLoopId", parentLoopId != null && parentLoopId != _defaultIdentityTypeValue ? parentLoopId : DBNull.Value);
			cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
			cmd.Parameters.AddWithValue("@transactionSetId", transactionSetId);
			cmd.Parameters.AddWithValue("@transactionSetCode", transactionSetCode);
			cmd.Parameters.AddWithValue("@specLoopId", loop.Specification.LoopId);
			cmd.Parameters.AddWithValue("@levelId", loop.Id);
			cmd.Parameters.AddWithValue("@levelCode", loop.LevelCode);

			ExecuteCmd(cmd);

			return hlId;
		}

		protected virtual string GetEntityTypeCode(Loop loop)
		{
			if (new[] { "CLI", "CUR", "G18", "MRC", "N1", "NM1", "NX1", "RDI" }.Contains(loop.SegmentId))
				return loop.GetElement(1);

			if (new[] { "ENT", "LCD", "NX1", "PLA", "PT" }.Contains(loop.SegmentId))
				return loop.GetElement(2);

			if (new[] { "IN1", "NX1", "SCH" }.Contains(loop.SegmentId))
				return loop.GetElement(3);

			return null;
		}

		protected string GetSaveLoopSql(
			object id,
			Loop loop,
			object interchangeId,
			object transactionSetId,
			string transactionSetCode,
			object parentLoopId)
		{
			var entityIdentifierCode = GetEntityTypeCode(loop);

			var sql = new StringBuilder();

			sql.AppendFormat(
				@"
INSERT INTO [{0}].[Loop] (Id, ParentLoopId, InterchangeId, TransactionSetId, TransactionSetCode, SpecLoopId, StartingSegmentId, EntityIdentifierCode)
VALUES ('{1}', {2}, '{3}', '{4}', '{5}', '{6}', '{7}', {8}) ",
				_schema
				,
				id
				,
				parentLoopId == _defaultIdentityTypeValue ? "NULL" : string.Format("'{0}'", parentLoopId)
				,
				interchangeId
				,
				transactionSetId
				,
				transactionSetCode
				,
				loop.Specification.LoopId
				,
				loop.SegmentId
				,
				entityIdentifierCode == null ? "NULL" : string.Format("'{0}'", entityIdentifierCode)
				);

			return sql.ToString();
		}

		protected virtual object SaveLoop(
			Loop loop,
			object interchangeId,
			object transactionSetId,
			string transactionSetCode,
			object parentLoopId)
		{
			var id = _idProvider.NextId(_schema, "Loop");

			_segmentBatch.AddLoop(
				id,
				loop,
				interchangeId,
				transactionSetId != _defaultIdentityTypeValue ? transactionSetId : null,
				transactionSetCode,
				parentLoopId != _defaultIdentityTypeValue ? parentLoopId : null,
				GetEntityTypeCode(loop));

			return id;
		}

		private bool SegmentHasChanged(
			DetachedSegment segment,
			int positionInInterchange,
			object interchangeId,
			int? previousRevisionId)
		{
			using (var conn = new SqlConnection(_dsn))
			{
				var cmd = new SqlCommand(string.Format(@"
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
						throw new InvalidOperationException(
							string.Format(
								"Segment {0} of interchange {1} in position {2} has already been deleted by {3} at {4}.",
								segment.SegmentId,
								interchangeId,
								positionInInterchange,
								reader["RevisedBy"],
								reader["RevisionDate"]));

					if (previousRevisionId.HasValue && Convert.ToInt64(reader["RevisionId"]) != Convert.ToInt64(previousRevisionId))
						throw new InvalidOperationException(
							string.Format(
								"Segment {0} of interchange {1} in position {2} has already been revised by {3} at {4}.",
								segment.SegmentId,
								interchangeId,
								positionInInterchange,
								reader["RevisedBy"],
								reader["RevisionDate"]));

					return Convert.ToString(reader["Segment"]) != segment.SegmentString;
				}
				throw new InvalidOperationException(
					string.Format(
						"A segment does not exist for interchange {0} at position {1}.",
						interchangeId,
						positionInInterchange));
			}
		}

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
			if (!revisionId.HasValue || SegmentHasChanged(segment, positionInInterchange, interchangeId, previousRevisionId) ||
			    deleted)
			{
				_segmentBatch.AddSegment(
					tran,
					interchangeId,
					positionInInterchange,
					revisionId ?? 0,
					ConvertT(functionalGroupId),
					ConvertT(transactionSetId),
					ConvertT(parentLoopId),
					ConvertT(loopId),
					deleted,
					segment,
					_specs.ContainsKey(segment.SegmentId) ? _specs[segment.SegmentId] : null);

				if (tran != null || _segmentBatch._segmentTable.Rows.Count >= _batchSize)
				{
					ExecuteBatch(tran);
				}
			}
		}

		internal virtual void ExecuteBatch(SqlTransaction tran)
		{
			if (_segmentBatch.LoopCount > 0)
			{
				try
				{
					using (var conn = tran == null ? new SqlConnection(_dsn) : tran.Connection)
					{
						if (conn.State != ConnectionState.Open)
							conn.Open();

						using (var sbc = new SqlBulkCopy(conn))
						{
							sbc.DestinationTableName = string.Format("[{0}].[Container]", _commonDb.Schema);

							var containerTable = new DataTable();
							containerTable.Columns.Add("Id", _identityType);
							containerTable.Columns.Add("SchemaName", typeof (string));
							containerTable.Columns.Add("Type", typeof (string));

							foreach (DataRow row in _segmentBatch._loopTable.Rows)
							{
								var containerId = _idProvider.NextId(_commonDb.Schema, "Container");
								containerTable.Rows.Add(containerId, _schema, row["StartingSegmentId"]);
							}

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
					Trace.TraceInformation(
						"Error Saving {0} loops to db starting with {1}.",
						_segmentBatch.LoopCount,
						_segmentBatch.StartingSegment);

					throw;
				}
			}

			if (_segmentBatch.SegmentCount > 0)
			{
				try
				{
					using (var conn = tran == null ? new SqlConnection(_dsn) : tran.Connection)
					{
						if (conn.State != ConnectionState.Open)
							conn.Open();
						using (var sbc = new SqlBulkCopy(conn))
						{
							sbc.DestinationTableName = string.Format("[{0}].Segment", _schema);
							foreach (DataColumn c in _segmentBatch._segmentTable.Columns)
								sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
							sbc.WriteToServer(_segmentBatch._segmentTable);

							foreach (var pair in _segmentBatch._parsedTables)
							{
								sbc.ColumnMappings.Clear();

								sbc.DestinationTableName = string.Format("[{0}].[{1}]", _schema, pair.Key);
								foreach (DataColumn c in pair.Value.Columns)
									sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
								sbc.WriteToServer(pair.Value);
							}
						}
					}
					_segmentBatch = new SegmentBatch(this, _identityType);
				}
				catch (Exception exc)
				{
					Trace.WriteLine(exc.Message);
					Trace.TraceInformation(
						"Error Saving {0} segments to db starting with {1}.",
						_segmentBatch.SegmentCount,
						_segmentBatch.StartingSegment);

					throw;
				}
			}
		}

		public object PersistParsingError(
			object interchangeId,
			int positionInInterchange,
			int? revisionId,
			string errorMessage)
		{
			var errorId = _idProvider.NextId(_schema, "ParsingError");
			var cmd = new SqlCommand(string.Format(@"
INSERT INTO [{0}].ParsingError (Id, InterchangeId,PositionInInterchange,RevisionId,Message) 
VALUES (@id, @interchangeId, @positionInInterchange, @revisionId, @message)
", _schema));

			cmd.Parameters.AddWithValue("@id", errorId);
			cmd.Parameters.AddWithValue("@interchangeId", interchangeId);
			cmd.Parameters.AddWithValue("@positionInInterchange", positionInInterchange);
			cmd.Parameters.AddWithValue("@revisionId", revisionId ?? 0);
			cmd.Parameters.AddWithValue("@message", errorMessage);

      ExecuteCmd(cmd);

			return errorId;
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