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
        private string _segmentCode;
        protected string _segment;
        private string[] _dataElements;
        private X12DelimiterSet _delimiters;

        internal Segment(X12DelimiterSet delimiters, string segment)
        {
            _delimiters = delimiters;
            Initialize(segment);
        }

        protected virtual void Initialize(string segment)
        {
            PreValidate();
            if (segment == null)
                throw new ArgumentNullException("segment");
            _segment = segment.Trim();
            int separatorIndex = _segment.IndexOf(_delimiters.ElementSeparator);
            if (separatorIndex >= 0)
            {
                _segmentCode = segment.Substring(0, separatorIndex);
                _dataElements = segment.Substring(separatorIndex + 1).Split(_delimiters.ElementSeparator);
            }
            PostValidate();
        }

        protected virtual void PreValidate()
        {
        }

        protected virtual void PostValidate()
        {
        }

        internal string SegmentId
        {
            get { return _segmentCode; }
            set { _segment = value; }
        }


        internal string[] DataElements { get { return _dataElements; } }

        public string SegmentString { get { return _segment; } }

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        public virtual void WriteXml(XmlWriter writer)
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
            return _segment;
        }

        #endregion

    }
}
