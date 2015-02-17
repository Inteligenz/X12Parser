using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentPRV : TypedSegment
    {
        public TypedSegmentPRV()
            : base("PRV")
        {
        }

        public string PRV01_ProviderCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string PRV02_ReferenceIdQualifier
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string PRV03_ProviderTaxonomyCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string PRV04_StateOrProvinceCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string PRV05_ProviderSpecialtyInformation
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string PRV06_ProviderOrganizationCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }
    }
}
