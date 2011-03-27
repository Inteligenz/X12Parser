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
        internal HierarchicalLoop(X12DelimiterSet delimiters, string segment)
            : base(delimiters, segment)
        {
        }

        public HierarchicalLoopSpecification Specification { get; internal set; }

        public override IList<LoopSpecification> AllowedChildLoops
        {
            get
            {
                if (Specification != null)
                    return Specification.LoopSpecifications;
                else
                    return new List<LoopSpecification>();
            }
        }

        public override IList<SegmentSpecification> AllowedChildSegments
        {
            get
            {
                if (Specification != null)
                    return Specification.SegmentSpecifications;
                else
                    return new List<SegmentSpecification>();
            }
        }

        protected override void PostValidate()
        {
            if (this.SegmentId != "HL")
                throw new ArgumentException(String.Format("Segment Id expected to be 'HL' but got '{0}'.", SegmentId));

            if (this.DataElements.Length < 3)
                throw new ArgumentException("hl argument requires 3 data elements.", "hl");
        }

        //public string LoopId { get { return _loopId; } internal set { _loopId = value; } }

        [XmlAttribute("Id")]
        public string Id
        {
            get { return DataElements[0]; }
            set { DataElements[0] = value; }
        }

        [XmlAttribute("ParentId")]
        public string ParentId
        {
            get { return DataElements[1]; }
            set { DataElements[1] = value; }
        }

        internal string LevelCode
        {
            get { return DataElements[2]; }
            set { DataElements[2] = value; }
        }

        public bool HasChildren { get { return DataElements[3] == "1"; } }


        public override void WriteXml(System.Xml.XmlWriter writer)
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
            return String.Format("Loop(Level={0},ChildLoops={1}, ChildSegments={2})", LevelCode, HLoops.Count, Segments.Count());
        }
    }
}
