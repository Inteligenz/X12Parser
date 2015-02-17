using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentAK9 : TypedSegment
    {
        public TypedSegmentAK9()
            : base("AK9")
        {
        }

        public string AK901_FunctionalGroupAcknowledgeCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public int AK902_NumberOfTransactionSetsIncluded
        {
            get
            {
                int count;
                if (int.TryParse(_segment.GetElement(2), out count))
                    return count;
                else
                    return 0;

            }
            set { _segment.SetElement(2, value.ToString()); }
        }

        public int AK903_NumberOfReceivedTransactionSets
        {
            get
            {
                int count;
                if (int.TryParse(_segment.GetElement(3), out count))
                    return count;
                else
                    return 0;

            }
            set { _segment.SetElement(3, value.ToString()); }
        }

        public int AK904_NumberOfAcceptedTransactionSets
        {
            get
            {
                int count;
                if (int.TryParse(_segment.GetElement(4), out count))
                    return count;
                else
                    return 0;

            }
            set { _segment.SetElement(4, value.ToString()); }
        }

        public string AK905_FunctionalGroupSyntaxErrorCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string AK906_FunctionalGroupSyntaxErrorCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }
        public string AK907_FunctionalGroupSyntaxErrorCode
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }
        public string AK908_FunctionalGroupSyntaxErrorCode
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }
        public string AK909_FunctionalGroupSyntaxErrorCode
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }
    }
}
