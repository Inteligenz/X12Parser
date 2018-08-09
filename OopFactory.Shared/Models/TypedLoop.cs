namespace OopFactory.X12.Shared.Models
{
    using OopFactory.X12.Specifications;

    public abstract class TypedLoop
    {
        internal string SegmentId { get; set; }

        internal Loop Loop { get; set; }

        protected TypedLoop(string segmentId)
        {
            this.SegmentId = segmentId;
        }

        public Loop AddLoop(string segmentString)
        {
            return this.Loop.AddLoop(segmentString);
        }

        public T AddLoop<T>(T loop) where T : TypedLoop
        {
            return this.Loop.AddLoop(loop);
        }

        public Segment AddSegment(string segmentString)
        {
            return this.Loop.AddSegment(segmentString);
        }

        public T AddSegment<T>(T segment) where T : TypedSegment
        {
            return this.Loop.AddSegment(segment);
        }

        internal virtual string GetSegmentString(X12DelimiterSet delimiters)
        {
            return string.Format("{0}{1}", this.SegmentId, delimiters.ElementSeparator);
        }

        internal virtual void Initialize(Container parent, X12DelimiterSet delimiters, LoopSpecification loopSpecification)
        {
            this.Loop = new Loop(parent, delimiters, this.SegmentId, loopSpecification);
        }
    }
}
