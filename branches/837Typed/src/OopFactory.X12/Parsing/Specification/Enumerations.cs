using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OopFactory.X12.Parsing.Specification
{
    [XmlType(AnonymousType = true)]
    public enum RequirementEnum
    {
        Mandatory,
        Optional
    }

    [XmlType(AnonymousType = true)]
    public enum UsageEnum
    {
        Required,
        Situational,
        [XmlEnum("Not Used")]
        NotUsed
    }

    [XmlType(AnonymousType = true)]
    public enum ElementDataTypeEnum
    {
        [XmlEnum("AN")]
        String,
        [XmlEnum("N")]
        Numeric,
        [XmlEnum("R")]
        Decimal,
        [XmlEnum("ID")]
        Identifier,
        [XmlEnum("DT")]
        Date,
        [XmlEnum("TM")]
        Time,
        [XmlEnum("B")]
        Binary
    }
}
