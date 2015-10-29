using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Repositories
{
	[Obsolete("Use OopFactory.X12.Sql library and namespace")]
	public class RepoLoopSearchCriteria<T> where T : struct
    {
        public T? LoopId { get; set; }
        public T? ParentLoopId { get; set; }
        public T? InterchangeId { get; set; }
        public T? TransactionSetId { get; set; }
        public string TransactionSetCode { get; set; }
        public string SpecLoopId { get; set; }
        public string LevelId { get; set; }
        public string LevelCode { get; set; }
        public string StartingSegmentId { get; set; }
        public string EntityIdentifierCode { get; set; }
    }
}
