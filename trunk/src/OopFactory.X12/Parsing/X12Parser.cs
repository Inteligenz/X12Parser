using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Reflection;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing
{
    public class X12Parser
    {
        private ISpecificationFinder _specFinder;

        public X12Parser(ISpecificationFinder specFinder)
        {
            _specFinder = specFinder;
        }

        public X12Parser()
            : this(new SpecificationFinder())
        {

        }
        private string ReadNextSegment(StreamReader reader, char segmentDelimiter)
        {
            StringBuilder sb = new StringBuilder();
            char[] one = new char[1];
            while (reader.Read(one, 0, 1) == 1)
            {
                if (one[0] == segmentDelimiter)
                    break;
                else
                    sb.Append(one);
            }
            return sb.ToString().TrimStart();
        }

        private string ReadSegmentId(string segmentString, char elementDelimiter)
        {
            int index = segmentString.IndexOf(elementDelimiter);
            if (index >= 0)
                return segmentString.Substring(0, index);
            else
                return null;
        }

        public Interchange Parse(string x12)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(x12);
            using (MemoryStream mstream = new MemoryStream(byteArray))
            {
                return Parse(mstream);
            }
        }
                       
        public Interchange Parse(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                char[] header = new char[106];
                if (reader.Read(header, 0, 106) < 106)
                    throw new ArgumentException("ISA segment and terminator is expected to be at least 106 characters.");

                X12DelimiterSet delimiters = new X12DelimiterSet(header);

                Interchange envelop = new Interchange(_specFinder, new string(header));
                Container currentContainer = envelop;
                FunctionGroup fg = null;
                Transaction tr = null;
                Dictionary<string, HierarchicalLoop> hloops = new Dictionary<string, HierarchicalLoop>();
                string segmentString = ReadNextSegment(reader, delimiters.SegmentTerminator);
                string segmentId = ReadSegmentId(segmentString, delimiters.ElementSeparator);
                int segmentIndex = 1;
                while (segmentString.Length > 0)
                {
                    switch (segmentId)
                    {
                        case "GS":
                            if (envelop == null)
                                throw new InvalidOperationException(String.Format("Segment '{0}' cannot occur before and ISA segment.", segmentString));

                            currentContainer = fg = envelop.AddFunctionGroup(segmentString); ;
                            break;
                        case "GE":
                            if (fg == null)
                                throw new InvalidOperationException(String.Format("Segment '{0}' does not have a matching GS segment precending it.", segmentString));
                            fg.SetTerminatingTrailerSegment(segmentString);
                            fg = null;
                            break;
                        case "ST":
                            if (fg == null)
                                throw new InvalidOperationException(string.Format("segment '{0}' cannot occur without a preceding GS segment.", segmentString));

                            currentContainer = tr = fg.AddTransaction(segmentString);
                            hloops = new Dictionary<string, HierarchicalLoop>();
                            break;
                        case "SE":
                            if (tr == null)
                                throw new InvalidOperationException(string.Format("Segment '{0}' does not have a matching ST segment preceding it.", segmentString));

                            tr.SetTerminatingTrailerSegment(segmentString);
                            tr = null;
                            break;
                        case "HL":
                            Segment hlSegment = new Segment(null, delimiters, segmentString);
                            string id = hlSegment.GetElement(1);
                            string parentId = hlSegment.GetElement(2);

                            if (parentId == "")
                                currentContainer = tr.AddHLoop(segmentString);
                            else
                            {
                                if (hloops.ContainsKey(parentId))
                                    currentContainer = hloops[parentId].AddHLoop(segmentString);
                                else
                                    throw new InvalidOperationException(String.Format("Hierarchical Loop {0} expects Parent ID {1} which did not occur preceding it.", id, parentId));
                            }
                            if (hloops.ContainsKey(id))
                                throw new InvalidOperationException(String.Format("Hierarchical Loop {0} cannot be added to transaction {1} because the ID {2} already exists.", segmentString, tr.ControlNumber, id));
                            hloops.Add(id, (HierarchicalLoop)currentContainer);
                            break;
                        case "IEA":
                            if (envelop == null)
                                throw new InvalidOperationException(string.Format("Segment {0} does not have a matching ISA segment preceding it.", segmentString));
                            envelop.SetTerminatingTrailerSegment(segmentString);
                            break;
                        case "TA1": // Technical acknowledgement
                            if (envelop == null)
                                throw new InvalidOperationException(string.Format("Segment {0} does not have a matching ISA segment preceding it.", segmentString));
                            envelop.AddSegment(segmentString);
                            break;  
                        default:
                            while (currentContainer != null)
                            {
                                if (currentContainer.AddSegment(segmentString) != null)
                                    break;
                                else
                                {
                                    if (currentContainer is LoopContainer)
                                    {
                                        LoopContainer loopContainer = (LoopContainer)currentContainer;

                                        try
                                        {
                                            currentContainer = loopContainer.AddLoop(segmentString);
                                            break;
                                        }
                                        catch (TransactionValidationException)
                                        {
                                            if (currentContainer is Transaction)
                                            {
                                                var tran = (Transaction)currentContainer;

                                                throw new TransactionValidationException(
                                                    "Segment '{3}' in position {4} within transaction '{1}' cannot be identified within the supplied specification for transaction set {0}.", tran.IdentifierCode, tran.ControlNumber, "", segmentString, segmentIndex);
                                            }
                                            else
                                            {
                                                currentContainer = currentContainer.Parent;
                                                continue;
                                            }
                                        }
                                    }
                                    else
                                        break;
                                }
                            }
                            break;

                    }
                    segmentString = ReadNextSegment(reader, delimiters.SegmentTerminator);
                    segmentId = ReadSegmentId(segmentString, delimiters.ElementSeparator);
                    segmentIndex++;
                }
                return envelop;
            }
        }

        public string TransformToX12(string xml)
        {
            var transform = new XslCompiledTransform();
            transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.X12-XML-to-X12.xslt")));

            using (var writer = new StringWriter())
            {
                transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), writer);
                return writer.GetStringBuilder().ToString();
            }
        }

        public List<Interchange> UnbundleByLoop(Interchange interchange, string loopId)
        {
            char terminator = interchange._delimiters.SegmentTerminator;
            var service = new UnbundlingService(interchange._delimiters.SegmentTerminator);
            string isa = interchange.SegmentString;
            string iea = interchange.TrailerSegments.First().SegmentString;
            List<string> list = new List<string>();
            foreach (FunctionGroup group in interchange.FunctionGroups)
                foreach (Transaction transaction in group.Transactions)
                {
                    service.UnbundleHLoops(list, transaction, loopId);
                }

            List<Interchange> interchanges = new List<Interchange>();
            foreach (var item in list)
            {
                StringBuilder x12 = new StringBuilder();
                x12.AppendFormat("{0}{1}", isa, terminator);
                x12.Append(item);
                x12.AppendFormat("{0}{1}", iea, terminator);
                using (MemoryStream mstream = new MemoryStream(Encoding.ASCII.GetBytes(x12.ToString())))
                {
                    interchanges.Add(Parse(mstream));
                }
            }
            return interchanges;
        }         
    }
}
