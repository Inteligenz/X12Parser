using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Xml.Xsl;
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;
using OopFactory.X12.Hipaa.Claims.Forms.Professional;
using OopFactory.X12.Hipaa.Common;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public class ClaimTransformationService
    {
        /// <summary>
        /// Reads a claim that has been st
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public ClaimDocument Transform837ToClaimDocument(Stream stream)
        {

            var parser = new X12Parser();
            var interchange = parser.Parse(stream);
            var xml = interchange.Serialize();

            var transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Claims.Services.Xsl.X12-837-To-ClaimDocument.xslt");

            var transform = new XslCompiledTransform();
            if (transformStream != null) transform.Load(XmlReader.Create(transformStream));

            var outputStream = new MemoryStream();

            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;
            xml = new StreamReader(outputStream).ReadToEnd();

            return ClaimDocument.Deserialize(xml);
        }

#if DEBUG
        public UB04Claim TransformX12837ToUB04Model(Stream stream)
        {
            var parser = new X12Parser();
            var interchange = parser.Parse(stream);
            var xml = interchange.Serialize();

            var transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Claims.Forms.Institutional.X12-837I-To-UB04Model.xslt");

            var transform = new XslCompiledTransform();
            if (transformStream != null) transform.Load(XmlReader.Create(transformStream));
            
            var outputStream = new MemoryStream();
            
            transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), outputStream);
            outputStream.Position = 0;
            var claim = UB04Claim.Deserialize(new StreamReader(outputStream).ReadToEnd());
            return claim;
        }

        public string TransformUB04ClaimToFoXml(UB04Claim claim, string imageFilename)
        {
            var xml = claim.Serialize();
            var transformStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Hipaa.Claims.Forms.Institutional.UB04Model-To-FoXml.xslt");

            var transform = new XslCompiledTransform();
            transform.Load(XmlReader.Create(transformStream));

            var outputStream = new MemoryStream();
            var args = new XsltArgumentList();
            args.AddParam("claim-image", "", imageFilename);

            transform.Transform(XmlReader.Create(new StringReader(xml)), args, outputStream);
            outputStream.Position = 0;

            return new StreamReader(outputStream).ReadToEnd();
        }

        public string TransformHCFA1500ClaimToFoXml(HCFA1500Claim claim, string imageFilename)
        {
            return string.Empty;
        }

        /// <summary>
        /// Takes a generic claim object stream parameter and maps properties to 
        /// corresponding properties in the HCFA 1500 claim. Returns a HCFA1500 claim.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public HCFA1500Claim TransformX12837ToHCFA1500Model(Stream stream)
        {
            // call to existing xslt
            Claim claim = Transform837ToClaimDocument(stream).Claims.First();
            var hcfa = new HCFA1500Claim();
            // Service Location
            hcfa.Field32_FacilityLocationInfo_Name = claim.ServiceLocation.Name.LastName;
            hcfa.Field32_FacilityLocationInfo_Street = claim.ServiceLocation.Address.Line1;
            hcfa.Field32_FacilityLocationInfo_City = claim.ServiceLocation.Address.City;
            hcfa.Field32_FacilityLocationInfo_State = claim.ServiceLocation.Address.StateCode;
            hcfa.Field32_FacilityLocationInfo_Zip = claim.ServiceLocation.Address.PostalCode;
            // Pay To Provider
            hcfa.Field33_BillingProvider_Name = claim.PayToProvider.Name.LastName;
            hcfa.Field33_BillingProvider_Street = claim.PayToProvider.Address.Line1;
            hcfa.Field33_BillingProvider_City = claim.PayToProvider.Address.City;
            hcfa.Field33_BillingProvider_State = claim.PayToProvider.Address.StateCode;
            hcfa.Field33_BillingProvider_Zip = claim.PayToProvider.Address.PostalCode;
            // Patient Control Number
            hcfa.Field26_PatientAccountNumber = claim.PatientControlNumber;
            // Type of Bill
            //hcfa.??? = claim.MedicalRecordNumber; // where on the 1500 is the medical record number?
            // Type of Bill (again?)
            hcfa.Field32a_FacilityNationalProviderIdentifier = claim.ServiceLocationInfo.FacilityCode;
            //hcfa.field ??? = claim.ServiceLocationInfo.Qualifier;  // where on the 1500 is the Qualifier?
            //hcfa.field ??? = claim.ServiceLocationInfo.FrequencyTypeCode;  // where on the 1500 is the FrequencyTypeCode?
            // Federal Tax Number
            hcfa.Field25_FederalTaxIDNumber = claim.PayToProvider.TaxId;
            // shouldnt we represent hcfa.Field25_IsSSN and Field25_IsEIN to know which type TaxID?
            // Statement Covers Period
            hcfa.Field18_HospitalizationDateFrom = claim.StatementFromDate;
            hcfa.Field18_HospitalizationDateTo = claim.StatementToDate;
            // Filler
            ClaimMember patient = claim.Patient ?? claim.Subscriber;
            // Patient Name
            hcfa.Field02_PatientsLastName = patient.Name.LastName;
            hcfa.Field02_PatientsFirstName = patient.Name.FirstName;
            hcfa.Field02_PatientsMiddleName = patient.Name.MiddleName;
            // patient.MemberId // where on the 1500 is the MemberId?
            // Patient Address
            if (patient.Address != null)
            {
                hcfa.Field05_PatientsAddress_Street = patient.Address.Line1;
                    // do we care about , patient.Address.Line2?
                hcfa.Field05_PatientsAddress_City = patient.Address.City;
                hcfa.Field05_PatientsAddress_State = patient.Address.StateCode;
                hcfa.Field05_PatientsAddress_Zip = patient.Address.PostalCode;
            }
            // patient birthdate
            hcfa.Field03_PatientsDateOfBirth = patient.DateOfBirth;
            // patient sex  // should I get rid of false and just use null?
            if (patient.Gender == GenderEnum.Male)
            {
                hcfa.Field03_PatientsSexFemale = null; // is this necessary or would always be null at this point anyway?
                hcfa.Field03_PatientsSexMale = true;
            }
            else if (patient.Gender == GenderEnum.Female)
            {
                hcfa.Field03_PatientsSexFemale = true;
                hcfa.Field03_PatientsSexMale = null;
            }
            // Admission date and hour
            hcfa.Field18_HospitalizationDateFrom = claim.AdmissionDate;
            // Condition Codes  // are these are in "21 - Diagnosis"? If yes, should they be a collection in the HCFA1500Claim.cs class?
            // Occurrences - uh oh... if not service lines then what are they?
            //foreach (var s in claim.Occurrences.Select(occurrence => new HCFA1500ServiceLine
            //                                                             {
            //                                                                 Field24d_ProcedureCode = occurrence.Code, Field24a_DateFrom = occurrence.Date
            //                                                             }))
            //{
            //    hcfa.Field24_ServiceLines.Add(s);
            //}

            // shouldn't occurrence span be part of the occurrence??

            // Responsible Party (Value codes?)

            // Service Lines
            foreach (var line in claim.ServiceLines)
            {
                var hcfaLine = new HCFA1500ServiceLine();
                hcfaLine.Field24d_ProcedureCode = line.Procedure.ProcedureCode;
                // line.RevenueCode
                hcfaLine.Field24d_ProcedureCode = line.Procedure.ProcedureCode;
                hcfaLine.Field24d_Mod1 = line.Procedure.Modifier1;
                hcfaLine.Field24d_Mod2 = line.Procedure.Modifier2;
                hcfaLine.Field24d_Mod3 = line.Procedure.Modifier3;
                hcfaLine.Field24d_Mod4 = line.Procedure.Modifier4;
                // line.Procedure.Description
                hcfaLine.Field24f_Charges = line.ChargeAmount;
                // hcfaLine.Field24g_DaysOrUnits = line.Quantity; ??
                // line.NonCoveredChargeAmount
                hcfaLine.Field24a_DateFrom = line.ServiceDateFrom;
                hcfaLine.Field24a_DateTo = line.ServiceDateTo;
                // hcfaLine.Field24_CommentLine = line.Notes[0]. - is Comment Line same as notes?
                // line.OperatingPhysician
                // line.OperatingPhysician.Name.LastName
                // line.OperatingPhysician.Name.FirstName
                if (line.OperatingPhysician != null)
                {
                    hcfaLine.Field24j_RenderingProviderNpi = line.OperatingPhysician.Npi;
                    hcfaLine.Field24j_RenderingProviderId = line.OperatingPhysician.ProviderInfo.Id;
                }

                hcfa.Field24_ServiceLines.Add(hcfaLine);
            }

            // Diagnosis (collection?) (where are these on the 1500?)

            // Procedures collection (where are these on the 1500?)
            // claim.Procedures

            // Attending Physician
            // claim.AttendingProvider.Name.FirstName
            // claim.AttendingProvider.Identifications[0].Qualifier
            // claim.AttendingProvider.Identifications[0].Id

            // Operating Physician
            // claim.OperatingPhysician.Name.FirstName

            // Other
            //claim.OtherOperatingPhysician.Name.FirstName


            return hcfa;
        }
#endif
    }
}
