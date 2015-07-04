using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopLIN : TypedLoop
    {
        public TypedLoopLIN() : base("LIN") { }
        public TypedLoopLIN(Loop loop) : base(loop) { }

        public string LIN01_AssignedIdentification
        {
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public string LIN02_ProductOrServiceIdQualifier
        {
            get { return Loop.GetElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public string LIN03_ProductOrServiceId
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }
    }
}
