namespace OopFactory.X12.Shared.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using OopFactory.X12.Specifications;

    public abstract class Container : Segment
    {
        protected List<Segment> segments;
        
        private Segment terminatingTrailerSegment;

        internal Container(Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
        }

        internal abstract IList<SegmentSpecification> AllowedChildSegments { get; }

        public IEnumerable<Segment> Segments => this.segments;

        internal abstract IEnumerable<string> TrailerSegmentIds { get; }

        public IEnumerable<Segment> TrailerSegments
        {
            get
            {
                var list = new List<Segment>();
                if (this.terminatingTrailerSegment != null)
                {
                    list.Add(this.terminatingTrailerSegment);
                }

                return list;
            }
        }

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            this.segments = new List<Segment>();
        }

        public Transaction Transaction
        {
            get
            {
                Container container = this;
                while (!(container is Transaction))
                {
                    container = container.Parent;
                    if (container == null)
                    {
                        return null;
                    }
                }

                return (Transaction)container;
            }
        }

        public Segment AddSegment(string segmentString)
        {
            return this.AddSegment(segmentString, false);
        }

        public Segment AddSegment(string segmentString, bool forceAdd)
        {
            var segment = new Segment(this, this.DelimiterSet, segmentString);
            SegmentSpecification spec = this.AllowedChildSegments.FirstOrDefault(acs => acs.SegmentId == segment.SegmentId);
            if (spec != null || segmentString.StartsWith("TA1") || forceAdd)
            {
                this.segments.Add(segment);
                return segment;
            }
            else if (this.SegmentId == "NM1" &&
                new[] { "N3", "N4", "PER", "REF" }.Contains(segment.SegmentId))
            {
                this.segments.Add(segment);
                return segment;
            }
            else
            {
                return null;
            }
        }

        public T AddSegment<T>(T segment) where T : TypedSegment
        {
            segment.Initialize(this, this.DelimiterSet);
            SegmentSpecification spec = this.AllowedChildSegments.FirstOrDefault(acs => acs.SegmentId == segment._segment.SegmentId);
            if (spec != null)
            {
                this.segments.Add(segment._segment);
                return segment;
            }

            return null;
        }
        
        internal void SetTerminatingTrailerSegment(string segmentString)
        {
            this.terminatingTrailerSegment = new Segment(this, this.DelimiterSet, segmentString);
        }

        internal virtual int CountTotalSegments()
        {
            return 1 + this.Segments.Count() + this.TrailerSegments.Count();
        }

        internal bool UpdateTrailerSegmentCount(string segmentId, int elementNumber, int count)
        {
            Segment segment = this.terminatingTrailerSegment;
            if (segment != null)
            {
                if (segment.ElementCount >= elementNumber)
                {
                    segment.SetElement(elementNumber, count.ToString());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        internal abstract string SerializeBodyToX12(bool addWhitespace);
        
        internal override string ToX12String(bool addWhitespace)
        {
            var sb = new StringBuilder(base.ToX12String(addWhitespace));

            foreach (Segment segment in this.Segments.Where(seg => !this.TrailerSegmentIds.Contains(seg.SegmentId)))
            {
                sb.Append(addWhitespace
                        ? segment.ToX12String(addWhitespace).Replace("\r\n", "\r\n  ")
                        : segment.ToX12String(addWhitespace));
            }

            sb.Append(addWhitespace
                    ? this.SerializeBodyToX12(addWhitespace).Replace("\r\n", "\r\n  ")
                    : this.SerializeBodyToX12(addWhitespace));

            foreach (Segment segment in this.Segments.Where(seg => this.TrailerSegmentIds.Contains(seg.SegmentId)))
            {
                sb.Append(addWhitespace
                        ? segment.ToX12String(addWhitespace).Replace("\r\n", "\r\n  ")
                        : segment.ToX12String(addWhitespace));
            }

            foreach (var segment in this.TrailerSegments)
            {
                var wrapperSegments = new[] { "SE", "GE", "IEA" };
                if (addWhitespace && !wrapperSegments.Contains(segment.SegmentId))
                {
                    sb.Append(segment.ToX12String(addWhitespace).Replace("\r\n", "\r\n  "));
                }
                else
                {
                    sb.Append(segment.ToX12String(addWhitespace));
                }
            }

            return sb.ToString();
        }
    }
}
