using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Dental
{
    #if DEBUG
    class J515ServiceLines
    {
        //private string _field24_ProcedureDate;                      // MMDDCCYY
        //private string _field25_AreaOfOralCavity;
        //private string _field26_ToothSystem;
        //private string _field27_ToothNumberOrLetter;
        //private string _field28_ToothSurface;
        //private string _field29_ProcedureCode;
        //private string _field30_Description;
        //private string _field31_Fee;

        public string Field24_ProcedureDate { get; set; }
        public string Field25_AreaOfOralCavity { get; set; }
        public string Field26_ToothSystem { get; set; }
        public string Field27_ToothNumberOrLetter { get; set; }
        public string Field28_ToothSurface { get; set; }
        public string Field29_ProcedureCode { get; set; }
        public string Field30_Description { get; set; }
        public decimal Field31_Fee { get; set; }
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

        //private string _permanentMissing;

        public string MissingTeethInfo_Permanent_hi { get; set; }
    }

    class Field34_MissingTeethInfo_Primary
    {
        /*  Primary teeth are the 20 teeth of a child. */
        //private string _primaryMissing;

        public string PrimaryMissing { get; set; }
        //private int[] _primaryMissing = new int[32];

        //public int this[int j]
        //{
        //    get { return _primaryMissing[j]; }
        //    set { _primaryMissing[j] = j; }
        //}

    }
#endif
}
