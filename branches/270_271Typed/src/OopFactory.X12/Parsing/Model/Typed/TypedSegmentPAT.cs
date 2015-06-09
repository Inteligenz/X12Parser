using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
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
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string PAT02_PatientLocationCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string PAT03_EmploymentStatusCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string PAT04_StudentStatusCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string PAT05_DateTimePeriodFormatQualifier
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public DateTime? PAT06_DateOfDeath
        {
            get 
            {
                string element = _segment.GetElement(6);
                if (element.Length == 8)
                    return DateTime.ParseExact(element, "yyyyMMdd", null);
                else
                    return null; 
            }
            set 
            { 
                _segment.SetElement(6, String.Format("{0:yyyyMMdd}", value)); 
            }
        }

        public string PAT07_UnitOrBasisForMeasurementCode
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public decimal? PAT08_PatientWeight
        {
            get 
            {
                decimal weight;
                if (decimal.TryParse(_segment.GetElement(8), out weight))
                    return weight;
                else
                    return null; 
            }
            set { _segment.SetElement(8, String.Format("{0}", value)); }
        }

        public bool? PAT09_PregnancyIndicator
        {
            get 
            {
                switch (_segment.GetElement(9))
                {
                    case "Y": return true;
                    case "N": return false;
                    default: return null;
                }
            }
            set 
            {
                if (value.HasValue)
                    _segment.SetElement(9, value.Value ? "Y" : "N");
                else
                    _segment.SetElement(9, "");
            }
        }
    }
}
