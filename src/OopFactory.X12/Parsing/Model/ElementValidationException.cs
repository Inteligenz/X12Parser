using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model
{
    public class ElementValidationException : ArgumentException
    {
        public ElementValidationException(string segmentId, int elementNumber, string value, string message)
            : base(message)
        {
            ElementId = String.Format("{0}{1:00}", segmentId, elementNumber);
            Value = value;
        }

        public string ElementId { get; private set; }
        public string Value { get; private set; }
    }
}
