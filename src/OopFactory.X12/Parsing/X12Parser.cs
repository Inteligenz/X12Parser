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
                        fg = new FunctionGroup(envelop, delimiters, segmentString);
                        envelop.FunctionGroups.Add(fg);
                        currentContainer = fg;
                        break;
                    case "GE":
                        fg.AddTerminatingSegment(fg, segmentString);
                        fg = null;
                        break;
                    case "ST":
                        tr = fg.CreateTransaction(fg, segmentString, _specification);
                        fg.Transactions.Add(tr);
                        currentContainer = tr;
                        hloops = new Dictionary<string, HierarchicalLoop>();
                        break;
                    case "CTT":
                        tr.AddTerminatingSegment(tr, segmentString);
                        break;
                    case "SE":
                        tr.AddTerminatingSegment(tr, segmentString);
                        tr = null;
                        break;
                    case "HL":
                        HierarchicalLoop hl = tr.CreateHLoop(tr, tr, segmentString);
                        if (hl.ParentId == "")
                            tr.HLoops.Add(hl);
                        else
                        {
                            if (hloops.ContainsKey(hl.ParentId))
                            {
                                hl = tr.CreateHLoop(hloops[hl.ParentId], tr, segmentString);
                                hloops[hl.ParentId].HLoops.Add(hl);
                            }
                            else
                                throw new InvalidOperationException(String.Format("Hierarchical Loop {0} expects Parent ID {1} which did not occur preceding it.", hl.Id, hl.ParentId));
                        }
                        if (hloops.ContainsKey(hl.Id))
                            throw new InvalidOperationException(String.Format("Hierarchical Loop {0} cannot be added to transaction {1} because the ID {2} already exists.", hl.SegmentString, tr.ControlNumber, hl.Id));
                        hloops.Add(hl.Id, hl);
                        currentContainer = hl;
                        break;
                    case "IEA":
                        envelop.AddTerminatingSegment(envelop, segmentString);
                        break;
                    default:
                        while (currentContainer != null)
                        {
                            if (currentContainer is LoopContainer)
                            {
                                LoopContainer loopContainer = (LoopContainer)currentContainer;

                                Segment segment = loopContainer.CreateSegment(tr, segmentString);
                                if (segment is Loop)
                                {
                                    loopContainer.Loops.Add((Loop)segment);
                                    currentContainer = (Loop)segment;
                                    break;
                                }
                                else
                                {
                                    if (currentContainer.AddSegment(segmentString))
                                        break;
                                    else
                                    {
                                        currentContainer = currentContainer.Parent;
                                        continue;
                                    }
                                }
                            }
                            else
                            {
                                if (currentContainer.AddSegment(segmentString))
                                    break;
                                else
                                {
                                    currentContainer = currentContainer.Parent;
                                    continue;
                                }
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
