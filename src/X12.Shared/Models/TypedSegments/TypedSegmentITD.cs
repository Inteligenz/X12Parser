namespace X12.Shared.Models.TypedSegments
{
    using System;

    /// <summary>
    /// Terms of Sale/Deferred Terms of Sale
    /// </summary>
    public class TypedSegmentITD : TypedSegment
    {
        public TypedSegmentITD()
            : base("ITD")
        {
        }

        public string ITD01_TermsTypeCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        /// <summary>
        /// 1 = Ship Date
        /// 2 = Delivery Date
        /// 3 = Invoice Date
        /// </summary>
        public string ITD02_TermsBasisDateCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public decimal? ITD03_TermsDiscountPercent
        {
            get { return this.Segment.GetDecimalElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public DateTime? ITD04_TermsDiscountDueDate
        {
            get { return this.Segment.GetDate8Element(4); }
            set { this.Segment.SetDate8Element(4, value); }
        }

        public int? ITD05_TermsDiscountDaysDue
        {
            get { return this.Segment.GetIntElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public DateTime? ITD06_TermsNetDueDate
        {
            get { return this.Segment.GetDate8Element(6); }
            set { this.Segment.SetDate8Element(6, value); }
        }

        public int? ITD07_TermsNetDays
        {
            get { return this.Segment.GetIntElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? ITD08_TermsDiscountAmountN2
        {
            get { return this.Segment.GetIntElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public DateTime? ITD09_TermsDeferredDueDate
        {
            get { return this.Segment.GetDate8Element(9); }
            set { this.Segment.SetDate8Element(9, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? ITD10_DeferredAmountDueN2
        {
            get { return this.Segment.GetIntElement(10); }
            set { this.Segment.SetElement(10, value); }
        }

        public decimal? ITD11_PercentOfInvoicePayable
        {
            get { return this.Segment.GetDecimalElement(11); }
            set { this.Segment.SetElement(11, value); }
        }

        public string ITD12_Description
        {
            get { return this.Segment.GetElement(12); }
            set { this.Segment.SetElement(12, value); }
        }

        public int? ITD13_DayOfMonth
        {
            get
            {
                return this.Segment.GetIntElement(13);
            }

            set 
            {
                if (value >= 1 && value <= 31)
                {
                    this.Segment.SetElement(13, value);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(this.ITD13_DayOfMonth),
                        $"{value} is not a value between 1 and 31.");
                }
            }
        }

        public string ITD14_PaymentMethodCode
        {
            get { return this.Segment.GetElement(14); }
            set { this.Segment.SetElement(14, value); }
        }

        public decimal? ITD15_Percent
        {
            get { return this.Segment.GetDecimalElement(15); }
            set { this.Segment.SetElement(15, value); }
        }
    }
}
