using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model
{
    public class TransactionValidationException : ArgumentException
    {
        public TransactionValidationException(string formatString, string transactionCode, string controlNumber, string elementId, string value, params object[] args)
            : base(String.Format(formatString, transactionCode, controlNumber, elementId, value, args.Length > 0 ? args[0] : null, args.Length > 1 ? args[1] : null), transactionCode)
        {
            TransactionCode = transactionCode;
            ControlNumber = controlNumber;
            ElementId = elementId;
            Value = value;
        }

        public string TransactionCode { get; private set; }
        public string ControlNumber { get; private set; }
        public string ElementId { get; private set; }
        public string Value { get; private set; }
    }
}
