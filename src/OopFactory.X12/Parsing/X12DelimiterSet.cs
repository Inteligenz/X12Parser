using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing
{
    public class X12DelimiterSet
    {
        private char _segmentTerminator;
        private char _elementSeparator;
        private char _subElementSeparator;

        public X12DelimiterSet(string isaSegmentAndTerminator)
        {
            if (isaSegmentAndTerminator.Length < 105)
                throw new ArgumentException("ISA segment and terminator is expected to be exactly 106 characters.");
            if (isaSegmentAndTerminator.Substring(0, 3).ToUpper() != "ISA")
                throw new ArgumentException("First segment must start with ISA");

            _elementSeparator = isaSegmentAndTerminator[3];
            _subElementSeparator = isaSegmentAndTerminator[104];

            if (isaSegmentAndTerminator.Length >= 106)
                _segmentTerminator = isaSegmentAndTerminator[105];

            if (char.IsLetterOrDigit(_elementSeparator))
                throw new ArgumentException(_elementSeparator + " is not a valid element separator in position 4 of the file.");

            if (char.IsLetterOrDigit(_subElementSeparator))
                throw new ArgumentException(_subElementSeparator + " is not a valid subelement separator in position 105 of the file.");

            if (char.IsLetterOrDigit(_segmentTerminator))
                throw new ArgumentException(_segmentTerminator + " is not a valid segment terminator in position 106 of the file.");

        }

        public char SegmentTerminator
        {
            get { return _segmentTerminator; }
        }

        public char ElementSeparator
        {
            get { return _elementSeparator; }
        }

        public char SubElementSeparator
        {
            get { return _subElementSeparator; }
        }
    }
}
