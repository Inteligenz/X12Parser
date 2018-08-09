using System;
using System.Linq;
using System.Text;
using System.IO;

namespace OopFactory.X12.Parsing
{
    public class X12StreamReader : IDisposable
    {
        private readonly StreamReader reader;
        private readonly X12DelimiterSet delimiters;
        private readonly char[] ignoredChars;
        private string gsSegment;
        private string isaSegment;
        private string transactionCode;

        public X12StreamReader(Stream stream, Encoding encoding, char[] ignoredChars)
        {
            this.reader = new StreamReader(stream, encoding);
            var header = new char[106];
            if (this.reader.Read(header, 0, 106) < 106)
            {
                throw new ArgumentException("ISA segment and terminator is expected to be at least 106 characters.");
            }

            this.delimiters = new X12DelimiterSet(header);
            this.isaSegment = new string(header);
            this.ignoredChars = ignoredChars;
        }

        public X12StreamReader(Stream stream, Encoding encoding)
            : this(stream, encoding, new char[] { })
        {
        }

        public X12DelimiterSet Delimiters => this.delimiters;

        public string CurrentIsaSegment => this.isaSegment;

        public string CurrentGsSegment => this.gsSegment;

        public string LastTransactionCode => this.transactionCode;

        public string ReadSegmentId(string segmentString)
        {
            int index = segmentString.IndexOf(this.delimiters.ElementSeparator);
            return index >= 0 ? segmentString.Substring(0, index) : null;
        }

        public string[] SplitSegment(string segmentString)
        {
            int endSegmentIndex = segmentString.IndexOf(this.Delimiters.SegmentTerminator);
            return endSegmentIndex >= 0 
                       ? segmentString.Substring(0, endSegmentIndex).Split(this.Delimiters.ElementSeparator)
                       : segmentString.Split(this.Delimiters.ElementSeparator);
        }

        public bool TransactionContainsSegment(string transaction, string segmentId)
        {
            var segments = transaction.Split(this.Delimiters.SegmentTerminator).ToList();
            return segments.Exists(s => s.StartsWith(segmentId + this.Delimiters.ElementSeparator));                
        }

        public string ReadNextSegment()
        {
            bool isBinary = false;
            var sb = new StringBuilder();
            var one = new char[1];
            while (this.reader.Read(one, 0, 1) == 1)
            {
                if (this.ignoredChars.Contains(one[0]))
                {
                    continue;
                }

                if (one[0] == this.delimiters.SegmentTerminator && sb.ToString().Trim().Length == 0)
                {
                    continue;
                }

                if (one[0] == this.delimiters.SegmentTerminator)
                {
                    break;
                }

                if (one[0] != 0)
                {
                    sb.Append(one);
                }

                if (isBinary && one[0] == this.delimiters.ElementSeparator)
                {
                    int binarySize = 0;
                    string[] elements = sb.ToString().Split(this.delimiters.ElementSeparator);
                    if (elements[0] == "BIN" && elements.Length >= 2)
                    {
                        int.TryParse(sb.ToString().Split(this.delimiters.ElementSeparator)[1], out binarySize);
                    }

                    if (elements[0] == "BDS" && elements.Length >= 3)
                    {
                        int.TryParse(sb.ToString().Split(this.delimiters.ElementSeparator)[2], out binarySize);
                    }

                    if (binarySize > 0)
                    {
                        var buffer = new char[binarySize];
                        this.reader.Read(buffer, 0, binarySize);
                        sb.Append(buffer);
                        break;
                    }
                }

                if (!isBinary && (sb.ToString() == "BIN" + this.delimiters.ElementSeparator 
                                  || sb.ToString() == "BDS" + this.delimiters.ElementSeparator))
                {
                    isBinary = true;
                }
            }

            return sb.ToString().TrimStart();
        }

        public void Dispose()
        {
            this.reader.Dispose();
        }

        public X12FlatTransaction ReadNextTransaction()
        {
            StringBuilder segments = new StringBuilder();

            string segmentString = this.ReadNextSegment();
            string segmentId = this.ReadSegmentId(segmentString);
            do
            {
                switch (segmentId)
                {
                    case "ISA":
                        this.isaSegment = segmentString + this.delimiters.SegmentTerminator;
                        break;
                    case "GS":
                        this.gsSegment = segmentString + this.delimiters.SegmentTerminator;
                        break;
                    case "IEA":
                    case "GE":
                        break;
                    default:
                        if (segmentId == "ST")
                        {
                            this.transactionCode = this.SplitSegment(segmentString)[1];
                        }

                        segments.Append(segmentString);
                        segments.Append(this.delimiters.SegmentTerminator);
                        break;
                }
                segmentString = this.ReadNextSegment();
                segmentId = this.ReadSegmentId(segmentString);
            }
            while (!string.IsNullOrEmpty(segmentString) && segmentId != "SE");

            return new X12FlatTransaction(
                this.CurrentIsaSegment,
                this.CurrentGsSegment,
                segments.ToString());
        }
    }
}
