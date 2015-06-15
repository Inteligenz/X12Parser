using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopAK2 : TypedLoop
    {
        public TypedLoopAK2() : base("AK2") { }

        public string AK201_TransactionSetIdentifierCode
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public string AK202_TransactionSetControlNumber
        {
            get { return Loop.GetElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public string AK203_ImplementationConventionReference
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }
    }
}
