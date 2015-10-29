using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Repositories
{
	[Obsolete("Use OopFactory.X12.Sql library and namespace")]
	public class RepoTransactionSetSearchCriteria<T> where T : struct
    {
        public T? InterchangeId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string InterchangeControlNumber { get; set; }
        public DateTime? InterchangeMinDate { get; set; }
        public DateTime? InterchangeMaxDate { get; set; }

        public T? FunctionalGroupId { get; set; }
        public string FunctionalIdCode { get; set; }
        public string FunctionalGroupControlNumber { get; set; }
        public string VersionPattern { get; set; }

        public T? TransactionSetId { get; set; }
        public string TransactionSetCode { get; set; }
        public string TransactionSetControlNumber { get; set; }
    }
}
