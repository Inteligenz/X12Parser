using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing
{
    public class X12FlatTransaction
    {
        internal X12FlatTransaction(string isaSegment, string gsSegment, string transaction)
        {
            IsaSegment = isaSegment;
            GsSegment = gsSegment;
            Transactions = new List<string>();
            Transactions.Add(transaction);
        }

        public string IsaSegment { get; private set; }
        public string GsSegment { get; private set; }
        public List<string> Transactions { get; private set; }

        public int GetSize()
        {
            int size = IsaSegment.Length + GsSegment.Length;
            foreach (string tran in Transactions)
                size += tran.Length;
            return size;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(IsaSegment);
            sb.Append(GsSegment);
            foreach (string tran in Transactions)
                sb.Append(tran);

            char elementDelimiter = IsaSegment[3];
            char segmentDelimiter = IsaSegment[105];
            string[] isaElements = IsaSegment.Split(elementDelimiter);
            string[] gsElements = GsSegment.Split(elementDelimiter);

            sb.AppendFormat("GE{1}{2}{1}{3}{0}", segmentDelimiter, elementDelimiter, Transactions.Count, gsElements[6]);
            sb.AppendFormat("IEA{1}1{1}{2}{0}", segmentDelimiter, elementDelimiter, isaElements[13]);
            return sb.ToString();
        }
    }
}
