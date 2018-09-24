namespace X12.Tests.Integration.Repositories
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using X12.Parsing;
    using X12.Specifications.Finders;
    using X12.Sql;

    [TestClass, Ignore]
    public class LoadInterchangeTester
    {
        [TestMethod]
        public void LoadAllTestFiles()
        {
            var indexedSegments = new[]
                                      {
                                          "AMT",
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
                                          "TOO"
                                      };

            var repo = new SqlTransactionRepository(
                "Data Source=127.0.0.1;Initial Catalog=X12;Integrated Security=True",
                new SpecificationFinder(),
                indexedSegments,
                typeof(long),
                "Inbound");

            var parser = new X12Parser();

            foreach (var resource in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                if (resource.StartsWith("X12.Tests.Unit.Parsing._SampleEdiFiles") && !resource.EndsWith(".xml"))
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
