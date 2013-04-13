using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Repositories
{
    public class RepoSegment<T> where T : struct
    {
        public T InterchangeId { get; set; }
        public T? FunctionalGroupId { get; set; }
        public T? TransactionSetId { get; set; }
        public T? ParentLoopId { get; set; }
        public T? LoopId { get; set; }
        public T RevisionId { get; set; }
        public int PositionInInterchange { get; set; }
        public string SpecLoopId { get; set; }
        public string SegmentId { get; set; }
        public string SegmentString { get; set; }
        public char SegmentTerminator { get; set; }
        public char ElementSeparator { get; set; }
        public char ComponentSeparator { get; set; }
        public bool Deleted { get; set; }
    }
}
