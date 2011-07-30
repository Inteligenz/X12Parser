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

        internal void Initialize(Container parent, X12DelimiterSet delimiters)
        {
            _segment = new Segment(parent, delimiters, _segmentId);
        }
    }
}
