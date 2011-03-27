using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing
{
    internal class X12Parser
    {
        private List<Segment> _segments;
        private TransactionSpecification _specification;
        private X12DelimiterSet _delimiters;

        public X12Parser(string rawX12, TransactionSpecification specification)
        {
            if (rawX12.Length < 106)
                throw new ArgumentException("ISA segment and terminator is expected to be at least 106 characters.");
            _delimiters = new X12DelimiterSet(rawX12.Substring(0, 106));

            _specification = specification;
            string[] segments = rawX12.Split(_delimiters.SegmentTerminator);

            _segments = new List<Segment>();
            foreach (var segment in segments)
            {
                _segments.Add(new Segment(_delimiters, segment.Trim()));
            }
        }
                
        private bool ParseSegment(Transaction transaction, LoopContainer currentContainer, ref int index)
        {
            if (index < _segments.Count)
            {
                var segmentId = _segments[index].SegmentId;
                Segment segment = currentContainer.CreateSegment(transaction, _segments[index]);

                if (segment is HierarchicalLoop) // This is a hierarchical loop
                {
                    if (currentContainer is HierarchicalLoopContainer)
                    {
                        var hloop = (HierarchicalLoop)segment;
                        transaction.AllLoops.Add(hloop);

                        if (string.IsNullOrEmpty(hloop.ParentId)) // add top level loop to transaction
                        {
                            transaction.HLoops.Add((HierarchicalLoop)hloop);
                        }
                        else // add nested loop to its parent
                        {
                            var parent = transaction.AllLoops.Where(hl => hl.Id == hloop.ParentId).FirstOrDefault();
                            parent.HLoops.Add((HierarchicalLoop)hloop);
                        }
                        index++;
                        segmentId = _segments[index].SegmentId;
                        while (index > 0 && index < _segments.Count && segmentId != "SE")
                        {
                            var parsed = ParseSegment(transaction, hloop, ref index);
                            if (index < _segments.Count)
                                segmentId = _segments[index].SegmentId;
                            if (!parsed)
                            {
                                return false;
                            }
                        }
                        index++;
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    if (segment is Loop)
                    {
                        currentContainer.Loops.Add((Loop)segment);

                        index++;
                        while (ParseSegment(transaction, (LoopContainer)segment, ref index)) ;
                        return true;
                    }
                    else if (currentContainer.AllowedChildSegments.Any(cs => cs.SegmentId == segment.SegmentId))
                    {
                        currentContainer.AddSegment(segment);
                        index++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else return false;
        }

        internal Interchange Parse()
        {
            int index = 0;
            var envelop = new Interchange(_segments[index++].SegmentString);
            while (_segments[index].SegmentId == "GS")
            {
                var functionGroup = new FunctionGroup(_delimiters, _segments[index++].SegmentString);
                envelop.FunctionGroups.Add(functionGroup);
                while (index < _segments.Count && _segments[index].SegmentId != "ST")
                    envelop.AddSegment(_segments[index++].SegmentString);

                var transaction = functionGroup.CreateTransaction(_segments[index].SegmentString, _specification);
                functionGroup.Transactions.Add(transaction);

                index++;
                bool done = false;
                do
                {
                    if (!ParseSegment(transaction, transaction, ref index))
                    {
                        var segmentId = _segments[index].SegmentId;
                        switch (segmentId)
                        {
                            case "ST":
                                transaction = functionGroup.CreateTransaction(_segments[index].SegmentString, _specification);
                                functionGroup.Transactions.Add(transaction);
                                index++;
                                break;
                            case "GS":
                                done = true;
                                break;
                            case "GE":
                                index++;
                                break;
                            default:
                                done = true;
                                break;
                        }
                    }

                } while (!done);
            }

            return envelop;
        }
    }
}
