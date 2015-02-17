using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopLIN : TypedLoop
    {
        public TypedLoopLIN() : base("LIN") { }

        public string LIN01_AssignedIdentification
        {
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public string LIN02_ProductOrServiceIdQualifier
        {
            get { return _loop.GetElement(2); }
            set { _loop.SetElement(2, value); }
        }

        public string LIN03_ProductOrServiceId
        {
            get { return _loop.GetElement(3); }
            set { _loop.SetElement(3, value); }
        }
    }
}
