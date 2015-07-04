using OopFactory.X12.Parsing.Model.Typed.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentHI : TypedSegment
    {
        public TypedSegmentHI()
            : base("HI")
        {
        }

        public TypedSegmentHI(Segment segment) : base(segment) { }

        public TypedElementHealthCareCodeInformation HI01_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 1); }
            set { this._segment.SetElement(1, value); }
        }

        public TypedElementHealthCareCodeInformation HI02_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 2); }
            set { this._segment.SetElement(2, value); }
        }

        public TypedElementHealthCareCodeInformation HI03_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 3); }
            set { this._segment.SetElement(3, value); }
        }

        public TypedElementHealthCareCodeInformation HI04_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 4); }
            set { this._segment.SetElement(4, value); }
        }

        public TypedElementHealthCareCodeInformation HI05_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 5); }
            set { this._segment.SetElement(5, value); }
        }

        public TypedElementHealthCareCodeInformation HI06_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 6); }
            set { this._segment.SetElement(6, value); }
        }

        public TypedElementHealthCareCodeInformation HI07_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 7); }
            set { this._segment.SetElement(7, value); }
        }

        public TypedElementHealthCareCodeInformation HI08_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 8); }
            set { this._segment.SetElement(8, value); }
        }

        public TypedElementHealthCareCodeInformation HI09_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 9); }
            set { this._segment.SetElement(9, value); }
        }

        public TypedElementHealthCareCodeInformation HI10_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 10); }
            set { this._segment.SetElement(10, value); }
        }

        public TypedElementHealthCareCodeInformation HI11_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 11); }
            set { this._segment.SetElement(11, value); }
        }

        public TypedElementHealthCareCodeInformation HI12_HealthCareCodeInformation
        {
            get { return new TypedElementHealthCareCodeInformation(_segment, 12); }
            set { this._segment.SetElement(12, value); }
        }

        public TypedElementHealthCareCodeInformation CreateNewTypedElementHealthCareCodeInformation(int elementNumber, CodeListQualifierCode CodeListQualifierCode, string IndustryCode)
        {
            return new TypedElementHealthCareCodeInformation(_segment, elementNumber)
            {
                _1_CodeListQualifierCode = CodeListQualifierCode,
                _2_IndustryCode = IndustryCode,
            };
        }
    }
}
