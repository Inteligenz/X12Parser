using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopAK3 : TypedLoop
    {
        public TypedLoopAK3() : base("AK3") { }

        public string AK301_SegmentIdCode
        {
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public decimal? AK302_SegmentPositionInTransaction
        {
            get { return _loop.GetDecimalElement(2); }
            set { _loop.SetElement(2, value); }
        }

        public string AK303_LoopIdentifierCode
        {
            get { return _loop.GetElement(3); }
            set { _loop.SetElement(3, value); }
        }

        public string AK304_SegmentSyntaxErrorCode {
            get { return _loop.GetElement(4); }
            set { _loop.SetElement(4, value); }
        }
    }
}
