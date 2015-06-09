using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Model.Typed;
using OopFactory.X12.Transformations;

namespace OopFactory.X12.Tests.Unit.DocumentationCodeSamples.X12InterchangeModel
{
    [TestClass]
    public class ReadingAnExistingX12File
    {
        private string inquiry = @"ISA*00*          *00*          *01*9012345720000  *01*9088877320000  *020816*1144*U*00401*000000031*0*T*:~GS*HS*901234572000*908887732000*20070816*1615*31*X*00501X092A1~ST*270*1234~BHT*0022*13*10001234*20070816*1319*00~HL*1**20*1~NM1*PR*2*ABC BILLING SERVICE*****PI*842610001~HL*2*1*21*1~NM1*1P*2*BONE AND JOINT CLINIC*****SV*2000035~HL*3*2*22*0~TRN*1*93175-012547*9877281234~NM1*IL*1*SMITH*ROBERT*MI****11122333301~DMG*D8*19430519~DTP*291*D8*20060501~EQ*30~SE*13*1234~GE*1*31~IEA*1*000000031~";
        
        private string inquiryOutline = @"ISA*00*          *00*          *01*9012345720000  *01*9088877320000  *020816*1144*U*00401*000000031*0*T*:~
  GS*HS*901234572000*908887732000*20070816*1615*31*X*00501X092A1~
    ST*270*1234~
      BHT*0022*13*10001234*20070816*1319*00~
      HL*1**20*1~
        NM1*PR*2*ABC BILLING SERVICE*****PI*842610001~
        HL*2*1*21*1~
          NM1*1P*2*BONE AND JOINT CLINIC*****SV*2000035~
          HL*3*2*22*0~
            TRN*1*93175-012547*9877281234~
            NM1*IL*1*SMITH*ROBERT*MI****11122333301~
              DMG*D8*19430519~
              DTP*291*D8*20060501~
              EQ*30~
    SE*13*1234~
  GE*1*31~
IEA*1*000000031~
";
        [TestMethod]        
        public void OutlineIsSameAsOriginal()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(new MemoryStream(Encoding.ASCII.GetBytes(inquiry))).First();

            Interchange interchangeFromOutline = parser.ParseMultiple(new MemoryStream(Encoding.ASCII.GetBytes(inquiryOutline))).First();
            Debug.WriteLine(interchange.Serialize());
            Assert.AreEqual(interchange.SerializeToX12(false), interchangeFromOutline.SerializeToX12(false));
        }


        [TestMethod]        
        public void Read270Test()
        {
            X12Parser parser = new X12Parser();
            Interchange interchange = parser.ParseMultiple(new MemoryStream(Encoding.ASCII.GetBytes(inquiry))).First();

            Assert.AreEqual("9088877320000  ", interchange.InterchangeReceiverId);

            Transaction transaction = interchange.FunctionGroups.First().Transactions.First();
            Segment bht = transaction.Segments.First();

            Assert.AreEqual("10001234", bht.GetElement(3));

            HierarchicalLoop subscriberLoop = transaction.FindHLoop("3");

            Loop subscriberNameLoop = subscriberLoop.Loops.First();

            Assert.AreEqual("SMITH", subscriberNameLoop.GetElement(3), "Subscriber last name not expected.");
            Assert.AreEqual("11122333301", subscriberNameLoop.GetElement(9), "Subscriber member id not expected.");

        }

