namespace NLib.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EventArgsTest
    {
        [TestMethod]
        public void CtorTest1()
        {
            var e = new EventArgs<int>(3);
            Assert.AreEqual(3, e.Value);
        }
    }
}
