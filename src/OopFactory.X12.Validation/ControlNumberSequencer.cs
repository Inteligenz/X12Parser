using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Validation
{
    public class ControlNumberSequencer : IControlNumberGenerator
    {
        private int _currentNumber;

        public ControlNumberSequencer()
        {
            Reset();
        }

        public int CurrentNumber { get { return _currentNumber; } }

        public void Reset()
        {
            _currentNumber = 0;
        }

        public string GetNextControlNumber()
        {
            return string.Format("{0:000000000}", ++_currentNumber);
        }
    }
}
