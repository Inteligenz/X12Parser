using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Specification
{
    public interface IContainerSpecification
    {
        string LoopId { get; }
        List<SegmentSpecification> SegmentSpecifications { get; }
        List<LoopSpecification> LoopSpecifications { get; }        
    }
}
