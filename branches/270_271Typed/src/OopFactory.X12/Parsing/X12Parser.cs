using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Reflection;
using OopFactory.X12.Parsing.Model;
using OopFactory.X12.Parsing.Specification;

namespace OopFactory.X12.Parsing
{
    public class X12Parser
    {
        private ISpecificationFinder _specFinder;
        private bool _throwExceptionOnSyntaxErrors;
        private char[] _ignoredChars;

        public X12Parser(ISpecificationFinder specFinder, bool throwExceptionOnSyntaxErrors, char[] ignoredChars)
        {
            _specFinder = specFinder;
            _throwExceptionOnSyntaxErrors = throwExceptionOnSyntaxErrors;
            _ignoredChars = ignoredChars;
        }

        public X12Parser(ISpecificationFinder specFinder, bool throwExceptionOnSyntaxErrors)
            : this(specFinder, throwExceptionOnSyntaxErrors, new char[] { })
        {
        }

        public X12Parser(ISpecificationFinder specFinder)
            : this(specFinder, true, new char[] {})
        {
        }

        public X12Parser(bool throwExceptionsOnSyntaxErrors)
            : this(new SpecificationFinder(), throwExceptionsOnSyntaxErrors, new char[] { })
        {
        }

        public X12Parser()
            : this(new SpecificationFinder(), true, new char[] { })
        {

        }

        public delegate void X12ParserWarningEventHandler(object sender, X12ParserWarningEventArgs args);

        public event X12ParserWarningEventHandler ParserWarning;

        private void OnParserWarning(X12ParserWarningEventArgs args)
        {
            if (this.ParserWarning != null)
                ParserWarning(this, args);
        }

        [Obsolete("Use ParseMultiple instead.  Parse will only return the first interchange in the file.")]
        public Interchange Parse(string x12)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(x12);
            using (MemoryStream mstream = new MemoryStream(byteArray))
            {
                return Parse(mstream);
            }
        }

        [Obsolete("Use ParseMultiple instead.  Parse will only return the first interchange in the file.")]
        public Interchange Parse(Stream stream)
        {
            var interchanges = ParseMultiple(stream);
            if (interchanges.Count > 1)
                throw new ApplicationException("Your file contains more than one interchange, you must use ParseMultiple instead of Parse to get all the records in this file.");
            return interchanges.FirstOrDefault();
        }

