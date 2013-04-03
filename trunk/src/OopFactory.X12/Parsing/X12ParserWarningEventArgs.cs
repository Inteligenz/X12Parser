using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing
{
    public class X12ParserWarningEventArgs : EventArgs
    {
        public bool FileIsValid { get; set; }
        public string SegmentId { get; set; }
        public string Segment { get; set; }
        public string Message { get; set; }
    }
}
