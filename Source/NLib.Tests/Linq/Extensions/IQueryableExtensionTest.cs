namespace NLib.Tests.Linq.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib.Collections.Generic;
    using NLib.Linq.Extensions;

    [TestClass]
    public class IQueryableExtensionTest
    {
        [TestMethod]
        public void BetweenTest1()
        {
            var c = Generator.Generate<int>(10, x => ++x).AsQueryable();
            var c2 = c.Between(5, 8);

            CollectionAssert.AreEqual(new[] { 5, 6, 7, 8 }, c2.ToList());
        }

        [TestMethod]
        public void BetweenTest2()
        {
            var m1 = new MyClass { P1 = 1, P2 = "Foo" + 1 };
            var m2 = new MyClass { P1 = 2, P2 = "Foo" + 2 };
            var m3 = new MyClass { P1 = 3, P2 = "Foo" + 3 };
            var m4 = new MyClass { P1 = 4, P2 = "Foo" + 4 };
            var m5 = new MyClass { P1 = 5, P2 = "Foo" + 5 };
            var m6 = new MyClass { P1 = 6, P2 = "Foo" + 6 };
            var m7 = new MyClass { P1 = 7, P2 = "Foo" + 7 };
            var m8 = new MyClass { P1 = 8, P2 = "Foo" + 8 };
            var m9 = new MyClass { P1 = 9, P2 = "Foo" + 9 };

            var c = new List<MyClass> { m1, m2, m3, m4, m5, m6, m7, m8, m9 }.AsQueryable();

            var c2 = c.Between(x => x.P1, 5, 8);

            CollectionAssert.AreEqual(new[] { m5, m6, m7, m8 }, c2.ToList());
        }

        [TestMethod]
        public void PaginateTest1()
        {
            var c = Generator.Generate<int>(10, x => ++x).AsQueryable();
            var c2 = c.Paginate(1, 3);

            CollectionAssert.AreEqual(new[] { 4, 5, 6 }, c2.ToList());
        }

        public class MyClass
        {
            public int P1 { get; set; }
            public string P2 { get; set; }
        }
    }
}
