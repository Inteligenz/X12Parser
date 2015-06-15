using OopFactory.X12.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed.Enums
{
    public enum FollowupActionCode
    {
        [EDIFieldValue("C")]
        PleaseCorrectAndResubmit,
        [EDIFieldValue("N")]
        ResubmissionNotAllowed,
        [EDIFieldValue("P")]
        PleaseResubmitOriginalTransaction,
        [EDIFieldValue("R")]
        ResubmissionAllowed,
        [EDIFieldValue("S")]
        DoNotResubmit_InquiryInitiatedToAThirdParty,
        [EDIFieldValue("Y")]
        DoNotResubmit_WeWillHoldYourRequestAndRespondAgainShortly,
    }
}
