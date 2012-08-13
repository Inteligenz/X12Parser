using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedElementContextIdentification
    {
        private int _elementNumber;
        private Segment _segment;
        private string _name;
        private string _reference;

        internal TypedElementContextIdentification(Segment segment, int elementNumber)
        {
            _segment = segment;
            _elementNumber = elementNumber;
        }

        private void UpdateElement()
        {
            string value = string.Format("{1}{0}{2}",
                _segment._delimiters.SubElementSeparator,
                _name, _reference);
            value = value.TrimEnd(_segment._delimiters.SubElementSeparator);
            _segment.SetElement(_elementNumber, value);
        }

        public string _1_ContextName
        {
            get { return _name; }
            set { _name = value; UpdateElement(); }
        }

        public string _2_ContextReference
        {
            get { return _reference; }
            set { _reference = value; UpdateElement(); }
        }
    }
}
