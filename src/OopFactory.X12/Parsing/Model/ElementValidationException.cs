using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model
{
    public class ElementValidationException : ArgumentException
    {
        public ElementValidationException(string formatString, string elementId, string value, params object[] args)
            : base(String.Format(formatString, elementId, value, args), elementId)
        {
            ElementId = elementId;
            Value = value;
        }

        public string ElementId { get; private set; }
        public string Value { get; private set; }
    }
}
