using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Parsing.Model.Typed
{
    public class TypedSegmentHI : TypedSegment
    {
        private TypedElementHealthCareCodeInformation _HI01;
        private TypedElementHealthCareCodeInformation _HI02;
        private TypedElementHealthCareCodeInformation _HI03;
        private TypedElementHealthCareCodeInformation _HI04;
        private TypedElementHealthCareCodeInformation _HI05;
        private TypedElementHealthCareCodeInformation _HI06;
        private TypedElementHealthCareCodeInformation _HI07;
        private TypedElementHealthCareCodeInformation _HI08;
        private TypedElementHealthCareCodeInformation _HI09;
        private TypedElementHealthCareCodeInformation _HI10;
        private TypedElementHealthCareCodeInformation _HI11;
        private TypedElementHealthCareCodeInformation _HI12;

        public TypedSegmentHI() : base("HI")
        {
        }

        internal override void Initialize(Container parent, X12DelimiterSet delimiters) {
            base.Initialize(parent, delimiters);
            _HI01 = new TypedElementHealthCareCodeInformation(_segment, 1);
            _HI02 = new TypedElementHealthCareCodeInformation(_segment, 2);
            _HI03 = new TypedElementHealthCareCodeInformation(_segment, 3);
            _HI04 = new TypedElementHealthCareCodeInformation(_segment, 4);
            _HI05 = new TypedElementHealthCareCodeInformation(_segment, 5);
            _HI06 = new TypedElementHealthCareCodeInformation(_segment, 6);
            _HI07 = new TypedElementHealthCareCodeInformation(_segment, 7);
            _HI08 = new TypedElementHealthCareCodeInformation(_segment, 8);
            _HI09 = new TypedElementHealthCareCodeInformation(_segment, 9);
            _HI10 = new TypedElementHealthCareCodeInformation(_segment, 10);
            _HI11 = new TypedElementHealthCareCodeInformation(_segment, 11);
            _HI12 = new TypedElementHealthCareCodeInformation(_segment, 12);
        }

        public TypedElementHealthCareCodeInformation HI01_HealthCareCodeInformation {
            get { return _HI01; }
        }

        public TypedElementHealthCareCodeInformation HI02_HealthCareCodeInformation {
            get { return _HI02; }
        }

        public TypedElementHealthCareCodeInformation HI03_HealthCareCodeInformation {
            get { return _HI03; }
        }

        public TypedElementHealthCareCodeInformation HI04_HealthCareCodeInformation {
            get { return _HI04; }
        }

        public TypedElementHealthCareCodeInformation HI05_HealthCareCodeInformation {
            get { return _HI05; }
        }

        public TypedElementHealthCareCodeInformation HI06_HealthCareCodeInformation {
            get { return _HI06; }
        }

        public TypedElementHealthCareCodeInformation HI07_HealthCareCodeInformation {
            get { return _HI07; }
        }

        public TypedElementHealthCareCodeInformation HI08_HealthCareCodeInformation {
            get { return _HI08; }
        }

        public TypedElementHealthCareCodeInformation HI09_HealthCareCodeInformation {
            get { return _HI09; }
        }

        public TypedElementHealthCareCodeInformation HI10_HealthCareCodeInformation {
            get { return _HI10; }
        }

        public TypedElementHealthCareCodeInformation HI11_HealthCareCodeInformation {
            get { return _HI11; }
        }

        public TypedElementHealthCareCodeInformation HI12_HealthCareCodeInformation {
            get { return _HI12; }
        }
    }
}
