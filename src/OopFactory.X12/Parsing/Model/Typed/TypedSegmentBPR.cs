using System;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Beginning Segment for Payment Order/Remittance Advice
    /// </summary>
    public class TypedSegmentBPR : TypedSegment
    {
        public TypedSegmentBPR()
            : base("BPR")
        {
        }

        public TypedSegmentBPR(Segment seg) : base(seg) { }

        public string BPR01_TransactionHandlingCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }            
        }

        public decimal? BPR02_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string BPR03_CreditDebitFlagCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string BPR04_PaymentMethodCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string BPR05_PaymentFormatCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string BPR06_DFI_IdNumberQaulifier
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string BPR07_DFI_IdNumber
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string BPR08_AccountNumberQualifier
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string BPR09_AccountNumber
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string BPR10_OriginatingCompanyIdentifier
        {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public string BPR11_OriginatingCompanySupplementalCode
        {
            get { return _segment.GetElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public string BPR12_DFI_IdNumberQualifier
        {
            get { return _segment.GetElement(12); }
            set { _segment.SetElement(12, value); }
        }

        public string BPR13_DFI_IdNumber
        {
            get { return _segment.GetElement(13); }
            set { _segment.SetElement(13, value); }
        }

        public string BPR14_AccountNumberQualifier
        {
            get { return _segment.GetElement(14); }
            set { _segment.SetElement(14, value); }
        }

        public string BPR15_AccountNumber
        {
            get { return _segment.GetElement(15); }
            set { _segment.SetElement(15, value); }
        }

        public DateTime? BPR16_Date
        {
            get { return _segment.GetDate8Element(16); }
            set { _segment.SetDate8Element(16, value); }
        }

        public string BPR17_BusinessFunctionCode
        {
            get { return _segment.GetElement(17); }
            set { _segment.SetElement(17, value); }
        }

        public string BPR18_DFI_IdNumberQualifier
        {
            get { return _segment.GetElement(18); }
            set { _segment.SetElement(18, value); }
        }

        public string BPR19_DFI_IdNumber
        {
            get { return _segment.GetElement(19); }
            set { _segment.SetElement(19, value); }
        }

        public string BPR20_AccountNumberQualifier
        {
            get { return _segment.GetElement(20); }
            set { _segment.SetElement(20, value); }
        }

        public string BPR21_AccountNumber
        {
            get { return _segment.GetElement(21); }
            set { _segment.SetElement(21, value); }
        }
    }
}
