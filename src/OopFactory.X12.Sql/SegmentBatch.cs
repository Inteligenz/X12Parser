namespace OopFactory.X12.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using OopFactory.X12.Shared.Models;
    using OopFactory.X12.Specifications;
    using OopFactory.X12.Specifications.Enumerations;
    using OopFactory.X12.Sql.Interfaces;

    internal class SegmentBatch
    {
        private readonly IParsingErrorRepo errorRepo;
        private readonly Type identityType;
        private readonly object defaultIdentityTypeValue;

        public SegmentBatch(IParsingErrorRepo errorRepo, Type identityType)
        {
            this.identityType = identityType;
            this.defaultIdentityTypeValue = identityType.GetDefaultValue();
            this.errorRepo = errorRepo;
            this.ParsedTables = new Dictionary<string, DataTable>();

            this.SegmentTable = new DataTable();
            this.SegmentTable.Columns.Add("InterchangeId", identityType);
            this.SegmentTable.Columns.Add("PositionInInterchange", typeof(int));
            this.SegmentTable.Columns.Add("RevisionId", typeof(int));
            this.SegmentTable.Columns.Add("FunctionalGroupId", identityType);
            this.SegmentTable.Columns.Add("TransactionSetId", identityType);
            this.SegmentTable.Columns.Add("ParentLoopId", identityType);
            this.SegmentTable.Columns.Add("LoopId", identityType);
            this.SegmentTable.Columns.Add("Deleted", typeof(bool));
            this.SegmentTable.Columns.Add("SegmentId", typeof(string));
            this.SegmentTable.Columns.Add("Segment", typeof(string));

            this.LoopTable = new DataTable();
            this.LoopTable.Columns.Add("Id", identityType);
            this.LoopTable.Columns.Add("ParentLoopId", identityType);
            this.LoopTable.Columns.Add("InterchangeId", identityType);
            this.LoopTable.Columns.Add("TransactionSetId", identityType);
            this.LoopTable.Columns.Add("TransactionSetCode", typeof(string));
            this.LoopTable.Columns.Add("SpecLoopId", typeof(string));
            this.LoopTable.Columns.Add("StartingSegmentId", typeof(string));
            this.LoopTable.Columns.Add("EntityIdentifierCode", typeof(string));
        }

        internal Dictionary<string, DataTable> ParsedTables { get; set; }

        internal DataTable SegmentTable { get; set; }

        internal DataTable LoopTable { get; set; }

        public int LoopCount => this.LoopTable.Rows.Count;

        public int SegmentCount => this.SegmentTable.Rows.Count;

        public string StartingSegment
        {
            get
            {
                if (this.SegmentTable.Rows.Count > 0)
                {
                    var firstSegment = this.SegmentTable.Rows[0];
                    return $"{firstSegment["Segment"]} (InterchangeId:{firstSegment["InterchangeId"]};Position:{firstSegment["PositionInInterchange"]})";
                }

                return null;
            }
        }

        /// <summary>
        /// Clears the tables of data
        /// </summary>
        public void Clear()
        {
            this.ParsedTables.Clear();
            this.SegmentTable.Clear();
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
            this.SegmentTable.Rows.Add(
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
                int maxElements = spec.Elements.Count;

                for (var i = 1; i == 1 || i <= maxElements; i++)
                {
                    fieldNames.Add(string.Format("{0:00}", i));
                }

                if (!this.ParsedTables.ContainsKey(segment.SegmentId))
                {
                    this.ParsedTables.Add(segment.SegmentId, new DataTable());
                    this.ParsedTables[segment.SegmentId].Columns.Add("InterchangeId", this.identityType);
                    this.ParsedTables[segment.SegmentId].Columns.Add("PositionInInterchange", typeof(int));
                    this.ParsedTables[segment.SegmentId].Columns.Add("TransactionSetId", this.identityType);
                    this.ParsedTables[segment.SegmentId].Columns.Add("ParentLoopId", this.identityType);
                    this.ParsedTables[segment.SegmentId].Columns.Add("LoopId", this.identityType);
                    this.ParsedTables[segment.SegmentId].Columns.Add("RevisionId", typeof(int));
                    this.ParsedTables[segment.SegmentId].Columns.Add("Deleted", typeof(bool));

                    foreach (string f in fieldNames)
                    {
                        this.ParsedTables[segment.SegmentId].Columns.Add(f, typeof(string));
                    }

                    this.ParsedTables[segment.SegmentId].Columns.Add("ErrorId", this.identityType);
                }

                DataRow row = this.ParsedTables[segment.SegmentId].NewRow();

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
                            int intVal;
                            if (string.IsNullOrWhiteSpace(val))
                            {
                                row[column] = null;
                            }
                            else if (int.TryParse(val, out intVal))
                            {
                                var denominator = (decimal)Math.Pow(10, elementSpec.ImpliedDecimalPlaces);
                                row[column] = intVal / denominator;
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
                            decimal decVal;
                            if (string.IsNullOrWhiteSpace(val))
                            {
                                row[column] = null;
                            }
                            else if (decimal.TryParse(val, out decVal))
                            {
                                row[column] = val;
                            }
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
                            {
                                row[column] = null;
                            }
                            else
                            {
                                DateTime date;
                                if (val.Length == 8 &&
                                    DateTime.TryParse(
                                        $"{val.Substring(0, 4)}-{val.Substring(4, 2)}-{val.Substring(6, 2)}",
                                        out date))
                                {
                                    row[column] = date;
                                }
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
                        {
                            row[column] = val;
                        }
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
                {
                    row["ErrorId"] = this.errorRepo.PersistParsingError(
                        interchangeId,
                        positionInInterchange,
                        revisionId,
                        parsingError.ToString());
                }

                this.ParsedTables[segment.SegmentId].Rows.Add(row);
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
            var row = this.LoopTable.NewRow();

            row["Id"] = id;
            row["ParentLoopId"] = (parentLoopId != null && parentLoopId != this.defaultIdentityTypeValue)
                ? parentLoopId
                : DBNull.Value;
            row["InterchangeId"] = interchangeId;
            row["TransactionSetId"] = (transactionSetId != null && transactionSetId != this.defaultIdentityTypeValue)
                ? transactionSetId
                : DBNull.Value;
            row["TransactionSetCode"] = transactionSetCode;
            row["SpecLoopId"] = loop.Specification.LoopId;
            row["StartingSegmentId"] = loop.SegmentId;
            row["EntityIdentifierCode"] = entityIdentifierCode;

            this.LoopTable.Rows.Add(row);
        }
    }
}