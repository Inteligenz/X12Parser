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
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public string AK202_TransactionSetControlNumber
        {
            get { return _loop.GetElement(2); }
            set { _loop.SetElement(2, value); }
        }

        public string AK203_ImplementationConventionReference
        {
            get { return _loop.GetElement(3); }
            set { _loop.SetElement(3, value); }
        }
    }
}
