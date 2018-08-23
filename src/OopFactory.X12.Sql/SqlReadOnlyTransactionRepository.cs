namespace OopFactory.X12.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Text;

    /// <summary>
    /// Collection of readonly methods for retrieving data from database into X12 models
    /// </summary>
    public class SqlReadOnlyTransactionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlReadOnlyTransactionRepository"/> class
        /// </summary>
        /// <param name="dsn">Data source name</param>
        /// <param name="identityType">Data type used for conversions</param>
        /// <param name="schema">Database schema name</param>
        public SqlReadOnlyTransactionRepository(string dsn, Type identityType, string schema = "dbo")
        {
            this.Dsn = dsn;
            this.Schema = schema;
            this.IdentityType = identityType;
            this.DefaultIdentityTypeValue = identityType.GetDefaultValue();
        }

        /// <summary>
        /// Gets the data source name
        /// </summary>
        protected string Dsn { get; }

        /// <summary>
        /// Gets the database schema name
        /// </summary>
        protected string Schema { get; }

        /// <summary>
        /// Gets the data type used in conversions
        /// </summary>
        protected Type IdentityType { get; }

        /// <summary>
        /// Gets the default value for <see cref="IdentityType"/>
        /// </summary>
        protected object DefaultIdentityTypeValue { get; }

        /// <summary>
        ///     Retrieves all the segments within a transaction
        /// </summary>
        /// <param name="transactionSetId">Identifier of the TransactionSet</param>
        /// <param name="revisionId">Use 0 for the original version Int32.MaxValue when you want the latest revision</param>
        /// <param name="includeControlSegments">This will include the ISA, GS, GE and IEA segments</param>
        /// <returns>List of <see cref="RepoSegment"/> objects from TransactionSet</returns>
        public List<RepoSegment> GetTransactionSetSegments(
            object transactionSetId,
            int revisionId,
            bool includeControlSegments = false)
        {
            using (var conn = new SqlConnection(this.Dsn))
            {
                var cmd = new SqlCommand(
                    string.Format(
@"select ts.InterchangeId, ts.FunctionalGroupId, ts.TransactionSetId, ts.ParentLoopId, ts.LoopId, ts.RevisionId, ts.Deleted,
ts.PositionInInterchange, l.SpecLoopId, ts.SegmentId, ts.Segment, i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator
from [{0}].GetTransactionSetSegments(@transactionSetId, @includeControlSegments, @revisionId) ts
join [{0}].Interchange i on ts.InterchangeId = i.Id
left join [{0}].Loop l on ts.LoopId = l.Id
order by PositionInInterchange",
                    this.Schema),
                    conn);

                cmd.Parameters.AddWithValue("@transactionSetId", transactionSetId);
                cmd.Parameters.AddWithValue("@includeControlSegments", includeControlSegments);
                cmd.Parameters.AddWithValue("@revisionId", revisionId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                var s = new List<RepoSegment>();
                while (reader.Read())
                {
                    s.Add(this.RepoSegmentFromReader(reader));
                }

                return s;
            }
        }

        /// <summary>
        ///     This will affectively unbundle the transaction from the rest of the transaction set and show you segments related
        ///     to that loopId.
        /// </summary>
        /// <param name="loopId">The loopId for retrieving it's ancestor and descendant segments</param>
        /// <param name="revisionId">Use 0 for the original version and Int32.MaxValue for the latest version</param>
        /// <param name="includeControlSegments">This will include the ISA, GS, GE and IEA segments</param>
        /// <returns>List of <see cref="RepoSegment"/> object from Transaction</returns>
        public List<RepoSegment> GetTransactionSegments(object loopId, int revisionId, bool includeControlSegments = false)
        {
            using (var conn = new SqlConnection(this.Dsn))
            {
                var cmd = new SqlCommand(
                    string.Format(
@"select ts.InterchangeId, ts.FunctionalGroupId, ts.TransactionSetId, ts.ParentLoopId, ts.LoopId, ts.RevisionId, ts.Deleted,
ts.PositionInInterchange, l.SpecLoopId, ts.SegmentId, ts.Segment, i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator
from [{0}].GetTransactionSegments(@loopId, @includeControlSegments, @revisionId) ts
join [{0}].Interchange i on ts.InterchangeId = i.Id
left join [{0}].Loop l on ts.LoopId = l.Id
order by PositionInInterchange",
                    this.Schema),
                    conn);

                cmd.Parameters.AddWithValue("@loopId", loopId);
                cmd.Parameters.AddWithValue("@includeControlSegments", includeControlSegments);
                cmd.Parameters.AddWithValue("@revisionId", revisionId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                var s = new List<RepoSegment>();
                while (reader.Read())
                {
                    s.Add(this.RepoSegmentFromReader(reader));
                }

                return s;
            }
        }

        /// <summary>
        ///   Returns collection of <see cref="RepoTransactionSet"/> from database
        /// </summary>
        /// <param name="criteria">Search criteria for constraining results</param>
        /// <returns>List of <see cref="RepoTransactionSet"/> found within criteria from database</returns>
        public List<RepoTransactionSet> GetTransactionSets(RepoTransactionSetSearchCriteria criteria)
        {
            var sql = string.Format(
@"select ts.Id, ts.InterchangeId, i.SenderId, i.ReceiverId, i.ControlNumber as InterchangeControlNumber, i.[Date] as InterchangeDate, i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator, ts.FunctionalGroupId, fg.FunctionalIdCode, fg.ControlNumber as FunctionalGroupControlNumber, fg.[Version], ts.IdentifierCode as TransactionSetCode, ts.ControlNumber, ts.ImplementationConventionRef
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
  and ts.ControlNumber = isnull(@transactionSetControlNumber, ts.ControlNumber)",
                this.Schema);

            using (var conn = new SqlConnection(this.Dsn))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@interchangeId", criteria.InterchangeId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@senderId", (object)criteria.SenderId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@receiverId", (object)criteria.ReceiverId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@interchangeControlNumber", (object)criteria.InterchangeControlNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@interchangeMinDate", (object)criteria.InterchangeMinDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@interchangeMaxDate", (object)criteria.InterchangeMaxDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@functionGroupId", criteria.FunctionalGroupId ?? DBNull.Value);
                cmd.Parameters.AddWithValue(
                    "@functionGroupControlNumber",
                    (object)criteria.FunctionalGroupControlNumber ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@versionPattern", (object)criteria.VersionPattern ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@transactionSetId", criteria.TransactionSetId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@transactionSetCode", (object)criteria.TransactionSetCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue(
                    "@transactionSetControlNumber",
                    (object)criteria.TransactionSetControlNumber ?? DBNull.Value);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                var s = new List<RepoTransactionSet>();

                while (reader.Read())
                {
                    s.Add(this.RepoTransactionSetFromReader(reader));
                }

                return s;
            }
        }

        /// <summary>
        ///   Returns collection of <see cref="RepoLoop"/> from database
        /// </summary>
        /// <param name="criteria">Search criteria for constraining results</param>
        /// <returns>List of <see cref="RepoLoop"/> found within criteria from database</returns>
        public List<RepoLoop> GetLoops(RepoLoopSearchCriteria criteria)
        {
            var sql = string.Format(
@"select l.Id, l.ParentLoopId, l.InterchangeId, l.TransactionSetId, l.TransactionSetCode, 
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
and isnull(l.EntityIdentifierCode,'') = coalesce(@entityIdentifierCode, l.EntityIdentifierCode,'')",
                this.Schema);

            using (var conn = new SqlConnection(this.Dsn))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@loopId", criteria.LoopId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@parentLoopId", criteria.ParentLoopId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@interchangeId", criteria.InterchangeId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@transactionSetId", criteria.TransactionSetId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@transactionSetCode", (object)criteria.TransactionSetCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@specLoopId", (object)criteria.SpecLoopId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@levelId", (object)criteria.LevelId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@levelCode", (object)criteria.LevelCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@startingSegmentId", (object)criteria.StartingSegmentId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@entityIdentifierCode", (object)criteria.EntityIdentifierCode ?? DBNull.Value);

                var list = new List<RepoLoop>();
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(this.RepoLoopFromReader(reader));
                    }
                }

                return list;
            }
        }

        /// <summary>
        ///   Returns collection of <see cref="RepoEntity"/> from database
        /// </summary>
        /// <param name="criteria">Search criteria for constraining results</param>
        /// <returns>List of <see cref="RepoEntity"/> found within criteria from database</returns>
        public List<RepoEntity> GetEntities(RepoEntitySearchCriteria criteria)
        {
            var sql = new StringBuilder($"select * from [{this.Schema}].Entity where 1=1 ");

            if (!string.IsNullOrEmpty(criteria.EntityIdentifierCodes))
            {
                var codes =
                    this.GetSqlInString(criteria.EntityIdentifierCodes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                sql.AppendFormat(" and EntityIdentifierCode in ({0})", codes);
            }

            if (!string.IsNullOrEmpty(criteria.EntityIdentifierContains))
            {
                sql.AppendFormat(" and EntityIdentifier like '%{0}%'", criteria.EntityIdentifierContains);
            }

            if (criteria.InterchangeId != this.DefaultIdentityTypeValue)
            {
                sql.AppendFormat(" and InterchangeId = '{0}'", criteria.InterchangeId);
            }

            if (criteria.TransactionSetId != this.DefaultIdentityTypeValue)
            {
                sql.AppendFormat(" and TransactionSetId = '{0}'", criteria.TransactionSetId);
            }

            if (!string.IsNullOrEmpty(criteria.TransactionSetCode))
            {
                sql.AppendFormat(" and TransactionSetCode = '{0}'", criteria.TransactionSetCode);
            }

            if (criteria.ParentLoopId != this.DefaultIdentityTypeValue)
            {
                sql.AppendFormat(" and ParentLoopId = '{0}'", criteria.ParentLoopId);
            }

            if (!string.IsNullOrEmpty(criteria.SpecLoopId))
            {
                sql.AppendFormat(" and SpecLoopId = '{0}'", criteria.SpecLoopId);
            }

            if (!string.IsNullOrEmpty(criteria.StartingSegmentId))
            {
                sql.AppendFormat(" and StartingSegmentId = '{0}'", criteria.StartingSegmentId);
            }

            if (!string.IsNullOrEmpty(criteria.NameContains))
            {
                sql.AppendFormat(" and Name like '%{0}%'", criteria.NameContains);
            }

            if (criteria.IsPerson.HasValue)
            {
                sql.AppendFormat(" and IsPerson = {0}", criteria.IsPerson.Value ? "1" : "0");
            }

            if (!string.IsNullOrEmpty(criteria.LastNameStartsWith))
            {
                sql.AppendFormat(" and LastName like '{0}%'", criteria.LastNameStartsWith);
            }

            if (!string.IsNullOrEmpty(criteria.FirstNameContains))
            {
                sql.AppendFormat(" and FirstName like '%{0}%'", criteria.FirstNameContains);
            }

            if (!string.IsNullOrEmpty(criteria.IdQualifier))
            {
                sql.AppendFormat(" and IdQualifier = '{0}'", criteria.IdQualifier);
            }

            if (!string.IsNullOrEmpty(criteria.Identification))
            {
                sql.AppendFormat(" and Identification = '{0}'", criteria.Identification);
            }

            if (!string.IsNullOrEmpty(criteria.Ssn))
            {
                sql.AppendFormat(" and Ssn = '{0}'", criteria.Ssn);
            }

            if (!string.IsNullOrEmpty(criteria.Npi))
            {
                sql.AppendFormat(" and Npi = '{0}'", criteria.Npi);
            }

            if (!string.IsNullOrEmpty(criteria.City))
            {
                sql.AppendFormat(" and City = '{0}'", criteria.City);
            }

            if (!string.IsNullOrEmpty(criteria.StateCode))
            {
                sql.AppendFormat(" and StateCode = '{0}'", criteria.StateCode);
            }

            if (!string.IsNullOrEmpty(criteria.PostalCode))
            {
                sql.AppendFormat(" and PostalCode = '{0}'", criteria.PostalCode);
            }

            if (!string.IsNullOrEmpty(criteria.County))
            {
                sql.AppendFormat(" and County = '{0}'", criteria.County);
            }

            if (!string.IsNullOrEmpty(criteria.CountryCode))
            {
                sql.AppendFormat(" and CountryCode = '{0}'", criteria.CountryCode);
            }

            if (criteria.DateOfBirthOn.HasValue)
            {
                sql.AppendFormat(" and DateOfBirth = '{0:yyyyMMdd}'", criteria.DateOfBirthOn);
            }

            if (criteria.DateOfBirthOnOrAfter.HasValue)
            {
                sql.AppendFormat(" and DateOfBirth >= '{0:yyyyMMdd}'", criteria.DateOfBirthOnOrAfter);
            }

            if (criteria.DateOfBirthOnOrBefore.HasValue)
            {
                sql.AppendFormat(" and DateOfBirth <= '{0:yyyyMMdd}'", criteria.DateOfBirthOnOrBefore);
            }

            if (!string.IsNullOrEmpty(criteria.Gender))
            {
                sql.AppendFormat(" and Gender = '{0}'", criteria.Gender);
            }

            using (var conn = new SqlConnection(this.Dsn))
            {
                var list = new List<RepoEntity>();
                conn.Open();
                using (var reader = new SqlCommand(sql.ToString(), conn).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(this.RepoEntityFromReader(reader));
                    }
                }

                return list;
            }
        }

        /// <summary>
        /// Converts the value to <see cref="IdentityType"/>
        /// </summary>
        /// <param name="val">Object to be converted</param>
        /// <returns>Boxed object of <see cref="IdentityType"/></returns>
        protected object ConvertT(object val)
        {
            if (this.IdentityType == typeof(Guid) && val == null)
            {
                return Guid.Empty;
            }
            else if (this.IdentityType == typeof(Guid))
            {
                return Guid.Parse(val.ToString());
            }
            else if ((this.IdentityType == typeof(long?) || this.IdentityType == typeof(int?))
                && val == null)
            {
                return 0;
            }
            else if (this.IdentityType == typeof(long))
            {
                return Convert.ToInt64(val);
            }
            else
            {
                return Convert.ToInt32(val);
            }
        }

        private RepoSegment RepoSegmentFromReader(SqlDataReader reader)
        {
            var segment = new RepoSegment(
                Convert.ToString(reader["Segment"]),
                Convert.ToChar(reader["SegmentTerminator"]),
                Convert.ToChar(reader["ElementSeparator"]),
                Convert.ToChar(reader["ComponentSeparator"]))
                          {
                              InterchangeId = this.ConvertT(reader["InterchangeId"]),
                              PositionInInterchange = Convert.ToInt32(reader["PositionInInterchange"]),
                              RevisionId = Convert.ToInt32(reader["RevisionId"]),
                              Deleted = Convert.ToBoolean(reader["Deleted"]),
                              SpecLoopId = Convert.ToString(reader["SpecLoopId"])
                          };

            if (!reader.IsDBNull(reader.GetOrdinal("FunctionalGroupId")))
            {
                segment.FunctionalGroupId = this.ConvertT(reader["FunctionalGroupId"]);
            }

            if (!reader.IsDBNull(reader.GetOrdinal("TransactionSetId")))
            {
                segment.TransactionSetId = this.ConvertT(reader["TransactionSetId"]);
            }

            if (!reader.IsDBNull(reader.GetOrdinal("ParentLoopId")))
            {
                segment.ParentLoopId = this.ConvertT(reader["ParentLoopId"]);
            }

            if (!reader.IsDBNull(reader.GetOrdinal("LoopId")))
            {
                segment.LoopId = this.ConvertT(reader["LoopId"]);
            }

            return segment;
        }

        private RepoTransactionSet RepoTransactionSetFromReader(SqlDataReader reader)
        {
            var set = new RepoTransactionSet(
                Convert.ToChar(reader["SegmentTerminator"]),
                Convert.ToChar(reader["ElementSeparator"]),
                Convert.ToChar(reader["ComponentSeparator"]))
            {
                TransactionSetId = this.ConvertT(reader["Id"]),
                InterchangeId = this.ConvertT(reader["InterchangeId"]),
                SenderId = Convert.ToString(reader["SenderId"]),
                ReceiverId = Convert.ToString(reader["ReceiverId"]),
                InterchangeControlNumber = Convert.ToString(reader["InterchangeControlNumber"]),
                FunctionalGroupId = this.ConvertT(reader["FunctionalGroupId"]),
                FunctionalIdCode = Convert.ToString(reader["FunctionalIdCode"]),
                FunctionalGroupControlNumber = Convert.ToString(reader["FunctionalGroupControlNumber"]),
                Version = Convert.ToString(reader["Version"]),
                TransactionSetCode = Convert.ToString(reader["TransactionSetCode"]),
                ControlNumber = Convert.ToString(reader["ControlNumber"])
            };

            if (!reader.IsDBNull(reader.GetOrdinal("InterchangeDate")))
            {
                set.InterchangeDate = Convert.ToDateTime(reader["InterchangeDate"]);
            }

            if (!reader.IsDBNull(reader.GetOrdinal("ImplementationConventionRef")))
            {
                set.ImplementationConventionRef = Convert.ToString(reader["ImplementationConventionRef"]);
            }

            return set;
        }

        private RepoLoop RepoLoopFromReader(SqlDataReader reader)
        {
            var loop = new RepoLoop(
                Convert.ToString(reader["Segment"]),
                Convert.ToChar(reader["SegmentTerminator"]),
                Convert.ToChar(reader["ElementSeparator"]),
                Convert.ToChar(reader["ComponentSeparator"]))
                   {
                       LoopId = this.ConvertT(reader["Id"]),
                       InterchangeId = this.ConvertT(reader["InterchangeId"]),
                       TransactionSetId = this.ConvertT(reader["TransactionSetId"]),
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
            {
                loop.ParentLoopId = this.ConvertT(reader["ParentLoopId"]);
            }

            return loop;
        }

        private RepoEntity RepoEntityFromReader(SqlDataReader reader)
        {
            var entity = new RepoEntity
            {
                EntityId = this.ConvertT(reader["EntityId"]),
                EntityIdentifierCode = Convert.ToString(reader["EntityIdentifierCode"]),
                EntityIdentifier = Convert.ToString(reader["EntityIdentifier"]),
                InterchangeId = this.ConvertT(reader["InterchangeId"]),
                TransactionSetId = this.ConvertT(reader["TransactionSetId"]),
                TransactionSetCode = Convert.ToString(reader["TransactionSetCode"]),
                ParentLoopId = this.ConvertT(reader["ParentLoopId"]),
                SpecLoopId = Convert.ToString(reader["SpecLoopId"]),
                StartingSegmentId = Convert.ToString(reader["StartingSegmentId"]),
                Name = Convert.ToString(reader["Name"]),
                LastName = Convert.ToString(reader["LastName"]),
                FirstName = Convert.ToString(reader["FirstName"]),
                MiddleName = Convert.ToString(reader["MiddleName"]),
                NamePrefix = Convert.ToString(reader["NamePrefix"]),
                NameSuffix = Convert.ToString(reader["NameSuffix"]),
                IdQualifier = Convert.ToString(reader["IdQualifier"]),
                Identification = Convert.ToString(reader["Identification"]),
                Ssn = Convert.ToString(reader["Ssn"]),
                Npi = Convert.ToString(reader["Npi"]),
                TelephoneNumber = Convert.ToString(reader["TelephoneNumber"]),
                AddressLine1 = Convert.ToString(reader["AddressLine1"]),
                AddressLine2 = Convert.ToString(reader["AddressLine2"]),
                City = Convert.ToString(reader["City"]),
                StateCode = Convert.ToString(reader["StateCode"]),
                PostalCode = Convert.ToString(reader["PostalCode"]),
                County = Convert.ToString(reader["County"]),
                CountryCode = Convert.ToString(reader["CountryCode"]),
                Gender = Convert.ToString(reader["Gender"])
            };

            if (!reader.IsDBNull(reader.GetOrdinal("IsPerson")))
            {
                entity.IsPerson = Convert.ToBoolean(reader["IsPerson"]);
            }

            if (!reader.IsDBNull(reader.GetOrdinal("DateOfBirth")))
            {
                try
                {
                    entity.DateOfBirth = DateTime.ParseExact(Convert.ToString(reader["DateOfBirth"]), "yyyyMMdd", null);
                }
                catch (FormatException)
                {
                    Trace.TraceWarning(
                        "Could not parse date of birth {1} to a date time for entity ID: {0}",
                        entity.EntityId,
                        reader["DateOfBirth"]);
                }
            }

            return entity;
        }

        private string GetSqlInString(IEnumerable<string> filters)
        {
            var quotedValues = new List<string>();
            foreach (string filter in filters)
            {
                quotedValues.Add($"'{filter.Replace("'", "''")}'");
            }

            return string.Join(",", quotedValues);
        }
    }
}