namespace NLib.Tests.Text.Extensions
{
    using System;
    using System.Globalization;
    using System.Text;

    using NLib.Text.Extensions;

    using Xunit;

    public class StringBuilderExtensionsTest
    {
        [Fact]
        public void AppendLineFormat1()
        {
            var sb = new StringBuilder();
            sb.AppendLineFormat("{0} {1}", "foo", "bar");

            Assert.Equal(string.Format("foo bar{0}", Environment.NewLine), sb.ToString());
        }

        [Fact]
        public void AppendLineFormat2()
        {
            var sb = new StringBuilder();
            sb.AppendLineFormat(CultureInfo.CurrentCulture, "{0} {1}", "foo", "bar");

            Assert.Equal(string.Format("foo bar{0}", Environment.NewLine), sb.ToString());
        }
    }
}
