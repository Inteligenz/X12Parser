using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementContextIdentification : BaseElementReference
    {
        private string _name;
        private string _reference;

        public TypedElementContextIdentification(Segment segment, int elementNumber)
            : base(segment, elementNumber)
        {
        }
        
        public override string ToString()
        {
            string value = string.Format("{1}{0}{2}",
                 Segment._delimiters.SubElementSeparator,
                 _name, _reference);
            value = value.TrimEnd(Segment._delimiters.SubElementSeparator);
            return value;
        }
        public string _1_ContextName
        {
            get { return _name; }
            set { _name = value;  }
        }

        public string _2_ContextReference
        {
            get { return _reference; }
            set { _reference = value;  }
        }
    }
}
