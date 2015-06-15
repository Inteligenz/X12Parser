using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopIK3 : TypedLoop
    {
        public TypedLoopIK3() : base("IK3") { }

        public string IK301_SegmentIdCode
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public int? IK302_SegmentPositionInTransactionSet
        {
            get
            {
                int position;
                if (int.TryParse(Loop.GetElement(2), out position))
                    return position;
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    Loop.SetElement(2, value.ToString());
                else
                    Loop.SetElement(2, "");
            }
        }

        public string IK303_LoopIdentifierCode
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public string IK304_SyntaxErrorCode
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }
    }
}
