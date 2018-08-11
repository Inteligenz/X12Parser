namespace OopFactory.X12.Shared.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using OopFactory.X12.Specifications;

    public class Transaction : HierarchicalLoopContainer
    {
        private List<string> loopStartingSegmentIds;
        private List<string> loopWithLoopsStartingSegmentIds;

        internal Transaction(Container parent, X12DelimiterSet delimiters, string segment, TransactionSpecification spec)
            : base(parent, delimiters, segment)
        {
            this.Specification = spec;
        }

        public FunctionGroup FunctionGroup => (FunctionGroup)this.Parent;

        public TransactionSpecification Specification { get; }

        public string IdentifierCode
        {
            get { return this.GetElement(1); }
            set { this.SetElement(1, value); }
        }
        public string ControlNumber
        {
            get { return this.GetElement(2); }
            set { this.SetElement(2, value); }
        }

        internal override IList<LoopSpecification> AllowedChildLoops =>
            this.Specification != null ? this.Specification.LoopSpecifications : new List<LoopSpecification>();

        internal override IList<SegmentSpecification> AllowedChildSegments => 
            this.Specification != null ? this.Specification.SegmentSpecifications : new List<SegmentSpecification>();
        
        public override bool AllowsHierarchicalLoop(string levelCode)
        {
            return this.Specification
                .HierarchicalLoopSpecifications
                .Exists(hl => hl.LevelCode == levelCode || hl.LevelCode == null || hl.LevelCode == string.Empty);
        }

        public override HierarchicalLoop AddHLoop(string id, string levelCode, bool? willHoldChildHLoops)
        {
            var hloop = base.AddHLoop(string.Format("HL{0}{1}{0}{0}{2}{0}", this.DelimiterSet.ElementSeparator, id, levelCode));
            if (willHoldChildHLoops.HasValue)
            {
                hloop.HierarchicalChildCode = willHoldChildHLoops.Value ? "1" : "0";
            }
            return hloop;
        }
        
        internal override void Initialize(string segment)
        {
            base.Initialize(segment);
            this.loopStartingSegmentIds = new List<string> { "NM1" };
            this.loopWithLoopsStartingSegmentIds = new List<string>();
        }

        internal override IEnumerable<string> TrailerSegmentIds
        {
            get
            {
                var list = new List<string>();

                foreach (var spec in this.Specification.SegmentSpecifications.Where(ss => ss.Trailer == true))
                {
                    list.Add(spec.SegmentId);
                }

                return list;
            }
        }

        public override string ToX12String(bool addWhitespace)
        {
            this.UpdateTrailerSegmentCount("SE", 1, this.CountTotalSegments());
            return base.ToX12String(addWhitespace);
        }

        internal override void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.SegmentId))
            {
                writer.WriteStartElement("Transaction");
                writer.WriteAttributeString("ControlNumber", this.ControlNumber);

                base.WriteXml(writer);

                writer.WriteEndElement();
            }
        }
    }
}
