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
            Check.Current.ArgumentException(foo > 0, "foo");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentException2()
        {
            var foo = -1;
            Check.Current.ArgumentException(foo > 0, "foo");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentException3()
        {
            var foo = 3;
            Check.Current.ArgumentException(foo > 0, "foo", "foo is negative");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentException4()
        {
            var foo = -1;
            Check.Current.ArgumentException(foo > 0, "foo", "foo is negative");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentException5()
        {
            var foo = -1;
            try
            {
                Check.Current.ArgumentException(foo > 0, "foo", "foo is negative");
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("foo is negative\r\nParameter name: foo", ex.Message);
            }
        }

        [TestMethod]
        public void ArgumentNullException1()
        {
            Check.Current.ArgumentNullException(string.Empty, "foo");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException2()
        {
            Check.Current.ArgumentNullException((string)null, "foo");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullException3()
        {
            Check.Current.ArgumentNullException(string.Empty, "foo", "foo is null");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException4()
        {
            Check.Current.ArgumentNullException(null, "foo", "foo is null");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullException5()
        {
            try
            {
                Check.Current.ArgumentNullException(null, "foo", "foo is null");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foo is null\r\nParameter name: foo", ex.Message);
            }
        }

        [TestMethod]
        public void ArgumentNullException6()
        {
            var foo = string.Empty;
            Check.Current.ArgumentNullException(() => foo);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException7()
        {
            string foo = null;
            Check.Current.ArgumentNullException(() => foo);
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullException8()
        {
            var foo = string.Empty;
            Check.Current.ArgumentNullException(() => foo, "foo is null");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullException9()
        {
            string foo = null;
            Check.Current.ArgumentNullException(() => foo, "foo is null");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullException10()
        {
            try
            {
                string foo = null;
                Check.Current.ArgumentNullException(() => foo, "foo is null");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foo is null\r\nParameter name: foo", ex.Message);
            }
        }

        [TestMethod]
        public void ArgumentNullOrEmptyException1()
        {
            Check.Current.ArgumentNullOrEmptyException("Test", "foo");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrEmptyException2()
        {
            Check.Current.ArgumentNullOrEmptyException((string)null, "foo");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrEmptyException3()
        {
            Check.Current.ArgumentNullOrEmptyException("Test", "foo", "foo is null or empty");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrEmptyException4()
        {
            Check.Current.ArgumentNullOrEmptyException(string.Empty, "foo", "foo is null or empty");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrEmptyException5()
        {
            try
            {
                Check.Current.ArgumentNullOrEmptyException(null, "foo", "foo is null");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foo is null\r\nParameter name: foo", ex.Message);
            }
        }
        
        [TestMethod]
        public void ArgumentNullOrEmptyException6()
        {
            var foo = "Test";
            Check.Current.ArgumentNullOrEmptyException(() => foo);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrEmptyException7()
        {
            string foo = null;
            Check.Current.ArgumentNullOrEmptyException(() => foo);
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrEmptyException8()
        {
            var foo = "Test";
            Check.Current.ArgumentNullOrEmptyException(() => foo, "foo is null or empty");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrEmptyException9()
        {
            var foo = string.Empty;
            Check.Current.ArgumentNullOrEmptyException(() => foo, "foo is null or empty");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrEmptyException10()
        {
            try
            {
                string foo = null;
                Check.Current.ArgumentNullOrEmptyException(() => foo, "foo is null");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foo is null\r\nParameter name: foo", ex.Message);
            }
        }

        [TestMethod]
        public void ArgumentNullOrWhiteSpaceException1()
        {
            Check.Current.ArgumentNullOrWhiteSpaceException("Test", "foo");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException2()
        {
            Check.Current.ArgumentNullOrWhiteSpaceException((string)null, "foo");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException3()
        {
            Check.Current.ArgumentNullOrWhiteSpaceException("    ", "foo");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException4()
        {
            Check.Current.ArgumentNullOrWhiteSpaceException(string.Empty, "foo");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrWhiteSpaceException5()
        {
            Check.Current.ArgumentNullOrWhiteSpaceException("Test", "foo", "foo is null or empty");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException6()
        {
            Check.Current.ArgumentNullOrWhiteSpaceException(string.Empty, "foo", "foo is null or empty");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException7()
        {
            Check.Current.ArgumentNullOrWhiteSpaceException("     ", "foo", "foo is null or empty");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrWhiteSpaceException8()
        {
            try
            {
                Check.Current.ArgumentNullOrWhiteSpaceException(null, "foo", "foo is null");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foo is null\r\nParameter name: foo", ex.Message);
            }
        }

        [TestMethod]
        public void ArgumentNullOrWhiteSpaceException9()
        {
            var foo = "Test";
            Check.Current.ArgumentNullOrWhiteSpaceException(() => foo);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException10()
        {
            string foo = null;
            Check.Current.ArgumentNullOrWhiteSpaceException(() => foo);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException11()
        {
            var foo = "     ";
            Check.Current.ArgumentNullOrWhiteSpaceException(() => foo);
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException12()
        {
            var foo = string.Empty;
            Check.Current.ArgumentNullOrWhiteSpaceException(() => foo);
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrWhiteSpaceException13()
        {
            var foo = "Test";
            Check.Current.ArgumentNullOrWhiteSpaceException(() => foo, "foo is null or empty");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException14()
        {
            var foo = string.Empty;
            Check.Current.ArgumentNullOrWhiteSpaceException(() => foo, "foo is null or empty");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNullOrWhiteSpaceException15()
        {
            var foo = "     ";
            Check.Current.ArgumentNullOrWhiteSpaceException(() => foo, "foo is null or empty");
            Assert.Fail();
        }

        [TestMethod]
        public void ArgumentNullOrWhiteSpaceException16()
        {
            try
            {
                string foo = null;
                Check.Current.ArgumentNullOrWhiteSpaceException(() => foo, "foo is null");
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("foo is null\r\nParameter name: foo", ex.Message);
            }
        }

        [TestMethod]
        public void NotNull1()
        {
            Check.Current.NotNull(string.Empty, "foo");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NotNull2()
        {
            Check.Current.NotNull(null, "foo");
            Assert.Fail();
        }

        [TestMethod]
        public void NotNull3()
        {
            Check.Current.NotNull(string.Empty, "foo", "foo is null");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NotNull4()
        {
            Check.Current.NotNull(null, "foo", "foo is null");
            Assert.Fail();
        }

        [TestMethod]
        public void NotNull5()
        {
            try
            {
                Check.Current.NotNull(null, "foo", "foo is null");
                Assert.Fail();
            }
            catch (NullReferenceException ex)
            {
                Assert.AreEqual("'foo' is null. foo is null", ex.Message);
            }
        }

        [TestMethod]
        public void NotNull6()
        {
            var foo = string.Empty;
            Check.Current.NotNull(() => foo);
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NotNull7()
        {
            string foo = null;
            Check.Current.NotNull(() => foo);
            Assert.Fail();
        }

        [TestMethod]
        public void NotNull8()
        {
            var foo = string.Empty;
            Check.Current.NotNull(() => foo, "foo is null");
            Assert.IsTrue(true);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NotNull9()
        {
            string foo = null;
            Check.Current.NotNull(() => foo, "foo is null");
            Assert.Fail();
        }

        [TestMethod]
        public void NotNull10()
        {
            try
            {
                string foo = null;
                Check.Current.NotNull(() => foo, "foo is null");
                Assert.Fail();
            }
            catch (NullReferenceException ex)
            {
                Assert.AreEqual("foo is null", ex.Message);
            }
        }

        [TestMethod]
        public void Requires1()
        {
            Check.Current.Requires<ArgumentException>(true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires2()
        {
            Check.Current.Requires<ArgumentException>(false);
        }

        [TestMethod]
        public void Requires3()
        {
            try
            {
                Check.Current.Requires<ArgumentException>(false, "A message");
                Assert.Fail();
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
                Check.Current.Requires<TestException>(false, "A message");
                Assert.Fail();
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
            Check.Current.Requires<Test2Exception>(false, "A message");
        }

        [TestMethod]
        public void Requires6()
        {
            try
            {
                Check.Current.Requires<ArgumentNullException>(false, "A message", new { paramName = "1" });
                Assert.Fail();
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
            Check.Current.Requires<TestException>(false, new { message = "error", foo = "bar", test = "test" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires8()
        {
            Check.Current.Requires(false, new ArgumentException());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires9()
        {
            Check.Current.Requires<ArgumentException>(false);
        }

        [TestMethod]
        public void Requires10()
        {
            Check.Current.Requires<ArgumentException>(true);
        }

        [TestMethod]
        public void Requires11()
        {
            Check.Current.Requires(true, new ArgumentException());
        }

        [TestMethod]
        public void Requires12()
        {
            Check.Current.Requires<ArgumentException>(() => true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires13()
        {
            Check.Current.Requires<ArgumentException>(() => false);
        }

        [TestMethod]
        public void Requires14()
        {
            try
            {
                Check.Current.Requires<ArgumentException>(() => false, "A message");
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("A message", ex.Message);
            }
        }

        [TestMethod]
        public void Requires15()
        {
            try
            {
                Check.Current.Requires<TestException>(() => false, "A message");
                Assert.Fail();
            }
            catch (TestException ex)
            {
                Assert.AreEqual("A message", ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MissingConstructorException))]
        public void Requires16()
        {
            Check.Current.Requires<Test2Exception>(() => false, "A message");
        }

        [TestMethod]
        public void Requires17()
        {
            try
            {
                Check.Current.Requires<ArgumentNullException>(() => false, "A message", new { paramName = "1" });
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("A message\r\nParameter name: 1", ex.Message);
                Assert.AreEqual("1", ex.ParamName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MissingConstructorException))]
        public void Requires18()
        {
            Check.Current.Requires<TestException>(() => false, new { message = "error", foo = "bar", test = "test" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires19()
        {
            Check.Current.Requires(() => false, new ArgumentException());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires20()
        {
            Check.Current.Requires<ArgumentException>(() => false);
        }

        [TestMethod]
        public void Requires21()
        {
            Check.Current.Requires<ArgumentException>(() => true);
        }

        [TestMethod]
        public void Requires22()
        {
            Check.Current.Requires(() => true, new ArgumentException());
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
