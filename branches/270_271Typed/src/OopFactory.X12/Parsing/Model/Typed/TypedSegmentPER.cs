using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public enum CommunicationNumberQualifer
    {
        Undefined,
        ElectronicMail,
        TelephoneExtension,
        Facsimile,
        Telephone
    }
    public class TypedSegmentPER : TypedSegment
    {
        public TypedSegmentPER()
            : base("PER")
        {
        }

        public string PER01_ContactFunctionCode
        {
            get { return _segment.GetElement(1); }
            set { _segment.SetElement(1, value); }
        }

        public string PER02_Name
        {
            get { return _segment.GetElement(2); }
            set { _segment.SetElement(2, value); }
        }

        private CommunicationNumberQualifer GetQualifier(int elementNumber)
        {
            switch (_segment.GetElement(elementNumber))
            {
                case "EM": return CommunicationNumberQualifer.ElectronicMail;
                case "EX": return CommunicationNumberQualifer.TelephoneExtension;
                case "FX": return CommunicationNumberQualifer.Facsimile;
                case "TE": return CommunicationNumberQualifer.Telephone;
                default: return CommunicationNumberQualifer.Undefined;
            }
        }

        private void SetQualifier(int elementNumber, CommunicationNumberQualifer value)
        {
            switch (value)
            {
                case CommunicationNumberQualifer.ElectronicMail:
                    _segment.SetElement(elementNumber, "EM"); break;
                case CommunicationNumberQualifer.TelephoneExtension:
                    _segment.SetElement(elementNumber, "EX"); break;
                case CommunicationNumberQualifer.Facsimile:
                    _segment.SetElement(elementNumber, "FX"); break;
                case CommunicationNumberQualifer.Telephone:
                    _segment.SetElement(elementNumber, "TE"); break;
                default:
                    _segment.SetElement(elementNumber, ""); break;
            }
        }

        public CommunicationNumberQualifer PER03_CommunicationNumberQualifier
        {
            get { return GetQualifier(3); }
            set { SetQualifier(3, value); }
        }

        public string PER04_CommunicationNumber
        {
            get { return _segment.GetElement(4); }
            set { _segment.SetElement(4, value); }
        }

        public CommunicationNumberQualifer PER05_CommunicationNumberQualifier
        {
            get { return GetQualifier(5); }
            set { SetQualifier(5, value); }
        }

        public string PER06_CommunicationNumber
        {
            get { return _segment.GetElement(6); }
            set { _segment.SetElement(6, value); }
        }

        public CommunicationNumberQualifer PER07_CommunicationNumberQualifier
        {
            get { return GetQualifier(7); }
            set { SetQualifier(7, value); }
        }

        public string PER08_CommunicationNumber
        {
            get { return _segment.GetElement(8); }
            set { _segment.SetElement(8, value); }
        }

        public string PER09_ContactInquiryReference
        {
            get { return _segment.GetElement(9); }
            set { _segment.SetElement(9, value); }
        }
    }
}
