using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Attributes;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public enum EntityTypeQualifier
    {
        [EDIFieldValue("")]
        Undefined = 0,
        [EDIFieldValue("1")]
        Person = 1,
        [EDIFieldValue("2")]
        NonPersonEntity = 2,
        [EDIFieldValue("3")]
        Unknown,
        [EDIFieldValue("4")]
        Corporation,
        [EDIFieldValue("5")]
        Trust,
        [EDIFieldValue("6")]
        Organization,
        [EDIFieldValue("7")]
        LimitedLiabilityCorporation,
        [EDIFieldValue("8")]
        Partnership,
        [EDIFieldValue("9")]
        SCorporation,
        [EDIFieldValue("C")]
        Custodial,
        [EDIFieldValue("D")]
        NonProfitOrganization,
        [EDIFieldValue("E")]
        SoleProprietorship,
        [EDIFieldValue("G")]
        Government,
        [EDIFieldValue("L")]
        LimitedPartnership
    }
}
