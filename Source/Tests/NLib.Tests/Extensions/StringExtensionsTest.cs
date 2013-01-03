namespace NLib.Tests.Extensions
{
    using System;
    using System.Globalization;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Extensions;

    [TestClass]
    public class StringExtensionsTest
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
        public void ContainsTest1()
        {
            var s = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            var b = s.Contains("dolor sit", StringComparison.CurrentCultureIgnoreCase);

            Assert.IsTrue(b);
        }

        [TestMethod]
        public void ContainsTest2()
        {
            var s = "Lorem ipsum DolOr sIt amet, consectetur adipiscing elit";
            var b = s.Contains("dolor sit", StringComparison.CurrentCultureIgnoreCase);

            Assert.IsTrue(b);
        }

        [TestMethod]
        public void ContainsTest3()
        {
            var s = "Lorem ipsum DolOr sIt amet, consectetur adipiscing elit";
            var b = s.Contains("dolor sit", StringComparison.CurrentCulture);

            Assert.IsFalse(b);
        }

        [TestMethod]
        public void ContainsTest4()
        {
            var s = "Lorem ipsum DolOr sIt amet, consectetur adipiscing elit";
            var b = s.Contains(string.Empty, StringComparison.CurrentCulture);

            Assert.IsTrue(b);
        }

        [TestMethod]
        public void ContainsTest5()
        {
            var s = string.Empty;
            var b = s.Contains(string.Empty, StringComparison.CurrentCulture);
            Assert.IsTrue(b);
        }
    }
}
