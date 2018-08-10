namespace OopFactory.X12.Validation
{
    using OopFactory.X12.Specifications;

    public class SegmentInformation
    {
        public string SegmentId { get; set; }

        public int SegmentPosition { get; set; }

        public string[] Elements { get; set; }

        public string LoopId { get; set; }

        public SegmentSpecification Spec { get; set; }

        public override string ToString()
        {
            return string.Format("Id={0}, Pos={1}, Loop={2}", SegmentId, SegmentPosition, LoopId);
        }
    }
}
