namespace OopFactory.X12.Sql
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Diagnostics;
	using System.Linq;
	using System.Text;
	using Parsing.Model;
	using Parsing.Specification;
	using Repositories;

	internal class SegmentBatch
	{
		private readonly IParsingErrorRepo _errorRepo;
		internal Dictionary<string, DataTable> _parsedTables;
		internal DataTable _segmentTable;
		internal DataTable _loopTable;
		private readonly Type _identityType;
		private readonly object _defaultIdentityTypeValue;

		public SegmentBatch(IParsingErrorRepo errorRepo, Type identityType)
		{
			_identityType = identityType;
			_defaultIdentityTypeValue = identityType.GetDefaultValue();
			_errorRepo = errorRepo;
			_parsedTables = new Dictionary<string, DataTable>();

			_segmentTable = new DataTable();
			_segmentTable.Columns.Add("InterchangeId", identityType);
			_segmentTable.Columns.Add("PositionInInterchange", typeof (int));
			_segmentTable.Columns.Add("RevisionId", typeof (int));
			_segmentTable.Columns.Add("FunctionalGroupId", identityType);
			_segmentTable.Columns.Add("TransactionSetId", identityType);
			_segmentTable.Columns.Add("ParentLoopId", identityType);
			_segmentTable.Columns.Add("LoopId", identityType);
			_segmentTable.Columns.Add("Deleted", typeof (bool));
			_segmentTable.Columns.Add("SegmentId", typeof (string));
			_segmentTable.Columns.Add("Segment", typeof (string));

			_loopTable = new DataTable();
			_loopTable.Columns.Add("Id", identityType);
			_loopTable.Columns.Add("ParentLoopId", identityType);
			_loopTable.Columns.Add("InterchangeId", identityType);
			_loopTable.Columns.Add("TransactionSetId", identityType);
			_loopTable.Columns.Add("TransactionSetCode", typeof (string));
			_loopTable.Columns.Add("SpecLoopId", typeof (string));
			_loopTable.Columns.Add("StartingSegmentId", typeof (string));
			_loopTable.Columns.Add("EntityIdentifierCode", typeof (string));
		}

		public int LoopCount
		{
			get { return _loopTable.Rows.Count; }
		}

		public int SegmentCount
		{
			get { return _segmentTable.Rows.Count; }
		}

		public string StartingSegment
		{
			get
			{
				if (_segmentTable.Rows.Count > 0)
				{
					var firstSegment = _segmentTable.Rows[0];
					return string.Format(
						"{2} (InterchangeId:{0};Position:{1})",
						firstSegment["InterchangeId"],
						firstSegment["PositionInInterchange"],
						firstSegment["Segment"]);
				}
				return null;
			}
		}

		public void Clear()
		{
			_parsedTables.Clear();
			_segmentTable.Clear();
		}

		public void AddSegment(
			SqlTransaction tran,
			object interchangeId,
			int positionInInterchange,
			int revisionId,
			object functionalGroupId,
			object transactionSetId,
			object parentLoopId,
			object loopId,
			bool deleted,
			DetachedSegment segment,
			SegmentSpecification spec)
		{
			_segmentTable.Rows.Add(
				interchangeId,
				positionInInterchange,
				revisionId,
				functionalGroupId,
				transactionSetId,
				parentLoopId,
				loopId,
				deleted,
				segment.SegmentId,
				segment.SegmentString);

			if (spec != null)
			{
				var parsingError = new StringBuilder();

				var fieldNames = new List<string>();

				int maxElements = spec != null ? spec.Elements.Count : 0;

				for (var i = 1; i == 1 || i <= maxElements; i++)
				{
					fieldNames.Add(string.Format("{0:00}", i));
				}

				if (!_parsedTables.ContainsKey(segment.SegmentId))
				{
					_parsedTables.Add(segment.SegmentId, new DataTable());
					_parsedTables[segment.SegmentId].Columns.Add("InterchangeId", _identityType);
					_parsedTables[segment.SegmentId].Columns.Add("PositionInInterchange", typeof (int));
					_parsedTables[segment.SegmentId].Columns.Add("TransactionSetId", _identityType);
					_parsedTables[segment.SegmentId].Columns.Add("ParentLoopId", _identityType);
					_parsedTables[segment.SegmentId].Columns.Add("LoopId", _identityType);
					_parsedTables[segment.SegmentId].Columns.Add("RevisionId", typeof (int));
					_parsedTables[segment.SegmentId].Columns.Add("Deleted", typeof (bool));

					foreach (var f in fieldNames)
						_parsedTables[segment.SegmentId].Columns.Add(f, typeof (string));

					_parsedTables[segment.SegmentId].Columns.Add("ErrorId", _identityType);
				}
				var row = _parsedTables[segment.SegmentId].NewRow();

				row["InterchangeId"] = interchangeId;
				row["PositionInInterchange"] = positionInInterchange;
				row["TransactionSetId"] = transactionSetId ?? DBNull.Value;
				row["ParentLoopId"] = parentLoopId ?? DBNull.Value;
				row["LoopId"] = loopId ?? DBNull.Value;
				row["RevisionId"] = revisionId;
				row["Deleted"] = deleted;

				for (var i = 1; i <= segment.ElementCount && i <= maxElements; i++)
				{
					try
					{
						string val = segment.GetElement(i);
						var elementSpec = spec.Elements[i - 1];
						int maxLength = elementSpec.MaxLength;
						var column = string.Format("{0:00}", i);

						if (maxLength > 0 && val.Length > maxLength)
						{
							var message =
								string.Format(
									"Element {2}{3:00} in position {1} of interchange {0} will be truncated because {4} exceeds the max length of {5}.",
									interchangeId,
									positionInInterchange,
									segment.SegmentId,
									i,
									val,
									maxLength);
							Trace.TraceInformation(message);
							parsingError.AppendLine(message);
							val = val.Substring(0, maxLength);
						}

						if (elementSpec.Type == ElementDataTypeEnum.Numeric && elementSpec.ImpliedDecimalPlaces > 0)
						{
							var intVal = 0;
							if (string.IsNullOrWhiteSpace(val))
							{
								row[column] = null;
							}
							else if (int.TryParse(val, out intVal))
							{
								var denominator = (decimal) Math.Pow(10, elementSpec.ImpliedDecimalPlaces);
								row[column] = intVal/denominator;
							}
							else
							{
								var message =
									string.Format(
										"Element {2}{3:00} in position {1} of interchange {0} cannot be indexed because '{4}' could not be parsed into an implied decimal with precision {5}.",
										interchangeId,
										positionInInterchange,
										segment.SegmentId,
										i,
										val,
										elementSpec.ImpliedDecimalPlaces);
								Trace.TraceInformation(message);
								parsingError.AppendLine(message);
								row[column] = null;
							}
						}
						else if (elementSpec.Type == ElementDataTypeEnum.Numeric || elementSpec.Type == ElementDataTypeEnum.Decimal)
						{
							decimal decVal = 0;
							if (string.IsNullOrWhiteSpace(val))
								row[column] = null;
							else if (decimal.TryParse(val, out decVal))
								row[column] = val;
							else
							{
								var message =
									string.Format(
										"Element {2}{3:00} in position {1} of interchange {0} cannot be indexed because '{4}' could not be parsed into a decimal.",
										interchangeId,
										positionInInterchange,
										segment.SegmentId,
										i,
										val);
								Trace.TraceInformation(message);
								parsingError.AppendLine(message);
								row[column] = null;
							}
						}
						else if (elementSpec.Type == ElementDataTypeEnum.Date)
						{
							if (string.IsNullOrWhiteSpace(val))
								row[column] = null;
							else
							{
								var date = DateTime.MinValue;
								if (val.Length == 8 &&
								    DateTime.TryParse(
									    string.Format("{0}-{1}-{2}", val.Substring(0, 4), val.Substring(4, 2), val.Substring(6, 2)),
									    out date))
									row[column] = date;
								else
								{
									var message =
										string.Format(
											"Element {2}{3:00} in position {1} of interchange {0} cannot be indexed because '{4}' could not be parsed into a date.",
											interchangeId,
											positionInInterchange,
											segment.SegmentId,
											i,
											val);
									Trace.TraceInformation(message);
									parsingError.AppendLine(message);
									row[column] = null;
								}
							}
						}
						else
							row[column] = val;
					}
					catch (Exception e)
					{
						var message = string.Format(
							"Error parsing '{0}' using spec {1} with {2} elements: {3}",
							segment.SegmentString,
							spec.SegmentId,
							spec.Elements.Count(),
							e.Message);
						Trace.TraceInformation(message);
						parsingError.AppendLine(message);
					}
				}

				if (parsingError.Length > 0)
					row["ErrorId"] = _errorRepo.PersistParsingError(
						interchangeId,
						positionInInterchange,
						revisionId,
						parsingError.ToString());

				_parsedTables[segment.SegmentId].Rows.Add(row);
			}
		}

		public void AddLoop(
			object id,
			Loop loop,
			object interchangeId,
			object transactionSetId,
			string transactionSetCode,
			object parentLoopId,
			string entityIdentifierCode)
		{
			var row = _loopTable.NewRow();

			row["Id"] = id;
			row["ParentLoopId"] = (parentLoopId != null && parentLoopId != _defaultIdentityTypeValue)
				? parentLoopId
				: DBNull.Value;
			row["InterchangeId"] = interchangeId;
			row["TransactionSetId"] = (transactionSetId != null && transactionSetId != _defaultIdentityTypeValue)
				? transactionSetId
				: DBNull.Value;
			row["TransactionSetCode"] = transactionSetCode;
			row["SpecLoopId"] = loop.Specification.LoopId;
			row["StartingSegmentId"] = loop.SegmentId;
			row["EntityIdentifierCode"] = entityIdentifierCode;

			_loopTable.Rows.Add(row);
		}
	}
}