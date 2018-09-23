namespace OopFactory.X12.Shared.Models.TypedLoops
{
    using OopFactory.X12.Shared.Models.TypedElements;
    using OopFactory.X12.Specifications;

    public class TypedLoopIK4 : TypedLoop
    {
        public TypedElementPositionInSegment IK401 { get; private set; }

        public TypedLoopIK4()
            : base("IK4")
        {
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters, LoopSpecification loopSpecification)
        {
            base.Initialize(parent, delimiters, loopSpecification);
            this.IK401 = new TypedElementPositionInSegment(this.Loop, 1);
        }

        public string IK402_DataElementReferenceNumber
        {
            get { return this.Loop.GetElement(2); }
            set { this.Loop.SetElement(2, value); }
        }

        public string IK403_SyntaxErrorCode
        {
            get { return this.Loop.GetElement(3); }
            set { this.Loop.SetElement(3, value); }
        }

        public string IK404_CopyOfBaDataElement
        {
            get { return this.Loop.GetElement(4); }
            set { this.Loop.SetElement(4, value); }
        }
    }
}