        public List<Interchange> ParseMultiple(string x12)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(x12);
            using (MemoryStream mstream = new MemoryStream(byteArray))
            {
                return ParseMultiple(mstream);
            }
        }

        public List<Interchange> ParseMultiple(Stream stream)
        {
            return ParseMultiple(stream, Encoding.UTF8);
        }

        public List<Interchange> ParseMultiple(Stream stream, Encoding encoding)
        {
            var envelopes = new List<Interchange>();

            using (X12StreamReader reader = new X12StreamReader(stream, encoding, _ignoredChars))
            {
                Interchange envelop = new Interchange(_specFinder, reader.CurrentIsaSegment);
                envelopes.Add(envelop);
                Container currentContainer = envelop;
                FunctionGroup fg = null;
                Transaction tr = null;
                Dictionary<string, HierarchicalLoop> hloops = new Dictionary<string, HierarchicalLoop>();

                string segmentString = reader.ReadNextSegment();
                string segmentId = reader.ReadSegmentId(segmentString);
                int segmentIndex = 1;
                Stack<string> containerStack = new Stack<string>();
                while (segmentString.Length > 0)
                {
                    switch (segmentId)
                    {
                        case "ISA":
                            envelop = new Interchange(_specFinder, segmentString + reader.Delimiters.SegmentTerminator);
                            envelopes.Add(envelop);
                            currentContainer = envelop;
                            fg = null;
                            tr = null;
                            hloops = new Dictionary<string, HierarchicalLoop>();
                            break;
                        case "IEA":
                            if (envelop == null)
                                throw new InvalidOperationException(string.Format("Segment {0} does not have a matching ISA segment preceding it.", segmentString));
                            envelop.SetTerminatingTrailerSegment(segmentString);
                            break;
                        case "GS":
                            if (envelop == null)
                                throw new InvalidOperationException(String.Format("Segment '{0}' cannot occur before and ISA segment.", segmentString));

                            currentContainer = fg = envelop.AddFunctionGroup(segmentString); ;
                            break;
                        case "GE":
                            if (fg == null)
                                throw new InvalidOperationException(String.Format("Segment '{0}' does not have a matching GS segment precending it.", segmentString));
                            fg.SetTerminatingTrailerSegment(segmentString);
                            fg = null;
                            break;
                        case "ST":
                            if (fg == null)
                                throw new InvalidOperationException(string.Format("segment '{0}' cannot occur without a preceding GS segment.", segmentString));
                            segmentIndex = 1;
                            currentContainer = tr = fg.AddTransaction(segmentString);
                            hloops = new Dictionary<string, HierarchicalLoop>();
                            break;
                        case "SE":
                            if (tr == null)
                                throw new InvalidOperationException(string.Format("Segment '{0}' does not have a matching ST segment preceding it.", segmentString));

                            tr.SetTerminatingTrailerSegment(segmentString);
                            tr = null;
                            break;
                        case "HL":
                            Segment hlSegment = new Segment(null, reader.Delimiters, segmentString);
                            string id = hlSegment.GetElement(1);
                            string parentId = hlSegment.GetElement(2);
                            string levelCode = hlSegment.GetElement(3);

                            while (!(currentContainer is HierarchicalLoopContainer) || !((HierarchicalLoopContainer)currentContainer).AllowsHierarchicalLoop(levelCode))
                            {
                                if (currentContainer.Parent != null)
                                    currentContainer = currentContainer.Parent;
                                else
                                    throw new InvalidOperationException(String.Format("Heierchical Loop {0}  cannot be added to transaction set {1} because it's specification cannot be identified.", segmentString, tr.ControlNumber));
                            }
                            bool parentFound = false;
                            if (parentId != "")
                            {
                                if (hloops.ContainsKey(parentId))
                                {
                                    parentFound = true;
                                    currentContainer = hloops[parentId].AddHLoop(segmentString);
                                }
                                else
                                {
                                    if (_throwExceptionOnSyntaxErrors)
                                    {
                                        throw new InvalidOperationException(String.Format("Hierarchical Loop {0} expects Parent ID {1} which did not occur preceding it.  To change this to a warning, pass throwExceptionOnSyntaxErrors = false to the X12Parser constructor.", id, parentId));
                                    }
                                    else
                                    {
                                        OnParserWarning(new X12ParserWarningEventArgs
                                        {
                                            FileIsValid = false,
                                            InterchangeControlNumber = envelop.InterchangeControlNumber,
                                            FunctionalGroupControlNumber = fg.ControlNumber,
                                            TransactionControlNumber = tr.ControlNumber,
                                            SegmentPositionInInterchange = segmentIndex,
                                            Segment = segmentString,
                                            SegmentId = segmentId,
                                            Message = String.Format("Hierarchical Loop {0} expects Parent ID {1} which did not occur preceding it.  This will be parsed as if it has no parent, but the file may not be valid.", id, parentId)
                                        });
                                    }
                                }
                            }

                            if (parentId == "" || !parentFound)
                            {
                                while (!(currentContainer is HierarchicalLoopContainer && ((HierarchicalLoopContainer)currentContainer).HasHierachicalSpecs()))
                                {
                                    currentContainer = currentContainer.Parent;
                                }
                                currentContainer = ((HierarchicalLoopContainer)currentContainer).AddHLoop(segmentString);
                                //hloops = new Dictionary<string, HierarchicalLoop>();
                            }
                            if (hloops.ContainsKey(id))
                                throw new InvalidOperationException(String.Format("Hierarchical Loop {0} cannot be added to transaction {1} because the ID {2} already exists.", segmentString, tr.ControlNumber, id));
                            hloops.Add(id, (HierarchicalLoop)currentContainer);
                            break;
                        case "TA1": // Technical acknowledgement
                            if (envelop == null)
                                throw new InvalidOperationException(string.Format("Segment {0} does not have a matching ISA segment preceding it.", segmentString));
                            envelop.AddSegment(segmentString);
                            break;  
                        default:
                            var originalContainer = currentContainer;
                            containerStack.Clear();
                            while (currentContainer != null)
                            {
                                if (currentContainer.AddSegment(segmentString) != null)
                                {
                                    if (segmentId == "LE")
                                        currentContainer = currentContainer.Parent;
                                    break;
                                }
                                else
                                {
                                    if (currentContainer is LoopContainer)
                                    {
                                        LoopContainer loopContainer = (LoopContainer)currentContainer;

                                        Loop newLoop = loopContainer.AddLoop(segmentString);
                                        if (newLoop != null)
                                        {
                                            currentContainer = newLoop;
                                            break;
                                        }
                                        else
                                        {
                                            if (currentContainer is Transaction)
                                            {
                                                var tran = (Transaction)currentContainer;

                                                if (_throwExceptionOnSyntaxErrors)
                                                {
                                                    throw new TransactionValidationException(
                                                        "Segment '{3}' in segment position {4} within transaction '{1}' cannot be identified within the supplied specification for transaction set {0} in any of the expected loops: {5}.  To change this to a warning, pass throwExceptionOnSyntaxErrors = false to the X12Parser constructor.", tran.IdentifierCode, tran.ControlNumber, "", segmentString, segmentIndex, string.Join(",", containerStack));
                                                }
                                                else
                                                {
                                                    currentContainer = originalContainer;
                                                    currentContainer.AddSegment(segmentString, true);
                                                    OnParserWarning(new X12ParserWarningEventArgs
                                                    {
                                                        FileIsValid = false,
                                                        InterchangeControlNumber = envelop.InterchangeControlNumber,
                                                        FunctionalGroupControlNumber = fg.ControlNumber,
                                                        TransactionControlNumber = tran.ControlNumber,
                                                        SegmentPositionInInterchange = segmentIndex,
                                                        SegmentId = segmentId,
                                                        Segment = segmentString,
                                                        Message = string.Format("Segment '{3}' in segment position {4} within transaction '{1}' cannot be identified within the supplied specification for transaction set {0} in any of the expected loops: {5}.  It will be added to loop {6}, but this may invalidate all subsequent segments.", tran.IdentifierCode, tran.ControlNumber, "", segmentString, segmentIndex, string.Join(",", containerStack), containerStack.LastOrDefault())
                                                    });
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (currentContainer is Loop)
                                                    containerStack.Push(((Loop)currentContainer).Specification.LoopId);
                                                if (currentContainer is HierarchicalLoop)
                                                {
                                                    var hloop = ((HierarchicalLoop)currentContainer);
                                                    containerStack.Push(string.Format("{0}[{1}]", hloop.Specification.LoopId, hloop.Id));
                                                }

                                                currentContainer = currentContainer.Parent;
                                                continue;
                                            }
                                        }
                                    }
                                    else
                                        break;
                                }
                            }
                            break;

                    }
                    segmentString = reader.ReadNextSegment();
                    segmentId = reader.ReadSegmentId(segmentString);
                    segmentIndex++;
                }
                return envelopes;
            }
        }

        public string TransformToX12(string xml)
        {
            var transform = new XslCompiledTransform();
            transform.Load(XmlReader.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream("OopFactory.X12.Transformations.X12-XML-to-X12.xslt")));

            using (var writer = new StringWriter())
            {
                transform.Transform(XmlReader.Create(new StringReader(xml)), new XsltArgumentList(), writer);
                return writer.GetStringBuilder().ToString();
            }
        }

        public List<Interchange> UnbundleByLoop(Interchange interchange, string loopId)
        {
            char terminator = interchange._delimiters.SegmentTerminator;
            var service = new UnbundlingService(interchange._delimiters.SegmentTerminator);
            string isa = interchange.SegmentString;
            string iea = interchange.TrailerSegments.First().SegmentString;
            List<string> list = new List<string>();
            foreach (FunctionGroup group in interchange.FunctionGroups)
                foreach (Transaction transaction in group.Transactions)
                {
                    service.UnbundleHLoops(list, transaction, loopId);
                }

            List<Interchange> interchanges = new List<Interchange>();
            foreach (var item in list)
            {
                StringBuilder x12 = new StringBuilder();
                x12.AppendFormat("{0}{1}", isa, terminator);
                x12.Append(item);
                x12.AppendFormat("{0}{1}", iea, terminator);
                using (MemoryStream mstream = new MemoryStream(Encoding.ASCII.GetBytes(x12.ToString())))
                {
                    interchanges.AddRange(ParseMultiple(mstream));
                }
            }
            return interchanges;
        }

        public List<Interchange> UnbundleByTransaction(Interchange interchange)
        {
            List<Interchange> interchanges = new List<Interchange>();

            char terminator = interchange._delimiters.SegmentTerminator;
            var service = new UnbundlingService(interchange._delimiters.SegmentTerminator);
            string isa = interchange.SegmentString;
            string iea = interchange.TrailerSegments.First().SegmentString;
            List<string> list = new List<string>();
            foreach (FunctionGroup group in interchange.FunctionGroups)
            {
                foreach (Transaction transaction in group.Transactions)
                {
                    StringBuilder x12 = new StringBuilder();
                    x12.AppendFormat("{0}{1}", isa, terminator);
                    x12.AppendFormat("{0}{1}", group.SegmentString, terminator);
                    x12.Append(transaction.SerializeToX12(false));
                    x12.AppendFormat("{0}{1}", group.TrailerSegments.First().SegmentString, terminator);
                    x12.AppendFormat("{0}{1}", iea, terminator);
                    using (MemoryStream mstream = new MemoryStream(Encoding.ASCII.GetBytes(x12.ToString())))
                    {
                        interchanges.AddRange(ParseMultiple(mstream));
                    }
                }
            }

            return interchanges;
        }
    }
}
