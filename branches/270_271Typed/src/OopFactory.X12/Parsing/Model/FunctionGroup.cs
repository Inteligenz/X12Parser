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
        private ISpecificationFinder _specFinder;
        private List<Transaction> _transactions;

        internal FunctionGroup() : base(null, null, "GS") { }

        internal FunctionGroup(ISpecificationFinder specFinder, Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
            _specFinder = specFinder;
            _transactions = new List<Transaction>();
        }

        internal ISpecificationFinder SpecFinder
        {
            get { return _specFinder; }
        }

        public Interchange Interchange
        {
            get { return (Interchange)Parent; }
        }

        public string FunctionalIdentifierCode
        {
            get { return GetElement(1); }
            set { SetElement(1, value); }
        }

        public string ApplicationSendersCode
        {
            get { return GetElement(2); }
            set { SetElement(2, value); }
        }

        public string ApplicationReceiversCode
        {
            get { return GetElement(3); }
            set { SetElement(3, value); }
        }

        public DateTime Date
        {
            get
            {
                DateTime date;
                if (DateTime.TryParseExact(GetElement(4) + GetElement(5), "yyyyMMddHHmm", null, System.Globalization.DateTimeStyles.None, out date))
                    return date;
                else if (DateTime.TryParseExact(GetElement(4), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out date))
                    return date;
                else
                    throw new ArgumentException(String.Format("{0} and {1} cannot be converted into a date and time.", GetElement(4), GetElement(5)));

            }
            set
            {
                SetElement(4, string.Format("{0:yyyyMMdd}", value));
                SetElement(5, string.Format("{0:HHmm}", value));
            }
        }

        public int ControlNumber
        {
            get { return Int32.Parse(GetElement(6)); }
            set 
            { 
                SetElement(6, value.ToString()); 
            }
        }

        public string ResponsibleAgencyCode
        {
            get { return GetElement(7); }
            set { SetElement(7, value); }
        }

        public string VersionIdentifierCode
        {
            get { return GetElement(8); }
            set { SetElement(8, value); }
        }

        public List<Transaction> Transactions
        {
            get { return _transactions; }
        }

        public Transaction FindTransaction(string controlNumber)
        {
            return _transactions.FirstOrDefault(t => t.ControlNumber == controlNumber);
        }

        

        internal Transaction AddTransaction(string segmentString)
        {
            string transactionType = new Segment(null, _delimiters, segmentString).GetElement(1);

            TransactionSpecification spec = _specFinder.FindTransactionSpec(this.FunctionalIdentifierCode, this.VersionIdentifierCode, transactionType);

            Transaction transaction = new Transaction(this, _delimiters, segmentString, spec);
            //if (_transactions.ContainsKey(transaction.ControlNumber))
            //{
            //    throw new TransactionValidationException("Transaction control number {1} for transaction code {0} already exist within the functional group {4}.",
            //        transaction.IdentifierCode, transaction.ControlNumber, "ST02", transaction.ControlNumber, this.ControlNumber);
            //}
            //else
            //{
                _transactions.Add(transaction);
            //}
            return transaction;
        }

        public Transaction AddTransaction(string identifierCode, string controlNumber)
        {
            TransactionSpecification spec = _specFinder.FindTransactionSpec(this.FunctionalIdentifierCode, this.VersionIdentifierCode, identifierCode);

            Transaction transaction = new Transaction(this, _delimiters, 
                String.Format("ST{0}{0}{1}", _delimiters.ElementSeparator, _delimiters.SegmentTerminator), spec);
            transaction.IdentifierCode = identifierCode;
            transaction.ControlNumber = controlNumber;
            transaction.SetTerminatingTrailerSegment(
                string.Format("SE{0}0{0}{2}{1}", _delimiters.ElementSeparator, _delimiters.SegmentTerminator, controlNumber));

            _transactions.Add(transaction);
            return transaction;
        }

        internal override IList<SegmentSpecification> AllowedChildSegments
        {
            get
            {
                return new List<SegmentSpecification>();
            }
        }

        internal override IEnumerable<string> TrailerSegmentIds
        {
            get
            {
                return new List<string>();
            }
        }

        internal override string SerializeBodyToX12(bool addWhitespace)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var transaction in this.Transactions)
                sb.Append(transaction.ToX12String(addWhitespace));
            return sb.ToString();
        }

        internal override string ToX12String(bool addWhitespace)
        {
            UpdateTrailerSegmentCount("GE", 1, _transactions.Count());
            return base.ToX12String(addWhitespace);
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

                foreach (var segment in this.TrailerSegments)
                    segment.WriteXml(writer);

                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
