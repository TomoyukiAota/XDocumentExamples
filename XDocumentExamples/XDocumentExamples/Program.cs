using System.Xml.Linq;

namespace XDocumentExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            //XDocumentGeneration();
            XDocumentManipulation();
        }

        private static void XDocumentManipulation()
        {
            var xDocument = new XDocument();
            var root = new XElement("Root");
            var a = new XElement("a");
            var b = new XElement("b", new XText("value"));

            root.Add(a, b);
            xDocument.Add(root);
            XDocumentUtilities.DisplayInConsole(xDocument, "Add Element");
            XDocumentUtilities.SaveAsXmlFile(xDocument, "Add Element");
        }

        private static void XDocumentGeneration()
        {
            var xDocument = new XDocument(
                new XDeclaration(version: "1.0", encoding: "utf-8", standalone: null),
                new XElement("ElementA",
                    new XAttribute("Attribute", "AttributeValue"),
                    new XElement("ElementB",
                        new XText("ElementB_Text"),
                        new XCData("This is CData section not recognized as XML document.")
                    )));

            XDocumentUtilities.DisplayInConsole(xDocument, "XDocument-Generation");
            XDocumentUtilities.SaveAsXmlFile(xDocument, "XDocument-Generation");
        }
    }
}
