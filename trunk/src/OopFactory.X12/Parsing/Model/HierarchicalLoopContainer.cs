using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace OopFactory.X12.Parsing.Model
{
    public abstract class HierarchicalLoopContainer : LoopContainer
    {
        private List<HierarchicalLoop> _hLoops;

        internal HierarchicalLoopContainer(X12DelimiterSet delimiters, string startingSegment)
            : base(delimiters, startingSegment)
        {
        }

        protected override void Initialize(string segment)
        {
            base.Initialize(segment);
            _hLoops = new List<HierarchicalLoop>();
        }

        public List<HierarchicalLoop> HLoops
        {
            get { return _hLoops; }
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(base.SegmentId))
            {
                base.WriteXml(writer);

                foreach (var segment in this.Segments)
                    segment.WriteXml(writer);

                foreach (var loop in this.Loops)
                    loop.WriteXml(writer);

                foreach (var hloop in this.HLoops)
                    hloop.WriteXml(writer);

            }
        }
    }
}
