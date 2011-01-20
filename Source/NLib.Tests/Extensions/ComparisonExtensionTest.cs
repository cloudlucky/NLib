namespace NLib.Tests.Extensions
{
    using System;

    using NLib.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class ComparisonExtensionTest
    {
        [Test]
        public void Test1()
        {
            Comparison<int> ec = (i1, i2) => i1 + i2;

            Assert.AreEqual(5, ec(2, 3));
        }

        [Test]
        public void Test2()
        {
            Comparison<int> ec = (i1, i2) => i1 + i2;
            var t = ec.ToComparer();

            Assert.AreEqual(3, t.Compare(1, 2));
        }
    }
}
