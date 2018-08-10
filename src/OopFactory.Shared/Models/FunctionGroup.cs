namespace OopFactory.X12.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    
    using OopFactory.X12.Specifications;
    using OopFactory.X12.Specifications.Interfaces;

    public class FunctionGroup : Container, IXmlSerializable
    {
        public List<Transaction> Transactions { get; }

        internal ISpecificationFinder SpecFinder { get; }

        internal FunctionGroup() : base(null, null, "GS") { }

        internal FunctionGroup(ISpecificationFinder specFinder, Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
            this.SpecFinder = specFinder;
            this.Transactions = new List<Transaction>();
        }

        public Interchange Interchange => (Interchange)this.Parent;

        public string FunctionalIdentifierCode
        {
            get { return this.GetElement(1); }
            set { this.SetElement(1, value); }
        }

        public string ApplicationSendersCode
        {
            get { return this.GetElement(2); }
            set { this.SetElement(2, value); }
        }

        public string ApplicationReceiversCode
        {
            get { return this.GetElement(3); }
            set { this.SetElement(3, value); }
        }

        public DateTime Date
        {
            get
            {
                DateTime date;
                if (DateTime.TryParseExact(this.GetElement(4) + this.GetElement(5), "yyyyMMddHHmm", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }
                else if (DateTime.TryParseExact(this.GetElement(4), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    throw new ArgumentException(
                        string.Format(
                            "{0} and {1} cannot be converted into a date and time.",
                            GetElement(4),
                            GetElement(5)));
                }
            }

            set
            {
                this.SetElement(4, string.Format("{0:yyyyMMdd}", value));
                this.SetElement(5, string.Format("{0:HHmm}", value));
            }
        }

        public int ControlNumber
        {
            get { return int.Parse(this.GetElement(6)); }
            set { this.SetElement(6, value.ToString()); }
        }

        public string ResponsibleAgencyCode
        {
            get { return this.GetElement(7); }
            set { this.SetElement(7, value); }
        }

        public string VersionIdentifierCode
        {
            get { return this.GetElement(8); }
            set { this.SetElement(8, value); }
        }

        internal override IList<SegmentSpecification> AllowedChildSegments => new List<SegmentSpecification>();

        internal override IEnumerable<string> TrailerSegmentIds => new List<string>();

        public Transaction FindTransaction(string controlNumber)
        {
            return this.Transactions.FirstOrDefault(t => t.ControlNumber == controlNumber);
        }

        public Transaction AddTransaction(string segmentString)
        {
            string transactionType = new Segment(null, this.DelimiterSet, segmentString).GetElement(1);

            TransactionSpecification spec = this.SpecFinder.FindTransactionSpec(this.FunctionalIdentifierCode, this.VersionIdentifierCode, transactionType);

            var transaction = new Transaction(this, this.DelimiterSet, segmentString, spec);
            this.Transactions.Add(transaction);
            return transaction;
        }

        public Transaction AddTransaction(string identifierCode, string controlNumber)
        {
            TransactionSpecification spec = this.SpecFinder.FindTransactionSpec(this.FunctionalIdentifierCode, this.VersionIdentifierCode, identifierCode);
            var transaction = new Transaction(this, this.DelimiterSet, string.Format("ST{0}{0}{1}", this.DelimiterSet.ElementSeparator, this.DelimiterSet.SegmentTerminator), spec)
            {
                IdentifierCode = identifierCode,
                ControlNumber = controlNumber
            };
            transaction.SetTerminatingTrailerSegment(
                string.Format("SE{0}0{0}{2}{1}", this.DelimiterSet.ElementSeparator, this.DelimiterSet.SegmentTerminator, controlNumber));

            this.Transactions.Add(transaction);
            return transaction;
        }

        public virtual string Serialize()
        {
            var xmlSerializer = new XmlSerializer(this.GetType());
            var memoryStream = new MemoryStream();

            xmlSerializer.Serialize(memoryStream, this);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(memoryStream);
            return streamReader.ReadToEnd();
        }

        internal override string SerializeBodyToX12(bool addWhitespace)
        {
            var sb = new StringBuilder();
            foreach (var transaction in this.Transactions)
            {
                sb.Append(transaction.ToX12String(addWhitespace));
            }

            return sb.ToString();
        }

        public override string ToX12String(bool addWhitespace)
        {
            this.UpdateTrailerSegmentCount("GE", 1, this.Transactions.Count());
            return base.ToX12String(addWhitespace);
        }

        #region IXmlSerializable Members

        internal override void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.SegmentId))
            {
                writer.WriteStartElement("FunctionGroup");

                base.WriteXml(writer);

                foreach (var segment in this.Segments)
                {
                    segment.WriteXml(writer);
                }

                foreach (var transaction in this.Transactions)
                {
                    transaction.WriteXml(writer);
                }

                foreach (var segment in this.TrailerSegments)
                {
                    segment.WriteXml(writer);
                }

                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
