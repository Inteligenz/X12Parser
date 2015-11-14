namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopLX : TypedLoop
    {
        public TypedLoopLX() 
            : base("LX")
        {
        }

        public TypedLoopLX(Loop loop) : base(loop) { }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, Specification.LoopSpecification loopSpecification)
        {
            string segmentString = GetSegmentString(delimiters);

            Loop = new Loop(parent, delimiters, segmentString, loopSpecification);
        }

        public int? LX01_AssignedNumber
        {
            get { return Loop.GetIntElement(1); }
            set { Loop.SetElement(1, value); }
        }
    }
}
