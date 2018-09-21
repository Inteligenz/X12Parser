namespace OopFactory.X12.Shared.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using OopFactory.X12.Specifications;

    public abstract class LoopContainer : Container
    {
        internal LoopContainer(Container parent, X12DelimiterSet delimiters, string startingSegment)
            : base(parent, delimiters, startingSegment)
        {
        }

        public IList<Loop> Loops { get; private set; }

        internal abstract IList<LoopSpecification> AllowedChildLoops { get; }

        /// <summary>
        /// Adds a loop specified by the provided segment string to the <see cref="LoopContainer"/>
        /// </summary>
        /// <param name="segmentString">String representing the loop to be added</param>
        /// <returns>Loop added to the container; otherwise, null</returns>
        public Loop AddLoop(string segmentString) 
        {
            LoopSpecification loopSpec = this.GetLoopSpecification(segmentString);

            if (loopSpec != null)
            {
                var loop = new Loop(this, this.DelimiterSet, segmentString, loopSpec);
                this.Segments.Add(loop);
                this.Loops.Add(loop);
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
                this.Segments.Add(loop.Loop);
                this.Loops.Add(loop.Loop);
                return loop;
            }

            throw new TransactionValidationException(
                "Loop {3} could not be added because it could not be found in the specification for {2}",
                null,
                null,
                this.SegmentId,
                segmentString);
        }

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            this.Loops = new List<Loop>();
        }

        private LoopSpecification GetLoopSpecification(string segmentString)
        {
            var segment = new Segment(this, this.DelimiterSet, segmentString);

            IList<LoopSpecification> matchingLoopSpecs = this.AllowedChildLoops
                .Where(cl => cl.StartingSegment.SegmentId == segment.SegmentId)
                .ToList();

            if (matchingLoopSpecs.Count == 0)
            {
                return null;
            }

            if (segment.SegmentId == "NM1" || segment.SegmentId == "N1")
            {
                LoopSpecification spec = matchingLoopSpecs.FirstOrDefault(ls => ls.StartingSegment.EntityIdentifiers.Any(ei => ei.Code == segment.GetElement(1) || ei.Code == "Item" + segment.GetElement(1)));
                if (spec == null)
                {
                    if (matchingLoopSpecs.Count(ls => ls.StartingSegment.SegmentId == segment.SegmentId) == 1)
                    {
                        spec = matchingLoopSpecs.First(ls => ls.StartingSegment.SegmentId == segment.SegmentId);
                    }
                }

                return spec;
            }

            return matchingLoopSpecs.FirstOrDefault();
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
