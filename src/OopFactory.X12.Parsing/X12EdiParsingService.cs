namespace OopFactory.X12.Parsing
{
    using System.IO;
    using System.Linq;
    using System.Text;
    
    using OopFactory.X12.Shared.Models;
    using OopFactory.X12.Specifications.Interfaces;
    using OopFactory.X12.Transformations;

    public class X12EdiParsingService : ITransformationService
    {
        private readonly bool suppressComments;
        private readonly X12Parser parser;

        public X12EdiParsingService(bool suppressComments, X12Parser parser)
        {
            this.suppressComments = suppressComments;
            this.parser = parser;
        }

        public X12EdiParsingService(bool suppressComments) : this(suppressComments, new X12Parser())
        {
        }

        public X12EdiParsingService(bool suppressComments, ISpecificationFinder specFinder) : this(suppressComments, new X12Parser(specFinder, true))
        {
        }

        public string Transform(string x12)
        {
            Interchange interchange = this.parser.ParseMultiple(new MemoryStream(Encoding.ASCII.GetBytes(x12))).FirstOrDefault();
            return interchange.Serialize(this.suppressComments);
        }
        
    }
}
