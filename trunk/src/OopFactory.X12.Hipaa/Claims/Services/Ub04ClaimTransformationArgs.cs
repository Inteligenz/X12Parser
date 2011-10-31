using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Claims.Forms.Institutional;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public class Ub04ClaimTransformationArgs : EventArgs
    {
        public Ub04ClaimTransformationArgs(Claim source, UB04Claim target)
        {
            Source = source;
            Target = target;
        }

        public Claim Source { get; private set; }
        public UB04Claim Target { get; private set; }
    }
}
