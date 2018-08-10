namespace OopFactory.X12.Validation.Model
{
    using System.Collections.Generic;

    public class FunctionalGroupResponse
    {
        public FunctionalGroupResponse()
        {
            if (TransactionSetResponses == null)
                TransactionSetResponses = new List<TransactionSetResponse>();
            if (SyntaxErrorCodes == null)
                SyntaxErrorCodes = new List<string>();
        }

        public string SenderIdQualifier { get; set; }
        public string SenderId { get; set; }

        public string FunctionalIdCode { get; set; }
        public string GroupControlNumber { get; set; }
        public string VersionIdentifierCode { get; set; }

        public List<TransactionSetResponse> TransactionSetResponses { get; set; }

        public AcknowledgmentCodeEnum AcknowledgmentCode { get; set; }

        public List<string> SyntaxErrorCodes { get; set; }
    }
}
