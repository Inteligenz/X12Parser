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
        public int PositionInInterchange { get; set; }
        public string SpecLoopId { get; set; }
        public string SegmentId { get; set; }
        public string Segment { get; set; }
        public string SegmentTerminator { get; set; }

    }
}
