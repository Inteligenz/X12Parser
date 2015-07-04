using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopIK4 : TypedLoop
    {
        public TypedLoopIK4() : base("IK4") { }
        public TypedLoopIK4(Loop loop) : base(loop) { }
        public TypedElementPositionInSegment IK401
        {
            get { return new TypedElementPositionInSegment(Loop, 1); }
            set { Loop.SetElement(1, value); }
        }

        public string IK402_DataElementReferenceNumber
        {
            get { return Loop.GetElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public string IK403_SyntaxErrorCode
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public string IK404_CopyOfBaDataElement
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }
    }
}
