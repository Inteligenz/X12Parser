
namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentN1 : TypedSegment
    {
        public TypedSegmentN1()
            : base("N1")
        {
        }
        public TypedSegmentN1(Segment segment) : base(segment) { }

        public string N101_EntityIdentifierCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string N102_Name
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string N103_IdentificationCodeQualifier
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string N104_IdentificationCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string N105_EntityRelationshipCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string N106_EntityIdentifierCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }
    }
}
