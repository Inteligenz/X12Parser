﻿namespace X12.Sql
{
    using X12.Shared.Models;

    public class RepoSegment
    {
        public RepoSegment(string segmentString, char segmentTerminator, char elementSeparator, char componentSeparator)
        {
            this.Segment = new DetachedSegment(
                new X12DelimiterSet(segmentTerminator, elementSeparator, componentSeparator),
                segmentString);
        }

        public object InterchangeId { get; set; }

        public object FunctionalGroupId { get; set; }

        public object TransactionSetId { get; set; }

        public object ParentLoopId { get; set; }

        public object LoopId { get; set; }

        public int RevisionId { get; set; }

        public int PositionInInterchange { get; set; }

        public string SpecLoopId { get; set; }

        public DetachedSegment Segment { get; }

        public bool Deleted { get; set; }
    }
}