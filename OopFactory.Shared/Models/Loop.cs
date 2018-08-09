namespace OopFactory.X12.Shared.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using OopFactory.X12.Specifications;

    public class Loop : HierarchicalLoopContainer
    {
        internal Loop(Container parent, X12DelimiterSet delimiters, string startingSegment, LoopSpecification loopSpecification)
            : base(parent, delimiters, startingSegment)
        {
            this.Specification = loopSpecification;
        }

        public LoopSpecification Specification { get; }

        internal override IList<LoopSpecification> AllowedChildLoops => this.Specification.LoopSpecifications;

        internal override IList<SegmentSpecification> AllowedChildSegments => this.Specification.SegmentSpecifications;

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

        public override bool AllowsHierarchicalLoop(string levelCode)
        {
            return this.Specification
                .HierarchicalLoopSpecifications
                .Exists(hl => hl.LevelCode == levelCode 
                              || hl.LevelCode == null 
                              || hl.LevelCode == string.Empty);
        }

        public override HierarchicalLoop AddHLoop(string id, string levelCode, bool? willHoldChildHLoops)
        {
            var hloop = base.AddHLoop(string.Format("HL{0}{1}{0}{2}{0}{3}{0}", this.DelimiterSet.ElementSeparator, id, string.Empty, levelCode));
            if (willHoldChildHLoops.HasValue)
            {
                hloop.HierarchicalChildCode = willHoldChildHLoops.Value ? "1" : "0";
            }

            return hloop;
        }

        #region IXmlSerializable Members

        internal override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.SegmentId))
            {
                writer.WriteStartElement("Loop");

                if (this.Specification != null)
                {
                    writer.WriteAttributeString("LoopId", this.Specification.LoopId);
                    writer.WriteAttributeString("Name", this.Specification.Name);
                }

                base.WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
