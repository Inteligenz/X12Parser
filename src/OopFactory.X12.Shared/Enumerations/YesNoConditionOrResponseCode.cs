namespace OopFactory.X12.Shared.Enumerations
{
    using OopFactory.X12.Shared.Attributes;

    /// <summary>
    /// Condition or response codes
    /// </summary>
    public enum YesNoConditionOrResponseCode
    {
        /// <summary>
        /// No condition response
        /// </summary>
        [EdiFieldValue("N")]
        No,

        /// <summary>
        /// Unknown condition response
        /// </summary>
        [EdiFieldValue("U")]
        Unknown,

        /// <summary>
        /// Not applicable condition response
        /// </summary>
        [EdiFieldValue("W")]
        NotApplicable,

        /// <summary>
        /// Yes condition response
        /// </summary>
        [EdiFieldValue("Y")]
        Yes
    }
}
