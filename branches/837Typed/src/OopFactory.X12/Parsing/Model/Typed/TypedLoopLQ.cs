using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopLQ : TypedLoop
    {
        private string _entityIdentifer;

        public TypedLoopLQ(string entityIdentifier)
            : base("LQ")
        {
            _entityIdentifer = entityIdentifier;
        }

        internal override string GetSegmentString(X12DelimiterSet delimiters)
        {
            return String.Format("{0}{1}{2}", _segmentId, delimiters.ElementSeparator, _entityIdentifer);
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            string segmentString = GetSegmentString(delimiters);

            _loop = new Loop(parent, delimiters, segmentString, loopSpecification);
        }

        public string LQ01_CodeListQualifierCode {
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public string LQ02_IndustryCode {
            get { return _loop.GetElement(2); }
            set { _loop.SetElement(2, value); }
        }
	}
}