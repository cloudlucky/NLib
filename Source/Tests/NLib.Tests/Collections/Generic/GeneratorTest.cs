namespace NLib.Tests.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NLib.Collections.Generic;

    using Xunit;

    public class GeneratorTest
    {
        [Fact]
        public void GenerateTest1()
        {
            var collection = Generator.Generate<int>(10, x => ++x);

            Assert.Equal(Enumerable.Range(1, 10).ToList(), collection.ToList());
        }

        [Fact]
        public void GenerateTest2()
        {
            var collection = Generator.Generate<int>(0, x => ++x);

            Assert.Equal(Enumerable.Range(0, 0).ToList(), collection.ToList());
        }

        [Fact]
        public void GenerateTest3()
        {
            var collection = Generator.Generate<int>(-1, x => ++x);

            Assert.Throws<ArgumentException>(() => collection.Any());
        }

        [Fact]
        public void GenerateTest4()
        {
            var collection = Generator.Generate<int>(10, x => --x);
            Assert.True(collection.Any());
            Assert.Equal(10, collection.Count());
        }

        [Fact]
        public void GenerateTest5()
        {
            var collection = Generator.Generate<double>(5, x => 1.0 / ++x).ToList();

            Assert.Contains(1.0 / 1.0, collection);
            Assert.Contains(1.0 / 2.0, collection);
            Assert.DoesNotContain(1.0 / 6.0, collection);
        }


        [Fact]
        public void GenerateTest6()
        {
            var collection = Generator.Generate(10, x => ++x, 1);

            Assert.Equal(Enumerable.Range(2, 10).ToList(), collection.ToList());
        }

        [Fact]
        public void GenerateTest7()
        {
            var collection = Generator.Generate(0, x => ++x, 1);

            Assert.Equal(Enumerable.Range(0, 0).ToList(), collection.ToList());
        }

        [Fact]
        public void GenerateTest8()
        {
            var collection = Generator.Generate(-1, x => ++x, 1);
            Assert.Throws<ArgumentException>(() => collection.Any());
        }

        [Fact]
        public void GenerateTest9()
        {
            var collection = Generator.Generate(10, x => --x, 1).ToArray();
            Assert.True(collection.Any());
            Assert.Equal(10, collection.Count());
        }

        [Fact]
        public void GenerateTest10()
        {
            var collection = Generator.Generate(5, x => 1.0 / ++x, 1.0).ToList();

            Assert.Contains(1.0 / 2.0, collection);
            Assert.Contains(2.0 / 3.0, collection);
            Assert.DoesNotContain(1.0 / 6.0, collection);
        }
    }
}
