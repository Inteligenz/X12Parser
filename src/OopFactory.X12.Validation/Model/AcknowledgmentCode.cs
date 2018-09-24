namespace OopFactory.X12.Validation.Model
{
    public enum AcknowledgmentCode
    {
        A_Accepted,
        E_Accepted_ButErrorsWereNoted,
        M_Rejected_MACFailed,
        P_PartiallyAccepted,
        R_Rejected,
        W_Rejected_AssuranceFailedValidityTests,
        X_Rejected_ContentCouldNotBeAnalyzed
    }
}
