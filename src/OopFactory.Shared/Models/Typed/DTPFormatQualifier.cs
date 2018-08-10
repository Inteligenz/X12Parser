namespace OopFactory.X12.Shared.Models.Typed
{
    using OopFactory.X12.Shared.Attributes;

    public enum DTPFormatQualifier
    {
        [EdiFieldValue("CC")]
        CC,

        [EdiFieldValue("CD")]
        MMYYYY,

        [EdiFieldValue("CM")]
        CCYYMM,

        [EdiFieldValue("CQ")]
        CCYYQ,

        [EdiFieldValue("CY")]
        CCYY,

        [EdiFieldValue("D6")]
        YYMMDD,
        
        [EdiFieldValue("D8")]
        CCYYMMDD,

        [EdiFieldValue("DA")]
        DD_DD,

        [EdiFieldValue("DB")]
        MMDDCCYY,

        [EdiFieldValue("DD")]
        DD,

        /// <summary>
        /// Last Digit of Year and Julian Date Expressed in Format YDDD
        /// </summary>
        [EdiFieldValue("EH")]
        YDDD,

        [EdiFieldValue("KA")]
        YYMMMDD,

        [EdiFieldValue("MD")]
        MMDD,

        [EdiFieldValue("MM")]
        MM,

        [EdiFieldValue("RD")]
        MMDDCCYY_MMDDCCYY,

        /// <summary>
        /// Julian Date Expressed in Format DDD
        /// </summary>
        [EdiFieldValue("TC")]
        DDD,

        [EdiFieldValue("TM")]
        HHMM,

        [EdiFieldValue("TQ")]
        MMYY,

        [EdiFieldValue("TR")]
        DDMMYYHHMM,

        [EdiFieldValue("TS")]
        HHMMSS,

        [EdiFieldValue("TT")]
        MMDDYY,

        [EdiFieldValue("TU")]
        YYDDD,

        [EdiFieldValue("UN")]
        Unstructured,

        [EdiFieldValue("YM")]
        YYMM,

        [EdiFieldValue("YY")]
        YY,

        [EdiFieldValue("DTS")]
        CCYYMMDDHHMMSS_CCYYMMDDHHMMSS,

        [EdiFieldValue("RD2")]
        YY_YY,

        [EdiFieldValue("RD4")]
        CCYY_CCYY,

        [EdiFieldValue("RD5")]
        CCYYMM_CCYYMM,

        [EdiFieldValue("RD6")]
        YYMMDD_YYMMDD,

        [EdiFieldValue("RD8")]
        CCYYMMDD_CCYYMMDD,

        [EdiFieldValue("RDM")]
        YYMMDD_MMDD,

        [EdiFieldValue("RDT")]
        CCYYMMDDHHMM_CCYYMMDDHHMM,

        [EdiFieldValue("RMD")]
        MMDD_MMDD,

        [EdiFieldValue("RMY")]
        YYMM_YYMM,

        [EdiFieldValue("RTM")]
        HHMM_HHMM,

        [EdiFieldValue("RTS")]
        CCYYMMDDHHMMSS,

        [EdiFieldValue("YMM")]
        CCYYMMM_MMM
    }
}
