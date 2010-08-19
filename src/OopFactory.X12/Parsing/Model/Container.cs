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
        private List<Segment> _typedSegments;

        internal Container(X12DelimiterSet delimiters, string segment)
            : base(delimiters, segment)
        {
        }

        protected override void Initialize(string segment)
        {
            base.Initialize(segment);
            _segments = new List<Segment>();
            _typedSegments = new List<Segment>();
        }

        public abstract IList<SegmentSpecification> AllowedChildSegments { get; }

        public List<Segment> Segments
        {
            get { return _segments; }
        }
    }
}
