using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;
using OopFactory.X12.Parsing.Model.Typed.Enums;

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
            get { return Loop.GetElement(1); }
            set { Loop.SetElement(1, value); }
        }

        public string PID02_ProductProcessCharacteristicCode
        {
            get { return Loop.GetElement(2); }
            set { Loop.SetElement(2, value); }
        }

        public string PID03_AgencyQualifierCode
        {
            get { return Loop.GetElement(3); }
            set { Loop.SetElement(3, value); }
        }

        public string PID04_ProductDescriptionCode
        {
            get { return Loop.GetElement(4); }
            set { Loop.SetElement(4, value); }
        }

        public string PID05_Description
        {
            get { return Loop.GetElement(5); }
            set { Loop.SetElement(5, value); }
        }

        public string PID06_SurfaceLayerPositionCode
        {
            get { return Loop.GetElement(6); }
            set { Loop.SetElement(6, value); }
        }

        public string PID07_SourceSubqualifier
        {
            get { return Loop.GetElement(7); }
            set { Loop.SetElement(7, value); }
        }

        public YesNoConditionOrResponseCode PID08_YesNoConditionOrResponseCode
        {
            get { return Loop.GetElement(8).ToEnumFromEDIFieldValue<YesNoConditionOrResponseCode>(); }
            set { Loop.SetElement(8, value.EDIFieldValue()); }
        }

        public string PID09_LanguageCode
        {
            get { return Loop.GetElement(9); }
            set { Loop.SetElement(9, value); }
        }
    }
}
