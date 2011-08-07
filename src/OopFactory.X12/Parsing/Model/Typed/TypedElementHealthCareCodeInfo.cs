using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementHealthCareCodeInfo
    {
        private int _elementNumber;
        private Segment _segment;

        internal TypedElementHealthCareCodeInfo(Segment segment, int elementNumber)
        {
            _segment = segment;
            _elementNumber = elementNumber;
        }

        private void UpdateElement()
        {
        }
        /*
        public string _1_CodeListQualifierCode
        {
        }

        public string _2_IndustryCode
        {
        }
         * */
    }
}
