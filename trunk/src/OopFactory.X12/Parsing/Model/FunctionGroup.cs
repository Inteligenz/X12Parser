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
        private Dictionary<string,Transaction> _transactions;

        internal FunctionGroup() : base(null, null, "GS") { }

        internal FunctionGroup(Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
            _transactions = new Dictionary<string,Transaction>();
        }

        public IEnumerable<Transaction> Transactions
        {
            get { return _transactions.Values; }
        }

        public Transaction FindTransaction(string controlNumber)
        {
            if (_transactions.ContainsKey(controlNumber))
                return _transactions[controlNumber];
            else
                return null;
        }

        private static TransactionSpecification GetSpec(string transactionType)
        {
            switch (transactionType)
            {
                case "835":
                    return EmbeddedResources.Get835TransactionSpecification();
                case "837":
                    return EmbeddedResources.Get837TransactionSpecification();
                case "856":
                    return EmbeddedResources.Get856TransactionSpecification();
                case "997":
                    return EmbeddedResources.Get997TransactionSpecification();
                default:
                    throw new NotSupportedException(String.Format("Transaction Type {0} is not supported.", transactionType));
            }
        }

        internal Transaction AddTransaction(string segmentString)
        {
            if (!segmentString.StartsWith("ST" + _delimiters.ElementSeparator))
                throw new InvalidOperationException(string.Format("Segment {0} does not start with ST{1} as expected.", segmentString, _delimiters.ElementSeparator));

            string transactionType = new Segment(null, _delimiters, segmentString).GetElement(1);

            Transaction transaction = new Transaction(this, _delimiters, segmentString, GetSpec(transactionType));
            _transactions.Add(transaction.ControlNumber, transaction);
            return transaction;
        }

        internal override IList<SegmentSpecification> AllowedChildSegments
        {
            get
            {
                return new List<SegmentSpecification>();
            }
        }

        internal override string SerializeBodyToX12(bool addWhitespace)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var transaction in this.Transactions)
                sb.Append(transaction.ToX12String(addWhitespace));
            return sb.ToString();
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
                writer.WriteStartElement("FunctionGroup");

                base.WriteXml(writer);

                foreach (var segment in this.Segments)
                    segment.WriteXml(writer);

                foreach (var transaction in this.Transactions)
                    transaction.WriteXml(writer);

                foreach (var segment in this.TerminatingSegments)
                    segment.WriteXml(writer);

                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
