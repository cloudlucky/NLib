namespace NLib.Tests.Collections.Generic.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic;
    using NLib.Collections.Generic.Extensions;

    using Xunit;

    public class EnumerableExtensionsTest
    {
        private IEnumerable<TestBetween> testBetweenCollection;

        public EnumerableExtensionsTest()
        {
            this.testBetweenCollection = new List<TestBetween>
                    {
                        new TestBetween { Id = 1, Name = "Foo", BirthDate = new DateTime(1980, 9, 12) },
                        new TestBetween { Id = 2, Name = "Bar", BirthDate = new DateTime(1980, 11, 11) },
                        new TestBetween { Id = 3, Name = "Potato", BirthDate = new DateTime(1980, 12, 24) },
                        new TestBetween { Id = 4, Name = "Foo Toto", BirthDate = new DateTime(1981, 6, 27) }
                    };
        }

        [Fact]
        public void Between1()
        {
            var l = Generator.Generate<int>(20, x => ++x);

            Assert.Equal(new[] { 8, 9, 10, 11, 12 }, l.Between(8, 12).ToList());
            Assert.NotEqual(new[] { 8, 9, 10, 11, 13 }, l.Between(8, 12).ToList());
        }

        [Fact]
        public void Between2()
        {
            var expected = new List<TestBetween>
                    {
                        new TestBetween { Id = 1, Name = "Foo", BirthDate = new DateTime(1980, 9, 12) },
                        new TestBetween { Id = 2, Name = "Bar", BirthDate = new DateTime(1980, 11, 11) },
                        new TestBetween { Id = 3, Name = "Potato", BirthDate = new DateTime(1980, 12, 24) }
                    };

            var actual = this.testBetweenCollection.Between(x => x.BirthDate, new DateTime(1980, 1, 1), new DateTime(1980, 12, 31)).ToList();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ForEach1()
        {
            var l = Generator.Generate<int>(20, x => ++x);
            var i = 0;

            l.ForEach(x => Assert.Equal(x, ++i));
        }

        [Fact]
        public void ForEach2()
        {
            var l = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var i = 0;

            l.ForEach(x => Assert.Equal(x, ++i));
        }

        [Fact]
        public void Paginate1()
        {
            var l = Generator.Generate<int>(20, x => ++x).ToList();

            var p1 = l.Paginate(0, 5);
            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, p1.OrderBy(x => x));

            var p2 = l.Paginate(1, 5);
            Assert.Equal(new[] { 6, 7, 8, 9, 10 }, p2.OrderBy(x => x));

            var p3 = l.Paginate(2, 5);
            Assert.Equal(new[] { 11, 12, 13, 14, 15 }, p3.OrderBy(x => x));
        }

        private class TestBetween
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime BirthDate { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }

                var tb = obj as TestBetween;
                if (tb == null)
                { 
                    return false;
                }

                return this.Id == tb.Id
                    && this.Name == tb.Name
                    && this.BirthDate == tb.BirthDate;
            }

            public override int GetHashCode()
            {
                return this.Id.GetHashCode();
            }
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
