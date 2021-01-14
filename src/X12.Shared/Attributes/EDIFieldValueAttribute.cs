namespace X12.Shared.Attributes
{
    using System;

    using X12.Shared.Properties;

    /// <summary>
    /// Represents an X12 field value
    /// </summary>
    public class EdiFieldValueAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdiFieldValueAttribute"/> class
        /// </summary>
        /// <param name="value">Data to set value to</param>
        /// <exception cref="ArgumentNullException">Thrown if value is null or whitespace</exception>
        public EdiFieldValueAttribute(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value), Resources.NullStringArgument);
            }

            this.Value = value;
        }

        /// <summary>
        /// Gets the value set for the field
        /// </summary>
        public string Value { get; }
    }
}
