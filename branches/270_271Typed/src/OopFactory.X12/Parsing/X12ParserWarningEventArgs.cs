using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing
{
    public class X12ParserWarningEventArgs : EventArgs
    {
        public bool FileIsValid { get; set; }
        public string InterchangeControlNumber { get; set; }
        public int FunctionalGroupControlNumber { get; set; }
        public string TransactionControlNumber { get; set; }
        public int SegmentPositionInInterchange { get; set; }
        public string SegmentId { get; set; }
        public string Segment { get; set; }
        public string Message { get; set; }
    }
}
