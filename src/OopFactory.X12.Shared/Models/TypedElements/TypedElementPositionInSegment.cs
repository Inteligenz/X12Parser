namespace OopFactory.X12.Shared.Models.TypedElements
{
    public class TypedElementPositionInSegment
    {
        private readonly int elementNumber;

        private readonly Segment segment;

        private int? elementPositionInSegment;

        private int? componentDataElementPositionInComposite;

        private int? repeatingDataElementPosition;

        internal TypedElementPositionInSegment(Segment segment, int elementNumber)
        {
            this.segment = segment;
            this.elementNumber = elementNumber;
        }

        private void UpdateElement()
        {
            string value = string.Format(
                "{1}{0}{2}{0}{3}",
                this.segment.Delimiters.SubElementSeparator,
                this.elementPositionInSegment,
                this.componentDataElementPositionInComposite,
                this.repeatingDataElementPosition);
            value = value.TrimEnd(this.segment.Delimiters.SubElementSeparator);
            this.segment.SetElement(this.elementNumber, value);
        }

        public int? _1_ElementPositionInSegment
        {
            get
            {
                return this.elementPositionInSegment;
            }

            set
            {
                this.elementPositionInSegment = value;
                this.UpdateElement();
            }
        }

        public int? _2_ComponentDataElementPositionInComposite
        {
            get
            {
                return this.componentDataElementPositionInComposite;
            }

            set
            {
                this.componentDataElementPositionInComposite = value;
                this.UpdateElement();
            }
        }

        public int? _3_RepeatingDataElementPosition
        {
            get
            {
                return this.repeatingDataElementPosition;
            }

            set
            {
                this.repeatingDataElementPosition = value;
                this.UpdateElement();
            }
        }
    }
}
