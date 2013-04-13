using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public class Segment : DetachedSegment, IXmlSerializable
    {

        internal Segment(Container parent, X12DelimiterSet delimiters, string segment)
            : base(delimiters, segment)
        {
            Parent = parent;
            _delimiters = delimiters;
            Initialize(segment);
        }


        public static int ParseBinarySize(char elementSeparator, string segment, out int binaryStart)
        {
            binaryStart = -1;
            int firstIndex = segment.IndexOf(elementSeparator);
            string segmentId = segment.Substring(0, firstIndex);

            if (segmentId == "BDS")
                firstIndex = segment.IndexOf(elementSeparator, firstIndex + 1);

            int nextIndex = segment.IndexOf(elementSeparator, firstIndex + 1);

            if (nextIndex > firstIndex)
            {
                string slength = segment.Substring(firstIndex + 1, nextIndex - firstIndex - 1);
                binaryStart = nextIndex + 1;
                int length = 0;
                if (int.TryParse(slength, out length))
                    return length;
            }
            
            return 0;
        }



        protected override void ValidateAgainstSegmentSpecification(string elementId, int elementNumber, string value)
        {
            if (SegmentSpec != null)
            {
                ElementSpecification spec = SegmentSpec.Elements[elementNumber - 1];
                if (spec != null)
                {
                    if (value.Length == 0 && spec.Required)
                    {
                        throw new ElementValidationException("Element {0} is required.", elementId, value);
                    }
                    if (value.Length > 0)
                    {
                        if (value.Length < spec.MinLength || spec.MaxLength > 0 && value.Length > spec.MaxLength)
                            throw new ElementValidationException("Element {0} cannot contain the value '{1}' because it must be between {2} and {3} characters in length.",
                                elementId, value, spec.MinLength, spec.MaxLength);
                    }
                    switch (spec.Type)
                    {
                        case Specification.ElementDataTypeEnum.Numeric:
                            int number;
                            if (!int.TryParse(value, out number))
                                throw new ElementValidationException("Element {0} cannot contain the value '{1}' because it is constrained to be an implied decimal.",
                                    elementId, value);
                            break;
                        case Specification.ElementDataTypeEnum.Decimal:
                            decimal decNumber;
                            if (!decimal.TryParse(value, out decNumber))
                                throw new ElementValidationException("Element {0} cannot contain the value '{1}' because it is contrained to be a decimal.",
                                    elementId, value);
                            break;
                        case Specification.ElementDataTypeEnum.Identifier:
                            if (spec.AllowedListInclusive && spec.AllowedIdentifiers.Count > 0)
                            {
                                if (spec.AllowedIdentifiers.FirstOrDefault(ai => ai.ID == value) == null)
                                {
                                    string[] ids = new string[spec.AllowedIdentifiers.Count];
                                    for (int i = 0; i < spec.AllowedIdentifiers.Count; i++)
                                        ids[i] = spec.AllowedIdentifiers[i].ID;

                                    string expected = "";
                                    if (ids.Length > 1)
                                    {
                                        expected = String.Join(", ", ids, 0, ids.Length - 1);
                                        expected += " or " + ids[ids.Length - 1];
                                    }
                                    else
                                        expected = ids[0];

                                    throw new ElementValidationException("Element '{0}' cannot contain the value '{1}'.  Specification restricts this to {2}.", elementId, value, expected);
                                }
                            }
                            break;
                    }
                }
            }
        }



        internal virtual string ToX12String(bool addWhitespace)
        {
            StringBuilder sb = new StringBuilder();
            if (addWhitespace)
                sb.AppendLine();
            sb.Append(SegmentString);
            if ((_delimiters.SegmentTerminator != '\r' && _delimiters.SegmentTerminator != '\n'))
                sb.Append(_delimiters.SegmentTerminator);
            return sb.ToString();
        }

        public string SerializeToX12(bool addWhitespace)
        {
            return this.ToX12String(addWhitespace).Trim();
        }



        public Container Parent { get; private set; }

        private FunctionGroup FunctionGroup
        {
            get
            {
                if (this is Interchange)
                    return null;
                else
                {
                    if (this is FunctionGroup)
                        return (FunctionGroup)this;
                    else if (this is Transaction)
                        return ((Transaction)this).FunctionGroup;
                    else if (this.Parent is FunctionGroup)
                        return ((FunctionGroup)this.Parent);
                    else if (this.Parent is Interchange)
                        return null;
                    else
                        return (FunctionGroup)this.Parent.Transaction.Parent;
                }
            }
        }

        private ISpecificationFinder SpecFinder
        {
            get
            {
                if (FunctionGroup != null)
                    return FunctionGroup.SpecFinder;
                else if (this is Interchange)
                    return ((Interchange)this).SpecFinder;
                else 
                    return ((Interchange)this.Parent).SpecFinder;
            }
        }
         
        private Specification.SegmentSpecification SegmentSpec
        {
            get
            {
                if (FunctionGroup != null)
                    return SpecFinder.FindSegmentSpec(FunctionGroup.VersionIdentifierCode, SegmentId);
                else
                    return SpecFinder.FindSegmentSpec("", SegmentId);                
            }
        }

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
                for (int i = 0; i < _dataElements.Count; i++)
                {
                    string elementName = String.Format("{0}{1:00}", SegmentId, i + 1);

                    List<AllowedIdentifier> identifiers = new List<AllowedIdentifier>();

                    if (SegmentSpec != null && SegmentSpec.Elements.Count > i && !string.IsNullOrEmpty(_dataElements[i]))
                    {
                        writer.WriteComment(SegmentSpec.Elements[i].Name);
                        identifiers = SegmentSpec.Elements[i].AllowedIdentifiers;
                    }
                    if (_dataElements[i].IndexOf(_delimiters.SubElementSeparator) < 0 || SegmentId == "BIN" || SegmentId == "BDS")
                    {
                        writer.WriteStartElement(elementName);
                        writer.WriteValue(_dataElements[i]);
                        if (SegmentSpec != null && SegmentSpec.Elements.Count > i && SegmentSpec.Elements[i].Type == Specification.ElementDataTypeEnum.Identifier)
                        {
                            var allowedValue = identifiers.FirstOrDefault(ai => ai.ID == _dataElements[i]);
                            if (allowedValue != null)
                                writer.WriteComment(allowedValue.Description);
                        }
                        writer.WriteEndElement();
                        
                     }
                    else
                    {
                        writer.WriteStartElement(elementName);
                        var subElements = _dataElements[i].Split(_delimiters.SubElementSeparator);
                        for (int j = 0; j < subElements.Length; j++)
                        {
                            var subElementName = String.Format("{0}{1:00}", elementName, j + 1);
                            writer.WriteStartElement(subElementName);
                            writer.WriteValue(subElements[j]);
                            if (SegmentSpec != null && SegmentSpec.Elements.Count > i && SegmentSpec.Elements[i].Type == Specification.ElementDataTypeEnum.Identifier)
                            {
                                var allowedValue = identifiers.FirstOrDefault(ai => ai.ID == subElements[j]);
                                if (allowedValue != null)
                                    writer.WriteComment(allowedValue.Description);
                            }
                            writer.WriteEndElement();
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
