using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace OopFactory.X12.Repositories
{
    public class SqlReadOnlyTransactionRepository
    {
        protected readonly string _dsn;
        protected readonly string _schema;

        public SqlReadOnlyTransactionRepository(string dsn, string schema = "dbo")
        {
            _dsn = dsn;
            _schema = schema;
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
    }
}
