using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentN4 : TypedSegment
    {
        public TypedSegmentN4()
            : base("N4")
        {
        }

        public string N401_CityName
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string N402_StateOrProvinceCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string N403_PostalCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string N404_CountryCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string N405_LocationQualifier
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string N406_LocationIdentifier
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string N407_CountrySubdivisionCode
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }
    }
}
