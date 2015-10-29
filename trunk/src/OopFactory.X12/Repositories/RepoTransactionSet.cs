using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing;

namespace OopFactory.X12.Repositories
{
	[Obsolete("Use OopFactory.X12.Sql library and namespace")]
	public class RepoTransactionSet<T> where T : struct
    {
        public RepoTransactionSet(char segmentTerminator, char elementSeparator, char componentSeparator)
        {
            Delimiters = new X12DelimiterSet(segmentTerminator, elementSeparator, componentSeparator);
        }
        
        public T InterchangeId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string InterchangeControlNumber { get; set; }
        public DateTime? InterchangeDate { get; set; }
        public X12DelimiterSet Delimiters { get; private set; }

        public T FunctionalGroupId { get; set; }
        public string FunctionalIdCode { get; set; }
        public string FunctionalGroupControlNumber { get; set; }
        public string Version { get; set; }

        public T TransactionSetId { get; set; }
        public string TransactionSetCode { get; set; }
        public string ControlNumber { get; set; }
        public string ImplementationConventionRef { get; set; }
    }
}
