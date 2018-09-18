namespace OopFactory.X12.Parsing
{
    using System;

    /// <summary>
    /// Represents the X12Parser warning data to be included
    /// </summary>
    public class X12ParserWarningEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating whether the file is valid
        /// </summary>
        public bool FileIsValid { get; set; }

        /// <summary>
        /// Gets or sets the interchange control number for the interchange being parsed
        /// </summary>
        public string InterchangeControlNumber { get; set; }

        /// <summary>
        /// Gets or sets the functional group control number for the transaction being parsed
        /// </summary>
        public int FunctionalGroupControlNumber { get; set; }

        /// <summary>
        /// Gets or sets the transaction control number for the transaction being parsed
        /// </summary>
        public string TransactionControlNumber { get; set; }

        /// <summary>
        /// Gets or sets the Segment position in the interchange
        /// </summary>
        public int SegmentPositionInInterchange { get; set; }

        /// <summary>
        /// Gets or sets the Segment id
        /// </summary>
        public string SegmentId { get; set; }

        /// <summary>
        /// Gets or sets the segment
        /// </summary>
        public string Segment { get; set; }

        /// <summary>
        /// Gets or sets the message passed in the warning
        /// </summary>
        public string Message { get; set; }
    }
}
