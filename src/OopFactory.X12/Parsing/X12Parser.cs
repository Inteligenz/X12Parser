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

        internal virtual Transaction CreateTransaction(Segment segment)
        {
            return new Transaction(_delimiters, segment.SegmentString, _specification);
        }

        internal virtual Segment CreateSegment(Transaction transaction, Container parent, Segment segment)
        {
            if (segment.SegmentId == "HL")
            {
                var hl = new HierarchicalLoop(_delimiters, segment.SegmentString);

                foreach (HierarchicalLoopSpecification hlSpec in transaction.Specification.HierarchicalLoopSpecifications)
                {
                    if (hlSpec.LevelCode.ToString() == hl.LevelCode)
                        hl.Specification = hlSpec;
                }

                return hl;
            }
            else if (parent is LoopContainer) // (transaction.LoopStartingSegmentIds.Contains(segment.SegmentId))
            {
                IList<LoopSpecification> matchingLoopSpecs = null;

                if (((LoopContainer)parent).AllowedChildLoops != null)
                    matchingLoopSpecs = ((LoopContainer)parent).AllowedChildLoops.Where(cl => cl.StartingSegment.SegmentSpecification.SegmentId == segment.SegmentId).ToList();

                if (matchingLoopSpecs == null || matchingLoopSpecs.Count == 0)
                {
                    return segment;
                }
                else if (segment.SegmentId == "NM1" || segment.SegmentId == "N1")
                {
                    var entity = new Entity(_delimiters, segment.SegmentString);
                    entity.Specification = matchingLoopSpecs.Where(ls => ls.StartingSegment.EntityIdentifiers.Any(ei => ei.Code.ToString() == entity.EntityIdentifierCode || ei.Code.ToString() == "Item" + entity.EntityIdentifierCode)).FirstOrDefault();
                    return entity;
                }
                else
                {
                    var loop = new Loop(_delimiters, segment.SegmentString);
                    loop.Specification = matchingLoopSpecs.FirstOrDefault();
                    return loop;
                }
            }
            else
                return segment;
        }

        private bool ParseSegment(Transaction transaction, LoopContainer currentContainer, ref int index)
        {
            if (index < _segments.Count)
            {
                var segmentId = _segments[index].SegmentId;
                var segment = CreateSegment(transaction, currentContainer, _segments[index]);

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
                        currentContainer.Segments.Add((Segment)segment);
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
                    envelop.Segments.Add(new Segment(_delimiters, _segments[index++].SegmentString));

                var transaction = CreateTransaction(_segments[index]);
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
                                transaction = CreateTransaction(_segments[index]);
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
