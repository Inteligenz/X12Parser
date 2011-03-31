using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace OopFactory.X12.Parsing.Model
{
    public class Segment : IXmlSerializable
    {
        internal X12DelimiterSet _delimiters;
        private string[] _dataElements { get; set; }

        internal Segment(Container parent, X12DelimiterSet delimiters, string segment)
        {
            Parent = parent;
            _delimiters = delimiters;
            Initialize(segment);
        }

        internal virtual void Initialize(string segment)
        {
            if (segment == null)
                throw new ArgumentNullException("segment");
            SegmentString = segment.Trim();
            int separatorIndex = SegmentString.IndexOf(_delimiters.ElementSeparator);
            if (separatorIndex >= 0)
            {
                SegmentId = segment.Substring(0, separatorIndex);
                _dataElements = segment.TrimEnd(new char[] { _delimiters.SegmentTerminator }).Substring(separatorIndex + 1).Split(_delimiters.ElementSeparator);
            }
            PostValidate();
        }

        public int ElementCount { get { return _dataElements.Length; } }
        public string GetElement(int elementNumber)
        {
            return _dataElements[elementNumber - 1];
        }

        public void SetElement(int elementNumber, string value)
        {
            if (value.Contains(_delimiters.SegmentTerminator))
                throw new ArgumentException(String.Format("Value '{0}' cannot contain the segment terminator {1}.", value, _delimiters.SegmentTerminator));

            if (value.Contains(_delimiters.ElementSeparator))
                throw new ArgumentException(String.Format("Value '{0}' cannot contain the element separator {1}.", value, _delimiters.ElementSeparator));

            if (EmbeddedResources.Get4010Spec().ContainsKey(this.SegmentId))
            {
                var spec = EmbeddedResources.Get4010Spec()[SegmentId].GetElement(elementNumber);
                if (spec != null)
                {
                    if (value.Length < spec.MinLength || spec.MaxLength > 0 && value.Length > spec.MaxLength)
                        throw new ArgumentOutOfRangeException("value", String.Format("Element {0}{1:00} cannot be {2} because it must be between {3} and {4} characters in length.",
                            this.SegmentId, elementNumber, value, spec.MinLength, spec.MaxLength));

                    switch (spec.Type)
                    {
                        case Specification.ElementDataTypeEnum.Numeric:
                            int number;
                            if (!int.TryParse(value, out number))
                                throw new ArgumentException("value", String.Format("Element {0}{1:00} cannot be {2} because it is constrained to be an implied decimal.",
                                    this.SegmentId, elementNumber, value));
                            break;
                        case Specification.ElementDataTypeEnum.Decimal:
                            decimal decNumber;
                            if (!decimal.TryParse(value, out decNumber))
                                throw new ArgumentException("value", String.Format("Element {0}{1:00} cannot be {2} because it is contrained to be a decimal.",
                                    this.SegmentId, elementNumber, value));
                            break;

                    }
                }
            }

            _dataElements[elementNumber - 1] = value;
        }

        protected virtual void PostValidate()
        {
        }

        internal virtual string ToX12String(bool addWhitespace)
        {
            StringBuilder sb = new StringBuilder();
            if (addWhitespace)
                sb.AppendLine();
            sb.Append(SegmentId);
            foreach (var item in _dataElements)
            {
                sb.Append(_delimiters.ElementSeparator);
                sb.Append(item);
            }
            sb.Append(_delimiters.SegmentTerminator);
            return sb.ToString();
        }

        internal string SegmentId { get; set; }
        

        internal string SegmentString { get; set; }

        public Container Parent { get; private set; }

        #region IXmlSerializable Members

        System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            this.WriteXml(writer);
        }

        internal virtual void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(SegmentId))
            {
                writer.WriteStartElement(SegmentId);
                for (int i = 0; i < _dataElements.Length; i++)
                {
                    string elementName = String.Format("{0}{1:00}", SegmentId, i + 1);
                    if (_dataElements[i].IndexOf(_delimiters.SubElementSeparator) < 0)
                        writer.WriteElementString(elementName, _dataElements[i]);
                    else
                    {
                        writer.WriteStartElement(elementName);
                        var subElements = _dataElements[i].Split(_delimiters.SubElementSeparator);
                        for (int j = 0; j < subElements.Length; j++)
                        {
                            var subElementName = String.Format("{0}{1:00}", elementName, j + 1);
                            writer.WriteElementString(subElementName, subElements[j]);
                        }
                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
            }
        }

        public override string ToString()
        {
            return SegmentString;
        }

        #endregion

    }
}
