using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace XDocumentExamples
{
    /// <summary>
    /// Utility class for <see cref="XDocument"/>.
    /// </summary>
    /// <remarks>
    /// The content of <see cref="XDocument"/> object must be a valid XML document.
    /// e.g. the XML document must contain a single root node.
    /// </remarks>
    public class XDocumentUtilities
    {
        /// <summary>
        /// Display <see cref="XDocument"/> object in <see cref="Console"/> including XML declaration.
        /// </summary>
        public static void DisplayInConsole(XDocument xDocument, string title = "")
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine($"Title: {title}");
            }

            using (var writer = new Utf8StringWriter())
            {
                xDocument.Save(writer);
                Console.WriteLine(writer);
            }
        }

        private static readonly string SaveDirectory = @"../../../GeneratedXmlFiles";

        /// <summary>
        /// Save <see cref="XDocument"/> object as a XML file in the directory specified in <see cref="SaveDirectory"/>.
        /// </summary>
        public static void SaveAsXmlFile(XDocument xDocument, string title)
        {
            Directory.CreateDirectory(SaveDirectory);
            var now = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
            var path = $@"{SaveDirectory}/{now}_{title}.xml";
            xDocument.Save(path);
        }
    }
}