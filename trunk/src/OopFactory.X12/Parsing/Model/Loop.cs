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
    public class Loop : LoopContainer
    {
        internal Loop(Container parent, X12DelimiterSet delimiters, string startingSegment, LoopSpecification loopSpecification)
            : base(parent, delimiters, startingSegment)
        {
            Specification = loopSpecification;
        }

        public LoopSpecification Specification { get; private set; }

        internal override IList<LoopSpecification> AllowedChildLoops
        {
            get { return Specification.LoopSpecifications; }
        }

        internal override IList<SegmentSpecification> AllowedChildSegments
        {
            get { return Specification.SegmentSpecifications; }
        }

        #region IXmlSerializable Members

        internal override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(SegmentId))
            {
                writer.WriteStartElement("Loop");

                if (Specification != null)
                {
                    writer.WriteAttributeString("LoopId", Specification.LoopId);
                    writer.WriteAttributeString("Name", Specification.Name);
                }

                base.WriteXml(writer);

                foreach (var segment in this.Segments)
                    segment.WriteXml(writer);

                foreach (var segment in this.TrailerSegments)
                    segment.WriteXml(writer);

                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
