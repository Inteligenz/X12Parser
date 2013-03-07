using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedLoopPID : TypedLoop
    {
        public TypedLoopPID()
            : base("PID")
        {
        }
        
        /// <summary>
        /// F = Free form
        /// </summary>
        public string PID01_ItemDescriptionType
        {
            get { return _loop.GetElement(1); }
            set { _loop.SetElement(1, value); }
        }

        public string PID02_ProductProcessCharacteristicCode
        {
            get { return _loop.GetElement(2); }
            set { _loop.SetElement(2, value); }
        }

        public string PID03_AgencyQualifierCode
        {
            get { return _loop.GetElement(3); }
            set { _loop.SetElement(3, value); }
        }

        public string PID04_ProductDescriptionCode
        {
            get { return _loop.GetElement(4); }
            set { _loop.SetElement(4, value); }
        }

        public string PID05_Description
        {
            get { return _loop.GetElement(5); }
            set { _loop.SetElement(5, value); }
        }

        public string PID06_SurfaceLayerPositionCode
        {
            get { return _loop.GetElement(6); }
            set { _loop.SetElement(6, value); }
        }

        public string PID07_SourceSubqualifier
        {
            get { return _loop.GetElement(7); }
            set { _loop.SetElement(7, value); }
        }

        public YesNoConditionOrResponseCode PID08_YesNoConditionOrResponseCode
        {
            get { return _loop.GetElement(8).ToEnumFromEDIFieldValue<YesNoConditionOrResponseCode>(); }
            set { _loop.SetElement(8, value.EDIFieldValue()); }
        }

        public string PID09_LanguageCode
        {
            get { return _loop.GetElement(9); }
            set { _loop.SetElement(9, value); }
        }
    }
}
