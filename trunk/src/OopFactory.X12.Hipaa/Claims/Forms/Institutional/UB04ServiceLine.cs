using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
#if DEBUG
    [Serializable]
    public class UB04ServiceLine_2300Loop
    {
        /*
         * 2011/8/15, jhalliday - New Data Model for 837I (Institutional) claim - collections objects.
         * 
         * Team: dstrubhar, jhalliday and epkrause
         * 
         * Purpose:
         * To create a C# object model that will serve as a container for the X12 837I data
         * AS ENTERED from a UB-04 CMS-1450 (formerly UB-92) Institutional (Inpatient Hospital) claim form.
         * The classes in this namespace are used to add collection types to the main UB04Claim class
         * for value that repeat on the UB-04 form.
         * 
         * Goal:
         * The team has the overall goal of creating tools that can be used to consume and
         * manipulate X12 messages (AKA files/documents) without the need to have a big project
         * budget.  For that reason, this and the related X12 Parser project tools are all open
         * source and freely usable.
         */
        public string Field43_InvestigationalDeviceExemptionIdentifier { get; set; }
        //public decimal Field47_TotalSummary { get; set; }
        //public string Field48_NonCoveredTotalSummary { get; set; }
    }

    [Serializable]
    public class UB04ServiceLine_2400Loop
    {

        public string Field42_RevenueCode { get; set; }
        public string Field44_HCPCS { get; set; }
        public string Field44_HCPCS_RateCode { get; set; }
        public string Field44_AccommodationRate { get; set; }
        public string Field44_HCPCS_Modifier_1 { get; set; }
        public string Field44_HCPCS_Modifier_2 { get; set; }
        public string Field44_HCPCS_Modifier_3 { get; set; }
        public string Field44_HCPCS_Modifier_4 { get; set; }
        public string Field45_ServiceDate { get; set; }
        public string Field46_ServiceUnits { get; set; }
        public decimal Field47_CoveredLineItem { get; set; }
        public string Field48_NonCoveredLineItem { get; set; }
    }

    [Serializable]
    public class UB04ServiceLine_2410Loop
    {
        public string Field43_NDC_Number { get; set; }
        public string Field43_Quantity { get; set; }
    }
        
    //[Serializable]
    //public class UB04OccurrenceCodesAndDates
    //{
    //    public string Field31_34_OccurrenceCode { get; set; }
    //    public string Field31_34_OccurrenceDate { get; set; }
    //}

    //[Serializable]
    //public class UB04OccurrenceSpanCodesAndDates
    //{
    //    public string Field35_36_OccurrenceSpanCode { get; set; }
    //    public string Field35_36_OccurrenceSpanDate { get; set; }
    //}

    //[Serializable]
    //public class UB04ValueCodesAndAmounts
    //{
    //    public string ValueCode { get; set; }
    //    public decimal Amount { get; set; }
    //}

    //[Serializable]
    //public class UB04TotalChargesLine
    //{
    //    public string ServiceLineTotals_PageNumber { get; set; }
    //    public string ServiceLineTotals_CreationDate { get; set; }
    //    public decimal ServiceLineTotals_TotalCharges { get; set; }
    //}


    //[Serializable]
    //public class UB04Code_Code
    //{
    //    public string Field81a { get; set; }
    //    public string Field81b { get; set; }
    //    public string Field81c { get; set; }
    //}
#endif
}
