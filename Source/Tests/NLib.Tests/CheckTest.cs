namespace NLib.Tests
{
    using System;

    using Xunit;

    public class CheckTest
    {
        [Fact]
        public void ArgumentException1()
        {
            var foo = 3;
            Check.Current.ArgumentException(foo > 0, "foo");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentException2()
        {
            var foo = -1;
            Assert.Throws<ArgumentException>(() => Check.Current.ArgumentException(foo > 0, "foo"));
        }

        [Fact]
        public void ArgumentException3()
        {
            var foo = 3;
            Check.Current.ArgumentException(foo > 0, "foo", "foo is negative");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentException4()
        {
            var foo = -1;
            Assert.Throws<ArgumentException>(() => Check.Current.ArgumentException(foo > 0, "foo", "foo is negative"));
        }

        [Fact]
        public void ArgumentException5()
        {
            var foo = -1;
            var ex = Assert.Throws<ArgumentException>(() => Check.Current.ArgumentException(foo > 0, "foo", "foo is negative"));
            Assert.Equal("foo is negative\r\nParameter name: foo", ex.Message);
        }

        [Fact]
        public void ArgumentNullException1()
        {
            Check.Current.ArgumentNullException(string.Empty, "foo");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullException2()
        {
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullException((string)null, "foo"));
        }

        [Fact]
        public void ArgumentNullException3()
        {
            Check.Current.ArgumentNullException(string.Empty, "foo", "foo is null");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullException4()
        {
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullException(null, "foo", "foo is null"));
        }

        [Fact]
        public void ArgumentNullException5()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullException(null, "foo", "foo is null"));
            Assert.Equal("foo is null\r\nParameter name: foo", ex.Message);
        }

        [Fact]
        public void ArgumentNullException6()
        {
            var foo = string.Empty;
            Check.Current.ArgumentNullException(() => foo);
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullException7()
        {
            string foo = null;
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullException(() => foo));
        }

        [Fact]
        public void ArgumentNullException8()
        {
            var foo = string.Empty;
            Check.Current.ArgumentNullException(() => foo, "foo is null");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullException9()
        {
            string foo = null;
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullException(() => foo, "foo is null"));
        }

        [Fact]
        public void ArgumentNullException10()
        {
            string foo = null;
            var ex = Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullException(() => foo, "foo is null"));
            Assert.Equal("foo is null\r\nParameter name: foo", ex.Message);
        }

        [Fact]
        public void ArgumentNullOrEmptyException1()
        {
            Check.Current.ArgumentNullOrEmptyException("Test", "foo");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullOrEmptyException2()
        {
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrEmptyException((string)null, "foo"));
        }

        [Fact]
        public void ArgumentNullOrEmptyException3()
        {
            Check.Current.ArgumentNullOrEmptyException("Test", "foo", "foo is null or empty");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullOrEmptyException4()
        {
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrEmptyException(string.Empty, "foo", "foo is null or empty"));
        }

        [Fact]
        public void ArgumentNullOrEmptyException5()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrEmptyException(null, "foo", "foo is null"));
            Assert.Equal("foo is null\r\nParameter name: foo", ex.Message);
        }

        [Fact]
        public void ArgumentNullOrEmptyException6()
        {
            var foo = "Test";
            Check.Current.ArgumentNullOrEmptyException(() => foo);
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullOrEmptyException7()
        {
            string foo = null;
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrEmptyException(() => foo));
        }

        [Fact]
        public void ArgumentNullOrEmptyException8()
        {
            var foo = "Test";
            Check.Current.ArgumentNullOrEmptyException(() => foo, "foo is null or empty");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullOrEmptyException9()
        {
            var foo = string.Empty;
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrEmptyException(() => foo, "foo is null or empty"));
        }

        [Fact]
        public void ArgumentNullOrEmptyException10()
        {
            string foo = null;
            var ex = Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrEmptyException(() => foo, "foo is null"));
            Assert.Equal("foo is null\r\nParameter name: foo", ex.Message);
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException1()
        {
            Check.Current.ArgumentNullOrWhiteSpaceException("Test", "foo");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException2()
        {
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException((string)null, "foo"));
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException3()
        {
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException("    ", "foo"));
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException4()
        {
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException(string.Empty, "foo"));
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException5()
        {
            Check.Current.ArgumentNullOrWhiteSpaceException("Test", "foo", "foo is null or empty");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException6()
        {
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException(string.Empty, "foo", "foo is null or empty"));
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException7()
        {
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException("     ", "foo", "foo is null or empty"));
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException8()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException(null, "foo", "foo is null"));
            Assert.Equal("foo is null\r\nParameter name: foo", ex.Message);
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException9()
        {
            var foo = "Test";
            Check.Current.ArgumentNullOrWhiteSpaceException(() => foo);
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException10()
        {
            string foo = null;
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException(() => foo));
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException11()
        {
            var foo = "     ";
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException(() => foo));
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException12()
        {
            var foo = string.Empty;
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException(() => foo));
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException13()
        {
            var foo = "Test";
            Check.Current.ArgumentNullOrWhiteSpaceException(() => foo, "foo is null or empty");
            Assert.True(true);
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException14()
        {
            var foo = string.Empty;
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException(() => foo, "foo is null or empty"));
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException15()
        {
            var foo = "     ";
            Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException(() => foo, "foo is null or empty"));
        }

        [Fact]
        public void ArgumentNullOrWhiteSpaceException16()
        {
            string foo = null;
            var ex = Assert.Throws<ArgumentNullException>(() => Check.Current.ArgumentNullOrWhiteSpaceException(() => foo, "foo is null"));
            Assert.Equal("foo is null\r\nParameter name: foo", ex.Message);
        }

        [Fact]
        public void NotNull1()
        {
            Check.Current.NotNull(string.Empty, "foo");
            Assert.True(true);
        }

        [Fact]
        public void NotNull2()
        {
            Assert.Throws<NullReferenceException>(() => Check.Current.NotNull(null, "foo"));
        }

        [Fact]
        public void NotNull3()
        {
            Check.Current.NotNull(string.Empty, "foo", "foo is null");
            Assert.True(true);
        }

        [Fact]
        public void NotNull4()
        {
            Assert.Throws<NullReferenceException>(() => Check.Current.NotNull(null, "foo", "foo is null"));
        }

        [Fact]
        public void NotNull5()
        {
            var ex = Assert.Throws<NullReferenceException>(() => Check.Current.NotNull(null, "foo", "foo is null"));
            Assert.Equal("'foo' is null. foo is null", ex.Message);
        }

        [Fact]
        public void NotNull6()
        {
            var foo = string.Empty;
            Check.Current.NotNull(() => foo);
            Assert.True(true);
        }

        [Fact]
        public void NotNull7()
        {
            string foo = null;
            Assert.Throws<NullReferenceException>(() => Check.Current.NotNull(() => foo));
        }

        [Fact]
        public void NotNull8()
        {
            var foo = string.Empty;
            Check.Current.NotNull(() => foo, "foo is null");
            Assert.True(true);
        }

        [Fact]
        public void NotNull9()
        {
            string foo = null;
            Assert.Throws<NullReferenceException>(() => Check.Current.NotNull(() => foo, "foo is null"));
        }

        [Fact]
        public void NotNull10()
        {
            string foo = null;
            var ex = Assert.Throws<NullReferenceException>(() => Check.Current.NotNull(() => foo, "foo is null"));
            Assert.Equal("foo is null", ex.Message);
        }

        [Fact]
        public void Requires1()
        {
            Check.Current.Requires<ArgumentException>(true);
        }

        [Fact]
        public void Requires2()
        {
            Assert.Throws<ArgumentException>(() => Check.Current.Requires<ArgumentException>(false));
        }

        [Fact]
        public void Requires3()
        {
            var ex = Assert.Throws<ArgumentException>(() => Check.Current.Requires<ArgumentException>(false, "A message"));
            Assert.Equal("A message", ex.Message);
        }

        [Fact]
        public void Requires4()
        {
            var ex = Assert.Throws<TestException>(() => Check.Current.Requires<TestException>(false, "A message"));
            Assert.Equal("A message", ex.Message);
        }

        [Fact]
        public void Requires5()
        {
            Assert.Throws<MissingConstructorException>(() => Check.Current.Requires<Test2Exception>(false, "A message"));
        }

        [Fact]
        public void Requires6()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Check.Current.Requires<ArgumentNullException>(false, "A message", new { paramName = "1" }));
            Assert.Equal("A message\r\nParameter name: 1", ex.Message);
            Assert.Equal("1", ex.ParamName);
        }

        [Fact]
        public void Requires7()
        {
            Assert.Throws<MissingConstructorException>(() => Check.Current.Requires<TestException>(false, new { message = "error", foo = "bar", test = "test" }));
        }

        [Fact]
        public void Requires8()
        {
            Assert.Throws<ArgumentException>(() => Check.Current.Requires(false, new ArgumentException()));
        }

        [Fact]
        public void Requires9()
        {
            Assert.Throws<ArgumentException>(() => Check.Current.Requires<ArgumentException>(false));
        }

        [Fact]
        public void Requires10()
        {
            Check.Current.Requires<ArgumentException>(true);
        }

        [Fact]
        public void Requires11()
        {
            Check.Current.Requires(true, new ArgumentException());
        }

        [Fact]
        public void Requires12()
        {
            Check.Current.Requires<ArgumentException>(() => true);
        }

        [Fact]
        public void Requires13()
        {
            Assert.Throws<ArgumentException>(() => Check.Current.Requires<ArgumentException>(() => false));
        }

        [Fact]
        public void Requires14()
        {
            var ex = Assert.Throws<ArgumentException>(() => Check.Current.Requires<ArgumentException>(() => false, "A message"));
            Assert.Equal("A message", ex.Message);
        }

        [Fact]
        public void Requires15()
        {
            var ex = Assert.Throws<TestException>(() => Check.Current.Requires<TestException>(() => false, "A message"));
            Assert.Equal("A message", ex.Message);
        }

        [Fact]
        public void Requires16()
        {
            Assert.Throws<MissingConstructorException>(() => Check.Current.Requires<Test2Exception>(() => false, "A message"));
        }

        [Fact]
        public void Requires17()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => Check.Current.Requires<ArgumentNullException>(() => false, "A message", new { paramName = "1" }));
            Assert.Equal("A message\r\nParameter name: 1", ex.Message);
            Assert.Equal("1", ex.ParamName);
        }

        [Fact]
        public void Requires18()
        {
            Assert.Throws<MissingConstructorException>(() => Check.Current.Requires<TestException>(() => false, new { message = "error", foo = "bar", test = "test" }));
        }

        [Fact]
        public void Requires19()
        {
            Assert.Throws<ArgumentException>(() => Check.Current.Requires(() => false, new ArgumentException()));
        }

        [Fact]
        public void Requires20()
        {
            Assert.Throws<ArgumentException>(() => Check.Current.Requires<ArgumentException>(() => false));
        }

        [Fact]
        public void Requires21()
        {
            Check.Current.Requires<ArgumentException>(() => true);
        }

        [Fact]
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
