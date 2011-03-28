using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Specification;
using System.IO;
namespace OopFactory.X12.Parsing
{
    internal class X12Parser
    {
        private TransactionSpecification _specification;
        
        public X12Parser(TransactionSpecification specification)
        {
            _specification = specification;
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
        
        internal Interchange Parse(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            
            char[] header = new char[106];
            if (reader.Read(header, 0, 106) < 106)
                throw new ArgumentException("ISA segment and terminator is expected to be at least 106 characters.");

            X12DelimiterSet delimiters = new X12DelimiterSet(header);

            Interchange envelop = new Interchange(new string(header));
            Container currentContainer = envelop;
            FunctionGroup fg = null;
            Transaction tr = null;
            Dictionary<string, HierarchicalLoop> hloops = new Dictionary<string,HierarchicalLoop>();
            string segmentString = ReadNextSegment(reader, delimiters.SegmentTerminator);
            string segmentId = ReadSegmentId(segmentString, delimiters.ElementSeparator);
            while (segmentString.Length > 0)
            {
                switch (segmentId)
                {
                    case "GS":
                        if (envelop == null)
                            throw new InvalidOperationException(String.Format("Segment '{0}' cannot occur before and ISA segment.", segmentString));

                        currentContainer = fg = envelop.AddFunctionGroup(segmentString);;
                        break;
                    case "GE":
                        if (fg == null)
                            throw new InvalidOperationException(String.Format("Segment '{0}' does not have a matching GS segment precending it.", segmentString));
                        fg.AddTerminatingSegment(fg, segmentString);
                        fg = null;
                        break;
                    case "ST":
                        if (fg == null)
                            throw new InvalidOperationException(string.Format("segment '{0}' cannot occur without a preceding GS segment.", segmentString));

                        currentContainer = tr = fg.AddTransaction(segmentString, _specification);
                        hloops = new Dictionary<string, HierarchicalLoop>();
                        break;
                    case "CTT":
                        if (tr == null)
                            throw new InvalidOperationException(string.Format("Segment '{0}' must occur within a transaction set ST and SE segments.", segmentString));

                        tr.AddTerminatingSegment(tr, segmentString);
                        break;
                    case "SE":
                        if (tr == null)
                            throw new InvalidOperationException(string.Format("Segment '{0}' does not have a matching ST segment preceding it.", segmentString));

                        tr.AddTerminatingSegment(tr, segmentString);
                        tr = null;
                        break;
                    case "HL":
                        Segment hlSegment = new Segment(null, delimiters, segmentString);
                        string id = hlSegment.DataElements[0];
                        string parentId = hlSegment.DataElements[1];

                        if (parentId == "")
                            currentContainer = tr.AddHLoop(tr, segmentString); 
                        else
                        {
                            if (hloops.ContainsKey(parentId))
                                currentContainer = hloops[parentId].AddHLoop(tr, segmentString);
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
                        envelop.AddTerminatingSegment(envelop, segmentString);
                        break;
                    default:
                        while (currentContainer != null)
                        {
                            if (currentContainer is LoopContainer)
                            {
                                LoopContainer loopContainer = (LoopContainer)currentContainer;

                                LoopSpecification loopSpec = loopContainer.GetLoopSpecification(segmentString);

                                if (loopSpec != null)
                                {
                                    currentContainer = loopContainer.AddLoop(segmentString, loopSpec);
                                    break;
                                }
                            }
                                
                            if (currentContainer.AddSegment(segmentString))
                                break;
                            else
                            {
                                currentContainer = currentContainer.Parent;
                                continue;
                            }
                        }
                        if (currentContainer == null)
                        {
                            throw new InvalidOperationException(String.Format(
                                "Segment {0} cannot be identified in the specification for {1}.", segmentString, _specification.TransactionSetIdentifierCode));
                        }
                        break;

                }
                segmentString = ReadNextSegment(reader, delimiters.SegmentTerminator);
                segmentId = ReadSegmentId(segmentString, delimiters.ElementSeparator);
            
            }
            
            return envelop;
        }
    }
}
