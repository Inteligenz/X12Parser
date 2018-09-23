namespace OopFactory.X12.Shared.Enumerations
{
    using OopFactory.X12.Shared.Attributes;

    public enum TimeCode
    {
        [EdiFieldValue("01")]
        EquivalentToIsoP01,

        [EdiFieldValue("02")]
        EquivalentToIsoP02,

        [EdiFieldValue("03")]
        EquivalentToIsoP03,

        [EdiFieldValue("04")]
        EquivalentToIsoP04,

        [EdiFieldValue("05")]
        EquivalentToIsoP05,

        [EdiFieldValue("06")]
        EquivalentToIsoP06,

        [EdiFieldValue("07")]
        EquivalentToIsoP07,

        [EdiFieldValue("08")]
        EquivalentToIsoP08,

        [EdiFieldValue("09")]
        EquivalentToIsoP09,

        [EdiFieldValue("10")]
        EquivalentToIsoP10,

        [EdiFieldValue("11")]
        EquivalentToIsoP11,

        [EdiFieldValue("12")]
        EquivalentToIsoP12,

        [EdiFieldValue("13")]
        EquivalentToIsoM12,

        [EdiFieldValue("14")]
        EquivalentToIsoM11,

        [EdiFieldValue("15")]
        EquivalentToIsoM10,

        [EdiFieldValue("16")]
        EquivalentToIsoM09,

        [EdiFieldValue("17")]
        EquivalentToIsoM08,

        [EdiFieldValue("18")]
        EquivalentToIsoM07,

        [EdiFieldValue("19")]
        EquivalentToIsoM06,

        [EdiFieldValue("20")]
        EquivalentToIsoM05,

        [EdiFieldValue("21")]
        EquivalentToIsoM04,

        [EdiFieldValue("22")]
        EquivalentToIsoM03,

        [EdiFieldValue("23")]
        EquivalentToIsoM02,

        [EdiFieldValue("24")]
        EquivalentToIsoM01,

        [EdiFieldValue("AD")]
        AlaskaDaylightTime,

        [EdiFieldValue("AS")]
        AlaskaStandardTime,

        [EdiFieldValue("AT")]
        AlaskaTime,

        [EdiFieldValue("CD")]
        CentralDaylightTime,

        [EdiFieldValue("CS")]
        CentralStandardTime,

        [EdiFieldValue("CT")]
        CentralTime,

        [EdiFieldValue("ED")]
        EasternDaylightTime,

        [EdiFieldValue("ES")]
        EasternStandardTime,

        [EdiFieldValue("ET")]
        EasternTime,

        [EdiFieldValue("GM")]
        GreenwichMeanTime,

        [EdiFieldValue("HD")]
        Hawaii_AleutianDaylightTime,

        [EdiFieldValue("HS")]
        Hawaii_AleutianStandardTime,

        [EdiFieldValue("HT")]
        Hawaii_AleutianTime,

        [EdiFieldValue("LT")]
        LocalTime,

        [EdiFieldValue("MD")]
        MountainDaylightTime,

        [EdiFieldValue("MS")]
        MountainStandardTime,

        [EdiFieldValue("MT")]
        MountainTime,

        [EdiFieldValue("ND")]
        NewfoundlandDaylightTime,

        [EdiFieldValue("NS")]
        NewfoundlandStandardTime,

        [EdiFieldValue("NT")]
        NewfoundlandTime,

        [EdiFieldValue("PD")]
        PacificDaylightTime,

        [EdiFieldValue("PS")]
        PacificStandardTime,

        [EdiFieldValue("PT")]
        PacificTime,

        [EdiFieldValue("TD")]
        AtlanticDaylightTime,

        [EdiFieldValue("TS")]
        AtlanticStandardTime,

        [EdiFieldValue("TT")]
        AtlanticTime,

        [EdiFieldValue("UT")]
        UniversalTimeCoordinate
    }
}
