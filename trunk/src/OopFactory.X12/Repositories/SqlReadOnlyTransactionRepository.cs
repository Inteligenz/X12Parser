using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace OopFactory.X12.Repositories
{
    public class SqlReadOnlyTransactionRepository<T> where T : struct
    {
        protected readonly string _dsn;
        protected readonly string _schema;

        public SqlReadOnlyTransactionRepository(string dsn, string schema = "dbo")
        {
            _dsn = dsn;
            _schema = schema;
        }

        protected T ConvertT(object val)
        {
            if (typeof(T) == typeof(long))
                return (T)(object)Convert.ToInt64(val);
            else
                return (T)(object)Convert.ToInt32(val);
        }

        private RepoSegment<T> RepoSegmentFromReader(SqlDataReader reader)
        {
            RepoSegment<T> segment = new RepoSegment<T>
            {
                InterchangeId = ConvertT(reader["InterchangeId"]),
                PositionInInterchange = Convert.ToInt32(reader["PositionInInterchange"]),
                RevisionId = ConvertT(reader["RevisionId"]),
                Deleted = Convert.ToBoolean(reader["Deleted"]),
                SpecLoopId = Convert.ToString(reader["SpecLoopId"]),
                SegmentId = Convert.ToString(reader["SegmentId"]),
                SegmentString = Convert.ToString(reader["Segment"]),
                SegmentTerminator = Convert.ToChar(reader["SegmentTerminator"]),
                ElementSeparator = Convert.ToChar(reader["ElementSeparator"]),
                ComponentSeparator = Convert.ToChar(reader["ComponentSeparator"])
            };

            if (!reader.IsDBNull(reader.GetOrdinal("FunctionalGroupId")))
                segment.FunctionalGroupId = ConvertT(reader["FunctionalGroupId"]);

            if (!reader.IsDBNull(reader.GetOrdinal("TransactionSetId")))
                segment.TransactionSetId = ConvertT(reader["TransactionSetId"]);

            if (!reader.IsDBNull(reader.GetOrdinal("ParentLoopId")))
                segment.ParentLoopId = ConvertT(reader["ParentLoopId"]);

            if (!reader.IsDBNull(reader.GetOrdinal("LoopId")))
                segment.LoopId = ConvertT(reader["LoopId"]);
            return segment;
        }

        /// <summary>
        /// Retrieve all the segments within a transaction
        /// </summary>
        /// <param name="transactionSetId"></param>
        /// <param name="revisionId">Use 0 for the original version Int32.MaxValue when you want the latest revision</param>
        /// <param name="includeControlSegments">This will include the ISA, GS, GE and IEA segments</param>
        /// <returns></returns>
        public List<RepoSegment<T>> GetTransactionSetSegments(T transactionSetId, T revisionId, bool includeControlSegments = false)
        {
            using (var conn = new SqlConnection(_dsn))
            {
                SqlCommand cmd = new SqlCommand(string.Format(@"
select ts.InterchangeId, ts.FunctionalGroupId, ts.TransactionSetId, ts.ParentLoopId, ts.LoopId, ts.RevisionId, ts.Deleted,
    ts.PositionInInterchange, l.SpecLoopId, ts.SegmentId, ts.Segment, i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator
from [{0}].GetTransactionSetSegments(@transactionSetId, @includeControlSegments, @revisionId) ts
join [{0}].Interchange i on ts.InterchangeId = i.Id
left join [{0}].Loop l on ts.LoopId = l.Id
order by PositionInInterchange
", _schema), conn);
                cmd.Parameters.AddWithValue("@transactionSetId", transactionSetId);
                cmd.Parameters.AddWithValue("@includeControlSegments", includeControlSegments);
                cmd.Parameters.AddWithValue("@revisionId", revisionId);

                conn.Open();
                var reader = cmd.ExecuteReader();

                List<RepoSegment<T>> s = new List<RepoSegment<T>>();
                while (reader.Read())
                {
                    s.Add(RepoSegmentFromReader(reader));
                }
                reader.Close();

                return s;
            }
        }

        /// <summary>
        /// This will affectively unbundle the transaction from the rest of the transaction set and show you segments related to that loopId.
        /// </summary>
        /// <param name="loopId">The loopId for retrieving it's ancestor and descendant segments</param>
        /// <param name="revisionId">Use 0 for the original version and Int32.MaxValue for the latest version</param>
        /// <param name="includeControlSegments">This will include the ISA, GS, GE and IEA segments</param>
        /// <returns></returns>
        public List<RepoSegment<T>> GetTransactionSegments(T loopId, T revisionId, bool includeControlSegments = false)
        {
            using (var conn = new SqlConnection(_dsn))
            {
                SqlCommand cmd = new SqlCommand(string.Format(@"
select ts.InterchangeId, ts.FunctionalGroupId, ts.TransactionSetId, ts.ParentLoopId, ts.LoopId, ts.RevisionId, ts.Deleted,
    ts.PositionInInterchange, l.SpecLoopId, ts.SegmentId, ts.Segment, i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator
from [{0}].GetTransactionSegments(@loopId, @includeControlSegments, @revisionId) ts
join [{0}].Interchange i on ts.InterchangeId = i.Id
left join [{0}].Loop l on ts.LoopId = l.Id
order by PositionInInterchange", _schema), conn);
                cmd.Parameters.AddWithValue("@loopId", loopId);
                cmd.Parameters.AddWithValue("@includeControlSegments", includeControlSegments);
                cmd.Parameters.AddWithValue("@revisionId", revisionId);

                conn.Open();
                var reader = cmd.ExecuteReader();

                List<RepoSegment<T>> s = new List<RepoSegment<T>>();
                while (reader.Read())
                {
                    s.Add(RepoSegmentFromReader(reader));
                }
                reader.Close();

                return s;
            }
        }

        private RepoTransactionSet<T> RepoTransactionSetFromReader(SqlDataReader reader)
        {
            RepoTransactionSet<T> set = new RepoTransactionSet<T>(
                Convert.ToChar(reader["SegmentTerminator"]),
                Convert.ToChar(reader["ElementSeparator"]),
                Convert.ToChar(reader["ComponentSeparator"]));

            set.TransactionSetId = ConvertT(reader["Id"]);
            set.InterchangeId = ConvertT(reader["InterchangeId"]);
            set.SenderId = Convert.ToString(reader["SenderId"]);
            set.ReceiverId = Convert.ToString(reader["ReceiverId"]);
            set.InterchangeControlNumber = Convert.ToString(reader["InterchangeControlNumber"]);
            if (!reader.IsDBNull(reader.GetOrdinal("InterchangeDate")))
                set.InterchangeDate = Convert.ToDateTime(reader["InterchangeDate"]);
            
            set.FunctionalGroupId = ConvertT(reader["FunctionalGroupId"]);
            set.FunctionalIdCode = Convert.ToString(reader["FunctionalIdCode"]);
            set.FunctionalGroupControlNumber = Convert.ToString(reader["FunctionalGroupControlNumber"]);
            set.Version = Convert.ToString(reader["Version"]);
            
            set.TransactionSetCode = Convert.ToString(reader["TransactionSetCode"]);
            set.ControlNumber = Convert.ToString(reader["ControlNumber"]);
            if (!reader.IsDBNull(reader.GetOrdinal("ImplementationConventionRef")))
                set.ImplementationConventionRef = Convert.ToString(reader["ImplementationConventionRef"]);

            return set;
        }

        public List<RepoTransactionSet<T>> GetTransactionSets(RepoTransactionSetSearchCriteria<T> criteria)
        {
            string sql = string.Format(@"
select ts.Id, ts.InterchangeId, i.SenderId, i.ReceiverId, i.ControlNumber as InterchangeControlNumber, i.[Date] as InterchangeDate, i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator, ts.FunctionalGroupId, fg.FunctionalIdCode, fg.ControlNumber as FunctionalGroupControlNumber, fg.[Version], ts.IdentifierCode as TransactionSetCode, ts.ControlNumber, ts.ImplementationConventionRef
from [{0}].TransactionSet ts
join [{0}].Interchange i on ts.InterchangeId = i.Id
join [{0}].FunctionalGroup fg on ts.FunctionalGroupId = fg.Id
where ts.InterchangeId = isnull(@interchangeId, ts.InterchangeId)
  and i.SenderId = isnull(@senderId,i.SenderId)
  and i.ReceiverId = isnull(@receiverId,i.ReceiverId)
  and i.ControlNumber = isnull(@interchangeControlNumber, i.ControlNumber)
  and i.[Date] >= isnull(@interchangeMinDate,i.[Date])
  and i.[Date] <= isnull(@interchangeMaxDate,i.[Date])
  and ts.FunctionalGroupId = isnull(@functionGroupId, ts.FunctionalGroupId)
  and fg.ControlNumber = isnull(@functionGroupControlNumber, fg.ControlNumber)
  and fg.[Version] like isnull('%' + @versionPattern + '%',fg.[Version])
  and ts.Id = isnull(@transactionSetId, ts.Id)
  and ts.IdentifierCode = isnull(@transactionSetCode, ts.IdentifierCode)
  and ts.ControlNumber = isnull(@transactionSetControlNumber, ts.ControlNumber)
", _schema);
            using (var conn = new SqlConnection(_dsn))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@interchangeId", (object)criteria.InterchangeId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@senderId", (object)criteria.SenderId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@receiverId", (object)criteria.ReceiverId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@interchangeControlNumber", (object)criteria.InterchangeControlNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@interchangeMinDate", (object)criteria.InterchangeMinDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@interchangeMaxDate", (object)criteria.InterchangeMaxDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@functionGroupId", (object)criteria.FunctionalGroupId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@functionGroupControlNumber", (object)criteria.FunctionalGroupControlNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@versionPattern", (object)criteria.VersionPattern ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@transactionSetId", (object)criteria.TransactionSetId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@transactionSetCode", (object)criteria.TransactionSetCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@transactionSetControlNumber", (object)criteria.TransactionSetControlNumber ?? DBNull.Value);
                conn.Open();
                var reader = cmd.ExecuteReader();

                List<RepoTransactionSet<T>> s = new List<RepoTransactionSet<T>>();
                while (reader.Read())
                {
                    s.Add(RepoTransactionSetFromReader(reader));
                }
                reader.Close();

                return s;
            }
        }
    }
}
