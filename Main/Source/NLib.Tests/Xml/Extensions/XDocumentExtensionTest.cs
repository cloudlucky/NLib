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
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<data></data>");
                XDocument xdoc = xmlDoc.ToXDocument();
                Assert.AreEqual(xdoc.ToString(), "<data></data>");
           }

           [Test]
           public void ToXdocumentTest2()
           {
               XmlDocument doc = new XmlDocument();
               doc.LoadXml("<parent><child>text1</child></parent>");
               XDocument xdoc = doc.ToXDocument();
               var children = xdoc.Document.Element("parent").Elements("child");
               foreach (var child in children)
               {
                   Assert.AreEqual(child.Value,"text"); 
               }
           }
           

        
    }


}