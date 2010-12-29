namespace NLib.Tests.Collections.Generic.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic;
    using NLib.Collections.Generic.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class IEnumerableExtensionTest
    {
        private IEnumerable<TestBetween> testBetweenCollection;

        [SetUp]
        public void SetUp()
        {
            testBetweenCollection = new List<TestBetween>
                    {
                        new TestBetween { Id = 1, Name = "Foo", BirthDate = new DateTime(1980, 9, 12) },
                        new TestBetween { Id = 2, Name = "Bar", BirthDate = new DateTime(1980, 11, 11) },
                        new TestBetween { Id = 3, Name = "Potato", BirthDate = new DateTime(1980, 12, 24) },
                        new TestBetween { Id = 4, Name = "Foo Toto", BirthDate = new DateTime(1981, 6, 27) }
                    };
        }

        [Test]
        public void Between1()
        {
            var l = Generator.Generate<int>(20, x => ++x);

            CollectionAssert.AreEqual(new[] { 8, 9, 10, 11, 12 }, l.Between(8, 12));
            CollectionAssert.AreNotEqual(new[] { 8, 9, 10, 11, 13 }, l.Between(8, 12));
        }

        [Test]
        public void Between2()
        {
            var expected = new List<TestBetween>
                    {
                        new TestBetween { Id = 1, Name = "Foo", BirthDate = new DateTime(1980, 9, 12) },
                        new TestBetween { Id = 2, Name = "Bar", BirthDate = new DateTime(1980, 11, 11) },
                        new TestBetween { Id = 3, Name = "Potato", BirthDate = new DateTime(1980, 12, 24) }
                    };

            var actual = this.testBetweenCollection.Between(x => x.BirthDate, new DateTime(1980, 1, 1), new DateTime(1980, 12, 31)).ToList();

            CollectionAssert.AreEqual(expected, actual, new TestBetweenComparer());
        }

        private class TestBetween
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime BirthDate { get; set; }
        }

        private class TestBetweenComparer : IComparer<TestBetween>, IComparer
        {
            public int Compare(TestBetween x, TestBetween y)
            {
                if (x == null || y == null)
                {
                    return -1;
                }

                if (x.Id.CompareTo(y.Id) != 0)
                {
                    return x.Id.CompareTo(y.Id);
                }

                if (x.Name.CompareTo(y.Name) != 0)
                {
                    return x.Name.CompareTo(y.Name);
                }

                if (x.BirthDate.CompareTo(y.BirthDate) != 0)
                {
                    return x.BirthDate.CompareTo(y.BirthDate);
                }

                return 0;
            }

            public int Compare(object x, object y)
            {
                return this.Compare((TestBetween)x, (TestBetween)y);
            }
        }
    }
}
