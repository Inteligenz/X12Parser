namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using OopFactory.X12.Shared.Enumerations;
    using OopFactory.X12.Shared.Extensions;

    /// <summary>
    /// Service, Promotion, Allowance, or Charge Information
    /// </summary>
    public class TypedSegmentSAC : TypedSegment
    {
        public TypedSegmentSAC()
            : base("SAC")
        {
        }

        public AllowanceOrChargeIndicator SAC01_AllowanceOrChargeIndicator
        {
            get { return this.Segment.GetElement(1).ToEnumFromEdiFieldValue<AllowanceOrChargeIndicator>(); }
            set { this.Segment.SetElement(1, value.EdiFieldValue()); }
        }

        public string SAC02_ServicePromotionAllowanceOrChargeCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string SAC03_AgencyQualifierCode
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string SAC04_AgencyServicePromotionAllowanceOrChageCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        /// <summary>
        /// This is an implied decimal with 2 decimal points,
        /// multiply your decimal by 100 to assign here
        /// </summary>
        public int? SAC05_AmountN2
        {
            get
            {
                int element;
                if (int.TryParse(this.Segment.GetElement(5), out element))
                {
                    return element;
                }
                
                return null;
            }

            set
            {
                this.Segment.SetElement(5, value);
            }
        }

        /// <summary>
        /// 3 = Discount/Gross
        /// Z = Mutually Defined
        /// </summary>
        public string SAC06_AllowanceChargePercentQualifier
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public decimal? SAC07_Percent
        {
            get { return this.Segment.GetDecimalElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string SAC13_ReferenceIdentification
        {
            get { return this.Segment.GetElement(13); }
            set { this.Segment.SetElement(13, value); }
        }

        public string SAC14_OptionNumber
        {
            get { return this.Segment.GetElement(14); }
            set { this.Segment.SetElement(14, value); }
        }

        public string SAC15_Description
        {
            get { return this.Segment.GetElement(15); }
            set { this.Segment.SetElement(15, value); }
        }

        public string SAC16_LanguageCode
        {
            get { return this.Segment.GetElement(16); }
            set { this.Segment.SetElement(16, value); }
        }
    }
}
