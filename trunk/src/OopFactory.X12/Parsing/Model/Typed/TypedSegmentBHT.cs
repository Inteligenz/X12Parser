using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentBHT : TypedSegment
    {
        public TypedSegmentBHT()
            : base("BHT")
        {
        }

        public string BHT01_HierarchicalStructureCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string BHT02_TransactionSetPurposeCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string BHT03_ReferenceIdentification
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public DateTime? BHT04_Date
        {
            get { return _segment.GetDate8Element(4); }
            set { _segment.SetDate8Element(4, value); }
        }

        public string BHT05_Time
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string BHT06_TransactionTypeCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }
    }
}
