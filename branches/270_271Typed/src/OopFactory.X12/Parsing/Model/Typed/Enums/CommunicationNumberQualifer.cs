using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum CommunicationNumberQualifer
    {
        [EDIFieldValue("U")]
        Undefined,
        [EDIFieldValue("EM")]
        ElectronicMail,
        [EDIFieldValue("EX")]
        TelephoneExtension,
        [EDIFieldValue("FX")]
        Facsimile,
        [EDIFieldValue("TE")]
        Telephone
    }
}
