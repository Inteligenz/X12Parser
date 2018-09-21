namespace OopFactory.X12.Shared.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;
    
    using OopFactory.X12.Specifications;

    public class HierarchicalLoop : HierarchicalLoopContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalLoop"/> class with the provided parameters
        /// </summary>
        /// <param name="parent">Parent container</param>
        /// <param name="delimiters">Delimiter set indicating how to segregate segments and elements</param>
        /// <param name="segment">Segment string for the loop</param>
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
        public string Id => this.GetElement(1);

        [XmlAttribute("ParentId")]
        public string ParentId => this.GetElement(2);

        public string LevelCode => this.GetElement(3);

        public string HierarchicalChildCode
        {
            get { return this.GetElement(4); }
            internal set { this.SetElement(4, value); }
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
            if (!string.IsNullOrEmpty(this.SegmentId))
            {
                writer.WriteStartElement("HierarchicalLoop");

                if (this.Specification != null)
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
            return $"Loop(Id={this.Id},ParentId={this.ParentId},Level={this.LevelCode},ChildLoops={this.Loops.Count()}, ChildSegments={this.Segments.Count})";
        }
    }
}
