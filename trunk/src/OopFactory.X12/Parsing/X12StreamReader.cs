﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OopFactory.X12.Parsing
{
    public class X12StreamReader : IDisposable
    {
        private StreamReader _reader;
        private string _isaSegment;
        private X12DelimiterSet _delimiters;
        private string _gsSegment;

        public X12StreamReader(Stream stream, Encoding encoding)
        {
            _reader = new StreamReader(stream, encoding);
            char[] header = new char[106];
            if (_reader.Read(header, 0, 106) < 106)
                throw new ArgumentException("ISA segment and terminator is expected to be at least 106 characters.");
            _delimiters = new X12DelimiterSet(header);
            _isaSegment = new string(header);
        }

        public X12DelimiterSet Delimiters
        {
            get { return _delimiters; }
        }

        public string CurrentIsaSegment
        {
            get { return _isaSegment; }
        }

        public string CurrentGsSegment
        {
            get { return _gsSegment; }
        }

        public string ReadSegmentId(string segmentString)
        {
            int index = segmentString.IndexOf(_delimiters.ElementSeparator);
            if (index >= 0)
                return segmentString.Substring(0, index);
            else
                return null;
        }

        public string ReadNextSegment()
        {
            bool isBinary = false;
            StringBuilder sb = new StringBuilder();
            char[] one = new char[1];
            while (_reader.Read(one, 0, 1) == 1)
            {
                if (one[0] == _delimiters.SegmentTerminator && sb.ToString().Trim().Length == 0)
                    continue;
                else if(one[0] == _delimiters.SegmentTerminator)
                    break;
                else if (one[0] != 0)
                    sb.Append(one);
                if (isBinary && one[0] == _delimiters.ElementSeparator)
                {
                    int binarySize = 0;
                    int.TryParse(sb.ToString().Split(_delimiters.ElementSeparator)[1], out binarySize);
                    char[] buffer = new char[binarySize];
                    _reader.Read(buffer, 0, binarySize);
                    sb.Append(buffer);
                    break;                    
                }
                if (!isBinary && sb.ToString() == "BIN" + _delimiters.ElementSeparator)
                    isBinary = true;
            }
            return sb.ToString().TrimStart();
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public X12FlatTransaction ReadNextTransaction()
        {
            StringBuilder segments = new StringBuilder();

            string segmentString = ReadNextSegment();
            string segmentId = ReadSegmentId(segmentString);
            do
            {
                switch (segmentId)
                {
                    case "ISA":
                        _isaSegment = segmentString + _delimiters.SegmentTerminator;
                        break;
                    case "GS":
                        _gsSegment = segmentString + _delimiters.SegmentTerminator;
                        break;
                    case "IEA":
                    case "GE":
                        break;
                    default:
                        segments.Append(segmentString);
                        segments.Append(_delimiters.SegmentTerminator);
                        break;
                }
                segmentString = ReadNextSegment();
                segmentId = ReadSegmentId(segmentString);
            } while (!string.IsNullOrEmpty(segmentString) && segmentId != "SE"); // transaction trailer segment

            return new X12FlatTransaction(
                CurrentIsaSegment,
                CurrentGsSegment,
                segments.ToString());
        }
    }
}
