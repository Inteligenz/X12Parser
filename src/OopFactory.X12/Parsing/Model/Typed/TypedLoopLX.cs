using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopLX : TypedLoop
    {
        private string _entityIdentifer;

        public TypedLoopLX(string entityIdentifier) 
            : base("LX")
        {
            _entityIdentifer = entityIdentifier;
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            string segmentString = GetSegmentString(delimiters);

            _loop = new Loop(parent, delimiters, segmentString, loopSpecification);
        }

        public string LX01_AssignedNumber
        {
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }
    }
}
