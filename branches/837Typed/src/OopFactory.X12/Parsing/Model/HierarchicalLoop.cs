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
    public class HierarchicalLoop : HierarchicalLoopContainer
    {
        internal HierarchicalLoop(Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
        }

        public HierarchicalLoopSpecification Specification { get; internal set; }

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
            var hloop = base.AddHLoop(string.Format("HL{0}{1}{0}{2}{0}{3}{0}", _delimiters.ElementSeparator, id, this.Id, levelCode));
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

        internal override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(base.SegmentId))
            {
                writer.WriteStartElement("HierarchicalLoop");

                if (Specification != null)
                {
                    writer.WriteAttributeString("LoopId", Specification.LoopId);
                    writer.WriteAttributeString("LoopName", Specification.Name);
                }

                writer.WriteAttributeString("Id", this.Id);
                writer.WriteAttributeString("ParentId", this.ParentId);

                base.WriteXml(writer);

                writer.WriteEndElement();
            }
        }

        public override string ToString()
        {
            return String.Format("Loop(Id={0},ParentId={1},Level={2},ChildLoops={3}, ChildSegments={4})", Id, ParentId, LevelCode, Loops.Count(), Segments.Count());
        }
    }
}
