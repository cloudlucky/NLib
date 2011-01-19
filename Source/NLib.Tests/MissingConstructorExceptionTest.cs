namespace NLib.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class MissingConstructorExceptionTest
    {
        [Test]
        public void ConstructorTest1()
        {
            var e = new MissingConstructorException();

            Assert.AreEqual("Attempted to access a missing method.", e.Message);
        }

        [Test]
        public void ConstructorTest2()
        {
            var message = "My message";
            var e = new MissingConstructorException(message);

            Assert.AreEqual(message, e.Message);
        }

        [Test]
        public void ConstructorTest3()
        {
            var message = "My message";
            var className = "FooBar";
            var e = new MissingConstructorException(message, className);

            Assert.AreEqual(string.Format("{0}: {1}", className, message), e.Message);
        }
    }
}
