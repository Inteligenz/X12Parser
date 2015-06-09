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
        internal Loop _loop;

        protected TypedLoop(string segmentId)
        {
            _segmentId = segmentId;
        }

        internal virtual string GetSegmentString(X12DelimiterSet delimiters)
        {
            return String.Format("{0}{1}", _segmentId, delimiters.ElementSeparator);
        }
        

        internal virtual void Initialize(Container parent, X12DelimiterSet delimiters, LoopSpecification loopSpecification)
        {
            _loop = new Loop(parent, delimiters, _segmentId, loopSpecification);
        }

        public Loop AddLoop(string segmentString)
        {
            return _loop.AddLoop(segmentString);
        }

        public T AddLoop<T>(T loop) where T : TypedLoop
        {
            return _loop.AddLoop(loop);
        }

        public Segment AddSegment(string segmentString)
        {
            return _loop.AddSegment(segmentString);
        }

        public T AddSegment<T>(T segment) where T : TypedSegment
        {
            return _loop.AddSegment(segment);
        }

    }
}
