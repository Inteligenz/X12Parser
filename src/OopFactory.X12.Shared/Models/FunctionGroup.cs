namespace OopFactory.X12.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using OopFactory.X12.Shared.Properties;
    using OopFactory.X12.Specifications;
    using OopFactory.X12.Specifications.Interfaces;

    /// <summary>
    /// Represents an interchange function group container
    /// </summary>
    public class FunctionGroup : Container
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionGroup"/> class
        /// </summary>
        internal FunctionGroup()
            : base(null, null, "GS")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionGroup"/> class with the provided parameters
        /// </summary>
        /// <param name="specFinder">Specification finder for the container</param>
        /// <param name="parent">FunctionGroup parent container</param>
        /// <param name="delimiters">Delimiter set for segregating segments and elements</param>
        /// <param name="segment">Container segment string</param>
        internal FunctionGroup(ISpecificationFinder specFinder, Container parent, X12DelimiterSet delimiters, string segment)
            : base(parent, delimiters, segment)
        {
            this.SpecFinder = specFinder;
            this.Transactions = new List<Transaction>();
        }

        /// <summary>
        /// Gets the collection of transactions within the function group container
        /// </summary>
        public List<Transaction> Transactions { get; }

        /// <summary>
        /// Gets the parent interchange
        /// </summary>
        public Interchange Interchange => (Interchange)this.Parent;

        /// <summary>
        /// Gets or sets the containers identifier code
        /// </summary>
        public string FunctionalIdentifierCode
        {
            get { return this.GetElement(1); }
            set { this.SetElement(1, value); }
        }

        /// <summary>
        /// Gets or sets the application senders code
        /// </summary>
        public string ApplicationSendersCode
        {
            get { return this.GetElement(2); }
            set { this.SetElement(2, value); }
        }

        /// <summary>
        /// Gets or sets the application receivers code
        /// </summary>
        public string ApplicationReceiversCode
        {
            get { return this.GetElement(3); }
            set { this.SetElement(3, value); }
        }

        /// <summary>
        /// Gets or sets the date stamp
        /// </summary>
        public DateTime Date
        {
            get
            {
                DateTime date;
                if (DateTime.TryParseExact(this.GetElement(4) + this.GetElement(5), "yyyyMMddHHmm", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }

                if (DateTime.TryParseExact(this.GetElement(4), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }

                throw new ArgumentException(string.Format(Resources.DateTimeParsingError, this.GetElement(4), this.GetElement(5)));
            }

            set
            {
                this.SetElement(4, $"{value:yyyyMMdd}");
                this.SetElement(5, $"{value:HHmm}");
            }
        }

        /// <summary>
        /// Gets or sets the function groups control number
        /// </summary>
        public int ControlNumber
        {
            get { return int.Parse(this.GetElement(6)); }
            set { this.SetElement(6, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the agency code
        /// </summary>
        public string ResponsibleAgencyCode
        {
            get { return this.GetElement(7); }
            set { this.SetElement(7, value); }
        }

        /// <summary>
        /// Gets or sets the version id code
        /// </summary>
        public string VersionIdentifierCode
        {
            get { return this.GetElement(8); }
            set { this.SetElement(8, value); }
        }

        /// <summary>
        /// Gets the specification finder for the container
        /// </summary>
        internal ISpecificationFinder SpecFinder { get; }

        /// <summary>
        /// Gets the collection of allowed child <see cref="SegmentSpecification"/> objects
        /// </summary>
        internal override IList<SegmentSpecification> AllowedChildSegments => new List<SegmentSpecification>();

        /// <summary>
        /// Gets the collection of trailer segment id strings
        /// </summary>
        internal override IEnumerable<string> TrailerSegmentIds => new List<string>();

        /// <summary>
        /// Returns the first transaction found with the provided control number
        /// </summary>
        /// <param name="controlNumber">Control number for identifying the desired transaction</param>
        /// <returns>The transaction with the match control number; otherwise, null</returns>
        public Transaction FindTransaction(string controlNumber)
        {
            return this.Transactions.FirstOrDefault(t => t.ControlNumber == controlNumber);
        }

        /// <summary>
        /// Adds a provided segment string representing a transaction into the function group
        /// </summary>
        /// <param name="segmentString">Transaction segment string to add</param>
        /// <returns>The transaction that's added to the function group</returns>
        public Transaction AddTransaction(string segmentString)
        {
            string transactionType = new Segment(null, this.DelimiterSet, segmentString).GetElement(1);

            TransactionSpecification spec = this.SpecFinder.FindTransactionSpec(this.FunctionalIdentifierCode, this.VersionIdentifierCode, transactionType);

            var transaction = new Transaction(this, this.DelimiterSet, segmentString, spec);
            this.Transactions.Add(transaction);
            return transaction;
        }

        /// <summary>
        /// Adds a transaction with the provided id code and control number into the function group
        /// </summary>
        /// <param name="identifierCode">Id code for the transaction</param>
        /// <param name="controlNumber">Transaction control number</param>
        /// <returns>The transaction that's added to the function group</returns>
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

        /// <summary>
        /// Serializes the function group to the a string
        /// </summary>
        /// <returns>String representation of function group object</returns>
        public virtual string Serialize()
        {
            var xmlSerializer = new XmlSerializer(this.GetType());
            var memoryStream = new MemoryStream();

            xmlSerializer.Serialize(memoryStream, this);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(memoryStream);
            return streamReader.ReadToEnd();
        }

        /// <summary>
        /// Writes the function group object to an X12 string
        /// </summary>
        /// <param name="addWhitespace">Indicates whether additional whitespace should be added</param>
        /// <returns>X12 string representation of function group</returns>
        public override string ToX12String(bool addWhitespace)
        {
            this.UpdateTrailerSegmentCount("GE", 1, this.Transactions.Count());
            return base.ToX12String(addWhitespace);
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
    }
}
