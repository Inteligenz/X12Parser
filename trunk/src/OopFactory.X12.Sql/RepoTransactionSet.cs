namespace OopFactory.X12.Sql
{
	using System;
	using Parsing;

	public class RepoTransactionSet
	{
		public RepoTransactionSet(char segmentTerminator, char elementSeparator, char componentSeparator)
		{
			Delimiters = new X12DelimiterSet(segmentTerminator, elementSeparator, componentSeparator);
		}

		public object InterchangeId { get; set; }
		public string SenderId { get; set; }
		public string ReceiverId { get; set; }
		public string InterchangeControlNumber { get; set; }
		public DateTime? InterchangeDate { get; set; }
		public X12DelimiterSet Delimiters { get; private set; }

		public object FunctionalGroupId { get; set; }
		public string FunctionalIdCode { get; set; }
		public string FunctionalGroupControlNumber { get; set; }
		public string Version { get; set; }

		public object TransactionSetId { get; set; }
		public string TransactionSetCode { get; set; }
		public string ControlNumber { get; set; }
		public string ImplementationConventionRef { get; set; }
	}
}