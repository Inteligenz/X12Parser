using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopFactory.X12.Parsing;
using OopFactory.X12.Repositories;
using System.Reflection;
using System.Diagnostics;

namespace OopFactory.X12.Tests.Integration.Repositories
{
    [TestClass, Ignore]
    public class LoadInterchangeTester
    {
        [TestMethod]
        public void LoadAllTestFiles()
        {

            var repo = new SqlTransactionRepository<long>("Data Source=127.0.0.1;Initial Catalog=X12;Integrated Security=True", new SpecificationFinder(),
                new string[] {"AMT",
                "BHT",
                "CAS",
                "CL1",
                "CLM",
                "CN1",
                "DMG",
                "DN1",
                "DTP",
                "HCP",
                "HI",
                "HL",
                "K3",
                "LX",
                "MEA",
                "N3",
                "N4",
                "NM1",
                "NTE",
                "OI",
                "PAT",
                "PER",
                "PRV",
                "PWK",
                "QTY",
                "REF",
                "SBR",
                "SV1",
                "SV2",
                "SV3",
                "SVD",
                "TOO",}, "Inbound");

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
