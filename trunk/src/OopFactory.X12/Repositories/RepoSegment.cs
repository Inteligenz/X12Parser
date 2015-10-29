using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Repositories
{
	[Obsolete("Use OopFactory.X12.Sql library and namespace")]
	public class RepoSegment<T> where T : struct
    {
        public RepoSegment(string segmentString, char segmentTerminator, char elementSeparator, char componentSeparator)
        {
            Segment = new DetachedSegment(new X12DelimiterSet(segmentTerminator, elementSeparator, componentSeparator), segmentString);
        }
        public T InterchangeId { get; set; }
        public T? FunctionalGroupId { get; set; }
        public T? TransactionSetId { get; set; }
        public T? ParentLoopId { get; set; }
        public T? LoopId { get; set; }
        public int RevisionId { get; set; }
        public int PositionInInterchange { get; set; }
        public string SpecLoopId { get; set; }
        public DetachedSegment Segment { get; private set; }
        public bool Deleted { get; set; }
    }
}
