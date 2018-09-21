namespace OopFactory.X12.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using OopFactory.X12.Shared.Properties;

    /// <summary>
    /// Represents a segment that's not a part of any transaction or interchange
    /// </summary>
    public class DetachedSegment
    {
        internal X12DelimiterSet DelimiterSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetachedSegment"/> class
        /// </summary>
        /// <param name="delimiters">Delimiter set for the segment to use</param>
        /// <param name="segment">Segment string to initialize with</param>
        public DetachedSegment(X12DelimiterSet delimiters, string segment)
        {
            this.DelimiterSet = delimiters;
            this.Initialize(segment);
        }

        /// <summary>
        /// Gets the segment ID string
        /// </summary>
        public string SegmentId { get; private set; }

        /// <summary>
        /// Gets the <see cref="X12DelimiterSet"/> used by the segment
        /// </summary>
        public X12DelimiterSet Delimiters => this.DelimiterSet;

        /// <summary>
        /// Gets the number of elements stored on the segment
        /// </summary>
        public int ElementCount => this.DataElements.Count;

        /// <summary>
        /// Gets the segment string representing the object
        /// </summary>
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

        /// <summary>
        /// Gets or sets the collection of data element strings in the segment
        /// </summary>
        protected List<string> DataElements { get; set; }

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

        /// <summary>
        /// Sets the provided element at the position indicated by the elementNumber
        /// </summary>
        /// <param name="elementNumber">Position in segment to set value</param>
        /// <param name="value">Data to be stored</param>
        public void SetElement(int elementNumber, string value)
        {
            string elementId = $"{this.SegmentId}{elementNumber:00}";
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
                    foreach (string element in segment.TrimEnd(this.DelimiterSet.SegmentTerminator).Substring(separatorIndex + 1).Split(this.DelimiterSet.ElementSeparator))
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

        protected virtual void ValidateAgainstSegmentSpecification(string elementId, int elementNumber, string value)
        {
            // This method only applies once the segment is attached to an x12 interchange
        }

        private void ValidateContentFreeOfDelimiters(string elementId, string value)
        {
            if (value.Contains(this.DelimiterSet.SegmentTerminator))
            {
                throw new ElementValidationException(
                    Resources.ElementSegmentTerminatorError,
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
    }
}
