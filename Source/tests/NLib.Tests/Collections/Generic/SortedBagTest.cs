namespace NLib.Tests.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic;

    using Xunit;

    public class SortedBagTest
    {
        [Fact]
        public void CtorTest1()
        {
            var bag = new SortedBag<int> { 1, 2, 3, 4, 5, 6 };

            Assert.Equal(new[] { 1, 2, 3, 4, 5, 6 }, bag);
        }

        [Fact]
        public void CtorTest2()
        {
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };
            var bag = new SortedBag<int>(l);

            Assert.Equal(new[] { 1, 2, 3, 4, 5, 6 }, bag);
        }

        [Fact]
        public void CtorTest3()
        {
            var bag = new SortedBag<int>((x, y) => y.CompareTo(x)) { 1, 2, 3, 4, 5, 6 };

            Assert.Equal(new[] { 6, 5, 4, 3, 2, 1 }, bag);
        }

        [Fact]
        public void CtorTest5()
        {
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };
            var bag = new SortedBag<int>(l, (x, y) => y.CompareTo(x));

            Assert.Equal(new[] { 6, 5, 4, 3, 2, 1 }, bag);
        }

        [Fact]
        public void UniqueSetTest()
        {
            var bag = new SortedBag<int> { 1, 2, 3, 4, 5, 6 };
            var set = bag.UniqueSet;

            Assert.Equal(bag.OrderBy(x => x), set);
        }

        [Fact]
        public void UniqueSetTest2()
        {
            var bag = new SortedBag<int> { 1, 2, 2, 4, 5, 5, 3 };
            var set = bag.UniqueSet;

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, set);
        }

        [Fact]
        public void AddTest()
        {
            var bag = new SortedBag<int> { 1 };

            Assert.Equal(1, bag.Count);
            Assert.Equal(1, bag.GetCount(1));
        }

        [Fact]
        public void AddTest2()
        {
            var bag = new SortedBag<int> { 1, 2, 3 };

            Assert.Equal(3, bag.Count);
            Assert.Equal(1, bag.GetCount(1));
            Assert.Equal(1, bag.GetCount(2));
            Assert.Equal(1, bag.GetCount(3));
        }

        [Fact]
        public void AddTest3()
        {
            var bag = new SortedBag<int> { 1, 2, 1 };

            Assert.Equal(3, bag.Count);
            Assert.Equal(2, bag.GetCount(1));
            Assert.Equal(1, bag.GetCount(2));
        }

        [Fact]
        public void AddTest4()
        {
            var bag = new SortedBag<int> { { 1, 2 }, 2 };

            Assert.Equal(3, bag.Count);
            Assert.Equal(2, bag.GetCount(1));
            Assert.Equal(1, bag.GetCount(2));
        }

        [Fact]
        public void AddRangeTest1()
        {
            var bag = new SortedBag<int>();
            var l = new List<int> { 1, 2, 3 };
            bag.AddRange(l);

            Assert.Equal(3, bag.Count);
            Assert.Equal(1, bag.GetCount(1));
            Assert.Equal(1, bag.GetCount(2));
            Assert.Equal(1, bag.GetCount(3));
        }

        [Fact]
        public void AddRangeTest2()
        {
            var bag = new SortedBag<int>();
            var l = new List<int> { 1, 2, 1 };
            bag.AddRange(l);

            Assert.Equal(3, bag.Count);
            Assert.Equal(2, bag.GetCount(1));
            Assert.Equal(1, bag.GetCount(2));
        }

        [Fact]
        public void BagEqualsTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 2, 5, 3, 1, 6, 4 };

            Assert.True(bag.BagEquals(l));
        }

        [Fact]
        public void BagEqualsTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 2, 5, 3, 1, 6, 4, 6, 4, 2 };

            Assert.False(bag.BagEquals(l));
        }

        [Fact]
        public void BagEqualsTest3()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };

            Assert.False(bag.BagEquals(null));
        }

        [Fact]
        public void ClearTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            bag.Clear();

            Assert.Equal(0, bag.Count);
        }

        [Fact]
        public void ClearTest2()
        {
            var bag = new SortedBag<int>();
            bag.Clear();

            Assert.Equal(0, bag.Count);
        }

        [Fact]
        public void ContainsTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };

            Assert.True(bag.Contains(1));
            Assert.False(bag.Contains(10));
        }

        [Fact]
        public void CopyToTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[6];

            bag.CopyTo(array, 0);

            Assert.Equal(new[] { 1, 2, 3, 4, 5, 6 }, array);
        }

        [Fact]
        public void CopyToTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };

            Assert.Throws<ArgumentNullException>(() => bag.CopyTo(null, 0));
        }

        [Fact]
        public void CopyToTest3()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[6];

            Assert.Throws<ArgumentException>(() => bag.CopyTo(array, 2));
        }

        [Fact]
        public void CopyToTest4()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[4];

            Assert.Throws<ArgumentException>(() => bag.CopyTo(array, 0));
        }

        [Fact]
        public void CopyToTest5()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[6];

            Assert.Throws<ArgumentOutOfRangeException>(() => bag.CopyTo(array, -1));
        }

        [Fact]
        public void CopyToTest6()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var array = new int[6];

            Assert.Throws<ArgumentException>(() => bag.CopyTo(array, 18));
        }

        [Fact]
        public void ExceptAllWithTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 3, 5 };

            bag.ExceptAllWith(l);

            Assert.Equal(new[] { 2, 4, 6 }, bag);
        }

        [Fact]
        public void ExceptAllWithTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2, 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 3, 5 };

            bag.ExceptAllWith(l);

            Assert.Equal(new[] { 2, 2, 4, 4, 6, 6 }, bag);
        }

        [Fact]
        public void ExceptWithTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 3, 5 };

            bag.ExceptWith(l);

            Assert.Equal(new[] { 2, 4, 6 }, bag);
        }

        [Fact]
        public void ExceptWithTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2, 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 3, 5 };

            bag.ExceptWith(l);

            Assert.Equal(new[] { 1, 2, 2, 3, 4, 4, 5, 6, 6 }, bag);
        }

        [Fact]
        public void GetCountTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2, 3, 5, 1, 4, 6, 2, 7 };

            Assert.Equal(1, bag.GetCount(7));
            Assert.Equal(2, bag.GetCount(1));
        }

        [Fact]
        public void IntersectWithTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2, 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 7, 8, 9 };

            bag.IntersectWith(l);

            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, bag);
        }

        [Fact]
        public void IntersectWithTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2, 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 1, 2, 3, 3, 4, 5, 7, 8, 9 };

            bag.IntersectWith(l);

            Assert.Equal(new[] { 1, 1, 2, 3, 3, 4, 5 }, bag);
        }

        [Fact]
        public void IsProperSubBagOfTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            Assert.True(bag.IsProperSubBagOf(l));
        }

        [Fact]
        public void IsProperSubBagOfTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 5, 6 };

            Assert.True(bag.IsProperSubBagOf(l));
        }

        [Fact]
        public void IsProperSubBagOfTest3()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3 };

            Assert.False(bag.IsProperSubBagOf(l));
        }

        [Fact]
        public void IsProperSubBagOfTest4()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 9 };

            Assert.False(bag.IsProperSubBagOf(l));
        }

        [Fact]
        public void IsProperSuperBagOfTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            Assert.True(bag.IsProperSuperBagOf(l));
        }

        [Fact]
        public void IsProperSuperBagOfTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 5, 6 };

            Assert.False(bag.IsProperSuperBagOf(l));
        }

        [Fact]
        public void IsProperSuperBagOfTest3()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3 };

            Assert.True(bag.IsProperSuperBagOf(l));
        }

        [Fact]
        public void IsProperSuperBagOfTest4()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 9 };

            Assert.False(bag.IsProperSuperBagOf(l));
        }

        [Fact]
        public void IsSubBagOfTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            Assert.True(bag.IsSubBagOf(l));
        }

        [Fact]
        public void IsSubBagOfTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 5, 6 };

            Assert.True(bag.IsSubBagOf(l));
        }

        [Fact]
        public void IsSubBagOfTest3()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3 };

            Assert.True(bag.IsSubBagOf(l));
        }

        [Fact]
        public void IsSubBagOfTest4()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 9 };

            Assert.False(bag.IsSubBagOf(l));
        }

        [Fact]
        public void IsSuperBagOfTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            Assert.True(bag.IsSuperBagOf(l));
        }

        [Fact]
        public void IsSuperBagOfTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 5, 6 };

            Assert.False(bag.IsSuperBagOf(l));
        }

        [Fact]
        public void IsSuperBagOfTest3()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3 };

            Assert.True(bag.IsSuperBagOf(l));
        }

        [Fact]
        public void IsSuperBagOfTest4()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 9 };

            Assert.False(bag.IsSuperBagOf(l));
        }

        [Fact]
        public void OverlapsTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 9 };

            Assert.False(bag.Overlaps(l));
        }

        [Fact]
        public void OverlapsTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3 };

            Assert.True(bag.Overlaps(l));
        }

        [Fact]
        public void OverlapsTest3()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            Assert.True(bag.Overlaps(l));
        }

        [Fact]
        public void RemoveTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 4, 6, 2 };

            Assert.Equal(6, bag.Count);
            Assert.True(bag.Remove(1));
            Assert.Equal(5, bag.Count);
            Assert.False(bag.Remove(10));
            Assert.Equal(5, bag.Count);
        }

        [Fact]
        public void RemoveTest2()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 1, 4, 6, 2, 2, 2 };

            Assert.Equal(9, bag.Count);

            Assert.Equal(2, bag.Remove(1, 2));
            Assert.Equal(7, bag.Count);

            Assert.Equal(0, bag.Remove(10, 3));
            Assert.Equal(7, bag.Count);

            Assert.Equal(2, bag.Remove(2, 2));
            Assert.Equal(5, bag.Count);

            Assert.Equal(1, bag.Remove(5, 1));
            Assert.Equal(4, bag.Count);

            Assert.Equal(1, bag.Remove(2, 5));
            Assert.Equal(3, bag.Count);
        }

        [Fact]
        public void RemoveAllTest1()
        {
            var bag = new SortedBag<int> { 3, 5, 1, 1, 4, 6, 2, 2, 2 };

            Assert.Equal(9, bag.Count);

            Assert.Equal(2, bag.RemoveAll(1));
            Assert.Equal(7, bag.Count);

            Assert.Equal(0, bag.RemoveAll(10));
            Assert.Equal(7, bag.Count);

            Assert.Equal(3, bag.RemoveAll(2));
            Assert.Equal(4, bag.Count);

            Assert.Equal(1, bag.RemoveAll(5));
            Assert.Equal(3, bag.Count);
        }

        [Fact]
        public void SymmetricExceptWithTest1()
        {
            var bag = new SortedBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 1, 2, 3, 4 };

            bag.SymmetricExceptWith(l);

            Assert.Equal(0, bag.Count);
        }

        [Fact]
        public void SymmetricExceptWithTest2()
        {
            var bag = new SortedBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            bag.SymmetricExceptWith(l);

            Assert.Equal(2, bag.Count);
            Assert.Equal(new[] { 5, 6 }, bag);
        }

        [Fact]
        public void SymmetricExceptWithTest3()
        {
            var bag = new SortedBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 3, 4, 5, 6 };

            bag.SymmetricExceptWith(l);

            Assert.Equal(4, bag.Count);
            Assert.Equal(new[] { 1, 2, 5, 6 }, bag);
        }

        [Fact]
        public void UnionWithTest1()
        {
            var bag = new SortedBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 1, 2, 3, 4 };

            bag.UnionWith(l);

            Assert.Equal(8, bag.Count);
            Assert.Equal(new[] { 1, 1, 2, 2, 3, 3, 4, 4 }, bag.ToList());
        }

        [Fact]
        public void UnionWithTest2()
        {
            var bag = new SortedBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 1, 2, 3, 4, 5, 6 };

            bag.UnionWith(l);

            Assert.Equal(10, bag.Count);
            Assert.Equal(new[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 6 }, bag.ToList());
        }

        [Fact]
        public void UnionWithTest3()
        {
            var bag = new SortedBag<int> { 1, 2, 3, 4 };
            var l = new List<int> { 5, 6 };

            bag.UnionWith(l);

            Assert.Equal(6, bag.Count);
            Assert.Equal(new[] { 1, 2, 3, 4, 5, 6 }, bag.ToList());
        }

        [Fact]
        public void IsReadOnlyTest1()
        {
            var bag = new SortedBag<int> { 1, 2, 3, 4 };

            Assert.False((bag as ICollection<int>).IsReadOnly);
        }
    }
}
