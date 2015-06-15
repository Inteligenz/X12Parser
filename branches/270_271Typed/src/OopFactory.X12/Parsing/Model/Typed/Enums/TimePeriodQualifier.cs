using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum TimePeriodQualifier
    {
        [EDIFieldValue("6")]
        Hour,
        [EDIFieldValue("7")]
        Day,
        [EDIFieldValue("13")]
        _24Hours,
        [EDIFieldValue("21")]
        Years,
        [EDIFieldValue("22")]
        ServiceYear,
        [EDIFieldValue("23")]
        CalendarYear,
        [EDIFieldValue("24")]
        YearToDate,
        [EDIFieldValue("25")]
        Contract,
        [EDIFieldValue("26")]
        Episode,
        [EDIFieldValue("27")]
        Visit,
        [EDIFieldValue("28")]
        Outlier,
        [EDIFieldValue("29")]
        Remaining,
        [EDIFieldValue("30")]
        Exceeded,
        [EDIFieldValue("31")]
        NotExceeded,
        [EDIFieldValue("32")]
        Lifetime,
        [EDIFieldValue("33")]
        LifetimeRemaining,
        [EDIFieldValue("34")]
        Month,
        [EDIFieldValue("35")]
        Week,
        [EDIFieldValue("36")]
        Admisson,
    }
}
