namespace OopFactory.X12.Shared.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using OopFactory.X12.Specifications;

    /// <summary>
    /// Container segment that can hold other segments, and trailer segments. This class is abstract
    /// </summary>
    public abstract class Container : Segment
    {
        private Segment terminatingTrailerSegment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class with the provided parent, delimiters, and segment string.
        /// </summary>
        /// <param name="parent">Parent container</param>
        /// <param name="delimiters">Delimiter set for separating segment elements and segments</param>
        /// <param name="segment">Segment string representing container</param>
        internal Container(Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
        }
        
        /// <summary>
        /// Gets or sets the collection of <see cref="Segment"/> objects
        /// </summary>
        public IList<Segment> Segments { get; protected set; }

        /// <summary>
        /// Gets the collection of <see cref="Segment"/> objects representing the trailer segments
        /// </summary>
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

        /// <summary>
        /// Gets the container's transaction loop
        /// </summary>
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

        /// <summary>
        /// Gets the collection of segments allowed by the <see cref="SegmentSpecification"/>
        /// </summary>
        internal abstract IList<SegmentSpecification> AllowedChildSegments { get; }

        /// <summary>
        /// Gets the collection of trailer segment ID strings
        /// </summary>
        internal abstract IEnumerable<string> TrailerSegmentIds { get; }

        /// <summary>
        /// Adds provided segment to container's collection of segments, forced if indicated
        /// </summary>
        /// <param name="segmentString">Segment string to be added</param>
        /// <returns>Segment object represented by the provided segment string</returns>
        public Segment AddSegment(string segmentString)
        {
            return this.AddSegment(segmentString, false);
        }

        /// <summary>
        /// Adds provided segment to container's collection of segments, forced if indicated
        /// </summary>
        /// <param name="segmentString">Segment string to be added</param>
        /// <param name="forceAdd">Indicates whether the segment should be forced</param>
        /// <returns>Segment object represented by the provided segment string</returns>
        public Segment AddSegment(string segmentString, bool forceAdd)
        {
            var segment = new Segment(this, this.DelimiterSet, segmentString);
            SegmentSpecification spec = this.AllowedChildSegments.FirstOrDefault(acs => acs.SegmentId == segment.SegmentId);
            if (spec != null || segmentString.StartsWith("TA1") || forceAdd)
            {
                this.Segments.Add(segment);
                return segment;
            }

            if (this.SegmentId == "NM1" && new[] { "N3", "N4", "PER", "REF" }.Contains(segment.SegmentId))
            {
                this.Segments.Add(segment);
                return segment;
            }

            return null;
        }

        /// <summary>
        /// Adds a <see cref="TypedSegment"/> object to the container
        /// </summary>
        /// <typeparam name="T">Segment type</typeparam>
        /// <param name="segment">Segment to be added to the collection</param>
        /// <returns>Object reference to segment, if added; otherwise, null</returns>
        public T AddSegment<T>(T segment) where T : TypedSegment
        {
            segment.Initialize(this, this.DelimiterSet);
            SegmentSpecification spec = this.AllowedChildSegments.FirstOrDefault(acs => acs.SegmentId == segment.Segment.SegmentId);
            if (spec != null)
            {
                this.Segments.Add(segment.Segment);
                return segment;
            }

            return null;
        }
        
        /// <summary>
        /// Sets the provided segment string as the terminating trailer segment
        /// </summary>
        /// <param name="segmentString">Segment string to set as terminating trailer segment</param>
        public void SetTerminatingTrailerSegment(string segmentString)
        {
            this.terminatingTrailerSegment = new Segment(this, this.DelimiterSet, segmentString);
        }

        /// <summary>
        /// Writes <see cref="Container"/> data to an X12 string
        /// </summary>
        /// <param name="addWhitespace">Indicates whether additional whitespace should be added</param>
        /// <returns>X12 string representing the object</returns>
        public override string ToX12String(bool addWhitespace)
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

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            this.Segments = new List<Segment>();
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
            }

            return false;
        }

        internal abstract string SerializeBodyToX12(bool addWhitespace);
    }
}
