namespace OopFactory.X12.Specifications.Interfaces
{
    using System.Collections.Generic;

    using OopFactory.X12.Specifications;

    /// <summary>
    /// Provides an interface for container objects
    /// </summary>
    public interface IContainerSpecification
    {
        /// <summary>
        /// Gets the loop ID of the container
        /// </summary>
        string LoopId { get; }

        /// <summary>
        /// Gets the collection of segment specifications
        /// </summary>
        List<SegmentSpecification> SegmentSpecifications { get; }

        /// <summary>
        /// Gets the collection of loop specifications
        /// </summary>
        List<LoopSpecification> LoopSpecifications { get; }        
    }
}
