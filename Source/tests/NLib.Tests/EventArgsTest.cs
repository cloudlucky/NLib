namespace NLib.Tests
{
    using Xunit;

    public class EventArgsTest
    {
        [Fact]
        public void CtorTest1()
        {
            var e = new EventArgs<int>(3);
            Assert.Equal(3, e.Value);
        }
    }
}
