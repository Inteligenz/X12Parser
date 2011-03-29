using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Specification;
using System.IO;
namespace OopFactory.X12.Parsing
{
    public class X12Parser
    {
        private TransactionSpecification _specification;
        
        public X12Parser(string transactionType)
        {
            _specification = GetSpec(transactionType);
        }

        private static TransactionSpecification GetSpec(string transactionType)
        {
            switch (transactionType)
            {
                case "835":
                    return EmbeddedResources.Get835TransactionSpecification();
                case "837":
                    return EmbeddedResources.Get837TransactionSpecification();
                case "856":
                    return EmbeddedResources.Get856TransactionSpecification();
                default:
                    throw new NotSupportedException(String.Format("Transaction Type {0} is not supported.", transactionType));
            }
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

        public Interchange Create(string transactionType, DateTime date)
        {
            return Create(transactionType, date, '~', '*', ':');
        }

        public Interchange Create(string transactionType, DateTime date, char segmentDelimiter, char elementDelimiter, char subElementDelimiter)
        {
            string header = String.Format("ISA{1}00{1}          {1}00{1}          {1}01{1}SENDERID HERE  {1}01{1}RECIEVERID HERE{1}{3:yyMMdd}{1}{3:hhmm}{1}U{1}00401{1}000000001{1}1{1}P{1}{2}{0}", 
                segmentDelimiter, elementDelimiter, subElementDelimiter,
                date);
            return new Interchange(GetSpec(transactionType), header);
        }
        
        public Interchange Parse(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            
            char[] header = new char[106];
            if (reader.Read(header, 0, 106) < 106)
                throw new ArgumentException("ISA segment and terminator is expected to be at least 106 characters.");

            X12DelimiterSet delimiters = new X12DelimiterSet(header);

            Interchange envelop = new Interchange(_specification, new string(header));
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

                        currentContainer = tr = fg.AddTransaction(segmentString);
                        hloops = new Dictionary<string, HierarchicalLoop>();
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
