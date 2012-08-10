using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Validation.Model
{
    public class SegmentError
    {
        public string SegmentIdCode { get; set; }
        public int SegmentPosition { get; set; }
        public string LoopIdentifierCode { get; set; }
        public string ImplementationSegmentSyntaxErrorCode { get; set; }

        public List<ContextError> ContextErrors { get; set; }

        public List<DataElementNote> ElementNotes { get; set; }
    }
}
