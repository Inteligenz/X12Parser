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
    internal static class EmbeddedResources
    {
        private static Dictionary<string, SegmentSpecification> _4010Specification;
        internal static Dictionary<string, SegmentSpecification> Get4010Spec()
        {
            if (_4010Specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                SegmentSet set = SegmentSet.Deserialize(reader.ReadToEnd());
                _4010Specification = new Dictionary<string, SegmentSpecification>();
                foreach (var segment in set.Segments)
                    _4010Specification.Add(segment.SegmentId, segment);
            }
            return _4010Specification;
        }


        private static TransactionSpecification _270specification;

        internal static TransactionSpecification Get270TransactionSpecification()
        {
            if (_270specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-270-5010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _270specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _270specification;
        }

        private static TransactionSpecification _276specification;

        internal static TransactionSpecification Get276TransactionSpecification()
        {
            if (_276specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-276-5010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _276specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _276specification;
        }

        private static TransactionSpecification _997specification;

        internal static TransactionSpecification Get997TransactionSpecification()
        {
            if (_997specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-997-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _997specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _997specification;
        }

        private static TransactionSpecification _834specification;

        internal static TransactionSpecification Get834TransactionSpecification()
        {
            if (_834specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-834-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _834specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _834specification;
        }

        private static TransactionSpecification _837specification;

        internal static TransactionSpecification Get837TransactionSpecification()
        {
            if (_837specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-837-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _837specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _837specification;
        }

        private static TransactionSpecification _837_5010specification;

        internal static TransactionSpecification Get837_5010TransactionSpecification()
        {
            if (_837_5010specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-837-5010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _837_5010specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _837_5010specification;
        }

        private static XslCompiledTransform _837Transform;

        internal static XslCompiledTransform Get837Transform()
        {
            if (_837Transform == null)
            {
                WriteToFile("OopFactory.X12.Transformations.Ansi-Common.xslt", Environment.CurrentDirectory + "\\Ansi-Common.xslt");

                var xsltReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.Ansi-837-To-Claim.xslt"));
                _837Transform = new XslCompiledTransform();
                _837Transform.Load(xsltReader);
            }
            return _837Transform;
        }

        private static TransactionSpecification _835specification;

        internal static TransactionSpecification Get835TransactionSpecification()
        {
            if (_835specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-835-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _835specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _835specification;
        }

        private static XslCompiledTransform _835Transform;

        internal static XslCompiledTransform Get835Transform()
        {
            if (_835Transform == null)
            {                
                _835Transform = new XslCompiledTransform();

                WriteToFile("OopFactory.X12.Transformations.Ansi-Common.xslt", Environment.CurrentDirectory + "\\Ansi-Common.xslt");

                var xsltReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.Ansi-835-To-Payment.xslt"));
                
                _835Transform.Load(xsltReader);
                
            }
            return _835Transform;
        }

        private static TransactionSpecification _856specification;

        internal static TransactionSpecification Get856TransactionSpecification()
        {
            if (_856specification == null)
            {
                Stream specStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Specifications.Ansi-856-4010Specification.xml");
                StreamReader reader = new StreamReader(specStream);
                _856specification = TransactionSpecification.Deserialize(reader.ReadToEnd());
            }
            return _856specification;
        }

        private static XslCompiledTransform _856Transform;

        internal static XslCompiledTransform Get856Transform()
        {
            if (_856Transform == null)
            {
                _856Transform = new XslCompiledTransform();

                WriteToFile("OopFactory.X12.Transformations.Ansi-Common.xslt", Environment.CurrentDirectory + "\\Ansi-Common.xslt");

                var xsltReader = XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.Ansi-856-To-ShipNotice.xslt"));

                _856Transform.Load(xsltReader);
            }
            return _856Transform;
        }

        private static void WriteToFile(string resourceName, string fileName)
        {
            StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName));
            FileStream fs = new FileStream(fileName, FileMode.Create);
            StreamWriter writer = new StreamWriter(fs);
            writer.Write(reader.ReadToEnd());
            writer.Close();
            fs.Close();
        }
    }
}
