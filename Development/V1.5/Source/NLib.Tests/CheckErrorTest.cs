namespace NLib.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class CheckErrorTest
    {
        [Test]
        public void ArgumentException1()
        {
            var foo = 3;
            CheckError.ArgumentException(foo > 0, "foo");
            Assert.Pass();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentException2()
        {
            var foo = -1;
            CheckError.ArgumentException(foo > 0, "foo");
            Assert.Fail();
        }

        [Test]
        public void ArgumentException3()
        {
            var foo = 3;
            CheckError.ArgumentException(foo > 0, "foo", "foo is negative");
            Assert.Pass();
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentException4()
        {
            var foo = -1;
            CheckError.ArgumentException(foo > 0, "foo", "foo is negative");
            Assert.Fail();
        }

        [Test]
        public void ArgumentException5()
        {
            var foo = -1;
            try
            {
                CheckError.ArgumentException(foo > 0, "foo", "foo is negative");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("foo is negative\r\nParameter name: foo", ex.Message);
            }
        }

        [Test]
        public void ArgumentNullException1()
        {
            CheckError.ArgumentNullException(string.Empty, "foo");
            Assert.Pass();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException2()
        {
            CheckError.ArgumentNullException(null, "foo");
            Assert.Fail();
        }

        [Test]
        public void ArgumentNullException3()
        {
            CheckError.ArgumentNullException(string.Empty, "foo", "foo is null");
            Assert.Pass();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException4()
        {
            CheckError.ArgumentNullException(null, "foo", "foo is null");
            Assert.Fail();
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
