namespace NLib.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CheckTest
    {
        [TestMethod]
        public void ArgumentException1()
        {
            var foo = 3;
            Check.ArgumentException(foo > 0, "foo");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentException2()
        {
            var foo = -1;
            Check.ArgumentException(foo > 0, "foo");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentException3()
        {
            var foo = 3;
            Check.ArgumentException(foo > 0, "foo", "foo is negative");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentException4()
        {
            var foo = -1;
            Check.ArgumentException(foo > 0, "foo", "foo is negative");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentException5()
        {
            var foo = -1;
            try
            {
                Check.ArgumentException(foo > 0, "foo", "foo is negative");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("foo is negative\r\nParameter name: foo", ex.Message);
            }
        }

        [TestMethod]
        public void ArgumentNullException1()
        {
            Check.ArgumentNullException(string.Empty, "foo");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException2()
        {
            Check.ArgumentNullException(null, "foo");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullException3()
        {
            Check.ArgumentNullException(string.Empty, "foo", "foo is null");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException4()
        {
            Check.ArgumentNullException(null, "foo", "foo is null");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullException5()
        {
            try
            {
                Check.ArgumentNullException(null, "foo", "foo is null");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foo is null\r\nParameter name: foo", ex.Message);
            }
        }

        [TestMethod]
        public void ArgumentNullOrEmptyException1()
        {
            Check.ArgumentNullOrEmptyException("Test", "foo");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrEmptyException2()
        {
            Check.ArgumentNullOrEmptyException(null, "foo");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrEmptyException3()
        {
            Check.ArgumentNullOrEmptyException("Test", "foo", "foo is null or empty");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrEmptyException4()
        {
            Check.ArgumentNullOrEmptyException(string.Empty, "foo", "foo is null or empty");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrEmptyException5()
        {
            try
            {
                Check.ArgumentNullOrEmptyException(null, "foo", "foo is null");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foo is null\r\nParameter name: foo", ex.Message);
            }
        }

        [TestMethod]
        public void ArgumentNullOrWhiteSpaceException1()
        {
            Check.ArgumentNullOrWhiteSpaceException("Test", "foo");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException2()
        {
            Check.ArgumentNullOrWhiteSpaceException(null, "foo");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException3()
        {
            Check.ArgumentNullOrWhiteSpaceException("    ", "foo");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException4()
        {
            Check.ArgumentNullOrWhiteSpaceException(string.Empty, "foo");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrWhiteSpaceException5()
        {
            Check.ArgumentNullOrWhiteSpaceException("Test", "foo", "foo is null or empty");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException6()
        {
            Check.ArgumentNullOrWhiteSpaceException(string.Empty, "foo", "foo is null or empty");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException7()
        {
            Check.ArgumentNullOrWhiteSpaceException("     ", "foo", "foo is null or empty");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrWhiteSpaceException8()
        {
            try
            {
                Check.ArgumentNullOrWhiteSpaceException(null, "foo", "foo is null");
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foo is null\r\nParameter name: foo", ex.Message);
            }
        }

        [TestMethod]
        public void Requires1()
        {
            Check.Requires<ArgumentException>(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires2()
        {
            Check.Requires<ArgumentException>(false);
        }

        [TestMethod]
        public void Requires3()
        {
            try
            {
                Check.Requires<ArgumentException>(false, "A message");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("A message", ex.Message);
            }
        }

        [TestMethod]
        public void Requires4()
        {
            try
            {
                Check.Requires<TestException>(false, "A message");
            }
            catch (TestException ex)
            {
                Assert.AreEqual("A message", ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MissingConstructorException))]
        public void Requires5()
        {
            Check.Requires<Test2Exception>(false, "A message");
        }

        [TestMethod]
        public void Requires6()
        {
            try
            {
                Check.Requires<ArgumentNullException>(false, "A message", new { paramName = "1" });
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("A message\r\nParameter name: 1", ex.Message);
                Assert.AreEqual("1", ex.ParamName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MissingConstructorException))]
        public void Requires7()
        {
            Check.Requires<TestException>(false, new { message = "error", foo = "bar", test = "test" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires8()
        {
            Check.Requires(false, new ArgumentException());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires9()
        {
            Check.Requires<ArgumentException>(false);
        }

        [TestMethod]
        public void Requires10()
        {
            Check.Requires<ArgumentException>(true);
        }

        [TestMethod]
        public void Requires11()
        {
            Check.Requires(true, new ArgumentException());
        }

        private class TestException : Exception
        {
            public TestException(string message)
                : base(message)
            {
            }
        }

        private class Test2Exception : Exception
        {
        }
    }
}
