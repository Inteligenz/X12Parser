namespace OopFactory.X12.Shared.Models
{
    using System;

    public class X12DelimiterSet
    {
        private readonly char segmentTerminator;
        private readonly char elementSeparator;
        private readonly char subElementSeparator;

        public X12DelimiterSet(char segmentTerminator, char elementSeparator, char subElementSeparator)
        {
            this.segmentTerminator = segmentTerminator;
            this.elementSeparator = elementSeparator;
            this.subElementSeparator = subElementSeparator;
        }

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

            this.elementSeparator = isaSegmentAndTerminator[3];
            this.subElementSeparator = isaSegmentAndTerminator[104];

            if (isaSegmentAndTerminator.Length >= 106)
            {
                this.segmentTerminator = isaSegmentAndTerminator[105];
            }

            if (char.IsLetterOrDigit(this.elementSeparator))
            {
                throw new ArgumentException(this.elementSeparator + " is not a valid element separator in position 4 of the file.");
            }

            if (char.IsLetterOrDigit(this.subElementSeparator))
            {
                throw new ArgumentException(this.subElementSeparator + " is not a valid subelement separator in position 105 of the file.");
            }

            if (char.IsLetterOrDigit(this.segmentTerminator))
            {
                throw new ArgumentException(this.segmentTerminator + " is not a valid segment terminator in position 106 of the file.");
            }
        }

        public char SegmentTerminator => this.segmentTerminator;

        public char ElementSeparator => this.elementSeparator;

        public char SubElementSeparator => this.subElementSeparator;
    }
}
