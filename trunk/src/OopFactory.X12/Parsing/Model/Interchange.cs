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

            AddTerminatingSegment(this, 
                String.Format("IEA{0}0{0}{2:000000000}{1}", delimiters.ElementSeparator, delimiters.SegmentTerminator, controlNumber));
        }

        public Interchange (DateTime date, int controlNumber, bool production)
            : this(date, controlNumber, production, new X12DelimiterSet('~', '*', ':'))
        {            
        }

        public Interchange(DateTime date, int controlNumber, bool production, char segmentTerminator, char elementSeparator, char subElementSeparator)
            : this(date, controlNumber, production, new X12DelimiterSet(segmentTerminator, elementSeparator, subElementSeparator))
        {
        }

        public string SenderId
        {
            get { return GetElement(6); }
            set
            {
                if (value != null && value.Length > 15)
                    throw new ArgumentOutOfRangeException("SenderId", value, "SenderId cannot exceed 15 characters.");

                SetElement(6, String.Format("{0,-15}", value));
            }
        }

        public string ReceiverId
        {
            get { return GetElement(8); }
            set
            {
                if (value != null && value.Length > 15)
                    throw new ArgumentOutOfRangeException("ReceiverId", value, "ReceiverId cannot exceed 15 characters.");

                SetElement(8, String.Format("{0,-15}", value));
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

        public string SerializeToX12(bool addWhitespace)
        {
            return this.ToX12String(addWhitespace).Trim();
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

                foreach (var segment in this.TerminatingSegments)
                    segment.WriteXml(writer);

            }
        }

        #endregion
    }
}
