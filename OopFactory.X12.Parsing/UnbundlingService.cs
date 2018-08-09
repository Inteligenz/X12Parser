namespace OopFactory.X12.Parsing
{
    using System.Collections.Generic;
    using System.Text;
    
    using OopFactory.X12.Shared.Models;

    internal class UnbundlingService
    {
        private readonly char segmentTerminator;

        public UnbundlingService(char segmentTerminator)
        {
            this.segmentTerminator = segmentTerminator;
        }
       
        public void UnbundleLoops(IList<string> list, LoopContainer container, string loopId)
        {
            foreach (Loop loop in container.Loops)
            {
                if (loop.Specification.LoopId == loopId)
                {
                    list.Add(this.ExtractLoop(loop, loopId));
                }

                this.UnbundleLoops(list, loop, loopId);
            }
        }

        public void UnbundleHLoops(List<string> list, HierarchicalLoopContainer container, string loopId)
        {
            this.UnbundleLoops(list, container, loopId);
            foreach (HierarchicalLoop hloop in container.HLoops)
            {
                if (hloop.Specification.LoopId == loopId)
                {
                    list.Add(this.ExtractLoop(hloop, loopId));
                }
                else
                {
                    this.UnbundleHLoops(list, hloop, loopId);
                }
            }
        }

        private string ExtractLoop(LoopContainer loop, string loopId)
        {
            var sb = new StringBuilder();

            LoopContainer parent = (LoopContainer)loop.Parent;
            sb.AppendLine(this.SerializeParent(parent, loopId));
            sb.AppendLine(loop.ToX12String(true));
            foreach (var segment in loop.Transaction.TrailerSegments)
            {
                sb.Append(segment.SegmentString);
                sb.Append($"{this.segmentTerminator}");
            
            }

            foreach (var segment in loop.Transaction.FunctionGroup.TrailerSegments)
            {
                sb.Append(segment.SegmentString);
                sb.Append($"{this.segmentTerminator}");
            
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
                {
                    thisLoopId = ((Loop)container).Specification.LoopId;
                }

                if (container is HierarchicalLoop)
                {
                    thisLoopId = ((HierarchicalLoop)container).Specification.LoopId;
                }

                var sb = new StringBuilder(this.SerializeParent(parent, thisLoopId));
                sb.Append(container.SegmentString);
                sb.Append($"{this.segmentTerminator}");
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
                        sb.Append($"{this.segmentTerminator}");
                    }
                }

                return sb.ToString();
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append(container.Transaction.FunctionGroup.SegmentString);
                sb.Append($"{this.segmentTerminator}");
                sb.Append(container.Transaction.SegmentString);
                sb.Append($"{this.segmentTerminator}");

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
                        sb.Append($"{this.segmentTerminator}");
                    }
                }

                return sb.ToString();
            }
        }
    }
}
