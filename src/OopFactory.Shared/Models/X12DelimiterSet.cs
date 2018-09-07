namespace OopFactory.X12.Shared.Models
{
    using System;

    /// <summary>
    /// Represents the collection of delimiters used in X12 interchanges
    /// </summary>
    public class X12DelimiterSet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="X12DelimiterSet"/> class
        /// </summary>
        /// <param name="segmentTerminator">Desired segment terminator</param>
        /// <param name="elementSeparator">Desired element separator</param>
        /// <param name="subElementSeparator">Desired sub-element separator</param>
        public X12DelimiterSet(char segmentTerminator, char elementSeparator, char subElementSeparator)
        {
            this.SegmentTerminator = segmentTerminator;
            this.ElementSeparator = elementSeparator;
            this.SubElementSeparator = subElementSeparator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X12DelimiterSet"/> class with the provided ISA Segment
        /// </summary>
        /// <param name="isaSegmentAndTerminator">ISA Segment and terminator to parse delimiter set from</param>
        public X12DelimiterSet(char[] isaSegmentAndTerminator)
        {
            var prefix = new string(isaSegmentAndTerminator).Substring(0, 3);
            
            if (isaSegmentAndTerminator.Length < 105)
            {
                throw new ArgumentException("ISA segment and terminator is expected to be exactly 106 characters.");
            }

            if (prefix.ToUpper() != "ISA")
            {
                throw new ArgumentException("First segment must start with ISA");
            }

            this.ElementSeparator = isaSegmentAndTerminator[3];
            this.SubElementSeparator = isaSegmentAndTerminator[104];

            if (isaSegmentAndTerminator.Length >= 106)
            {
                this.SegmentTerminator = isaSegmentAndTerminator[105];
            }

            if (char.IsLetterOrDigit(this.ElementSeparator))
            {
                throw new ArgumentException(this.ElementSeparator + " is not a valid element separator in position 4 of the file.");
            }

            if (char.IsLetterOrDigit(this.SubElementSeparator))
            {
                throw new ArgumentException(this.SubElementSeparator + " is not a valid subelement separator in position 105 of the file.");
            }

            if (char.IsLetterOrDigit(this.SegmentTerminator))
            {
                throw new ArgumentException(this.SegmentTerminator + " is not a valid segment terminator in position 106 of the file.");
            }
        }

        /// <summary>
        /// Gets the segment terminator character
        /// </summary>
        public char SegmentTerminator { get; }

        /// <summary>
        /// Gets the element separator character
        /// </summary>
        public char ElementSeparator { get; }

        /// <summary>
        /// Gets the sub-element separator character
        /// </summary>
        public char SubElementSeparator { get; }
    }
}
