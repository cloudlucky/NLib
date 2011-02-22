namespace NLib.Tests.Xml.Extensions
{
    using NLib;
    using NLib.Extensions;
    using NLib.Xml.Extensions;
    using System;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class XDocumentExtensionTest
    {
        [Test]
        public void ToXdocumentTest1()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.LoadXml("<data></data>");

            var xdoc = xmlDoc.ToXDocument();

            Assert.AreEqual(xdoc.ToString(), "<data></data>");
        }

        [Test]
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