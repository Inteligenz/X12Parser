using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Repositories
{
	[Obsolete("Use OopFactory.X12.Sql library and namespace")]
	public class RepoEntity<T>
    {
        public T EntityId { get; set; }
        public string EntityIdentifierCode { get; set; }
        public string EntityIdentifier { get; set; }
        public T InterchangeId { get; set; }
        public T TransactionSetId { get; set; }
        public string TransactionSetCode { get; set; }
        public T ParentLoopId { get; set; }
        public string SpecLoopId { get; set; }
        public string StartingSegmentId { get; set; }
        public string Name { get; set; }
        public bool? IsPerson { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NamePrefix { get; set; }
        public string NameSuffix { get; set; }
        public string IdQualifier { get; set; }
        public string Identification { get; set; }
        public string Ssn { get; set; }
        public string Npi { get; set; }
        public string TelephoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string PostalCode { get; set; }
        public string County { get; set; }
        public string CountryCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
    }
}
