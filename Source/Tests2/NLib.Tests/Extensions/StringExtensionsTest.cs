namespace NLib.Tests.Extensions
{
    using System;
    using System.Globalization;
    using System.Threading;

    using NLib.Extensions;

    using Xunit;

    public class StringExtensionsTest : IDisposable
    {
        private CultureInfo previousCultureInfo;
        private CultureInfo previousUiCultureInfo;

        public StringExtensionsTest()
        {
            this.previousCultureInfo = CultureInfo.CurrentCulture;
            this.previousUiCultureInfo = CultureInfo.CurrentUICulture;

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-CA");
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = this.previousCultureInfo;
            Thread.CurrentThread.CurrentUICulture = this.previousUiCultureInfo;
        }

        [Fact]
        public void ContainsTest1()
        {
            var s = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            var b = s.Contains("dolor sit", StringComparison.CurrentCultureIgnoreCase);

            Assert.True(b);
        }

        [Fact]
        public void ContainsTest2()
        {
            var s = "Lorem ipsum DolOr sIt amet, consectetur adipiscing elit";
            var b = s.Contains("dolor sit", StringComparison.CurrentCultureIgnoreCase);

            Assert.True(b);
        }

        [Fact]
        public void ContainsTest3()
        {
            var s = "Lorem ipsum DolOr sIt amet, consectetur adipiscing elit";
            var b = s.Contains("dolor sit", StringComparison.CurrentCulture);

            Assert.False(b);
        }

        [Fact]
        public void ContainsTest4()
        {
            var s = "Lorem ipsum DolOr sIt amet, consectetur adipiscing elit";
            var b = s.Contains(string.Empty, StringComparison.CurrentCulture);

            Assert.True(b);
        }

        [Fact]
        public void ContainsTest5()
        {
            var s = string.Empty;
            var b = s.Contains(string.Empty, StringComparison.CurrentCulture);
            Assert.True(b);
        }
    }
}
