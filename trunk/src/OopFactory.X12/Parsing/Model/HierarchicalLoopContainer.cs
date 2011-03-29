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
        private Dictionary<string, HierarchicalLoop> _hLoops;

        internal HierarchicalLoopContainer(Container parent, X12DelimiterSet delimiters, string startingSegment)
            : base(parent, delimiters, startingSegment)
        {
        }

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            _hLoops = new Dictionary<string,HierarchicalLoop>();
        }

        public IEnumerable<HierarchicalLoop> HLoops
        {
            get { return _hLoops.Values; }
        }

        internal HierarchicalLoop AddHLoop(Transaction transaction, string segmentString)
        {
            var hl = new HierarchicalLoop(this, _delimiters, segmentString);

            hl.Specification = transaction.Specification.HierarchicalLoopSpecifications.FirstOrDefault(
                hls => hls.LevelCode.ToString() == hl.LevelCode);

            if (hl.Specification == null)
                throw new InvalidOperationException(String.Format("HL with level code {0} is not expected in transaction set {1}.",
                    hl.LevelCode, transaction.Specification.TransactionSetIdentifierCode));

            _hLoops.Add(hl.Id, hl);
            return hl;
        }

        internal override string SerializeBodyToX12(bool addWhitespace)
        {
            StringBuilder sb = new StringBuilder(base.SerializeBodyToX12(addWhitespace));
            foreach (var hloop in HLoops)
                sb.Append(hloop.ToX12String(addWhitespace));

            return sb.ToString();
        }

        internal override void WriteXml(System.Xml.XmlWriter writer)
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
