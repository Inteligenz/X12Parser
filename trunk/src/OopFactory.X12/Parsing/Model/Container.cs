using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public abstract class Container : Segment
    {
        private List<Segment> _segments;
        internal Container(X12DelimiterSet delimiters, string segment)
            : base(delimiters, segment)
        {
        }

        protected override void Initialize(string segment)
        {
            base.Initialize(segment);
            _segments = new List<Segment>();
        }

        public abstract IList<SegmentSpecification> AllowedChildSegments { get; }

        public IEnumerable<Segment> Segments { get { return _segments; } }

        public void AddSegment(string segmentString)
        {
            AddSegment( new Segment(_delimiters, segmentString));
            
        }

        public void AddSegment(Segment segment)
        {
            if (AllowedChildSegments.FirstOrDefault(acs => acs.SegmentId == segment.SegmentId) != null)
                _segments.Add(segment);
            else
                throw new InvalidOperationException(String.Format("Segment {0} is not allowed as a segment of {1}.", segment.SegmentId, this.SegmentId));
        }

        internal virtual Segment CreateSegment(Transaction transaction, Segment segment)
        {
            if (segment.SegmentId == "HL")
            {
                var hl = new HierarchicalLoop(_delimiters, segment.SegmentString);

                hl.Specification = transaction.Specification.HierarchicalLoopSpecifications.FirstOrDefault(
                    hls => hls.LevelCode.ToString() == hl.LevelCode);

                if (hl.Specification == null)
                    throw new InvalidOperationException(String.Format("HL with level code {0} is not expected in transaction set {1}.",
                        hl.LevelCode, transaction.Specification.TransactionSetIdentifierCode));
                return hl;
            }
            else if (this is LoopContainer) // (transaction.LoopStartingSegmentIds.Contains(segment.SegmentId))
            {
                IList<LoopSpecification> matchingLoopSpecs =  ((LoopContainer)this).AllowedChildLoops
                    .Where(cl => cl.StartingSegment.SegmentSpecification.SegmentId == segment.SegmentId).ToList();

                if (matchingLoopSpecs == null || matchingLoopSpecs.Count == 0)
                {
                    return segment;
                }
                else if (segment.SegmentId == "NM1" || segment.SegmentId == "N1")
                {
                    var loopSpecification = matchingLoopSpecs.Where(ls => ls.StartingSegment.EntityIdentifiers.Any(ei => ei.Code.ToString() == segment.DataElements[0] || ei.Code.ToString() == "Item" + segment.DataElements[0])).FirstOrDefault();
                    return new Entity(_delimiters, segment.SegmentString, loopSpecification);

                }
                else
                {
                    return new Loop(_delimiters, segment.SegmentString, matchingLoopSpecs.FirstOrDefault());
                }
            }
            else
                return segment;
        }

        internal virtual Transaction CreateTransaction(string segmentString, TransactionSpecification transactionSpecification)
        {
            return new Transaction(_delimiters, segmentString, transactionSpecification);
        }
    }
}
