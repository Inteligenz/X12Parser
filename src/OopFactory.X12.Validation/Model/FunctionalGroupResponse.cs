namespace OopFactory.X12.Validation.Model
{
    using System.Collections.Generic;

    public class FunctionalGroupResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionalGroupResponse"/> class
        /// </summary>
        public FunctionalGroupResponse()
        {
            if (this.TransactionSetResponses == null)
            {
                this.TransactionSetResponses = new List<TransactionSetResponse>();
            }

            if (this.SyntaxErrorCodes == null)
            {
                this.SyntaxErrorCodes = new List<string>();
            }
        }

        public string SenderIdQualifier { get; set; }

        public string SenderId { get; set; }

        public string FunctionalIdCode { get; set; }

        public string GroupControlNumber { get; set; }

        public string VersionIdentifierCode { get; set; }

        public List<TransactionSetResponse> TransactionSetResponses { get; set; }

        public AcknowledgmentCode AcknowledgmentCode { get; set; }

        public List<string> SyntaxErrorCodes { get; set; }
    }
}
