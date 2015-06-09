using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    /// <summary>
    /// Tax Information
    /// </summary>
    public class TypedSegmentTXI : TypedSegment
    {
        public TypedSegmentTXI()
            : base("TXI")
        {
        }

        public string TXI01_TaxTypeCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public decimal? TXI02_MonetaryAmount
        {
            get { return _segment.GetDecimalElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public decimal? TXI03_Percent
        {
            get { return _segment.GetDecimalElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string TXI04_TaxJurisdictionCodeQualifier
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string TXI05_TaxJurisdictionCode
        {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string TXI06_TaxExemptCode
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public RelationshipCode TXI07_RelationshipCode
        {
            get { return _segment.GetElement(7).ToEnumFromEDIFieldValue<RelationshipCode>(); }
            set { _segment.SetElement(7, value.EDIFieldValue()); }
        }

        public decimal? TXI08_DollarBasisForPercent
        {
            get { return _segment.GetDecimalElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string TXI09_TaxIdentificationNumber
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }

        public string TXI10_AssignedIdentification
        {
            get { return _segment.GetElement(10); }
            set { _segment.SetElement(10, value); }
        }
    }
}
