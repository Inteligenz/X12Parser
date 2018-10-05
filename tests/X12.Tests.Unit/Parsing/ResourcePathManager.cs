namespace X12.Tests.Unit.Parsing
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.XPath;

    /// <summary>
    /// Manages a sample EDI index file and provides a collection of resource paths for testing
    /// </summary>
    public static class ResourcePathManager
    {
        private static readonly string SampleFilesIndex = @"SampleEdiFileInventory.xml";

        /// <summary>
        /// Initializes static members of the <see cref="ResourcePathManager"/> class
        /// </summary>
        static ResourcePathManager()
        {
            ResourcePaths = new List<string>();
            QueryMap = new Dictionary<string, IDictionary<string, string>>();
            ExpectedValuesMap = new Dictionary<string, IDictionary<string, string>>();

            Stream stream = Extensions.GetEdi(SampleFilesIndex);
            using (XmlReader reader = XmlReader.Create(stream, new XmlReaderSettings()))
            {
                var document = new XPathDocument(reader);
                var navigator = document.CreateNavigator();

                XPathNodeIterator sampleFiles = navigator.Select("/SampleEdiFiles/TransactionSet/EdiFile");
                while (sampleFiles.MoveNext())
                {
                    var currentNavigator = sampleFiles.Current;
                    currentNavigator.MoveToFirstChild();

                    string resourcePath = string.Empty;
                    Dictionary<string, string> queries = new Dictionary<string, string>();
                    Dictionary<string, string> expectedValues = new Dictionary<string, string>();

                    do
                    {
                        if (currentNavigator.Name == "ResourcePath")
                        {
                            resourcePath = currentNavigator.Value;
                            continue;
                        }

                        if (currentNavigator.Name == "SourceUrl")
                        {
                            continue;
                        }

                        // Using an arbitrary value for the loop stop
                        for (int i = 0; i < 5; i++)
                        {
                            if (currentNavigator.Name == $"Query{i}")
                            {
                                queries.Add($"Query{i}", currentNavigator.Value);
                                break;
                            }

                            if (currentNavigator.Name == $"Expected{i}")
                            {
                                expectedValues.Add($"Expected{i}", currentNavigator.Value);
                                break;
                            }
                        }
                    }
                    while (currentNavigator.MoveToNext());

                    if (!string.IsNullOrEmpty(resourcePath))
                    {
                        ResourcePaths.Add(resourcePath);
                        QueryMap.Add(resourcePath, queries);
                        ExpectedValuesMap.Add(resourcePath, expectedValues);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the collection of resource path strings to sample EDI files for testing
        /// </summary>
        public static IList<string> ResourcePaths { get; }

        /// <summary>
        /// Gets the collection of queries, grouped by resource strings
        /// </summary>
        public static IDictionary<string, IDictionary<string, string>> QueryMap { get; }

        /// <summary>
        /// Gets the collection of expected values, grouped by resource strings
        /// </summary>
        public static IDictionary<string, IDictionary<string, string>> ExpectedValuesMap { get; }
    }
}
