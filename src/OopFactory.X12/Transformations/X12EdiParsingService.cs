using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Model;

namespace OopFactory.X12.Transformations
{
    public class X12EdiParsingService : ITransformationService
    {
        private bool _suppressComments;
        private X12Parser _parser;

        public X12EdiParsingService(bool suppressComments, X12Parser parser)
        {
            _suppressComments = suppressComments;
            _parser = parser;
        }

        public X12EdiParsingService(bool suppressComments) : this(suppressComments, new X12Parser()) {}

        public X12EdiParsingService(bool suppressComments, ISpecificationFinder specFinder) : this(suppressComments, new X12Parser(specFinder, true)) {}

        public string Transform(string x12)
        {
            Interchange interchange = _parser.ParseMultiple(new MemoryStream(Encoding.ASCII.GetBytes(x12))).FirstOrDefault();
            return interchange.Serialize(_suppressComments);
        }
        
    }
}
