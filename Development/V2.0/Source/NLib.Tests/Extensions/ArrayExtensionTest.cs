namespace NLib.Tests.Extensions
{
    using NLib.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class ArrayExtensionTest
    {
        [Test]
        public void SwapValues1()
        {
            var l = new[] { 1, 2, 3, 4, 5, 6 };
            l.SwapValues(2, 3);

            CollectionAssert.AreEquivalent(l, new[] { 1, 2, 4, 3, 5, 6 });
        }

        [Test]
        public void SwapValues2()
        {
            var l = new[] {1, 2, 3};
            l.SwapValues(0, 2);
            l.SwapValues(2, 0);
            l.SwapValues(1, 1);

            CollectionAssert.AreEquivalent(l, new[] {1,2,3});

        }

    }
}
