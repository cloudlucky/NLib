namespace NLib.Tests.Extensions
{
    using System;
    using System.Globalization;
    using System.Threading;

    using NLib.Extensions;

    using Xunit;

    public class ConvertExtensionsTest : IDisposable
    {
        private CultureInfo previousCultureInfo;
        private CultureInfo previousUiCultureInfo;

        public ConvertExtensionsTest()
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
        public void Test1()
        {
            var s = "46";
            var i = s.ChangeType<int>();

            Assert.Equal(s, i.ToString());
        }

        [Fact]
        public void Test2()
        {
            var s = "46,22";
            var i = s.ChangeType<float>();

            Assert.Equal(s, i.ToString());
        }

        [Fact]    
        public void Test3()
        {
            var s = "46";
            var i = s.ChangeType<string>();

            Assert.Equal(s, i);
        }
    }
}
