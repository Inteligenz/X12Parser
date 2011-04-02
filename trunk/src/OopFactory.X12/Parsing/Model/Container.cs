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
        
        private List<Segment> _trailerSegments;

        private Segment _terminatingTrailerSegment;

        internal Container(Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
        }

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            _segments = new List<Segment>();
            _trailerSegments = new List<Segment>();
        }

        internal abstract IList<SegmentSpecification> AllowedChildSegments { get; }

        public IEnumerable<Segment> Segments { get { return _segments; } }

        public Segment AddSegment(string segmentString)
        {
            Segment segment = new Segment(this, _delimiters, segmentString);
            SegmentSpecification spec = AllowedChildSegments.FirstOrDefault(acs => acs.SegmentId == segment.SegmentId);
            if (spec != null)
            {
                if (spec.Trailer)
                    _trailerSegments.Add(segment);
                else
                    _segments.Add(segment);
                return segment;
            }
            else
                return null;
        }

        public IEnumerable<Segment> TrailerSegments 
        { 
            get
            {
                if (_terminatingTrailerSegment == null)
                    return _trailerSegments;
                {
                    return _trailerSegments.Union(new Segment[] { _terminatingTrailerSegment });
                }
            } 
        }

        
        internal void SetTerminatingTrailerSegment(string segmentString)
        {
            _terminatingTrailerSegment = new Segment(this, _delimiters, segmentString);
        }

        internal virtual int CountTotalSegments()
        {
            return 1 + Segments.Count() + TrailerSegments.Count();
        }


        internal bool UpdateTrailerSegmentCount(string segmentId, int elementNumber, int count)
        {
            var segment = _terminatingTrailerSegment;
            if (segment != null)
            {
                if (segment.ElementCount >= elementNumber)
                {
                    segment.SetElement(elementNumber, count.ToString());
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
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

            foreach (var segment in this.TrailerSegments)
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
