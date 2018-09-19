namespace OopFactory.X12.Shared.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using OopFactory.X12.Specifications;

    public abstract class LoopContainer : Container
    {
        private List<Loop> loops;

        internal LoopContainer(Container parent, X12DelimiterSet delimiters, string startingSegment)
            : base(parent, delimiters, startingSegment)
        {
        }

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            this.loops = new List<Loop>();
        }

        internal abstract IList<LoopSpecification> AllowedChildLoops { get; }

        public IEnumerable<Loop> Loops => this.loops;

        public Loop AddLoop(string segmentString) 
        {
            LoopSpecification loopSpec = this.GetLoopSpecification(segmentString);

            if (loopSpec != null)
            {
                var loop = new Loop(this, this.DelimiterSet, segmentString, loopSpec);
                this.segments.Add(loop);
                this.loops.Add(loop);
                return loop;
            }

            return null;
        }

        public T AddLoop<T>(T loop) where T : TypedLoop
        {
            string segmentString = loop.GetSegmentString(this.DelimiterSet);
            LoopSpecification loopSpec = this.GetLoopSpecification(segmentString);

            if (loopSpec != null)
            {
                loop.Initialize(this, this.DelimiterSet, loopSpec);
                this.segments.Add(loop.Loop);
                this.loops.Add(loop.Loop);
                return loop;
            }

            throw new TransactionValidationException(
                "Loop {3} could not be added because it could not be found in the specification for {2}",
                null,
                null,
                this.SegmentId,
                segmentString);
        }

        private LoopSpecification GetLoopSpecification(string segmentString)
        {
            var segment = new Segment(this, this.DelimiterSet, segmentString);

            IList<LoopSpecification> matchingLoopSpecs = this.AllowedChildLoops
                .Where(cl => cl.StartingSegment.SegmentId == segment.SegmentId)
                .ToList();

            if (matchingLoopSpecs == null || matchingLoopSpecs.Count == 0)
            {
                return null;
            }
            else if (segment.SegmentId == "NM1" || segment.SegmentId == "N1")
            {
                LoopSpecification spec = matchingLoopSpecs.Where(ls => ls.StartingSegment.EntityIdentifiers.Any(ei => ei.Code.ToString() == segment.GetElement(1) || ei.Code.ToString() == "Item" + segment.GetElement(1))).FirstOrDefault();
                if (spec == null)
                {
                    if (matchingLoopSpecs.Count(ls => ls.StartingSegment.SegmentId == segment.SegmentId) == 1)
                    {
                        spec = matchingLoopSpecs.First(ls => ls.StartingSegment.SegmentId == segment.SegmentId);
                    }
                }

                return spec;
            }
            else
            {
                return matchingLoopSpecs.FirstOrDefault();
            }
        }

        internal override int CountTotalSegments()
        {
            return base.CountTotalSegments() + this.Loops.Sum(l => l.CountTotalSegments()) - this.Loops.Count();
        }

        internal override string SerializeBodyToX12(bool addWhitespace)
        {
            return string.Empty;
        }
    }
}
