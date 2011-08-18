using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Dental
{
    class J515ServiceLines
    {
        private string _field24_ProcedureDate;                      // MMDDCCYY
        private string _field25_AreaOfOralCavity;
        private string _field26_ToothSystem;
        private string _field27_ToothNumberOrLetter;
        private string _field28_ToothSurface;
        private string _field29_ProcedureCode;
        private string _field30_Description;
        private string _field31_Fee;

        public string Field24_ProcedureDate
        {
            get { return _field24_ProcedureDate; }
            set { _field24_ProcedureDate = value; }
        }

        public string Field25_AreaOfOralCavity
        {
            get { return _field25_AreaOfOralCavity; }
            set { _field25_AreaOfOralCavity = value; }
        }

        public string Field26_ToothSystem
        {
            get { return _field26_ToothSystem; }
            set { _field26_ToothSystem = value; }
        }

        public string Field27_ToothNumberOrLetter
        {
            get { return _field27_ToothNumberOrLetter; }
            set { _field27_ToothNumberOrLetter = value; }
        }

        public string Field28_ToothSurface
        {
            get { return _field28_ToothSurface; }
            set { _field28_ToothSurface = value; }
        }

        public string Field29_ProcedureCode
        {
            get { return _field29_ProcedureCode; }
            set { _field29_ProcedureCode = value; }
        }

        public string Field30_Description
        {
            get { return _field30_Description; }
            set { _field30_Description = value; }
        }

        public string Field31_Fee
        {
            get { return _field31_Fee; }
            set { _field31_Fee = value; }
        }
    }

    class Field34_MissingTeethInfo_Permanent
    {
        /*  Permanent teeth are the 32 possible teeth of an adult. */
        //private int[] _permanentMissing = new int[32];

        //public int this[int j]
        //{
        //    get { return _permanentMissing[j]; }
        //    set { _permanentMissing[j] = j; }
        //}

        private string _permanentMissing;

        public string Field34_MissingTeethInfo_Permanent
        {
            get { return _permanentMissing; }
            set { _permanentMissing = value; }
        }
    }

    class Field34_MissingTeethInfo_Primary
    {
        /*  Primary teeth are the 20 teeth of a child. */
        private string _primaryMissing;

        public string PrimaryMissing
        {
            get { return _primaryMissing; }
            set { _primaryMissing = value; }
        }

        //private int[] _primaryMissing = new int[32];

        //public int this[int j]
        //{
        //    get { return _primaryMissing[j]; }
        //    set { _primaryMissing[j] = j; }
        //}

    }

}
