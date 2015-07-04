
namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementPositionInSegment : BaseElementReference
    {
        public TypedElementPositionInSegment(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }

        public override string ToString()
        {
            string value = string.Format("{1}{0}{2}{0}{3}",
               Segment._delimiters.SubElementSeparator,
               _1_ElementPositionInSegment,
               _2_ComponentDataElementPositionInComposite,
               _3_RepeatingDataElementPosition);
            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }

        public int? _1_ElementPositionInSegment { get; set; }
        public int? _2_ComponentDataElementPositionInComposite { get; set; }
        public int? _3_RepeatingDataElementPosition { get; set; }
    }
}
