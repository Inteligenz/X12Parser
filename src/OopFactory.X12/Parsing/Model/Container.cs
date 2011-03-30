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

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            _segments = new List<Segment>();
            _terminatingSegments = new List<Segment>();
        }

        internal abstract IList<SegmentSpecification> AllowedChildSegments { get; }

        public IEnumerable<Segment> Segments { get { return _segments; } }

        public bool AddSegment(string segmentString)
        {
            Segment segment = new Segment(this, _delimiters, segmentString);
            SegmentSpecification spec = AllowedChildSegments.FirstOrDefault(acs => acs.SegmentId == segment.SegmentId);
            if (spec != null)
            {
                if (spec.Trailer)
                    _terminatingSegments.Add(segment);
                else
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

        internal abstract string SerializeBodyToX12(bool addWhitespace);
        
        internal override string ToX12String(bool addWhitespace)
        {
            StringBuilder sb = new StringBuilder(base.ToX12String(addWhitespace));
            foreach (var segment in this.Segments)
            {
                if (addWhitespace)
                    sb.Append(segment.ToX12String(addWhitespace).Replace("\r\n", "\r\n  "));
                else
                    sb.Append(segment.ToX12String(addWhitespace));
            }
            if (addWhitespace)
            {
                sb.Append(SerializeBodyToX12(addWhitespace).Replace("\r\n", "\r\n  "));
            }
            else
                sb.Append(SerializeBodyToX12(addWhitespace));

            foreach (var segment in this.TerminatingSegments)
            {
                string[] wrapperSegments = new string[] { "SE", "GE", "IEA" };
                if (addWhitespace && !wrapperSegments.Contains(segment.SegmentId))
                    sb.Append(segment.ToX12String(addWhitespace).Replace("\r\n", "\r\n  "));
                else
                    sb.Append(segment.ToX12String(addWhitespace));
            }

            return sb.ToString();
        }
    }
}
