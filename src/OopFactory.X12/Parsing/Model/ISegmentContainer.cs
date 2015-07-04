using System;

namespace OopFactory.X12.Parsing.Model {
    public interface ISegmentContainer {
        Segment AddSegment(string segmentString);
        Segment AddSegment(string segmentString, bool forceAdd);
        T AddSegment<T>(T segment) where T : TypedSegment;
        T AddSegment<T>(T segment, bool forceAdd) where T : TypedSegment;
    }
}
