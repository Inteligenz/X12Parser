namespace OopFactory.X12.Shared.Models
{
    using System;

    public abstract class TypedSegment 
    {
        private string _segmentId;
        internal Segment _segment;

        protected TypedSegment(string segmentId)
        {
            _segmentId = segmentId;
        }

        public event EventHandler Initializing;
        public event EventHandler Initialized;

        protected virtual void OnInitializing(EventArgs e)
        {
            this.Initializing?.Invoke(this, e);
        }
        protected virtual void OnInitialized(EventArgs e)
        {
            this.Initialized?.Invoke(this, e);
        }

        internal void Initialize(Container parent, X12DelimiterSet delimiters)
        {
            OnInitializing(new EventArgs());
            _segment = new Segment(parent, delimiters, _segmentId);
            OnInitialized(new EventArgs());
        }
    }
}
