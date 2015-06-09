using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Validation
{
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
                return base.ToString();
            else
                return string.Format("LoopId={0}, Segments={1}, Loop={2}", Spec.LoopId, Segments.Count, Containers.Count);

        }
    }
}
