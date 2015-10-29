namespace OopFactory.X12.Sql
{
	using System;

	public class RepoTransactionSetSearchCriteria
	{
		public object InterchangeId { get; set; }
		public string SenderId { get; set; }
		public string ReceiverId { get; set; }
		public string InterchangeControlNumber { get; set; }
		public DateTime? InterchangeMinDate { get; set; }
		public DateTime? InterchangeMaxDate { get; set; }

		public object FunctionalGroupId { get; set; }
		public string FunctionalIdCode { get; set; }
		public string FunctionalGroupControlNumber { get; set; }
		public string VersionPattern { get; set; }

		public object TransactionSetId { get; set; }
		public string TransactionSetCode { get; set; }
		public string TransactionSetControlNumber { get; set; }
	}
}