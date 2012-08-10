using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Validation
{
    public interface IControlNumberGenerator
    {
        void Reset();
        string GetNextControlNumber();
    }
}
