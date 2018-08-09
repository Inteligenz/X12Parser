namespace OopFactory.X12.Specifications.Interfaces
{
    using System.Collections.Generic;

    using OopFactory.X12.Specifications;

    public interface IContainerSpecification
    {
        string LoopId { get; }
        List<SegmentSpecification> SegmentSpecifications { get; }
        List<LoopSpecification> LoopSpecifications { get; }        
    }
}
