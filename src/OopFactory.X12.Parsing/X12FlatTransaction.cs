namespace OopFactory.X12.Parsing
{
    using System.Collections.Generic;
    using System.Text;
    
    public class X12FlatTransaction
    {
        internal X12FlatTransaction(string isaSegment, string gsSegment, string transaction)
        {
            this.IsaSegment = isaSegment;
            this.GsSegment = gsSegment;
            this.Transactions = new List<string> { transaction };
        }

        public string IsaSegment { get; }

        public string GsSegment { get; }

        public List<string> Transactions { get; }

        public int GetSize()
        {
            int size = this.IsaSegment.Length + this.GsSegment.Length;
            foreach (string tran in this.Transactions)
            {
                size += tran.Length;
            }

            return size;
        }

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
            string[] gsElements = this.GsSegment.Split(elementDelimiter);

            sb.Append($"GE{elementDelimiter}{this.Transactions.Count}{elementDelimiter}{gsElements[6]}{segmentDelimiter}");
            sb.Append($"IEA{elementDelimiter}1{elementDelimiter}{isaElements[13]}{segmentDelimiter}");
            return sb.ToString();
        }
    }
}
