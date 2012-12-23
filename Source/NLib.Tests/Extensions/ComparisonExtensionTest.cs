namespace NLib.Tests.Extensions
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Extensions;

    [TestClass]
    public class ComparisonExtensionTest
    {
        [TestMethod]
        public void Test1()
        {
            Comparison<int> ec = (i1, i2) => i1 + i2;

            Assert.AreEqual(5, ec(2, 3));
        }

        [TestMethod]
        public void Test2()
        {
            Comparison<int> ec = (i1, i2) => i1 + i2;
            var t = ec.ToComparer();

            Assert.AreEqual(3, t.Compare(1, 2));
        }

        [TestMethod]
        public void Test3()
        {
            Comparison<int> ec = (i1, i2) => i1 - i2;
            var t = ec.ToComparer();

            Assert.AreEqual(-1, t.Compare(0, 1));

        }
    }
}
