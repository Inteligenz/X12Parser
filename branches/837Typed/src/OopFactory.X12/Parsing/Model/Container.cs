using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public abstract class Container : Segment
    {
        protected List<Segment> _segments;
        
        private Segment _terminatingTrailerSegment;

        internal Container(Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
        }

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            _segments = new List<Segment>();
        }

        internal abstract IList<SegmentSpecification> AllowedChildSegments { get; }

        public Transaction Transaction
        {
            get
            {
                Container container = this;
                while (!(container is Transaction))
                {
                    container = container.Parent;
                    if (container == null)
                        return null;
                }
                return (Transaction)container;
            }
        }

        public IEnumerable<Segment> Segments { get { return _segments; } }

        internal abstract IEnumerable<string> TrailerSegmentIds { get; }

        public Segment AddSegment(string segmentString)
        {
            return AddSegment(segmentString, false);
        }
        public Segment AddSegment(string segmentString, bool forceAdd)
        {
            Segment segment = new Segment(this, _delimiters, segmentString);
            SegmentSpecification spec = AllowedChildSegments.FirstOrDefault(acs => acs.SegmentId == segment.SegmentId);
            if (spec != null || segmentString.StartsWith("TA1") || forceAdd)
            {
                _segments.Add(segment);
                return segment;
            }
            else if ((this.SegmentId == "NM1") &&
                (new string[] { "N3", "N4", "PER", "REF" }.Contains(segment.SegmentId)))
            {
                _segments.Add(segment);
                return segment;
            }
            else
                return null;
        }

        public T AddSegment<T>(T segment) where T : TypedSegment
        {
            segment.Initialize(this, _delimiters);
            SegmentSpecification spec = AllowedChildSegments.FirstOrDefault(acs => acs.SegmentId == segment._segment.SegmentId);
            if (spec != null)
            {
                _segments.Add(segment._segment);
                return segment;
            }
            else
                return null;
        }

        public IEnumerable<Segment> TrailerSegments 
        { 
            get
            {
                var list = new List<Segment>();
                if (_terminatingTrailerSegment != null)
                    list.Add(_terminatingTrailerSegment);
                return list;
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

            foreach (var segment in this.Segments.Where(seg=> !TrailerSegmentIds.Contains(seg.SegmentId)))
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

            foreach (var segment in this.Segments.Where(seg => TrailerSegmentIds.Contains(seg.SegmentId)))
            {
                if (addWhitespace)
                    sb.Append(segment.ToX12String(addWhitespace).Replace("\r\n", "\r\n  "));
                else
                    sb.Append(segment.ToX12String(addWhitespace));
            }

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
