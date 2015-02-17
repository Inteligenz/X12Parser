using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public class Transaction : HierarchicalLoopContainer
    {
        private List<string> _loopStartingSegmentIds;
        private List<string> _loopWithLoopsStartingSegmentIds;

        internal Transaction(Container parent, X12DelimiterSet delimiters, string segment, TransactionSpecification spec)
            : base(parent, delimiters, segment)
        {
            Specification = spec;
        }

        public FunctionGroup FunctionGroup
        {
            get { return (FunctionGroup)Parent; }
        }

        public TransactionSpecification Specification { get; private set; }

        public string IdentifierCode
        {
            get { return GetElement(1); }
            set { SetElement(1, value); }
        }
        public string ControlNumber
        {
            get { return GetElement(2); }
            set { SetElement(2, value); }
        }
        
        internal override IList<LoopSpecification> AllowedChildLoops
        {
            get
            {
                if (Specification != null)
                    return Specification.LoopSpecifications;
                else
                    return new List<LoopSpecification>();
            }
        }

        internal override IList<SegmentSpecification> AllowedChildSegments
        {
            get
            {
                if (Specification != null)
                    return Specification.SegmentSpecifications;
                else
                    return new List<SegmentSpecification>();
            }
        }

        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            _loopStartingSegmentIds = new List<string>();
            _loopStartingSegmentIds.Add("NM1");
            _loopWithLoopsStartingSegmentIds = new List<string>();
        }

        

        public override bool AllowsHierarchicalLoop(string levelCode)
        {
            return this.Specification.HierarchicalLoopSpecifications.Exists(hl => hl.LevelCode == levelCode || hl.LevelCode == null || hl.LevelCode == "");
        }

        public override HierarchicalLoop AddHLoop(string id, string levelCode, bool? willHoldChildHLoops)
        {
            var hloop = base.AddHLoop(string.Format("HL{0}{1}{0}{0}{2}{0}", 
                _delimiters.ElementSeparator, id, levelCode));
            if (willHoldChildHLoops.HasValue)
                hloop.HierarchicalChildCode = willHoldChildHLoops.Value ? "1" : "0";
            return hloop;
        }

        internal override IEnumerable<string> TrailerSegmentIds
        {
            get
            {
                var list = new List<string>();

                foreach (var spec in Specification.SegmentSpecifications.Where(ss => ss.Trailer == true))
                    list.Add(spec.SegmentId);
                return list;
            }
        }

        internal override string ToX12String(bool addWhitespace)
        {
            UpdateTrailerSegmentCount("SE", 1, CountTotalSegments());
            return base.ToX12String(addWhitespace);
        }

        internal override void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(SegmentId))
            {
                writer.WriteStartElement("Transaction");
                writer.WriteAttributeString("ControlNumber", ControlNumber);

                base.WriteXml(writer);

                writer.WriteEndElement();
            }
        }


    }
}
