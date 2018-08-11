namespace OopFactory.X12.Shared.Models.TypedSegments
{
    using System;

    public class TypedSegmentPAT : TypedSegment
    {
        public TypedSegmentPAT()
            : base("PAT")
        {
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            //PAT05_DateTimePeriodFormatQualifier = "D8";
        }

        public string PAT01_IndividualRelationshipCode
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public string PAT02_PatientLocationCode
        {
            get { return this.Segment.GetElement(2); }
            set { this.Segment.SetElement(2, value); }
        }

        public string PAT03_EmploymentStatusCode
        {
            get { return this.Segment.GetElement(3); }
            set { this.Segment.SetElement(3, value); }
        }

        public string PAT04_StudentStatusCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string PAT05_DateTimePeriodFormatQualifier
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public DateTime? PAT06_DateOfDeath
        {
            get 
            {
                string element = this.Segment.GetElement(6);
                if (element.Length == 8)
                    return DateTime.ParseExact(element, "yyyyMMdd", null);
                else
                    return null; 
            }
            set 
            { 
                this.Segment.SetElement(6, String.Format("{0:yyyyMMdd}", value)); 
            }
        }

        public string PAT07_UnitOrBasisForMeasurementCode
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public decimal? PAT08_PatientWeight
        {
            get 
            {
                decimal weight;
                if (decimal.TryParse(this.Segment.GetElement(8), out weight))
                    return weight;
                else
                    return null; 
            }
            set { this.Segment.SetElement(8, String.Format("{0}", value)); }
        }

        public bool? PAT09_PregnancyIndicator
        {
            get 
            {
                switch (this.Segment.GetElement(9))
                {
                    case "Y": return true;
                    case "N": return false;
                    default: return null;
                }
            }
            set 
            {
                if (value.HasValue)
                    this.Segment.SetElement(9, value.Value ? "Y" : "N");
                else
                    this.Segment.SetElement(9, "");
            }
        }
    }
}
