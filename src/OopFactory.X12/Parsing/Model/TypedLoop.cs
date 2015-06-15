using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public abstract class TypedLoop
    {
        internal string _segmentId;
        public Loop Loop { get; set; }

        protected TypedLoop(string segmentId)
        {
            _segmentId = segmentId;
        }

        public TypedLoop(Loop loop)
        {
            _segmentId = loop.SegmentId;
            Loop = loop;
        }

        internal virtual string GetSegmentString(X12DelimiterSet delimiters)
        {
            return String.Format("{0}{1}", _segmentId, delimiters.ElementSeparator);
        }


        internal virtual void Initialize(Container parent, X12DelimiterSet delimiters, LoopSpecification loopSpecification)
        {
            Loop = new Loop(parent, delimiters, _segmentId, loopSpecification);
        }

        public Loop AddLoop(string segmentString)
        {
            return Loop.AddLoop(segmentString);
        }

        public T AddLoop<T>(T loop) where T : TypedLoop
        {
            return Loop.AddLoop(loop);
        }

        public Segment AddSegment(string segmentString)
        {
            return Loop.AddSegment(segmentString);
        }

        public T AddSegment<T>(T segment) where T : TypedSegment
        {
            return Loop.AddSegment(segment);
        }

    }
}
