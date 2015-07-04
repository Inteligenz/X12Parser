
namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Trace Information
    /// </summary>
    public class TypedSegmentTRN : TypedSegment
    {
        public TypedSegmentTRN() : base("TRN") { }
        public TypedSegmentTRN(Segment segment) : base(segment) { }

        public string TRN01_TraceTypeCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string TRN02_ReferenceIdentification
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string TRN03_OriginatingCompanyIdentifier
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string TRN04_ReferenceIdentification
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }
    }
}
