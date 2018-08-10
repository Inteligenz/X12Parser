namespace OopFactory.X12.Shared.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    
    using OopFactory.X12.Specifications;

    public class HierarchicalLoop : HierarchicalLoopContainer
    {
        internal HierarchicalLoop(Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
        }

        public HierarchicalLoopSpecification Specification { get; internal set; }

        internal override IList<LoopSpecification> AllowedChildLoops => 
            this.Specification != null ? this.Specification.LoopSpecifications : new List<LoopSpecification>();

        internal override IList<SegmentSpecification> AllowedChildSegments =>
            this.Specification != null ? this.Specification.SegmentSpecifications : new List<SegmentSpecification>();

        [XmlAttribute("Id")]
        public string Id
        {
            get { return GetElement(1); }
        }

        [XmlAttribute("ParentId")]
        public string ParentId
        {
            get { return GetElement(2); }
        }

        public string LevelCode
        {
            get { return GetElement(3); }
        }

        public string HierarchicalChildCode
        {
            get { return GetElement(4); }
            internal set { SetElement(4, value); }
        }

        public override bool AllowsHierarchicalLoop(string levelCode)
        {
            return true;
        }

        public override HierarchicalLoop AddHLoop(string id, string levelCode, bool? willHoldChildHLoops)
        {
            var hloop = base.AddHLoop(string.Format("HL{0}{1}{0}{2}{0}{3}{0}", this.DelimiterSet.ElementSeparator, id, this.Id, levelCode));
            if (willHoldChildHLoops.HasValue)
            {
                hloop.HierarchicalChildCode = willHoldChildHLoops.Value ? "1" : "0";
            }

            return hloop;
        }

        internal override IEnumerable<string> TrailerSegmentIds
        {
            get
            {
                var list = new List<string>();

                foreach (var spec in this.Specification.SegmentSpecifications.Where(ss => ss.Trailer))
                {
                    list.Add(spec.SegmentId);
                }

                return list;
            }
        }

        internal override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(base.SegmentId))
            {
                writer.WriteStartElement("HierarchicalLoop");

                if (Specification != null)
                {
                    writer.WriteAttributeString("LoopId", this.Specification.LoopId);
                    writer.WriteAttributeString("LoopName", this.Specification.Name);
                }

                writer.WriteAttributeString("Id", this.Id);
                writer.WriteAttributeString("ParentId", this.ParentId);

                base.WriteXml(writer);

                writer.WriteEndElement();
            }
        }

        public override string ToString()
        {
            return string.Format(
                "Loop(Id={0},ParentId={1},Level={2},ChildLoops={3}, ChildSegments={4})",
                this.Id,
                this.ParentId,
                this.LevelCode,
                this.Loops.Count(),
                this.Segments.Count());
        }
    }
}
