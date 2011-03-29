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
                DataElements = segment.TrimEnd(new char[] { _delimiters.SegmentTerminator }).Substring(separatorIndex + 1).Split(_delimiters.ElementSeparator);
            }
            PostValidate();
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
            foreach (var item in DataElements)
            {
                sb.Append(_delimiters.ElementSeparator);
                sb.Append(item);
            }
            sb.Append(_delimiters.SegmentTerminator);
            return sb.ToString();
        }

        internal string SegmentId { get; set; }
        
        internal string[] DataElements { get; private set; }

        public string SegmentString { get; private set; }

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
                for (int i = 0; i < DataElements.Length; i++)
                {
                    string elementName = String.Format("{0}{1:00}", SegmentId, i + 1);
                    if (DataElements[i].IndexOf(_delimiters.SubElementSeparator) < 0)
                        writer.WriteElementString(elementName, DataElements[i]);
                    else
                    {
                        writer.WriteStartElement(elementName);
                        var subElements = DataElements[i].Split(_delimiters.SubElementSeparator);
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
