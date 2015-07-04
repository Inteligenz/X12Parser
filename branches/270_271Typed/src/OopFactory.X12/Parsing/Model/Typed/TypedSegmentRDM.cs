
namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentRDM : TypedSegment
    {
        public TypedSegmentRDM()
            : base("RDM")
        {
        }

        public TypedSegmentRDM(Segment segment)
            : base(segment)
        { }

        public string RDM01_ReportTransmissionCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string RDM02_Name
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string RDM03_CommunicationNumber
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public TypedElementReferenceIdentifier REF04_ReferenceId
        {
            get { return new TypedElementReferenceIdentifier(_segment, 4); }
            set { _segment.SetElement(4, value); }
        }

        public TypedElementReferenceIdentifier REF05_ReferenceId
        {
            get { return new TypedElementReferenceIdentifier(_segment, 5); }
            set { _segment.SetElement(5, value); }
        }
    }
}
