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

        public X12EdiParsingService(bool suppressComments)
        {
            _parser = new X12Parser();
            _suppressComments = suppressComments;
        }

        public X12EdiParsingService(bool suppressComments, ISpecificationFinder specFinder)
        {
            _parser = new X12Parser(specFinder);
        }

        public string Transform(string x12)
        {
            Interchange interchange = _parser.Parse(new MemoryStream(Encoding.ASCII.GetBytes(x12)));
            return interchange.Serialize(_suppressComments);
        }
        
    }
}
