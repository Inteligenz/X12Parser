namespace OopFactory.X12.Validation.Model
{
    using System.Collections.Generic;

    public class TransactionSetResponse
    {
        public TransactionSetResponse()
        {
            if (SegmentErrors == null)
                SegmentErrors = new List<SegmentError>();
            if (SyntaxErrorCodes == null)
                SyntaxErrorCodes = new List<string>();
        }

        public string TransactionSetIdentifierCode { get; set; }
        public string TransactionSetControlNumber { get; set; }
        public string ImplementationConventionReference { get; set; }

        public List<SegmentError> SegmentErrors { get; set; }

        public AcknowledgmentCodeEnum AcknowledgmentCode { get; set; }

        public List<string> SyntaxErrorCodes { get; set; }
    }
}
