namespace OopFactory.X12.Shared.Models.Typed
{
    using System;

    using OopFactory.X12.Shared.Enumerations;
    
    public class TypedSegmentDMG : TypedSegment
    {
        public TypedSegmentDMG()
            : base("DMG")
        {
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.DMG01_DateTimePeriodFormatQualifier = "D8";
        }

        public string DMG01_DateTimePeriodFormatQualifier
        {
            get { return this.Segment.GetElement(1); }
            set { this.Segment.SetElement(1, value); }
        }

        public DateTime? DMG02_DateOfBirth
        {
            get 
            { 
                string element = this.Segment.GetElement(2);
                if (element.Length == 8)
                {
                    return DateTime.ParseExact(element, "yyyyMMdd", null);
                }

                return null;
            }

            set 
            { 
                this.Segment.SetElement(2, $"{value:yyyyMMdd}"); 
            }
        }

        public Gender DMG03_Gender
        {
            get 
            { 
                switch (this.Segment.GetElement(3))
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
                        this.Segment.SetElement(3, "F");
                        break;
                    case Gender.Male:
                        this.Segment.SetElement(3, "M");
                        break;
                    case Gender.Unknown:
                        this.Segment.SetElement(3, "U");
                        break;
                    default:
                        this.Segment.SetElement(3, string.Empty);
                        break;
                }
            }
        }

        public string DMG04_MaritalStatusCode
        {
            get { return this.Segment.GetElement(4); }
            set { this.Segment.SetElement(4, value); }
        }

        public string DMG05_CompositeRaceOrEthnicityInformation
        {
            get { return this.Segment.GetElement(5); }
            set { this.Segment.SetElement(5, value); }
        }

        public string DMG06_CitizenStatusCode
        {
            get { return this.Segment.GetElement(6); }
            set { this.Segment.SetElement(6, value); }
        }

        public string DMG07_CountryCode
        {
            get { return this.Segment.GetElement(7); }
            set { this.Segment.SetElement(7, value); }
        }

        public string DMG08_BasisOfVerificationCode
        {
            get { return this.Segment.GetElement(8); }
            set { this.Segment.SetElement(8, value); }
        }

        public string DMG09_Quantity
        {
            get { return this.Segment.GetElement(9); }
            set { this.Segment.SetElement(9, value); }
        }

        public string DMG10_CodeListQualifierCode
        {
            get { return this.Segment.GetElement(10); }
            set { this.Segment.SetElement(10, value); }
        }

        public string DMG11_IndustryCode
        {
            get { return this.Segment.GetElement(11); }
            set { this.Segment.SetElement(11, value); }
        }
    }
}
