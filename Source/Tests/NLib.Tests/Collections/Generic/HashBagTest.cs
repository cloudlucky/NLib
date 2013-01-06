namespace NLib.Tests.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Collections.Generic;

    [TestClass]
    public class HashBagTest
    {
        [TestMethod]
        public void CtorTest1()
        {
            var bag = new HashBag<int> { 1, 2, 3, 4, 5, 6 };

            CollectionAssert.AreEquivalent(new[] { 1, 2, 3, 4, 5, 6 }, bag.ToList());
        }

        [TestMethod]
        public void CtorTest2()
        {
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };
            var bag = new HashBag<int>(l);

            CollectionAssert.AreEquivalent(new[] { 1, 2, 3, 4, 5, 6 }, bag.ToList());
        }

        [TestMethod]
        public void CtorTest3()
        {
            var bag = new HashBag<int>((x, y) => y.CompareTo(x) == 0) { 1, 2, 3, 4, 5, 6 };

            CollectionAssert.AreEquivalent(new[] { 6, 5, 4, 3, 2, 1 }, bag.ToList());
        }

        [TestMethod]
        public void CtorTest5()
        {
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };
            var bag = new HashBag<int>(l, (x, y) => y.CompareTo(x) == 0);

            CollectionAssert.AreEquivalent(new[] { 6, 5, 4, 3, 2, 1 }, bag.ToList());
        }

        [TestMethod]
        public void UniqueSetTest()
        {
            var bag = new HashBag<int> { 1, 2, 3, 4, 5, 6 };
            var set = bag.UniqueSet;

            CollectionAssert.AreEqual(bag.ToList(), set.ToList());
        }

        [TestMethod]
        public void UniqueSetTest2()
        {
            var bag = new HashBag<int> { 1, 2, 2, 4, 5, 5, 3 };
            var set = bag.UniqueSet;

            CollectionAssert.AreEqual(new[] { 1, 2, 4, 5, 3 }, set.ToList());
        }

        [TestMethod]
        public void AddTest()
        {
            var bag = new HashBag<int> { 1 };

            Assert.AreEqual(1, bag.Count);
            Assert.AreEqual(1, bag.GetCount(1));
        }

        [TestMethod]
        public void AddTest2()
        {
            var bag = new HashBag<int> { 1, 2, 3 };

            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(1, bag.GetCount(1));
            Assert.AreEqual(1, bag.GetCount(2));
            Assert.AreEqual(1, bag.GetCount(3));
        }

        [TestMethod]
        public void AddTest3()
        {
            var bag = new HashBag<int> { 1, 2, 1 };

            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(2, bag.GetCount(1));
            Assert.AreEqual(1, bag.GetCount(2));
        }

        [TestMethod]
        public void AddTest4()
        {
            var bag = new HashBag<int> { { 1, 2 }, 2 };

            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(2, bag.GetCount(1));
            Assert.AreEqual(1, bag.GetCount(2));
        }

        [TestMethod]
        public void AddRangeTest1()
        {
            var bag = new HashBag<int>();
            var l = new System.Collections.Generic.List<int> { 1, 2, 3 };
            bag.AddRange(l);

            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(1, bag.GetCount(1));
            Assert.AreEqual(1, bag.GetCount(2));
            Assert.AreEqual(1, bag.GetCount(3));
        }

        [TestMethod]
        public void AddRangeTest2()
        {
            var bag = new HashBag<int>();
            var l = new System.Collections.Generic.List<int> { 1, 2, 1 };
            bag.AddRange(l);

            Assert.AreEqual(3, bag.Count);
            Assert.AreEqual(2, bag.GetCount(1));
            Assert.AreEqual(1, bag.GetCount(2));
        }

        [TestMethod]
        public void BagEqualsTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 2, 5, 3, 1, 6, 4 };

            Assert.IsTrue(bag.BagEquals(l));
        }

        [TestMethod]
        public void BagEqualsTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 2, 5, 3, 1, 6, 4, 6, 4, 2 };

            Assert.IsFalse(bag.BagEquals(l));
        }

        [TestMethod]
        public void BagEqualsTest3()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };

            Assert.IsFalse(bag.BagEquals(null));
        }

        [TestMethod]
        public void ClearTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            bag.Clear();

            Assert.AreEqual(0, bag.Count);
        }

        [TestMethod]
        public void ClearTest2()
        {
            var bag = new HashBag<int>();
            bag.Clear();

            Assert.AreEqual(0, bag.Count);
        }

        [TestMethod]
        public void ContainsTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };

            Assert.IsTrue(bag.Contains(1));
            Assert.IsFalse(bag.Contains(10));
        }

        [TestMethod]
        public void CopyToTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[6];

            bag.CopyTo(array, 0);

            CollectionAssert.AreEqual(new[] { 3, 5, 1, 4, 6, 2 }, array);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyToTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[6];

            bag.CopyTo(null, 0);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyToTest3()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[6];

            bag.CopyTo(array, 2);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyToTest4()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[4];

            bag.CopyTo(array, 0);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyToTest5()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[6];

            bag.CopyTo(array, -1);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyToTest6()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[6];

            bag.CopyTo(array, 18);

            Assert.Fail();
        }

        [TestMethod]
        public void ExceptAllWithTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 3, 5 };

            bag.ExceptAllWith(l);

            CollectionAssert.AreEquivalent(new[] { 2, 4, 6 }, bag.ToList());
        }

        [TestMethod]
        public void ExceptAllWithTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2, 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 3, 5 };

            bag.ExceptAllWith(l);

            CollectionAssert.AreEquivalent(new[] { 2, 2, 4, 4, 6, 6 }, bag.ToList());
        }

        [TestMethod]
        public void ExceptWithTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 3, 5 };

            bag.ExceptWith(l);

            CollectionAssert.AreEquivalent(new[] { 2, 4, 6 }, bag.ToList());
        }

        [TestMethod]
        public void ExceptWithTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2, 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 3, 5 };

            bag.ExceptWith(l);

            CollectionAssert.AreEquivalent(new[] { 1, 2, 2, 3, 4, 4, 5, 6, 6 }, bag.ToList());
        }

        [TestMethod]
        public void GetCountTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2, 3, 5, 1, 4, 6, 2, 7 };

            Assert.AreEqual(1, bag.GetCount(7));
            Assert.AreEqual(2, bag.GetCount(1));
        }

        [TestMethod]
        public void IntersectWithTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2, 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 7, 8, 9 };

            bag.IntersectWith(l);

            CollectionAssert.AreEquivalent(new[] { 1, 2, 3, 4, 5 }, bag.ToList());
        }

        [TestMethod]
        public void IntersectWithTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2, 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 1, 2, 3, 3, 4, 5, 7, 8, 9 };

            bag.IntersectWith(l);

            CollectionAssert.AreEquivalent(new[] { 1, 1, 2, 3, 3, 4, 5 }, bag.ToList());
        }

        [TestMethod]
        public void IsProperSubBagOfTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            Assert.IsTrue(bag.IsProperSubBagOf(l));
        }

        [TestMethod]
        public void IsProperSubBagOfTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 5, 6 };

            Assert.IsTrue(bag.IsProperSubBagOf(l));
        }

        [TestMethod]
        public void IsProperSubBagOfTest3()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3 };

            Assert.IsFalse(bag.IsProperSubBagOf(l));
        }

        [TestMethod]
        public void IsProperSubBagOfTest4()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 9 };

            Assert.IsFalse(bag.IsProperSubBagOf(l));
        }

        [TestMethod]
        public void IsProperSuperBagOfTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            Assert.IsTrue(bag.IsProperSuperBagOf(l));
        }

        [TestMethod]
        public void IsProperSuperBagOfTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 5, 6 };

            Assert.IsFalse(bag.IsProperSuperBagOf(l));
        }

        [TestMethod]
        public void IsProperSuperBagOfTest3()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3 };

            Assert.IsTrue(bag.IsProperSuperBagOf(l));
        }

        [TestMethod]
        public void IsProperSuperBagOfTest4()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 9 };

            Assert.IsFalse(bag.IsProperSuperBagOf(l));
        }

        [TestMethod]
        public void IsSubBagOfTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            Assert.IsTrue(bag.IsSubBagOf(l));
        }

        [TestMethod]
        public void IsSubBagOfTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 5, 6 };

            Assert.IsTrue(bag.IsSubBagOf(l));
        }

        [TestMethod]
        public void IsSubBagOfTest3()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3 };

            Assert.IsTrue(bag.IsSubBagOf(l));
        }

        [TestMethod]
        public void IsSubBagOfTest4()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 9 };

            Assert.IsFalse(bag.IsSubBagOf(l));
        }

        [TestMethod]
        public void IsSuperBagOfTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            Assert.IsTrue(bag.IsSuperBagOf(l));
        }

        [TestMethod]
        public void IsSuperBagOfTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 5, 6 };

            Assert.IsFalse(bag.IsSuperBagOf(l));
        }

        [TestMethod]
        public void IsSuperBagOfTest3()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3 };

            Assert.IsTrue(bag.IsSuperBagOf(l));
        }

        [TestMethod]
        public void IsSuperBagOfTest4()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 9 };

            Assert.IsFalse(bag.IsSuperBagOf(l));
        }

        [TestMethod]
        public void OverlapsTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 9 };

            Assert.IsFalse(bag.Overlaps(l));
        }

        [TestMethod]
        public void OverlapsTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3 };

            Assert.IsTrue(bag.Overlaps(l));
        }

        [TestMethod]
        public void OverlapsTest3()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            Assert.IsTrue(bag.Overlaps(l));
        }

        [TestMethod]
        public void RemoveTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 4, 6, 2 };

            Assert.AreEqual(6, bag.Count);
            Assert.IsTrue(bag.Remove(1));
            Assert.AreEqual(5, bag.Count);
            Assert.IsFalse(bag.Remove(10));
            Assert.AreEqual(5, bag.Count);
        }

        [TestMethod]
        public void RemoveTest2()
        {
            var bag = new HashBag<int> { 3, 5, 1, 1, 4, 6, 2, 2, 2 };

            Assert.AreEqual(9, bag.Count);

            Assert.AreEqual(2, bag.Remove(1, 2));
            Assert.AreEqual(7, bag.Count);

            Assert.AreEqual(0, bag.Remove(10, 3));
            Assert.AreEqual(7, bag.Count);

            Assert.AreEqual(2, bag.Remove(2, 2));
            Assert.AreEqual(5, bag.Count);

            Assert.AreEqual(1, bag.Remove(5, 1));
            Assert.AreEqual(4, bag.Count);

            Assert.AreEqual(1, bag.Remove(2, 5));
            Assert.AreEqual(3, bag.Count);
        }

        [TestMethod]
        public void RemoveAllTest1()
        {
            var bag = new HashBag<int> { 3, 5, 1, 1, 4, 6, 2, 2, 2 };

            Assert.AreEqual(9, bag.Count);

            Assert.AreEqual(2, bag.RemoveAll(1));
            Assert.AreEqual(7, bag.Count);

            Assert.AreEqual(0, bag.RemoveAll(10));
            Assert.AreEqual(7, bag.Count);

            Assert.AreEqual(3, bag.RemoveAll(2));
            Assert.AreEqual(4, bag.Count);

            Assert.AreEqual(1, bag.RemoveAll(5));
            Assert.AreEqual(3, bag.Count);
        }

        [TestMethod]
        public void SymmetricExceptWithTest1()
        {
            var bag = new HashBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 1, 2, 3, 4 };

            bag.SymmetricExceptWith(l);

            Assert.AreEqual(0, bag.Count);
        }

        [TestMethod]
        public void SymmetricExceptWithTest2()
        {
            var bag = new HashBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            bag.SymmetricExceptWith(l);

            Assert.AreEqual(2, bag.Count);
            CollectionAssert.AreEquivalent(new[] { 5, 6 }, bag.ToList());
        }

        [TestMethod]
        public void SymmetricExceptWithTest3()
        {
            var bag = new HashBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 3, 4, 5, 6 };

            bag.SymmetricExceptWith(l);

            Assert.AreEqual(4, bag.Count);
            CollectionAssert.AreEquivalent(new[] { 1, 2, 5, 6 }, bag.ToList());
        }

        [TestMethod]
        public void UnionWithTest1()
        {
            var bag = new HashBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 1, 2, 3, 4 };

            bag.UnionWith(l);

            Assert.AreEqual(8, bag.Count);
            CollectionAssert.AreEqual(new[] { 1, 1, 2, 2, 3, 3, 4, 4 }, bag.ToList());
        }

        [TestMethod]
        public void UnionWithTest2()
        {
            var bag = new HashBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            bag.UnionWith(l);

            Assert.AreEqual(10, bag.Count);
            CollectionAssert.AreEqual(new[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 6 }, bag.ToList());
        }

        [TestMethod]
        public void UnionWithTest3()
        {
            var bag = new HashBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 5, 6 };

            bag.UnionWith(l);

            Assert.AreEqual(6, bag.Count);
            CollectionAssert.AreEqual(new[] { 1, 2, 3, 4, 5, 6 }, bag.ToList());
        }

        [TestMethod]
        public void IsReadOnlyTest1()
        {
            var bag = new HashBag<int> { 1, 2, 3, 4 };

            Assert.IsFalse((bag as ICollection<int>).IsReadOnly);
        }
    }
}
