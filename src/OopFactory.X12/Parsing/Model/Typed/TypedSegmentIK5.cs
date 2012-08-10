using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentIK5 : TypedSegment
    {
        public TypedSegmentIK5() : base("IK5") { }

        public string IK501_TransactionSetAcknowledgmentCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string IK502_SyntaxErrorCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string IK503_SyntaxErrorCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }
        public string IK504_SyntaxErrorCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }
        public string IK505_SyntaxErrorCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }
        public string IK506_SyntaxErrorCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }
    }
}
