using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    public class UB04PatientAddress
    {
        public string a_Street { get; set; }
        public string b_City { get; set; }
        public string c_State { get; set; }
        public string d_PostalCode { get; set; }
        public string e_CountryCode { get; set; }

    }
}
