using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Health Care Pricing, to specify pricing or repricing information about a health care claim or line item
    /// </summary>
    public class TypedSegmentHCP : TypedSegment
    {
        public TypedSegmentHCP()
            : base("HCP")
        {
        }

        public string HCP01_PricingMethodology
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public PricingMethodology HCP01_PricingMethodologyEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<PricingMethodology>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public decimal? HCP02_AllowedAmount
        {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? HCP03_SavingsAmount
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string HCP04_RepricingOrganizationIdentificationNumber
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public decimal? HCP05_Rate
        {
            get { return _segment.GetDecimalElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string HCP06_ApprovedDrgCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public decimal? HCP07_ApprovedDrgAmount
        {
            get { return _segment.GetDecimalElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string HCP08_ApprovedRevenueCode
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string HCP09_Qualifier
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string HCP10_ApprovedProcedureCode
        {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public string HCP11_UnitOrBasisForMeasurementCode
        {
            get { return _segment.GetElement(11); }
            set { _segment.SetElement(11, value); }
        }

        public UnitOrBasisOfMeasurementCode HCP11_UnitOrBasisOfMeasurementCodeEnum
        {
            get { return _segment.GetElement(11).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { _segment.SetElement(11, value.EDIFieldValue()); }
        }

        public decimal? HCP12_Quantity
        {
            get { return _segment.GetDecimalElement(12); }
            set { _segment.SetElement(12, value); }
        }

        public string HCP13_RejectReasonCode
        {
            get { return _segment.GetElement(13); }
            set { _segment.SetElement(13, value); }
        }

        public string HCP14_PolicyComplianceCode
        {
            get { return _segment.GetElement(14); }
            set { _segment.SetElement(14, value); }
        }

        public string HCP15_ExceptionCode
        {
            get { return _segment.GetElement(15); }
            set { _segment.SetElement(15, value); }
        }

    }
}
