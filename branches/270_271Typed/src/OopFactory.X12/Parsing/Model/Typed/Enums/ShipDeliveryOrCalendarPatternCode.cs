using OopFactory.X12.Attributes;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    /// <summary>
    /// Code which specifies the routine shipments, deliveries, or calendar pattern
    /// </summary>
    public enum ShipDeliveryOrCalendarPatternCode
    {
        [EDIFieldValue("1")]
        _1stWeekOfTheMonth,
        [EDIFieldValue("2")]
        _2ndWeekOfTheMonth,
        [EDIFieldValue("3")]
        _3rdWeekOfTheMonth,
        [EDIFieldValue("4")]
        _4thWeekOfTheMonth,
        [EDIFieldValue("5")]
        _5thWeekOfTheMonth,
        [EDIFieldValue("6")]
        _1stAnd3rdWeeksOfTheMonth,
        [EDIFieldValue("7")]
        _2ndAnd4thWeeksOfTheMonth,
        [EDIFieldValue("8")]
        _1stWorkingDayOfPeriod,
        [EDIFieldValue("9")]
        LastWorkingDayOfPeriod,
        [EDIFieldValue("A")]
        MondayThroughFriday,
        [EDIFieldValue("B")]
        MondayThroughSaturday,
        [EDIFieldValue("C")]
        MondayThroughSunday,
        [EDIFieldValue("D")]
        Monday,
        [EDIFieldValue("E")]
        Tuesday,
        [EDIFieldValue("F")]
        Wednesday,
        [EDIFieldValue("G")]
        Thursday,
        [EDIFieldValue("H")]
        Friday,
        [EDIFieldValue("J")]
        Saturday,
        [EDIFieldValue("K")]
        Sunday,
        [EDIFieldValue("L")]
        MondayThroughThursday,
        [EDIFieldValue("M")]
        Immediately,
        [EDIFieldValue("N")]
        AsDirected,
        [EDIFieldValue("O")]
        DailyMonThroughFri,
        [EDIFieldValue("P")]
        HalfOnMonAndHalfOnThurs,
        [EDIFieldValue("Q")]
        HalfOnTuesAndHalfOnThurs,
        [EDIFieldValue("R")]
        HalfOnWedAndHalfOnFri,
        [EDIFieldValue("S")]
        OnceAnytimeMonThroughFri,
        [EDIFieldValue("SG")]
        TuesdayThroughFriday,
        [EDIFieldValue("SL")]
        MondayTuesdayAndThursday,
        [EDIFieldValue("SP")]
        MondayTuesdayAndFriday,
        [EDIFieldValue("SX")]
        WednesdayAndThursday,
        [EDIFieldValue("SY")]
        MondayWednesdayAndThursday,
        [EDIFieldValue("SZ")]
        TuesdayThursdayAndFriday,
        [EDIFieldValue("T")]
        HalfOnTueAndHalfOnFri,
        [EDIFieldValue("U")]
        HalfOnMonAndHalfOnWed,
        [EDIFieldValue("V")]
        AThirdOnMonAThirdOnWedAThirdOnFri,
        [EDIFieldValue("W")]
        WheneverNecessary,
        [EDIFieldValue("X")]
        HalfByWedBalByFri,
        /// <summary>
        /// (Also Used to Cancel or Override a Previous Pattern)
        /// </summary>
        [EDIFieldValue("Y")]
        None,
    }
}
