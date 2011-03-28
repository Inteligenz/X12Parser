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

        internal Interchange() : base(null, null, "GS") { }

        internal Interchange(string segment)
            : base(null, new X12DelimiterSet(segment.ToCharArray()), segment)
        {
            _functionGroups = new List<FunctionGroup>();
        }

        public IEnumerable<FunctionGroup> FunctionGroups
        {
            get { return _functionGroups; }
        }

        public FunctionGroup AddFunctionGroup(string segmentString)
        {
            if (!segmentString.StartsWith("GS" + _delimiters.ElementSeparator))
                throw new InvalidOperationException(String.Format("Segment {0} does not start with GS{1} as expected.", segmentString, _delimiters.ElementSeparator));

            FunctionGroup fg = new FunctionGroup(this, _delimiters, segmentString);
            _functionGroups.Add(fg);
            return fg;
        }

        internal override IList<SegmentSpecification> AllowedChildSegments
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

        internal override void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(SegmentId))
            {
                base.WriteXml(writer);

                foreach (var segment in this.Segments)
                    segment.WriteXml(writer);

                foreach (var functionGroup in this.FunctionGroups)
                    functionGroup.WriteXml(writer);

                foreach (var segment in this.TerminatingSegments)
                    segment.WriteXml(writer);

            }
        }

        #endregion
    }
}
