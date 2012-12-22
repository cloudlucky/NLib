namespace NLib.Tests.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class GeneratorTest
    {
        [Test]
        public void GenerateTest1()
        {
            var collection = Generator.Generate<int>(10, x => ++x);

            CollectionAssert.AreEqual(Enumerable.Range(1, 10), collection);
        }

        [Test]
        public void GenerateTest2()
        {
            var collection = Generator.Generate<int>(0, x => ++x);

            CollectionAssert.AreEqual(Enumerable.Range(0, 0), collection);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateTest3()
        {
            var collection = Generator.Generate<int>(-1, x => ++x);

            Assert.True(collection.Any());
        }

        [Test]
        public void GenerateTest4()
        {
            var collection = Generator.Generate<int>(10, x => --x);
            Assert.True(collection.Any());
            Assert.AreEqual(10, collection.Count());
        }

        [Test]
        public void GenerateTest5()
        {
            var collection = Generator.Generate<double>(5, x => 1.0 / ++x);

            CollectionAssert.Contains(collection, 1.0 / 1.0);
            CollectionAssert.Contains(collection, 1.0 / 2.0);
            CollectionAssert.DoesNotContain(collection, 1.0 / 6.0);
        }


        [Test]
        public void GenerateTest6()
        {
            var collection = Generator.Generate(10, x => ++x, 1);

            CollectionAssert.AreEqual(Enumerable.Range(2, 10), collection);
        }

        [Test]
        public void GenerateTest7()
        {
            var collection = Generator.Generate(0, x => ++x, 1);

            CollectionAssert.AreEqual(Enumerable.Range(0, 0), collection);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GenerateTest8()
        {
            var collection = Generator.Generate(-1, x => ++x, 1);

            Assert.True(collection.Any());
        }

        [Test]
        public void GenerateTest9()
        {
            var collection = Generator.Generate(10, x => --x, 1);
            Assert.True(collection.Any());
            Assert.AreEqual(10, collection.Count());
        }

        [Test]
        public void GenerateTest10()
        {
            var collection = Generator.Generate(5, x => 1.0 / ++x, 1.0);

            CollectionAssert.Contains(collection, 1.0 / 2.0);
            CollectionAssert.Contains(collection, 2.0 / 3.0);
            CollectionAssert.DoesNotContain(collection, 1.0 / 6.0);
        }
    }
}
