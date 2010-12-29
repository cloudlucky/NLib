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
    }
}
