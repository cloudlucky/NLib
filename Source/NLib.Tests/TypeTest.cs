namespace NLib.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class TypeTest
    {
        [Test]
        public void Type1()
        {
            var i = 1;

            var t = new Type<int>(i);

            Assert.AreEqual(i, t.Value);
            Assert.AreEqual(typeof(int), t.CurrentType);
        }

        [Test]
        public void Type2()
        {
            var o = new { P1 = 1, P2 = "Foo" };

            var t = new Type<object>(o);

            Assert.AreEqual(o, t.Value);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Type3()
        {
            var t = new Type<object>(null);

            Assert.Fail("Expect exception");
        }
    }
}
