using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopCLP : TypedLoop
    {
        public TypedLoopCLP() : base("CLP") { }
        public TypedLoopCLP(Loop loop) : base(loop) { }

        public string CLP01_ClaimSubmittersIdentifier
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public string CLP02_ClaimStatusCode
        {
            get { return Loop.GetElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public decimal? CLP03_MonetaryAmount
        {
            get { return Loop.GetDecimalElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public decimal? CLP04_MonetaryAmount
        {
            get { return Loop.GetDecimalElement(4); }
            set { Loop.SetElement(4, value); }
        }

        public decimal? CLP05_MonetaryAmount
        {
            get { return Loop.GetDecimalElement(5); }
            set { Loop.SetElement(5, value); }
        }

        public string CLP06_ClaimFilingIndicatorCode
        {
            get { return Loop.GetElement(6); }
            set { Loop.SetElement(6, value); }
        }

        public string CLP07_ReferenceIdentification
        {
            get { return Loop.GetElement(7); }
            set { Loop.SetElement(7, value); }
        }

        public string CLP08_FacilityCodeValue
        {
            get { return Loop.GetElement(8); }
            set { Loop.SetElement(8, value); }
        }

        public string CLP09_ClaimFrequencyTypeCode
        {
            get { return Loop.GetElement(9); }
            set { Loop.SetElement(9, value); }
        }

        public string CLP10_PatientStatusCode
        {
            get { return Loop.GetElement(10); }
            set { Loop.SetElement(10, value); }
        }

        public string CLP11_DiagnosisRelatedGroupCode
        {
            get { return Loop.GetElement(11); }
            set { Loop.SetElement(11, value); }
        }

        public decimal? CLP12_Quantity
        {
            get { return Loop.GetDecimalElement(12); }
            set { Loop.SetElement(12, value); }
        }

        public decimal? CLP13_PercentageAsDecimal
        {
            get { return Loop.GetDecimalElement(13); }
            set { Loop.SetElement(13, value); }
        }

        public YesNoConditionOrResponseCode? CLP14_PatientAuthorizationToCoordinateBenefits
        {
            get { return Loop.GetElement(14).ToEnumFromEDIFieldValueSafe<YesNoConditionOrResponseCode>(); }
            set { Loop.SetElement(14, value.EDIFieldValueSafe()); }
        }
    }
}
