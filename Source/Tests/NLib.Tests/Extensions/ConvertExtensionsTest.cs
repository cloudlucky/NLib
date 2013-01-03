namespace NLib.Tests.Extensions
{
    using System.Globalization;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Extensions;

    [TestClass]
    public class ConvertExtensionsTest
    {
        private CultureInfo previousCultureInfo;
        private CultureInfo previousUiCultureInfo;

        [TestInitialize]
        public void TestInitialize()
        {
            this.previousCultureInfo = CultureInfo.CurrentCulture;
            this.previousUiCultureInfo = CultureInfo.CurrentUICulture;

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-CA");
        }

        [TestCleanup]
        public void Cleanup()
        {
            Thread.CurrentThread.CurrentCulture = this.previousCultureInfo;
            Thread.CurrentThread.CurrentUICulture = this.previousUiCultureInfo;
        }

        [TestMethod]
        public void Test1()
        {
            var s = "46";
            var i = s.ChangeType<int>();

            Assert.AreEqual(s, i.ToString());
        }

        [TestMethod]
        public void Test2()
        {
            var s = "46,22";
            var i = s.ChangeType<float>();

            Assert.AreEqual(s, i.ToString());
        }

        [TestMethod]    
        public void Test3()
        {
            var s = "46";
            var i = s.ChangeType<string>();

            Assert.AreEqual(s, i);
        }
    }
}
