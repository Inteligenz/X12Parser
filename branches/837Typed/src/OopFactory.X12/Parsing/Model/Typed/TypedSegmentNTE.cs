using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Note/Secial Instruction
    /// </summary>
    public class TypedSegmentNTE : TypedSegment
    {
        public TypedSegmentNTE()
            : base("NTE")
        {
        }

        /// <summary>
        /// GEN = Entire Transaction Set
        /// </summary>
        public string NTE01_NoteReferenceCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string NTE02_Description
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }
    }
}
