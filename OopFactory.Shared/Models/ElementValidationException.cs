namespace OopFactory.X12.Shared.Models
{
    using System;

    /// <summary>
    /// Represents a validation exception of an X12 element
    /// </summary>
    public class ElementValidationException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementValidationException"/> class
        /// </summary>
        /// <param name="formatString">Base string to format into Message</param>
        /// <param name="elementId">Element id when exception was thrown</param>
        /// <param name="value">Value of element when exception was thrown</param>
        /// <param name="args">Additional exception arguments</param>
        public ElementValidationException(string formatString, string elementId, string value, params object[] args)
            : base(
                string.Format(
                    formatString,
                    elementId,
                    value,
                    args.Length > 0 ? args[0] : null,
                    args.Length > 1 ? args[1] : null,
                    args.Length > 2? args[2] : null),
                elementId)
        {
            this.ElementId = elementId;
            this.Value = value;
        }
        
        /// <summary>
        /// Gets the id of element at time of exception
        /// </summary>
        public string ElementId { get; }

        /// <summary>
        /// Gets the value of element at time of exception
        /// </summary>
        public string Value { get; }
    }
}
