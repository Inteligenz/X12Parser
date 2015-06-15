using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    /// <summary>
    /// Code which specifies the time for routine shipments or deliveries
    /// </summary>
    public enum ShipDeliveryPatternTimeCode
    {

        [EDIFieldValue("A")]
        _1stShift_NormalWorkingHours,
        [EDIFieldValue("B")]
        _2ndShift,
        [EDIFieldValue("C")]
        _3rdShift,
        [EDIFieldValue("D")]
        AM,
        [EDIFieldValue("E")]
        PM,
        [EDIFieldValue("F")]
        AsDirected,
        [EDIFieldValue("G")]
        AnyShift,
        /// <summary>
        /// (Also Used to Cancel or Override a Previous Pattern)
        /// </summary>
        [EDIFieldValue("Y")]
        None,
    }
}
