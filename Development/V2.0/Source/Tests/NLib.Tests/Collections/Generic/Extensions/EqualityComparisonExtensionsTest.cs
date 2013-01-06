namespace NLib.Tests.Collections.Generic.Extensions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Collections.Generic.Extensions;

    [TestClass]
    public class EqualityComparisonExtensionsTest
    {
        [TestMethod]
        public void Test1()
        {
            EqualityComparison<int> ec = (i1, i2) => i1 == i2;

            Assert.IsTrue(ec(2, 2));
        }

        [TestMethod]
        public void Test2()
        {
            EqualityComparison<int> ec = (i1, i2) => i1 == i2;
            var t = ec.ToEqualityComparer();

            Assert.IsTrue(t.Equals(1, 1));
            Assert.AreEqual(1, t.GetHashCode(1));
        }
    }
}
