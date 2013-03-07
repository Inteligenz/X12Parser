using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Attributes;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public enum DTPFormatQualifier
    {
        [EDIFieldValue("CC")]
        CC,

        [EDIFieldValue("CD")]
        MMYYYY,

        [EDIFieldValue("CM")]
        CCYYMM,

        [EDIFieldValue("CQ")]
        CCYYQ,

        [EDIFieldValue("CY")]
        CCYY,

        [EDIFieldValue("D6")]
        YYMMDD,
        
        [EDIFieldValue("D8")]
        CCYYMMDD,

        [EDIFieldValue("DA")]
        DD_DD,

        [EDIFieldValue("DB")]
        MMDDCCYY,

        [EDIFieldValue("DD")]
        DD,

        /// <summary>
        /// Last Digit of Year and Julian Date Expressed in Format YDDD
        /// </summary>
        [EDIFieldValue("EH")]
        YDDD,

        [EDIFieldValue("KA")]
        YYMMMDD,

        [EDIFieldValue("MD")]
        MMDD,

        [EDIFieldValue("MM")]
        MM,

        [EDIFieldValue("RD")]
        MMDDCCYY_MMDDCCYY,

        /// <summary>
        /// Julian Date Expressed in Format DDD
        /// </summary>
        [EDIFieldValue("TC")]
        DDD,

        [EDIFieldValue("TM")]
        HHMM,

        [EDIFieldValue("TQ")]
        MMYY,

        [EDIFieldValue("TR")]
        DDMMYYHHMM,

        [EDIFieldValue("TS")]
        HHMMSS,

        [EDIFieldValue("TT")]
        MMDDYY,

        [EDIFieldValue("TU")]
        YYDDD,

        [EDIFieldValue("UN")]
        Unstructured,

        [EDIFieldValue("YM")]
        YYMM,

        [EDIFieldValue("YY")]
        YY,

        [EDIFieldValue("DTS")]
        CCYYMMDDHHMMSS_CCYYMMDDHHMMSS,

        [EDIFieldValue("RD2")]
        YY_YY,

        [EDIFieldValue("RD4")]
        CCYY_CCYY,

        [EDIFieldValue("RD5")]
        CCYYMM_CCYYMM,

        [EDIFieldValue("RD6")]
        YYMMDD_YYMMDD,

        [EDIFieldValue("RD8")]
        CCYYMMDD_CCYYMMDD,

        [EDIFieldValue("RDM")]
        YYMMDD_MMDD,

        [EDIFieldValue("RDT")]
        CCYYMMDDHHMM_CCYYMMDDHHMM,

        [EDIFieldValue("RMD")]
        MMDD_MMDD,

        [EDIFieldValue("RMY")]
        YYMM_YYMM,

        [EDIFieldValue("RTM")]
        HHMM_HHMM,

        [EDIFieldValue("RTS")]
        CCYYMMDDHHMMSS,

        [EDIFieldValue("YMM")]
        CCYYMMM_MMM
    }
}
