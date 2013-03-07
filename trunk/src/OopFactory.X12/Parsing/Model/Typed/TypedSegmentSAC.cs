using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
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
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<AllowanceOrChargeIndicator>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public string SAC02_ServicePromotionAllowanceOrChargeCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string SAC03_AgencyQualifierCode
        {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string SAC04_AgencyServicePromotionAllowanceOrChageCode
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
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
                if (int.TryParse(_segment.GetElement(5), out element))
                    return element;
                else
                    return null;
            }
            set { _segment.SetElement(5, string.Format("{0}", value)); }
        }

        /// <summary>
        /// 3 = Discount/Gross
        /// Z = Mutually Defined
        /// </summary>
        public string SAC06_AllowanceChargePercentQualifier
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public decimal? SAC07_Percent
        {
            get { return _segment.GetDecimalElement(7); }
            set { _segment.SetElement(7, value); }
        }

        public string SAC13_ReferenceIdentification
        {
            get { return _segment.GetElement(13); }
            set { _segment.SetElement(13, value); }
        }

        public string SAC14_OptionNumber
        {
            get { return _segment.GetElement(14); }
            set { _segment.SetElement(14, value); }
        }

        public string SAC15_Description
        {
            get { return _segment.GetElement(15); }
            set { _segment.SetElement(15, value); }
        }

        public string SAC16_LanguageCode
        {
            get { return _segment.GetElement(16); }
            set { _segment.SetElement(16, value); }
        }
    }
}
