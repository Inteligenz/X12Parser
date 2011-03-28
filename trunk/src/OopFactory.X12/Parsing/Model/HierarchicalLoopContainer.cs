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

        internal HierarchicalLoopContainer(Container parent, X12DelimiterSet delimiters, string startingSegment)
            : base(parent, delimiters, startingSegment)
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

        public HierarchicalLoop CreateHLoop(Container parent, Transaction transaction, string segmentString)
        {
            var hl = new HierarchicalLoop(parent, _delimiters, segmentString);

            hl.Specification = transaction.Specification.HierarchicalLoopSpecifications.FirstOrDefault(
                hls => hls.LevelCode.ToString() == hl.LevelCode);

            if (hl.Specification == null)
                throw new InvalidOperationException(String.Format("HL with level code {0} is not expected in transaction set {1}.",
                    hl.LevelCode, transaction.Specification.TransactionSetIdentifierCode));
            return hl;
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

                foreach (var segment in this.TerminatingSegments)
                    segment.WriteXml(writer);
            }
        }
    }
}
