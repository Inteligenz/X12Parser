namespace X12.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using X12.Shared.Properties;
    using X12.Specifications;
    using X12.Specifications.Enumerations;
    using X12.Specifications.Interfaces;

    public class Segment : DetachedSegment, IXmlSerializable
    {
        public Segment(Container parent, X12DelimiterSet delimiters, string segment)
            : base(delimiters, segment)
        {
            this.Parent = parent;
            this.Initialize(segment);
            this.DelimiterSet = delimiters;
        }

        public Container Parent { get; }

        private SegmentSpecification SegmentSpec =>
            this.SpecFinder.FindSegmentSpec(this.FunctionGroup != null ? this.FunctionGroup.VersionIdentifierCode : string.Empty, this.SegmentId);

        private FunctionGroup FunctionGroup
        {
            get
            {
                if (this is Interchange)
                {
                    return null;
                }

                if (this is FunctionGroup)
                {
                    return (FunctionGroup)this;
                }

                if (this is Transaction)
                {
                    return ((Transaction)this).FunctionGroup;
                }

                if (this.Parent is FunctionGroup functionGroup)
                {
                    return functionGroup;
                }

                if (this.Parent is Interchange)
                {
                    return null;
                }

                return (FunctionGroup)this.Parent.Transaction.Parent;
            }
        }

        private ISpecificationFinder SpecFinder
        {
            get
            {
                if (this.FunctionGroup != null)
                {
                    return this.FunctionGroup.SpecFinder;
                }

                if (this is Interchange)
                {
                    return ((Interchange)this).SpecFinder;
                }

                return ((Interchange)this.Parent).SpecFinder;
            }
        }

        public static int ParseBinarySize(char elementSeparator, string segment, out int binaryStart)
        {
            binaryStart = -1;
            int firstIndex = segment.IndexOf(elementSeparator);
            string segmentId = segment.Substring(0, firstIndex);

            if (segmentId == "BDS")
            {
                firstIndex = segment.IndexOf(elementSeparator, firstIndex + 1);
            }

            int nextIndex = segment.IndexOf(elementSeparator, firstIndex + 1);

            if (nextIndex > firstIndex)
            {
                string slength = segment.Substring(firstIndex + 1, nextIndex - firstIndex - 1);
                binaryStart = nextIndex + 1;
                int length;
                if (int.TryParse(slength, out length))
                {
                    return length;
                }
            }
            
            return 0;
        }
        
        public virtual string ToX12String(bool addWhitespace)
        {
            var sb = new StringBuilder();
            if (addWhitespace)
            {
                sb.AppendLine();
            }

            sb.Append(this.SegmentString);
            if (this.DelimiterSet.SegmentTerminator != '\r' && this.DelimiterSet.SegmentTerminator != '\n')
            {
                sb.Append(this.DelimiterSet.SegmentTerminator);
            }

            return sb.ToString();
        }

        public string SerializeToX12(bool addWhitespace)
        {
            return this.ToX12String(addWhitespace).Trim();
        }

        public override string ToString()
        {
            return this.SegmentString;
        }

        internal virtual void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.SegmentId))
            {
                writer.WriteStartElement(this.SegmentId);
                for (int i = 0; i < this.DataElements.Count; i++)
                {
                    string elementName = string.Format("{0}{1:00}", this.SegmentId, i + 1);
                    var identifiers = new List<AllowedIdentifier>();

                    if (this.SegmentSpec != null 
                        && this.SegmentSpec.Elements.Count > i 
                        && !string.IsNullOrEmpty(this.DataElements[i]))
                    {
                        writer.WriteComment(this.SegmentSpec.Elements[i].Name);
                        identifiers = this.SegmentSpec.Elements[i].AllowedIdentifiers;
                    }

                    if (this.DataElements[i].IndexOf(this.DelimiterSet.SubElementSeparator) < 0 
                        || this.SegmentId == "BIN" 
                        || this.SegmentId == "BDS")
                    {
                        writer.WriteStartElement(elementName);
                        writer.WriteValue(this.DataElements[i]);
                        if (this.SegmentSpec != null
                            && this.SegmentSpec.Elements.Count > i 
                            && this.SegmentSpec.Elements[i].Type == ElementDataType.Identifier)
                        {
                            var allowedValue = identifiers.FirstOrDefault(ai => ai.ID == this.DataElements[i]);
                            if (allowedValue != null)
                            {
                                writer.WriteComment(allowedValue.Description);
                            }
                        }

                        writer.WriteEndElement();
                    }
                    else
                    {
                        writer.WriteStartElement(elementName);
                        var subElements = this.DataElements[i].Split(this.DelimiterSet.SubElementSeparator);
                        for (int j = 0; j < subElements.Length; j++)
                        {
                            string subElementName = string.Format("{0}{1:00}", elementName, j + 1);
                            writer.WriteStartElement(subElementName);
                            writer.WriteValue(subElements[j]);
                            if (this.SegmentSpec != null 
                                && this.SegmentSpec.Elements.Count > i 
                                && this.SegmentSpec.Elements[i].Type == ElementDataType.Identifier)
                            {
                                var allowedValue = identifiers.FirstOrDefault(ai => ai.ID == subElements[j]);
                                if (allowedValue != null)
                                {
                                    writer.WriteComment(allowedValue.Description);
                                }
                            }

                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
            }
        }

        protected override void ValidateAgainstSegmentSpecification(string elementId, int elementNumber, string value)
        {
            ElementSpecification spec = this.SegmentSpec?.Elements[elementNumber - 1];

            if (spec != null)
            {
                if (value.Length == 0 && spec.Required)
                {
                    throw new ElementValidationException(Resources.ElementRequiredError, elementId, value);
                }

                if (value.Length > 0)
                {
                    if (value.Length < spec.MinLength || spec.MaxLength > 0 && value.Length > spec.MaxLength)
                    {
                        throw new ElementValidationException(
                            "Element {0} cannot contain the value '{1}' because it must be between {2} and {3} characters in length.",
                            elementId,
                            value,
                            spec.MinLength,
                            spec.MaxLength);
                    }
                }

                switch (spec.Type)
                {
                    case ElementDataType.Numeric:
                        int number;
                        if (!int.TryParse(value, out number))
                        {
                            throw new ElementValidationException(
                                "Element {0} cannot contain the value '{1}' because it is constrained to be an implied decimal.",
                                elementId,
                                value);
                        }

                        break;
                    case ElementDataType.Decimal:
                        decimal decNumber;
                        if (!decimal.TryParse(value, out decNumber))
                        {
                            throw new ElementValidationException(
                                "Element {0} cannot contain the value '{1}' because it is constrained to be a decimal.",
                                elementId,
                                value);
                        }

                        break;
                    case ElementDataType.Identifier:
                        if (spec.AllowedListInclusive && spec.AllowedIdentifiers.Count > 0)
                        {
                            if (spec.AllowedIdentifiers.FirstOrDefault(ai => ai.ID == value) == null)
                            {
                                string[] ids = new string[spec.AllowedIdentifiers.Count];
                                for (int i = 0; i < spec.AllowedIdentifiers.Count; i++)
                                {
                                    ids[i] = spec.AllowedIdentifiers[i].ID;
                                }

                                string expected = string.Empty;
                                if (ids.Length > 1)
                                {
                                    expected = string.Join(", ", ids, 0, ids.Length - 1);
                                    expected += " or " + ids[ids.Length - 1];
                                }
                                else
                                {
                                    expected = ids[0];
                                }

                                throw new ElementValidationException(
                                    "Element '{0}' cannot contain the value '{1}'.  Specification restricts this to {2}.",
                                    elementId,
                                    value,
                                    expected);
                            }
                        }

                        break;
                }
            }
        }

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
    }
}
