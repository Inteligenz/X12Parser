namespace OopFactory.X12.Shared.Models.TypedLoops
{
    public class TypedLoopIK3 : TypedLoop
    {
        public TypedLoopIK3() : base("IK3") { }

        public string IK301_SegmentIdCode
        {
            get { return this.Loop.GetElement(1); }
            set { this.Loop.SetElement(1, value); }
        }

        public int? IK302_SegmentPositionInTransactionSet
        {
            get
            {
                int position;
                if (int.TryParse(this.Loop.GetElement(2), out position))
                    return position;
                else
                    return null;
            }
            set
            {
                if (value.HasValue)
                    this.Loop.SetElement(2, value.ToString());
                else
                    this.Loop.SetElement(2, "");
            }
        }

        public string IK303_LoopIdentifierCode
        {
            get { return this.Loop.GetElement(3); }
            set { this.Loop.SetElement(3, value); }
        }

        public string IK304_SyntaxErrorCode
        {
            get { return this.Loop.GetElement(4); }
            set { this.Loop.SetElement(4, value); }
        }
    }
}
