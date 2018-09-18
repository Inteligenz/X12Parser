namespace OopFactory.X12.Sql
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Text;

    using OopFactory.X12.Sql.Properties;

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
@"SELECT ts.InterchangeId, ts.FunctionalGroupId, ts.TransactionSetId, ts.ParentLoopId, ts.LoopId, ts.RevisionId, ts.Deleted,
ts.PositionInInterchange, l.SpecLoopId, ts.SegmentId, ts.Segment, i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator
FROM [{0}].GetTransactionSetSegments(@transactionSetId, @includeControlSegments, @revisionId) ts
JOIN [{0}].Interchange i ON ts.InterchangeId = i.Id
LEFT JOIN [{0}].Loop l ON ts.LoopId = l.Id
ORDER BY PositionInInterchange",
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
@"SELECT ts.InterchangeId, ts.FunctionalGroupId, ts.TransactionSetId, ts.ParentLoopId, ts.LoopId, ts.RevisionId, ts.Deleted,
ts.PositionInInterchange, l.SpecLoopId, ts.SegmentId, ts.Segment, i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator
FROM [{0}].GetTransactionSegments(@loopId, @includeControlSegments, @revisionId) ts
JOIN [{0}].Interchange i ON ts.InterchangeId = i.Id
LEFT JOIN [{0}].Loop l ON ts.LoopId = l.Id
ORDER BY PositionInInterchange",
                    this.Schema),
                    conn);

                cmd.Parameters.AddWithValue("@loopId", loopId);
                cmd.Parameters.AddWithValue("@includeControlSegments", includeControlSegments);
                cmd.Parameters.AddWithValue("@revisionId", revisionId);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    var s = new List<RepoSegment>();
                    while (reader.Read())
                    {
                        s.Add(this.RepoSegmentFromReader(reader));
                    }

                    return s;
                }
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
@"SELECT ts.Id, ts.InterchangeId, i.SenderId, i.ReceiverId,
  i.ControlNumber AS InterchangeControlNumber, i.[Date] as InterchangeDate,
  i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator, ts.FunctionalGroupId,
  fg.FunctionalIdCode, fg.ControlNumber AS FunctionalGroupControlNumber, fg.[Version],
  ts.IdentifierCode AS TransactionSetCode, ts.ControlNumber, ts.ImplementationConventionRef
FROM [{0}].TransactionSet ts
JOIN [{0}].Interchange i ON ts.InterchangeId = i.Id
JOIN [{0}].FunctionalGroup fg ON ts.FunctionalGroupId = fg.Id
WHERE ts.InterchangeId = isnull(@interchangeId, ts.InterchangeId)
  AND i.SenderId = isnull(@senderId,i.SenderId)
  AND i.ReceiverId = isnull(@receiverId,i.ReceiverId)
  AND i.ControlNumber = isnull(@interchangeControlNumber, i.ControlNumber)
  AND i.[Date] >= isnull(@interchangeMinDate,i.[Date])
  AND i.[Date] <= isnull(@interchangeMaxDate,i.[Date])
  AND ts.FunctionalGroupId = isnull(@functionGroupId, ts.FunctionalGroupId)
  AND fg.ControlNumber = isnull(@functionGroupControlNumber, fg.ControlNumber)
  AND fg.[Version] like isnull('%' + @versionPattern + '%',fg.[Version])
  AND ts.Id = isnull(@transactionSetId, ts.Id)
  AND ts.IdentifierCode = isnull(@transactionSetCode, ts.IdentifierCode)
  AND ts.ControlNumber = isnull(@transactionSetControlNumber, ts.ControlNumber)",
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
@"SELECT l.Id, l.ParentLoopId, l.InterchangeId, l.TransactionSetId, l.TransactionSetCode, 
  l.SpecLoopId, l.LevelId, l.LevelCode, l.StartingSegmentId, l.EntityIdentifierCode,
  s1.RevisionId, s1.PositionInInterchange, s1.Segment, 
  i.SegmentTerminator, i.ElementSeparator, i.ComponentSeparator
FROM [{0}].[Loop] l
JOIN [{0}].Interchange i ON l.InterchangeId = i.Id
JOIN [{0}].Segment s1 ON l.Id = s1.LoopId
WHERE s1.Deleted = 0
AND s1.RevisionId = (SELECT max(RevisionId) 
                    FROM [{0}].Segment s2 
                    WHERE s1.InterchangeId = s2.InterchangeId 
                      AND s1.PositionInInterchange = s2.PositionInInterchange)
