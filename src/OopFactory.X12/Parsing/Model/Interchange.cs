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
    public class Interchange : Container, IXmlSerializable
    {
        private List<FunctionGroup> _functionGroups;

        internal Interchange() : base(null, "GS") { }

        internal Interchange(string segment)
            : base(new X12DelimiterSet(segment), segment)
        {
            _functionGroups = new List<FunctionGroup>();
        }

        public List<FunctionGroup> FunctionGroups
        {
            get { return _functionGroups; }
        }

        public override IList<SegmentSpecification> AllowedChildSegments
        {
            get
            {
                var list = new List<SegmentSpecification>();
                return list;
            }
        }

        public virtual string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(
                this.GetType());
            MemoryStream memoryStream = new MemoryStream();

            xmlSerializer.Serialize(memoryStream, this);
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader streamReader = new StreamReader(memoryStream);
            return streamReader.ReadToEnd();

        }

        #region IXmlSerializable Members

        public override void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(SegmentId))
            {
                base.WriteXml(writer);

                foreach (var segment in this.Segments)
                    segment.WriteXml(writer);

                foreach (var functionGroup in this.FunctionGroups)
                    functionGroup.WriteXml(writer);
            }
        }

        #endregion
    }
}
