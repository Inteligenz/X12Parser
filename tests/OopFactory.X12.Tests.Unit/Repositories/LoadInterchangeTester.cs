using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing;
using OopFactory.X12.Repositories;
using System.Reflection;
using System.Diagnostics;

namespace OopFactory.X12.Tests.Unit.Repositories
{
    [TestClass]
    public class LoadInterchangeTester
    {
        [TestMethod]
        public void LoadAllTestFiles()
        {

            var repo = new SqlTransactionRepository("Data Source=DSTRU-PC;Initial Catalog=X12;Integrated Security=True", new SpecificationFinder(),
                new string[] {"AMT",
                "BHT",
                "CAS",
                "CL1",
                "CLM",
                "CN1",
                "CR1",
                "CR2",
                "CR3",
                "CR4",
                "CR5",
                "CR6",
                "CR7",
                "CR8",
                "CRC",
                "CTP",
                "CUR",
                "DMG",
                "DN1",
                "DN2",
                "DSB",
                "DTP",
                "HCP",
                "HI",
                "HL",
                "HSD",
                "IMM",
                "K3",
                "LIN",
                "LX",
                "MEA",
                "MIA",
                "MOA",
                "N2",
                "N3",
                "N4",
                "NM1",
                "NTE",
                "OI",
                "PAT",
                "PER",
                "PRV",
                "PS1",
                "PWK",
                "QTY",
                "REF",
                "SBR",
                "SE",
                "ST",
                "SV1",
                "SV2",
                "SV3",
                "SV4",
                "SV5",
                "SV6",
                "SV7",
                "SVD",
                "TOO",
                "UR"}, "Inbound");

            var parser = new X12Parser();

            foreach (var resource in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                if (resource.StartsWith("OopFactory.X12.Tests.Unit.Parsing._SampleEdiFiles") && !resource.EndsWith(".xml"))
                {
                    var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

                    try
                    {
                        var interchanges = parser.ParseMultiple(stream);
                        foreach (var interchange in interchanges)
                        {
                            repo.Save(interchange, resource, "dstrubhar");
                        }
                    }
                    catch (Exception exc)
                    {
                        Trace.WriteLine(resource);
                        Trace.WriteLine(exc.Message);
                    }
                }
            }
        }
    }
}
