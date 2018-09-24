namespace X12.Tests.Integration.Repositories
{
    using System.Reflection;

    using NUnit.Framework;

    using X12.Parsing;
    using X12.Specifications.Finders;
    using X12.Sql;

    [TestFixture, Ignore("Database tests have issues with authentication, SetUp, and TearDown")]
    public class LoadInterchangeTester
    {
        [Test]
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
                "Data Source=localhost;Initial Catalog=X12;Integrated Security=True",
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
                    
                    var interchanges = parser.ParseMultiple(stream);
                    foreach (var interchange in interchanges)
                    {
                        repo.Save(interchange, resource, "dstrubhar");
                    }
                }
            }
        }
    }
}
