namespace OopFactory.X12.Shared.Models.Typed
{
    using OopFactory.X12.Shared.Extensions;

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
            get { return this.Loop.GetElement(1); }
            set { this.Loop.SetElement(1, value); }
        }

        public string PID02_ProductProcessCharacteristicCode
        {
            get { return this.Loop.GetElement(2); }
            set { this.Loop.SetElement(2, value); }
        }

        public string PID03_AgencyQualifierCode
        {
            get { return this.Loop.GetElement(3); }
            set { this.Loop.SetElement(3, value); }
        }

        public string PID04_ProductDescriptionCode
        {
            get { return this.Loop.GetElement(4); }
            set { this.Loop.SetElement(4, value); }
        }

        public string PID05_Description
        {
            get { return this.Loop.GetElement(5); }
            set { this.Loop.SetElement(5, value); }
        }

        public string PID06_SurfaceLayerPositionCode
        {
            get { return this.Loop.GetElement(6); }
            set { this.Loop.SetElement(6, value); }
        }

        public string PID07_SourceSubqualifier
        {
            get { return this.Loop.GetElement(7); }
            set { this.Loop.SetElement(7, value); }
        }

        public YesNoConditionOrResponseCode PID08_YesNoConditionOrResponseCode
        {
            get { return this.Loop.GetElement(8).ToEnumFromEDIFieldValue<YesNoConditionOrResponseCode>(); }
            set { this.Loop.SetElement(8, value.EDIFieldValue()); }
        }

        public string PID09_LanguageCode
        {
            get { return this.Loop.GetElement(9); }
            set { this.Loop.SetElement(9, value); }
        }
    }
}
