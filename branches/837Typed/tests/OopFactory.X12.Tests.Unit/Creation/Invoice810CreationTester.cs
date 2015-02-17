using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Model.Typed;

namespace OopFactory.X12.Tests.Unit.Creation
{
    [TestClass]
    public class Invoice810CreationTester
    {
        private string expectedX12 = @"ISA*00*          *00*          *30*943274043      *16*0069088189999  *990104*1532*U*00401*000000035*1*T*>~
  GS*IN*943274043TO*0069088189999*19990104*1532*1*X*004010~
    ST*810*0001~
      BIG*19981014*3662*****N6~
      N1*BT*The Scheduling Coordinator, Inc~
        N3*53241 Hamilton Dr~
        N4*Palo Alto*CA*95622*US~
      N1*RE*Bank of America- (Mkt and GMC)~
        N3*1850 Gateway Boulevard~
        N4*Concord*CA*94520*US~
        REF*11*1233626208~
        REF*01*026009593~
      ITD*03*****19981020~
      IT1*1*1*EA*2896035.3~
        PID*X****RMR Scheduling Coordinator - Estimated RMR~
      TDS*289603530~
      CTT*1~
    SE*16*0001~
  GE*1*1~
IEA*1*000000035~";

        [TestMethod]
        public void Create810_4010Version()
        {
            var message = new Interchange(Convert.ToDateTime("1/4/99 15:32"), 35, false, '~','*','>')
            {
                SecurityInfoQualifier = "00",
                InterchangeSenderIdQualifier = "30",
                InterchangeSenderId = "943274043",
                InterchangeReceiverIdQualifier = "16",
                InterchangeReceiverId = "0069088189999"
            };

            var fg = message.AddFunctionGroup("IN", Convert.ToDateTime("1/4/1999 15:32"), 1);
            fg.ApplicationSendersCode = "943274043TO";
            fg.ApplicationReceiversCode = "0069088189999";
            fg.ResponsibleAgencyCode = "X";
            fg.VersionIdentifierCode = "004010";

            var trans = fg.AddTransaction("810", "0001");

            var big = trans.AddSegment(new TypedSegmentBIG());
            big.BIG01_InvoiceDate = Convert.ToDateTime("10/14/1998");
            big.BIG02_InvoiceNumber = "3662";
            big.BIG07_TransactionTypeCode = "N6";

            var billTo = trans.AddLoop(new TypedLoopN1());
            billTo.N101_EntityIdentifierCodeEnum = EntityIdentifierCode.BillToParty;
            billTo.N102_Name = "The Scheduling Coordinator, Inc";
            
            var billToAddress = billTo.AddSegment(new TypedSegmentN3());
            billToAddress.N301_AddressInformation = "53241 Hamilton Dr";
            
            var billToLocale = billTo.AddSegment(new TypedSegmentN4());
            billToLocale.N401_CityName = "Palo Alto";
            billToLocale.N402_StateOrProvinceCode = "CA";
            billToLocale.N403_PostalCode = "95622";
            billToLocale.N404_CountryCode = "US";

            var remitTo = trans.AddLoop(new TypedLoopN1());
            remitTo.N101_EntityIdentifierCodeEnum = EntityIdentifierCode.PartyToReceiveCommercialInvoiceRemittance;
            remitTo.N102_Name = "Bank of America- (Mkt and GMC)";

            var remitToAddress = remitTo.AddSegment(new TypedSegmentN3());
            remitToAddress.N301_AddressInformation = "1850 Gateway Boulevard";

            var remitToLocale = remitTo.AddSegment(new TypedSegmentN4());
            remitToLocale.N401_CityName = "Concord";
            remitToLocale.N402_StateOrProvinceCode = "CA";
            remitToLocale.N403_PostalCode = "94520";
            remitToLocale.N404_CountryCode = "US";

            var remitToRef1 = remitTo.AddSegment(new TypedSegmentREF());
            remitToRef1.REF01_ReferenceIdQualifier = "11";
            remitToRef1.REF02_ReferenceId = "1233626208";

            var remitToRef2 = remitTo.AddSegment(new TypedSegmentREF());
            remitToRef2.REF01_ReferenceIdQualifier = "01";
            remitToRef2.REF02_ReferenceId = "026009593";

            var itd = trans.AddSegment(new TypedSegmentITD());
            itd.ITD01_TermsTypeCode = "03";
            itd.ITD06_TermsNetDueDate = Convert.ToDateTime("10/20/1998");

            var it1 = trans.AddLoop(new TypedLoopIT1());
            it1.IT101_AssignedIdentification = "1";
            it1.IT102_QuantityInvoiced = 1;
            it1.IT103_UnitOrBasisForMeasurementCode = UnitOrBasisOfMeasurementCode.Each;
            it1.IT104_UnitPrice = 2896035.3m;

            var pid = it1.AddLoop(new TypedLoopPID());
            pid.PID01_ItemDescriptionType = "X";
            pid.PID05_Description = "RMR Scheduling Coordinator - Estimated RMR";

            var tds = trans.AddSegment(new TypedSegmentTDS());
            tds.TDS01_AmountN2 = 289603530;

            var ctt = trans.AddSegment(new TypedSegmentCTT());
            ctt.CTT01_NumberOfLineItems = 1;

            var x12 = message.SerializeToX12(true);

            Trace.Write(x12);

            Assert.AreEqual(expectedX12, x12);
        }
    }
}
