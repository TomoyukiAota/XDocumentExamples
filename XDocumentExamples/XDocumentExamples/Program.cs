using System;
using System.Linq;
using System.Xml.Linq;

namespace XDocumentExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayLineAndCall(ReadXDocument);
            DisplayLineAndCall(GenerationXDocument);
            DisplayLineAndCall(ManipulateXDocument);
        }

        private static void DisplayLineAndCall(Action action)
        {
            Console.WriteLine("---------------------------------");
            action();
        }

        private static void ReadXDocument()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                <ROOT>
                                <A />
                                <B />
                                <B>
                                  <C>
                                    <D>text-d</D>
                                    <E>text-e</E>
                                    <E><N x=""y"" /></E>
                                  </C>
                                  <F><G /></F>
                                  <H>text-h</H>
                                  Text of B
                                </B>
                                </ROOT>";

            var xDocument = XDocument.Parse(xml);
            XDocumentUtilities.DisplayInConsole(xDocument, title: "XDocument to be read");

            var c = xDocument.Element("ROOT").Elements("B").Where(b => !b.IsEmpty).Elements("C");

            var d = c.Elements("D").ToList()[0];
            var dName = d.Name.LocalName;
            var dValue = d.Value;
            Console.WriteLine($"For {d}, name is {dName} and value is {dValue}");

            var n = c.Elements("E").ToList()[1].Element("N");
            var attributeName = n.Attribute("x").Name.LocalName;
            var attributeValue = n.Attribute("x").Value;
            Console.WriteLine($"For the attribute of {n}, name is {attributeName} and value is {attributeValue}");
        }

        private static void ManipulateXDocument()
        {
            var xDocument = new XDocument();
            var root = new XElement("Root");
            var a = new XElement("a");
            var b = new XElement("b", new XText("value"));

            root.Add(a, b);
            xDocument.Add(root);
            XDocumentUtilities.DisplayInConsole(xDocument, title: "Add Element");
            XDocumentUtilities.SaveAsXmlFile(xDocument, title: "Add Element");
        }

        private static void GenerationXDocument()
        {
            var xDocument = new XDocument(
                new XDeclaration(version: "1.0", encoding: "utf-8", standalone: null),
                new XElement("ElementA",
                    new XAttribute("Attribute", "AttributeValue"),
                    new XElement("ElementB",
                        new XText("ElementB_Text"),
                        new XCData("This is CData section not recognized as XML document.")
                    )));

            XDocumentUtilities.DisplayInConsole(xDocument, title: "XDocument-Generation");
            XDocumentUtilities.SaveAsXmlFile(xDocument, title: "XDocument-Generation");
        }
    }
}
