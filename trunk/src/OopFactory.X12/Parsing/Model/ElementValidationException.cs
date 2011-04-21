using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model
{
    public class ElementValidationException : ArgumentException
    {
        public ElementValidationException(string formatString, string elementId, string value, params object[] args)
            : base(String.Format(formatString, elementId, value, args.Length > 0 ? args[0] : null, args.Length > 1 ? args[1] : null, args.Length > 2 ? args[2] : null), elementId)
        {
            ElementId = elementId;
            Value = value;
        }
        
        public string ElementId { get; private set; }
        public string Value { get; private set; }
    }
}
