namespace NLib.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class CheckErrorTest
    {
        [Test]
        public void ArgumentNullException1()
        {
            CheckError.ArgumentNullException(string.Empty, "foo");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException2()
        {
            CheckError.ArgumentNullException(null, "foo");
        }

        [Test]
        public void ArgumentNullException3()
        {
            CheckError.ArgumentNullException(string.Empty, "foo", "foo is null");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException4()
        {
            CheckError.ArgumentNullException(null, "foo", "foo is null");
        }

        [Test]
        public void ArgumentNullException5()
        {
            try
            {
                CheckError.ArgumentNullException(null, "foo", "foo is null");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foo is null\r\nParameter name: foo", ex.Message);
            }
        }
    }
}
