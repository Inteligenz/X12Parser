using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public abstract class LoopContainer : Container
    {
        internal LoopContainer(Container parent, X12DelimiterSet delimiters, string startingSegment)
            : base(parent, delimiters, startingSegment)
        {
        }

        protected override void Initialize(string segment)
        {
            base.Initialize(segment);
            Loops = new List<Loop>();
        }

        public abstract IList<LoopSpecification> AllowedChildLoops { get; }
        
        public List<Loop> Loops { get; private set; }
        
        internal virtual Segment CreateSegment(Transaction transaction, string segmentString)
        {
            Segment segment = new Segment(this, _delimiters, segmentString);

            if (this is LoopContainer) // (transaction.LoopStartingSegmentIds.Contains(segment.SegmentId))
            {
                IList<LoopSpecification> matchingLoopSpecs = ((LoopContainer)this).AllowedChildLoops
                    .Where(cl => cl.StartingSegment.SegmentSpecification.SegmentId == segment.SegmentId).ToList();

                if (matchingLoopSpecs == null || matchingLoopSpecs.Count == 0)
                {
                    return segment;
                }
                else if (segment.SegmentId == "NM1" || segment.SegmentId == "N1")
                {
                    var loopSpecification = matchingLoopSpecs.Where(ls => ls.StartingSegment.EntityIdentifiers.Any(ei => ei.Code.ToString() == segment.DataElements[0] || ei.Code.ToString() == "Item" + segment.DataElements[0])).FirstOrDefault();
                    return new Loop(this, _delimiters, segment.SegmentString, loopSpecification);

                }
                else
                {
                    return new Loop(this, _delimiters, segment.SegmentString, matchingLoopSpecs.FirstOrDefault());
                }
            }
            else
                return segment;
        }
    }
}
