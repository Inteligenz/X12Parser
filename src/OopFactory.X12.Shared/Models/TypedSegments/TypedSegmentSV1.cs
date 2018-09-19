namespace OopFactory.X12.Shared.Models.TypedSegments
{
    public class TypedSegmentSV1 : TypedSegment
    {
        public TypedSegmentSV1() : base("SV1")
        {
        }

        public string SV101_CompositeMedicalProcedure
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string SV102_MonetaryAmount
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }
        
        public string SV103_UnitBasisMeasCode
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string SV104_Quantity
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }
        public string SV105_FacilityCode
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string SV107_CompDiagCodePoint
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string SV109_YesNoCondRespCode
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }

        public string SV111_YesNoCondRespCode
        {
            get { return this.Segment.GetElement(11); }
            set { this.Segment.SetElement(11, value); }
        }

        public string SV112_YesNoCondRespCode
        {
            get { return this.Segment.GetElement(12); }
            set { this.Segment.SetElement(12, value); }
        }        
        
        public string SV115_CopayStatusCode
        {
            get { return this.Segment.GetElement(15); }
            set { this.Segment.SetElement(15, value); }
        }
    }
}
