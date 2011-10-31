using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Hipaa.Claims.Forms.Institutional
{
    public class UB04Diagnosis
    {
        public string Code { get; set; }
        public string PresentOnAdmissionIndicator { get; set; }

        public UB04Diagnosis CopyFrom(Diagnosis source)
        {
            if (string.IsNullOrWhiteSpace(source.Code) || source.Code.Length <= 3)
                Code = source.Code;
            else
                Code = String.Format("{0}.{1}", source.Code.Substring(0, 3), source.Code.Substring(3));
            PresentOnAdmissionIndicator = source.PoiIndicator;
            return this;
        }
    }
}
