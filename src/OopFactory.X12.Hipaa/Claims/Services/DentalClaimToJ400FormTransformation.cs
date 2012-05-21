using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Common;
using OopFactory.X12.Hipaa.Claims.Forms;
using OopFactory.X12.Hipaa.Claims.Forms.Dental;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public class DentalClaimToJ400FormTransformation : IClaimToClaimFormTransfomation
    {
        private string _formImagePath;

        public DentalClaimToJ400FormTransformation(string formImagePath)
        {
            _formImagePath = formImagePath;
        }

        public J400Claim TransformClaimToJ400(Claim claim)
        {
            var j400 = new J400Claim();

            j400.Field12_SubscriberInformation.Line1 = claim.Subscriber.Name.Formatted();
            if (claim.Subscriber.Address != null)
            {
                j400.Field12_SubscriberInformation.Line2 = claim.Subscriber.Address.Line1;
                if (!string.IsNullOrEmpty(claim.Subscriber.Address.Line2))
                {
                    j400.Field12_SubscriberInformation.Line3 = claim.Subscriber.Address.Line2;
                    j400.Field12_SubscriberInformation.Line4 = claim.Subscriber.Address.Locale;
                }
                else
                    j400.Field12_SubscriberInformation.Line3 = claim.Subscriber.Address.Locale;
            }

            j400.Field13_SubscriberDateOfBirth = claim.Subscriber.DateOfBirth;
            j400.Field14_SubscriberGender_Female = claim.Subscriber.Gender == GenderEnum.Female;
            j400.Field14_SubscriberGender_Male = claim.Subscriber.Gender == GenderEnum.Male;
            j400.Field15_SubscriberId = claim.Subscriber.Name.Identification.Id;
            j400.Field16_SubscriberGroupNumber = claim.SubscriberInformation.ReferenceIdentification;
            
            if (claim.Patient == null)
            {
                j400.Field18_PatientRelationshipToSubscriber_Self = true;
            }
            else
            {
                switch (claim.Patient.Relationship.Code)
                {
                    case "18":
                        j400.Field18_PatientRelationshipToSubscriber_Self = true;
                        break;
                    case "01":
                        j400.Field18_PatientRelationshipToSubscriber_Spouse = true;
                        break;
                    case "19":
                        j400.Field18_PatientRelationshipToSubscriber_Dependent = true;
                        break;
                    default:
                        j400.Field18_PatientRelationshipToSubscriber_Other = true;
                        break;
                }
                
            }
            
            var patient = claim.Patient ?? claim.Subscriber;

            j400.Field20_PatientInformation.Line1 = patient.Name.Formatted();
            if (patient.Address != null)
            {
                j400.Field20_PatientInformation.Line2 = patient.Address.Line1;
                if (!string.IsNullOrEmpty(patient.Address.Line2))
                {
                    j400.Field20_PatientInformation.Line3 = patient.Address.Line2;
                    j400.Field20_PatientInformation.Line4 = patient.Address.Locale;
                }
                else
                    j400.Field20_PatientInformation.Line3 = patient.Address.Locale;
            }
            j400.Field21_PatientDateOfBirth = patient.DateOfBirth;
            j400.Field22_PatientGender_Female = patient.Gender == GenderEnum.Female;
            j400.Field22_PatientGender_Male = patient.Gender == GenderEnum.Male;
            j400.Field23_PatientAccountNumber = claim.PatientControlNumber;


            foreach (var line in claim.ServiceLines)
            {
                var j400Line = new J400ServiceLine
                {
                    Field24_ProcedureDate = line.ServiceDateFrom,
                    Field25_AreaOfOralCavity = string.Join(",", line.OralCavityDesignations.Select(ocd => ocd.Code)),
                    Field27_ToothNumberOrLetter = string.Join(",", line.ToothInformations.Select(ti => ti.ToothCode)),
                    Field28_ToothSurface = string.Join("", line.ToothInformations.Select(t => string.Join("", t.ToothSurfaces.Select(ts => ts.Code)))),
                    Field29_ProcedureCode = line.Procedure != null ? line.Procedure.ProcedureCode : "",
                    Field31_Fee = line.ChargeAmount
                };
                
                j400.ServiceLines.Add(j400Line);
            }
            return j400;
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text)
        {
            return AddBlock(page, x, y, width, text, TextAlignEnum.left);
        }

        private FormBlock AddBlock(FormPage page, decimal x, decimal y, decimal width, string text, TextAlignEnum textAlign)
        {
            decimal xScale = 0.100m; // 0.0839m;
            decimal yScale = 0.16667m; // 0.1656m;
            var block = new FormBlock
            {
                LetterSpacing = "1.2px",
                TextAlign = textAlign,
                Left = 0.14m + xScale * x,
                Top = 0.06m + yScale * y,
                Width = xScale * width,
                Height = yScale * 1.1m,
                Text = text
            };
            page.Blocks.Add(block);
            return block;
        }


        public List<FormPage> TransformJ400ToFormPages(J400Claim j400)
        {
            List<FormPage> pages = new List<FormPage>();
            int pageCount = 1 + ((j400.ServiceLines.Count - 1) / 10);
            FormPage page = null;
            for (int i = 0; i < j400.ServiceLines.Count; i++)
            {
                if (i % 10 == 0)
                {
                    page = new FormPage();
                    pages.Add(page);
                    page.MasterReference = "j400";
                    page.ImagePath = _formImagePath;

                    AddBlock(page, 43, 8, 38, j400.Field12_SubscriberInformation.Line1);
                    AddBlock(page, 43, 9, 38, j400.Field12_SubscriberInformation.Line2);
                    AddBlock(page, 43, 10, 38, j400.Field12_SubscriberInformation.Line3);
                    AddBlock(page, 43, 11, 38, j400.Field12_SubscriberInformation.Line4);
                    AddBlock(page, 43, 13, 10, string.Format("{0:MM/dd/yyyy}", j400.Field13_SubscriberDateOfBirth));
                    AddBlock(page, 57, 13, 1, j400.Field14_SubscriberGender_Female ? "X" : "");
                    AddBlock(page, 60, 13, 1, j400.Field14_SubscriberGender_Male ? "X" : "");
                    AddBlock(page, 65, 13, 15, j400.Field15_SubscriberId);
                    AddBlock(page, 43, 15, 11, j400.Field16_SubscriberGroupNumber);
                    AddBlock(page, 56, 15, 24, 'X'.Repeat(24));

                    AddBlock(page, 43, 18, 1, j400.Field18_PatientRelationshipToSubscriber_Self ? "X": "");
                    AddBlock(page, 48, 18, 1, j400.Field18_PatientRelationshipToSubscriber_Spouse ? "X" : "");
                    AddBlock(page, 54, 18, 1, j400.Field18_PatientRelationshipToSubscriber_Dependent ? "X" : "");
                    AddBlock(page, 63, 18, 1, j400.Field04_OtherDentalOrMedicalCoverage ? "X" : "");
                    AddBlock(page, 43, 20, 38, j400.Field20_PatientInformation.Line1);
                    AddBlock(page, 43, 21, 38, j400.Field20_PatientInformation.Line2);
                    AddBlock(page, 43, 22, 38, j400.Field20_PatientInformation.Line3);
                    AddBlock(page, 43, 23, 38, j400.Field20_PatientInformation.Line4);
                    AddBlock(page, 43, 25, 10, string.Format("{0:MM/dd/yyyy}", j400.Field21_PatientDateOfBirth));
                    AddBlock(page, 57, 25, 1, j400.Field22_PatientGender_Female ? "X" : "");
                    AddBlock(page, 60, 25, 1, j400.Field22_PatientGender_Male ? "X" : "");
                    AddBlock(page, 64, 25, 17, j400.Field23_PatientAccountNumber);
                }
                decimal y = 29 + (i % 10);
                var line = j400.ServiceLines[i];
                AddBlock(page, 2, y, 10, string.Format("{0:MM/dd/yyyy}", line.Field24_ProcedureDate));
                AddBlock(page, 13, y, 2, line.Field25_AreaOfOralCavity);
                AddBlock(page, 16, y, 2, line.Field26_ToothSystem);
                AddBlock(page, 19, y, 11, line.Field27_ToothNumberOrLetter);
                AddBlock(page, 31, y, 5, line.Field28_ToothSurface);
                AddBlock(page, 37, y, 5, line.Field29_ProcedureCode);
                AddBlock(page, 43, y, 31, 'D'.Repeat(31)); // line.Field30_Description);
                string amount = string.Format("{0:0.00}", line.Field31_Fee).Replace(".","");
                AddBlock(page, 81 - amount.Length, y, amount.Length, amount);
                if (i % 10 == 9 || i == j400.ServiceLines.Count - 1) // Footer
                {
                }
            }

            return pages;
        }

        public List<FormPage> TransformClaimToClaimFormFoXml(Claim claim)
        {
            J400Claim j400 = TransformClaimToJ400(claim);

            return TransformJ400ToFormPages(j400);
        }
    }
}
