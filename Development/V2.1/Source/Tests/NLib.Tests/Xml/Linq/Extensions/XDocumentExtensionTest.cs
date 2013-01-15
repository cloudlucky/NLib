namespace NLib.Tests.Xml.Linq.Extensions
{
    using System.Xml.Linq;

    using NLib.Xml.Linq.Extensions;

    using Xunit;

    public class XDocumentExtensionTest
    {
        [Fact]
        public void ToXmlDocumentTest1()
        {
            var xmlDoc = new XDocument(new XElement("data"));

            var xdoc = xmlDoc.ToXmlDocument();

            Assert.Equal(xdoc.InnerXml, "<data />");
        }

        [Fact]
        public void ToXmldocumentTest2()
        {
            var doc = new XDocument(new XElement("parent", new XElement("child", new XCData("text1"))));

            var xdoc = doc.ToXmlDocument();
            var child = xdoc.FirstChild.FirstChild;

            Assert.Equal(child.InnerText, "text1");
        }
    }
}
