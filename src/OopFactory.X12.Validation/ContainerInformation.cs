namespace OopFactory.X12.Validation
{
    using System.Collections.Generic;

    using OopFactory.X12.Specifications.Interfaces;

    public class ContainerInformation
    {
        public ContainerInformation()
        {
            Segments = new List<SegmentInformation>();
            Containers = new List<ContainerInformation>();
        }

        public IContainerSpecification Spec { get; set; }

        public string HLoopNumber { get; set; }

        public List<SegmentInformation> Segments { get; private set; }

        public List<ContainerInformation> Containers { get; private set; }

        public override string ToString()
        {
            if (Spec == null)
            {
                return base.ToString();
            }
            else
            {
                return $"LoopId={Spec.LoopId}, Segments={Segments.Count}, Loop={Containers.Count}";
            }
        }
    }
}
