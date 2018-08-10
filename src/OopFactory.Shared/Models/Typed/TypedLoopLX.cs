namespace OopFactory.X12.Shared.Models.Typed
{
    using OopFactory.X12.Specifications;

    public class TypedLoopLX : TypedLoop
    {
        private readonly string entityIdentifer;

        public TypedLoopLX(string entityIdentifier) 
            : base("LX")
        {
            this.entityIdentifer = entityIdentifier;
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, LoopSpecification loopSpecification)
        {
            string segmentString = GetSegmentString(delimiters);

            this.Loop = new Loop(parent, delimiters, segmentString, loopSpecification);
        }

        public string LX01_AssignedNumber
        {
            get { return this.Loop.GetElement(1); }
            set { this.Loop.SetElement(1, value); }
        }
    }
}
