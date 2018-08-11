namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using OopFactory.X12.Shared.Enumerations;
    using OopFactory.X12.Shared.Extensions;

    /// <summary>
    /// Transaction Totals
    /// </summary>
    public class TypedSegmentCTT : TypedSegment
    {
        public TypedSegmentCTT()
            : base("CTT")
        {
        }

        public int? CTT01_NumberOfLineItems
        {
            get { return this.Segment.GetIntElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public decimal? CTT02_HashTotal
        {
            get { return this.Segment.GetDecimalElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public decimal? CTT03_Weight
        {
            get { return this.Segment.GetDecimalElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public UnitOrBasisOfMeasurementCode CTT04_UnitOrBasisForMeasurementCode
        {
            get { return this.Segment.GetElement(4).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { this.Segment.SetElement(4, value.EDIFieldValue()); }
        }

        public decimal? CTT05_Volume
        {
            get { return this.Segment.GetDecimalElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public UnitOrBasisOfMeasurementCode CTT06_UnitOrBasisForMeasurementCode
        {
            get { return this.Segment.GetElement(6).ToEnumFromEDIFieldValue<UnitOrBasisOfMeasurementCode>(); }
            set { this.Segment.SetElement(6, value.EDIFieldValue()); }
        }

        public string CTT07_Description
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }
    }
}
