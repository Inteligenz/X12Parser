namespace X12.Parsing
{
    using System.Collections.Generic;
    using System.Text;
    
    using X12.Shared.Models;

    /// <summary>
    /// Provides methods for unbundling loops from different loop container types
    /// </summary>
    internal class UnbundlingService
    {
        private readonly char segmentTerminator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnbundlingService"/> class
        /// </summary>
        /// <param name="segmentTerminator">Termination character for a segment</param>
        public UnbundlingService(char segmentTerminator)
        {
            this.segmentTerminator = segmentTerminator;
        }
       
        /// <summary>
        /// Unbundles each loop from a <see cref="LoopContainer"/>
        /// </summary>
        /// <param name="list">Collection of loops being unbundled</param>
        /// <param name="container">Container with loops to be unbundled</param>
        /// <param name="loopId">Loop identifier</param>
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

        /// <summary>
        /// Unbundles each loop from a <see cref="HierarchicalLoopContainer"/>
        /// </summary>
        /// <param name="list">Collection of loops being unbundled</param>
        /// <param name="container">Container with loops to be unbundled</param>
        /// <param name="loopId">Loop identifier</param>
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
                sb.Append(this.segmentTerminator);
            }

            foreach (var segment in loop.Transaction.FunctionGroup.TrailerSegments)
            {
                sb.Append(segment.SegmentString);
                sb.Append(this.segmentTerminator);
            }

            return sb.ToString();
        }

        private string SerializeParent(LoopContainer container, string excludedLoopId)
        {
            if (!(container is Transaction))
            {
                LoopContainer parent = (LoopContainer)container.Parent;
                string thisLoopId = excludedLoopId;
                if (container is Loop containerLoop)
                {
                    thisLoopId = containerLoop.Specification.LoopId;
                }

                if (container is HierarchicalLoop hierarchicalLoop)
                {
                    thisLoopId = hierarchicalLoop.Specification.LoopId;
                }

                var sb = new StringBuilder(this.SerializeParent(parent, thisLoopId));
                sb.Append(container.SegmentString);
                sb.Append(this.segmentTerminator);
                foreach (var segment in container.Segments)
                {
                    if (segment is Loop loop)
                    {
                        if (loop.Specification.LoopId != excludedLoopId)
                        {
                            sb.AppendLine(segment.SerializeToX12(true));
                        }
                    }
                    else
                    {
                        sb.Append(segment.SegmentString);
                        sb.Append(this.segmentTerminator);
                    }
                }

                return sb.ToString();
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append(container.Transaction.FunctionGroup.SegmentString);
                sb.Append(this.segmentTerminator);
                sb.Append(container.Transaction.SegmentString);
                sb.Append(this.segmentTerminator);

                foreach (var segment in container.Transaction.Segments)
                {
                    if (segment is Loop loop)
                    {
                        if (loop.Specification.LoopId != excludedLoopId)
                        {
                            sb.AppendLine(segment.SerializeToX12(true));
                        }
                    }
                    else
                    {
                        sb.Append(segment.SegmentString);
                        sb.Append(this.segmentTerminator);
                    }
                }

                return sb.ToString();
            }
        }
    }
}
