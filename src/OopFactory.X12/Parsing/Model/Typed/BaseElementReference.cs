using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public abstract class BaseElementReference
    {
        public int ElementNumber { get; set; }
        public Segment Segment;

        public BaseElementReference(Segment _segment, int _elementNumber)
        {
            Segment = _segment;
            ElementNumber = _elementNumber;
        }
    }
}
