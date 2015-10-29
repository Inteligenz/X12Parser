using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace OopFactory.X12.Repositories
{
	[Obsolete("Use OopFactory.X12.Sql library and namespace")]
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
            if (typeof(T) == typeof(Guid) && val == null)
                return (T)(object)Guid.Empty;
            else if (typeof(T) == typeof(Guid))
                return (T)(object)Guid.Parse(val.ToString());
            else if ((typeof(T) == typeof(long?) || typeof(T) == typeof(int?)) && val == null)
                return (T)(object)0;
            else if (typeof(T) == typeof(long))
                return (T)(object)Convert.ToInt64(val);
            else
                return (T)(object)Convert.ToInt32(val);
        }

        private RepoSegment<T> RepoSegmentFromReader(SqlDataReader reader)
        {
            RepoSegment<T> segment = new RepoSegment<T>(Convert.ToString(reader["Segment"]), Convert.ToChar(reader["SegmentTerminator"]),Convert.ToChar(reader["ElementSeparator"]), Convert.ToChar(reader["ComponentSeparator"]) )
            {
                InterchangeId = ConvertT(reader["InterchangeId"]),
                PositionInInterchange = Convert.ToInt32(reader["PositionInInterchange"]),
                RevisionId = Convert.ToInt32(reader["RevisionId"]),
                Deleted = Convert.ToBoolean(reader["Deleted"]),
                SpecLoopId = Convert.ToString(reader["SpecLoopId"])
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
        public List<RepoSegment<T>> GetTransactionSetSegments(T transactionSetId, int revisionId, bool includeControlSegments = false)
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
        public List<RepoSegment<T>> GetTransactionSegments(T loopId, int revisionId, bool includeControlSegments = false)
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

        private RepoLoop<T> RepoLoopFromReader(SqlDataReader reader)
        {
            var loop = new RepoLoop<T>(Convert.ToString(reader["Segment"]), Convert.ToChar(reader["SegmentTerminator"]), Convert.ToChar(reader["ElementSeparator"]), Convert.ToChar(reader["ComponentSeparator"]))
            {
                LoopId = ConvertT(reader["Id"]),
                InterchangeId = ConvertT(reader["InterchangeId"]),
                TransactionSetId = ConvertT(reader["TransactionSetId"]),
                TransactionSetCode = Convert.ToString(reader["TransactionSetCode"]),
                SpecLoopId = Convert.ToString(reader["SpecLoopId"]),
                LevelId = Convert.ToString(reader["LevelId"]),
                LevelCode = Convert.ToString(reader["LevelCode"]),
                StartingSegmentId = Convert.ToString(reader["StartingSegmentId"]),
                EntityIdentifierCode = Convert.ToString(reader["EntityIdentifierCode"]),
                RevisionId = Convert.ToInt32(reader["RevisionId"]),
                PositionInInterchange = Convert.ToInt32(reader["PositionInInterchange"])
            };
            if (!reader.IsDBNull(reader.GetOrdinal("ParentLoopId")))
                loop.ParentLoopId = ConvertT(reader["ParentLoopId"]);
            return loop;
        }

        public List<RepoLoop<T>> GetLoops(RepoLoopSearchCriteria<T> criteria)
        {
            string sql = string.Format(@"
select l.Id, l.ParentLoopId, l.InterchangeId, l.TransactionSetId, l.TransactionSetCode, 
  l.SpecLoopId, l.LevelId, l.LevelCode, l.StartingSegmentId, l.EntityIdentifierCode,
  s1.RevisionId, s1.PositionInInterchange, s1.Segment, 
  i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator
from [{0}].[Loop] l
join [{0}].Interchange i on l.InterchangeId = i.Id
join [{0}].Segment s1 on l.Id = s1.LoopId
where s1.Deleted = 0
and s1.RevisionId = (select max(RevisionId) 
                    from [{0}].Segment s2 
                    where s1.InterchangeId = s2.InterchangeId 
                      and s1.PositionInInterchange = s2.PositionInInterchange)
and l.Id = isnull(@loopId,l.Id)
and isnull(l.ParentLoopId,0) = coalesce(@parentLoopId,l.ParentLoopId,0)
and l.InterchangeId = isnull(@interchangeId,l.InterchangeId)
and l.TransactionSetId = isnull(@transactionSetId,l.TransactionSetId)
and l.TransactionSetCode = isnull(@transactionSetCode, l.TransactionSetCode)
and isnull(l.SpecLoopId,'') = coalesce(@specLoopId, l.SpecLoopId,'')
and isnull(l.LevelId,'') = coalesce(@levelId, l.LevelId,'')
and isnull(l.LevelCode,'') = coalesce(@levelCode, l.LevelCode,'')
and l.StartingSegmentId = isnull(@startingSegmentId,l.StartingSegmentId)
and isnull(l.EntityIdentifierCode,'') = coalesce(@entityIdentifierCode, l.EntityIdentifierCode,'')
", _schema);

            using (var conn = new SqlConnection(_dsn))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@loopId", (object)criteria.LoopId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@parentLoopId", (object)criteria.ParentLoopId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@interchangeId", (object)criteria.InterchangeId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@transactionSetId", (object)criteria.TransactionSetId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@transactionSetCode", (object)criteria.TransactionSetCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@specLoopId", (object)criteria.SpecLoopId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@levelId", (object)criteria.LevelId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@levelCode", (object)criteria.LevelCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@startingSegmentId", (object)criteria.StartingSegmentId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@entityIdentifierCode", (object)criteria.EntityIdentifierCode ?? DBNull.Value);

                var list = new List<RepoLoop<T>>();

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(RepoLoopFromReader(reader));
                    }                    
                }
                return list;
            }
        }

        private RepoEntity<T> RepoEntityFromReader(SqlDataReader reader)
        {
            var entity = new RepoEntity<T>();
            entity.EntityId = ConvertT(reader["EntityId"]);
            entity.EntityIdentifierCode = Convert.ToString(reader["EntityIdentifierCode"]);
            entity.EntityIdentifier = Convert.ToString(reader["EntityIdentifier"]);
            entity.InterchangeId = ConvertT(reader["InterchangeId"]);
            entity.TransactionSetId = ConvertT(reader["TransactionSetId"]);
            entity.TransactionSetCode = Convert.ToString(reader["TransactionSetCode"]);
            entity.ParentLoopId = ConvertT(reader["ParentLoopId"]);
            entity.SpecLoopId = Convert.ToString(reader["SpecLoopId"]);
            entity.StartingSegmentId = Convert.ToString(reader["StartingSegmentId"]);
            entity.Name = Convert.ToString(reader["Name"]);
            entity.LastName = Convert.ToString(reader["LastName"]);
            entity.FirstName = Convert.ToString(reader["FirstName"]);
            entity.MiddleName = Convert.ToString(reader["MiddleName"]);
            entity.NamePrefix = Convert.ToString(reader["NamePrefix"]);
            entity.NameSuffix = Convert.ToString(reader["NameSuffix"]);
            entity.IdQualifier = Convert.ToString(reader["IdQualifier"]);
            entity.Identification = Convert.ToString(reader["Identification"]);
            entity.Ssn = Convert.ToString(reader["Ssn"]);
            entity.Npi = Convert.ToString(reader["Npi"]);
            entity.TelephoneNumber = Convert.ToString(reader["TelephoneNumber"]);
            entity.AddressLine1 = Convert.ToString(reader["AddressLine1"]);
            entity.AddressLine2 = Convert.ToString(reader["AddressLine2"]);
            entity.City = Convert.ToString(reader["City"]);
            entity.StateCode = Convert.ToString(reader["StateCode"]);
            entity.PostalCode = Convert.ToString(reader["PostalCode"]);
            entity.County = Convert.ToString(reader["County"]);
            entity.CountryCode = Convert.ToString(reader["CountryCode"]);
            entity.Gender = Convert.ToString(reader["Gender"]);

            if (!reader.IsDBNull(reader.GetOrdinal("IsPerson")))
                entity.IsPerson = Convert.ToBoolean(reader["IsPerson"]);

            if (!reader.IsDBNull(reader.GetOrdinal("DateOfBirth")))
            {
                try
                {
                    entity.DateOfBirth = DateTime.ParseExact(Convert.ToString(reader["DateOfBirth"]),"yyyyMMdd",null);
                }
                catch (FormatException)
                {
                    System.Diagnostics.Trace.TraceWarning("Could not parse date of birth {1} to a date time for entity ID: {0}", entity.EntityId, reader["DateOfBirth"]);
                }
            }

            return entity;
        }

        private string GetSqlInString(string[] filters)
        {
            List<string> quotedValues = new List<string>();
            foreach (var filter in filters)
                quotedValues.Add(string.Format("'{0}'", filter.Replace("'","''")));
            return string.Join(",",quotedValues);
        }

        public List<RepoEntity<T>> GetEntities(RepoEntitySearchCriteria<T> criteria)
        {
            StringBuilder sql = new StringBuilder(string.Format("select * from [{0}].Entity where 1=1 ", _schema));

            if (!string.IsNullOrEmpty(criteria.EntityIdentifierCodes))
            {
                var codes = GetSqlInString( criteria.EntityIdentifierCodes.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries));
                
                sql.AppendFormat(" and EntityIdentifierCode in ({0})", codes);
            }

            if (!string.IsNullOrEmpty(criteria.EntityIdentifierContains))
                sql.AppendFormat(" and EntityIdentifier like '%{0}%'", criteria.EntityIdentifierContains);

            if (criteria.InterchangeId.HasValue)
                sql.AppendFormat(" and InterchangeId = '{0}'", criteria.InterchangeId.Value);

            if (criteria.TransactionSetId.HasValue)
                sql.AppendFormat(" and TransactionSetId = '{0}'", criteria.TransactionSetId.Value);

            if (!string.IsNullOrEmpty(criteria.TransactionSetCode))
                sql.AppendFormat(" and TransactionSetCode = '{0}'", criteria.TransactionSetCode);

            if (criteria.ParentLoopId.HasValue)
                sql.AppendFormat(" and ParentLoopId = '{0}'", criteria.ParentLoopId.Value);

            if (!string.IsNullOrEmpty(criteria.SpecLoopId))
                sql.AppendFormat(" and SpecLoopId = '{0}'", criteria.SpecLoopId);

            if (!string.IsNullOrEmpty(criteria.StartingSegmentId))
                sql.AppendFormat(" and StartingSegmentId = '{0}'", criteria.StartingSegmentId);

            if (!string.IsNullOrEmpty(criteria.NameContains))
                sql.AppendFormat(" and Name like '%{0}%'", criteria.NameContains);

            if (criteria.IsPerson.HasValue)
                sql.AppendFormat(" and IsPerson = {0}", criteria.IsPerson.Value ? "1" : "0");

            if (!string.IsNullOrEmpty(criteria.LastNameStartsWith))
                sql.AppendFormat(" and LastName like '{0}%'", criteria.LastNameStartsWith);

            if (!string.IsNullOrEmpty(criteria.FirstNameContains))
                sql.AppendFormat(" and FirstName like '%{0}%'", criteria.FirstNameContains);

            if (!string.IsNullOrEmpty(criteria.IdQualifier))
                sql.AppendFormat(" and IdQualifier = '{0}'", criteria.IdQualifier);

            if (!string.IsNullOrEmpty(criteria.Identification))
                sql.AppendFormat(" and Identification = '{0}'", criteria.Identification);

            if (!string.IsNullOrEmpty(criteria.Ssn))
                sql.AppendFormat(" and Ssn = '{0}'", criteria.Ssn);

            if (!string.IsNullOrEmpty(criteria.Npi))
                sql.AppendFormat(" and Npi = '{0}'", criteria.Npi);

            if (!string.IsNullOrEmpty(criteria.City))
                sql.AppendFormat(" and City = '{0}'", criteria.City);

            if (!string.IsNullOrEmpty(criteria.StateCode))
                sql.AppendFormat(" and StateCode = '{0}'", criteria.StateCode);

            if (!string.IsNullOrEmpty(criteria.PostalCode))
                sql.AppendFormat(" and PostalCode = '{0}'", criteria.PostalCode);

            if (!string.IsNullOrEmpty(criteria.County))
                sql.AppendFormat(" and County = '{0}'", criteria.County);

            if (!string.IsNullOrEmpty(criteria.CountryCode))
                sql.AppendFormat(" and CountryCode = '{0}'", criteria.CountryCode);

            if (criteria.DateOfBirthOn.HasValue)
                sql.AppendFormat(" and DateOfBirth = '{0:yyyyMMdd}'", criteria.DateOfBirthOn);

            if (criteria.DateOfBirthOnOrAfter.HasValue)
                sql.AppendFormat(" and DateOfBirth >= '{0:yyyyMMdd}'", criteria.DateOfBirthOnOrAfter);

            if (criteria.DateOfBirthOnOrBefore.HasValue)
                sql.AppendFormat(" and DateOfBirth <= '{0:yyyyMMdd}'", criteria.DateOfBirthOnOrBefore);

            if (!string.IsNullOrEmpty(criteria.Gender))
                sql.AppendFormat(" and Gender = '{0}'", criteria.Gender);

            using (var conn = new SqlConnection(_dsn))
            {
                var list = new List<RepoEntity<T>>();
                conn.Open();
                using (var reader = new SqlCommand(sql.ToString(), conn).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(RepoEntityFromReader(reader));
                    }
                }

                return list;
            }
        }
    }
}
