using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// ELIGIBILITY OR BENEFIT INFORMATION
    /// </summary>
    public class TypedLoopEB : TypedLoop
    {

        public TypedLoopEB() : base("EB") { }

        public TypedLoopEB(Loop loop) : base(loop) { }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            string segmentString = GetSegmentString(delimiters);

            Loop = new Loop(parent, delimiters, segmentString, loopSpecification);
        }

        public EligibilityOrBenefitInformation EB01_EligibilityOrBenefitInformationEnum
        {
            get { return Loop.GetElement(1).ToEnumFromEDIFieldValue<EligibilityOrBenefitInformation>(); }
            set { Loop.SetElement(1, value.EDIFieldValue()); }
        }

        public CoverageLevelCode? EB02_CoverageLevelCodeEnum
        {
            get { return Loop.GetElement(2).ToEnumFromEDIFieldValueSafe<CoverageLevelCode>(); }
            set { Loop.SetElement(2, value.EDIFieldValue()); }
        }
        public string EB02_CoverageLevelCode
        {
            get { return Loop.GetElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public IEnumerable<ServiceTypeCode> EB03_ServiceTypeCodeEnums
        {
            get { return Loop.GetElement(3).ToMultiEnumFromEDIFieldValueSafe<ServiceTypeCode>(Loop._delimiters.RepetitionSeparator); }
            set { Loop.SetElement(3, value.EDIFieldValue<ServiceTypeCode>(Loop._delimiters.RepetitionSeparator)); }
        }

        public InsuranceTypeCode? EB04_InsuranceTypeCodeEnum
        {
            get { return Loop.GetElement(4).ToEnumFromEDIFieldValueSafe<InsuranceTypeCode>(); }
            set { Loop.SetElement(4, value.EDIFieldValue()); }
        }

        public string EB05_PlanCoverageDescription
        {
            get { return Loop.GetElement(5); }
            set { Loop.SetElement(5, value); }
        }

        public TimePeriodQualifier? EB06_TimePeriodQualifierEnum
        {
            get { return Loop.GetElement(6).ToEnumFromEDIFieldValueSafe<TimePeriodQualifier>(); }
            set { Loop.SetElement(6, value.EDIFieldValue()); }
        }

        public string EB07_MonetaryAmount
        {
            get { return Loop.GetElement(7); }
            set { Loop.SetElement(7, value); }
        }

        public string EB08_Percent
        {
            get { return Loop.GetElement(8); }
            set { Loop.SetElement(8, value); }
        }


        public QuantityQualifier? EB09_QuantityQualifierEnum
        {
            get { return Loop.GetElement(9).ToEnumFromEDIFieldValueSafe<QuantityQualifier>(); }
            set { Loop.SetElement(9, value.EDIFieldValue()); }
        }

        public string EB10_Quantity
        {
            get { return Loop.GetElement(10); }
            set { Loop.SetElement(10, value); }
        }

        public YesNoConditionOrResponseCode? EB11_AuthorizationOrCertificationIndicator
        {
            get { return Loop.GetElement(11).ToEnumFromEDIFieldValueSafe<YesNoConditionOrResponseCode>(); }
            set { Loop.SetElement(11, value.EDIFieldValue()); }
        }

        public YesNoConditionOrResponseCode? EB12_InPlanNetworkIndicator
        {
            get { return Loop.GetElement(12).ToEnumFromEDIFieldValueSafe<YesNoConditionOrResponseCode>(); }
            set { Loop.SetElement(12, value.EDIFieldValue()); }
        }

        public string EB13_CompositeMedicalProcedure
        {
            get { return Loop.GetElement(13); }
            set { Loop.SetElement(13, value); }
        }
    }
}
