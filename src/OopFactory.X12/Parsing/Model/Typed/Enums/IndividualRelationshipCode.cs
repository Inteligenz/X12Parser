using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum IndividualRelationshipCode
    {
        [EDIFieldValue("01")]
        Spouse,
        [EDIFieldValue("19")]
        Child,
        [EDIFieldValue("20")]
        Employee,
        [EDIFieldValue("21")]
        Unknown,
        [EDIFieldValue("34")]
        OtherAdult,
        [EDIFieldValue("39")]
        OrganDonor,
        [EDIFieldValue("40")]
        CadaverDonor,
        [EDIFieldValue("53")]
        LifePartner,
        [EDIFieldValue("G8")]
        OtherRelationship
    }
}
