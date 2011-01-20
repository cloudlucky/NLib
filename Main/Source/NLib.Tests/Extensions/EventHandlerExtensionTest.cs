namespace NLib.Tests.Extensions
{
    using System;

    using NLib;
    using NLib.Extensions;

    using NUnit.Framework;

    [TestFixture]
    public class EventHandlerExtensionTest
    {
        public event EventHandler Event1;
        public event EventHandler<EventArgs<int>> Event2;

        [TestFixtureSetUp]
        public void SetUp()
        {
            this.Event1 = null;
            this.Event2 = null;
        }

        [Test]
        public void RaiseEventTest1()
        {
            this.Event1.RaiseEvent(this);

            Assert.Pass();
        }

        [Test]
        public void RaiseEventTest2()
        {
            var i = 1;
            this.Event1 += (sender, e) => i++;

            this.Event1.RaiseEvent(this);

            Assert.AreEqual(2, i);
        }

        [Test]
        public void RaiseEventTest3()
        {
            var i = 1;
            this.Event1 += (sender, e) => i++;

            this.Event1.RaiseEvent(this, EventArgs.Empty);

            Assert.AreEqual(2, i);
        }

        [Test]
        public void RaiseEventTest4()
        {
            var i = 1;
            this.Event2 += (sender, e) => i += e.Value;

            this.Event2.RaiseEvent(this, new EventArgs<int>(2));

            Assert.AreEqual(3, i);
        }
    }
}
