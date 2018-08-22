namespace OopFactory.X12.Validation
{
    using System.Collections.Generic;

    using OopFactory.X12.Specifications.Interfaces;

    /// <summary>
    /// Represents information about a container
    /// </summary>
    public class ContainerInformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerInformation"/> class
        /// </summary>
        public ContainerInformation()
        {
            this.Segments = new List<SegmentInformation>();
            this.Containers = new List<ContainerInformation>();
        }

        /// <summary>
        /// Gets or sets the specification for the container
        /// </summary>
        public IContainerSpecification Spec { get; set; }

        /// <summary>
        /// Gets or sets the hierarchical loop number
        /// </summary>
        public string HLoopNumber { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="SegmentInformation"/>
        /// </summary>
        public List<SegmentInformation> Segments { get; }

        /// <summary>
        /// Gets the collection of <see cref="ContainerInformation"/>
        /// </summary>
        public List<ContainerInformation> Containers { get; }

        /// <summary>
        /// Returns a string representation of the <see cref="ContainerInformation"/>
        /// </summary>
        /// <returns>String representation of the <see cref="ContainerInformation"/></returns>
        public override string ToString()
        {
            return this.Spec == null
                ? base.ToString()
                : $"LoopId={this.Spec.LoopId}, Segments={this.Segments.Count}, Loop={this.Containers.Count}";
        }
    }
}
