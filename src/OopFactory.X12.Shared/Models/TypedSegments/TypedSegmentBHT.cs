namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using System;

    public class TypedSegmentBHT : TypedSegment
    {
        public TypedSegmentBHT()
            : base("BHT")
        {
        }

        public string BHT01_HierarchicalStructureCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string BHT02_TransactionSetPurposeCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string BHT03_ReferenceIdentification
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public DateTime? BHT04_Date
        {
            get { return this.Segment.GetDate8Element(4); }
            set { this.Segment.SetDate8Element(4, value); }
        }

        public string BHT05_Time
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string BHT06_TransactionTypeCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }
    }
}
