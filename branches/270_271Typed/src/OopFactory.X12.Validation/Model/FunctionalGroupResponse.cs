using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Validation.Model
{
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
