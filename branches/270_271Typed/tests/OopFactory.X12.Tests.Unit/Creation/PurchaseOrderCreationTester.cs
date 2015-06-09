using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Tests.Unit.Creation
{
	[TestClass]
	public class PurchaseOrderCreationTester
	{
		[TestMethod]
		public void CreatePurchaseOrder850()
		{
            string expected = @"ISA*00*          *00*          *01*828513080      *01*001903202U     *100817*0850*U*00401*000000245*0*P*:~
  GS*PO*828513080*001903202U*20100817*0850*245*X*005010X222~
    ST*850*0001~
      BEG*05*SA*S41000439**20100810~
      CUR*BY*USD~
      PER*IC*Doe,Jane******Doe,Jane~
    SE*5*0001~
  GE*1*1~
IEA*1*000000245~";

			DateTime purcaseOrderDate = new DateTime(2010, 8, 17, 08, 50, 0);
			Interchange interchange = new Interchange(purcaseOrderDate, 245, true)
			{
				InterchangeSenderIdQualifier = "01",
				InterchangeSenderId = "828513080",
				InterchangeReceiverIdQualifier = "01",
				InterchangeReceiverId = "001903202U",
				InterchangeDate = purcaseOrderDate,
			};

			interchange.SetElement(14, "0"); //No Aknowlegement is 0

			FunctionGroup group = interchange.AddFunctionGroup("PO", purcaseOrderDate, 1, "005010X222");
			group.ApplicationSendersCode = "828513080";
			group.ApplicationReceiversCode = "001903202U";
			group.Date = purcaseOrderDate;
			group.ControlNumber = 245;

			Transaction transaction = group.AddTransaction("850", "0001");

			Segment bhtSegment = transaction.AddSegment("BEG");
			bhtSegment.SetElement(1, "05");
			bhtSegment.SetElement(2, "SA");
			bhtSegment.SetElement(3, "S41000439");
			bhtSegment.SetElement(5, "20100810");

			bhtSegment = transaction.AddSegment("CUR");
			bhtSegment.SetElement(1, "BY");
			bhtSegment.SetElement(2, "USD");

			bhtSegment = transaction.AddSegment("PER");
			bhtSegment.SetElement(1, "IC");
			bhtSegment.SetElement(2, "Doe,Jane");
			bhtSegment.SetElement(8, "Doe,Jane");

			var x12 = interchange.SerializeToX12(true);
            Assert.AreEqual(expected, x12);
		}

		[TestMethod]
		public void TestEmptyElement()
		{
			string expectedSegmentString = "BEG*05*SA*S41000439";
			DateTime purcaseOrderDate = new DateTime(2010, 8, 17, 08, 50, 0);
			Interchange interchange = new Interchange(purcaseOrderDate, 245, true)
			{
				InterchangeSenderIdQualifier = "01",
				InterchangeSenderId = "828513080",
				InterchangeReceiverIdQualifier = "01",
				InterchangeReceiverId = "001903202U",
				InterchangeDate = purcaseOrderDate,
			};

			interchange.SetElement(14, "0"); //No Aknowlegement is 0

			FunctionGroup group = interchange.AddFunctionGroup("PO", purcaseOrderDate, 1, "005010X222");
			group.ApplicationSendersCode = "828513080";
			group.ApplicationReceiversCode = "001903202U";
			group.Date = purcaseOrderDate;
			group.ControlNumber = 245;

			Transaction transaction = group.AddTransaction("850", "0001");

			string segmentString = string.Empty;

			Segment bhtSegment = transaction.AddSegment("BEG");
			segmentString = bhtSegment.SegmentString;  //test getting it prematurely
			bhtSegment.SetElement(1, "05");
			bhtSegment.SetElement(2, "SA");
			bhtSegment.SetElement(3, "S41000439");
			bhtSegment.SetElement(5, "");

			segmentString = bhtSegment.SegmentString;

			Assert.AreEqual(expectedSegmentString, segmentString);

		}


		[TestMethod]
		public void CreatePurchaseOrderChangeNotice860()
		{
            string expected = @"ISA*00*          *00*          *01*828513080      *01*001903202U     *100818*0850*U*00401*000000245*0*P*:~
  GS*PO*828513080*001903202U*20100818*0850*245*X*005010X222~
    ST*860*0001~
      BCH*01*SA****20100817*****20100818~
      REF*IA*1to30chars~
      DTM*010*20100819~
    SE*5*0001~
  GE*1*1~
IEA*1*000000245~";

			DateTime purcaseOrderDate = new DateTime(2010, 8, 17, 08, 50, 0);
            DateTime changeOrderDate = purcaseOrderDate.AddDays(1d);
            DateTime requestedShipDate = purcaseOrderDate.AddDays(2d);

			Interchange interchange = new Interchange(changeOrderDate , 245, true)
			{
				InterchangeSenderIdQualifier = "01",
				InterchangeSenderId = "828513080",
				InterchangeReceiverIdQualifier = "01",
				InterchangeReceiverId = "001903202U",
				InterchangeDate = changeOrderDate,
			};
			interchange.SetElement(14, "0"); //No Aknowlegement is 0

			FunctionGroup group = interchange.AddFunctionGroup("PO", changeOrderDate, 1, "005010X222");
			group.ApplicationSendersCode = "828513080";
			group.ApplicationReceiversCode = "001903202U";
			group.Date = changeOrderDate;
			group.ControlNumber = 245;

			Transaction transaction = group.AddTransaction("860", "0001");
			
			//BCH - Beginning Segment for Purchase Order Change
			//Mandatory / Max Use=1 time
			Segment bhtSegment = transaction.AddSegment("BCH");
			bhtSegment.SetElement((int)BeginningSegmentPurchaseOrderChangeIndex.TransactionSetPurpose , "01");  //01 is cancelllation  04 is change
			bhtSegment.SetElement((int)BeginningSegmentPurchaseOrderChangeIndex.TransactionSetTypeCode, "SA");
			bhtSegment.SetElement((int)BeginningSegmentPurchaseOrderChangeIndex.PurchaseOrderDate, purcaseOrderDate.ToString("yyyyMMdd"));
			bhtSegment.SetElement((int)BeginningSegmentPurchaseOrderChangeIndex.ChangeRequestDate, changeOrderDate.ToString("yyyyMMdd"));

			//REF- Reference Identification
			//Optional / Max Use=1 time
			bhtSegment = transaction.AddSegment("REF");
			bhtSegment.SetElement((int)ReferenceIdentificationIndex.ReferenceIdentificationQualifier , "IA");
			bhtSegment.SetElement((int)ReferenceIdentificationIndex.ReferenceNumber, "1to30chars");

			//DTM – Date/Time Reference		                   
			//Optional/ Max Use=1 time
			bhtSegment = transaction.AddSegment("DTM");
			bhtSegment.SetElement((int)DateTimeReferenceIndex.TermsTypeCode, "010");
			bhtSegment.SetElement((int)DateTimeReferenceIndex.RequestedShipDate, requestedShipDate.ToString("yyyyMMdd"));

			//Add to test
			//N1 – Name
			//Optional / Max Use=1

			//N3 – Address Information
			//Conditional – use if N1 used/ Max Use=2  

			//N4 – Geographic Location
			//Conditional – use if N3 used / Max Use=1 time 

			//POC – Line Item Change
			//Mandatory / Max Use=1 times per loop / Max Loop=99 times

			//DTM – Date/Time Reference
			//Mandatory / Max Use=1 times per loop 

			//CTT – Transaction Totals
			//Mandatory / Max Use=1 time

			var x12 = interchange.SerializeToX12(true);

            Assert.AreEqual(expected, x12);

		}

		enum BeginningSegmentPurchaseOrderChangeIndex
		{
			TransactionSetPurpose = 1,
 			TransactionSetTypeCode = 2,
			PurchaseOrderNumber = 3,
			PurchaseOrderDate = 6,
			ChangeRequestDate = 11
 
		}

		enum ReferenceIdentificationIndex
		{
			ReferenceIdentificationQualifier = 1,
			ReferenceNumber = 2
		}

		enum DateTimeReferenceIndex
		{
 			TermsTypeCode = 1,
			RequestedShipDate = 2
		}

	}
}