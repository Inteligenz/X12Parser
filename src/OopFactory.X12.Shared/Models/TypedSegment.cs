namespace OopFactory.X12.Shared.Models
{
    using System;

    public abstract class TypedSegment 
    {
        private readonly string segmentId;

        public event EventHandler Initializing;
        
        public event EventHandler Initialized;

        protected TypedSegment(string segmentId)
        {
            this.segmentId = segmentId;
        }

        internal Segment Segment { get; set; }

        internal void Initialize(Container parent, X12DelimiterSet delimiters)
        {
            this.OnInitializing(new EventArgs());
            this.Segment = new Segment(parent, delimiters, this.segmentId);
            this.OnInitialized(new EventArgs());
        }

        protected virtual void OnInitializing(EventArgs e)
        {
            this.Initializing?.Invoke(this, e);
        }

        protected virtual void OnInitialized(EventArgs e)
        {
            this.Initialized?.Invoke(this, e);
        }
    }
}
