using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementPositionInSegment
    {
        private int _elementNumber;
        private Segment _segment;
        private int? _elementPositionInSegment;
        private int? _componentDataElementPositionInComposite;
        private int? _repeatingDataElementPosition;

        internal TypedElementPositionInSegment(Segment segment, int elementNumber)
        {
            _segment = segment;
            _elementNumber = elementNumber;
        }

        private void UpdateElement()
        {
            string value = string.Format("{1}{0}{2}{0}{3}",
                _segment._delimiters.SubElementSeparator,
                _elementPositionInSegment, _componentDataElementPositionInComposite, _repeatingDataElementPosition);
            value = value.TrimEnd(_segment._delimiters.SubElementSeparator);
            _segment.SetElement(_elementNumber, value);
        }

        public int? _1_ElementPositionInSegment
        {
            get { return _elementPositionInSegment; }
            set
            {
                _elementPositionInSegment = value;
                UpdateElement();
            }
        }

        public int? _2_ComponentDataElementPositionInComposite
        {
            get { return _componentDataElementPositionInComposite; }
            set
            {
                _componentDataElementPositionInComposite = value;
                UpdateElement();
            }
        }

        public int? _3_RepeatingDataElementPosition
        {
            get { return _repeatingDataElementPosition; }
            set
            {
                _repeatingDataElementPosition = value;
                UpdateElement();
            }
        }
    }
}
