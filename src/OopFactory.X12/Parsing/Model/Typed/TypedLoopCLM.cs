using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;


namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopCLM : TypedLoop
    {
        public TypedLoopCLM() : base("CLM") { }
        public TypedLoopCLM(Loop loop) : base(loop) { }

        public TypedElementServiceLocationInfo CreateTypedElementServiceLocationInfo(PlaceOfServiceCodes FacilityCodeValue, string FacilityCodeQualifier, string ClaimFrequencyTypeCode)
        {
            return new TypedElementServiceLocationInfo(Loop, 5)
            {
                _1_FacilityCodeValue = FacilityCodeValue,
                _2_FacilityCodeQualifier = FacilityCodeQualifier,
                _3_ClaimFrequencyTypeCode = ClaimFrequencyTypeCode
            };
        }

        public TypedElementRelatedCausesInfo CreateTypedElementRelatedCausesInfo(string RelatedCausesCode1, string RelatedCausesCode2, string RelatedCausesCode3)
        {
            return new TypedElementRelatedCausesInfo(Loop, 11)
            {
                _1_RelatedCausesCode = RelatedCausesCode1,
                _2_RelatedCausesCode = RelatedCausesCode2,
                _3_RelatedCausesCode = RelatedCausesCode3
            };
        }

        public string CLM01_PatientControlNumber
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public decimal CLM02_TotalClaimChargeAmount
        {
            get
            {
                decimal amount;
                if (decimal.TryParse(Loop.GetElement(2), out amount))
                    return amount;
                else
                    return 0;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Total Claim Charge Amount must be greater than or equal to zero.");
                Loop.SetElement(2, value.ToString().TrimStart('0'));
            }
        }

        public string CLM03_ClaimFilingIndicatorCode
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public string CLM04_NonInstitutionalClaimTypeCode
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }

        public TypedElementServiceLocationInfo CLM05
        {
            get { return new TypedElementServiceLocationInfo(Loop, 5); }
            set { Loop.SetElement(5, value); }
        }

        public YesNoConditionOrResponseCode CLM06_ProviderOrSupplierSignatureIndicator
        {
            get { return Loop.GetElement(6).ToEnumFromEDIFieldValue<YesNoConditionOrResponseCode>(); }
            set { Loop.SetElement(6, value.EDIFieldValue()); }
        }

        public string CLM07_ProviderAcceptAssignmentCode
        {
            get { return Loop.GetElement(7); }
            set { Loop.SetElement(7, value); }
        }

        public YesNoConditionOrResponseCode CLM08_BenefitsAssignmentCerficationIndicator
        {
            get { return Loop.GetElement(8).ToEnumFromEDIFieldValue<YesNoConditionOrResponseCode>(); }
            set { Loop.SetElement(8, value.EDIFieldValue()); }
        }

        public string CLM09_ReleaseOfInformationCode
        {
            get { return Loop.GetElement(9); }
            set { Loop.SetElement(9, value); }
        }

        public string CLM10_PatientSignatureSourceCode
        {
            get { return Loop.GetElement(10); }
            set { Loop.SetElement(10, value); }
        }

        public TypedElementRelatedCausesInfo CLM11
        {
            get { return new TypedElementRelatedCausesInfo(Loop, 11); }
            set { Loop.SetElement(11, value); }
        }

        public string CLM12_SpecialProgramCode
        {
            get { return Loop.GetElement(12); }
            set { Loop.SetElement(12, value); }
        }

        public string CLM20_DelayReasonCode
        {
            get { return Loop.GetElement(20); }
            set { Loop.SetElement(20, value); }
        }
    }
}
