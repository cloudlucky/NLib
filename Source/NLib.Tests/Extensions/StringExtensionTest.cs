namespace NLib.Tests.Extensions
{
    using System;

    using NLib.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class StringExtensionTest
    {
        [Test]
        [SetCulture("fr-CA")]
        public void ContainsTest1()
        {
            var s = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
            var b = s.Contains("dolor sit", StringComparison.CurrentCultureIgnoreCase);

            Assert.True(b);
        }

        [Test]
        [SetCulture("fr-CA")]
        public void ContainsTest2()
        {
            var s = "Lorem ipsum DolOr sIt amet, consectetur adipiscing elit";
            var b = s.Contains("dolor sit", StringComparison.CurrentCultureIgnoreCase);

            Assert.True(b);
        }

        [Test]
        [SetCulture("fr-CA")]
        public void ContainsTest3()
        {
            var s = "Lorem ipsum DolOr sIt amet, consectetur adipiscing elit";
            var b = s.Contains("dolor sit", StringComparison.CurrentCulture);

            Assert.False(b);
        }

        [Test]
        [SetCulture("fr-CA")]
        public void ContainsTest4()
        {
            var s = "Lorem ipsum DolOr sIt amet, consectetur adipiscing elit";
            var b = s.Contains(string.Empty, StringComparison.CurrentCulture);

            Assert.True(b);
        }
    }
}
