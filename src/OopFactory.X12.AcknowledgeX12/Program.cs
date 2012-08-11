using System;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.IO;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Validation;
using OopFactory.X12.Validation.Model;

namespace OopFactory.X12.AcknowledgeX12
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilename = args[0];
            string outputFilename = args[1];
            string isaControlNumber = args.Length > 2 ? args[2] : "999";
            string gsControlNumber = args.Length > 3 ? args[3] : "99";

            var service = new X12AcknowledgmentService();

            using (FileStream fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                // Create aknowledgements and identify errors
                var responses = service.AcknowledgeTransactions(fs);

                // Change any acknowledgment codes here
                // CUSTOM BUSINESS LOGIC HERE

                // Transform to outbound interchange for serialization
                var interchange = new Interchange(DateTime.Now, int.Parse(isaControlNumber), true);
                interchange.AuthorInfoQualifier = ConfigurationManager.AppSettings["AuthorInfoQualifier"];
                interchange.AuthorInfo = ConfigurationManager.AppSettings["AuthorInfo"];
                interchange.SecurityInfoQualifier = ConfigurationManager.AppSettings["SecurityInfoQualifier"];
                interchange.SecurityInfo = ConfigurationManager.AppSettings["SecurityInfo"];
                interchange.InterchangeSenderIdQualifier = ConfigurationManager.AppSettings["InterchangeSenderIdQualifier"];
                interchange.InterchangeSenderId = ConfigurationManager.AppSettings["InterchangeSenderId"];
                interchange.InterchangeReceiverIdQualifier = responses.First().SenderIdQualifier;
                interchange.InterchangeReceiverId = responses.First().SenderId;
                interchange.SetElement(12, "00501");
                
               
                var group = interchange.AddFunctionGroup("FA", DateTime.Now, int.Parse(gsControlNumber));
                group.ApplicationSendersCode = ConfigurationManager.AppSettings["InterchangeSenderId"];
                group.VersionIdentifierCode = "005010X231A1";

                group.Add999Transaction(responses, new ControlNumberSequencer());

                File.WriteAllText(outputFilename, interchange.SerializeToX12(true));
            }
        }
    }
}
