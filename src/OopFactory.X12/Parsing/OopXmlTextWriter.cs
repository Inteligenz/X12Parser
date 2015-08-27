using System.IO;
using System.Text;
using System.Xml;

namespace OopFactory.X12.Parsing
{
    public class NoCommentXmlTextWriter : XmlTextWriter
    {
        /// <summary>
        /// Creates an instance of the XmlTextWriter class using the specified stream and encoding.
        /// </summary>
        /// <param name="w"></param>
        /// <param name="encoding"></param>
        public NoCommentXmlTextWriter(Stream w, Encoding encoding)
            : base(w, encoding)
        {
        }
        /// <summary>
        /// Creates an instance of the XmlTextWriter class using the specified file.
        /// </summary>
        /// <param name="w"></param>
        /// <param name="encoding"></param>
        public NoCommentXmlTextWriter(string fileName, Encoding encoding)
            : base(fileName, encoding)
        {
        }
        /// <summary>
        /// Creates an instance of the XmlTextWriter class using the specified TextWriter.
        /// </summary>
        /// <param name="tw"></param>
        public NoCommentXmlTextWriter(TextWriter tw)
            : base(tw)
        {
        }
        /// <summary>
        /// Writes out a comment <!--...--> containing the specified text if the writer is not set to SuppressComments. (Overrides XmlWriter.WriteComment(String).)
        /// </summary>
        /// <param name="text"></param>
        public override void WriteComment(string text)
        {
            //Do nothing!
        }
    }
}
