namespace NLib.Tests.Extensions
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NLib;
    using NLib.Extensions;

    [TestClass]
    public class EventHandlerExtensionsTest
    {
        public event EventHandler Event1;
        public event EventHandler<EventArgs<int>> Event2;

        [TestInitialize]
        public void TestInitialize()
        {
            this.Event1 = null;
            this.Event2 = null;
        }

        [TestMethod]
        public void RaiseEventTest1()
        {
            this.Event1.RaiseEvent(this);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RaiseEventTest2()
        {
            var i = 1;
            this.Event1 += (sender, e) => i++;

            this.Event1.RaiseEvent(this);

            Assert.AreEqual(2, i);
        }

        [TestMethod]
        public void RaiseEventTest3()
        {
            var i = 1;
            this.Event1 += (sender, e) => i++;

            this.Event1.RaiseEvent(this, EventArgs.Empty);

            Assert.AreEqual(2, i);
        }

        [TestMethod]
        public void RaiseEventTest4()
        {
            var i = 1;
            this.Event2 += (sender, e) => i += e.Value;

            this.Event2.RaiseEvent(this, new EventArgs<int>(2));

            Assert.AreEqual(3, i);
        }

        [TestMethod]
        public void RaiseEventTest5()
        {
            var i = 1;
            this.Event1 += (sender, e) => i--;
            this.Event2 += (sender, e) => i += e.Value;
            this.Event1.RaiseEvent(this);
            this.Event2.RaiseEvent(this, new EventArgs<int>(2));

            Assert.AreEqual(2,i);
        }
    }
}
