using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Claims.Forms;
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public class InstitutionalClaimToUB04ClaimFormTransformation : IClaimToClaimFormTransfomation
    {
        private string _formImagePath;

        public InstitutionalClaimToUB04ClaimFormTransformation(string formImagePath)
        {
            _formImagePath = formImagePath;
        }

        public delegate void TransformCompletedHandler(object sender, Ub04ClaimTransformationArgs args);
        public event TransformCompletedHandler TransformCompleted;

        protected void OnTransformCompleted(Ub04ClaimTransformationArgs args)
        {
            if (TransformCompleted != null)
                TransformCompleted(this, args);
        }

        public virtual UB04Claim TransformClaimToUB04(Claim claim)
        {
            var ub = new UB04Claim();
            ub.Field01_BillingProvider = new UB04Block
            {
                Line1 = claim.BillingProvider.Name.ToString()
            };
            foreach (var line in claim.ServiceLines)
            {
                ub.ServiceLines.Add(new UB04ServiceLine {
                    Field42_RevenueCode = line.RevenueCode
                });
            }
            return ub;
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text)
        {
            return AddBlock(page, x, y, width, text, TextAlignEnum.left);
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text, TextAlignEnum textAlign)
        {
            decimal xScale = 0.08333m; // 0.0839m;
            decimal yScale = 0.16667m; // 0.1656m;
            var block = new FormBlock
            {
                TextAlign = textAlign,
                Left = 0.06m + xScale * x,
                Top = 0.06m + yScale * y,
                Width = xScale * width,
                Height = yScale * 1.1m,
                Text = text
            };
            page.Blocks.Add(block);
            return block;
        }
        
        public virtual List<FormPage> TransformUB04ToFormPages(UB04Claim ub04)
        {
            List<FormPage> pages = new List<FormPage>();
            int pageCount = 1 + ((ub04.ServiceLines.Count - 1) / 22);
            FormPage page = null;
            for (int i = 0; i < ub04.ServiceLines.Count; i++)
            {
                if (i % 22 == 0)
                {
                    page = new FormPage();
                    pages.Add(page);
                    page.MasterReference = "ub04";
                    page.ImagePath = _formImagePath;

                    // header
                    // Box 1
                    AddBlock(page, 2, 1, 28, ub04.Field01_BillingProvider.Line1);
                    AddBlock(page, 2, 2, 28, ub04.Field01_BillingProvider.Line2);
                    AddBlock(page, 2, 3, 28, ub04.Field01_BillingProvider.Line3);
                    AddBlock(page, 2, 4, 28, ub04.Field01_BillingProvider.Line4);
                    
                    // Box 2
                    AddBlock(page, 32, 1, 28, ub04.Field02_PayToProvider.Line1);
                    AddBlock(page, 32, 2, 28, ub04.Field02_PayToProvider.Line2);
                    AddBlock(page, 32, 3, 28, ub04.Field02_PayToProvider.Line3);
                    AddBlock(page, 32, 4, 28, ub04.Field02_PayToProvider.Line4);

                    // Box 3
                    AddBlock(page, 65, 1, 27, ub04.Field03a_PatientControlNumber);
                    AddBlock(page, 65, 2, 27, ub04.Field03b_MedicalRecordNumber);
                    
                    // Box 4
                    AddBlock(page, 94, 2, 5, ub04.Field04_TypeOfBill); 
                    
                    // Box 5
                    AddBlock(page, 62, 4, 10, ub04.Field05_FederalTaxId);

                    // Box 6 - Statement Covers Period
                    AddBlock(page, 74, 4, 9, ub04.Field06_StatementCoversPeriod.FromDate);
                    AddBlock(page, 82, 4, 9, ub04.Field06_StatementCoversPeriod.ThroughDate);
                    
                    // Box 7 - Blank
                    AddBlock(page, 91, 3, 8, ub04.Field07.Line1);
                    AddBlock(page, 91, 4, 8, ub04.Field07.Line2);

                    // Box 8
                    AddBlock(page, 15, 5, 21, ub04.Field08_PatientName_a);
                    AddBlock(page, 3, 6, 33, ub04.Field08_PatientName_b);

                    // Box 9
                    AddBlock(page, 51, 5, 48, ub04.Field09_PatientAddress.a_Street);
                    AddBlock(page, 39, 6, 37, ub04.Field09_PatientAddress.b_City);
                    AddBlock(page, 78, 6, 2, ub04.Field09_PatientAddress.c_State);
                    AddBlock(page, 83, 6, 10, ub04.Field09_PatientAddress.d_PostalCode);
                    AddBlock(page, 96, 6, 3, ub04.Field09_PatientAddress.e_CountryCode);

                    // Box 10
                    AddBlock(page, 2, 8, 8, ub04.Field10_Birthdate);

                    // Box 11
                    AddBlock(page, 13, 8, 1, ub04.Field11_Sex);

                    // Box 12-15
                    AddBlock(page, 16, 8, 6, ub04.Field12_AdmissionDate);
                    AddBlock(page, 23, 8, 2, ub04.Field13_AdmissionHour);
                    AddBlock(page, 27, 8, 2, ub04.Field14_AdmissionType);
                    AddBlock(page, 30, 8, 2, ub04.Field15_AdmissionSource);

                    // Box 16
                    AddBlock(page, 34, 8, 2, ub04.Field16_DischargeHour);

                    // Box 17
                    AddBlock(page, 38, 8, 2,  ub04.Field17_DischargeStatus);
 
                    // Box 18 - 28 Condition Codes
                    AddBlock(page, 41, 8, 2, ub04.Field18_ConditionCode01);
                    AddBlock(page, 45, 8, 2, ub04.Field19_ConditionCode02);
                    AddBlock(page, 48, 8, 2, ub04.Field20_ConditionCode03);
                    AddBlock(page, 52, 8, 2, ub04.Field21_ConditionCode04);
                    AddBlock(page, 56, 8, 2, ub04.Field22_ConditionCode05);
                    AddBlock(page, 59, 8, 2, ub04.Field23_ConditionCode06);
                    AddBlock(page, 63, 8, 2, ub04.Field24_ConditionCode07);
                    AddBlock(page, 66, 8, 2, ub04.Field25_ConditionCode08);
                    AddBlock(page, 70, 8, 2, ub04.Field26_ConditionCode09);
                    AddBlock(page, 73.5m, 8, 2, ub04.Field27_ConditionCode10);
                    AddBlock(page, 77, 8, 2, ub04.Field28_ConditionCode11);

                    // Box 29
                    AddBlock(page, 81, 8, 2, ub04.Field29_AccidentState);
                    
                    // Box 30
                    AddBlock(page, 85, 8, 14, ub04.Field30);

                    // Box 31
                    AddBlock(page, 2, 10, 2, ub04.Field31a_Occurrence.Code);
                    AddBlock(page, 6, 10, 6, ub04.Field31a_Occurrence.Date);
                    AddBlock(page, 2, 11, 2, ub04.Field31b_Occurrence.Code);
                    AddBlock(page, 6, 11, 6, ub04.Field31b_Occurrence.Date);

                   // Box 32
                    AddBlock(page, 14, 10, 2, ub04.Field32a_Occurrence.Code);
                    AddBlock(page, 18, 10, 6, ub04.Field32a_Occurrence.Date);
                    AddBlock(page, 14, 11, 2, ub04.Field32b_Occurrence.Code);
                    AddBlock(page, 18, 11, 6, ub04.Field32b_Occurrence.Date);

                    // Box 33
                    AddBlock(page, 26, 10, 2, ub04.Field33a_Occurrence.Code);
                    AddBlock(page, 30, 10, 6, ub04.Field33a_Occurrence.Date);
                    AddBlock(page, 26, 11, 2, ub04.Field33b_Occurrence.Code);
                    AddBlock(page, 30, 11, 6, ub04.Field33b_Occurrence.Date);

                    // Box 34
                    AddBlock(page, 38, 10, 2, ub04.Field34a_Occurrence.Code);
                    AddBlock(page, 41, 10, 6, ub04.Field34a_Occurrence.Date);
                    AddBlock(page, 38, 11, 2, ub04.Field34b_Occurrence.Code);
                    AddBlock(page, 41, 11, 6, ub04.Field34b_Occurrence.Date);

                    // Box 35
                    AddBlock(page, 50, 10, 2, ub04.Field35a_OccurrenceSpan.Code);
                    AddBlock(page, 53, 10, 6, ub04.Field35a_OccurrenceSpan.FromDate);
                    AddBlock(page, 62, 10, 6, ub04.Field35a_OccurrenceSpan.ThroughDate);
                    AddBlock(page, 50, 11, 2, ub04.Field35b_OccurrenceSpan.Code);
                    AddBlock(page, 53, 11, 6, ub04.Field35b_OccurrenceSpan.FromDate);
                    AddBlock(page, 62, 11, 6, ub04.Field35b_OccurrenceSpan.ThroughDate);

                    // Box 36
                    AddBlock(page, 70, 10, 2, ub04.Field36a_OccurrenceSpan.Code);
                    AddBlock(page, 74, 10, 6, ub04.Field36a_OccurrenceSpan.FromDate);
                    AddBlock(page, 82, 10, 6, ub04.Field36a_OccurrenceSpan.ThroughDate);
                    AddBlock(page, 70, 11, 2, ub04.Field36b_OccurrenceSpan.Code);
                    AddBlock(page, 74, 11, 6, ub04.Field36b_OccurrenceSpan.FromDate);
                    AddBlock(page, 82, 11, 6, ub04.Field36b_OccurrenceSpan.ThroughDate);

                    // Box 37 - Blank
                    AddBlock(page, 90, 10, 9, ub04.Field37.Line1);
                    AddBlock(page, 90, 11, 9, ub04.Field37.Line2);

                    // Box 38 - Responsible Party
                    AddBlock(page, 2, 12, 48, 'X'.Repeat(48));
                    AddBlock(page, 2, 13, 48, 'X'.Repeat(48));
                    AddBlock(page, 2, 14, 48, 'X'.Repeat(48));
                    AddBlock(page, 2, 15, 48, 'X'.Repeat(48));
                    AddBlock(page, 2, 16, 48, 'X'.Repeat(48));

                    // Box 39 - Value Codes
                    AddBlock(page, 53, 13, 2, "XX");
                    AddBlock(page, 57, 13, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 53, 14, 2, "XX");
                    AddBlock(page, 57, 14, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 53, 15, 2, "XX");
                    AddBlock(page, 57, 15, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 53, 16, 2, "XX");
                    AddBlock(page, 57, 16, 12, "0 00", TextAlignEnum.right);

                    // Box 40
                    AddBlock(page, 69, 13, 2, "XX");
                    AddBlock(page, 72.5m, 13, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 69, 14, 2, "XX");
                    AddBlock(page, 72.5m, 14, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 69, 15, 2, "XX");
                    AddBlock(page, 72.5m, 15, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 69, 16, 2, "XX");
                    AddBlock(page, 72.5m, 16, 12, "0 00", TextAlignEnum.right);

                    // Box 41 - Value Codes
                    AddBlock(page, 84, 13, 2, "XX");
                    AddBlock(page, 88, 13, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 84, 14, 2, "XX");
                    AddBlock(page, 88, 14, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 84, 15, 2, "XX");
                    AddBlock(page, 88, 15, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 84, 16, 2, "XX");
                    AddBlock(page, 88, 16, 12, "0 00", TextAlignEnum.right);


                }

                // service lines
                decimal y = 18 + (i % 22);
                var line = ub04.ServiceLines[i];
                
                // Box 42 - 49 - Service Lines
                AddBlock(page, 2, y, 4, line.Field42_RevenueCode);
                AddBlock(page, 7, y, 29, 'X'.Repeat(29));
                AddBlock(page, 37, y, 17, 'X'.Repeat(17));
                AddBlock(page, 56, y, 6, "MMDDYY");
                AddBlock(page, 64, y, 9, "0.00", TextAlignEnum.right);
                AddBlock(page, 74, y, 11, "0 00", TextAlignEnum.right);
                AddBlock(page, 86, y, 11, "0 00", TextAlignEnum.right);
                AddBlock(page, 97, y, 2, "XX");

                if (i % 22 == 21 || i == ub04.ServiceLines.Count - 1) // Footer
                {
                    int pageIndex = 1 + ((i - 1) / 22);
                    AddBlock(page, 13, 40, 3, pageIndex.ToString(), TextAlignEnum.right);
                    AddBlock(page, 20, 40, 3, pageCount.ToString(), TextAlignEnum.right);

                    AddBlock(page, 74, 40, 11, "0 00", TextAlignEnum.right);
                    AddBlock(page, 86, 40, 11, "0 00", TextAlignEnum.right);

                    // Box 50
                    AddBlock(page, 2, 42, 26, 'X'.Repeat(26));
                    AddBlock(page, 2, 43, 26, 'X'.Repeat(26));
                    AddBlock(page, 2, 44, 26, 'X'.Repeat(26));

                    // Box 51
                    AddBlock(page, 29, 42, 17, 'X'.Repeat(17));
                    AddBlock(page, 29, 43, 17, 'X'.Repeat(17));
                    AddBlock(page, 29, 44, 17, 'X'.Repeat(17));

                    // Box 52 - Patient Relationship
                    AddBlock(page, 46.5m, 42, 2, "18");
                    AddBlock(page, 46.5m, 43, 2, "22");
                    AddBlock(page, 46.5m, 44, 2, "23");

                    // Box 53
                    AddBlock(page, 50, 42, 2, "XX");
                    AddBlock(page, 50, 43, 2, "XX");
                    AddBlock(page, 50, 44, 2, "XX");

                    // Box 54
                    AddBlock(page, 54.25m, 42, 11, "0 00", TextAlignEnum.right);
                    AddBlock(page, 54.25m, 43, 11, "0 00", TextAlignEnum.right);
                    AddBlock(page, 54.25m, 44, 11, "0 00", TextAlignEnum.right);

                    // Box 55
                    AddBlock(page, 66.5m, 42, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 66.5m, 43, 12, "0 00", TextAlignEnum.right);
                    AddBlock(page, 66.5m, 44, 12, "0 00", TextAlignEnum.right);

                    // Box 56
                    AddBlock(page, 85, 41, 10, 'X'.Repeat(10));

                    // Box 57
                    AddBlock(page, 82, 42, 17, 'X'.Repeat(17));
                    AddBlock(page, 82, 43, 17, 'X'.Repeat(17));
                    AddBlock(page, 82, 44, 17, 'X'.Repeat(17));

                    // Box 58
                    AddBlock(page, 2, 46, 29, 'X'.Repeat(29));
                    AddBlock(page, 2, 47, 29, 'X'.Repeat(29));
                    AddBlock(page, 2, 48, 29, 'X'.Repeat(29));

                    // Box 59
                    AddBlock(page, 33, 46, 2, "18");
                    AddBlock(page, 33, 47, 2, "19");
                    AddBlock(page, 33, 48, 2, "20");

                    // Box 60
                    AddBlock(page, 36, 46, 23, 'X'.Repeat(23));
                    AddBlock(page, 36, 47, 23, 'X'.Repeat(23));
                    AddBlock(page, 36, 48, 23, 'X'.Repeat(23));

                    // Box 61
                    AddBlock(page, 60, 46, 17, 'X'.Repeat(17));
                    AddBlock(page, 60, 47, 17, 'X'.Repeat(17));
                    AddBlock(page, 60, 48, 17, 'X'.Repeat(17));

                    // Box 62
                    AddBlock(page, 78, 46, 21, 'X'.Repeat(21));
                    AddBlock(page, 78, 47, 21, 'X'.Repeat(21));
                    AddBlock(page, 78, 48, 21, 'X'.Repeat(21));

                    // Box 63
                    AddBlock(page, 2, 50, 35, 'X'.Repeat(35));
                    AddBlock(page, 2, 51, 35, 'X'.Repeat(35));
                    AddBlock(page, 2, 52, 35, 'X'.Repeat(35));

                    // Box 64 - Document Control Number
                    AddBlock(page, 39, 50, 30, 'X'.Repeat(30));
                    AddBlock(page, 39, 51, 30, 'X'.Repeat(30));
                    AddBlock(page, 39, 52, 30, 'X'.Repeat(30));

                    // Box 65 - Employer Name
                    AddBlock(page, 70, 50, 29, 'X'.Repeat(29));
                    AddBlock(page, 70, 51, 29, 'X'.Repeat(29));
                    AddBlock(page, 70, 52, 29, 'X'.Repeat(29));

                    // Box 66 - ICD Version
                    AddBlock(page, 1, 54, 1, "9");

                    // Box 67 - Primary Diagnosis
                    AddBlock(page, 3, 53, 6, "123.45");
                    AddBlock(page, 10.5m, 53, 1, "X");

                    // Box 67A
                    AddBlock(page, 13, 53, 6, "123.45");
                    AddBlock(page, 20, 53, 1, "X");

                    // Box 67B
                    AddBlock(page, 22, 53, 6, "123.45");
                    AddBlock(page, 29.75m, 53, 1, "X");

                    // Box 67C
                    AddBlock(page, 32, 53, 6, "123.45");
                    AddBlock(page, 39.25m, 53, 1, "X");

                    // Box 67D
                    AddBlock(page, 42, 53, 6, "123.45");
                    AddBlock(page, 49m, 53, 1, "X");

                    // Box 67E
                    AddBlock(page, 51, 53, 6, "123.45");
                    AddBlock(page, 58.5m, 53, 1, "X");

                    // Box 67F
                    AddBlock(page, 61, 53, 6, "123.45");
                    AddBlock(page, 68m, 53, 1, "X");
                    
                    // Box 67G
                    AddBlock(page, 70, 53, 6, "123.45");
                    AddBlock(page, 77.75m, 53, 1, "X");

                    // Box 67H
                    AddBlock(page, 80, 53, 6, "123.45");
                    AddBlock(page, 87.25m, 53, 1, "X");

                    // Box 67I
                    AddBlock(page, 3, 54, 6, "123.45");
                    AddBlock(page, 10.5m, 54, 1, "X");
                    
                    // Box 67J
                    AddBlock(page, 13, 54, 6, "123.45");
                    AddBlock(page, 20, 54, 1, "X");

                    // Box 67K
                    AddBlock(page, 22, 54, 6, "123.45");
                    AddBlock(page, 29.75m, 54, 1, "X");

                    // Box 67L
                    AddBlock(page, 32, 54, 6, "123.45");
                    AddBlock(page, 39.25m, 54, 1, "X");

                    // Box 67M
                    AddBlock(page, 42, 54, 6, "123.45");
                    AddBlock(page, 49m, 54, 1, "X");

                    // Box 67N
                    AddBlock(page, 51, 54, 6, "123.45");
                    AddBlock(page, 58.5m, 54, 1, "X");

                    // Box 67O
                    AddBlock(page, 61, 54, 6, "123.45");
                    AddBlock(page, 68m, 54, 1, "X");

                    // Box 67P
                    AddBlock(page, 70, 54, 6, "123.45");
                    AddBlock(page, 77.75m, 54, 1, "X");

                    // Box 67Q
                    AddBlock(page, 80, 54, 6, "123.45");
                    AddBlock(page, 87.25m, 54, 1, "X");
                    
                    // Box 68
                    AddBlock(page, 90, 53, 9, 'X'.Repeat(9));
                    AddBlock(page, 90, 54, 9, 'X'.Repeat(9));

                    // Box 69 - Admitting Diagnosis
                    AddBlock(page, 6, 55, 6, "123.45");

                    // Box 70 - Patient Reason Diagnosis
                    AddBlock(page, 21, 55, 6, "123.45");
                    AddBlock(page, 29, 55, 6, "123.45");
                    AddBlock(page, 38, 55, 6, "123.45");

                    // Box 71 - PPS Code
                    AddBlock(page, 51, 55, 5, "12345");

                    // Box 72 - External Cause of Injury
                    AddBlock(page, 59, 55, 6, "123.45");
                    AddBlock(page, 67m, 55, 1, "X");
                    AddBlock(page, 69, 55, 6, "123.45");
                    AddBlock(page, 76.75m, 55, 1, "X");
                    AddBlock(page, 79, 55, 6, "123.45");
                    AddBlock(page, 86.25m, 55, 1, "X");

                    // Box 73 - Blank
                    AddBlock(page, 89, 55, 10, 'X'.Repeat(10));

                    // Box 74
                    AddBlock(page, 2, 57, 8, 'X'.Repeat(8));
                    AddBlock(page, 12, 57, 6, "MMDDYY");
                    AddBlock(page, 20, 57, 8, 'X'.Repeat(8));
                    AddBlock(page, 29, 57, 6, "MMDDYY");
                    AddBlock(page, 38, 57, 8, 'X'.Repeat(8));
                    AddBlock(page, 48, 57, 6, "MMDDYY");
                    AddBlock(page, 2, 59, 8, 'X'.Repeat(8));
                    AddBlock(page, 12, 59, 6, "MMDDYY");
                    AddBlock(page, 20, 59, 8, 'X'.Repeat(8));
                    AddBlock(page, 29, 59, 6, "MMDDYY");
                    AddBlock(page, 38, 59, 8, 'X'.Repeat(8));
                    AddBlock(page, 48, 59, 6, "MMDDYY");

                    // Box 75
                    AddBlock(page, 56, 56, 4, 'X'.Repeat(4));
                    AddBlock(page, 56, 57, 4, 'X'.Repeat(4));
                    AddBlock(page, 56, 58, 4, 'X'.Repeat(4));
                    AddBlock(page, 56, 59, 4, 'X'.Repeat(4));

                    // Box 76
                    AddBlock(page, 72, 56, 10, 'X'.Repeat(10));
                    AddBlock(page, 86, 56, 2, "XX");
                    AddBlock(page, 89, 56, 10, 'X'.Repeat(10));
                    AddBlock(page, 64, 57, 18, 'X'.Repeat(18));
                    AddBlock(page, 86, 57, 13, 'X'.Repeat(13));

                    // Box 77
                    AddBlock(page, 72, 58, 10, 'X'.Repeat(10));
                    AddBlock(page, 86, 58, 2, "XX");
                    AddBlock(page, 89, 58, 10, 'X'.Repeat(10));
                    AddBlock(page, 64, 59, 18, 'X'.Repeat(18));
                    AddBlock(page, 86, 59, 13, 'X'.Repeat(13));

                    // Box 78
                    AddBlock(page, 72, 60, 10, 'X'.Repeat(10));
                    AddBlock(page, 86, 60, 2, "XX");
                    AddBlock(page, 89, 60, 10, 'X'.Repeat(10));
                    AddBlock(page, 64, 61, 18, 'X'.Repeat(18));
                    AddBlock(page, 86, 61, 13, 'X'.Repeat(13));

                    // Box 79
                    AddBlock(page, 72, 62, 10, 'X'.Repeat(10));
                    AddBlock(page, 86, 62, 2, "XX");
                    AddBlock(page, 89, 62, 10, 'X'.Repeat(10));
                    AddBlock(page, 64, 63, 18, 'X'.Repeat(18));
                    AddBlock(page, 86, 63, 13, 'X'.Repeat(13));

                    // Box 80
                    AddBlock(page, 2, 61, 27, 'X'.Repeat(27));
                    AddBlock(page, 2, 62, 27, 'X'.Repeat(27));
                    AddBlock(page, 2, 63, 27, 'X'.Repeat(27));

                    // Box 81
                    AddBlock(page, 32, 60, 2, "AB");
                    AddBlock(page, 35, 60, 10, 'X'.Repeat(10));
                    AddBlock(page, 48, 60, 12, 'X'.Repeat(12));
                    AddBlock(page, 32, 61, 2, "AB");
                    AddBlock(page, 35, 61, 10, 'X'.Repeat(10));
                    AddBlock(page, 48, 61, 12, 'X'.Repeat(12));
                    AddBlock(page, 32, 62, 2, "AB");
                    AddBlock(page, 35, 62, 10, 'X'.Repeat(10));
                    AddBlock(page, 48, 62, 12, 'X'.Repeat(12));
                    AddBlock(page, 32, 63, 2, "AB");
                    AddBlock(page, 35, 63, 10, 'X'.Repeat(10));
                    AddBlock(page, 48, 63, 12, 'X'.Repeat(12));
                }
            }
            return pages;
        }

        public List<FormPage> TransformClaimToClaimFormFoXml(Claim claim)
        {
            UB04Claim ub04 = TransformClaimToUB04(claim);
            OnTransformCompleted(new Ub04ClaimTransformationArgs(claim, ub04));

            return TransformUB04ToFormPages(ub04);
        }
    }
}