namespace OopFactory.X12.Parsing
{
    using System.IO;
    using System.Linq;
    using System.Text;
    
    using OopFactory.X12.Shared.Models;
    using OopFactory.X12.Specifications.Interfaces;
    using OopFactory.X12.Transformations;

    /// <summary>
    /// Transformation service which provides X12 to XML transformation
    /// </summary>
    public class X12EdiParsingService : ITransformationService
    {
        private readonly bool suppressComments;
        private readonly X12Parser parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="X12EdiParsingService"/> class
        /// </summary>
        /// <param name="suppressComments">Indicates whether error comments should be suppressed</param>
        /// <param name="parser">X12 parser used for parsing the document</param>
        public X12EdiParsingService(bool suppressComments, X12Parser parser)
        {
            this.suppressComments = suppressComments;
            this.parser = parser;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X12EdiParsingService"/> class
        /// </summary>
        /// <param name="suppressComments">Indicates whether error comments should be suppressed</param>
        public X12EdiParsingService(bool suppressComments)
            : this(suppressComments, new X12Parser())
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="X12EdiParsingService"/> class
        /// </summary>
        /// <param name="suppressComments">Indicates whether error comments should be suppressed</param>
        /// <param name="specFinder">Specification finder for the EDI file being parsed</param>
        public X12EdiParsingService(bool suppressComments, ISpecificationFinder specFinder)
            : this(suppressComments, new X12Parser(specFinder, true))
        {
        }

        /// <summary>
        /// Transforms the X12 string into an XML string
        /// </summary>
        /// <param name="x12">X12 data to be transformed</param>
        /// <returns>XML string from parsed X12 data</returns>
        public string Transform(string x12)
        {
            Interchange interchange = this.parser.ParseMultiple(new MemoryStream(Encoding.ASCII.GetBytes(x12))).FirstOrDefault();
            return interchange?.Serialize(this.suppressComments);
        }
    }
}
