namespace NLib.Tests.Xml.Linq.Extensions
{
    using System.Xml.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Xml.Linq.Extensions;

    [TestClass]
    public class XDocumentExtensionTest
    {
        [TestMethod]
        public void ToXmlDocumentTest1()
        {
            var xmlDoc = new XDocument(new XElement("data"));

            var xdoc = xmlDoc.ToXmlDocument();

            Assert.AreEqual(xdoc.InnerXml, "<data />");
        }

        [TestMethod]
        public void ToXmldocumentTest2()
        {
            var doc = new XDocument(new XElement("parent", new XElement("child", new XCData("text1"))));

            var xdoc = doc.ToXmlDocument();
            var child = xdoc.FirstChild.FirstChild;

            Assert.AreEqual(child.InnerText, "text1");
        }
    }
}
