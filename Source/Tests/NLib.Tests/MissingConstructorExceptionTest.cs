namespace NLib.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MissingConstructorExceptionTest
    {
        [TestMethod]
        public void ConstructorTest1()
        {
            var e = new MissingConstructorException();

            Assert.AreEqual("Attempted to access a missing method.", e.Message);
        }

        [TestMethod]
        public void ConstructorTest2()
        {
            var message = "My message";
            var e = new MissingConstructorException(message);

            Assert.AreEqual(message, e.Message);
        }

        [TestMethod]
        public void ConstructorTest3()
        {
            var message = "My message";
            var className = "FooBar";
            var e = new MissingConstructorException(message, className);

            Assert.AreEqual(string.Format("{0}: {1}", className, message), e.Message);
        }
    }
}
