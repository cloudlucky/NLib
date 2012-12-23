namespace NLib.Tests.Reflection.Extensions
{
    using System.Reflection;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Reflection.Extensions;

    [TestClass]
    public class AssemblyExtensionTest
    {
        [TestMethod]
        public void GetManifestResourceStringTest1()
        {
            var s = Assembly.GetExecutingAssembly().GetManifestResourceString("NLib.Tests.Reflection.Extensions.TextFile1.txt");

            Assert.AreEqual(s, "This is an embedded resource.");
        }
    }
}
