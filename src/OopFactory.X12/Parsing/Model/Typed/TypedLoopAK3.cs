using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopAK3 : TypedLoop
    {
        public TypedLoopAK3() : base("AK3") { }
        public TypedLoopAK3(Loop loop) : base(loop) { }

        public string AK301_SegmentIdCode
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public decimal? AK302_SegmentPositionInTransaction
        {
            get { return Loop.GetDecimalElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public string AK303_LoopIdentifierCode
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public string AK304_SegmentSyntaxErrorCode
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }
    }
}
