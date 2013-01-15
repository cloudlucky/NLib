namespace NLib.Tests
{
    using Xunit;

    public class MissingConstructorExceptionTest
    {
        [Fact]
        public void ConstructorTest1()
        {
            var e = new MissingConstructorException();

            Assert.Equal("Attempted to access a missing method.", e.Message);
        }

        [Fact]
        public void ConstructorTest2()
        {
            var message = "My message";
            var e = new MissingConstructorException(message);

            Assert.Equal(message, e.Message);
        }

        [Fact]
        public void ConstructorTest3()
        {
            var message = "My message";
            var className = "FooBar";
            var e = new MissingConstructorException(message, className);

            Assert.Equal(string.Format("{0}: {1}", className, message), e.Message);
        }
    }
}
