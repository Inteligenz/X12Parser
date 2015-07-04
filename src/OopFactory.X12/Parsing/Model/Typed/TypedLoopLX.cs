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

        public TypedLoopLX(Loop loop) : base(loop) { }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            string segmentString = GetSegmentString(delimiters);

            Loop = new Loop(parent, delimiters, segmentString, loopSpecification);
        }

        public string LX01_AssignedNumber
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }
    }
}
