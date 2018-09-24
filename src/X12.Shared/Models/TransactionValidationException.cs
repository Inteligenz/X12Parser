namespace X12.Shared.Models
{
    using System;

    public class TransactionValidationException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionValidationException"/> class
        /// </summary>
        /// <param name="formatString">String message to be printed</param>
        /// <param name="transactionCode">Transaction code</param>
        /// <param name="controlNumber">Transaction control number</param>
        /// <param name="elementId">Element ID</param>
        /// <param name="value">Element value</param>
        /// <param name="args">Additional arguments</param>
        public TransactionValidationException(
            string formatString,
            string transactionCode,
            string controlNumber,
            string elementId,
            string value,
            params object[] args)
            : base(string.Format(formatString, transactionCode, controlNumber, elementId, value, args.Length > 0 ? args[0] : null, args.Length > 1 ? args[1] : null), transactionCode)
        {
            this.TransactionCode = transactionCode;
            this.ControlNumber = controlNumber;
            this.ElementId = elementId;
            this.Value = value;
        }

        /// <summary>
        /// Gets the transaction code when the exception was thrown
        /// </summary>
        public string TransactionCode { get; }

        /// <summary>
        /// Gets the control number for the transaction
        /// </summary>
        public string ControlNumber { get; }

        /// <summary>
        /// Gets the element id when the exception was thrown
        /// </summary>
        public string ElementId { get; }

        /// <summary>
        /// Gets a value that the exception was thrown with
        /// </summary>
        public string Value { get; }
    }
}
