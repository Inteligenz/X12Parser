using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Attributes;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public enum RelationshipCode
    {
        [EDIFieldValue("A")]
        Add,
        [EDIFieldValue("D")]
        Delete,
        [EDIFieldValue("I")]
        Include,
        [EDIFieldValue("O")]
        InformationOnly,
        [EDIFieldValue("S")]
        Substituted
    }
}
