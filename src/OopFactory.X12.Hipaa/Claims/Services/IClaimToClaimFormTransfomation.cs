using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Hipaa.Claims.Forms;

namespace OopFactory.X12.Hipaa.Claims.Services
{
    public interface IClaimToClaimFormTransfomation
    {
        List<FormPage> TransformClaimToClaimFormFoXml(Claim claim);
    }
}
