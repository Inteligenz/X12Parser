using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public enum Gender
    {
        Undefined,
        Female,
        Male,
        Unknown
    }
    
    public class TypedSegmentDMG : TypedSegment
    {
        public TypedSegmentDMG() : base("DMG") { }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DMG01_DateTimePeriodFormatQualifier = "D8"; // default value
        }

        public string DMG01_DateTimePeriodFormatQualifier
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public DateTime? DMG02_DateOfBirth
        {
            get 
            { 
                string element = _segment.GetElement(2);
                if (element.Length == 8)
                    return DateTime.ParseExact(element, "yyyyMMdd", null);
                else
                    return null;
            }
            set 
            { 
                _segment.SetElement(2, String.Format("{0:yyyyMMdd}",value)); 
            }
        }

        public Gender DMG03_Gender
        {
            get 
            { 
                switch (_segment.GetElement(3))
                {
                    case "F": return Gender.Female;
                    case "M": return Gender.Male;
                    case "U": return Gender.Unknown;
                    default: return Gender.Undefined;
                }
            }
            set 
            {
                switch (value)
                {
                    case Gender.Female:
                        _segment.SetElement(3, "F"); break;
                    case Gender.Male:
                        _segment.SetElement(3, "M"); break;
                    case Gender.Unknown:
                        _segment.SetElement(3, "U"); break;
                    default:
                        _segment.SetElement(3, ""); break;
                }
            }
        }

        public string DMG04_MaritalStatusCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string DMG05_CompositeRaceOrEthnicityInformation
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string DMG06_CitizenStatusCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string DMG07_CountryCode
        {
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string DMG08_BasisOfVerificationCode
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string DMG09_Quantity
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string DMG10_CodeListQualifierCode
        {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }

        public string DMG11_IndustryCode
        {
            get { return _segment.GetElement(11); }
            set { _segment.SetElement(11, value); }
        }
    }
}
