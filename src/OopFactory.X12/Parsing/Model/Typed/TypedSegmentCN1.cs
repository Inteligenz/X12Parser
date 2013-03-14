using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentCN1 : TypedSegment
    {
        public TypedSegmentCN1()
            : base("CN1")
        {
        }

        public string CN101_ContractTypeCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public ContractTypeCode CN101_ContractTypeCodeEnum
        {
            get { return _segment.GetElement(1).ToEnumFromEDIFieldValue<ContractTypeCode>(); }
            set { _segment.SetElement(1, value.EDIFieldValue()); }
        }

        public decimal? CN102_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? CN103_Percent
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string CN104_ReferenceIdentification
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public decimal? CN105_TermsDiscountPercent
        {
            get { return _segment.GetDecimalElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string CN106_VersionIdentifier
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }
    }
}
