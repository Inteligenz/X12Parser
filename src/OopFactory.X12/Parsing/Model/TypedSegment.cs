using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model
{
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
            if (Initializing != null)
                Initializing(this, e);
        }
        protected virtual void OnInitialized(EventArgs e)
        {
            if (Initialized != null)
                Initialized(this, e);
        }

        internal void Initialize(Container parent, X12DelimiterSet delimiters)
        {
            OnInitializing(new EventArgs());
            _segment = new Segment(parent, delimiters, _segmentId);
            OnInitialized(new EventArgs());
        }
    }
}
