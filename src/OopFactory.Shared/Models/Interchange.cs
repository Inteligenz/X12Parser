namespace OopFactory.X12.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    
    using OopFactory.X12.Specifications;
    using OopFactory.X12.Specifications.Finders;
    using OopFactory.X12.Specifications.Interfaces;

    public class Interchange : Container, IXmlSerializable
    {
        private readonly ISpecificationFinder specFinder;
        private readonly List<FunctionGroup> functionGroups;

        public Interchange(ISpecificationFinder specFinder, string segmentString)
            : base(null, new X12DelimiterSet(segmentString.ToCharArray()), segmentString)
        {
            this.specFinder = specFinder;
            this.functionGroups = new List<FunctionGroup>();
        }

        public Interchange(ISpecificationFinder specFinder, DateTime date, int controlNumber, bool production, X12DelimiterSet delimiters)
            : base(null, delimiters, string.Format(
                "ISA{1}00{1}          {1}00{1}          {1}01{1}SENDERID HERE  {1}01{1}RECIEVERID HERE{1}{3:yyMMdd}{1}{3:HHmm}{1}U{1}00401{1}{4:000000000}{1}1{1}{5}{1}{2}{0}",
                delimiters.SegmentTerminator,
                delimiters.ElementSeparator,
                delimiters.SubElementSeparator,
                date,
                controlNumber,
                production ? "P" : "T"))
        {
            this.specFinder = specFinder;
            if (controlNumber > 999999999 || controlNumber < 1)
            {
                throw new ElementValidationException(
                    "{0} Interchange Control Number must be a positive number between 1 and 999999999.",
                    "ISA00",
                    controlNumber.ToString());
            }

            this.functionGroups = new List<FunctionGroup>();
            this.SetTerminatingTrailerSegment(string.Format("IEA{0}0{0}{2:000000000}{1}", delimiters.ElementSeparator, delimiters.SegmentTerminator, controlNumber));
        }

        public Interchange(DateTime date, int controlNumber, bool production)
            : this(new SpecificationFinder(), date, controlNumber, production, new X12DelimiterSet('~', '*', ':'))
        {
        }

        public Interchange(DateTime date, int controlNumber, bool production, char segmentTerminator, char elementSeparator, char subElementSeparator)
            : this(new SpecificationFinder(), date, controlNumber, production, new X12DelimiterSet(segmentTerminator, elementSeparator, subElementSeparator))
        {
        }

        internal Interchange()
            : base(null, null, "GS")
        {
        }

        public string AuthorInfoQualifier
        {
            get { return this.GetElement(1); }
            set { this.SetElement(1, string.Format("{0,-2}", value)); }
        }

        public string AuthorInfo
        {
            get { return this.GetElement(2); }
            set { this.SetElement(2, string.Format("{0,-10}", value)); }
        }

        public string SecurityInfoQualifier
        {
            get { return this.GetElement(3); }
            set { this.SetElement(3, string.Format("{0,-2}", value)); }
        }

        public string SecurityInfo
        {
            get { return this.GetElement(4); }
            set { this.SetElement(4, string.Format("{0,-10}", value)); }
        }

        public string InterchangeSenderIdQualifier
        {
            get { return this.GetElement(5); }
            set { this.SetElement(5, string.Format("{0,-2}", value)); }
        }

        public string InterchangeSenderId
        {
            get { return this.GetElement(6); }
            set { this.SetElement(6, string.Format("{0,-15}", value)); }
        }

        public string InterchangeReceiverIdQualifier
        {
            get { return this.GetElement(7); }
            set { this.SetElement(7, string.Format("{0,-2}", value)); }
        }

        public string InterchangeReceiverId
        {
            get { return this.GetElement(8); }
            set { this.SetElement(8, string.Format("{0,-15}", value)); }
        }

        public DateTime InterchangeDate
        {
            get
            {
                DateTime date;
                if (DateTime.TryParseExact(this.GetElement(9) + this.GetElement(10), "yyMMddHHmm", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }
                else if (DateTime.TryParseExact(this.GetElement(9), "yyMMdd", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    throw new ArgumentException(
                        string.Format(
                            "{0} and {1} in ISA09 and ISA10 cannot be converted into a date and time.",
                            this.GetElement(9),
                            this.GetElement(10)));
                }
            }

            set
            {
                this.SetElement(9, string.Format("{0:yyMMdd}", value));
                this.SetElement(10, string.Format("{0:HHmm}", value));
            }
        }

        public string InterchangeControlNumber => this.GetElement(13);

        public IEnumerable<FunctionGroup> FunctionGroups => this.functionGroups;

        internal ISpecificationFinder SpecFinder => this.specFinder;

        internal override IList<SegmentSpecification> AllowedChildSegments => new List<SegmentSpecification>();

        internal override IEnumerable<string> TrailerSegmentIds => new List<string>();

        public FunctionGroup AddFunctionGroup(string segmentString)
        {
            var fg = new FunctionGroup(specFinder, this, this.DelimiterSet, segmentString);
            this.functionGroups.Add(fg);
            return fg;
        }

        public FunctionGroup AddFunctionGroup(string functionIdCode, DateTime date, int controlNumber)
        {
            return this.AddFunctionGroup(functionIdCode, date, controlNumber, "004010X096A1");
        }

        public FunctionGroup AddFunctionGroup(string functionIdCode, DateTime date, int controlNumber, string version)
        {
            if (controlNumber > 999999999 || controlNumber < 1)
            {
                throw new ElementValidationException(
                    "Element {0} cannot containe the value '{1}' because it must be a positive number between 1 and 999999999.",
                    "GS06",
                    controlNumber.ToString());
            }

            var fg = new FunctionGroup(
                this.specFinder,
                this,
                this.DelimiterSet,
                string.Format("GS{0}{0}{0}{0}{0}{0}{0}X{0}{2}{1}", this.DelimiterSet.ElementSeparator, this.DelimiterSet.SegmentTerminator, version));
            fg.FunctionalIdentifierCode = functionIdCode;
            fg.Date = date;
            fg.ControlNumber = controlNumber;

            fg.SetTerminatingTrailerSegment(string.Format("GE{0}0{0}{2}{1}", this.DelimiterSet.ElementSeparator, this.DelimiterSet.SegmentTerminator, controlNumber));
            this.functionGroups.Add(fg);
            return fg;
        }

        internal override string SerializeBodyToX12(bool addWhitespace)
        {
            var sb = new StringBuilder();
            foreach (var fg in this.functionGroups)
            {
                sb.Append(fg.ToX12String(addWhitespace));
            }

            return sb.ToString();
        }

        public override string ToX12String(bool addWhitespace)
        {
            this.UpdateTrailerSegmentCount("IEA", 1, this.functionGroups.Count);
            return base.ToX12String(addWhitespace);
        }

        public string Serialize()
        {
            return this.Serialize(false);
        }

        private void RemoveComments(XmlElement element)
        {
            List<XmlComment> comments = new List<XmlComment>();

            foreach (XmlNode childElement in element.ChildNodes)
            {
                if (childElement is XmlComment)
                {
                    comments.Add((XmlComment)childElement);
                }
            }

            foreach (XmlComment comment in comments)
            {
                XmlWhitespace prev = comment.PreviousSibling as XmlWhitespace;
                XmlWhitespace next = comment.NextSibling as XmlWhitespace;
                if (prev != null && prev.Value != null & prev.Value.StartsWith(Environment.NewLine)
                    && next != null && next.Value != null && next.Value.StartsWith(Environment.NewLine))
                {
                    element.RemoveChild(next);
                }

                element.RemoveChild(comment);
            }

            foreach (XmlNode childElement in element.ChildNodes)
            {
                if (childElement is XmlElement && childElement.HasChildNodes)
                {
                    this.RemoveComments((XmlElement)childElement);
                }
            }
        }

        public virtual string Serialize(bool suppressComments)
        {
            var memoryStream = new MemoryStream();
            this.Serialize(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var streamReader = new StreamReader(memoryStream);
            string xml = streamReader.ReadToEnd();

            if (suppressComments)
            {
                var doc = new XmlDocument
                {
                    PreserveWhitespace = true
                };
                doc.LoadXml(xml);
                this.RemoveComments((XmlElement)doc.SelectSingleNode("Interchange"));
                xml = doc.OuterXml;
            }

            return xml;
        }

        public void Serialize(Stream stream)
        {
            var xmlSerializer = new XmlSerializer(this.GetType());
            xmlSerializer.Serialize(stream, this);
        }

        #region IXmlSerializable Members

        internal override void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(this.SegmentId))
            {
                switch (this.DelimiterSet.SegmentTerminator)
                {
                    case '\x1D':
                        string terminator = Convert.ToBase64String(Encoding.ASCII.GetBytes(this.DelimiterSet.SegmentTerminator.ToString()));
                        writer.WriteAttributeString("segment-terminator", terminator);
                        break;
                    default:
                        writer.WriteAttributeString("segment-terminator", this.DelimiterSet.SegmentTerminator.ToString());
                        break;
                }

                writer.WriteAttributeString("element-separator", this.DelimiterSet.ElementSeparator.ToString());
                writer.WriteAttributeString("sub-element-separator", this.DelimiterSet.SubElementSeparator.ToString());
                base.WriteXml(writer);

                foreach (var segment in this.Segments)
                {
                    segment.WriteXml(writer);
                }

                foreach (var functionGroup in this.FunctionGroups)
                {
                    functionGroup.WriteXml(writer);
                }

                foreach (var segment in this.TrailerSegments)
                {
                    segment.WriteXml(writer);
                }
            }
        }

        #endregion
    }
}
