using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public abstract class LoopContainer : Container
    {
        private List<Loop> _loops;

        internal LoopContainer(Container parent, X12DelimiterSet delimiters, string startingSegment)
            : base(parent, delimiters, startingSegment)
        {
        }

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            _loops = new List<Loop>();
        }

        internal abstract IList<LoopSpecification> AllowedChildLoops { get; }

        public IEnumerable<Loop> Loops { get { return _loops; } }

        public Loop AddLoop(string segmentString) 
        {
            LoopSpecification loopSpec = GetLoopSpecification(segmentString);

            if (loopSpec != null)
            {
                var loop = new Loop(this, _delimiters, segmentString, loopSpec);
                _segments.Add(loop);
                _loops.Add(loop);
                return loop;
            }
            else
                return null;
        }

        public T AddLoop<T>(T loop) where T : TypedLoop
        {
            string segmentString = loop.GetSegmentString(_delimiters);
            LoopSpecification loopSpec = GetLoopSpecification(segmentString);

            if (loopSpec != null)
            {
                loop.Initialize(this, _delimiters, loopSpec);
                _segments.Add(loop._loop);
                _loops.Add(loop._loop);
                return loop;
            }
            else
                throw new TransactionValidationException("Loop {3} could not be added because it could not be found in the specification for {2}",
                    null, null, this.SegmentId, segmentString);
        }

        private LoopSpecification GetLoopSpecification(string segmentString)
        {
            Segment segment = new Segment(this, _delimiters, segmentString);

            IList<LoopSpecification> matchingLoopSpecs = ((LoopContainer)this).AllowedChildLoops
                        .Where(cl => cl.StartingSegment.SegmentId == segment.SegmentId).ToList();

            if (matchingLoopSpecs == null || matchingLoopSpecs.Count == 0)
            {
                return null;
            }
            else if (segment.SegmentId == "NM1" || segment.SegmentId == "N1")
            {
                LoopSpecification spec = matchingLoopSpecs.Where(ls => ls.StartingSegment.EntityIdentifiers.Any(ei => ei.Code.ToString() == segment.GetElement(1) || ei.Code.ToString() == "Item" + segment.GetElement(1))).FirstOrDefault();
                if (spec == null)
                {
                    if (matchingLoopSpecs.Where(ls => ls.StartingSegment.SegmentId == segment.SegmentId).Count() == 1)
                    {
                        spec = matchingLoopSpecs.Where(ls => ls.StartingSegment.SegmentId == segment.SegmentId).First();
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
            return "";
        }
    }
}
