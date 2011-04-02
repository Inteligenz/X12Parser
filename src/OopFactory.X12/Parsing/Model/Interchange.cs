using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing.Model
{
    public class Interchange : Container, IXmlSerializable
    {
        private List<FunctionGroup> _functionGroups;

        internal Interchange() : base(null, null, "GS") { }

        internal Interchange(string segmentString)
            : base(null, new X12DelimiterSet(segmentString.ToCharArray()), segmentString)
        {
            _functionGroups = new List<FunctionGroup>();
        }

        internal Interchange(DateTime date, int controlNumber, bool production,  X12DelimiterSet delimiters)
            : base(null, delimiters, String.Format("ISA{1}00{1}          {1}00{1}          {1}01{1}SENDERID HERE  {1}01{1}RECIEVERID HERE{1}{3:yyMMdd}{1}{3:hhmm}{1}U{1}00401{1}{4:000000000}{1}1{1}{5}{1}{2}{0}",
                delimiters.SegmentTerminator, delimiters.ElementSeparator, delimiters.SubElementSeparator, date, controlNumber, production ? "P" : "T"))
        {
            if (controlNumber > 999999999 || controlNumber < 1)
                throw new ArgumentOutOfRangeException("controlNumber", controlNumber, "ControlNumber must be a positive number between 1 and 999999999.");

            _functionGroups = new List<FunctionGroup>();

            SetTerminatingTrailerSegment(String.Format("IEA{0}0{0}{2:000000000}{1}", delimiters.ElementSeparator, delimiters.SegmentTerminator, controlNumber));
        }

        public Interchange (DateTime date, int controlNumber, bool production)
            : this(date, controlNumber, production, new X12DelimiterSet('~', '*', ':'))
        {            
        }

        public Interchange(DateTime date, int controlNumber, bool production, char segmentTerminator, char elementSeparator, char subElementSeparator)
            : this(date, controlNumber, production, new X12DelimiterSet(segmentTerminator, elementSeparator, subElementSeparator))
        {
        }

        public string AuthorInfoQualifier
        {
            get { return GetElement(1); }
            set { SetElement(1, String.Format("{0,-2}", value)); }
        }

        public string AuthorInfo
        {
            get { return GetElement(2); }
            set { SetElement(2, String.Format("{0,-10}", value)); }
        }

        public string SecurityInfoQualifier
        {
            get { return GetElement(3); }
            set { SetElement(3, String.Format("{0,-2}", value)); }
        }

        public string SecurityInfo
        {
            get { return GetElement(4); }
            set { SetElement(4, String.Format("{0,-10}", value)); }
        }

        public string InterchangeSenderIdQualifier
        {
            get { return GetElement(5); }
            set { SetElement(5, String.Format("{0,-2}", value)); }
        }

        public string InterchangeSenderId
        {
            get { return GetElement(6); }
            set { SetElement(6, String.Format("{0,-15}", value)); }
        }

        public string InterchangeReceiverIdQualifier
        {
            get { return GetElement(7); }
            set { SetElement(7, String.Format("{0,-2", value)); }
        }

        public string InterchangeReceiverId
        {
            get { return GetElement(8); }
            set { SetElement(8, String.Format("{0,-15}", value)); }
        }

        public DateTime InterchangeDate
        {
            get
            {
                DateTime date;
                if (DateTime.TryParseExact(GetElement(9) + GetElement(10), "yyMMddhhmm", null, System.Globalization.DateTimeStyles.None, out date))
                    return date;
                else if (DateTime.TryParseExact(GetElement(9), "yyMMdd", null, System.Globalization.DateTimeStyles.None, out date))
                    return date;
                else
                    throw new ArgumentException(String.Format("{0} and {1} cannot be converted into a date and time.", GetElement(9), GetElement(10)));
                
            }
            set
            {
                SetElement(9, string.Format("{0:yyMMdd}", value));
                SetElement(10, string.Format("{0:hhmm}", value));
            }
        }
        
        public IEnumerable<FunctionGroup> FunctionGroups
        {
            get { return _functionGroups; }
        }

        internal FunctionGroup AddFunctionGroup(string segmentString)
        {
            if (!segmentString.StartsWith("GS" + _delimiters.ElementSeparator))
                throw new InvalidOperationException(String.Format("Segment {0} does not start with GS{1} as expected.", segmentString, _delimiters.ElementSeparator));

            FunctionGroup fg = new FunctionGroup(this, _delimiters, segmentString);
            _functionGroups.Add(fg);
            return fg;
        }

        public FunctionGroup AddFunctionGroup(string functionIdCode, DateTime date, int controlNumber)
        {
            if (controlNumber > 999999999 || controlNumber < 1)
                throw new ArgumentOutOfRangeException("controlNumber", controlNumber, "ControlNumber must be a positive number between 1 and 999999999.");

            FunctionGroup fg = new FunctionGroup(this, _delimiters,
                string.Format("GS{0}{0}{0}{0}{0}{0}{0}X{0}004010X096A1{1}", _delimiters.ElementSeparator, _delimiters.SegmentTerminator));
            fg.FunctionalIdentifierCode = functionIdCode;
            fg.Date = date;
            fg.ControlNumber = controlNumber;

            fg.SetTerminatingTrailerSegment(String.Format("GE{0}0{0}{2}{1}", _delimiters.ElementSeparator, _delimiters.SegmentTerminator, controlNumber));
            _functionGroups.Add(fg);
            return fg;
        }
        internal override IList<SegmentSpecification> AllowedChildSegments
        {
            get
            {
                var list = new List<SegmentSpecification>();
                return list;
            }
        }

        internal override string SerializeBodyToX12(bool addWhitespace)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var fg in _functionGroups)
                sb.Append(fg.ToX12String(addWhitespace));
            return sb.ToString();
        }

        internal override string ToX12String(bool addWhitespace)
        {
            UpdateTrailerSegmentCount("IEA", 1, _functionGroups.Count);
            return base.ToX12String(addWhitespace);
        }

        

        public virtual string Serialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(
                this.GetType());
            MemoryStream memoryStream = new MemoryStream();

            xmlSerializer.Serialize(memoryStream, this);
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader streamReader = new StreamReader(memoryStream);
            return streamReader.ReadToEnd();

        }

        #region IXmlSerializable Members

        internal override void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(SegmentId))
            {
                base.WriteXml(writer);

                foreach (var segment in this.Segments)
                    segment.WriteXml(writer);

                foreach (var functionGroup in this.FunctionGroups)
                    functionGroup.WriteXml(writer);

                foreach (var segment in this.TrailerSegments)
                    segment.WriteXml(writer);

            }
        }

        #endregion
    }
}
