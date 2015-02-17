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
                return _loop.GetIntElement(2);
            }
            set
            {
                _loop.SetElement(2, value);
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
