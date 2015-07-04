using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentAK5 : TypedSegment
    {
        public TypedSegmentAK5() : base("AK5") { }
        public TypedSegmentAK5(Segment seg) : base(seg) { }

        public string AK501_TransactionSetAcknowledgmentCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string AK502_TransactionSetSyntaxErrorCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string AK503_TransactionSetSyntaxErrorCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string AK504_TransactionSetSyntaxErrorCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string AK505_TransactionSetSyntaxErrorCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string AK506_TransactionSetSyntaxErrorCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }
    }
}
