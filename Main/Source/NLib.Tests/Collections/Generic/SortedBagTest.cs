namespace NLib.Tests.Collections.Generic
{
    using NLib.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class SortedBagTest
    {
        [Test]
        public void AddTest()
        {
            var bag = new SortedBag<int> { 1 };

            Assert.AreEqual(1, bag.Count);
            Assert.AreEqual(1, bag.GetCount(1));
        }

        [Test]
        public void AddTest2()
        {
            var bag = new SortedBag<int> {1, 2, 3};

            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(1, bag.GetCount(1));
            Assert.AreEqual(1, bag.GetCount(2));
            Assert.AreEqual(1, bag.GetCount(3));
        }

        [Test]
        public void AddTest3()
        {
            var bag = new SortedBag<int> { 1, 2, 1 };

            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(2, bag.GetCount(1));
            Assert.AreEqual(1, bag.GetCount(2));
        }

        [Test]
        public void AddTest4()
        {
            var bag = new SortedBag<int> { { 1, 2 }, 2 };

            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(2, bag.GetCount(1));
            Assert.AreEqual(1, bag.GetCount(2));
        }

        [Test]
        public void AddRangeTest1()
        {
            var bag = new SortedBag<int>();
            var l = new System.Collections.Generic.List<int> { 1, 2, 3 };
            bag.AddRange(l);

            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(1, bag.GetCount(1));
            Assert.AreEqual(1, bag.GetCount(2));
            Assert.AreEqual(1, bag.GetCount(3));
        }

        [Test]
        public void AddRangeTest2()
        {
            var bag = new SortedBag<int>();
            var l = new System.Collections.Generic.List<int> { 1, 2, 1 };
            bag.AddRange(l);

            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(2, bag.GetCount(1));
            Assert.AreEqual(1, bag.GetCount(2));
        }
    }
}
