using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
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
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        /// <summary>
        /// 1 = Ship Date
        /// 2 = Delivery Date
        /// 3 = Invoice Date
        /// </summary>
        public string ITD02_TermsBasisDateCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? ITD03_TermsDiscountPercent
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public DateTime? ITD04_TermsDiscountDueDate
        {
            get { return _segment.GetDate8Element(4); }
            set { _segment.SetDate8Element(4, value); }
        }

        public int? ITD05_TermsDiscountDaysDue
        {
            get { return _segment.GetIntElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public DateTime? ITD06_TermsNetDueDate
        {
            get { return _segment.GetDate8Element(6); }
            set { _segment.SetDate8Element(6, value); }
        }

        public int? ITD07_TermsNetDays
        {
            get { return _segment.GetIntElement(7); }
            set { _segment.SetElement(7, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? ITD08_TermsDiscountAmountN2
        {
            get { return _segment.GetIntElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public DateTime? ITD09_TermsDeferredDueDate
        {
            get { return _segment.GetDate8Element(9); }
            set { _segment.SetDate8Element(9, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? ITD10_DeferredAmountDueN2
        {
            get { return _segment.GetIntElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public decimal? ITD11_PercentOfInvoicePayable
        {
            get { return _segment.GetDecimalElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public string ITD12_Description
        {
            get { return _segment.GetElement(12); }
            set { _segment.SetElement(12, value); }
        }

        public int? ITD13_DayOfMonth
        {
            get { return _segment.GetIntElement(13); }
            set 
            {
                if (value >= 1 && value <= 31)
                    _segment.SetElement(13, value);
                else
                    throw new ArgumentOutOfRangeException("ITD13_DayOfMonth", string.Format("{0} is not a value between 1 and 31.", value));
            }
        }

        public string ITD14_PaymentMethodCode
        {
            get { return _segment.GetElement(14); }
            set { _segment.SetElement(14, value); }
        }

        public decimal? ITD15_Percent
        {
            get { return _segment.GetDecimalElement(15); }
            set { _segment.SetElement(15, value); }
        }
    }
}
