using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentOI : TypedSegment
    {
        public TypedSegmentOI()
            : base("OI")
        {
        }

        public string OI01_ClaimFilingIndicatorCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string OI02_ClaimSubmissionReasonCode
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        public string OI03_YesNoCondRespCode {
            get { return _segment.GetElement(3); }
            set { _segment.SetElement(3, value); }
        }

        public string OI04_PatientSignatureSourceCode {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public string OI05_ProviderAgreementCode {
            get { return _segment.GetElement(5); }
            set { _segment.SetElement(5, value); }
        }

        public string OI06_ReleaseOfInformationCode {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public string OI07_ProviderAcceptAssignmentCode{
            get { return _segment.GetElement(7); }
            set { _segment.SetElement(7, value); }
        }
    }
}
