using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing
{
    internal class UnbundlingService
    {
        private char _segmentTerminator;

        public UnbundlingService(char segmentTerminator)
        {
            _segmentTerminator = segmentTerminator;
        }
       
        public void UnbundleLoops(IList<string> list, LoopContainer container, string loopId)
        {
            foreach (Loop loop in container.Loops)
            {
                if (loop.Specification.LoopId == loopId)
                    list.Add(ExtractLoop(loop, loopId));
                UnbundleLoops(list, loop, loopId);
            }
        }

        public void UnbundleHLoops(List<string> list, HierarchicalLoopContainer container, string loopId)
        {
            UnbundleLoops(list, container, loopId);
            foreach (HierarchicalLoop hloop in container.HLoops)
            {
                if (hloop.Specification.LoopId == loopId)
                    list.Add(ExtractLoop(hloop, loopId));
                else
                    UnbundleHLoops(list, hloop, loopId);
            }
        }

        private string ExtractLoop(LoopContainer loop, string loopId)
        {
            StringBuilder sb = new StringBuilder();

            LoopContainer parent = (LoopContainer)loop.Parent;
            sb.AppendLine(SerializeParent(parent, loopId));
            sb.AppendLine(loop.ToX12String(true));
            foreach (var segment in loop.Transaction.TrailerSegments)
            {
                sb.Append(segment.SegmentString);
                sb.AppendFormat("{0}", _segmentTerminator);
            
            }
            foreach (var segment in loop.Transaction.FunctionGroup.TrailerSegments)
            {
                sb.Append(segment.SegmentString);
                sb.AppendFormat("{0}", _segmentTerminator);
            
            }
            return sb.ToString();
        }

        private string SerializeParent(LoopContainer container, string excludedLoopId)
        {
            if (!(container is Transaction))
            {
                LoopContainer parent = (LoopContainer)container.Parent;
                string thisLoopId = excludedLoopId;
                if (container is Loop)
                    thisLoopId = ((Loop)container).Specification.LoopId;
                else if (container is HierarchicalLoop)
                    thisLoopId = ((HierarchicalLoop)container).Specification.LoopId;
                StringBuilder sb = new StringBuilder(SerializeParent(parent, thisLoopId));
                sb.Append(container.SegmentString);
                sb.AppendFormat("{0}", _segmentTerminator);
                foreach (var segment in container.Segments)
                {
                    if (segment is Loop)
                    {
                        if (((Loop)segment).Specification.LoopId != excludedLoopId)
                        {
                            sb.AppendLine(segment.SerializeToX12(true));
                        }
                    }
                    else
                    {
                        sb.Append(segment.SegmentString);
                        sb.AppendFormat("{0}", _segmentTerminator);
                    }

                }
                return sb.ToString();
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(container.Transaction.FunctionGroup.SegmentString);
                sb.AppendFormat("{0}", _segmentTerminator);
                sb.Append(container.Transaction.SegmentString);
                sb.AppendFormat("{0}", _segmentTerminator);

                foreach (var segment in container.Transaction.Segments)
                {
                    if (segment is Loop)
                    {
                        if (((Loop)segment).Specification.LoopId != excludedLoopId)
                        {
                            sb.AppendLine(segment.SerializeToX12(true));
                        }
                    }
                    else
                    {
                        sb.Append(segment.SegmentString);
                        sb.AppendFormat("{0}", _segmentTerminator);
                    }
                }
                return sb.ToString();
            }
        }

    }
}
