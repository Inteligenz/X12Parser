namespace OopFactory.X12.Shared.Models
{
    using System;

    using OopFactory.X12.Shared.Properties;

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
        /// <exception cref="ArgumentException">Thrown if the ISA segment or terminator are invalid</exception>
        public X12DelimiterSet(char[] isaSegmentAndTerminator)
        {
            var prefix = new string(isaSegmentAndTerminator).Substring(0, 3);
            
            if (isaSegmentAndTerminator.Length < 105)
            {
                throw new ArgumentException(Resources.IsaLengthMismatchError);
            }

            if (prefix.ToUpper() != "ISA")
            {
                throw new ArgumentException(Resources.IsaSegmentMissingPrefixError);
            }

            this.ElementSeparator = isaSegmentAndTerminator[3];
            this.SubElementSeparator = isaSegmentAndTerminator[104];

            if (isaSegmentAndTerminator.Length >= 106)
            {
                this.SegmentTerminator = isaSegmentAndTerminator[105];
            }

            if (char.IsLetterOrDigit(this.ElementSeparator))
            {
                throw new ArgumentException(string.Format(Resources.InvalidElementSeparatorError, this.ElementSeparator));
            }

            if (char.IsLetterOrDigit(this.SubElementSeparator))
            {
                throw new ArgumentException(string.Format(Resources.InvalidSubelementSeparatorError, this.SubElementSeparator));
            }

            if (char.IsLetterOrDigit(this.SegmentTerminator))
            {
                throw new ArgumentException(string.Format(Resources.InvalidSegmentTerminatorError, this.SegmentTerminator));
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
