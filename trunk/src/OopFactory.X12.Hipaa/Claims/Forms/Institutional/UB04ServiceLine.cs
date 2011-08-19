using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    [Serializable]
    public class UB04ServiceLine
    {
        /*
         * 2011/8/15, jhalliday - New Data Model for 837I (Institutional) claim - collections objects.
         * 
         * Team: dstrubhar, jhalliday and epkrause
         * 
         * Purpose:
         * To create a C# object model that will serve as a container for the X12 837I data
         * AS ENTERED from a UB-04 CMS-1450 (formerly UB-92) Institutional (Inpatient Hospital) claim form.
         * The classes in this namespace are used to add collection types to the main UB04Claim class
         * for value that repeat on the UB-04 form.
         * 
         * Goal:
         * The team has the overall goal of creating tools that can be used to consume and
         * manipulate X12 messages (AKA files/documents) without the need to have a big project
         * budget.  For that reason, this and the related X12 Parser project tools are all open
         * source and freely usable.
         */

        private string _field42_RevenueCode;
        private string _field43_RevenueDescription;
        private string _field44_HCPCS_Rates;
        private string _field45_ServiceDate;
        private string _field46_UnitsOfService;
        private decimal _field47_TotalCharges;
        private string _field48_NonCoveredCharges;
        private string _field49_Filler;

        public string Field42_RevenueCode 
        {
            get { return _field42_RevenueCode; }
            set { _field42_RevenueCode = value; }
        }
        public string Field43_RevenueDescription
        {
            get { return _field43_RevenueDescription; }
            set { _field43_RevenueDescription = value; }
        }
        public string Field44_HCPCS_Rates
        {
            get { return _field44_HCPCS_Rates; }
            set { _field44_HCPCS_Rates = value; }
        }
        public string Field45_ServiceDate
        {
            get { return _field45_ServiceDate; }
            set { _field45_ServiceDate = value; }
        }
        public string Field46_UnitsOfService
        {
            get { return _field46_UnitsOfService; }
            set { _field46_UnitsOfService = value; }
        }
        public decimal Field47_TotalCharges
        {
            get { return _field47_TotalCharges; }
            set { _field47_TotalCharges = value; }
        }
        public string Field48_NonCoveredCharges
        {
            get { return _field48_NonCoveredCharges; }
            set { _field48_NonCoveredCharges = value; }
        }

        public string Field49_Filler            // Field 49 - Reserved by NUBC for future use.
        {
            get { return _field49_Filler; }
            set { _field49_Filler = value; }
        }
    }

    [Serializable]
    public class UB04OccurrenceCodesAndDates
    {
        private string _field31_34_OccurrenceCode;
        private string _field31_34_OccurrenceDate;

        public string Field31_34_OccurrenceCode
        {
            get { return _field31_34_OccurrenceCode; }
            set { _field31_34_OccurrenceCode = value; }
        }
        public string Field31_34_OccurrenceDate
        {
            get { return _field31_34_OccurrenceDate; }
            set { _field31_34_OccurrenceDate = value; }
        }
    }

    [Serializable]
    public class UB04OccurrenceSpanCodesAndDates
    {
        private string _field35_36_OccurrenceSpanCode;
        private string _field35_36_OccurrenceSpanDate;

        public string Field35_36_OccurrenceSpanCode
        {
            get { return _field35_36_OccurrenceSpanCode; }
            set { _field35_36_OccurrenceSpanCode = value; }
        }
        public string Field35_36_OccurrenceSpanDate
        {
            get { return _field35_36_OccurrenceSpanDate; }
            set { _field35_36_OccurrenceSpanDate = value; }
        }
    }

    [Serializable]
    public class UB04ValueCodesAndAmounts
    {
        public string ValueCode { get; set; }

        public string Amount { get; set; }
    }

    [Serializable]
    public class UB04TotalChargesLine
    {
        private string _serviceLineTotals_PageNumber;
        private string _serviceLineTotals_CreationDate;
        private string _serviceLineTotals_TotalCharges;

        public string ServiceLineTotals_PageNumber
        {
            get { return _serviceLineTotals_PageNumber; }
            set { _serviceLineTotals_PageNumber = value; }
        }

        public string ServiceLineTotals_CreationDate
        {
            get { return _serviceLineTotals_CreationDate; }
            set { _serviceLineTotals_CreationDate = value; }
        }

        public string ServiceLineTotals_TotalCharges
        {
            get { return _serviceLineTotals_TotalCharges; }
            set { _serviceLineTotals_TotalCharges = value; }
        }
    }

    [Serializable]
    public class UB04OtherProcedureCodes
    {
        // First occurrence is assumed to be the primary procedure code.
        private string _procedureCode;
        private string _procedureDate;

        public string ProcedureCode
        {
            get { return _procedureCode; }
            set { _procedureCode = value; }
        }

        public string ProcedureDate
        {
            get { return _procedureDate; }
            set { _procedureDate = value; }
        }
    }

    [Serializable]
    public class UB04Code_Code
    {
        private string _field81a;
        private string _field81b;
        private string _field81c;

        public string Field81a
        {
            get { return _field81a; }
            set { _field81a = value; }
        }

        public string Field81b
        {
            get { return _field81b; }
            set { _field81b = value; }
        }

        public string Field81c
        {
            get { return _field81c; }
            set { _field81c = value; }
        }
    }
}
