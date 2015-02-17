using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Message Text
    /// </summary>
    public class TypedSegmentMSG : TypedSegment
    {
        public TypedSegmentMSG()
            : base("MSG")
        {
        }

        public string MSG01_FreeFormMessageText
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string MSG02_PrinterCarriageControlCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public int? MSG03_Number
        {
            get { return _segment.GetIntElement(3); }
            set { _segment.SetElement(3, value); }
        }
    }
}
