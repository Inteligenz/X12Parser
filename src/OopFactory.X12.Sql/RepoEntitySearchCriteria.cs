namespace OopFactory.X12.Sql
{
	using System;

	public class RepoEntitySearchCriteria
	{
		/// <summary>
		///     Comma delimited string of entity identifierCodes to include in results
		/// </summary>
		public string EntityIdentifierCodes { get; set; }

		public string EntityIdentifierContains { get; set; }
		public object InterchangeId { get; set; }
		public object TransactionSetId { get; set; }
		public string TransactionSetCode { get; set; }
		public object ParentLoopId { get; set; }
		public string SpecLoopId { get; set; }
		public string StartingSegmentId { get; set; }
		public string NameContains { get; set; }
		public bool? IsPerson { get; set; }
		public string LastNameStartsWith { get; set; }
		public string FirstNameContains { get; set; }
		public string IdQualifier { get; set; }
		public string Identification { get; set; }
		public string Ssn { get; set; }
		public string Npi { get; set; }
		public string City { get; set; }
		public string StateCode { get; set; }
		public string PostalCode { get; set; }
		public string County { get; set; }
		public string CountryCode { get; set; }
		public DateTime? DateOfBirthOn { get; set; }
		public DateTime? DateOfBirthOnOrAfter { get; set; }
		public DateTime? DateOfBirthOnOrBefore { get; set; }
		public string Gender { get; set; }
	}
}