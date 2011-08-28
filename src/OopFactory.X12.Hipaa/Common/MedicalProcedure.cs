using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Common
{
    public class MedicalProcedure
    {
        public string Qualifier { get; set; }

        public string ProcedureCode { get; set; }
        public string ProcedureCodeEnd { get; set; }

        public string Modifier1 { get; set; }
        public string Modifier2 { get; set; }
        public string Modifier3 { get; set; }
        public string Modifier4 { get; set; }
    }
}
