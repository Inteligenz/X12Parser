namespace X12.Specifications.Enumerations
{
    using System.Xml.Serialization;

    /// <summary>
    /// Collection of segment requirement indicators
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum Requirement
    {
        /// <summary>
        /// Segment is mandatory
        /// </summary>
        Mandatory,

        /// <summary>
        /// Segment is optional
        /// </summary>
        Optional
    }

    /// <summary>
    /// Collection of segment usage indicators
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum Usage
    {
        /// <summary>
        /// Segment is required
        /// </summary>
        Required,

        /// <summary>
        /// Segment may be required depending on situation
        /// </summary>
        Situational,

        /// <summary>
        /// Segment is not used
        /// </summary>
        [XmlEnum("Not Used")]
        NotUsed
    }

    /// <summary>
    /// Collection of data type indicators
    /// </summary>
    [XmlType(AnonymousType = true)]
    public enum ElementDataType
    {
        /// <summary>
        /// String data type
        /// </summary>
        [XmlEnum("AN")]
        String,

        /// <summary>
        /// Numeric data type
        /// </summary>
        [XmlEnum("N")]
        Numeric,

        /// <summary>
        /// Decimal data type
        /// </summary>
        [XmlEnum("R")]
        Decimal,

        /// <summary>
        /// Identifier data type
        /// </summary>
        [XmlEnum("ID")]
        Identifier,

        /// <summary>
        /// Date data type
        /// </summary>
        [XmlEnum("DT")]
        Date,

        /// <summary>
        /// Time data type
        /// </summary>
        [XmlEnum("TM")]
        Time,

        /// <summary>
        /// Binary data type
        /// </summary>
        [XmlEnum("B")]
        Binary
    }
}
