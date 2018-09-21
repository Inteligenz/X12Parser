namespace OopFactory.X12.Shared.Models
{
    using OopFactory.X12.Specifications;

    /// <summary>
    /// Represents a loop with a specified type
    /// </summary>
    public abstract class TypedLoop
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypedLoop"/> class with the provided segment id
        /// </summary>
        /// <param name="segmentId">Segment id of the loop</param>
        protected TypedLoop(string segmentId)
        {
            this.SegmentId = segmentId;
        }

        /// <summary>
        /// Gets or sets the loop segment id
        /// </summary>
        internal string SegmentId { get; set; }

        /// <summary>
        /// Gets or sets the containing loop
        /// </summary>
        internal Loop Loop { get; set; }

        /// <summary>
        /// Adds a loop specified by the provided segment string to the <see cref="TypedLoop"/>
        /// </summary>
        /// <param name="segmentString">Loop segment string to add</param>
        /// <returns>Loop created from the provied segment string</returns>
        public Loop AddLoop(string segmentString)
        {
            return this.Loop.AddLoop(segmentString);
        }

        /// <summary>
        /// Inserts the loop, of <see cref="TypedLoop"/>
        /// </summary>
        /// <typeparam name="T">Loop which derives TypedLoop</typeparam>
        /// <param name="loop">Loop to be added</param>
        /// <returns>Loop that was inserted</returns>
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
            return $"{this.SegmentId}{delimiters.ElementSeparator}";
        }

        internal virtual void Initialize(Container parent, X12DelimiterSet delimiters, LoopSpecification loopSpecification)
        {
            this.Loop = new Loop(parent, delimiters, this.SegmentId, loopSpecification);
        }
    }
}
