using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Attributes;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentDTP : TypedSegment
    {

        public TypedSegmentDTP() : base("DTP") { }

        public DTPQualifier DTP01_DateTimeQualifier
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<DTPQualifier>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public DTPFormatQualifier DTP02_DateTimePeriodFormatQualifier
        {
            get { return _segment.GetElement(2).ToEnumFromEDIFieldValue<DTPFormatQualifier>(); }
            set { _segment.SetElement(2, value.EDIFieldValue()); }
        }

        public DateTime? DTP03_Date
        {
            get
            {
                string element = _segment.GetElement(3);
                if (element.Length == 8)
                    return DateTime.ParseExact(element, "yyyyMMdd", null);
                return null;
            }
            set
            {
                _segment.SetElement(3, String.Format("{0:yyyyMMdd}", value));
            }
        }

    }

    public enum DTPQualifier
    {

        [EDIFieldValue("096")] 
        Discharge,

        [EDIFieldValue("102")] 
        Issue,

        [EDIFieldValue("152")] 
        EffectiveDateOfChange,

        [EDIFieldValue("291")] 
         Plan,

        [EDIFieldValue("307")] 
        Eligibility,

        [EDIFieldValue("318")] 
         Added,

        [EDIFieldValue("340")] 
         ConsolidatedOmnibusBudgetReconciliationAct,

        [EDIFieldValue("341")] 
         ConsolidatedOmnibusBudgetReconciliationAct_COBRA,

        [EDIFieldValue("342")] 
         PremiumPaidToDateBegin,

        [EDIFieldValue("343")] 
         PremiumPaidToDateEnd,

        [EDIFieldValue("346")] 
        PlanBegin,

        [EDIFieldValue("347")] 
         PlanEnd,

        [EDIFieldValue("356")] 
         EligibilityBegin,

        [EDIFieldValue("357")] 
         EligibilityEnd,

        [EDIFieldValue("382")] 
         Enrollment,
        
        [EDIFieldValue("435")] 
         Admission,

        [EDIFieldValue("442")] 
         DateOfDeath,

        [EDIFieldValue("458")] 
         Certification,

        [EDIFieldValue("472")] 
         Service,

        [EDIFieldValue("539")] 
         PolicyEffective,

        [EDIFieldValue("540")] 
         PolicyExpiration,

        [EDIFieldValue("636")] 
         DateOfLastUpdate,

        [EDIFieldValue("771")] 
         Status

    }

    public enum DTPFormatQualifier
    {
        [EDIFieldValue("D8")]
        CCYYMMDD,

        [EDIFieldValue("RD8")]
        CCYYMMDD_CCYYMMDD
    }


    

}
