using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Attributes
{
    public class EDIFieldValueAttribute : Attribute
    {
        public string Value { get; private set; }
        public EDIFieldValueAttribute(string value)
        {
            this.Value = value;

        }
    }
}
