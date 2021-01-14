﻿namespace X12.AcknowledgeX12
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Text;

    using X12.Parsing;
    using X12.Shared.Models;
    using X12.Specifications.Finders;
    using X12.Validation;
    using X12.Validation.Model;

    /// <summary>
    /// Primary class for the AcknowledgeX12 application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Primary entry point for the AcknowldegeX12 utility
        /// </summary>
        /// <param name="args">Additional command arguments for option overloading</param>
        public static void Main(string[] args)
        {
            string inputFilename = args[0];
            string outputFilename = args[1];
            string isaControlNumber = args.Length > 2 ? args[2] : "999";
            string gsControlNumber = args.Length > 3 ? args[3] : "99";

            var service = new X12AcknowledgmentService();

            using (var fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new X12StreamReader(fs, Encoding.UTF8))
                {
                    var firstTrans = reader.ReadNextTransaction();
                    if (reader.LastTransactionCode == "837")
                    {
                        if (reader.TransactionContainsSegment(firstTrans.Transactions[0], "SV2"))
                        {
                            service = new InstitutionalClaimAcknowledgmentService();
                        }

                        if (reader.TransactionContainsSegment(firstTrans.Transactions[0], "SV1"))
                        {
                            service = new X12AcknowledgmentService(new ProfessionalClaimSpecificationFinder());
                        }
                    }
                }
            }

            using (var fs = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                // Create aknowledgements and identify errors
                IList<FunctionalGroupResponse> responses = service.AcknowledgeTransactions(fs);

                // Change any acknowledgment codes here to reject transactions with errors
                // CUSTOM BUSINESS LOGIC HERE

                // Transform to outbound interchange for serialization
                var interchange = new Interchange(DateTime.Now, int.Parse(isaControlNumber), true)
                {
                    AuthorInfoQualifier = ConfigurationManager.AppSettings["AuthorInfoQualifier"],
                    AuthorInfo = ConfigurationManager.AppSettings["AuthorInfo"],
                    SecurityInfoQualifier = ConfigurationManager.AppSettings["SecurityInfoQualifier"],
                    SecurityInfo = ConfigurationManager.AppSettings["SecurityInfo"],
                    InterchangeSenderIdQualifier = ConfigurationManager.AppSettings["InterchangeSenderIdQualifier"],
                    InterchangeSenderId = ConfigurationManager.AppSettings["InterchangeSenderId"],
                    InterchangeReceiverIdQualifier = responses.First().SenderIdQualifier,
                    InterchangeReceiverId = responses.First().SenderId
                };
                interchange.SetElement(12, "00501");
                
                FunctionGroup group = interchange.AddFunctionGroup("FA", DateTime.Now, int.Parse(gsControlNumber));
                group.ApplicationSendersCode = ConfigurationManager.AppSettings["InterchangeSenderId"];
                group.VersionIdentifierCode = "005010X231A1";

                group.Add999Transaction(responses);

                // This is a demonstration example only, change true to false to create continous x12 without line feeds.
                File.WriteAllText(outputFilename, interchange.SerializeToX12(true));
            }
        }
    }
}
