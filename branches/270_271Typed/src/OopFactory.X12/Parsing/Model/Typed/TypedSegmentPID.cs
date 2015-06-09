using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Product/Item Description
    /// </summary>
    public class TypedSegmentPID : TypedSegment
    {
        public TypedSegmentPID()
            : base("PID")
        {
        }

        /// <summary>
        /// F = Free form
        /// </summary>
        public string PID01_ItemDescriptionType
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string PID02_ProductProcessCharacteristicCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string PID03_AgencyQualifierCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string PID04_ProductDescriptionCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string PID05_Description
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string PID06_SurfaceLayerPositionCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string PID07_SourceSubqualifier
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public YesNoConditionOrResponseCode PID08_YesNoConditionOrResponseCode
        {
            get { return _segment.GetElement(8).ToEnumFromEDIFieldValue<YesNoConditionOrResponseCode>(); }
            set { _segment.SetElement(8, value.EDIFieldValue()); }
        }

        public string PID09_LanguageCode
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }
    }
}
