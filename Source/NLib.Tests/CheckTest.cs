namespace NLib.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class CheckTest
    {
        [Test]
        public void Requires1()
        {
            Check.Requires<ArgumentException>(true);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires2()
        {
            Check.Requires<ArgumentException>(false);
        }

        [Test]
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

        [Test]
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

        [Test]
        [ExpectedException(typeof(MissingConstructorException))]
        public void Requires5()
        {
            Check.Requires<Test2Exception>(false, "A message");
        }

        [Test]
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

        [Test]
        [ExpectedException(typeof(MissingConstructorException))]
        public void Requires7()
        {
            Check.Requires<TestException>(false, new { message = "error", foo = "bar", test = "test" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires8()
        {
            Check.Requires(false, new ArgumentException());
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Requires9()
        {
            Check.Requires<ArgumentException>(false);
        }

        [Test]
        public void Requires10()
        {
            Check.Requires<ArgumentException>(true);
        }

        [Test]
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
