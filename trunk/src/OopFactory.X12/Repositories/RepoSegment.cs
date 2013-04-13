using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Repositories
{
    public class RepoSegment
    {
        public int InterchangeId { get; set; }
        public int? FunctionalGroupId { get; set; }
        public int? TransactionSetId { get; set; }
        public int? ParentLoopId { get; set; }
        public int? LoopId { get; set; }
        public int RevisionId { get; set; }
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
