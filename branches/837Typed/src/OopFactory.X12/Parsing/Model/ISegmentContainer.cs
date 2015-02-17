using System;

namespace OopFactory.X12.Parsing.Model {
    public interface ISegmentContainer {
        Segment AddSegment(string segmentString);
        T AddSegment<T>(T segment) where T : TypedSegment;
    }
}
