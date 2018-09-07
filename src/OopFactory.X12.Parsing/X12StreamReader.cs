namespace OopFactory.X12.Parsing
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    using OopFactory.X12.Parsing.Properties;
    using OopFactory.X12.Shared.Models;

    /// <summary>
    /// Represents a <see cref="StreamReader"/> for reading an X12 file
    /// </summary>
    public class X12StreamReader : IDisposable
    {
        private readonly StreamReader reader;
        private readonly char[] ignoredChars;

        /// <summary>
        /// Initializes a new instance of the <see cref="X12StreamReader"/> class
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> used for reading</param>
        /// <param name="encoding"><see cref="Encoding"/> used for properly reading the stream</param>
        /// <param name="ignoredChars">Array of characters to be ignored while reading</param>
        public X12StreamReader(Stream stream, Encoding encoding, char[] ignoredChars)
        {
            this.reader = new StreamReader(stream, encoding);
            var header = new char[106];
            if (this.reader.Read(header, 0, 106) < 106)
            {
                throw new ArgumentException(Resources.X12ReaderInvalidHeader);
            }

            this.Delimiters = new X12DelimiterSet(header);
            this.CurrentIsaSegment = new string(header);
            this.ignoredChars = ignoredChars;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X12StreamReader"/> class
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> used for reading</param>
        /// <param name="encoding"><see cref="Encoding"/> used for properly reading the stream</param>
        public X12StreamReader(Stream stream, Encoding encoding)
            : this(stream, encoding, new char[] { })
        {
        }

        /// <summary>
        /// Gets the X12 Delimiters
        /// </summary>
        public X12DelimiterSet Delimiters { get; }

        /// <summary>
        /// Gets the current ISA segment
        /// </summary>
        public string CurrentIsaSegment { get; private set; }

        /// <summary>
        /// Gets the current GS segment
        /// </summary>
        public string CurrentGsSegment { get; private set; }

        /// <summary>
        /// Gets the last transaction code
        /// </summary>
        public string LastTransactionCode { get; private set; }

        /// <summary>
        /// Gets the segment id for the current segment
        /// </summary>
        /// <param name="segmentString">Segment string with id to extract</param>
        /// <returns>The current segment id</returns>
        public string ReadSegmentId(string segmentString)
        {
            int index = segmentString.IndexOf(this.Delimiters.ElementSeparator);
            return index >= 0 ? segmentString.Substring(0, index) : null;
        }

        /// <summary>
        /// Splits the current segment string
        /// </summary>
        /// <param name="segmentString">Segment string to split</param>
        /// <returns>Array of segment parts</returns>
        public string[] SplitSegment(string segmentString)
        {
            int endSegmentIndex = segmentString.IndexOf(this.Delimiters.SegmentTerminator);
            return endSegmentIndex >= 0 
                       ? segmentString.Substring(0, endSegmentIndex).Split(this.Delimiters.ElementSeparator)
                       : segmentString.Split(this.Delimiters.ElementSeparator);
        }

        /// <summary>
        /// Checks if the provided segment id is contained in the transaction
        /// </summary>
        /// <param name="transaction">Transaction to test</param>
        /// <param name="segmentId">Segment id to check for</param>
        /// <returns>True if the segment id is present; otherwise, false</returns>
        public bool TransactionContainsSegment(string transaction, string segmentId)
        {
            var segments = transaction.Split(this.Delimiters.SegmentTerminator).ToList();
            return segments.Exists(s => s.StartsWith(segmentId + this.Delimiters.ElementSeparator));                
        }

        /// <summary>
        /// Reads the next segment in the stream
        /// </summary>
        /// <returns>Segment string read from stream</returns>
        public string ReadNextSegment()
        {
            bool isBinary = false;
            var sb = new StringBuilder();
            var one = new char[1];
            while (this.reader.Read(one, 0, 1) == 1)
            {
                if (this.ignoredChars.Contains(one[0])
                || (one[0] == this.Delimiters.SegmentTerminator && sb.ToString().Trim().Length == 0))
                {
                    continue;
                }

                if (one[0] == this.Delimiters.SegmentTerminator)
                {
                    break;
                }

                if (one[0] != 0)
                {
                    sb.Append(one);
                }

                if (isBinary && one[0] == this.Delimiters.ElementSeparator)
                {
                    int binarySize = 0;
                    string[] elements = sb.ToString().Split(this.Delimiters.ElementSeparator);
                    if (elements[0] == "BIN" && elements.Length >= 2)
                    {
                        int.TryParse(sb.ToString().Split(this.Delimiters.ElementSeparator)[1], out binarySize);
                    }

                    if (elements[0] == "BDS" && elements.Length >= 3)
                    {
                        int.TryParse(sb.ToString().Split(this.Delimiters.ElementSeparator)[2], out binarySize);
                    }

                    if (binarySize > 0)
                    {
                        var buffer = new char[binarySize];
                        this.reader.Read(buffer, 0, binarySize);
                        sb.Append(buffer);
                        break;
                    }
                }

                if (!isBinary && (sb.ToString() == "BIN" + this.Delimiters.ElementSeparator 
                                  || sb.ToString() == "BDS" + this.Delimiters.ElementSeparator))
                {
                    isBinary = true;
                }
            }

            return sb.ToString().TrimStart();
        }

        /// <summary>
        /// Reads the next transaction in the stream
        /// </summary>
        /// <returns>Transaction read from the stream</returns>
        public X12FlatTransaction ReadNextTransaction()
        {
            var segments = new StringBuilder();

            string segmentString = this.ReadNextSegment();
            string segmentId = this.ReadSegmentId(segmentString);
            do
            {
                switch (segmentId)
                {
                    case "ISA":
                        this.CurrentIsaSegment = segmentString + this.Delimiters.SegmentTerminator;
                        break;
                    case "GS":
                        this.CurrentGsSegment = segmentString + this.Delimiters.SegmentTerminator;
                        break;
                    case "IEA":
                    case "GE":
                        break;
                    default:
                        if (segmentId == "ST")
                        {
                            this.LastTransactionCode = this.SplitSegment(segmentString)[1];
                        }

                        segments.Append(segmentString);
                        segments.Append(this.Delimiters.SegmentTerminator);
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

        /// <summary>
        /// Releases unmanaged resources
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged resources if disposing is true
        /// </summary>
        /// <param name="disposing">Flag indicating is object is being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.reader?.Dispose();
            }
        }
    }
}
