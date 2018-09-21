namespace OopFactory.X12.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using OopFactory.X12.Shared.Properties;
    using OopFactory.X12.Specifications;
    using OopFactory.X12.Specifications.Finders;
    using OopFactory.X12.Specifications.Interfaces;

    public class Interchange : Container
    {
        private readonly List<FunctionGroup> functionGroups;

        public Interchange(ISpecificationFinder specFinder, string segmentString)
            : base(null, new X12DelimiterSet(segmentString.ToCharArray()), segmentString)
        {
            this.SpecFinder = specFinder;
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
            this.SpecFinder = specFinder;
            if (controlNumber > 999999999 || controlNumber < 1)
            {
                throw new ElementValidationException(
                    Resources.InterchangeValueOutOfRange,
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
            set { this.SetElement(1, $"{value,-2}"); }
        }

        public string AuthorInfo
        {
            get { return this.GetElement(2); }
            set { this.SetElement(2, $"{value,-10}"); }
        }

        public string SecurityInfoQualifier
        {
            get { return this.GetElement(3); }
            set { this.SetElement(3, $"{value,-2}"); }
        }

        public string SecurityInfo
        {
            get { return this.GetElement(4); }
            set { this.SetElement(4, $"{value,-10}"); }
        }

        public string InterchangeSenderIdQualifier
        {
            get { return this.GetElement(5); }
            set { this.SetElement(5, $"{value,-2}"); }
        }

        public string InterchangeSenderId
        {
            get { return this.GetElement(6); }
            set { this.SetElement(6, $"{value,-15}"); }
        }

        public string InterchangeReceiverIdQualifier
        {
            get { return this.GetElement(7); }
            set { this.SetElement(7, $"{value,-2}"); }
        }

        public string InterchangeReceiverId
        {
            get { return this.GetElement(8); }
            set { this.SetElement(8, $"{value,-15}"); }
        }

        /// <summary>
        /// Gets or sets the interchange date
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if ISA date time elements cannot be parsed</exception>
        public DateTime InterchangeDate
        {
            get
            {
                DateTime date;
                if (DateTime.TryParseExact(this.GetElement(9) + this.GetElement(10), "yyMMddHHmm", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }

                if (DateTime.TryParseExact(this.GetElement(9), "yyMMdd", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }

                throw new ArgumentException(
                    string.Format(
                        Resources.IsaDateTimeParsingError,
                        this.GetElement(9),
                        this.GetElement(10)));
            }

            set
            {
                this.SetElement(9, $"{value:yyMMdd}");
                this.SetElement(10, $"{value:HHmm}");
            }
        }

        public string InterchangeControlNumber => this.GetElement(13);

        public IEnumerable<FunctionGroup> FunctionGroups => this.functionGroups;

        internal ISpecificationFinder SpecFinder { get; }

        internal override IList<SegmentSpecification> AllowedChildSegments => new List<SegmentSpecification>();

        internal override IEnumerable<string> TrailerSegmentIds => new List<string>();

        public FunctionGroup AddFunctionGroup(string segmentString)
        {
            var fg = new FunctionGroup(this.SpecFinder, this, this.DelimiterSet, segmentString);
            this.functionGroups.Add(fg);
            return fg;
        }

        public FunctionGroup AddFunctionGroup(string functionIdCode, DateTime date, int controlNumber)
        {
            return this.AddFunctionGroup(functionIdCode, date, controlNumber, "004010X096A1");
        }

        /// <summary>
        /// Adds a new <see cref="FunctionGroup"/> to the interchange
        /// </summary>
        /// <param name="functionIdCode">Id code of new FunctionGroup</param>
        /// <param name="date">DateTime for function group</param>
        /// <param name="controlNumber">FunctionGroup control number</param>
        /// <param name="version">Version for FunctionGroup</param>
        /// <returns>New FunctionGroup object</returns>
        /// <exception cref="ElementValidationException">Thrown if the control number is not within acceptable range</exception>
        public FunctionGroup AddFunctionGroup(string functionIdCode, DateTime date, int controlNumber, string version)
        {
            if (controlNumber > 999999999 || controlNumber < 1)
            {
                throw new ElementValidationException(
                    Resources.ElementValueOutOfRange,
                    "GS06",
                    controlNumber.ToString());
            }

            var fg = new FunctionGroup(
                this.SpecFinder,
                this,
                this.DelimiterSet,
                string.Format("GS{0}{0}{0}{0}{0}{0}{0}X{0}{2}{1}", this.DelimiterSet.ElementSeparator, this.DelimiterSet.SegmentTerminator, version))
            {
                FunctionalIdentifierCode = functionIdCode,
                Date = date,
                ControlNumber = controlNumber
            };

            fg.SetTerminatingTrailerSegment(string.Format("GE{0}0{0}{2}{1}", this.DelimiterSet.ElementSeparator, this.DelimiterSet.SegmentTerminator, controlNumber));
            this.functionGroups.Add(fg);
            return fg;
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

        internal override string SerializeBodyToX12(bool addWhitespace)
        {
            var sb = new StringBuilder();
            foreach (var fg in this.functionGroups)
            {
                sb.Append(fg.ToX12String(addWhitespace));
            }

            return sb.ToString();
        }

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

        private void RemoveComments(XmlElement element)
        {
            var comments = new List<XmlComment>();

            foreach (XmlNode childElement in element.ChildNodes)
            {
                if (childElement is XmlComment xmlComment)
                {
                    comments.Add(xmlComment);
                }
            }

            foreach (XmlComment comment in comments)
            {
                XmlWhitespace prev = comment.PreviousSibling as XmlWhitespace;
                XmlWhitespace next = comment.NextSibling as XmlWhitespace;
                if (prev?.Value != null && prev.Value.StartsWith(Environment.NewLine)
                    && next?.Value != null && next.Value.StartsWith(Environment.NewLine))
                {
                    element.RemoveChild(next);
                }

                element.RemoveChild(comment);
            }

            foreach (XmlNode childElement in element.ChildNodes)
            {
                if (childElement is XmlElement xmlElement && childElement.HasChildNodes)
                {
                    this.RemoveComments(xmlElement);
                }
            }
        }
    }
}
