using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OopFactory.X12.Presentation
{
    public interface IRenderingService
    {
        byte[] CreatePdf(string domainXml);
    }
}
