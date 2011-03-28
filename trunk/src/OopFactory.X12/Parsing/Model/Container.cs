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
        private List<Segment> _terminatingSegments;

        internal Container(Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
        }

        protected override void Initialize(string segment)
        {
            base.Initialize(segment);
            _segments = new List<Segment>();
            _terminatingSegments = new List<Segment>();
        }

        public abstract IList<SegmentSpecification> AllowedChildSegments { get; }

        public IEnumerable<Segment> Segments { get { return _segments; } }

        public bool AddSegment(string segmentString)
        {
            Segment segment = new Segment(this, _delimiters, segmentString);
            if (AllowedChildSegments.FirstOrDefault(acs => acs.SegmentId == segment.SegmentId) != null)
            {
                _segments.Add(segment);
                return true;
            }
            else
                return false;
        }

        public IEnumerable<Segment> TerminatingSegments { get { return _terminatingSegments; } }

        public void AddTerminatingSegment(Container parent, string segmentString)
        {
            _terminatingSegments.Add(new Segment(parent, _delimiters, segmentString));
        }
        
        internal virtual Transaction CreateTransaction(Container parent, string segmentString, TransactionSpecification transactionSpecification)
        {
            return new Transaction(parent, _delimiters, segmentString, transactionSpecification);
        }
    }
}
