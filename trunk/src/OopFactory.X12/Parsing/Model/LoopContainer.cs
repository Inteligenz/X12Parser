using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public abstract class LoopContainer : Container
    {
        private List<Loop> _loops;

        internal LoopContainer(X12DelimiterSet delimiters, string startingSegment)
            : base(delimiters, startingSegment)
        {
        }

        protected override void Initialize(string segment)
        {
            base.Initialize(segment);
            _loops = new List<Loop>();
        }

        public abstract IList<LoopSpecification> AllowedChildLoops { get; }


        public List<Loop> Loops
        {
            get { return _loops; }
        }

    }
}
