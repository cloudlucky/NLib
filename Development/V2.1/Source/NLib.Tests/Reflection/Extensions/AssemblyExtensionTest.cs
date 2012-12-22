namespace NLib.Tests.Reflection.Extensions
{
    using System.Reflection;

    using NLib.Reflection.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class AssemblyExtensionTest
    {
        [Test]
        public void GetManifestResourceStringTest1()
        {
            var s = Assembly.GetExecutingAssembly().GetManifestResourceString("NLib.Tests.Reflection.Extensions.TextFile1.txt");

            Assert.AreEqual(s, "This is an embedded resource.");
        }
    }
}
