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
                XDocument xdoc = new XDocument();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml("<data></data>");
                xdoc = xmlDoc.ToXDocument();

                Assert.AreEqual(xdoc.ToString(), "<data></data>");
           }

        
    }


}