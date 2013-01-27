namespace NLib.Tests.Xml.Extensions
{
    using System.Xml;

    using NLib.Xml.Extensions;

    using Xunit;

    public class XDocumentExtensionsTest
    {
        [Fact]
        public void ToXdocumentTest1()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.LoadXml("<data></data>");

            var xdoc = xmlDoc.ToXDocument();

            Assert.Equal(xdoc.ToString(), "<data></data>");
        }

        [Fact]
        public void ToXdocumentTest2()
        {
            var doc = new XmlDocument();
            
            doc.LoadXml("<parent><child>text1</child></parent>");
            
            var xdoc = doc.ToXDocument();
            var children = xdoc.Document.Element("parent").Elements("child");
            
            foreach (var child in children)
            {
                Assert.Equal(child.Value, "text1");
            }
        }
    }
}