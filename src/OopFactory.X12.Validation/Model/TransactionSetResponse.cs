namespace OopFactory.X12.Validation.Model
{
    using System.Collections.Generic;

    public class TransactionSetResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionSetResponse"/> class
        /// </summary>
        public TransactionSetResponse()
        {
            if (this.SegmentErrors == null)
            {
                this.SegmentErrors = new List<SegmentError>();
            }

            if (this.SyntaxErrorCodes == null)
            {
                this.SyntaxErrorCodes = new List<string>();
            }
        }

        public string TransactionSetIdentifierCode { get; set; }

        public string TransactionSetControlNumber { get; set; }

        public string ImplementationConventionReference { get; set; }

        public List<SegmentError> SegmentErrors { get; set; }

        public AcknowledgmentCode AcknowledgmentCode { get; set; }

        public List<string> SyntaxErrorCodes { get; set; }
    }
}
