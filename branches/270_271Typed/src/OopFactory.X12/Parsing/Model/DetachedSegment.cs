using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model
{
    public class DetachedSegment
    {
        internal X12DelimiterSet _delimiters;
        protected List<string> _dataElements { get; set; }

        public DetachedSegment(X12DelimiterSet delimiters, string segment)
        {
            _delimiters = delimiters;
            Initialize(segment);
        }

        public string SegmentId { get; private set; }


        public string SegmentString
        {
            get
            {
                StringBuilder sb = new StringBuilder(SegmentId);
                int iLastContent = _dataElements.Count - 1;
                while (iLastContent >= 0)
                {
                    if (!string.IsNullOrWhiteSpace(_dataElements[iLastContent]))
                        break;
                    iLastContent--;
                }
                for (int i = 0; i <= iLastContent; i++)
                {
                    sb.Append(_delimiters.ElementSeparator);
                    sb.Append(_dataElements[i]);
                }
                return sb.ToString();
            }
        }

        public X12DelimiterSet Delimiters
        {
            get { return _delimiters; }
        }

        internal virtual void Initialize(string segment)
        {
            if (segment == null)
                throw new ArgumentNullException("segment");
            _dataElements = new List<string>();
            int separatorIndex = segment.IndexOf(_delimiters.ElementSeparator);
            if (separatorIndex >= 0)
            {
                SegmentId = segment.Substring(0, separatorIndex);
                if (SegmentId == "BIN")
                {
                    int binaryStartIndex;
                    int size = Segment.ParseBinarySize(_delimiters.ElementSeparator, segment, out binaryStartIndex);
                    _dataElements.Add(size.ToString());
                    _dataElements.Add(segment.Substring(binaryStartIndex, size));
                }
                else if (SegmentId == "BDS")
                {
                    int nextIndex = segment.IndexOf(_delimiters.ElementSeparator, separatorIndex + 1);
                    if (nextIndex > separatorIndex + 1)
                    {
                        _dataElements.Add(segment.Substring(separatorIndex + 1, nextIndex - separatorIndex - 1));

                        int binaryStartIndex;
                        int size = Segment.ParseBinarySize(_delimiters.ElementSeparator, segment, out binaryStartIndex);
                        _dataElements.Add(size.ToString());
                        _dataElements.Add(segment.Substring(binaryStartIndex, size));
                    }
                }
                else
                {
                    foreach (string element in segment.TrimEnd(new char[] { _delimiters.SegmentTerminator }).Substring(separatorIndex + 1).Split(_delimiters.ElementSeparator))
                        _dataElements.Add(element);
                }
            }
            else
            {
                SegmentId = segment;
            }
        }

        private void ValidateContentFreeOfDelimiters(string elementId, string value)
        {
            if (value.Contains(_delimiters.SegmentTerminator))
                throw new ElementValidationException("Element {0} cannot contain the value '{1}' with the segment terminator {2}", elementId, value, _delimiters.SegmentTerminator);

            if (value.Contains(_delimiters.ElementSeparator))
                throw new ElementValidationException("Element {0} cannot contain the value '{1}' with the element separator {2}.", elementId, value, _delimiters.ElementSeparator);
        }


        public int ElementCount { get { return _dataElements.Count(); } }
        public string GetElement(int elementNumber)
        {
            return _dataElements.ElementAtOrDefault(elementNumber - 1);
        }

        public decimal? GetDecimalElement(int elementNumber)
        {
            decimal element;
            if (decimal.TryParse(GetElement(elementNumber), out element))
                return element;
            else
                return null;
        }

        public int? GetIntElement(int elementNumber)
        {
            int element;
            if (int.TryParse(GetElement(elementNumber), out element))
                return element;
            else
                return null;
        }

        public DateTime? GetDate8Element(int elementNumber)
        {
            string element = GetElement(elementNumber);
            if (element.Length == 8)
                return DateTime.ParseExact(element, "yyyyMMdd", null);
            else
                return null;
        }

        protected virtual void ValidateAgainstSegmentSpecification(string elementId, int elementNumber, string value)
        {
            // do nothing, this only applies once the segment is attached to an x12 interchange
        }

        public void SetElement(int elementNumber, string value)
        {
            string elementId = String.Format("{0}{1:00}", SegmentId, elementNumber);
            ValidateContentFreeOfDelimiters(elementId, value);
            ValidateAgainstSegmentSpecification(elementId, elementNumber, value);
            if (elementNumber > _dataElements.Count)
            {
                for (int i = _dataElements.Count; i < elementNumber; i++)
                    _dataElements.Add("");
            }
            _dataElements[elementNumber - 1] = value;
        }

        public void SetElement(int elementNumber, decimal? value)
        {
            SetElement(elementNumber, string.Format("{0}", value));
        }

        public void SetElement(int elementNumber, int? value)
        {
            SetElement(elementNumber, string.Format("{0}", value));
        }

        public void SetDate8Element(int elementNumber, DateTime? value)
        {
            SetElement(elementNumber, string.Format("{0:yyyyMMdd}", value));
        }
    }
}
