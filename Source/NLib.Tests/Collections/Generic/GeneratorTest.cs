namespace NLib.Tests.Collections.Generic
{
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
        public void GenerateTest3()
        {
            var collection = Generator.Generate<int>(-1, x => ++x);

            Assert.True(collection.Any());
            Assert.AreEqual(1, collection.Count());
        }        

        [Test]
        public void GenerateTest4()
        {
            var collection = Generator.Generate<int>(10, x => --x );
            Assert.True(collection.Any());
            Assert.AreEqual(10, collection.Count());
        }
    }
}
