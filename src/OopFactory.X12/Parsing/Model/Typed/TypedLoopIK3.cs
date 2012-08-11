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
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public int? IK302_SegmentPositionInTransactionSet
        {
            get
            {
                int position;
                if (int.TryParse(_loop.GetElement(2), out position))
                    return position;
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    _loop.SetElement(2, value.ToString());
                else
                    _loop.SetElement(2, "");
            }
        }

        public string IK303_LoopIdentifierCode
        {
            get { return _loop.GetElement(3); }
            set { _loop.SetElement(3, value); }
        }

        public string IK304_SyntaxErrorCode
        {
            get { return _loop.GetElement(4); }
            set { _loop.SetElement(4, value); }
        }
    }
}
