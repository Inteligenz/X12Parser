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
    public class FunctionGroup : Container, IXmlSerializable
    {
        private List<Transaction> _transactions;

        internal FunctionGroup() : base(null, "GS") { }

        internal FunctionGroup(X12DelimiterSet delimiters, string segment)
            : base(delimiters, segment)
        {
            _transactions = new List<Transaction>();
        }

        public List<Transaction> Transactions
        {
            get { return _transactions; }
        }

        public override IList<SegmentSpecification> AllowedChildSegments
        {
            get
            {
                return new List<SegmentSpecification>();
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
                writer.WriteStartElement("FunctionGroup");

                base.WriteXml(writer);

                foreach (var segment in this.Segments)
                    segment.WriteXml(writer);

                foreach (var transaction in this.Transactions)
                    transaction.WriteXml(writer);

                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
