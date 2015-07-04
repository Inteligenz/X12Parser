
namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementPositionInSegment : BaseElementReference
    {
        public TypedElementPositionInSegment(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }

        private int? _elementPositionInSegment;
        private int? _componentDataElementPositionInComposite;
        private int? _repeatingDataElementPosition;

        public override string ToString()
        {
            string value = string.Format("{1}{0}{2}{0}{3}",
               Segment._delimiters.SubElementSeparator,
               _elementPositionInSegment, _componentDataElementPositionInComposite, _repeatingDataElementPosition);
            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public int? _1_ElementPositionInSegment
        {
            get { return _elementPositionInSegment; }
            set
            {
                _elementPositionInSegment = value;
            }
        }

        public int? _2_ComponentDataElementPositionInComposite
        {
            get { return _componentDataElementPositionInComposite; }
            set
            {
                _componentDataElementPositionInComposite = value;

            }
        }

        public int? _3_RepeatingDataElementPosition
        {
            get { return _repeatingDataElementPosition; }
            set
            {
                _repeatingDataElementPosition = value;

            }
        }
    }
}
