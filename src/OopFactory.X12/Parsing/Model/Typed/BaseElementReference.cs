using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public abstract class BaseElementReference
    {
        public int ElementNumber { get; set; }
        public IEnumerable<string> SubElements { get; set; }
        public Segment Segment;

        public BaseElementReference(Segment _segment, int _elementNumber)
        {
            Segment = _segment;
            ElementNumber = _elementNumber;
            var elementString = Segment.GetElement(_elementNumber);
            if (!string.IsNullOrWhiteSpace(elementString))
            {
                SubElements = elementString.Split(_segment.Delimiters.SubElementSeparator);
            }
            else
            {
                SubElements = new string[] { };
            }
        }
    }
}