        [TestMethod]        
        public void Create270Test()
        {
            /*Good documentation sources for understanding X12
             * http://docs.oracle.com/cd/E19398-01/820-1275/agdaw/index.html
             * https://www.empireblue.com/provider/noapplication/f4/s3/t2/pw_ad086848.pdf?refer=ehpprovider
            */

            //Create the top level interchange
            #region Create Interchange
            var Message = new Interchange(DateTime.Now, 31, false)
            {
                AuthorInfoQualifier = "00",                                     //ISA01 - Authorization Info Qual - 00(No Auth present)
                AuthorInfo = String.Format("{0,-10}", " "),                     //ISA02 - Authorization Info Must be 10 spaces if ISA01 = 00
                SecurityInfoQualifier = "00",                                   //ISA03 - Security Info Qual (00 no password)
                SecurityInfo = String.Format("{0,-10}", " "),                   //ISA04 - Password, 10 spaces if no password                                  
                InterchangeSenderIdQualifier = "01",                            //ISA05 01=Duns 14=Duns plus suffix 20=Health insurance number (HIN) 27=CMS carrier ID number 28=CMS fiscal intermediary ID number 29=CMS Medicare provider /supplier ID 30 U.S. federal tax ID 33=NAIC ID ZZ=Mutually defined. 
                InterchangeSenderId = "9012345720000  ",                        //ISA06 Registration information for sender
                InterchangeReceiverIdQualifier = "01",                          //ISA07 Mutually defined
                InterchangeReceiverId = "9088877320000  ",                      //ISA08 - Depends on who is processing it. Variable by company. We need to get a table of these values 
                InterchangeDate = DateTime.Parse("2002/08/16")                      //ISA09 - Date we sent this                                  
            };

            Message.SetElement(10, "1144");                                     //ISA10 Interchange time Format HHMM
            Message.SetElement(11, "U");                                        //ISA11 Interchange Control standards ID
            Message.SetElement(12, "00401");                                    //ISA12 Interchange control version number - set by receiver
            Message.SetElement(13, "000000031");                                //ISA13 Interchange control number - Must be unique within 180 days
            Message.SetElement(14, "0");                                        //ISA14 Interchange Ack requested - For 270 must be 0
            Message.SetElement(15, "T");                                        //ISA15 Usage (T=Test, P=Production)
            //Message.SetElement(16, "^");                                        //ISA16 Component Element sep - Can't be in any value in document

            Debug.WriteLine("Interchange level:");
            Debug.WriteLine("ISA*00*          *00*          *01*9012345720000  *01*9088877320000  *020816*1144*U*00401*000000031*0*T*:~IEA*0*000000031~");
            Debug.WriteLine(Message.SerializeToX12(false));
            Assert.AreEqual("ISA*00*          *00*          *01*9012345720000  *01*9088877320000  *020816*1144*U*00401*000000031*0*T*:~IEA*0*000000031~", Message.SerializeToX12(false));
            #endregion

            #region Create GS - Function Group
            //GS - First record of a functional group
            var Group = Message.AddFunctionGroup("HS", DateTime.Now, 31, "00501X092A1");
            Group.FunctionalIdentifierCode = "HS";                              //GS01 FA=999, HS=270, HB=271
            Group.ApplicationSendersCode = "901234572000";                      //GS02 - RegistationCode given by receiver organization
            Group.ApplicationReceiversCode = "908887732000";                    //GS03 - Recievers code (Get from the receiver organization)
            Group.Date = DateTime.Parse("2007/08/16");                          //GS04 - Date sent from us

            Group.SetElement(5, "1615");                                        //GS05 - HHMM time from sending system
            Group.ControlNumber = 31;                                           //GS06 Must equal following GE02 and be unique to each functional group in message
            Group.ResponsibleAgencyCode = "X";                                  //GS07 X=ASCX12
            Group.VersionIdentifierCode = "00501X092A1";                        //GS08 00501X092A1 for 270/271, 005010 for 999

            Debug.WriteLine("Function Group level:");
            Debug.WriteLine("GS*HS*901234572000*908887732000*20070816*1615*31*X*00501X092A1~GE*0*31~");
            Debug.WriteLine(Group.SerializeToX12(false));
            Assert.AreEqual("GS*HS*901234572000*908887732000*20070816*1615*31*X*00501X092A1~GE*0*31~", Group.SerializeToX12(false));
            
            #endregion

            #region Create Transaction
            var TransactionSet = Group.AddTransaction("270", "1234");              //1234 is the ST02 control number  
            
            #endregion

            #region Create BHT - Beginning of Hierarchal Transaction
            // Beginning of Hierarchical Transaction
            var BHTSegment = TransactionSet.AddSegment(new TypedSegmentBHT());
            BHTSegment.BHT01_HierarchicalStructureCode = "0022";                //BHT01 - Order for structure: 22 - Information Source, Information Reciever, Subscriber, Dependent 
            BHTSegment.BHT02_TransactionSetPurposeCode = "13";                  //BHT02 - Transaction Set purpose 01- Cancel (Cancel a previous 270), 13 - Request
            BHTSegment.BHT03_ReferenceIdentification = "10001234";              //BHT03 - Value used to track this request (From our side) Max 50 char
            BHTSegment.BHT04_Date = DateTime.Parse("2007/08/16");               //BHT04 - CCYYMMDD (CC = First two digits of year 20..) - When transaction was created
            BHTSegment.BHT05_Time = "1319";                                     //BHT05 - Time Transaction created HHMM
            BHTSegment.BHT06_TransactionTypeCode = "00";                        //BHT06 - Transaction Type - RT for getting Medicaid SpendDown info
          
            Debug.WriteLine("BHT - Beginning of Hierarchal Transaction:");
            Debug.WriteLine("ST*270*1234~BHT*0022*13*10001234*20070816*1319*00~SE*3*1234~");
            Debug.WriteLine(TransactionSet.SerializeToX12(false));            
            Assert.AreEqual("ST*270*1234~BHT*0022*13*10001234*20070816*1319*00~SE*3*1234~", TransactionSet.SerializeToX12(false));
            
            #endregion
            
            /* Note on the following HL levels
             * HL1 - Information Source level - Payer that maintains the info regarding the patient's coverage
             * HL2 - Information Receiver Level - The Entity requesting info regarding the patient's coverage
             * HL3 - Subscriber level - the subscriber, who may or may not be the patient - the member.
             * HL4 - Dependent level - The dependent of the member, who may or may not be the patient, is related to the subscriber/mnember
             */


            #region HL1 Level - Information Source - Loop 2100A
            var HL1InformationSourceLevel = TransactionSet.AddHLoop("1", "20", true); //"1" is HL01 the Id number for this loop,  "20" is HL02 - "20" means it is an info source

            //Billing info HL1 level
            var HL1SourceLoop = HL1InformationSourceLevel.AddLoop(new TypedLoopNM1("PR"));
            //InformationSourceLoop.NM101_EntityIdCode = "PR"; //2B Third-Party Administrator, 36 Employer, GP Gateway Provider, P5 Plan Sponsor, PR Payer
            HL1SourceLoop.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            HL1SourceLoop.NM103_NameLastOrOrganizationName = "ABC BILLING SERVICE";
            HL1SourceLoop.NM104_NameFirst = "";
            HL1SourceLoop.NM105_NameMiddle = "";
            HL1SourceLoop.NM106_NamePrefix = "";
            HL1SourceLoop.NM107_NameSuffix = "";
            HL1SourceLoop.NM108_IdCodeQualifier = "PI"; //code describing what the NM109 is
            /*
             * 24 Employer's Identification Number
                 46 Electronic Transmitter Identification Number (ETIN)
                 FI Federal Taxpayer's Identification Number
                 NI National Association of Insurance Commissioners (NAIC) Identification
                 PI Payor Identification
                 XV Centers for Medicare and Medicaid Services PlanID
                 CODE SOURCE: CODE SOURCE:
                     540: Centers for Medicare and Medicaid Services PlanID Centers for Medicare and Medicaid Services PlanID
                         XX Centers for Medicare and Medicaid Services National Provider Identifier
                 CODE SOURCE: CODE SOURCE:
                         537: Centers for Medicare and Medicaid Services National Provider Identifier Centers for Medicare and Medicaid Services National Provider Identifier
             */
            HL1SourceLoop.NM109_IdCode = "842610001"; //Code defining a party or other value lookup from external code list
            
            Debug.WriteLine("HL1 level:");
            Debug.WriteLine(HL1InformationSourceLevel.SerializeToX12(false));
            Debug.WriteLine("HL*1**20*1~NM1*PR*2*ABC BILLING SERVICE*****PI*842610001~");
            Assert.AreEqual("HL*1**20*1~NM1*PR*2*ABC BILLING SERVICE*****PI*842610001~",HL1InformationSourceLevel.SerializeToX12(false));

            
            #endregion
                 
            #region HL2 Level - Information Receiver - Loop 2100B
            var HL2SourceLoop = HL1InformationSourceLevel.AddHLoop("2", "21", true);//This is the 2100 Loop - The receiver information
            
            var HL2Info = HL2SourceLoop.AddLoop(new TypedLoopNM1("1P"));
            HL2Info.NM102_EntityTypeQualifier = EntityTypeQualifier.NonPersonEntity;
            HL2Info.NM103_NameLastOrOrganizationName = "BONE AND JOINT CLINIC";
            HL2Info.NM104_NameFirst = "";
            HL2Info.NM105_NameMiddle = "";
            HL2Info.NM106_NamePrefix = "";
            HL2Info.NM107_NameSuffix = "";
            HL2Info.NM108_IdCodeQualifier = "SV"; //code describing what the NM109 is
            //HL2Info.NM108_IdCodeQualifierEnum = IdentificationCodeQualifier.CanadianPetroleumAssociation;
            HL2Info.NM109_IdCode = "2000035";

            Debug.WriteLine("HL2 Level:");
            Debug.WriteLine(HL2SourceLoop.SerializeToX12(false));
            Debug.WriteLine("HL*2*1*21*1~NM1*1P*2*BONE AND JOINT CLINIC*****SV*2000035~");
            Assert.AreEqual("HL*2*1*21*1~NM1*1P*2*BONE AND JOINT CLINIC*****SV*2000035~", HL2SourceLoop.SerializeToX12(false));
            #endregion
                     
            #region HL3 Level - Member info level
            var HL3Info = HL2SourceLoop.AddHLoop("3", "22", false);
            //We need to create a TypedLoopTRN
            HL3Info.AddSegment("TRN*1*93175-012547*9877281234");

            var Member = HL3Info.AddLoop(new TypedLoopNM1("IL"));
            Member.NM102_EntityTypeQualifier = EntityTypeQualifier.Person;
            Member.NM103_NameLastOrOrganizationName = "SMITH";
            Member.NM104_NameFirst = "ROBERT";
            Member.NM105_NameMiddle="MI";
            Member.NM109_IdCode= "11122333301";

            TypedSegmentDMG Birthday = Member.AddSegment(new TypedSegmentDMG());
            Birthday.DMG02_DateOfBirth = DateTime.Parse("05/19/1943");

            TypedSegmentDTP SubscribeDate = Member.AddSegment(new TypedSegmentDTP());
            SubscribeDate.DTP01_DateTimeQualifier = DTPQualifier.Plan;
            SubscribeDate.DTP02_DateTimePeriodFormatQualifier = DTPFormatQualifier.CCYYMMDD;
            SubscribeDate.DTP03_Date = new DateTimePeriod(DateTime.Parse("05/01/2006"));

            //Need to create enumeration of the EQ categories, so not just asking for the general 30 information.
            Member.AddLoop("EQ*30");

            Debug.WriteLine("HL3 Test:");
            Debug.WriteLine(HL3Info.SerializeToX12(false));
            Debug.WriteLine("HL*3*2*22*0~TRN*1*93175-012547*9877281234~NM1*IL*1*SMITH*ROBERT*MI****11122333301~DMG*D8*19430519~DTP*291*D8*20060501~EQ*30~");
            Assert.AreEqual("HL*3*2*22*0~TRN*1*93175-012547*9877281234~NM1*IL*1*SMITH*ROBERT*MI****11122333301~DMG*D8*19430519~DTP*291*D8*20060501~EQ*30~", HL3Info.SerializeToX12(false));
            #endregion

            //Test entire 270
            Debug.WriteLine("");
            Debug.WriteLine("Overall Whole:");
            Debug.WriteLine(Message.SerializeToX12(false));
            Debug.WriteLine(inquiry);
            Debug.WriteLine("Outlined Result:");
            Debug.WriteLine(Message.SerializeToX12(true));
            Debug.WriteLine("XML version of the above to help in understanding layout.");
            Debug.WriteLine(Message.Serialize());


            Assert.AreEqual(inquiry, Message.SerializeToX12(false));

        }


        [TestMethod]
        public void Transform270ToHtml()
        {
            var htmlService = new X12HtmlTransformationService(new X12EdiParsingService(suppressComments: false));

            Stream ediFile = new MemoryStream(Encoding.ASCII.GetBytes(inquiry));

            string html = htmlService.Transform(new StreamReader(ediFile).ReadToEnd());

            Trace.Write(html);
        }

    }
}
