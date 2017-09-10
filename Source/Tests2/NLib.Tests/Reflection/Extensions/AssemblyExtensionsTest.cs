namespace NLib.Tests.Reflection.Extensions
{
    using System.Reflection;

    using NLib.Reflection.Extensions;

    using Xunit;

    public class AssemblyExtensionsTest
    {
        [Fact]
        public void GetManifestResourceStringTest1()
        {
            var s = Assembly.GetExecutingAssembly().GetManifestResourceString("NLib.Tests.Reflection.Extensions.TextFile1.txt");

            Assert.Equal(s, "This is an embedded resource.");
        }
    }
}
