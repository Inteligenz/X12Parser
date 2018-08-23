namespace OopFactory.X12.Validation.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents an error in a Segment
    /// </summary>
    public class SegmentError
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SegmentError"/> class
        /// </summary>
        public SegmentError()
        {
            if (this.ContextErrors == null)
            {
                this.ContextErrors = new List<ContextError>();
            }

            if (this.ElementNotes == null)
            {
                this.ElementNotes = new List<DataElementNote>();
            }
        }

        public string SegmentIdCode { get; set; }

        public int SegmentPosition { get; set; }

        public string LoopIdentifierCode { get; set; }

        public string ImplementationSegmentSyntaxErrorCode { get; set; }

        public List<ContextError> ContextErrors { get; set; }

        public List<DataElementNote> ElementNotes { get; set; }

        /// <summary>
        /// Returns the error string associated to the provided code
        /// </summary>
        /// <param name="code">Error code to retrieve error message</param>
        /// <returns>String message associated to error code</returns>
        public static string GetErrorDescription(string code)
        {
            switch (code)
            {
                case "1": return "Unrecognized segment ID";
                case "2": return "Unexpected segment";
                case "3": return "Required Segment Missing";
                case "4": return "Loop Occurs Over Maximum Times";
                case "5": return "Segment Exceeds Maximum Use";
                case "6": return "Segment Not in Defined Transaction Set";
                case "7": return "Segment Not in Proper Sequence";
                case "8": return "Segment Has Data Element Errors";
                case "I4": return "Implementation “Not Used” Segment Present";
                case "I6": return "Implementation Dependent Segment Missing";
                case "I7": return "Implementation Loop Occurs Under Minimum Times";
                case "I8": return "Implementation Segment Below Minimum Use";
                case "I9": return "Implementation Dependent “Not Used” Segment Present";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Returns a string representation of the <see cref="SegmentError"/> class
        /// </summary>
        /// <returns>String representation of class</returns>
        public override string ToString()
        {
            return string.Format(
                "Id={0}, Pos={1}, LoopId={2}, Error={3}: {4}",
                this.SegmentIdCode,
                this.SegmentPosition,
                this.LoopIdentifierCode,
                this.ImplementationSegmentSyntaxErrorCode,
                GetErrorDescription(this.ImplementationSegmentSyntaxErrorCode));
        }
    }
}
