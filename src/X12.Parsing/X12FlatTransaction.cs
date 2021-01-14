namespace X12.Parsing
{
    using System.Collections.Generic;
    using System.Text;
    
    /// <summary>
    /// Represents a flattened transaction
    /// </summary>
    public class X12FlatTransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="X12FlatTransaction"/> class
        /// </summary>
        /// <param name="isaSegment">ISA segment string</param>
        /// <param name="gsSegment">Function group segment string</param>
        /// <param name="transaction">Transaction segment string</param>
        internal X12FlatTransaction(string isaSegment, string gsSegment, string transaction)
        {
            this.IsaSegment = isaSegment;
            this.GsSegment = gsSegment;
            this.Transactions = new List<string> { transaction };
        }

        /// <summary>
        /// Gets the ISA segment for this transaction
        /// </summary>
        public string IsaSegment { get; }

        /// <summary>
        /// Gets the function group segment for this transaction
        /// </summary>
        public string GsSegment { get; }

        /// <summary>
        /// Gets the collection of transactions stored in this transaction
        /// </summary>
        public List<string> Transactions { get; }

        /// <summary>
        /// Returns the size of the flat transaction
        /// </summary>
        /// <returns>Size of transaction</returns>
        public int GetSize()
        {
            int size = this.IsaSegment.Length + this.GsSegment.Length;
            foreach (string tran in this.Transactions)
            {
                size += tran.Length;
            }

            return size;
        }

        /// <summary>
        /// Returns the string representation of the flat transaction
        /// </summary>
        /// <returns>String representation of the transaction</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.IsaSegment);
            sb.Append(this.GsSegment);
            foreach (string tran in this.Transactions)
            {
                sb.Append(tran);
            }

            char elementDelimiter = this.IsaSegment[3];
            char segmentDelimiter = this.IsaSegment[105];
            string[] isaElements = this.IsaSegment.Split(elementDelimiter);
            string[] functionGroupElements = this.GsSegment.Split(elementDelimiter);

            sb.Append($"GE{elementDelimiter}{this.Transactions.Count}{elementDelimiter}{functionGroupElements[6]}{segmentDelimiter}");
            sb.Append($"IEA{elementDelimiter}1{elementDelimiter}{isaElements[13]}{segmentDelimiter}");
            return sb.ToString();
        }
    }
}
