using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Extensions;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentCL1 : TypedSegment
    {
        public TypedSegmentCL1()
            : base("CL1")
        {
        }

        public string CL101_AdmissionTypeCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string CL102_AdmissionSourceCode {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string CL103_PatientStatusCode {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string CL104_NursingHomeResidentialStatusCode {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }
    }
}
