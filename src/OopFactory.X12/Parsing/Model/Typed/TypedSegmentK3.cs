using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Note/Secial Instruction
    /// </summary>
    public class TypedSegmentK3 : TypedSegment
    {
        public TypedSegmentK3()
            : base("K3")
        {
        }
        public TypedSegmentK3(Segment segment) : base(segment) { }

        public string K301_FixedFormatInformation
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string K302_RecordFormatCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public TypedElementCompositeUnitOfMeasure K303_CompositeUnitOfMeasure {
            get { return new TypedElementCompositeUnitOfMeasure(_segment, 3);  }
            set { _segment.SetElement(3, value); }
        }
    }
}
