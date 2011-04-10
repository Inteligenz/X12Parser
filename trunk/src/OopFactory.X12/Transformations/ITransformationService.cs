using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Transformations
{
    public interface ITransformationService
    {
        string Transform(string x12);
    }
}
