using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using OopFactory.X12.Parsing;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12
{
    [Obsolete("Use OopFactory.X12.Parsing.X12Parser directly instead")]
    public class X12ParsingService
    {
        private bool _verbose;

        public X12ParsingService(bool verbose)
        {
            _verbose = verbose;
        }

    [Obsolete("Use OopFactory.X12.Parsing.X12Parser directly instead")]
        public string ParseToXml(string rawX12)
        {
            return ParseToXml(new MemoryStream(ASCIIEncoding.Default.GetBytes(rawX12)));
        }

    [Obsolete("Use OopFactory.X12.Parsing.X12Parser directly instead")]

        public string ParseToXml(Stream stream)
        {
            return new X12Parser().Parse(stream).Serialize();
        }

    [Obsolete("Use OopFactory.X12.Parsing.X12Parser directly instead")]
    public string ParseToDomainXml(string rawX12)
        {
            return ParseToDomainXml(new MemoryStream(ASCIIEncoding.Default.GetBytes(rawX12)));
        }
    [Obsolete("Use OopFactory.X12.Parsing.X12Parser directly instead")]
    public string ParseToDomainXml(Stream stream)
        {
            throw new NotSupportedException();
        }
    }
}
