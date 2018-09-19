namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using System;

    /// <summary>
    /// Beginning Segment for Invoice
    /// </summary>
    public class TypedSegmentBIG : TypedSegment
    {
        public TypedSegmentBIG()
            : base("BIG")
        {
        }

        public DateTime? BIG01_InvoiceDate
        {
            get { return this.Segment.GetDate8Element(1); }
            set { this.Segment.SetDate8Element(1, value); }            
        }

        public string BIG02_InvoiceNumber
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public DateTime? BIG03_PurchaseOrderDate
        {
            get { return this.Segment.GetDate8Element(3); }
            set { this.Segment.SetDate8Element(3, value); }
        }

        public string BIG04_PurchaseOrderNumber
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        /// <summary>
        /// CN = Credit Invoice
        /// CR = Credit Memo
        /// DI = Debit Invoice
        /// DR = Debit Memo
        /// </summary>
        public string BIG07_TransactionTypeCode
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string BIG08_TransactionSetPurposeCode
        {
            get { return this.Segment.GetElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string BIG09_ActionCode
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }

        public string BIG10_InvoiceNumber
        {
            get { return this.Segment.GetElement(10); }
            set { this.Segment.SetElement(10, value); }
        }
    }
}
