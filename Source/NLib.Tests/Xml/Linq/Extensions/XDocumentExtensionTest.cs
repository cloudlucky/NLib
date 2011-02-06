namespace NLib.Tests.Xml.Linq.Extensions
{
    using System.Xml;
    using System.Xml.Linq;

    using NLib.Xml.Linq.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class XDocumentExtensionTest
    {
        [Test]
        public void ToXmlDocumentTest1()
        {
            var xmlDoc = new XDocument(new XElement("data"));

            var xdoc = xmlDoc.ToXmlDocument();

            Assert.AreEqual(xdoc.InnerXml, "<data />");
        }

        [Test]
        public void ToXmldocumentTest2()
        {
            var doc = new XDocument(new XElement("parent", new XElement("child", new XCData("text1"))));

            var xdoc = doc.ToXmlDocument();
            var child = xdoc.FirstChild.FirstChild;

            Assert.AreEqual(child.InnerText, "text1");
        }
    }
}