AND l.Id = isnull(@loopId,l.Id)
AND isnull(l.ParentLoopId,0) = coalesce(@parentLoopId,l.ParentLoopId,0)
AND l.InterchangeId = isnull(@interchangeId,l.InterchangeId)
AND l.TransactionSetId = isnull(@transactionSetId,l.TransactionSetId)
AND l.TransactionSetCode = isnull(@transactionSetCode, l.TransactionSetCode)
AND isnull(l.SpecLoopId,'') = coalesce(@specLoopId, l.SpecLoopId,'')
AND isnull(l.LevelId,'') = coalesce(@levelId, l.LevelId,'')
AND isnull(l.LevelCode,'') = coalesce(@levelCode, l.LevelCode,'')
AND l.StartingSegmentId = isnull(@startingSegmentId,l.StartingSegmentId)
AND isnull(l.EntityIdentifierCode,'') = coalesce(@entityIdentifierCode, l.EntityIdentifierCode,'')",
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
            var sql = new StringBuilder($"SELECT * FROM [{this.Schema}].Entity");
            if (criteria != null)
            {
                sql.Append(" WHERE 1=1");

                if (!string.IsNullOrEmpty(criteria.EntityIdentifierCodes))
                {
                    var codes = this.GetSqlInString(
                        criteria.EntityIdentifierCodes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                    sql.AppendFormat(" AND EntityIdentifierCode IN ({0})", codes);
                }

                if (!string.IsNullOrEmpty(criteria.EntityIdentifierContains))
                {
                    sql.AppendFormat(" AND EntityIdentifier LIKE '%{0}%'", criteria.EntityIdentifierContains);
                }

                if (criteria.InterchangeId != this.DefaultIdentityTypeValue)
                {
                    sql.AppendFormat(" AND InterchangeId = '{0}'", criteria.InterchangeId);
                }

                if (criteria.TransactionSetId != this.DefaultIdentityTypeValue)
                {
                    sql.AppendFormat(" AND TransactionSetId = '{0}'", criteria.TransactionSetId);
                }

                if (!string.IsNullOrEmpty(criteria.TransactionSetCode))
                {
                    sql.AppendFormat(" AND TransactionSetCode = '{0}'", criteria.TransactionSetCode);
                }

                if (criteria.ParentLoopId != this.DefaultIdentityTypeValue)
                {
                    sql.AppendFormat(" AND ParentLoopId = '{0}'", criteria.ParentLoopId);
                }

                if (!string.IsNullOrEmpty(criteria.SpecLoopId))
                {
                    sql.AppendFormat(" AND SpecLoopId = '{0}'", criteria.SpecLoopId);
                }

                if (!string.IsNullOrEmpty(criteria.StartingSegmentId))
                {
                    sql.AppendFormat(" AND StartingSegmentId = '{0}'", criteria.StartingSegmentId);
                }

                if (!string.IsNullOrEmpty(criteria.NameContains))
                {
                    sql.AppendFormat(" AND Name LIKE '%{0}%'", criteria.NameContains);
                }

                if (criteria.IsPerson.HasValue)
                {
                    sql.AppendFormat(" AND IsPerson = {0}", criteria.IsPerson.Value ? "1" : "0");
                }

                if (!string.IsNullOrEmpty(criteria.LastNameStartsWith))
                {
                    sql.AppendFormat(" AND LastName LIKE '{0}%'", criteria.LastNameStartsWith);
                }

                if (!string.IsNullOrEmpty(criteria.FirstNameContains))
                {
                    sql.AppendFormat(" AND FirstName LIKE '%{0}%'", criteria.FirstNameContains);
                }

                if (!string.IsNullOrEmpty(criteria.IdQualifier))
                {
                    sql.AppendFormat(" AND IdQualifier = '{0}'", criteria.IdQualifier);
                }

                if (!string.IsNullOrEmpty(criteria.Identification))
                {
                    sql.AppendFormat(" AND Identification = '{0}'", criteria.Identification);
                }

                if (!string.IsNullOrEmpty(criteria.Ssn))
                {
                    sql.AppendFormat(" AND Ssn = '{0}'", criteria.Ssn);
                }

                if (!string.IsNullOrEmpty(criteria.Npi))
                {
                    sql.AppendFormat(" AND Npi = '{0}'", criteria.Npi);
                }

                if (!string.IsNullOrEmpty(criteria.City))
                {
                    sql.AppendFormat(" AND City = '{0}'", criteria.City);
                }

                if (!string.IsNullOrEmpty(criteria.StateCode))
                {
                    sql.AppendFormat(" AND StateCode = '{0}'", criteria.StateCode);
                }

                if (!string.IsNullOrEmpty(criteria.PostalCode))
                {
                    sql.AppendFormat(" AND PostalCode = '{0}'", criteria.PostalCode);
                }

                if (!string.IsNullOrEmpty(criteria.County))
                {
                    sql.AppendFormat(" AND County = '{0}'", criteria.County);
                }

                if (!string.IsNullOrEmpty(criteria.CountryCode))
                {
                    sql.AppendFormat(" AND CountryCode = '{0}'", criteria.CountryCode);
                }

                if (criteria.DateOfBirthOn.HasValue)
                {
                    sql.AppendFormat(" AND DateOfBirth = '{0:yyyyMMdd}'", criteria.DateOfBirthOn);
                }

                if (criteria.DateOfBirthOnOrAfter.HasValue)
                {
                    sql.AppendFormat(" AND DateOfBirth >= '{0:yyyyMMdd}'", criteria.DateOfBirthOnOrAfter);
                }

                if (criteria.DateOfBirthOnOrBefore.HasValue)
                {
                    sql.AppendFormat(" AND DateOfBirth <= '{0:yyyyMMdd}'", criteria.DateOfBirthOnOrBefore);
                }

                if (!string.IsNullOrEmpty(criteria.Gender))
                {
                    sql.AppendFormat(" AND Gender = '{0}'", criteria.Gender);
                }
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

            if (this.IdentityType == typeof(Guid))
            {
                return Guid.Parse(val.ToString());
            }

            if ((this.IdentityType == typeof(long?) || this.IdentityType == typeof(int?))
                && val == null)
            {
                return 0;
            }

            if (this.IdentityType == typeof(long))
            {
                return Convert.ToInt64(val);
            }

            return Convert.ToInt32(val);
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
                        Resources.DateOfBirthParsingError,
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