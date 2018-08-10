namespace OopFactory.X12.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DetachedSegment
    {
        internal X12DelimiterSet DelimiterSet;

        public DetachedSegment(X12DelimiterSet delimiters, string segment)
        {
            this.DelimiterSet = delimiters;
            this.Initialize(segment);
        }

        public string SegmentId { get; private set; }

        public X12DelimiterSet Delimiters => this.DelimiterSet;

        public int ElementCount => this.DataElements.Count();

        protected List<string> DataElements { get; set; }

        public string SegmentString
        {
            get
            {
                var sb = new StringBuilder(this.SegmentId);
                int lastContentIndex = this.DataElements.Count - 1;
                while (lastContentIndex >= 0)
                {
                    if (!string.IsNullOrWhiteSpace(this.DataElements[lastContentIndex]))
                    {
                        break;
                    }

                    lastContentIndex--;
                }

                for (int i = 0; i <= lastContentIndex; i++)
                {
                    sb.Append(this.DelimiterSet.ElementSeparator);
                    sb.Append(this.DataElements[i]);
                }

                return sb.ToString();
            }
        }

        internal virtual void Initialize(string segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }

            this.DataElements = new List<string>();
            int separatorIndex = segment.IndexOf(this.DelimiterSet.ElementSeparator);
            if (separatorIndex >= 0)
            {
                this.SegmentId = segment.Substring(0, separatorIndex);
                if (this.SegmentId == "BIN")
                {
                    int binaryStartIndex;
                    int size = Segment.ParseBinarySize(this.DelimiterSet.ElementSeparator, segment, out binaryStartIndex);
                    this.DataElements.Add(size.ToString());
                    this.DataElements.Add(segment.Substring(binaryStartIndex, size));
                }
                else if (this.SegmentId == "BDS")
                {
                    int nextIndex = segment.IndexOf(this.DelimiterSet.ElementSeparator, separatorIndex + 1);
                    if (nextIndex > separatorIndex + 1)
                    {
                        this.DataElements.Add(segment.Substring(separatorIndex + 1, nextIndex - separatorIndex - 1));

                        int binaryStartIndex;
                        int size = Segment.ParseBinarySize(this.DelimiterSet.ElementSeparator, segment, out binaryStartIndex);
                        this.DataElements.Add(size.ToString());
                        this.DataElements.Add(segment.Substring(binaryStartIndex, size));
                    }
                }
                else
                {
                    foreach (string element in segment.TrimEnd(new char[] { this.DelimiterSet.SegmentTerminator }).Substring(separatorIndex + 1).Split(this.DelimiterSet.ElementSeparator))
                    {
                        this.DataElements.Add(element);
                    }
                }
            }
            else
            {
                this.SegmentId = segment;
            }
        }

        private void ValidateContentFreeOfDelimiters(string elementId, string value)
        {
            if (value.Contains(this.DelimiterSet.SegmentTerminator))
            {
                throw new ElementValidationException(
                    "Element {0} cannot contain the value '{1}' with the segment terminator {2}",
                    elementId,
                    value,
                    this.DelimiterSet.SegmentTerminator);
            }

            if (value.Contains(this.DelimiterSet.ElementSeparator))
            {
                throw new ElementValidationException(
                    "Element {0} cannot contain the value '{1}' with the element separator {2}.",
                    elementId,
                    value,
                    this.DelimiterSet.ElementSeparator);
            }
        }

        public string GetElement(int elementNumber)
        {
            return this.DataElements.ElementAtOrDefault(elementNumber - 1);
        }

        public decimal? GetDecimalElement(int elementNumber)
        {
            decimal element;
            if (decimal.TryParse(this.GetElement(elementNumber), out element))
            {
                return element;
            }

            return null;
        }

        public int? GetIntElement(int elementNumber)
        {
            int element;
            if (int.TryParse(this.GetElement(elementNumber), out element))
            {
                return element;
            }

            return null;
        }

        public DateTime? GetDate8Element(int elementNumber)
        {
            string element = this.GetElement(elementNumber);
            if (element.Length == 8)
            {
                return DateTime.ParseExact(element, "yyyyMMdd", null);
            }

            return null;
        }

        protected virtual void ValidateAgainstSegmentSpecification(string elementId, int elementNumber, string value)
        {
            // do nothing, this only applies once the segment is attached to an x12 interchange
        }

        public void SetElement(int elementNumber, string value)
        {
            string elementId = string.Format("{0}{1:00}", this.SegmentId, elementNumber);
            this.ValidateContentFreeOfDelimiters(elementId, value);
            this.ValidateAgainstSegmentSpecification(elementId, elementNumber, value);
            if (elementNumber > this.DataElements.Count)
            {
                for (int i = this.DataElements.Count; i < elementNumber; i++)
                {
                    this.DataElements.Add(string.Empty);
                }
            }

            this.DataElements[elementNumber - 1] = value;
        }

        public void SetElement(int elementNumber, decimal? value)
        {
            this.SetElement(elementNumber, $"{value}");
        }

        public void SetElement(int elementNumber, int? value)
        {
            this.SetElement(elementNumber, $"{value}");
        }

        public void SetDate8Element(int elementNumber, DateTime? value)
        {
            this.SetElement(elementNumber, string.Format("{0:yyyyMMdd}", value));
        }
    }
}
