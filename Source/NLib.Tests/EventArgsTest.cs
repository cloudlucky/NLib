namespace NLib.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class EventArgsTest
    {
        [Test]
        public void CtorTest1()
        {
            var e = new EventArgs<int>(3);
            Assert.AreEqual(3, e.Value);
        }
    }
}
