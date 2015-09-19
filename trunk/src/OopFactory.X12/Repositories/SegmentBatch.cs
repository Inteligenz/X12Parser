using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Repositories
{

    internal class SegmentBatch<T> where T : struct
    {
        private IParsingErrorRepo<T> _errorRepo;
        internal Dictionary<string, DataTable> _parsedTables;
        internal DataTable _segmentTable;
        
        public SegmentBatch(IParsingErrorRepo<T> errorRepo)
        {
            _errorRepo = errorRepo;
            _parsedTables = new Dictionary<string, DataTable>();

            _segmentTable = new DataTable();
            _segmentTable.Columns.Add("InterchangeId", typeof(T));
            _segmentTable.Columns.Add("PositionInInterchange", typeof(int));
            _segmentTable.Columns.Add("RevisionId", typeof(int));
            _segmentTable.Columns.Add("FunctionalGroupId", typeof(T));
            _segmentTable.Columns.Add("TransactionSetId", typeof(T));
            _segmentTable.Columns.Add("ParentLoopId", typeof(T));
            _segmentTable.Columns.Add("LoopId", typeof(T));
            _segmentTable.Columns.Add("Deleted", typeof(bool));
            _segmentTable.Columns.Add("SegmentId", typeof(string));
            _segmentTable.Columns.Add("Segment", typeof(string));
            
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
                    DataRow firstSegment = _segmentTable.Rows[0];
                    return string.Format("{2} (InterchangeId:{0};Position:{1})",
                        firstSegment["InterchangeId"],
                        firstSegment["PositionInInterchange"],
                        firstSegment["Segment"]);
                }
                else
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
            T interchangeId,
            int positionInInterchange,
            int revisionId,
            T functionalGroupId,
            T transactionSetId,
            T parentLoopId,
            T loopId,
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
                StringBuilder parsingError = new StringBuilder();

                List<string> fieldNames = new List<string>();

                int maxElements = spec != null ? spec.Elements.Count : 0;

                for (int i = 1; i == 1 || i <= maxElements; i++)
                {
                    fieldNames.Add(string.Format("{0:00}", i));                    
                }

                if (!_parsedTables.ContainsKey(segment.SegmentId))
                {
                    _parsedTables.Add(segment.SegmentId, new DataTable());
                    _parsedTables[segment.SegmentId].Columns.Add("InterchangeId", typeof(T));
                    _parsedTables[segment.SegmentId].Columns.Add("PositionInInterchange", typeof(int));
                    _parsedTables[segment.SegmentId].Columns.Add("TransactionSetId", typeof(T));
                    _parsedTables[segment.SegmentId].Columns.Add("ParentLoopId", typeof(T));
                    _parsedTables[segment.SegmentId].Columns.Add("LoopId", typeof(T));
                    _parsedTables[segment.SegmentId].Columns.Add("RevisionId", typeof(int));
                    _parsedTables[segment.SegmentId].Columns.Add("Deleted", typeof(bool));

                    foreach (var f in fieldNames)
                        _parsedTables[segment.SegmentId].Columns.Add(f, typeof(string));

                    _parsedTables[segment.SegmentId].Columns.Add("ErrorId", typeof(T));
                }
                DataRow row = _parsedTables[segment.SegmentId].NewRow();

                row["InterchangeId"] = interchangeId;
                row["PositionInInterchange"] = positionInInterchange;
                row["TransactionSetId"] = transactionSetId;
                row["ParentLoopId"] = parentLoopId;
                row["LoopId"] = loopId;
                row["RevisionId"] = revisionId;
                row["Deleted"] = deleted;
                
                T errorID;

                for (int i = 1; i <= segment.ElementCount && i <= maxElements; i++)
                {
                    try
                    {
                        string val = segment.GetElement(i);
                        var elementSpec = spec.Elements[i - 1];
                        int maxLength = elementSpec.MaxLength;
                        string column = string.Format("{0:00}", i);

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
                                row[column] = null;
                            }
                            else if (int.TryParse(val, out intVal))
                            {
                                decimal denominator = (decimal)Math.Pow(10, elementSpec.ImpliedDecimalPlaces);
                                row[column] = (decimal)intVal / denominator;
                            }
                            else
                            {
                                string message = string.Format("Element {2}{3:00} in position {1} of interchange {0} cannot be indexed because '{4}' could not be parsed into an implied decimal with precision {5}.", interchangeId, positionInInterchange, segment.SegmentId, i, val, elementSpec.ImpliedDecimalPlaces);
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
                                string message = string.Format("Element {2}{3:00} in position {1} of interchange {0} cannot be indexed because '{4}' could not be parsed into a decimal.", interchangeId, positionInInterchange, segment.SegmentId, i, val);
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
                                DateTime date = DateTime.MinValue;
                                if (val.Length == 8 && DateTime.TryParse(string.Format("{0}-{1}-{2}", val.Substring(0, 4), val.Substring(4, 2), val.Substring(6, 2)), out date))
                                    row[column] = date;
                                else
                                {
                                    string message = string.Format("Element {2}{3:00} in position {1} of interchange {0} cannot be indexed because '{4}' could not be parsed into a date.", interchangeId, positionInInterchange, segment.SegmentId, i, val);
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
                        string message = string.Format("Error parsing '{0}' using spec {1} with {2} elements: {3}", segment.SegmentString, spec.SegmentId, spec.Elements.Count(), e.Message);
                        Trace.TraceInformation(message);
                        parsingError.AppendLine(message);
                    }
                }

                if (parsingError.Length > 0)
                    row["ErrorId"] = _errorRepo.PersistParsingError(interchangeId, positionInInterchange, revisionId, parsingError.ToString()); 
                
                _parsedTables[segment.SegmentId].Rows.Add(row);
            }

        }


    }
}
