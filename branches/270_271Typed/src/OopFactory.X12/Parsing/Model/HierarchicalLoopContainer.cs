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
        protected Dictionary<string, HierarchicalLoop> _allHLoops;

        private Dictionary<string, HierarchicalLoop> _hLoops;

        internal HierarchicalLoopContainer(Container parent, X12DelimiterSet delimiters, string startingSegment)
            : base(parent, delimiters, startingSegment)
        {
            _allHLoops = new Dictionary<string, HierarchicalLoop>();

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

        internal void AddToHLoopDictionary(HierarchicalLoop hloop)
        {
            _allHLoops.Add(hloop.Id, hloop);
        }

        public HierarchicalLoop FindHLoop(string id)
        {
            if (_allHLoops.ContainsKey(id))
                return _allHLoops[id];
            else
                return null;
        }
        public bool HasHierachicalSpecs()
        {
            if (this is Transaction)
                return true;
            else if (this is HierarchicalLoop)
                return false;
            else if (this is Loop)
                return (((Loop)this).Specification.HierarchicalLoopSpecifications.Count > 0);
            else
                return false;
        }

        internal HierarchicalLoop AddHLoop(string segmentString)
        {
            Transaction transaction = this.Transaction;

            var hl = new HierarchicalLoop(this, _delimiters, segmentString);

            HierarchicalLoopContainer specContainer = this;
            while (!(specContainer is HierarchicalLoopContainer && specContainer.HasHierachicalSpecs()))
            {
                if (specContainer.Parent is HierarchicalLoopContainer)
                    specContainer = (HierarchicalLoopContainer)specContainer.Parent;
                else
                    throw new InvalidOperationException(string.Format("Cannot find specification for hierarichal loop {0}", segmentString));
            }

            if (specContainer is Transaction)
            {
                hl.Specification = transaction.Specification.HierarchicalLoopSpecifications.FirstOrDefault(
                    hls => hls.LevelCode == null || hls.LevelCode.ToString() == hl.LevelCode);

            }
            else if (specContainer is HierarchicalLoop)
            {
                HierarchicalLoop loopWithSpec = (HierarchicalLoop)specContainer;
                hl.Specification = loopWithSpec.Specification.HierarchicalLoopSpecifications.FirstOrDefault(
                    hls => hls.LevelCode == null || hls.LevelCode.ToString() == hl.LevelCode);
            }
            else if (specContainer is Loop)
            {
                Loop loopWithSpec = (Loop)specContainer;
                hl.Specification = loopWithSpec.Specification.HierarchicalLoopSpecifications.FirstOrDefault(
                    hls => hls.LevelCode == null || hls.LevelCode.ToString() == hl.LevelCode);
            }
            if (hl.Specification == null)
                throw new TransactionValidationException("{0} Transaction does not expect {2} level code value {3} that appears in transaction control number {1}.",
                    transaction.IdentifierCode, transaction.ControlNumber, "HL03", hl.LevelCode);

            _hLoops.Add(hl.Id, hl);
            // loop id must be unique throughout the transaction
            try
            {
                specContainer.AddToHLoopDictionary(hl);
            }
            catch (ArgumentException exc)
            {
                if (exc.Message == "An item with the same key has already been added.")
                    throw new TransactionValidationException("Hierarchical Loop ID {3} cannot be added to {0} transaction with control number {1} because it already exists.",
                        transaction.IdentifierCode, transaction.ControlNumber, "HL01", hl.Id);
                else
                    throw;
            }
            return hl;
        }

        public abstract bool AllowsHierarchicalLoop(string levelCode);

        public abstract HierarchicalLoop AddHLoop(string id, string levelCode, bool? existingHierarchalLoops);
        
        internal override int CountTotalSegments()
        {
            return base.CountTotalSegments() + HLoops.Sum(hl => hl.CountTotalSegments());
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

                foreach (var hloop in this.HLoops)
                    hloop.WriteXml(writer);

                foreach (var segment in this.TrailerSegments)
                    segment.WriteXml(writer);
            }
        }
    }
}
