namespace NLib.Tests.Xml.Extensions
{
    using System.Xml;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Xml.Extensions;

    [TestClass]
    public class XDocumentExtensionTest
    {
        [TestMethod]
        public void ToXdocumentTest1()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.LoadXml("<data></data>");

            var xdoc = xmlDoc.ToXDocument();

            Assert.AreEqual(xdoc.ToString(), "<data></data>");
        }

        [TestMethod]
        public void ToXdocumentTest2()
        {
            var doc = new XmlDocument();
            
            doc.LoadXml("<parent><child>text1</child></parent>");
            
            var xdoc = doc.ToXDocument();
            var children = xdoc.Document.Element("parent").Elements("child");
            
            foreach (var child in children)
            {
                Assert.AreEqual(child.Value, "text1");
            }
        }
    }
}