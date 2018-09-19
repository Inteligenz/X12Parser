namespace OopFactory.X12.Shared.Models.TypedLoops
{
    public class TypedLoopAK2 : TypedLoop
    {
        public TypedLoopAK2() : base("AK2")
        {
        }

        public string AK201_TransactionSetIdentifierCode
        {
            get { return this.Loop.GetElement(1); }
            set { this.Loop.SetElement(1, value); }
        }

        public string AK202_TransactionSetControlNumber
        {
            get { return this.Loop.GetElement(2); }
            set { this.Loop.SetElement(2, value); }
        }

        public string AK203_ImplementationConventionReference
        {
            get { return this.Loop.GetElement(3); }
            set { this.Loop.SetElement(3, value); }
        }
    }
}
